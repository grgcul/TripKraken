using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TripKraken.DataSource.Model;
using TripKraken.Model.Main;
using TripKraken.Service.ViewModel;

namespace TripKraken.Service
{
    public class CountryInfoService
    {
        public List<Country> GetCountries()
        {
            var db = new ApplicationDbContext();
            return db.Country.ToList();
        }

        public SelectList GetSelectListCountries(int countryId)
        {
            var item = new SelectListItem { Text = "Choose country", Value = "0", Selected = (countryId == null)};
            var newList = new SelectList(new List<SelectListItem>(){item});
            return new SelectList(newList.Union(GetCountries().Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString(),Selected=x.Id == countryId})));
        }
        public List<CountryInfo> GetCountryInfoes()
        {
            var db = new ApplicationDbContext();
            return db.CountryInfo.ToList();
        }

        public CountryInfo GetCountryInfo(int countryId)
        {
            var db = new ApplicationDbContext();
            return db.CountryInfo.SingleOrDefault(x => x.CountryID == countryId);
        }

        public CountryInfoView GetCountryInfoValues(int countryId)
        {
            var db = new ApplicationDbContext();
            var result = new CountryInfoView();

            var countryInfoResult = GetCountryInfo(countryId);

            Dictionary<string, decimal> costOfLivingAvgList = new Dictionary<string, decimal>();
            Dictionary<string, decimal> crimeRateAvgList = new Dictionary<string, decimal>();

            var costOfLivingTemp = countryInfoResult.CostOfLivingInfo.GroupBy(x => x.PriceForType.Name).Select(g => new {Value = g.Sum(c => c.Value),Count = g.Count(), g.Key});
            var crimeRateTemp = countryInfoResult.CrimeRate.GroupBy(x => x.CrimeRateType.Name).Select(g => new { Value = g.Sum(c => c.Value), Count = g.Count(), g.Key });

            foreach (var item in costOfLivingTemp)
            {
                var itemAvg = item.Value / item.Count;
                costOfLivingAvgList.Add(item.Key, itemAvg);
            }

            foreach (var item in crimeRateTemp)
            {
                var itemAvg = item.Value / item.Count;
                crimeRateAvgList.Add(item.Key, itemAvg);
            }

            result.CostOfLifeAvg = costOfLivingAvgList;
            result.CrimeRates = crimeRateAvgList;
            result.MobileInternetSpeed = countryInfoResult.MobileInternetSpeed;
            result.WiredInternetSpeed = countryInfoResult.WiredInternetSpeed;
            result.PolutionRate = countryInfoResult.PolutionRate;
            try
            {
                result.HealthCareAvg = countryInfoResult.HealthCareInfo.Sum(x => x.Value) /
                                       countryInfoResult.HealthCareInfo.Count;
            }catch { }

            result.Id = countryInfoResult.Id;

            return result;
        }

        //public List<CountryRanksView> GetTopCountryRanks()
        //{

        //}

        //public List<MaxValues> GetMaxValues()
        //{

        //}

        //public List<AvgValues> GetMaxValues()
        //{

        //}

    }

}
