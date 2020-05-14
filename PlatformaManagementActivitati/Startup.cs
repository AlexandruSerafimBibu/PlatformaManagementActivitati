using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlatformaManagementActivitati.Startup))]
namespace PlatformaManagementActivitati
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
