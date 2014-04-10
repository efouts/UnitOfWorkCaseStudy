using System.Collections.Generic;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Transactions
{
    public interface IEventHandlerMapper
    {
        IEnumerable<IEventHandler<T>> GetHandlersFor<T>(T @event) where T : IEvent;
        void RegisterHandlersFor<T>(params IEventHandler<T>[] handlers) where T : IEvent;
    }
}
