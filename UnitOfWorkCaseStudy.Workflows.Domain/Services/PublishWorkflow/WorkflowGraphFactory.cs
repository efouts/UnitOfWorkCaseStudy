using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Services.PublishWorkflow
{
    public interface IWorkflowGraphFactory
    {
        WorkflowGraph CreateGraphFor(Guid workflowId);
    }

    public class WorkflowGraphFactory : IWorkflowGraphFactory
    {
        private IWorkflowStatesProjector statesProjector;
        private IWorkflowTransitionsProjector transitionsProjector;

        public WorkflowGraphFactory(IWorkflowStatesProjector statesProjector, IWorkflowTransitionsProjector transitionsProjector,
            IStateFieldsProjector stateFieldsProjector, IStateResponsiblePartyProjector stateResponsiblePartyProjector)
        {
            this.statesProjector = statesProjector;
            this.transitionsProjector = transitionsProjector;
        }

        public WorkflowGraph CreateGraphFor(Guid workflowId)
        {
            var workflowGraph = new WorkflowGraph();

            var states = statesProjector.FindAllOnWorkflow(workflowId);

            foreach (var state in states)
                workflowGraph.AddState(state.StateId);

            var transitions = transitionsProjector.FindAllOnWorkflow(workflowId);

            foreach (var transition in transitions)
                workflowGraph.AddTransition(transition.FromStateId, transition.ToStateId);

            return workflowGraph;
        }
    }
}