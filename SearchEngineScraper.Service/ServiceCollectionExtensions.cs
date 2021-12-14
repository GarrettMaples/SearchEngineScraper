using Microsoft.Extensions.DependencyInjection;
using SearchEngineScraper.Service.Scraping.Url;
using SearchEngineScraper.Service.Scraping.Url.Google;

namespace SearchEngineScraper.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient<IUrlScraper, GoogleUrlScraper>();
        }
    }
}