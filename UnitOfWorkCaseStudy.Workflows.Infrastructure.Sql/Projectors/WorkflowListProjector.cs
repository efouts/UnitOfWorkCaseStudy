
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Api.Projections;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Projectors
{
    public class WorkflowListProjector : IWorkflowListProjector
    {
        private SqlConnection connection;

        public WorkflowListProjector(SqlConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<WorkflowListProjection> FindAll()
        {
            var sql = @"SELECT Id, Title FROM Workflows";
            var projections = new List<WorkflowListProjection>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var projection = new WorkflowListProjection();
                        projection.Id = Guid.Parse(Convert.ToString(reader["Id"]));
                        projection.Title = Convert.ToString(reader["Title"]);
                        projections.Add(projection);
                    }
                }
            }

            return projections;
        }

        public WorkflowListProjection Get(Guid id)
        {
            var sql = @"SELECT Id, Title FROM Workflows WHERE Id = @Id";

            using (var command = connection.CreateCommand())
            {
                command.Parameters.AddWithValue("@Id", id);
                command.CommandText = sql;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var projection = new WorkflowListProjection();
                        projection.Id = Guid.Parse(Convert.ToString(reader["Id"]));
                        projection.Title = Convert.ToString(reader["Title"]);
                        return projection;
                    }
                }
            }

            throw new Exception();
        }
    }
}
