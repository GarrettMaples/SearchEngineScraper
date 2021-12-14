using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SearchEngineScraper.Api.Models.Requests;
using SearchEngineScraper.Api.Models.Results;
using SearchEngineScraper.Service.Scraping.Url;
using System.Threading.Tasks;

namespace SearchEngineScraper.Api.Controllers
{
    [ApiController]
    [Route("api/google-scraper")]
    public class GoogleScraperController : ControllerBase
    {
        private readonly IUrlScraper _urlScraper;
        private readonly IMapper _mapper;
        
        public GoogleScraperController(IUrlScraper urlScraper, IMapper mapper)
        {
            _urlScraper = urlScraper;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlScrapeRequestModel"></param>
        /// <returns></returns>
        [HttpPost("/url-search")]
        public async Task<ActionResult<UrlScrapeResultsModel>> GetUrlScrapeResults(
            [FromBody] UrlScrapeRequestModel urlScrapeRequestModel)
        {
            var results = await _urlScraper.GetUrlScrapeResults(urlScrapeRequestModel.Search, urlScrapeRequestModel.Url,
                urlScrapeRequestModel.ResultCount);

            var modelResults = _mapper.Map<UrlScrapeResultsModel>(results);
            return Ok(modelResults);
        }
    }
}