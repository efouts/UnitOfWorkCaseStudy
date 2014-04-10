using System;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Events
{
    public class WorkflowCreatedEvent : IEvent
    {
        public Guid Id { get; private set; } 
        public String Title { get; private set; }

        public WorkflowCreatedEvent(Guid id, String title)
        {
            Id = id;
            Title = title;
        }
    }
}
