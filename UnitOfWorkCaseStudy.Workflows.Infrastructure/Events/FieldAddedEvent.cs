using System;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Events
{
    public class FieldAddedEvent : IEvent
    {
        public Guid WorkflowId { get; private set; }
        public Guid FieldId { get; private set; }

        public FieldAddedEvent(Guid workflowId, Guid fieldId)
        {
            WorkflowId = workflowId;
            FieldId = fieldId;
        }
    }
}
