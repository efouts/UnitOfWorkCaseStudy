using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.EventHandlers
{
    public class StatesTable : IEventHandler<StateAddedEvent>
    {
        public SqlTransaction Transaction { get; set; }
        private SqlConnection connection;

        public StatesTable(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Handle(StateAddedEvent @event)
        {
            var sql = "INSERT INTO States (Id, WorkflowId, Title) VALUES (@Id, @WorkflowId, @Title)";

            using (var command = connection.CreateCommand())
            {
                command.Transaction = Transaction;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@Id", @event.Id);
                command.Parameters.AddWithValue("@WorkflowId", @event.WorkflowId);
                command.Parameters.AddWithValue("@Title", @event.Title);
                command.ExecuteNonQuery();
            }
        }

    }
}
