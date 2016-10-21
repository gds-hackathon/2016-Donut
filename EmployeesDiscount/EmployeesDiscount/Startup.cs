using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployeesDiscount.Startup))]
namespace EmployeesDiscount
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
