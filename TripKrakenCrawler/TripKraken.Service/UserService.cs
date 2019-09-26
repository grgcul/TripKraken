using Microsoft.AspNet.Identity.EntityFramework;
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
        public ApplicationUserManager UserManager
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

        public  List<IdentityRole> GetRoles()
        {
            var db = new ApplicationDbContext();
            return db.Roles.ToList();
        }

        public string GetRoleName(string id)
        {
            var db = new ApplicationDbContext();
            return db.Roles.SingleOrDefault(x => x.Id == id).Name;
        }

        public ApplicationUser GetUserById(string id)
        {
            return UserManager.Users.SingleOrDefault(x => x.Id == id);
        }


        public void DeleteUser(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                var forDeleteCOL = db.CostOfLivingInfo.Where(x => x.ApplicationUserId == id);
                var forDeleteCR = db.CrimeRateInfo.Where(x => x.ApplicationUserId == id);
                db.CostOfLivingInfo.RemoveRange(forDeleteCOL);
                db.CrimeRateInfo.RemoveRange(forDeleteCR);
                var result = db.Users.FirstOrDefault(x => x.Id == id);
                db.Users.Remove(result);
                db.SaveChanges();
            }
        }

        public void AddRole(string id, string role)
        {
            if(role != null)
            {
                using (var db = new ApplicationDbContext())
                {
                    var result = db.Users.FirstOrDefault(x => x.Id == id);
                    result.Roles.Clear();
                    db.SaveChanges();
                    result.Roles.Add(new IdentityUserRole() { UserId = id, RoleId = role });
                    db.SaveChanges();
                }
            }

        }

        public List<string> GetUsers()
        {
            return UserManager.Users.Select(x => x.Id).ToList();
        }


        public List<ApplicationUser> GetUserList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Users.ToList();
        }


        public List<CostOfLivingInfo> GetUserCostOfLiving(string username,int? country = null)
        {
            var user = username != null ? GetUser(username) : null;
            var result = new List<CostOfLivingInfo>();
            ApplicationDbContext db = new ApplicationDbContext();

            if(country == null)
                result = db.CostOfLivingInfo.Where(x => x.ApplicationUserId != null).ToList();
            else
                result = db.CostOfLivingInfo.Where(x => x.ApplicationUserId != null && x.CountryInfo.Country.Id == country).OrderByDescending(x => x.CountryInfo.Country.Name).ToList();

            if (username != null)
                result = result.Where(x => user.Id == x.ApplicationUserId).ToList();

            return result;
        }

        public List<CrimeRateInfo> GetUserCrimeRateInfo(string username=null,int? country = null)
        {
            var user = username != null ? GetUser(username) : null;
            var result = new List<CrimeRateInfo>();
            ApplicationDbContext db = new ApplicationDbContext();

            if(country == null)
                result = db.CrimeRateInfo.Where(x => x.ApplicationUserId != null).ToList();
            else
                result = db.CrimeRateInfo.Where(x => x.ApplicationUserId != null && x.CountryInfo.Country.Id == country).OrderByDescending(x => x.CountryInfo.Country.Name).ToList();

            if (username != null)
                result = result.Where(x => user.Id == x.ApplicationUserId).ToList();

            return result;
        }
    }
}
