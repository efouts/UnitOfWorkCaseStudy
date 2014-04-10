using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitOfWorkCaseStudy.Workflows.Tests.Integration.Main;

namespace UnitOfWorkCaseStudy.Tests.Integration
{
    public abstract class IntegrationTest
    {
        [TestInitialize]
        public void SetupIntegrationTest()
        {
            IntegrationSuite.BeginUnitOfWork();
            IntegrationSuite.IocContainer.BuildUp(this);
            IntegrationSuite.OpenConnection();
        }

        [TestCleanup]
        public void TearDownIntegrationTest()
        {
            IntegrationSuite.CloseConnection();
        }     
    }
}
