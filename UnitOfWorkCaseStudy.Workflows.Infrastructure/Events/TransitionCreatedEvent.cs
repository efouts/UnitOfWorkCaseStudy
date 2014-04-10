using System;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Events
{
    public class TransitionCreatedEvent : IEvent
    {
        public Guid FromStateId { get; private set; }
        public Guid Id { get; private set; }
        public Guid ToStateId { get; private set; }
        public String Name { get; private set; }
        public Guid WorkflowId { get; private set; }

        public TransitionCreatedEvent(Guid workflowId, Guid transitionId, Guid fromStateId, Guid toStateId, String name)
        {
            FromStateId = fromStateId;
            Id = transitionId;
            WorkflowId = workflowId;
            ToStateId = toStateId;
            Name = name;
        }
    }
}
