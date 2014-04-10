using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Api.Services;
using UnitOfWorkCaseStudy.Workflows.Domain.Creation;
using UnitOfWorkCaseStudy.Workflows.Domain.Repositories;
using UnitOfWorkCaseStudy.Workflows.Domain.Services;
using UnitOfWorkCaseStudy.Workflows.Domain.Transactions;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Gateways;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Repositories;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Transactions;

namespace UnitOfWorkCaseStudy.Workflows.Tests.Integration.Main
{
    public static class WorkflowServiceFactory
    {
        public static IWorkflowService CreateWorkflowService(SqlConnection connection, IUnitOfWork unitOfWork, IAggregateTracker aggregateTracker)
        {
            var factory = new WorkflowFactory();

            var workflowsGateway = new SqlWorkflowsReadGateway(connection);
            var statesGateway = new SqlStatesReadGateway(connection);
            var fieldsGateway = new SqlWorkflowFieldsReadGateway(connection);
            var metadataRepository = new WorkflowMetadataRepository(workflowsGateway, statesGateway, fieldsGateway);

            var workflowRepository = new WorkflowRepository(aggregateTracker, factory, metadataRepository);

            return new WorkflowService(factory, workflowRepository);
        }

        public static IStateService CreateStateService(SqlConnection connection, IUnitOfWork unitOfWork, IAggregateTracker aggregateTracker)
        {
            var factory = new WorkflowFactory();

            var workflowsGateway = new SqlWorkflowsReadGateway(connection);
            var statesGateway = new SqlStatesReadGateway(connection);
            var fieldsGateway = new SqlWorkflowFieldsReadGateway(connection);
            var metadataRepository = new WorkflowMetadataRepository(workflowsGateway, statesGateway, fieldsGateway);

            var workflowRepository = new WorkflowRepository(aggregateTracker, factory, metadataRepository);

            return new StateService(workflowRepository);
        }
    }
}
