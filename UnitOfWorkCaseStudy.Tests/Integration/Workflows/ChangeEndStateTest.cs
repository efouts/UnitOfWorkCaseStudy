using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;
using UnitOfWorkCaseStudy.Workflows.Api.Services;
using UnitOfWorkCaseStudy.Workflows.Domain.Creation;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;
using UnitOfWorkCaseStudy.Workflows.Domain.Repositories;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Transactions;

namespace UnitOfWorkCaseStudy.Tests.Integration.Workflows
{
    [TestClass]
    public class ChangeEndStateTest : IntegrationTest
    {
        public IRepository<Workflow> WorkflowRepository { get; set; }
        public IWorkflowFactory WorkflowFactory { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        public IWorkflowService WorkflowService { get; set; }
        public IWorkflowStatesProjector WorkflowStatesProjector { get; set; }

        private Guid workflowId;

        [TestInitialize]
        public void CreateWorkflow()
        {
            workflowId = Guid.NewGuid();
            var workflow = WorkflowFactory.Create(workflowId, "Workflow Title");
            workflow.AddState(Guid.NewGuid(), "StateOne");
            WorkflowRepository.Add(workflow);
            UnitOfWork.Commit();
        }

        [TestMethod]
        public void ChangeEndState()
        {
            var firstState = WorkflowStatesProjector.FindAllOnWorkflow(workflowId).First();

            WorkflowService.ChangeEndState(workflowId, firstState.StateId);
            UnitOfWork.Commit();

            var endState = WorkflowStatesProjector.GetEndState(workflowId);
            Assert.AreEqual(firstState.StateId, endState.StateId);
        }
    }
}