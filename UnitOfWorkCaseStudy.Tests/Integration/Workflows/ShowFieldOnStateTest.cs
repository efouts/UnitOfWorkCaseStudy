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
    public class ShowFieldOnStateTest : IntegrationTest
    {
        public IWorkflowFactory WorkflowFactory { get; set; }
        public IRepository<Workflow> WorkflowRepository { get; set; }
        public IStateService StateService { get; set; }
        public IWorkflowStatesProjector WorkflowStateProjector { get; set; }
        public IWorkflowFieldsProjector WorkflowFieldProjector { get; set; }
        public IStateFieldsProjector StateFieldsProjector { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }        

        private Guid workflowId;

        [TestInitialize]
        public void TestInitialize()
        {
            workflowId = Guid.NewGuid();
            var fieldId = Guid.NewGuid();

            var workflow = WorkflowFactory.Create(workflowId, "Workflow Title");
            workflow.AddState(Guid.NewGuid(), "FirstState");
            workflow.AddField(fieldId);

            WorkflowRepository.Add(workflow);
            UnitOfWork.Commit();
        }

        [TestMethod]
        public void ShowOnState()
        {
            var states = WorkflowStateProjector.FindAllOnWorkflow(workflowId);
            var firstState = states.First(s => s.Title.Equals("FirstState"));

            var fields = WorkflowFieldProjector.FindAllOnWorkflow(workflowId);
            var firstField = fields.First();

            StateService.ShowFieldOnState(workflowId, firstState.StateId, firstField.FieldId);
            UnitOfWork.Commit();

            var stateFields = StateFieldsProjector.FindAllOnState(firstState.StateId);
            Assert.AreEqual(1, stateFields.Count());
            Assert.AreEqual(firstField.FieldId, stateFields.First().FieldId);
        }
    }
}
