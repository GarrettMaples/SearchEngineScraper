using SearchEngineScraper.Service.Dtos;
using System.Threading.Tasks;

namespace SearchEngineScraper.Service.Scraping.Url
{
    /// <summary>
    /// Interface for scraping a webpage for a specific URL based on a search argument
    /// </summary>
    public interface IUrlScraper
    {
        /// <summary>
        /// Scrape a webpage for a specific URL based on the search argument
        /// </summary>
        /// <param name="search">The keyword(s) to search for</param>
        /// <param name="url">The URL to search for</param>
        /// <param name="resultCount">The number of results to return</param>
        /// <returns>Returns collection of indexes and URLs found</returns>
        Task<UrlScrapeResultsDto> GetUrlScrapeResults(string search, string url, int resultCount);
    }
}