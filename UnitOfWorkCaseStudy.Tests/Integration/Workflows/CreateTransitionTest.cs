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
    public class CreateTransitionTest : IntegrationTest
    {
        public IWorkflowFactory WorkflowFactory { get; set; }
        public IRepository<Workflow> WorkflowRepository { get; set; }
        public IWorkflowService WorkflowService { get; set; }
        public IWorkflowStatesProjector WorkflowStateProjector { get; set; }
        public IWorkflowTransitionsProjector WorkflowTransitionProjector { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }        

        private Guid workflowId;

        [TestInitialize]
        public void TestInitialize()
        {
            workflowId = Guid.NewGuid();
                        
            var workflow = WorkflowFactory.Create(workflowId, "Workflow Title");
            workflow.AddState(Guid.NewGuid(), "FirstState");
            workflow.AddState(Guid.NewGuid(), "SecondState");

            WorkflowRepository.Add(workflow);
            UnitOfWork.Commit();
        }

        [TestMethod]
        public void CreateTransition()
        {
            var states = WorkflowStateProjector.FindAllOnWorkflow(workflowId);
            var firstState = states.First(s => s.Title.Equals("FirstState"));
            var secondState = states.First(s => s.Title.Equals("SecondState"));

            WorkflowService.AddTransition(workflowId, firstState.StateId, secondState.StateId, "FirstToSecond");
            UnitOfWork.Commit();

            var workflowTransitions = WorkflowTransitionProjector.FindAllOnWorkflow(workflowId);
            Assert.AreEqual(1, workflowTransitions.Count());

            var transition = workflowTransitions.First();
            Assert.AreEqual(workflowId, transition.WorkflowId);
            Assert.AreEqual(firstState.StateId, transition.FromStateId);
            Assert.AreEqual(secondState.StateId, transition.ToStateId);
            Assert.AreEqual("FirstToSecond", transition.Name);
        }
    }
}
