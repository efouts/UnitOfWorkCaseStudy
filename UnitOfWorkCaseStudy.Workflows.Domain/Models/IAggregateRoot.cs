using System;
using System.Collections.Generic;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Models
{
    public interface IAggregateRoot
    {
        IEnumerable<IEvent> Events { get; }
        Guid Id { get; }
    }
}
