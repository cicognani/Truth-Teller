using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KmovieS.Startup))]
namespace KmovieS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
