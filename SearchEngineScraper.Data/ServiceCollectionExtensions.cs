using Microsoft.Extensions.DependencyInjection;
using Refit;
using SearchEngineScraper.Data.Clients;
using System;
using System.Net.Http.Headers;

namespace SearchEngineScraper.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddRefitClient<IGoogleClient>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri("https://www.google.com");
                    // Add Chrome User Agents
                    client.DefaultRequestHeaders.UserAgent.ParseAdd(
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.93 Safari/537.36");
                });

            return serviceCollection;
        }
    }
}