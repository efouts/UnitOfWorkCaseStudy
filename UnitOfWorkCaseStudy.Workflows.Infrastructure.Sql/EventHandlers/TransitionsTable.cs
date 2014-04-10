using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.EventHandlers
{
    public class TransitionsTable : IEventHandler<TransitionCreatedEvent>
    {
        public SqlTransaction Transaction { get; set; }
        private SqlConnection connection;

        public TransitionsTable(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Handle(TransitionCreatedEvent @event)
        {
            var sql = @"
                INSERT INTO Transitions (Id, WorkflowId, Name, FromStateId, ToStateId)
                VALUES (@Id, @WorkflowId, @Name, @FromStateId, @ToStateId)";

            using (var command = connection.CreateCommand())
            {
                command.Transaction = Transaction;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@Id", @event.Id);
                command.Parameters.AddWithValue("@WorkflowId", @event.WorkflowId);
                command.Parameters.AddWithValue("@Name", @event.Name);
                command.Parameters.AddWithValue("@FromStateId", @event.FromStateId);
                command.Parameters.AddWithValue("@ToStateId", @event.ToStateId);
                command.ExecuteNonQuery();
            }
        }
    }
}
