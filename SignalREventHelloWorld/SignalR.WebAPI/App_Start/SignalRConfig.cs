using Owin;
using SignalR.EventAggregatorProxy.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.WebAPI.App_Start
{
    public static class SignalRConfig
    {
        public static void Configuration(IAppBuilder app)
        {
            app.MapSignalR();

            /*
             * This demo is based on Ninject as dependecy resolver. If your project is small and do not have a Resolver you  can use the Resolver built into SignalR.
             * Make sure you register your event aggregator proxy after MapSignalR otherwise it will override all registered types.
             */

            //var proxy = new Lazy<IEventAggregator>(() => new MyEventAggregatorProxy());
            //GlobalHost.DependencyResolver.Register(typeof(IEventAggregator), () => proxy.Value);

            app.MapEventProxy<Event.Contract.Events.Event>();
        }
    }
}