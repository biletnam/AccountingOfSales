using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AccountingOfSales.Startup))]
namespace AccountingOfSales
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
