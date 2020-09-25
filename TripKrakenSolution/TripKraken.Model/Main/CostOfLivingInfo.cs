using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripKraken.Model.Main
{
    public class CostOfLivingInfo
    {
        [Key]
        public int Id { get; set; }

        // Audit fields
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        // Foreign Key
        public string ApplicationUserId { get; set; }
        public int PriceForTypeID { get; set; }
        public int CountryInfoID { get; set; }
        public int? CurrencyID { get; set; }

        // Prices
        public decimal Value { get; set; }

        [StringLength(500)]
        public string InfoDescription { get; set; }

        // Constraints
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("PriceForTypeID")]
        public virtual PriceForType PriceForType { get; set; }

        [ForeignKey("CountryInfoID")]
        public virtual CountryInfo CountryInfo { get; set; }

        [ForeignKey("CurrencyID")]
        public virtual Currency Currency { get; set; }
    }
}