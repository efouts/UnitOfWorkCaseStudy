using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Api.Projections;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Projectors
{
    public class WorkflowFieldsProjector : IWorkflowFieldsProjector
    {
        private SqlConnection connection;

        public WorkflowFieldsProjector(SqlConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<WorkflowFieldProjection> FindAllOnWorkflow(Guid workflowId)
        {
            var sql = @"
                SELECT WorkflowId, FieldId
                FROM WorkflowFields
                WHERE WorkflowId = @WorkflowId";

            var projections = new List<WorkflowFieldProjection>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;

                var parameter = new SqlParameter("@WorkflowId", workflowId);
                command.Parameters.Add(parameter);                

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var projection = new WorkflowFieldProjection();
                        projection.WorkflowId = Guid.Parse(Convert.ToString(reader["WorkflowId"]));
                        projection.FieldId = Guid.Parse(Convert.ToString(reader["FieldId"]));
                        projections.Add(projection);
                    }
                }
            }

            return projections;
        }
    }
}
