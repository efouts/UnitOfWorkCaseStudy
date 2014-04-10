using System;
using System.Linq;
using System.Collections.Generic;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Models
{
    public class State
    {
        public HashSet<Guid> Fields { get; set; }
        public HashSet<Guid> EditableFields { get; set; }
        public HashSet<Guid> RequiredFields { get; set; }
        public HashSet<Guid> ResponsiblePartyFields { get; set; }

        public State()
        {
            Fields = new HashSet<Guid>();
            EditableFields = new HashSet<Guid>();
            RequiredFields = new HashSet<Guid>();
            ResponsiblePartyFields = new HashSet<Guid>();
        }
    }
}