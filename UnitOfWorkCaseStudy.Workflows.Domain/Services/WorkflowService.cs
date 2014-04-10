using System;
using UnitOfWorkCaseStudy.Workflows.Api.Services;
using UnitOfWorkCaseStudy.Workflows.Domain.Creation;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;
using UnitOfWorkCaseStudy.Workflows.Domain.Repositories;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Services
{
    public class WorkflowService : IWorkflowService
    {
        private IRepository<Workflow> workflowRepository;
        private IWorkflowFactory workflowFactory;

        public WorkflowService(IWorkflowFactory workflowFactory, IRepository<Workflow> workflowRepository)
        {
            this.workflowFactory = workflowFactory;
            this.workflowRepository = workflowRepository;
        }

        public void Create(Guid id, String title)
        {
            var workflow = workflowFactory.Create(id, title);
            workflow.AddState(Guid.NewGuid(), "Baby Node");

            workflowRepository.Add(workflow);
        }

        public void Rename(Guid id, String title)
        {
            var workflow = workflowRepository.Get(id);
            workflow.ChangeTitle(title);
        }

        public void AddState(Guid workflowId, String title)
        {
            var workflow = workflowRepository.Get(workflowId);
            workflow.AddState(Guid.NewGuid(), title);
        }

        public void AddTransition(Guid workflowId, Guid fromStateId, Guid toStateId, String name)
        {
            var workflow = workflowRepository.Get(workflowId);
            workflow.AddTransition(fromStateId, toStateId, name);
        }

        public void AddField(Guid id, Guid fieldId)
        {
            var workflow = workflowRepository.Get(id);
            workflow.AddField(fieldId);
        }

        public void ChangeBeginState(Guid workflowId, Guid stateId)
        {
            var workflow = workflowRepository.Get(workflowId);
            workflow.SetBeginState(stateId);
        }

        public void ChangeEndState(Guid workflowId, Guid stateId)
        {
            var workflow = workflowRepository.Get(workflowId);
            workflow.SetEndState(stateId);
        }
    }
}
