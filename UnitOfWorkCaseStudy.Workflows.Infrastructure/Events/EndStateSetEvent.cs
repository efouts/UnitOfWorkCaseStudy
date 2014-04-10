using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Events
{
    public class EndStateSetEvent : IEvent
    {
        public Guid WorkflowId { get; private set; }
        public Guid EndStateId { get; private set; }

        public EndStateSetEvent(Guid workflowId, Guid endStateId)
        {
            WorkflowId = workflowId;
            EndStateId = endStateId;
        }
    }
}