using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Repositories;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Gateways
{
    public class SqlStatesReadGateway : IStatesReadGateway
    {
        private SqlConnection connection;

        public SqlStatesReadGateway(SqlConnection connection)
        {
            this.connection = connection;
        }

        public StateMetadata Select(Guid id)
        {
            var sql = @"SELECT Id, WorkflowId, Title FROM States WHERE Id = @Id";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.AddWithValue("@Id", id);
                
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var record = new StateMetadata();

                        record.Id = Guid.Parse(Convert.ToString(reader["Id"]));
                        record.WorkflowId = Guid.Parse(Convert.ToString(reader["WorkflowId"]));
                        record.Title = Convert.ToString(reader["Title"]);

                        return record;
                    }
                }
            }

            throw new Exception();
        }


        public IEnumerable<StateMetadata> SelectAll(Guid workflowId)
        {
            var sql = @"SELECT Id, WorkflowId, Title FROM States WHERE WorkflowId = @WorkflowId";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.AddWithValue("@WorkflowId", workflowId);

                using (var reader = command.ExecuteReader())
                {
                    var records = new List<StateMetadata>();

                    while (reader.Read())
                    {
                        var record = new StateMetadata();

                        record.Id = Guid.Parse(Convert.ToString(reader["Id"]));
                        record.WorkflowId = Guid.Parse(Convert.ToString(reader["WorkflowId"]));
                        record.Title = Convert.ToString(reader["Title"]);

                        records.Add(record);
                    }

                    return records;
                }
            }
        }
    }
}
