using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EComerceMVC.Startup))]
namespace EComerceMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
