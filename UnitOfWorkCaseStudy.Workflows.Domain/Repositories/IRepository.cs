using System;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Repositories
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        void Add(T aggregate);
        T Get(Guid id);
    }
}
