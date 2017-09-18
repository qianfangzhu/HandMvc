using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hand.Web.Startup))]
namespace Hand.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
