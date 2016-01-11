using Microsoft.Owin;
using Owin;
using Website;

[assembly: OwinStartup(typeof(Startup))]
namespace Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}