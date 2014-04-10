using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Repositories;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Gateways
{
    public class SqlWorkflowFieldsReadGateway : IWorkflowFieldReadGateway
    {
        private SqlConnection connection;

        public SqlWorkflowFieldsReadGateway(SqlConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<WorkflowFieldMetadata> Select(Guid workflowId)
        {
            var sql = @"
                SELECT WorkflowId, FieldId
                FROM WorkflowFields
                WHERE WorkflowId = @WorkflowId";

            var metas = new List<WorkflowFieldMetadata>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;

                var parameter = new SqlParameter("@WorkflowId", workflowId);
                command.Parameters.Add(parameter);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var meta = new WorkflowFieldMetadata();
                        meta.WorkflowId = Guid.Parse(Convert.ToString(reader["WorkflowId"]));
                        meta.FieldId = Guid.Parse(Convert.ToString(reader["FieldId"]));
                        metas.Add(meta);
                    }
                }
            }

            return metas;
        }
    }
}
