using System;
using System.Collections.Generic;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Repositories;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Gateways
{
    public interface IStatesReadGateway
    {
        StateMetadata Select(Guid id);
        IEnumerable<StateMetadata> SelectAll(Guid workflowId);
    }
}
