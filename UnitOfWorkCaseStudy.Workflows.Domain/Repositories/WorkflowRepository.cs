using System;
using UnitOfWorkCaseStudy.Workflows.Domain.Creation;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;
using UnitOfWorkCaseStudy.Workflows.Domain.Transactions;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Repositories;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Repositories
{
    public class WorkflowRepository : IRepository<Workflow>
    {
        private IAggregateTracker aggregateTracker;
        private IWorkflowFactory workflowFactory;
        private IWorkflowMetadataRepository metadataRepository;

        public WorkflowRepository(IAggregateTracker aggregateTracker, IWorkflowFactory workflowFactory,
            IWorkflowMetadataRepository metadataRepository)
        {
            this.aggregateTracker = aggregateTracker;
            this.workflowFactory = workflowFactory;
            this.metadataRepository = metadataRepository;
        }

        public void Add(Workflow workflow)
        {
            aggregateTracker.Track(workflow);
        }

        public Workflow Get(Guid id)
        {
            var metadata = metadataRepository.Get(id);
            var workflow = workflowFactory.Reconstitute(metadata);
            aggregateTracker.Track(workflow);
            return workflow;
        }
    }
}
