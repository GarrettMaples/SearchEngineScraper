namespace SearchEngineScraper.Service.Scraping.Url.Google
{
    /// <summary>
    /// Request object for making a Google URL scrape request
    /// </summary>
    /// <param name="Search">The keyword(s) to search for</param>
    /// <param name="Url">The URL to search for</param>
    /// <param name="ResultCount">The number of results to search through</param>
    public record GoogleUrlScrapeRequest(string Search, string Url, int ResultCount);
}