using System;
using System.Collections.Generic;
using System.Globalization;
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

        public PriceForType GetPriceForType(string name)
        {
            var db = new ApplicationDbContext();
            return db.PriceForType.First(x => x.Name == name);
        }

        public CrimeRateType GetCrimeRateType(string name)
        {
            var db = new ApplicationDbContext();
            return db.CrimeRateType.First(x => x.Name == name);
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

        public CountryInfo GetCountryInfo(int? countryId = null,string name = null)
        {
            var db = new ApplicationDbContext();
            if (!String.IsNullOrEmpty(name))
            {
                return db.CountryInfo.SingleOrDefault(x => name.Contains(x.Country.Name));
            }
            else
            {
                return db.CountryInfo.SingleOrDefault(x => x.CountryID == countryId);
            }
        }

        public CountryInfoView GetCountryInfoValues(int? countryId=null,string countryName = null)
        {
            var db = new ApplicationDbContext();
            var result = new CountryInfoView();

            var countryInfoResult = GetCountryInfo(countryId,countryName);

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

            result.Country = countryInfoResult.Country;
            result.CostOfLifeAvg = costOfLivingAvgList;
            result.CrimeRates = crimeRateAvgList;

            result.MobileInternetSpeed = countryInfoResult.MobileInternetSpeed;
            result.WiredInternetSpeed = decimal.Parse(countryInfoResult.WiredInternetSpeed, CultureInfo.InvariantCulture);
            result.PolutionRate = countryInfoResult.PolutionRate;
            result.Population = countryInfoResult.Population;
            result.LandArea = countryInfoResult.LandArea;
            result.Density = countryInfoResult.Density;
            result.CrimeRateAvg = crimeRateAvgList.SingleOrDefault(x => x.Key == "TotalCrimeRate").Value;
            result.SafetyRateAvg = crimeRateAvgList.SingleOrDefault(x => x.Key == "TotalSafetyRate").Value;
            try
            {
                result.HealthCareAvg = Math.Round(countryInfoResult.HealthCareInfo.Sum(x => x.Value) /
                                       countryInfoResult.HealthCareInfo.Count);
            }catch { }


            result.Id = countryInfoResult.Id;

            return result;
        }

        public void DeleteCostOfLiving(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.CostOfLivingInfo.Find(id);
                db.CostOfLivingInfo.Remove(result);
                db.SaveChanges();
            }
        }

        public void DeleteCrimeRate(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.CrimeRateInfo.Find(id);
                db.CrimeRateInfo.Remove(result);
                db.SaveChanges();
            }
        }

        public void CreateCostOfLiving(CostOfLivingInfo model)
        {
            using (var db = new ApplicationDbContext())
            {
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                db.CostOfLivingInfo.Add(model);
                db.SaveChanges();
            }
        }

        public void CreateCrimeRate(CrimeRateInfo model)
        {
            using (var db = new ApplicationDbContext())
            {
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                db.CrimeRateInfo.Add(model);
                db.SaveChanges();
            }
        }
        //TODO: TU STAO TREBA DODATI LISTU INPUTA I EDIT DELETE ZA NJIH

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
