using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;
using UnitOfWorkCaseStudy.Workflows.Domain.Creation;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Tests.Unit.Workflows
{
    [TestClass]
    public class StateFieldTests
    {
        private Workflow workflow;
        private Guid stateId;
        private Guid fieldId;

        [TestInitialize]
        public void Setup()
        {
            var workflowFactory = new WorkflowFactory();
            workflow = workflowFactory.Create(Guid.NewGuid(), "Title");

            stateId = Guid.NewGuid();
            fieldId = Guid.NewGuid();

            workflow.AddState(stateId, "State");
            workflow.AddFieldTo(stateId, fieldId);
        }

        [TestMethod]
        public void AddEditableFieldToState()
        {
            workflow.MakeFieldEditableOn(stateId, fieldId);
            Assert.IsTrue(workflow.Events.Any(e => e is FieldMadeEditableEvent));
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CannotAlterFieldNotOnState()
        {
            workflow.MakeFieldEditableOn(stateId, Guid.NewGuid());
        }

        [TestMethod]
        public void AddRequiredFieldToState()
        {
            workflow.MakeFieldEditableOn(stateId, fieldId);
            workflow.MakeFieldRequiredOn(stateId, fieldId);
            Assert.IsTrue(workflow.Events.Any(e => e is FieldMadeRequiredEvent));
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CannotMakeFieldNotOnStateRequired()
        {
            workflow.MakeFieldRequiredOn(stateId, Guid.NewGuid());
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CannotMakeUneditableFieldRequired()
        {
            var newFieldId = Guid.NewGuid();
            workflow.AddFieldTo(stateId, newFieldId);
            workflow.MakeFieldRequiredOn(stateId, newFieldId);
        }
    }
}