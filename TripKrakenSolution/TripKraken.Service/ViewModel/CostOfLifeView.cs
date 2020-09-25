using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripKraken.Model.Main;

namespace TripKraken.Service.ViewModel
{
    public class CreateCostOfLivingInfoView
    {
        public int Id { get; set; }

        // Audit fields
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        // Foreign Key
        public string ApplicationUserId { get; set; }
        public int PriceForTypeID { get; set; }
        public int CountryInfoID { get; set; }

        // Prices
        [Display(Name ="Value(EUR)")]
        [Range(0.001, 9999999, ErrorMessage = "Invalid value")]
        [Required]
        public decimal Value { get; set; }

        // Constraints
        public virtual ApplicationUser User { get; set; }
        public virtual PriceForType PriceForType { get; set; }
        public virtual CountryInfo CountryInfo { get; set; }
        public virtual Currency Currency { get; set; }
    }

    public class EditCostOfLivingInfoView
    {
            public int Id { get; set; }

            // Rate
            [Range(0.001, 9999999, ErrorMessage = "Invalid value")]
            [Required]
            public decimal Value { get; set; }
    }
}
