using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JuegaOk2.Startup))]
namespace JuegaOk2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
