using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Api.Projections;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Projectors
{
    public class WorkflowTransitionsProjector : IWorkflowTransitionsProjector
    {
        private SqlConnection connection;

        public WorkflowTransitionsProjector(SqlConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<TransitionProjection> FindAllOnWorkflow(Guid workflowId)
        {
            var sql = @"SELECT Id, WorkflowId, FromStateId, ToStateId, Name FROM Transitions WHERE WorkflowId = @WorkflowId";
            var projections = new List<TransitionProjection>();

            using (var command = connection.CreateCommand())
            {
                command.Parameters.AddWithValue("@WorkflowId", workflowId);
                command.CommandText = sql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var projection = new TransitionProjection();
                        projection.Id = Guid.Parse(Convert.ToString(reader["Id"]));
                        projection.WorkflowId = Guid.Parse(Convert.ToString(reader["WorkflowId"]));
                        projection.FromStateId = Guid.Parse(Convert.ToString(reader["FromStateId"]));
                        projection.ToStateId = Guid.Parse(Convert.ToString(reader["ToStateId"]));
                        projection.Name = Convert.ToString(reader["Name"]);
                        projections.Add(projection);
                    }
                }
            }

            return projections;
        }
    }
}
