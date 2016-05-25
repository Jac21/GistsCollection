using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoMapperDemo.Startup))]
namespace AutoMapperDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
