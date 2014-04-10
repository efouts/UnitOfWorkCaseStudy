using System;
using System.Collections.Generic;
using UnitOfWorkCaseStudy.Workflows.Api.Projections;

namespace UnitOfWorkCaseStudy.Workflows.Api.Projectors
{
    public interface IWorkflowTransitionsProjector
    {
        IEnumerable<TransitionProjection> FindAllOnWorkflow(Guid workflowId);
    }
}
