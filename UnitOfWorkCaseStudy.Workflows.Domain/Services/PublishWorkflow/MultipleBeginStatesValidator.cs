using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Services.PublishWorkflow
{
    public class MultipleBeginStatesValidator : IPublishWorkflowValidator
    {
        public IEnumerable<ValidationError> Validate(Guid workflowId, WorkflowGraph workflowGraph)
        {
            var beginStateIds = workflowGraph.FindAllBeginStateIds();
            
            if (beginStateIds.Count() > 1)
                foreach (var id in beginStateIds)
                    yield return new ValidationError() { Key = "MultipleBeginStates", Message = Convert.ToString(id) };
        }
    }
}
