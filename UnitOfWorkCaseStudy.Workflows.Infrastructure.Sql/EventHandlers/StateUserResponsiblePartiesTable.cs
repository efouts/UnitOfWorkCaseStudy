using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.EventHandlers
{
    public class StateUserResponsiblePartiesTable : IEventHandler<UserAddedAsResponsiblePartyEvent>
    {
        public SqlTransaction Transaction { get; set; }
        private SqlConnection connection;

        public StateUserResponsiblePartiesTable(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Handle(UserAddedAsResponsiblePartyEvent @event)
        {
            var sql = @"
                INSERT INTO StateUserResponsibleParties (StateId, UserId)
                VALUES (@StateId, @UserId)";

            using (var command = connection.CreateCommand())
            {
                command.Transaction = Transaction;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@StateId", @event.StateId);
                command.Parameters.AddWithValue("@UserId", @event.UserId);
                command.ExecuteNonQuery();
            }
        }
    }
}
