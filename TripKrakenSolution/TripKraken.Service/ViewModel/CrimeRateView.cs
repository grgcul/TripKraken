using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripKraken.Model.Main;

namespace TripKraken.Service.ViewModel
{
    public class CreateCrimeRateInfo
    {
        // Audit fields
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        // Foreign Key
        public string ApplicationUserId { get; set; }
        public int CrimeRateTypeID { get; set; }
        public int CountryInfoID { get; set; }

        // Rate
        [Display(Name ="Value (0-100)")]
        [Range(0.001, 100, ErrorMessage = "Invalid value")]
        [Required]
        public decimal Value { get; set; }

        // Constraints
        public virtual ApplicationUser User { get; set; }

        public virtual CrimeRateType CrimeRateType { get; set; }

        public virtual CountryInfo CountryInfo { get; set; }
    }

    public class EditCrimeRateInfo
    {
        public int Id { get; set; }

        // Rate
        [Display(Name = "Value (0-100)")]
        [Range(0.001, 100, ErrorMessage = "Invalid value")]
        [Required]
        public decimal Value { get; set; }
    }
}
