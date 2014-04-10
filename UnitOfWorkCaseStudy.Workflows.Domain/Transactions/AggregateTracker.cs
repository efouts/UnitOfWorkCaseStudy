using System.Collections.Generic;
using System.Linq;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;
using UnitOfWorkCaseStudy.Workflows.Domain.Transactions;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Transactions;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Transactions
{
    public class AggregateTracker : IEventProvider, IAggregateTracker
    {
        private List<IAggregateRoot> trackedAggregates;

        public AggregateTracker()
        {
            trackedAggregates = new List<IAggregateRoot>();
        }

        public void Track(IAggregateRoot aggregate)
        {
            trackedAggregates.Add(aggregate);
        }

        public IEnumerable<IEvent> GetEvents()
        {
            return trackedAggregates.SelectMany(a => a.Events);
        }

        public void Clear()
        {
            trackedAggregates.Clear();
        }
    }
}
