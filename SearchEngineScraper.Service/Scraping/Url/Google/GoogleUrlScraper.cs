using SearchEngineScraper.Data.Clients;
using SearchEngineScraper.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchEngineScraper.Service.Scraping.Url.Google
{
    /// <summary>
    /// URL Scraper implementation for Google
    /// </summary>
    internal class GoogleUrlScraper : IUrlScraper
    {
        private const int MaxDisplayCount = 100;
        private const string GroupName = "url";

        private static readonly Regex _urlRegex =
            new($@"<div[^>]+?class\s*?=\s*?(?:""yuRUbf""|""DOqJne"")[^>]*?>.*?<a[^>]+?href\s*?=\s*?""(?<{GroupName}>.+?)""",
                RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(15));

        private readonly IGoogleClient _googleClient;

        public GoogleUrlScraper(IGoogleClient googleClient)
        {
            _googleClient = googleClient;
        }

        public async Task<UrlScrapeResultsDto> GetUrlScrapeResults(string search, string url, int resultCount)
        {
            // Pass 0 as the resultStart argument because we want to start with the first result returned
            var results = GetUrlScrapeResults(search, url, resultCount, resultStart: 0);
            return new UrlScrapeResultsDto(search, url, resultCount, await results.ToListAsync());
        }

        private async IAsyncEnumerable<KeyValuePair<int, string>> GetUrlScrapeResults(string search, string url,
            int resultCount, int resultStart)
        {
            var htmlResults =
                await _googleClient.GetSearchResults(search, Math.Min(resultCount, MaxDisplayCount), resultStart);

            // Throw exception here because an empty result is not typical and could signal that the
            // search endpoint has changed on Google's side.
            if (string.IsNullOrWhiteSpace(htmlResults))
            {
                throw new GoogleUrlScrapeException(
                    "Empty response from Google search HTTP request. Google search endpoint may have changed.");
            }

            var matchResults = _urlRegex.Matches(htmlResults);
            
            if (matchResults.Count == 0)
            {
                yield break;
            }
            
            for (var i = 0; i < matchResults.Count; i++)
            {
                var matchUrl = matchResults[i].Groups[GroupName].Value;

                if (matchUrl.Contains(url, StringComparison.OrdinalIgnoreCase))
                {
                    yield return new KeyValuePair<int, string>(i + 1, matchUrl);
                }
            }

            // Exit because the requested result count is less than the max display count for Google (100)
            // and the number of results matches the requested result count
            // or there are less possible results than the requested result count
            if (resultCount <= MaxDisplayCount && matchResults.Count <= resultCount)
            {
                yield break;
            }

            var nextPageResultCount = resultCount - MaxDisplayCount;
            var nextPageResultStart = resultStart + MaxDisplayCount;

            var nextPageResults = GetUrlScrapeResults(search, url, nextPageResultCount, nextPageResultStart);

            await foreach (var nextPageResult in nextPageResults)
            {
                yield return nextPageResult;
            }
        }
    }
}