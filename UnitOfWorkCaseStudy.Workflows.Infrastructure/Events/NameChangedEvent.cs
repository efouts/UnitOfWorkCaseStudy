using System;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Events
{
    public class TitleChangedEvent : IEvent
    {
        public Guid WorkflowId { get; private set; }
        public String Title { get; private set; }

        public TitleChangedEvent(Guid workflowId, String title)
        {
            WorkflowId = workflowId;
            Title = title;
        }
    }
}
