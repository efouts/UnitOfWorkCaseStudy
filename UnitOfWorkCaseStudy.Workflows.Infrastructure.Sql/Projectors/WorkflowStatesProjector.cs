using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Api.Projections;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Projectors
{
    public class WorkflowStatesProjector : IWorkflowStatesProjector
    {
        private SqlConnection connection;

        public WorkflowStatesProjector(SqlConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<StateListProjection> FindAllOnWorkflow(Guid workflowId)
        {
            var sql = @"SELECT Id, WorkflowId, Title FROM States WHERE WorkflowId = @WorkflowId";
            var projections = new List<StateListProjection>();

            using (var command = connection.CreateCommand())
            {
                command.Parameters.AddWithValue("@WorkflowId", workflowId);
                command.CommandText = sql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var projection = new StateListProjection();
                        projection.StateId = Guid.Parse(Convert.ToString(reader["Id"]));
                        projection.Title = Convert.ToString(reader["Title"]);
                        projections.Add(projection);
                    }
                }
            }

            return projections;
        }

        public StateListProjection GetBeginState(Guid workflowId)
        {
            var sql = @"
                SELECT w.BeginStateId, w.Id, s.Title
                FROM Workflows w
                INNER JOIN States s ON w.BeginStateId = s.Id
                WHERE w.Id = @WorkflowId";

            using (var command = connection.CreateCommand())
            {
                command.Parameters.AddWithValue("@WorkflowId", workflowId);
                command.CommandText = sql;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var projection = new StateListProjection();
                        projection.StateId = Guid.Parse(Convert.ToString(reader["BeginStateId"]));
                        projection.Title = Convert.ToString(reader["Title"]);
                        return projection;
                    }
                }
            }

            return null;
        }

        public StateListProjection GetEndState(Guid workflowId)
        {
            var sql = @"
                SELECT w.EndStateId, w.Id, s.Title
                FROM Workflows w
                INNER JOIN States s ON w.EndStateId = s.Id
                WHERE w.Id = @WorkflowId";

            using (var command = connection.CreateCommand())
            {
                command.Parameters.AddWithValue("@WorkflowId", workflowId);
                command.CommandText = sql;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var projection = new StateListProjection();
                        projection.StateId = Guid.Parse(Convert.ToString(reader["EndStateId"]));
                        projection.Title = Convert.ToString(reader["Title"]);
                        return projection;
                    }
                }
            }

            return null;
        }
    }
}