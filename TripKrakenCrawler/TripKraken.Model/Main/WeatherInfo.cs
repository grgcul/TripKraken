using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripKraken.Model.Main
{
    public class WeatherInfo
    {
        [Key]
        public int Id { get; set; }

        // Audit fields
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        // Foreign Key
        public string ApplicationUserId { get; set; }
        public int CountryInfoID { get; set; }
        public int MonthID { get; set; }


        // Values

        public decimal TempAvg { get; set; }
        public decimal SunValue { get; set; }
        public decimal RainValue { get; set; }
        public decimal HumidityValue { get; set; }


        [StringLength(500)]
        public string InfoDescription { get; set; }


        [ForeignKey("CountryInfoID")]
        public virtual CountryInfo CountryInfo { get; set; }

        [ForeignKey("MonthID")]
        public virtual Month Month { get; set; }

    }
}
