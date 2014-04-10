using System.Data.SqlClient;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Events
{
    public interface IEventHandler<T> where T : IEvent
    {
        SqlTransaction Transaction { get; set; }
        void Handle(T @event);
    }
}
