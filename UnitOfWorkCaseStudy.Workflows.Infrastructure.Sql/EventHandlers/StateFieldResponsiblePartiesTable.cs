using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.EventHandlers
{
    public class StateFieldResponsiblePartiesTable : IEventHandler<FieldAddedAsResponsiblePartyEvent>
    {
        public SqlTransaction Transaction { get; set; }
        private SqlConnection connection;

        public StateFieldResponsiblePartiesTable(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Handle(FieldAddedAsResponsiblePartyEvent @event)
        {
            var sql = @"
                INSERT INTO StateFieldResponsibleParties (StateId, FieldId)
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