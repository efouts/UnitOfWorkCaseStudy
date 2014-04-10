using System;
using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Repositories;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Gateways
{
    public class SqlWorkflowsReadGateway : IWorkflowsReadGateway
    {
        private SqlConnection connection;

        public SqlWorkflowsReadGateway(SqlConnection connection)
        {
            this.connection = connection;
        }

        public WorkflowMetadata Select(Guid id)
        {
            var sql = @"SELECT Id, Number, Title FROM Workflows WHERE Id = @Id";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var record = new WorkflowMetadata();

                        record.Id = Guid.Parse(Convert.ToString(reader["Id"]));
                        record.Number = Convert.ToInt32(reader["Number"]);
                        record.Title = Convert.ToString(reader["Title"]);

                        return record;
                    }
                }
            }

            throw new Exception();
        }
    }
}
