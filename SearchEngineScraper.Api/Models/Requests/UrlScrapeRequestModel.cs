using FluentValidation;

namespace SearchEngineScraper.Api.Models.Requests
{
    /// <summary>
    /// Contains parameters for a Google results URL scrape
    /// </summary>
    /// <param name="Search">
    /// The keyword(s) to search Google for. This can be multiple keywords.
    /// It will be submitted to Google verbatim.
    /// </param>
    /// <param name="Url">
    /// The URL to search for in the Google results.
    /// This is a wildcard search and will match the URL on any portion of this parameter
    /// </param>
    /// <param name="ResultCount">The number of results to search through (inclusive)</param>
    public record UrlScrapeRequestModel(string Search, string Url, int ResultCount);

    public class UrlScrapeRequestModelValidator : AbstractValidator<UrlScrapeRequestModel>
    {
        public UrlScrapeRequestModelValidator()
        {
            RuleFor(x => x.Search).NotEmpty();
            RuleFor(x => x.Url).NotEmpty();
            RuleFor(x => x.ResultCount).GreaterThan(0);
        }
    }
}