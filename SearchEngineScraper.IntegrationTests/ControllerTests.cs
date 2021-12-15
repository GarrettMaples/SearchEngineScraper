using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using SearchEngineScraper.Api;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SearchEngineScraper.IntegrationTests
{
    public class ControllerTests
    {
        private readonly HttpClient _client;

        public ControllerTests()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }
        
        [Theory]
        [InlineData("/api/google-scrape/url-scrape?search=test&url=test.com&resultCount=100")]
        public async Task GetRequest_ReturnsSuccessfulStatusCode(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}