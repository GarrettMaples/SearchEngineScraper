using System.Collections.Generic;

namespace SearchEngineScraper.Api.Models.Results
{
    public class UrlScrapeResultsModel
    {
        public string Search { get; init; }
        public string Url { get; init; }
        public int ResultCount { get; init; }
        public IEnumerable<UrlScrapeResultModel> Results { get; init; }
    }

    public record UrlScrapeResultModel(int Index, string Value);
}