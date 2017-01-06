using Caliburn.Micro;
using SignalR.Event.Contract.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignalR.Event.Web.Controllers
{
    [RoutePrefix("api/Service")]
    public class ServiceController : ApiController
    {
        private readonly IEventAggregator eventAggregator;
        public ServiceController(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        [HttpPost]
        [Route("GenericEvent")]
        public void GenericEvent([FromBody] string text)
        {
            eventAggregator.Publish(new GenericEvent<string>(text));
        }

        [HttpPost]
        [Route("ConstrainedEvent")]
        public void ConstrainedEvent([FromBody] string text)
        {
            eventAggregator.Publish(new ConstrainedEvent(text));
        }

    }
}