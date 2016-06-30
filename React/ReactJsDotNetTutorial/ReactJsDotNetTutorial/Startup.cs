using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReactJsDotNetTutorial.Startup))]
namespace ReactJsDotNetTutorial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
