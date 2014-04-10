using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Repositories;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Creation
{
    public class WorkflowFactory : IWorkflowFactory
    {
        public Workflow Create(Guid id, String title)
        {
            var workflow = new Workflow();
            workflow.SetId(id);
            workflow.LogEvent(new WorkflowCreatedEvent(workflow.Id, title));
            return workflow;
        }

        public Workflow Reconstitute(Metadata metadata)
        {
            var workflow = new Workflow();
            workflow.SetId(metadata.WorkflowMetadata.Id);
            workflow.fields = new HashSet<Guid>(metadata.FieldsMetadata.Select(f => f.FieldId));

            foreach (var meta in metadata.StatesMetadata)
                workflow.states.Add(meta.Id, new State());

            return workflow;
        }
    }
}