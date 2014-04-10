using System;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Events
{
    public class UserAddedAsResponsiblePartyEvent : IEvent
    {
        public Guid WorkflowId { get; private set; }
        public Guid StateId { get; private set; }
        public Guid UserId { get; private set; }

        public UserAddedAsResponsiblePartyEvent(Guid workflowId, Guid stateId, Guid userId)
        {
            WorkflowId = workflowId;
            StateId = stateId;
            UserId = userId;
        }
    }
}
