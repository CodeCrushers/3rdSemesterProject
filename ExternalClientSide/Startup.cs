using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExternalClientSide.Startup))]
namespace ExternalClientSide
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
