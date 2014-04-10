using System;
using System.Collections.Generic;
using UnitOfWorkCaseStudy.Workflows.Api.Projections;

namespace UnitOfWorkCaseStudy.Workflows.Api.Projectors
{
    public interface IWorkflowFieldsProjector
    {
        IEnumerable<WorkflowFieldProjection> FindAllOnWorkflow(Guid workflowId);
    }
}
