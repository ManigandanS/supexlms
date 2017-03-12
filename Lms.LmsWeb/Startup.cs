using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lms.LmsWeb.Startup))]
namespace Lms.LmsWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
