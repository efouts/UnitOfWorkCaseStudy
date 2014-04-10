using System;
using System.Collections.Generic;
using UnitOfWorkCaseStudy.Workflows.Api.Projections;

namespace UnitOfWorkCaseStudy.Workflows.Api.Projectors
{
    public interface IStateResponsiblePartyProjector
    {
        IEnumerable<Guid> FindAllUsersOnState(Guid stateId);
        IEnumerable<Guid> FindAllFieldsOnState(Guid stateId);
    }
}
