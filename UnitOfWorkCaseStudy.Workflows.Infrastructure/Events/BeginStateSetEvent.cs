using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Events
{
    public class BeginStateSetEvent : IEvent
    {
        public Guid WorkflowId { get; private set; }
        public Guid BeginStateId { get; private set; }

        public BeginStateSetEvent(Guid workflowId, Guid beginStateId)
        {
            WorkflowId = workflowId;
            BeginStateId = beginStateId;
        }
    }
}