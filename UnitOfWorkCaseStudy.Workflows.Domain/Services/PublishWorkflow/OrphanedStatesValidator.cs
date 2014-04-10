using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkCaseStudy.Workflows.Api.Services;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Services.PublishWorkflow
{
    public class OrphanedStatesValidator : IPublishWorkflowValidator
    {
        public IEnumerable<ValidationError> Validate(Guid workflowId, WorkflowGraph workflowGraph)
        {
            var orphanedStates = workflowGraph.GetOrphanedStates();
            foreach (var stateId in orphanedStates)
                yield return new ValidationError() { Key = "OrphanedState", Message = Convert.ToString(stateId) };
        }
    }
}
