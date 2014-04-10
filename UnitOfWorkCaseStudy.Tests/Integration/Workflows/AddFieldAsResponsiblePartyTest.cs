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
    public class AddFieldAsResponsiblePartyTest : IntegrationTest
    {
        public IWorkflowFactory WorkflowFactory { get; set; }
        public IRepository<Workflow> WorkflowRepository { get; set; }
        public IStateService StateService { get; set; }
        public IWorkflowStatesProjector WorkflowStateProjector { get; set; }
        public IWorkflowFieldsProjector WorkflowFieldProjector { get; set; }
        public IStateResponsiblePartyProjector StateResponsiblePartyProjector { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }        

        private Guid workflowId;

        [TestInitialize]
        public void TestInitialize()
        {
            workflowId = Guid.NewGuid();

            var workflow = WorkflowFactory.Create(workflowId, "Workflow Title");
            workflow.AddState(Guid.NewGuid(), "FirstState");
            workflow.AddField(Guid.NewGuid());

            WorkflowRepository.Add(workflow);
            UnitOfWork.Commit();
        }

        [TestMethod]
        public void AddFieldAsResponsibleParty()
        {
            var states = WorkflowStateProjector.FindAllOnWorkflow(workflowId);
            var firstState = states.First(s => s.Title.Equals("FirstState"));

            var fields = WorkflowFieldProjector.FindAllOnWorkflow(workflowId);
            var firstField = fields.First();

            StateService.AddFieldAsResponsibleParty(workflowId, firstState.StateId, firstField.FieldId);
            UnitOfWork.Commit();

            var partyIds = StateResponsiblePartyProjector.FindAllFieldsOnState(firstState.StateId);
            Assert.AreEqual(1, partyIds.Count());
            Assert.AreEqual(firstField.FieldId, partyIds.First());
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void OnlyAddFieldsOnWorkflowAsResponsibleParty()
        {
            var states = WorkflowStateProjector.FindAllOnWorkflow(workflowId);
            var firstState = states.First(s => s.Title.Equals("FirstState"));

            var fieldIdNotOnWorkflow = Guid.NewGuid();

            StateService.AddFieldAsResponsibleParty(workflowId, firstState.StateId, fieldIdNotOnWorkflow);
            UnitOfWork.Commit();
        }
    }
}