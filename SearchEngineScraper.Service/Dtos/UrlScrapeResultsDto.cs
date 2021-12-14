using System.Collections.Generic;

namespace SearchEngineScraper.Service.Dtos
{
    public record UrlScrapeResultsDto(string Search, string Url, int ResultCount,
        IEnumerable<KeyValuePair<int, string>> Results);
}