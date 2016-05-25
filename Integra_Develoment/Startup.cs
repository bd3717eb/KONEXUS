using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Integra_Develoment.Startup))]
namespace Integra_Develoment
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
