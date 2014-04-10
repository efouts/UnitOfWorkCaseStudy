using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Services.PublishWorkflow
{
    public interface IPublishWorkflowValidator
    {
        IEnumerable<ValidationError> Validate(Guid workflowId, WorkflowGraph workflowGraph);
    }

    public class ValidationError
    {
        public String Key { get; set; }
        public String Message { get; set; }
    }
}
