using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripKraken.Crawler.Service;
using TripKraken.DataSource.Model;

namespace TripKraken.Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var scrape = new ScrapeInfoService("https://www.numbeo.com/cost-of-living/",ScrapeData.Countries);
            var scrape2 = new ScrapeInfoService("https://www.speedtest.net/global-index", ScrapeData.InternetSpeed);
            scrape.ScrapeDataCostOfLifeValues();
            scrape.ScrapeCrimeRate();

            // Countries list
            //scrape.Start();

            //// Internet speed
            //scrape2.Start();


            //// Scrape info run
            //scrape.ScrapeCrimeRateTypes();

            //scrape.ScrapeDataCostOfLifeTypes();

            //scrape.ScrapingBasicCountryInfo();

            scrape.ScrapeCrimeRate();

            //scrape.ScrapeDataCostOfLifeValues();

            //scrape.ScrapeHealthCare();

            //scrape.ScrapePollutionRate();

            Console.ReadKey();
        }
    }
}
