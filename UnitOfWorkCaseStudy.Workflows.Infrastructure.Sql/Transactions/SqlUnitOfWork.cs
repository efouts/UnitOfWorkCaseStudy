using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Transactions;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Transactions
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private IEventProvider eventProvider;
        private SqlConnection connection;
        private IEventHandlerMapper eventHandlerMapper;

        public SqlUnitOfWork(SqlConnection connection, IEventProvider eventProvider, IEventHandlerMapper eventHandlerMapper)
        {
            this.connection = connection;
            this.eventHandlerMapper = eventHandlerMapper;
            this.eventProvider = eventProvider;
        }

        public void Commit()
        {
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var @event in eventProvider.GetEvents())
                    HandleEvent(@event, transaction);

                transaction.Commit();
                eventProvider.Clear();
            }
        }

        private void HandleEvent(dynamic @event, SqlTransaction transaction)
        {
            var handlers = eventHandlerMapper.GetHandlersFor(@event);

            foreach (dynamic handler in handlers)
            {
                handler.Transaction = transaction;
                handler.Handle(@event);
            }
        }
    }
}
