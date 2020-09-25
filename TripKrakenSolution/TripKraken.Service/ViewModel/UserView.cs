using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripKraken.Model.Main;

namespace TripKraken.Service.ViewModel
{
    public class UserView
    {
        public List<ApplicationUser> UserList { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}
