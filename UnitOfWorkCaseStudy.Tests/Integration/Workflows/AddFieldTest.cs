using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;
using UnitOfWorkCaseStudy.Workflows.Api.Services;
using UnitOfWorkCaseStudy.Workflows.Domain.Creation;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;
using UnitOfWorkCaseStudy.Workflows.Domain.Repositories;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Transactions;

namespace UnitOfWorkCaseStudy.Tests.Integration.Workflows
{
    [TestClass]
    public class AddFieldTest : IntegrationTest
    {
        public IRepository<Workflow> WorkflowRepository { get; set; }
        public IWorkflowFactory WorkflowFactory { get; set; }
        public IWorkflowService WorkflowService { get; set; }
        public IWorkflowFieldsProjector WorkflowFieldProjector { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }        

        private Guid workflowId;

        [TestInitialize]
        public void TestInitialize()
        {
            workflowId = Guid.NewGuid();
            var workflow = WorkflowFactory.Create(workflowId, "Workflow Title");
            WorkflowRepository.Add(workflow);
            UnitOfWork.Commit();
        }

        [TestMethod]
        public void AddField()
        {
            var addedFieldId = Guid.NewGuid();
            WorkflowService.AddField(workflowId, addedFieldId);
            UnitOfWork.Commit();

            var fields = WorkflowFieldProjector.FindAllOnWorkflow(workflowId);
            Assert.AreEqual(1, fields.Count());
            Assert.AreEqual(addedFieldId, fields.First().FieldId);
        }
    }
}
