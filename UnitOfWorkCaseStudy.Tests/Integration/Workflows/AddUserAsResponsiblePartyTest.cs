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
    public class AddUserAsResponsiblePartyTest : IntegrationTest
    {
        public IWorkflowFactory WorkflowFactory { get; set; }
        public IRepository<Workflow> WorkflowRepository { get; set; }
        public IStateService StateService { get; set; }
        public IWorkflowStatesProjector WorkflowStateProjector { get; set; }
        public IStateResponsiblePartyProjector StateResponsiblePartyProjector { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }        

        private Guid workflowId;

        [TestInitialize]
        public void TestInitialize()
        {
            workflowId = Guid.NewGuid();

            var workflow = WorkflowFactory.Create(workflowId, "Workflow Title");
            workflow.AddState(Guid.NewGuid(), "FirstState");

            WorkflowRepository.Add(workflow);
            UnitOfWork.Commit();
        }

        [TestMethod]
        public void AddUserAsResponsibleParty()
        {
            var states = WorkflowStateProjector.FindAllOnWorkflow(workflowId);
            var firstState = states.First(s => s.Title.Equals("FirstState"));
            var userId = Guid.NewGuid();

            StateService.AddUserAsResponsiblePartyToState(workflowId, firstState.StateId, userId);
            UnitOfWork.Commit();

            var stateResponsibleParties = StateResponsiblePartyProjector.FindAllUsersOnState(firstState.StateId);
            Assert.AreEqual(1, stateResponsibleParties.Count());
            Assert.AreEqual(userId, stateResponsibleParties.First());
        }
    }
}
