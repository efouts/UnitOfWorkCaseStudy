using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.EventHandlers
{
    public class WorkflowsTable : IEventHandler<TitleChangedEvent>, 
                                  IEventHandler<WorkflowCreatedEvent>, 
                                  IEventHandler<BeginStateSetEvent>,
                                  IEventHandler<EndStateSetEvent>
    {
        public SqlTransaction Transaction { get; set; }
        private SqlConnection connection;

        public WorkflowsTable(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Handle(WorkflowCreatedEvent @event)
        {
            var sql = @"
                INSERT INTO Workflows (Id, Number, Title) VALUES (@Id, 0, @Title)";

            using (var command = connection.CreateCommand())
            {
                command.Transaction = Transaction;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@Id", @event.Id);
                command.Parameters.AddWithValue("@Title", @event.Title);
                command.ExecuteNonQuery();
            }
        }

        public void Handle(TitleChangedEvent @event)
        {
            var sql = @"UPDATE Workflows SET Title = @Title WHERE Id = @Id";

            using (var command = connection.CreateCommand())
            {
                command.Transaction = Transaction;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@Id", @event.WorkflowId);
                command.Parameters.AddWithValue("@Title", @event.Title);
                command.ExecuteNonQuery();
            }
        }

        public void Handle(BeginStateSetEvent @event)
        {
            var sql = @"UPDATE Workflows SET BeginStateId = @BeginStateId WHERE Id = @Id";

            using (var command = connection.CreateCommand())
            {
                command.Transaction = Transaction;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@Id", @event.WorkflowId);
                command.Parameters.AddWithValue("@BeginStateId", @event.BeginStateId);
                command.ExecuteNonQuery();
            }
        }

        public void Handle(EndStateSetEvent @event)
        {
            var sql = @"UPDATE Workflows SET EndStateId = @EndStateId WHERE Id = @Id";

            using (var command = connection.CreateCommand())
            {
                command.Transaction = Transaction;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@Id", @event.WorkflowId);
                command.Parameters.AddWithValue("@EndStateId", @event.EndStateId);
                command.ExecuteNonQuery();
            }
        }
    }
}
