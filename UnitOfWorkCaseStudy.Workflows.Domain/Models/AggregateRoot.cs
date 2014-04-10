using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Models
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        public Guid Id { get; protected set; }
        public IEnumerable<IEvent> Events { get { return events.ToList(); } }

        private List<IEvent> events;

        public AggregateRoot()
        {
            events = new List<IEvent>();
        }

        internal void LogEvent(IEvent @event)
        {
            events.Add(@event);
        }
    }
}
