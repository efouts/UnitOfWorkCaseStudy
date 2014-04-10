using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.EventHandlers
{
    public class WorkflowFieldsTable : IEventHandler<FieldAddedEvent>
    {
        public SqlTransaction Transaction { get; set; }
        private SqlConnection connection;

        public WorkflowFieldsTable(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Handle(FieldAddedEvent @event)
        {
            var sql = @"
                INSERT INTO WorkflowFields (WorkflowId, FieldId)
                VALUES (@WorkflowId, @FieldId)";

            using (var command = connection.CreateCommand())
            {
                command.Transaction = Transaction;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@WorkflowId", @event.WorkflowId);
                command.Parameters.AddWithValue("@FieldId", @event.FieldId);
                command.ExecuteNonQuery();
            }
        }
    }
}
