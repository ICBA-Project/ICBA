using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ICBA.Web.Startup))]
namespace ICBA.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
