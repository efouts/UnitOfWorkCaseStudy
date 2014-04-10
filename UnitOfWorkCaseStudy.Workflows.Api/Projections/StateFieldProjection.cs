using System;

namespace UnitOfWorkCaseStudy.Workflows.Api.Projections
{
    public class StateFieldProjection
    {
        public Guid StateId { get; set; }
        public Guid FieldId { get; set; }
        public Boolean Editable { get; set; }
        public Boolean Required { get; set; }
    }
}
