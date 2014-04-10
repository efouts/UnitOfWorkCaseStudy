using System;
using UnitOfWorkCaseStudy.Workflows.Api.Services;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;
using UnitOfWorkCaseStudy.Workflows.Domain.Repositories;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Services
{
    public class StateService : IStateService
    {
        private IRepository<Workflow> workflowRepository;

        public StateService(IRepository<Workflow> workflowRepository)
        {
            this.workflowRepository = workflowRepository;
        }

        public void AddFieldAsResponsibleParty(Guid workflowId, Guid stateId, Guid fieldId)
        {
            var workflow = workflowRepository.Get(workflowId);
            workflow.AddFieldAsResponsiblePartyTo(stateId, fieldId);
        }

        public void ShowFieldOnState(Guid workflowId, Guid stateId, Guid fieldId)
        {
            var workflow = workflowRepository.Get(workflowId);
            workflow.AddFieldTo(stateId, fieldId);
        }

        public void AddUserAsResponsiblePartyToState(Guid workflowId, Guid stateId, Guid userId)
        {
            var workflow = workflowRepository.Get(workflowId);
            workflow.AddUserAsResponsiblePartyTo(stateId, userId);
        }
    }
}