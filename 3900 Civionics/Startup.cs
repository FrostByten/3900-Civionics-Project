using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_3900_Civionics.Startup))]
namespace _3900_Civionics
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
