using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaxWeb.Startup))]
namespace TaxWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
