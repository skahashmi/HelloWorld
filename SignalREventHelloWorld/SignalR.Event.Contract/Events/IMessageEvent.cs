using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Event.Contract.Events
{
    public interface IMessageEvent<TMessage>
    {
        TMessage Message { get; set; }
    }
}
