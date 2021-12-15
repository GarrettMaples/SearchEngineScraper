using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SearchEngineScraper.Api.Models.Requests;
using SearchEngineScraper.Api.Models.Results;
using SearchEngineScraper.Service.Scraping.Url;
using SearchEngineScraper.Service.Scraping.Url.Google;
using System.Threading.Tasks;

namespace SearchEngineScraper.Api.Controllers
{
    [Route("api/google-scrape")]
    [ApiController]
    public class GoogleScraperController : ControllerBase
    {
        private readonly IUrlScraper<GoogleUrlScrapeRequest> _urlScraper;
        private readonly IMapper _mapper;

        public GoogleScraperController(IUrlScraper<GoogleUrlScrapeRequest> urlScraper, IMapper mapper)
        {
            _urlScraper = urlScraper;
            _mapper = mapper;
        }

        /// <summary>
        /// Endpoint for scraping Google search results for a URL found based on a user provided search
        /// </summary>
        /// <param name="urlScrapeRequestModel">Contains parameters for the Google results URL scrape</param>
        /// <returns>Returns collection of Scrape results that include the index and exact URL found</returns>
        [HttpGet("url-scrape")]
        public async Task<ActionResult<UrlScrapeResultsModel>> GetUrlScrapeResults(
            [FromQuery] UrlScrapeRequestModel urlScrapeRequestModel)
        {
            var request = new GoogleUrlScrapeRequest(urlScrapeRequestModel.Search, urlScrapeRequestModel.Url,
                urlScrapeRequestModel.ResultCount);
            var results = await _urlScraper.GetUrlScrapeResults(request);
            
            var modelResults = _mapper.Map<UrlScrapeResultsModel>(results);
            return Ok(modelResults);
        }
    }
}