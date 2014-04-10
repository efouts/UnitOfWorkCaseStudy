using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitOfWorkCaseStudy.Workflows.Api.Services;
using UnitOfWorkCaseStudy.Workflows.Domain.Creation;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;
using UnitOfWorkCaseStudy.Workflows.Domain.Repositories;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Transactions;

namespace UnitOfWorkCaseStudy.Tests.Integration.Workflows
{
    [TestClass]
    public class AddFieldTwiceTest : IntegrationTest
    {
        public IWorkflowFactory WorkflowFactory { get; set; }
        public IRepository<Workflow> WorkflowRepository { get; set; }
        public IWorkflowService WorkflowService { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }        

        private Guid workflowId;
        private Guid fieldId;

        [TestInitialize]
        public void TestInitialize()
        {
            workflowId = Guid.NewGuid();
            fieldId = Guid.NewGuid();

            var workflow = WorkflowFactory.Create(workflowId, "Workflow Title");
            workflow.AddField(fieldId);

            WorkflowRepository.Add(workflow);
            UnitOfWork.Commit();
        }
    }
}
