using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;
using UnitOfWorkCaseStudy.Workflows.Api.Services;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Transactions;

namespace UnitOfWorkCaseStudy.Tests.Integration.Workflows
{
    [TestClass]
    public class RenameWorkflowTest : IntegrationTest
    {
        public IWorkflowService WorkflowService { get; set; }
        public IWorkflowListProjector WorkflowTitleProjector { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }        

        private Guid existingWorkflowId;

        [TestInitialize]
        public void TestInitialize()
        {
            existingWorkflowId = Guid.NewGuid();
            WorkflowService.Create(existingWorkflowId, "Name");
            UnitOfWork.Commit();
        }

        [TestMethod]
        public void RenameWorkflow()
        {
            var newTitle = "NewName";
            WorkflowService.Rename(existingWorkflowId, newTitle);
            UnitOfWork.Commit();

            var titleProjection = WorkflowTitleProjector.Get(existingWorkflowId);
            Assert.AreEqual(newTitle, titleProjection.Title);
        }
    }
}
