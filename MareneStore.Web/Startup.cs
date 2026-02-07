using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MareneStore.Web.Startup))]
namespace MareneStore.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
