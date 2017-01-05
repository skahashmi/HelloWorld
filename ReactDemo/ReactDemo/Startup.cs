using Microsoft.Owin;
using Owin;
using React;

[assembly: OwinStartupAttribute(typeof(ReactDemo.Startup))]
namespace ReactDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ReactSiteConfiguration.Configuration = new ReactSiteConfiguration()
                .AddScript("~/Scripts/App/HelloWorld.jsx");
            ConfigureAuth(app);
        }
    }
}
