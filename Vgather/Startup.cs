using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vgather.Startup))]
namespace Vgather
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
