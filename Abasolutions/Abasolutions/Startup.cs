using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Abasolutions.Startup))]
namespace Abasolutions
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
