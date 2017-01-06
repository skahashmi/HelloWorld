using SignalR.Event.Contract.Events;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using SignalR.WebAPI.Models;
using Xarios.PhoneManager;
using SignalR.WebAPI.Common;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR.WebAPI.Controllers
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
        [Route("PublishEvent")]
        public void PublishEvent([FromBody] string text)
        {
            Post("GenericEvent", "Hello World " + text + "!");
        }

        [HttpPost]
        [Route("ConstrainedEvent")]
        public void ConstrainedEvent([FromBody] string text)
        {
            Post("ConstrainedEvent", text);
        }


        private void Post(string method, string message)
        {
            var client = new HttpClient();
            var url = System.Configuration.ConfigurationManager.AppSettings["WebURL"];
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.PostAsJsonAsync(string.Format("api/service/{0}", method), message);
        }
    }
}