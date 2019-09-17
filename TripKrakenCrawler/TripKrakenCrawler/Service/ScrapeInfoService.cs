using IronWebScraper;
using System;
using TripKraken.DataSource.Model;
using TripKraken.Model.Main;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using TripKraken.DataSource.Model;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
                    break;
                case ScrapeData.Countries:
                    ScrapeDataCountries(response);
                    break;
                case ScrapeData.InternetSpeed:
                    ScrapeDataInternetSpeed(response);
                    break;
                case ScrapeData.Weather:
                    break;
            }
        }

        public void ScrapeDataCountries(Response response)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            // Loop on all options
            foreach (var item in response.GetElementById("country").ChildNodes)
            {
                // Read option value
                string strTitle = item.TextContentClean;
                if (!String.IsNullOrEmpty(strTitle) && !dbContext.Country.Any(x => x.Name == strTitle))
                {
                    dbContext.Country.Add(new Country()
                    {
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Name = strTitle,
                        Link = strTitle.Replace(' ', '+')
                    });
                    Console.WriteLine($"Row inserted: {strTitle}");
                }
            }
            dbContext.SaveChanges();
        }
        public void ScrapeDataInternetSpeed(Response response)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            // Loop on all options
            foreach (var item in response.GetElementById("column-fixed").ChildNodes[1].ChildNodes[5].ChildNodes[3].ChildNodes)
            {
                // Read option value
                if (item.ChildNodes.Length > 7)
                {
                    string strSpeed = item.ChildNodes[7].TextContentClean;
                    string strTitle = item.ChildNodes[5].ChildNodes[1].TextContentClean;
                    if (!String.IsNullOrEmpty(strTitle) && !dbContext.CountryInfo.Any(x => x.Country.Name == strTitle))
                    {
                        if (dbContext.Country.SingleOrDefault(x => x.Name == strTitle) != null)
                        {
                            dbContext.CountryInfo.Add(new CountryInfo()
                            {
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                                CountryID = dbContext.Country.SingleOrDefault(x => x.Name == strTitle).Id,
                                WiredInternetSpeed = strSpeed,

                            });
                            // Save Result to File
                            Scrape(new ScrapedData()
                            {
                                {"CityName", strTitle},
                                {"AvgInternetSpeed", strSpeed}
                            }, "ScraperInternetSpeed.Json");
                        }
                    }
                }
            }
            dbContext.SaveChanges();
        }

        #region types
        // Cost of life types using Croatia for default
        public void ScrapeDataCostOfLifeTypes()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://www.numbeo.com/cost-of-living/country_result.jsp?country=Croatia");
                var element = driver.FindElementByClassName("data_wide_table");
                var elements = element.FindElements(By.TagName("tbody")).First().FindElements(By.TagName("tr")).ToList();
                using (var db = new ApplicationDbContext())
                {
                    foreach (var elementItem in elements)
                    {
                        if (!(elementItem.FindElements(By.TagName("th")).Count > 0))
                        {
                            var priceType = elementItem.FindElements(By.TagName("td")).First().Text;
                            if (!db.PriceForType.Any(x => x.Name == priceType))
                            {
                                db.PriceForType.Add(new PriceForType()
                                {
                                    Name = priceType
                                });
                                Console.WriteLine($@"Inserting type: {priceType}");
                            }
                            else
                            {
                                Console.WriteLine($@"Duplicate type: {priceType}");
                            }

                        }
                    }
                    db.SaveChanges();
                }
            }
        }

        //  Crime types using Croatia for default
        public void ScrapeCrimeRateTypes()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://www.numbeo.com/crime/country_result.jsp?country=Croatia");
                var element = driver.FindElementByClassName("data_wide_table");
                var elements = element.FindElements(By.TagName("tbody")).First().FindElements(By.TagName("tr")).ToList();
                using (var db = new ApplicationDbContext())
                {
                    foreach (var elementItem in elements)
                    {
                        if (!(elementItem.FindElements(By.TagName("th")).Count > 0))
                        {
                            var crimeType = elementItem.FindElements(By.TagName("td")).First().Text;
                            if (!db.CrimeRateType.Any(x => x.Name == crimeType))
                            {
                                db.CrimeRateType.Add(new CrimeRateType()
                                {
                                    Name = crimeType
                                });
                                Console.WriteLine($@"Inserting type: {crimeType}");
                            }
                            else
                            {
                                Console.WriteLine($@"Duplicate type: {crimeType}");
                            }

                        }
                    }

                    db.CrimeRateType.Add(new CrimeRateType() { Name = "TotalCrimeRate" });
                    db.CrimeRateType.Add(new CrimeRateType() { Name = "TotalSafetyRate" });
                    db.SaveChanges();
                }
            }
        }
        #endregion types

        #region scrape values
        // Cost of life value
        public void ScrapeDataCostOfLifeValues()
        {
            Console.WriteLine("Scraping starting...");
            using (var db = new ApplicationDbContext())
            {
                var timespan = TimeSpan.FromMinutes(3);
                using (var driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions(), timespan))
                {

                    foreach (var countryItem in db.Country.ToList())
                    {
                        if (!db.CountryInfo.Any(x => x.CountryID == countryItem.Id))
                        {
                            var countryInfo = new CountryInfo()
                            {
                                CountryID = countryItem.Id,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now
                            };

                            driver.Navigate().GoToUrl($@"https://www.numbeo.com/cost-of-living/country_result.jsp?country={countryItem.Link}&displayCurrency=EUR");
                            db.CountryInfo.Add(countryInfo);
                            db.SaveChanges();
                            var element = driver.FindElementByClassName("data_wide_table");
                            var elements = element.FindElements(By.TagName("tbody")).First().FindElements(By.TagName("tr")).ToList();
                            Console.WriteLine($"Scraping for ({countryItem.Name}): \n");
                            foreach (var elementItem in elements)
                            {
                                try
                                {
                                    if (!(elementItem.FindElements(By.TagName("th")).Count > 0))
                                    {
                                        var priceType = elementItem.FindElements(By.TagName("td")).First().Text;
                                        var priceValue = elementItem.FindElements(By.TagName("td")).ElementAt(1).Text;
                                        priceValue = priceValue.Substring(0, priceValue.IndexOf(' ') > 0 ? priceValue.IndexOf(' ') : priceValue.Length);
                                        //if (!db.CostOfLivingInfo.Any(x => x.CountryInfo.Country.Name == countryItem.Name && x.PriceForType.Name == priceType))
                                        //{
                                        db.CostOfLivingInfo.Add(new CostOfLivingInfo()
                                        {
                                            Value = Convert.ToDecimal(priceValue == "?" ? "0" : priceValue),
                                            PriceForTypeID = db.PriceForType.First(x => x.Name == priceType).Id,
                                            CountryInfoID = countryInfo.Id,
                                            CreateDate = DateTime.Now,
                                            UpdateDate = DateTime.Now
                                        });
                                        Console.WriteLine($@"Inserting value for - {priceType}");
                                        //}
                                        //else
                                        //{
                                        //    Console.WriteLine($@"Duplicate type: {priceType}");
                                        //}

                                    }
                                }
                                catch { }

                            }
                            //System.Threading.Thread.Sleep(5000);
                            Console.WriteLine("\n");
                            db.SaveChanges();
                        }

                    }

                }
            }
            Console.WriteLine("Scraping ended...");
        }

        // Crime rate /100
        public void ScrapeCrimeRate()
        {
            Console.WriteLine("Scraping starting...");
            using (var db = new ApplicationDbContext())
            {
                var timespan = TimeSpan.FromMinutes(3);
                using (var driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions(), timespan))
                {

                    foreach (var countryItem in db.CountryInfo.ToList())
                    {
                        if (!db.CrimeRateInfo.Any(x => x.CountryInfo.CountryID == countryItem.CountryID))
                        {
                            driver.Navigate()
                                .GoToUrl(
                                    $@"https://www.numbeo.com/crime/country_result.jsp?country={countryItem.Country.Name}");


                            // Total index scraping and adding single record for total value
                            var elementTotal = driver.FindElementByClassName("table_indices")
                                .FindElements(By.TagName("tbody")).First().FindElements(By.TagName("tr")).ToList();

                            var crimeRateT = elementTotal.ElementAt(1).FindElements(By.TagName("td")).ElementAt(1).Text;
                            var safetyRateT = elementTotal.ElementAt(2).FindElements(By.TagName("td")).ElementAt(1)
                                .Text;

                            db.CrimeRateInfo.Add(new CrimeRateInfo()
                            {
                                Value = Convert.ToDecimal(crimeRateT == "?" ? "0" : crimeRateT),
                                CrimeRateTypeID = db.CrimeRateType.First(x => x.Name == "TotalCrimeRate").Id,
                                CountryInfoID = countryItem.Id,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now
                            });

                            db.CrimeRateInfo.Add(new CrimeRateInfo()
                            {
                                Value = Convert.ToDecimal(safetyRateT == "?" ? "0" : safetyRateT),
                                CrimeRateTypeID = db.CrimeRateType.First(x => x.Name == "TotalSafetyRate").Id,
                                CountryInfoID = countryItem.Id,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now
                            });

                            // Getting elements for all values
                            var element = driver.FindElementByClassName("data_wide_table");
                            var elements = element.FindElements(By.TagName("tbody")).First()
                                .FindElements(By.TagName("tr")).ToList();
                            Console.WriteLine($"Scraping crime for ({countryItem.Country.Name}): \n");

                            foreach (var elementItem in elements)
                            {
                                if (!(elementItem.FindElements(By.TagName("th")).Count > 0))
                                {
                                    var crimeType = elementItem.FindElements(By.TagName("td")).First().Text;
                                    var crimeRate = elementItem.FindElements(By.TagName("td")).ElementAt(2).Text;
                                    crimeRate = crimeRate.Substring(0,
                                        crimeRate.IndexOf(' ') > 0 ? crimeRate.IndexOf(' ') : crimeRate.Length);
                                    if (!db.CrimeRateInfo.Any(x =>
                                        x.CountryInfo.Country.Name == countryItem.Country.Name &&
                                        x.CrimeRateType.Name == crimeType))
                                    {
                                        db.CrimeRateInfo.Add(new CrimeRateInfo()
                                        {
                                            Value = Convert.ToDecimal(crimeRate == "?" ? "0" : crimeRate),
                                            CrimeRateTypeID = db.CrimeRateType.First(x => x.Name == crimeType).Id,
                                            CountryInfoID = countryItem.Id,
                                            CreateDate = DateTime.Now,
                                            UpdateDate = DateTime.Now
                                        });
                                        Console.WriteLine($@"Inserting value for - {crimeType}");
                                    }

                                }

                            }

                            //System.Threading.Thread.Sleep(5000);
                            Console.WriteLine("\n");
                            db.SaveChanges();
                        }
                    }

                }
            }
            Console.WriteLine("Scraping ended...");
        }

        public void ScrapeHealthCare()
        {
            Console.WriteLine("Scraping starting...");
            using (var db = new ApplicationDbContext())
            {
                var timespan = TimeSpan.FromMinutes(3);
                using (var driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions(), timespan))
                {
                    driver.Navigate().GoToUrl($@"https://www.numbeo.com/health-care/rankings_by_country.jsp");
                    var element = driver.FindElementById("t2");
                    var elements = element.FindElements(By.TagName("tbody")).First().FindElements(By.TagName("tr")).ToList();
                    Console.WriteLine($"Scraping health care\n");
                    foreach (var elementItem in elements)
                    {
                        try
                        {
                            if (!(elementItem.FindElements(By.TagName("th")).Count > 0))
                            {
                                var healthCareIndex = elementItem.FindElements(By.TagName("td")).ElementAt(2).Text;
                                var country = elementItem.FindElements(By.TagName("td")).ElementAt(1).Text;
                                db.HealthCareInfo.Add(new HealthCareInfo()
                                {
                                    Value = Convert.ToDecimal(healthCareIndex == "?" ? "0" : healthCareIndex),
                                    CountryInfoID = db.CountryInfo.SingleOrDefault(x => x.Country.Name == country).Id,
                                    CreateDate = DateTime.Now,
                                    UpdateDate = DateTime.Now
                                });
                                Console.WriteLine($@"Inserting value for health index - ");
                            }
                        }
                        catch { }

                    }
                    //System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n");
                    db.SaveChanges();
                }
            }
            Console.WriteLine("Scraping ended...");
        }
        #endregion

        public void ScrapeWeather()
        {
            Console.WriteLine("Scraping starting...");
            using (var db = new ApplicationDbContext())
            {
                var timespan = TimeSpan.FromMinutes(3);
                using (var driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions(), timespan))
                {
                    foreach (var countryItem in db.CountryInfo.ToList())
                    {
                        try
                        {

                            Console.WriteLine($"Scraping for ({countryItem.Country.Name}): \n");
                            foreach (var month in db.Months)
                            {
                                Console.WriteLine($"Scraping for ({month.Name}) \n");
                                // This web is not ok I need to find other one, so for now we will have live forecast 
                                //driver.Navigate().GoToUrl($@"https://www.thomascook.com/holidays/weather/{countryItem.Country.Link.Replace("+", "-").ToLower()}/{month.Name}");
                                var elements = driver.FindElementsByClassName("destination-weather-stat-subtitle");
                                var avgTemp = Convert.ToDecimal(driver.FindElementByClassName("destination-avg-max-temp").Text);

                                var sunPerDay = Convert.ToDecimal(elements.ElementAt(0).Text);
                                var rainPerMonth = Convert.ToDecimal(elements.ElementAt(1).Text);
                                var humidityPercent = Convert.ToDecimal(elements.ElementAt(2).Text);

                                var newWeatherInfo = new WeatherInfo()
                                {
                                    TempAvg = avgTemp,
                                    SunValue = sunPerDay,
                                    RainValue = rainPerMonth,
                                    HumidityValue = humidityPercent,
                                    MonthID = month.Id,
                                    CountryInfoID = countryItem.Id
                                };
                                db.WeatherInfo.Add(newWeatherInfo);
                                //System.Threading.Thread.Sleep(5000);
                                db.SaveChanges();
                            }

                        }
                        catch
                        {
                            Console.WriteLine($"Scraping failed for ({countryItem.Country.Name}): \n");
                        }
                    }

                }
            }
            Console.WriteLine("Scraping ended...");
        }

        public void ScrapePollutionRate()
        {
            Console.WriteLine("Scraping starting...");
            using (var db = new ApplicationDbContext())
            {
                var timespan = TimeSpan.FromMinutes(3);
                using (var driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions(), timespan))
                {
                    driver.Navigate().GoToUrl($@"https://www.numbeo.com/pollution/rankings_by_country.jsp");
                    var element = driver.FindElementById("t2");
                    var elements = element.FindElements(By.TagName("tbody")).First().FindElements(By.TagName("tr")).ToList();
                    Console.WriteLine($"Scraping health care\n");
                    foreach (var elementItem in elements)
                    {
                        try
                        {
                            if (!(elementItem.FindElements(By.TagName("th")).Count > 0))
                            {
                                var pollutionRate = elementItem.FindElements(By.TagName("td")).ElementAt(2).Text;
                                var country = elementItem.FindElements(By.TagName("td")).ElementAt(1).Text;
                                var result = db.CountryInfo.SingleOrDefault(x => x.Country.Name == country);
                                result.PolutionRate = Convert.ToDecimal(pollutionRate == "?" ? "0" : pollutionRate);
                                db.SaveChanges();
                                Console.WriteLine($@"Inserting value for pollution index  - {country}");
                            }
                        }
                        catch { }

                    }
                    //System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n");
                }
            }
            Console.WriteLine("Scraping ended...");
        }

    }
}
