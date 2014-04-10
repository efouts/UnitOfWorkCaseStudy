using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;
using UnitOfWorkCaseStudy.Workflows.Api.Services;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Services.PublishWorkflow
{
    public class IncorrectBeginStateValidator : IPublishWorkflowValidator
    {
        private IWorkflowStatesProjector statesProjector;

        public IncorrectBeginStateValidator(IWorkflowStatesProjector statesProjector)
        {
            this.statesProjector = statesProjector;
        }

        public IEnumerable<ValidationError> Validate(Guid workflowId, WorkflowGraph workflowGraph)
        {
            var errors = new List<ValidationError>();
            var beginStateIds = workflowGraph.FindAllBeginStateIds();

            if (beginStateIds.Count() == 0)
                errors.Add(new ValidationError() { Key = "NoBeginState", Message = String.Empty });

            var actualBeginState = beginStateIds.First();
            var expectedBeginState = statesProjector.GetBeginState(workflowId);

            if (actualBeginState != expectedBeginState.StateId)
                errors.Add(new ValidationError() { Key = "WrongBeginState", Message = actualBeginState.ToString() });

            return errors;
        }
    }
}