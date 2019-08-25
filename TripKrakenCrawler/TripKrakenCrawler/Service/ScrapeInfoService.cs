using IronWebScraper;
using System;

namespace TripKraken.Crawler.Service
{
    public class ScrapeInfoService : WebScraper
    {
        public string Link { get; set; }
        public ScrapeData ScrapeDataFor { get; set; }
        public ScrapeInfoService(string link, ScrapeData scrapeDataFor)
        {
            this.Link = link;
            this.ScrapeDataFor = scrapeDataFor;
        }
        public override void Init()
        {
            this.Request(this.Link, Parse);
        }
        public override void Parse(Response response)
        {
            this.WorkingDirectory = System.AppContext.BaseDirectory + @"\HelloScraperSample\Output\";

            switch (ScrapeDataFor)
            {
                case ScrapeData.Cities:
                    ScrapeDataCities(response);
                    break;
                case ScrapeData.Countries:
                    break;
                case ScrapeData.InternetSpeed:
                    ScrapeDataInternetSpeed(response);
                    break;
                case ScrapeData.Weather:
                    break;
            }
        }

        public void ScrapeDataCities(Response response)
        {
            // Loop on all options
            foreach (var item in response.GetElementById("country").ChildNodes)
            {
                // Read option value
                string strTitle = item.TextContentClean;
                if (!String.IsNullOrEmpty(strTitle))
                {
                    // Save Result to File
                    Scrape(new ScrapedData()
                    {
                        { "Name", strTitle },
                        { "Link", strTitle.Replace(' ', '+') }
                    }, "ScraperCountries.Json");
                }
            }
        }
        public void ScrapeDataInternetSpeed(Response response)
        {
            var res = response.GetElementById("column-fixed").ChildNodes[1].ChildNodes[5].ChildNodes[3].ChildNodes;
            // Loop on all options
            foreach (var item in response.GetElementById("column-fixed").ChildNodes[1].ChildNodes[5].ChildNodes[3].ChildNodes)
            {
                try
                {
                    // Read option value
                    string strSpeed = item.ChildNodes[7].TextContentClean;
                    string strTitle = item.ChildNodes[5].ChildNodes[1].TextContentClean;
                    if (!String.IsNullOrEmpty(strTitle))
                    {
                        // Save Result to File
                        Scrape(new ScrapedData()
                        {
                            {"CityName", strTitle},
                            {"AvgInternetSpeed", strSpeed}
                        }, "ScraperInternetSpeed.Json");
                    }
                }
                catch
                {

                }

            }
        }

        public void ScrapeDataCostOfLife(Response response)
        {
            // Not developed yet.
            throw new NotImplementedException();
        }

        public void ScrapeDataQualityOfLife(Response response)
        {
            // Not developed yet.
            throw new NotImplementedException();
        }

        public void ScrapeDataTraffic(Response response)
        {
            // Not developed yet.
            throw new NotImplementedException();
        }

        public void ScrapeCrimeRate(Response response)
        {
            // Not developed yet.
            throw new NotImplementedException();
        }
    }
}
