using System;

namespace SearchEngineScraper.Service.Scraping.Url.Google
{
    public class GoogleUrlScrapeException : Exception
    {
        public GoogleUrlScrapeException(string message) : base(message)
        {
        }
        
        public GoogleUrlScrapeException(string message, string html) 
            : base($"{message}{Environment.NewLine}HTML:{Environment.NewLine}{html}")
        {
        }
    }
}