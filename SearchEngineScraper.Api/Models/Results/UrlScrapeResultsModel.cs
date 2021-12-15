using System.Collections.Generic;

namespace SearchEngineScraper.Api.Models.Results
{
    public class UrlScrapeResultsModel
    {
        // The keyword(s) used to search Google.
        public string Search { get; init; }

        // The URL that was searched for in the Google results
        public string Url { get; init; }

        //The number of results that were searched through
        public int ResultCount { get; init; }

        // Collection of Scrape results that include the index and exact URL found
        public IEnumerable<UrlScrapeResultModel> Results { get; init; }
    }

    /// <summary>
    /// Scrape Result
    /// </summary>
    /// <param name="Index">The position that the scrape result was found (starting at 1)</param>
    /// <param name="Url">The exact URL found</param>
    public record UrlScrapeResultModel(int Index, string Url);
}