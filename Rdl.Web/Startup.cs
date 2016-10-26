using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rdl.Startup))]
namespace Rdl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
