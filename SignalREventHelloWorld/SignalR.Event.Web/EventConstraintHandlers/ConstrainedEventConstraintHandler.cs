using SignalR.Event.Contract.Constraints;
using SignalR.Event.Contract.Events;
using SignalR.EventAggregatorProxy.Constraint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.Event.Web.EventConstraintHandlers
{
    public class ConstrainedEventConstraintHandler : EventConstraintHandler<ConstrainedEvent, ConstrainedEventConstraint>
    {
        public override bool Allow(ConstrainedEvent message, ConstraintContext context, ConstrainedEventConstraint constraint)
        {
            return message.Message == constraint.Message;
        }
    }
}