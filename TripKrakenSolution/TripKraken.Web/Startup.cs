using AutoMapper;
using Microsoft.Owin;
using TripKraken.Service.ViewModel;
using AutoMapper;
using TripKraken.Model.Main;
using Owin;

[assembly: OwinStartupAttribute(typeof(TripKraken.Web.Startup))]
namespace TripKraken.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Mapper.Initialize(
                 config =>
                 {
                     //Employee
                     config.CreateMap<CreateCostOfLivingInfoView, CostOfLivingInfo>();
                     config.CreateMap<CreateCrimeRateInfo, CrimeRateInfo>();
                 });
        }
    }
}
