using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TripKraken.Web.Startup))]
namespace TripKraken.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
