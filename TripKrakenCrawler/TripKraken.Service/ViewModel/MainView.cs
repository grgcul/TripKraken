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

    public class SearchView
    {
        public int? CountryId { get; set; }

        public List<Country> Countries { get; set; }

        public string action { get; set; }
        public string controller { get; set; }
    }
}
