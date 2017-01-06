using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IEventAggregator = SignalR.EventAggregatorProxy.EventAggregation.IEventAggregator;

namespace SignalR.Event.Web.EventProxy
{
    public class EventAggregatorProxy : IEventAggregator, IHandle<Event.Contract.Events.Event>
    {
        private Action<object> handler;

        public EventAggregatorProxy(Caliburn.Micro.IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void Subscribe(Action<object> handler)
        {
            this.handler = handler;
        }

        public void Handle(Event.Contract.Events.Event message)
        {
            if (handler != null) //Events can come in before the subsriber is hooked up
                handler(message);
        }
    }
}