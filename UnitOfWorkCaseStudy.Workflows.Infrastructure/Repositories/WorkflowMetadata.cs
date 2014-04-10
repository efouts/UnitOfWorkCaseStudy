using System;
using System.Collections.Generic;

namespace UnitOfWorkCaseStudy.Workflows.Infrastructure.Repositories
{
    public class Metadata
    {
        public WorkflowMetadata WorkflowMetadata { get; set; }
        public IEnumerable<StateMetadata> StatesMetadata { get; set; }
        public IEnumerable<TransitionMetadata> TransitionsMetadata { get; set; }
        public IEnumerable<WorkflowFieldMetadata> FieldsMetadata { get; set; }
    }

    public class WorkflowMetadata
    {
        public Guid Id { get; set; }
        public Int32 Number { get; set; }
        public String Title { get; set; }
    }

    public class StateMetadata
    {
        public Guid Id { get; set; }
        public Guid WorkflowId { get; set; }
        public String Title { get; set; }
    }

    public class TransitionMetadata
    {
        public Guid Id { get; set; }
        public Guid WorkflowId { get; set; }
        public Guid FromStateId { get; set; }
        public Guid ToStateId { get; set; }
        public String Name { get; set; }
    }

    public class WorkflowFieldMetadata
    {
        public Guid WorkflowId { get; set; }
        public Guid FieldId { get; set; }
    }
}
