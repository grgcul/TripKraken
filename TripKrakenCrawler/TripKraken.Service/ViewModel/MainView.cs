using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripKraken.Model.Main;

namespace TripKraken.Service.ViewModel
{
    public class MainView
    {
        public int CountryId { get; set; }

        public List<Country> Countries { get; set; }
    }
}
