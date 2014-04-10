using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Events
{
    public class FieldMadeRequiredEvent : IEvent
    {
        public Guid FieldId { get; private set; }
        public Guid StateId { get; private set; }

        public FieldMadeRequiredEvent(Guid fieldId, Guid stateId)
        {
            FieldId = fieldId;
            StateId = stateId;
        }
    }
}