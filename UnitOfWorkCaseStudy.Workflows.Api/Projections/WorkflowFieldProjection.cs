using System;

namespace UnitOfWorkCaseStudy.Workflows.Api.Projections
{
    public class WorkflowFieldProjection
    {
        public Guid WorkflowId { get; set; }
        public Guid FieldId { get; set; }
    }
}
