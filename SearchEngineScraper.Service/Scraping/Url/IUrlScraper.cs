using SearchEngineScraper.Service.Dtos;
using System.Threading.Tasks;

namespace SearchEngineScraper.Service.Scraping.Url
{
    /// <summary>
    /// Interface for scraping a webpage for a specific URL based on a search argument
    /// </summary>
    public interface IUrlScraper<in TRequest>
    {
        /// <summary>
        /// Scrape a webpage for a specific URL based on the search argument
        /// </summary>
        /// <param name="request">Request object containing the arguments for the URL scrape</param>
        /// <returns>Returns collection of indexes and URLs found</returns>
        Task<UrlScrapeResultsDto> GetUrlScrapeResults(TRequest request);
    }
}