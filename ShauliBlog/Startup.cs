using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShauliBlog.Startup))]
namespace ShauliBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
