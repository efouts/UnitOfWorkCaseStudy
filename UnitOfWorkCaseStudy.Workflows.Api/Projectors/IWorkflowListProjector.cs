using System;
using System.Collections.Generic;
using UnitOfWorkCaseStudy.Workflows.Api.Projections;

namespace UnitOfWorkCaseStudy.Workflows.Api.Projectors
{
    public interface IWorkflowListProjector
    {
        IEnumerable<WorkflowListProjection> FindAll();
        WorkflowListProjection Get(Guid id);
    }
}
