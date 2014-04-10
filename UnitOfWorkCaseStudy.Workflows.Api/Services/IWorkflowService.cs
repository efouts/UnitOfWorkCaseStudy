using System;

namespace UnitOfWorkCaseStudy.Workflows.Api.Services
{
    public interface IWorkflowService
    {
        void Create(Guid id, String title);
        void Rename(Guid id, String title);
        void AddField(Guid id, Guid fieldId);
        void AddState(Guid workflowId, String title);
        void AddTransition(Guid workflowId, Guid fromStateId, Guid toStateId, String name);
        void ChangeBeginState(Guid workflowId, Guid stateId);
        void ChangeEndState(Guid workflowId, Guid stateId);
    }
}
