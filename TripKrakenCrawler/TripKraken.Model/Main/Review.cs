using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripKraken.Model.Main
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        // Audit fields
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        // Foreign Key
        public string ApplicationUserId { get; set; }
        public int CountryInfoID { get; set; }

        // Prices
        public decimal RatingValue { get; set; }

        [StringLength(8000)]
        public string ReviewDescription { get; set; }

        // Constraints
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("CountryInfoID")]
        public virtual CountryInfo CountryInfo { get; set; }

    }
}
