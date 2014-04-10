using System;
using System.Collections.Generic;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Repositories;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Gateways
{
    public interface IWorkflowFieldReadGateway
    {
        IEnumerable<WorkflowFieldMetadata> Select(Guid id);
    }
}
