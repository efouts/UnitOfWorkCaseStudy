using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Api.Projections;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Projectors
{
    public class WorkflowStateFieldsProjector : IStateFieldsProjector
    {
        private SqlConnection connection;

        public WorkflowStateFieldsProjector(SqlConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<StateFieldProjection> FindAllOnState(Guid stateId)
        {
            var sql = @"
                SELECT StateId, FieldId, Editable, Required
                FROM StateFields
                WHERE StateId = @StateId";

            var projections = new List<StateFieldProjection>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;

                var parameter = new SqlParameter("@StateId", stateId);
                command.Parameters.Add(parameter);                

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var projection = new StateFieldProjection();
                        projection.StateId = Guid.Parse(Convert.ToString(reader["StateId"]));
                        projection.FieldId = Guid.Parse(Convert.ToString(reader["FieldId"]));
                        projection.Editable = Convert.ToBoolean(reader["Editable"]);
                        projection.Required = Convert.ToBoolean(reader["Required"]);
                        projections.Add(projection);
                    }
                }
            }

            return projections;
        }
    }
}
