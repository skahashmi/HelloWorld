using Microsoft.Owin;
using Owin;
using SignalR.EventAggregatorProxy.Owin;

[assembly: OwinStartupAttribute(typeof(SignalR.Event.Web.Startup))]
namespace SignalR.Event.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            app.MapEventProxy<Contract.Events.Event>();
        }
    }
}
