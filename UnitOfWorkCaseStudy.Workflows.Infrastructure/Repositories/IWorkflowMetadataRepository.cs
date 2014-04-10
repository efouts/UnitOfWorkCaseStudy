using System;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Repositories
{
    public interface IWorkflowMetadataRepository
    {
        Metadata Get(Guid id);
    }
}
