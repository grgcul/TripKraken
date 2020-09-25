using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripKraken.Model.Main
{
    public class Month
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public int Value { get; set; }

        public virtual ICollection<WeatherInfo> WeatherInfo { get; set; }
    }
}
