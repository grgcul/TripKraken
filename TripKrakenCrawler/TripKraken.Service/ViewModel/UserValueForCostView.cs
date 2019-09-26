using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripKraken.Model.Main;

namespace TripKraken.Service.ViewModel
{
    public class UserValueForCostView
    {
        public List<CostOfLivingInfo> CostList { get; set; }
        public int CountryId { get; set; }

        public List<Country> Countries { get; set; }
    }
}
