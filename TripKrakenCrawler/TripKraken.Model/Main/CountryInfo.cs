using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripKraken.Model.Main
{
    public class CountryInfo
    {
        [Key] public int Id { get; set; }

        // Audit fields
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int CountryID { get; set; }

        // /120
        // Facts
        public decimal? PolutionRate { get; set; }
        public decimal? LandArea { get; set; }
        public decimal? Population { get; set; }
        public decimal? Density { get; set; }
        [StringLength(50)]
        public string WiredInternetSpeed { get; set; }
        [StringLength(50)]
        public string MobileInternetSpeed { get; set; }

        // References
        [ForeignKey("CountryID ")]
        public virtual Country Country { get; set; }

        public virtual ICollection<CostOfLivingInfo> CostOfLivingInfo { get; set; }
        public virtual ICollection<CrimeRateInfo> CrimeRate { get; set; }
        // /100
        public virtual ICollection<HealthCareInfo> HealthCareInfo { get; set; }
        //public virtual ICollection<UserComment> UserComment { get; set; }
    }

    //public class UserComment
    //{
    //}

    //public class HealthCareInfo
    //{
    //}

    //public class CrimeRateInfo
    //{
    //}

    //public class TrafficInfo
    //{
    //}

}
