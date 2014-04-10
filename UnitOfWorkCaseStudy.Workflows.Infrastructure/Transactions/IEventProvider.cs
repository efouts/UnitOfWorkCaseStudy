using System.Collections.Generic;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Transactions
{
    public interface IEventProvider
    {
        void Clear();
        IEnumerable<IEvent> GetEvents();
    }
}
