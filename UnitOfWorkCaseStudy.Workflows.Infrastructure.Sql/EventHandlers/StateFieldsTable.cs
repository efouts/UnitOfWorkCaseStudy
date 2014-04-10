using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.EventHandlers
{
    public class StateFieldsTable : IEventHandler<StateFieldAddedEvent>
    {
        public SqlTransaction Transaction { get; set; }
        private SqlConnection connection;

        public StateFieldsTable(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Handle(StateFieldAddedEvent @event)
        {
            var sql = @"
                INSERT INTO StateFields (StateId, FieldId)
                VALUES (@StateId, @FieldId)";

            using (var command = connection.CreateCommand())
            {
                command.Transaction = Transaction;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@StateId", @event.StateId);
                command.Parameters.AddWithValue("@FieldId", @event.FieldId);
                command.ExecuteNonQuery();
            }
        }
    }
}
