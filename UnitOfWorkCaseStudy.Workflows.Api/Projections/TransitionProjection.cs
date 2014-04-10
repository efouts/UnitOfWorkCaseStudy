using System;

namespace UnitOfWorkCaseStudy.Workflows.Api.Projections
{
    public class TransitionProjection
    {
        public Guid FromStateId { get; set; }
        public Guid Id { get; set; }
        public Guid ToStateId { get; set; }
        public String Name { get; set; }
        public Guid WorkflowId { get; set; }
    }
}
