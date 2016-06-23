using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDash.Startup))]
namespace WebDash
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
