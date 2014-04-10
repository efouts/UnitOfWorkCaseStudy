using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkCaseStudy.Workflows.Domain.Creation;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;

namespace UnitOfWorkCaseStudy.Tests.Unit.Workflows
{
    [TestClass]
    public class PutOnlineBeginAndEndStateTests
    {
        private Workflow workflow;

        [TestInitialize]
        public void TestInitialize()
        {
            var workflowFactory = new WorkflowFactory();
            workflow = workflowFactory.Create(Guid.NewGuid(), "Title");
        }

        [TestMethod, ExpectedException(typeof(WorkflowExceptions.MissingBeginStateException))]
        public void PutOnlineWithoutBeginStateThrows()
        {            
            var stateId = Guid.NewGuid();
            workflow.AddState(stateId, "StateOne");
            workflow.SetEndState(stateId);
            workflow.PutOnline();
        }

        [TestMethod, ExpectedException(typeof(WorkflowExceptions.MissingEndStateException))]
        public void PutOnlineWithoutEndStateThrows()
        {
            var stateId = Guid.NewGuid();
            workflow.AddState(stateId, "StateOne");
            workflow.SetBeginState(stateId);
            workflow.PutOnline();
        }
    }
}
