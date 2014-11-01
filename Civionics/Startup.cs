using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Civionics.Startup))]
namespace Civionics
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
