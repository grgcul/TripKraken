using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripKraken.Model.Main
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string UnitName { get; set; }
        public string Code { get; set; }

        public virtual ICollection<CostOfLivingInfo> CostOfLivingInfo { get; set; }
    }
}
