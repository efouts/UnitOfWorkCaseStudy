using System;
using System.Collections.Generic;
using UnitOfWorkCaseStudy.Workflows.Api.Projections;

namespace UnitOfWorkCaseStudy.Workflows.Api.Projectors
{
    public interface IWorkflowStatesProjector
    {
        IEnumerable<StateListProjection> FindAllOnWorkflow(Guid workflowId);
        StateListProjection GetBeginState(Guid workflowId);
        StateListProjection GetEndState(Guid workflowId);
    }
}
