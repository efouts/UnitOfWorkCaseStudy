using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.EventHandlers;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Transactions;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Transactions;

namespace UnitOfWorkCaseStudy.Workflows.Tests.Integration.Main
{
    public static class UnitOfWorkFactory
    {
        public static IUnitOfWork CreateUnitOfWork(SqlConnection connection, IEventProvider eventProvider)
        {
            var mapper = new EventHandlerMapper();

            mapper.RegisterHandlersFor<WorkflowCreatedEvent>(new WorkflowsTable(connection));
            mapper.RegisterHandlersFor<TitleChangedEvent>(new WorkflowsTable(connection));
            mapper.RegisterHandlersFor<StateAddedEvent>(new StatesTable(connection));
            mapper.RegisterHandlersFor<TransitionCreatedEvent>(new TransitionsTable(connection));
            mapper.RegisterHandlersFor<FieldAddedEvent>(new WorkflowFieldsTable(connection));
            mapper.RegisterHandlersFor<StateFieldAddedEvent>(new StateFieldsTable(connection));
            mapper.RegisterHandlersFor<UserAddedAsResponsiblePartyEvent>(new StateUserResponsiblePartiesTable(connection));
            mapper.RegisterHandlersFor<FieldAddedAsResponsiblePartyEvent>(new StateFieldResponsiblePartiesTable(connection));
            mapper.RegisterHandlersFor<BeginStateSetEvent>(new WorkflowsTable(connection));
            mapper.RegisterHandlersFor<EndStateSetEvent>(new WorkflowsTable(connection));

            return new SqlUnitOfWork(connection, eventProvider, mapper);
        }
    }
}
