using System;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Models
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public Guid AggregateId { get { return aggregate.Id; } }

        private AggregateRoot aggregate;
        
        internal void SetAggregateRoot(AggregateRoot aggregate)
        {
            this.aggregate = aggregate;
        }

        internal void LogEvent(IEvent @event)
        {
            aggregate.LogEvent(@event);
        }
    }
}
