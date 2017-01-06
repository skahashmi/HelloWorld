using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using SignalR.EventAggregatorProxy.Owin;

[assembly: OwinStartupAttribute(typeof(SignalR.WebAPI.Startup))]

namespace SignalR.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            app.MapEventProxy<Event.Contract.Events.Event>();
        }
    }
}
