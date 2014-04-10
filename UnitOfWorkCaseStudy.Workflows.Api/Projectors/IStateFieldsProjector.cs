using System;
using System.Collections.Generic;
using UnitOfWorkCaseStudy.Workflows.Api.Projections;

namespace UnitOfWorkCaseStudy.Workflows.Api.Projectors
{
    public interface IStateFieldsProjector
    {
        IEnumerable<StateFieldProjection> FindAllOnState(Guid stateId);
    }
}