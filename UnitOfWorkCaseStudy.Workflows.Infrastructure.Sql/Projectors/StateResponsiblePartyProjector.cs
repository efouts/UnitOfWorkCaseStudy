using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Api.Projections;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Projectors
{
    public class StateResponsiblePartyProjector : IStateResponsiblePartyProjector
    {
        private SqlConnection connection;

        public StateResponsiblePartyProjector(SqlConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<Guid> FindAllUsersOnState(Guid stateId)
        {
            var sql = @"
                SELECT UserId
                FROM StateUserResponsibleParties
                WHERE StateId = @StateId";

            var projections = new List<Guid>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;

                var parameter = new SqlParameter("@StateId", stateId);
                command.Parameters.Add(parameter);

                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        projections.Add(Guid.Parse(Convert.ToString(reader["UserId"])));
            }

            return projections;
        }

        public IEnumerable<Guid> FindAllFieldsOnState(Guid stateId)
        {
            var sql = @"
                SELECT FieldId
                FROM StateFieldResponsibleParties
                WHERE StateId = @StateId";

            var projections = new List<Guid>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;

                var parameter = new SqlParameter("@StateId", stateId);
                command.Parameters.Add(parameter);

                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        projections.Add(Guid.Parse(Convert.ToString(reader["FieldId"])));
            }

            return projections;
        }
    }
}