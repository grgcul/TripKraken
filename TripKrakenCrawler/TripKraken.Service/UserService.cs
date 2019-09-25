using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripKraken.DataSource.Model;
using TripKraken.Model.Main;

namespace TripKraken.Service
{
    public class UserService
    {
        private ApplicationUserManager _userManager;
        protected ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    var db = new ApplicationDbContext();
                    return new ApplicationUserManager(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(db));
                }
                return _userManager;
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationUser GetUser(string username)
        {
            return UserManager.Users.SingleOrDefault(x => x.UserName == username);
        }

        public List<string> GetUsers()
        {
            return UserManager.Users.Select(x => x.Id).ToList();
        }

        public List<CostOfLivingInfo> GetUserCostOfLiving(string username,int? country = null)
        {
            var user = GetUser(username);

            ApplicationDbContext db = new ApplicationDbContext();
            if(country == null)
                return db.CostOfLivingInfo.Where(x => x.ApplicationUserId == user.Id).ToList();
            else
                return db.CostOfLivingInfo.Where(x => x.ApplicationUserId == user.Id && x.CountryInfo.Country.Id == country).OrderByDescending(x => x.CountryInfo.Country.Name).ToList();
        }

        public List<CrimeRateInfo> GetUserCrimeRateInfo(string username,int? country = null)
        {
            var user = GetUser(username);

            ApplicationDbContext db = new ApplicationDbContext();
            if(country == null)
                return db.CrimeRateInfo.Where(x => x.ApplicationUserId == user.Id).ToList();
            else
                return db.CrimeRateInfo.Where(x => x.ApplicationUserId == user.Id && x.CountryInfo.Country.Id == country).OrderByDescending(x => x.CountryInfo.Country.Name).ToList();
        }
    }
}
