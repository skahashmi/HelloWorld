using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Event.Contract.Events
{
    public class GenericEvent<T> : Event, IMessageEvent<T>
    {
        public T Message { get; set; }

        public GenericEvent(T message)
        {
            Message = message;
        }

        public string GetText()
        {
            return Message.ToString();
        }
    }
}
