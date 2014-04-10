using System;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Repositories;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Gateways;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Repositories
{
    public class WorkflowMetadataRepository : IWorkflowMetadataRepository
    {
        private IWorkflowsReadGateway workflowsGateway;
        private IStatesReadGateway statesGateway;
        private IWorkflowFieldReadGateway workflowFieldGateway;

        public WorkflowMetadataRepository(IWorkflowsReadGateway workflowsGateway, IStatesReadGateway statesGateway,
            IWorkflowFieldReadGateway workflowFieldGateway)
        {
            this.workflowsGateway = workflowsGateway;
            this.statesGateway = statesGateway;
            this.workflowFieldGateway = workflowFieldGateway;
        }

        public Metadata Get(Guid workflowId)
        {
            var workflowMetadata = workflowsGateway.Select(workflowId);
            var stateRecords = statesGateway.SelectAll(workflowId);
            var workflowFieldMetas = workflowFieldGateway.Select(workflowId);

            var someMetadata = new Metadata();
            someMetadata.WorkflowMetadata = workflowMetadata;
            someMetadata.FieldsMetadata = workflowFieldMetas;
            someMetadata.StatesMetadata = stateRecords;

            return someMetadata;
        }
    }
}
