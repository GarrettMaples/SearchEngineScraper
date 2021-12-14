using FluentValidation;

namespace SearchEngineScraper.Api.Models.Requests
{
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