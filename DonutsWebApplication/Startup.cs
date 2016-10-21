using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DonutsWebApplication.Startup))]
namespace DonutsWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
