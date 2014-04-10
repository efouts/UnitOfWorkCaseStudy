using System;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Models
{
    public class Transition 
    {
        public Guid TransitionId { get; set; }
        public Guid FromStateId { get; set; }
        public Guid ToStateId { get; set; }
        public String Name { get; set; }
    }
}