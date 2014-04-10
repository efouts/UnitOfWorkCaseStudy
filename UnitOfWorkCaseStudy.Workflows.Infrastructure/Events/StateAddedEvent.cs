using System;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Events
{
    public class StateAddedEvent : IEvent
    {
        public Guid Id { get; private set; }
        public Guid WorkflowId { get; private set; }
        public String Title { get; private set; }

        public StateAddedEvent(Guid workflowId, Guid stateId, String title)
        {
            Id = stateId;
            WorkflowId = workflowId;
            Title = title;
        }
    }
}