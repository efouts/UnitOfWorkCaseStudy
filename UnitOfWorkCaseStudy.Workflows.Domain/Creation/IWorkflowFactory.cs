using System;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Repositories;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Creation
{
    public interface IWorkflowFactory
    {
        Workflow Create(Guid id, String title);
        Workflow Reconstitute(Metadata metadata);
    }
}
