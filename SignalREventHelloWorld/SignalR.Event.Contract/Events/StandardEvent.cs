using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Event.Contract.Events
{
    public class StandardEvent : Event, IMessageEvent<string>
    {
        public string Message { get; set; }

        public StandardEvent(string message)
        {
            Message = message;
        }
    }
}
