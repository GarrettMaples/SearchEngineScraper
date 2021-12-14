using Refit;
using System.Threading.Tasks;

namespace SearchEngineScraper.Data.Clients
{
    public interface IGoogleClient
    {
        /// <summary>
        /// Google search engine endpoint
        /// </summary>
        /// <param name="search">The keyword(s) to search for</param>
        /// <param name="resultCount">The number of results to return</param>
        /// <param name="resultStart">The number of results to skip (For pagination)</param>
        /// <returns>Returns search results in HTML</returns>
        [Get("/search?q={search}&num={resultCount}&start={resultStart}")]
        Task<string> GetSearchResults(string search, int resultCount, int resultStart);
    }
}