using FluentAssertions;
using Moq;
using SearchEngineScraper.Data.Clients;
using SearchEngineScraper.Service;
using SearchEngineScraper.Service.Dtos;
using SearchEngineScraper.Service.Scraping.Url.Google;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace SearchEngineScraper.UnitTests
{
    public class GoogleUrlScraperTests
    {
        [Fact]
        public async Task GetUrlScrapeResults_InputLink_IsFoundInHtml()
        {
            const int resultCount = 100;
            const string searchUrl = "www.infotrack.com";
            const string filename = "./HtmlResults/efiling+integration.html";
            var testHtml = await File.ReadAllTextAsync(filename);

            var expectResults = new UrlScrapeResultsDto
            (
                string.Empty,
                searchUrl,
                resultCount,
                new Dictionary<int, string>
                {
                    { 1, "https://www.infotrack.com/clio/" },
                    { 2, "https://www.infotrack.com/blog/explained-integrated-efiling-and-continued-innovation/" }
                }
            );

            var stubClient = new Mock<IGoogleClient>();
            stubClient
                .Setup(client => client.GetSearchResults(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(testHtml);

            var googleUrlScraper = new GoogleUrlScraper(stubClient.Object);

            var results = await googleUrlScraper.GetUrlScrapeResults(string.Empty, searchUrl, resultCount);

            results.Should().BeEquivalentTo(expectResults);
        }
    }
}