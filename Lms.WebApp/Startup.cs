using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lms.WebApp.Startup))]
namespace Lms.WebApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
