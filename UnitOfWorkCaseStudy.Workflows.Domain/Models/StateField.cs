using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Models
{
    public class StateField : Entity
    {
        public readonly Guid StateId;
        public readonly Guid FieldId;
        public Boolean Editable { get; set; }

        public StateField(Guid stateId, Guid fieldId)
        {
            StateId = stateId;
            FieldId = fieldId;
        }
    }
}