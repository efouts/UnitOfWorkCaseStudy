using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitOfWorkCaseStudy.Workflows.Domain.Creation;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Tests.Unit.Workflows
{
    [TestClass]
    public class AddFieldsToWorkflow
    {
        [TestMethod]
        public void AddFieldTwice()
        {
            var factory = new WorkflowFactory();
            var workflow = factory.Create(Guid.NewGuid(), "Title");

            var fieldId = Guid.NewGuid();
            workflow.AddField(fieldId);
            workflow.AddField(fieldId);

            Assert.AreEqual(1, workflow.Events.Count(e => e is FieldAddedEvent));
        }
    }
    
}
