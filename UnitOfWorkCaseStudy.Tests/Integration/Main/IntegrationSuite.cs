using log4net.Config;
using StructureMap;
using System.Data.SqlClient;
using System.IO;
using UnitOfWorkCaseStudy.Tests.Integration;
using UnitOfWorkCaseStudy.Workflows.Domain.Transactions;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Transactions;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Transactions;

namespace UnitOfWorkCaseStudy.Workflows.Tests.Integration.Main
{
    public static class IntegrationSuite
    {
        public static IContainer IocContainer { get; private set; }

        private static IUnitOfWork unitOfWork;
        private static SqlConnection connection;
        private static AggregateTracker aggregateTracker;

        static IntegrationSuite()
        {
            XmlConfigurator.Configure(new FileInfo("Log4Net.xml"));

            var connectionString = @"Server=.;Database=UnitOfWorkCaseStudy.Database;Integrated Security=SSPI;";
            connection = new SqlConnection(connectionString);

            aggregateTracker = new AggregateTracker();

            IocContainer = new Container(c =>
            {
                c.For<SqlConnection>().Use(() => connection);
                c.For<IAggregateTracker>().Use(() => aggregateTracker);
                c.For<IEventProvider>().Use(() => aggregateTracker);
                c.For<IUnitOfWork>().Use(() => unitOfWork);
                c.AddRegistry<IocRegistry>();

                c.SetAllProperties(s =>
                {
                    s.Matching(r => typeof(IntegrationTest).IsAssignableFrom(r.DeclaringType));
                });
            });
        }      

        public static void OpenConnection()
        {
            connection.Open();
        }

        public static void CloseConnection()
        {
            connection.Close();
        }

        public static void BeginUnitOfWork()
        {
            unitOfWork = UnitOfWorkFactory.CreateUnitOfWork(connection, aggregateTracker);
        }

        public static void CommitUnitOfWork()
        {
            unitOfWork.Commit();
        }
    }
}
