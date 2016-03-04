using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClinicOne.Startup))]
namespace ClinicOne
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
