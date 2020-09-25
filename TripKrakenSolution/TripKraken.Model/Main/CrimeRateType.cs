using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripKraken.Model.Main
{
    public class CrimeRateType
    {
        [Key]
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public virtual ICollection<CrimeRateInfo> CrimeRateInfo { get; set; }
    }
}
