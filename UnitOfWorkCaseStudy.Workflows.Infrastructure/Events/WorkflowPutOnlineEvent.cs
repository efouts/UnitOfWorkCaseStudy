using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Events
{
    public class WorkflowPutOnlineEvent : IEvent
    {
        public Guid WorkflowId { get; private set; }

        public WorkflowPutOnlineEvent(Guid workflowId)
        {
            WorkflowId = workflowId;
        }
    }
}