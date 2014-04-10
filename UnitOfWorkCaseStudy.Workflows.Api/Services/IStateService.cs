using System;

namespace UnitOfWorkCaseStudy.Workflows.Api.Services
{
    public interface IStateService
    {
        void ShowFieldOnState(Guid workflowId, Guid stateId, Guid fieldId);
        void AddUserAsResponsiblePartyToState(Guid workflowId, Guid stateId, Guid userId);
        void AddFieldAsResponsibleParty(Guid workflowId, Guid stateId, Guid userId);
    }
}