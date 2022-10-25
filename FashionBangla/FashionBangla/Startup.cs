using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FashionBangla.Startup))]
namespace FashionBangla
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
