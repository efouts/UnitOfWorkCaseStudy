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
    public class PutOnline
    {
        [TestMethod]
        public void PutOnlineSuccess()
        {
            var workflowFactory = new WorkflowFactory();
            var workflow = workflowFactory.Create(Guid.NewGuid(), "Title");

            var beginStateId = Guid.NewGuid();
            workflow.AddState(beginStateId, "Begin State");
            workflow.SetBeginState(beginStateId);

            var endStateId = Guid.NewGuid();
            workflow.AddState(endStateId, "End State");
            workflow.SetEndState(endStateId);

            workflow.AddTransition(beginStateId, endStateId, "Transition");

            workflow.PutOnline();

            Assert.IsTrue(workflow.Events.Any(e => e is WorkflowPutOnlineEvent));
        }

        [TestMethod, ExpectedException(typeof(WorkflowExceptions.FieldNeedsToBeRequiredException))]
        public void FieldNeedsToBeRequired()
        {
            var workflowFactory = new WorkflowFactory();
            var workflow = workflowFactory.Create(Guid.NewGuid(), "Title");

            var fieldId = Guid.NewGuid();
            workflow.AddField(fieldId);

            var beginStateId = Guid.NewGuid();
            workflow.AddState(beginStateId, "Begin State");
            workflow.SetBeginState(beginStateId);
            workflow.AddFieldTo(beginStateId, fieldId);
            workflow.MakeFieldEditableOn(beginStateId, fieldId);

            var endStateId = Guid.NewGuid();
            workflow.AddState(endStateId, "End State");
            workflow.AddFieldAsResponsiblePartyTo(endStateId, fieldId);
            workflow.SetEndState(endStateId);

            workflow.AddTransition(beginStateId, endStateId, "Transition");

            workflow.PutOnline();
        }

        [TestMethod]
        public void OptionalFieldsAreOK()
        {
            var workflowFactory = new WorkflowFactory();
            var workflow = workflowFactory.Create(Guid.NewGuid(), "Title");

            var fieldId = Guid.NewGuid();
            workflow.AddField(fieldId);

            var beginStateId = Guid.NewGuid();
            workflow.AddState(beginStateId, "Begin State");
            workflow.SetBeginState(beginStateId);
            workflow.AddFieldTo(beginStateId, fieldId);
            workflow.MakeFieldEditableOn(beginStateId, fieldId);

            var endStateId = Guid.NewGuid();
            workflow.AddState(endStateId, "End State");
            workflow.SetEndState(endStateId);

            workflow.AddTransition(beginStateId, endStateId, "Transition");

            workflow.PutOnline();
        }

        [TestMethod, ExpectedException(typeof(WorkflowExceptions.FieldNeedsToBeRequiredException))]
        public void FieldNeedsToBeEditableAndRequired()
        {
            var workflowFactory = new WorkflowFactory();
            var workflow = workflowFactory.Create(Guid.NewGuid(), "Title");

            var fieldId = Guid.NewGuid();
            workflow.AddField(fieldId);

            var beginStateId = Guid.NewGuid();
            workflow.AddState(beginStateId, "Begin State");
            workflow.SetBeginState(beginStateId);
            workflow.AddFieldTo(beginStateId, fieldId);

            var endStateId = Guid.NewGuid();
            workflow.AddState(endStateId, "End State");
            workflow.AddFieldAsResponsiblePartyTo(endStateId, fieldId);
            workflow.SetEndState(endStateId);

            workflow.AddTransition(beginStateId, endStateId, "Transition");

            workflow.PutOnline();
        }
    }
}