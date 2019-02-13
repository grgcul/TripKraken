using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TripKraken.DataSource;

namespace TripKraken.Service.CoreService
{
    public class Cordinate
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }
    public class CityView
    {
        public float id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public Cordinate coord { get; set; }
    }
    public class CityList
    {
        public List<CityView> cityList { get; set; }
    }
    public class CrawlN
    {
        private DataSource.TravelWiseDB dbContext { get; set; }
        public CrawlN()
        {
            dbContext = new TravelWiseDB();
        }
        public List<City> InsertAllCities()
        {
            var result = new CityList();
                JsonSerializer serializer = new JsonSerializer();
                result = JsonConvert.DeserializeObject<CityList>(File.ReadAllText(@"C:\Users\grgcu\Documents\Gitprojects\TravelWise\TravelWiseCrawler\TravelWiseCrawler\TravelWiseCrawler\Resource\city.list.json"));
            foreach (var item in result.cityList)
            {
                City newCity = new City();
                newCity.CityName = item.name;
                newCity.CountryId = item.country;
                if(item.country != "")
                {
                        if(dbContext.Countries.Where(x => x.CountryId == item.country).Count()<1)
                    {
                        Country newCountry = new Country() { CountryId = item.country,CountryName="-" };
                        dbContext.Countries.Add(newCountry);
                        dbContext.SaveChanges();
                    }
                        dbContext.Cities.Add(newCity);
                    dbContext.SaveChanges();
                }
            }
            //dbContext.SaveChanges();
            return dbContext.Cities.ToList();
        }
        public List<string> GetPriceList()
        {
            WebClient client = new WebClient();
            List<Country> countryList = new List<Country>();
            List<string> resultList = new List<string>();
            //create an array of CultureInfo to hold all the cultures found, these include the users local cluture, and all the
            //cultures installed with the .Net Framework
            //CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            countryList = dbContext.Countries.ToList();
            int itr = 0;
            //loop through all the cultures found
            foreach (var country in countryList)
            {
                if (itr != 0 && itr < 50)
                {
                    try
                    {
                        ////pass the current culture's Locale ID (http://msdn.microsoft.com/en-us/library/0h88fahh.aspx)
                        ////to the RegionInfo contructor to gain access to the information for that culture
                        //RegionInfo region = new RegionInfo(culture.LCID);

                        ////make sure out generic list doesnt already
                        ////contain this country

                        //if (!(cultureList.Contains(region.EnglishName)))
                        //    //not there so add the EnglishName (http://msdn.microsoft.com/en-us/library/system.globalization.regioninfo.englishname.aspx)
                        //    //value to our generic list

                        string downloadString = client.DownloadString("https://www.numbeo.com/cost-of-living/country_result.jsp?country=" + country.CountryName+ "&displayCurrency=EUR");
                        Regex regex = new Regex("<td.*?>(.*?)</td>");
                        var v = regex.Matches(downloadString);
                        if (v.Count > 2)
                        {
                            string sTitle = v[0].ToString().Replace("<td>", "").Replace("</td>", "");
                            string sPrice = v[1].ToString()
                                .Replace("<td style=\"text-align: right\" class=\"priceValue \">", "")
                                .Replace("</td>", "")
                                .Replace("&nbsp", "")
                                .Replace("#36", "")
                                .Replace(";&#8364","")
                                .Replace("&nbspR", "")
                                .Replace("&;", "")
                                .Replace(";R", "")
                                .Replace(";", "");
              
                            resultList.Add(country.CountryName+" - "+sTitle + " - "+sPrice+" EUR");
                        }
                    }
                    catch
                    {

                    }
                }
                itr++;
            }
            return resultList;
        }
    }
}
