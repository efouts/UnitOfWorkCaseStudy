using UnitOfWorkCaseStudy.Workflows.Domain.Models;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Transactions
{
    public interface IAggregateTracker
    {
        void Track(IAggregateRoot aggregate);
    }
}
