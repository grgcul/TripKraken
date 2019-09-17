using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripKraken.Model.Main;

namespace TripKraken.Service.ViewModel
{
    public class CountryInfoView
    {
        public int Id { get; set; }

        // Audit fields
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int CountryId { get; set; }

        public List<Country> Countries { get; set; }

        // /120
        // Facts
        public decimal? PolutionRate { get; set; }

        public string WiredInternetSpeed { get; set; }

        public string MobileInternetSpeed { get; set; }

        public Country Country { get; set; }

        public Dictionary<string, decimal> CostOfLifeAvg { get; set; }
        public Dictionary<string, decimal> CrimeRates { get; set; }

        public decimal CrimeRateAvg { get; set; }
        public decimal HealthCareAvg { get; set; }
    }
}
