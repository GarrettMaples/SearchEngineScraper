﻿using System;

namespace SearchEngineScraper.Service.Scraping.Url.Google
{
    public class GoogleUrlScrapeException : Exception
    {
        public GoogleUrlScrapeException(string message) : base(message)
        {
        }
    }
}