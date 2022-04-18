using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Machinetest.Startup))]
namespace Machinetest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
