using System;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Repositories;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Gateways
{
    public interface IWorkflowsReadGateway
    {
        WorkflowMetadata Select(Guid id);
    }
}
