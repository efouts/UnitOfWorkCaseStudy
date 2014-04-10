using System;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Events
{
    public class StateFieldAddedEvent : IEvent
    {
        public Guid WorkflowId { get; private set; }
        public Guid FieldId { get; private set; }
        public Guid StateId { get; private set; }

        public StateFieldAddedEvent(Guid workflowId, Guid stateId, Guid fieldId)
        {
            WorkflowId = workflowId;
            FieldId = fieldId;
            StateId = stateId;
        }
    }
}
