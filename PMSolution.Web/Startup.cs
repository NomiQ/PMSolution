using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PMSolution.Web.Startup))]
namespace PMSolution.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
