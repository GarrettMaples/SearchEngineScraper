using FluentAssertions;
using Moq;
using SearchEngineScraper.Data.Clients;
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
        public async Task GetUrlScrapeResults_InputLinks_AreFoundInHtml()
        {
            const int resultCount = 200;
            const string searchUrl = "www.infotrack.com";
            const string filename = "./HtmlResults/efiling+integration_google.txt";
            var testHtml = await File.ReadAllTextAsync(filename);

            var expectResults = new UrlScrapeResultsDto
            (
                string.Empty,
                searchUrl,
                resultCount,
                new Dictionary<int, string>
                {
                    { 1, "https://www.infotrack.com/clio/" },
                    { 2, "https://www.infotrack.com/blog/explained-integrated-efiling-and-continued-innovation/" },
                    // Duplicated results expected here since we specify a resultCount greater than
                    // Google max results per page which forces the scraper to 'move to the next page'
                    // which is duplicated in this test
                    { 101, "https://www.infotrack.com/clio/" },
                    { 102, "https://www.infotrack.com/blog/explained-integrated-efiling-and-continued-innovation/" }
                }
            );

            var stubClient = new Mock<IGoogleClient>();
            stubClient
                .Setup(client => client.GetSearchResults(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(testHtml);

            var googleUrlScraper = new GoogleUrlScraper(stubClient.Object);

            var request = new GoogleUrlScrapeRequest(string.Empty, searchUrl, resultCount);
            var results = await googleUrlScraper.GetUrlScrapeResults(request);

            results.Should().BeEquivalentTo(expectResults);
        }
    }
}