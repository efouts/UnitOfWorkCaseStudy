using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Sql.Transactions
{
    public class EventHandlerMapper : IEventHandlerMapper
    {
        private Dictionary<Type, IEnumerable<Object>> mappings;

        public EventHandlerMapper()
        {
            mappings = new Dictionary<Type, IEnumerable<Object>>();
        }

        public void RegisterHandlersFor<T>(params IEventHandler<T>[] handlers) where T : IEvent
        {
            mappings.Add(typeof(T), handlers);
        }

        public IEnumerable<IEventHandler<T>> GetHandlersFor<T>(T @event) where T : IEvent
        {
            if (mappings.ContainsKey(typeof(T)))
                return mappings[typeof(T)].Cast<IEventHandler<T>>();

            return Enumerable.Empty<IEventHandler<T>>();
        }
    }
}
