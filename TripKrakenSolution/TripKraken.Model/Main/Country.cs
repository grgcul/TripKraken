using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripKraken.Model.Main
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        // Audit fields
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Link { get; set; }
        [StringLength(100)]
        public string Code { get; set; }

        public virtual ICollection<CostOfLivingInfo> CostOfLivingInfos { get; set; }
        public virtual ICollection<CountryInfo> CountryInfos { get; set; }
    }
}
