using System;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Events
{
    public class FieldAddedAsResponsiblePartyEvent : IEvent
    {
        public Guid WorkflowId { get; private set; }
        public Guid StateId { get; private set; }
        public Guid FieldId { get; private set; }

        public FieldAddedAsResponsiblePartyEvent(Guid workflowId, Guid stateId, Guid fieldId)
        {
            WorkflowId = workflowId;
            StateId = stateId;
            FieldId = fieldId;
        }
    }
}