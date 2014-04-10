using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkCaseStudy.Workflows.Api.Projections;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;
using UnitOfWorkCaseStudy.Workflows.Domain.Creation;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;
using UnitOfWorkCaseStudy.Workflows.Domain.Repositories;
using UnitOfWorkCaseStudy.Workflows.Domain.Services.PublishWorkflow;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Projectors;

namespace UnitOfWorkCaseStudy.Tests.Unit.Workflows
{
    [TestClass]
    public class PutOnlinePathValidationTests
    {
        private Workflow workflow;
        private Guid stateOneId;
        private Guid stateTwoId;

        [TestInitialize]
        public void TestInitialize()
        {
            var workflowFactory = new WorkflowFactory();
            workflow = workflowFactory.Create(Guid.NewGuid(), "Title");

            stateOneId = Guid.NewGuid();
            workflow.AddState(stateOneId, "1");
            workflow.SetBeginState(stateOneId);

            stateTwoId = Guid.NewGuid();
            workflow.AddState(stateTwoId, "2");
            workflow.SetEndState(stateTwoId);
        }

        [TestMethod, ExpectedException(typeof(WorkflowExceptions.MultipleBeginStatesException))]
        public void TwoStatesWithoutTransition()
        {
            workflow.PutOnline();
        }

        [TestMethod, ExpectedException(typeof(WorkflowExceptions.MultipleEndStatesException))]
        public void TwoEndStates()
        {
            var stateThreeId = Guid.NewGuid();
            workflow.AddState(stateThreeId, "3");

            workflow.AddTransition(stateOneId, stateTwoId, "1-2");
            workflow.AddTransition(stateOneId, stateThreeId, "1-3");

            workflow.PutOnline();
        }

        [TestMethod, ExpectedException(typeof(WorkflowExceptions.IncorrectEndStateException))]
        public void EndStateNotSetToEndState()
        {
            var stateThreeId = Guid.NewGuid();
            workflow.AddState(stateThreeId, "3");

            workflow.AddTransition(stateOneId, stateTwoId, "1-2");
            workflow.AddTransition(stateOneId, stateThreeId, "1-3");
            workflow.AddTransition(stateTwoId, stateThreeId, "2-3");

            workflow.PutOnline();
        }

        [TestMethod]
        public void ThreeStatesSucceed()
        {
            var stateThreeId = Guid.NewGuid();
            workflow.AddState(stateThreeId, "3");

            workflow.AddTransition(stateOneId, stateTwoId, "1-2");
            workflow.AddTransition(stateOneId, stateThreeId, "1-3");
            workflow.AddTransition(stateThreeId, stateTwoId, "3-2");

            workflow.PutOnline();

            Assert.IsTrue(workflow.Events.Any(e => e is WorkflowPutOnlineEvent));
        }

        [TestMethod]
        public void TwoStateLoopDoesntBreakTheUniverse()
        {
            var stateThreeId = Guid.NewGuid();
            var stateFourId = Guid.NewGuid();

            workflow.AddState(stateThreeId, "3");
            workflow.AddState(stateFourId, "4");

            workflow.AddTransition(stateOneId, stateFourId, "1-4");
            workflow.AddTransition(stateFourId, stateThreeId, "4-3");
            workflow.AddTransition(stateThreeId, stateFourId, "3-4");
            workflow.AddTransition(stateFourId, stateTwoId, "4-2");

            workflow.PutOnline();

            Assert.IsTrue(workflow.Events.Any(e => e is WorkflowPutOnlineEvent));
        }

        [TestMethod]
        public void TwoBeginStates()
        {
            var graph = new WorkflowGraph();
            var first = Guid.NewGuid();
            var second = Guid.NewGuid();
            var third = Guid.NewGuid();

            graph.AddState(first);
            graph.AddState(second);
            graph.AddState(third);
            graph.AddTransition(first, third);
            graph.AddTransition(second, third);
            
            var validator = new MultipleBeginStatesValidator();

            var workflowId = Guid.NewGuid();
            var errors = validator.Validate(workflowId, graph);

            Assert.AreEqual(2, errors.Count());

            var firstError = errors.ElementAt(0);
            Assert.AreEqual("MultipleBeginStates", firstError.Key);
            Assert.AreEqual(Convert.ToString(first), firstError.Message);

            var secondError = errors.ElementAt(1);
            Assert.AreEqual("MultipleBeginStates", secondError.Key);
            Assert.AreEqual(Convert.ToString(second), secondError.Message); 
        }

        [TestMethod]
        public void BeginStateNotSetToBeginState()
        {
            var graph = new WorkflowGraph();
            var first = Guid.NewGuid();
            var second = Guid.NewGuid();

            graph.AddState(first);
            graph.AddState(second);
            graph.AddTransition(first, second);

            var secondState = new StateListProjection();
            secondState.StateId = second;

            var mockProjector = new Mock<IWorkflowStatesProjector>();
            mockProjector.Setup(p => p.GetBeginState(It.IsAny<Guid>())).Returns(secondState);

            var validator = new IncorrectBeginStateValidator(mockProjector.Object);

            var workflowId = Guid.NewGuid();
            var errors = validator.Validate(workflowId, graph);

            Assert.AreEqual(1, errors.Count());

            var firstError = errors.First();
            Assert.AreEqual("WrongBeginState", firstError.Key);
            Assert.AreEqual(Convert.ToString(first), firstError.Message); 
        }

        [TestMethod]
        public void OrphanedState()
        {
            var graph = new WorkflowGraph();
            var first = Guid.NewGuid();
            var second = Guid.NewGuid();
            var orphan = Guid.NewGuid();
            graph.AddState(first);
            graph.AddState(second);
            graph.AddState(orphan);
            graph.AddTransition(first, second);

            var workflowId = Guid.NewGuid();
            var validator = new OrphanedStatesValidator();
            var errors = validator.Validate(workflowId, graph);

            Assert.AreEqual(1, errors.Count());

            var firstError = errors.First();
            Assert.AreEqual("OrphanedState", firstError.Key);
            Assert.AreEqual(Convert.ToString(orphan), firstError.Message); 
        }
    }
}