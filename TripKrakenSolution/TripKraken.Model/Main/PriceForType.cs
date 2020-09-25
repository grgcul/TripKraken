using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripKraken.Model.Main
{
    public class PriceForType
    {
        [Key]
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public virtual ICollection<CostOfLivingInfo> CostOfLivingInfo { get; set; }
    }
}
