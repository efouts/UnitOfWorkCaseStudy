using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkCaseStudy.Workflows.Api.Projections
{
    public class StateProjection
    {
        public Guid StateId { get; set; }
        public IEnumerable<Guid> OptionalFields { get; set; }
        public IEnumerable<Guid> RequiredFields { get; set; }
        public IEnumerable<Guid> ResponsiblePartyFields { get; set; }
    }
}
