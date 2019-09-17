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
            //scrape.ScrapeDataCostOfLifeValues();
            //scrape.Start();

            // Internet speed
            //scrape2.Start();

            //Scrape redom

            //scrape.ScrapeCrimeRateTypes();
            //scrape.ScrapeCrimeRate();
            //scrape.ScrapeHealthCare();

            Console.WriteLine(@"Choose scraping:
                                -- Scrape Cost of life
                                -- Scrape Crime Rate
                                -- Scrape Internet speed
                                -- Scrape Health care
                                -- Scrape Pollution
                                -- Scrape Weather"
            );
            // TODO-GC: For now using live weather and time zone, hard to scrape if we have just country
            //scrape.ScrapeWeather();
            Console.ReadKey();
        }
    }
}
