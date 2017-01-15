using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CampBg.Web.Startup))]

namespace CampBg.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
