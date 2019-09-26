using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using TripKraken.Model.Main;

namespace TripKraken.DataSource.Model
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDisposable
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Country> Country { get; set; }
        public DbSet<CountryInfo> CountryInfo { get; set; }
        public DbSet<CostOfLivingInfo> CostOfLivingInfo { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<PriceForType> PriceForType { get; set; }
        public DbSet<CrimeRateInfo> CrimeRateInfo { get; set; }
        public DbSet<CrimeRateType> CrimeRateType { get; set; }
        public DbSet<HealthCareInfo> HealthCareInfo { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<WeatherInfo> WeatherInfo{ get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
