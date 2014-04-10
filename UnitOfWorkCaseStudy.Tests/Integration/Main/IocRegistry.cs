using StructureMap;
using StructureMap.Configuration.DSL;
using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;
using UnitOfWorkCaseStudy.Workflows.Api.Services;
using UnitOfWorkCaseStudy.Workflows.Domain.Creation;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;
using UnitOfWorkCaseStudy.Workflows.Domain.Repositories;
using UnitOfWorkCaseStudy.Workflows.Domain.Transactions;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Gateways;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Projectors;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Repositories;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Transactions;

namespace UnitOfWorkCaseStudy.Workflows.Tests.Integration.Main
{
    public class IocRegistry : Registry
    {
        public IocRegistry()
        {
            For<IWorkflowListProjector>().Use<WorkflowListProjector>();
            For<IWorkflowStatesProjector>().Use<WorkflowStatesProjector>();
            For<IWorkflowTransitionsProjector>().Use<WorkflowTransitionsProjector>();
            For<IWorkflowFieldsProjector>().Use<WorkflowFieldsProjector>();
            For<IStateFieldsProjector>().Use<WorkflowStateFieldsProjector>();
            For<IWorkflowService>().Use(ResolveWorkflowService);
            For<IStateResponsiblePartyProjector>().Use<StateResponsiblePartyProjector>();
            For<IStateService>().Use(ResolveStateService);
            For<IWorkflowFactory>().Use<WorkflowFactory>();
            For<IRepository<Workflow>>().Use(ResolveWorkflowRepository);
        }

        private IWorkflowService ResolveWorkflowService(IContext context)
        {
            var connection = context.GetInstance<SqlConnection>();
            var eventProvider = context.GetInstance<IEventProvider>();
            var unitOfWork = context.GetInstance<IUnitOfWork>();

            var aggregateTracker = context.GetInstance<IAggregateTracker>();
            return WorkflowServiceFactory.CreateWorkflowService(connection, unitOfWork, aggregateTracker);
        }

        private IStateService ResolveStateService(IContext context)
        {
            var connection = context.GetInstance<SqlConnection>();
            var eventProvider = context.GetInstance<IEventProvider>();
            var unitOfWork = context.GetInstance<IUnitOfWork>();

            var aggregateTracker = context.GetInstance<IAggregateTracker>();
            return WorkflowServiceFactory.CreateStateService(connection, unitOfWork, aggregateTracker);
        }

        private IRepository<Workflow> ResolveWorkflowRepository(IContext context)
        {
            var aggregateTracker = context.GetInstance<IAggregateTracker>();
            var workflowFactory = new WorkflowFactory();

            var connection = context.GetInstance<SqlConnection>();
            var workflowsGateway = new SqlWorkflowsReadGateway(connection);
            var statesGateway = new SqlStatesReadGateway(connection);
            var workflowFieldsGateway = new SqlWorkflowFieldsReadGateway(connection);
            var metadataRepository = new WorkflowMetadataRepository(workflowsGateway, statesGateway, workflowFieldsGateway);
            return new WorkflowRepository(aggregateTracker, workflowFactory, metadataRepository);
        }
    }
}
