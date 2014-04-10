using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkCaseStudy.Workflows.Domain.Creation;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Tests.Unit.Workflows
{
    [TestClass]
    public class WorkflowTransitions
    {
        [TestMethod, ExpectedException(typeof(WorkflowExceptions.MissingFromStateException))]
        public void AddTransitionWithFromStateNotOnWorkflow()
        {
            var workflowFactory = new WorkflowFactory();
            var workflow = workflowFactory.Create(Guid.NewGuid(), "Title");

            var toStateId = Guid.NewGuid();
            workflow.AddState(toStateId, "To State");

            workflow.AddTransition(Guid.NewGuid(), toStateId, "Transition");
        }

        [TestMethod, ExpectedException(typeof(WorkflowExceptions.MissingToStateException))]
        public void AddTransitionWithToStateNotOnWorkflow()
        {
            var workflowFactory = new WorkflowFactory();
            var workflow = workflowFactory.Create(Guid.NewGuid(), "Title");

            var fromStateId = Guid.NewGuid();
            workflow.AddState(fromStateId, "From State");

            workflow.AddTransition(fromStateId, Guid.NewGuid(), "Transition");
        }

        [TestMethod]
        public void AddTransitionSuccess()
        {
            var workflowFactory = new WorkflowFactory();
            var workflow = workflowFactory.Create(Guid.NewGuid(), "Title");

            var fromStateId = Guid.NewGuid();
            workflow.AddState(fromStateId, "From State");

            var toStateId = Guid.NewGuid();
            workflow.AddState(toStateId, "To State");

            workflow.AddTransition(fromStateId, toStateId, "Transition");

            Assert.IsTrue(workflow.Events.Any(e => e is TransitionCreatedEvent));
        }
    }
}
