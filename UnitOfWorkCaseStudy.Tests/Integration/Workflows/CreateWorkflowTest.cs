using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;
using UnitOfWorkCaseStudy.Workflows.Api.Services;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Transactions;

namespace UnitOfWorkCaseStudy.Tests.Integration.Workflows
{
    [TestClass]
    public class CreateWorkflowTest : IntegrationTest
    {
        public IWorkflowService WorkflowService { get; set; }
        public IWorkflowListProjector WorkflowTitleProjector { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }        

        [TestMethod]
        public void CreateWorkflow()
        {
            var id = Guid.NewGuid();
            var title = "Workflow Title";            
            
            WorkflowService.Create(id, title);
            UnitOfWork.Commit();

            var titleProjection = WorkflowTitleProjector.Get(id);
            Assert.AreEqual(titleProjection.Title, title);
        }
    }
}
