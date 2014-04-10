using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;
using UnitOfWorkCaseStudy.Workflows.Api.Services;
using UnitOfWorkCaseStudy.Workflows.Domain.Models;
using UnitOfWorkCaseStudy.Workflows.Domain.Repositories;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Services.PublishWorkflow
{
    public class PublishWorkflowService : IPublishWorkflowService
    {
        private IWorkflowGraphFactory workflowGraphFactory;
        private IWorkflowStatesProjector statesProjector;
        private IRepository<Workflow> workflowRepository;
        private IStateResponsiblePartyProjector responsiblePartyProjector;
        private IStateFieldsProjector stateFieldProjector;
        private IEnumerable<IPublishWorkflowValidator> validators;

        public PublishWorkflowService(IWorkflowGraphFactory workflowGraphFactory,
            IWorkflowStatesProjector statesProjector, IRepository<Workflow> workflowRepository,
            IStateResponsiblePartyProjector responsiblePartyProjector,
            IStateFieldsProjector stateFieldProjector, IEnumerable<IPublishWorkflowValidator> validators)
        {
            this.workflowGraphFactory = workflowGraphFactory;
            this.statesProjector = statesProjector;
            this.workflowRepository = workflowRepository;
            this.responsiblePartyProjector = responsiblePartyProjector;
            this.stateFieldProjector = stateFieldProjector;
            this.validators = validators;
        }

        public void Publish(Guid workflowId)
        {
            var workflowGraph = workflowGraphFactory.CreateGraphFor(workflowId);
                        
            //CheckBeginState(workflowId, workflowGraph);
            CheckEndState(workflowId, workflowGraph);
            CheckResponsiblePartyFields(workflowId, workflowGraph);

            var workflow = workflowRepository.Get(workflowId);
            workflow.Publish();
        }

        //private void CheckBeginState(Guid workflowId, WorkflowGraph workflowGraph)
        //{
        //    var beginStateIds = workflowGraph.FindAllBeginStateIds();
        //    var numberOfBeginStates = beginStateIds.Count();

        //    if (numberOfBeginStates == 0)
        //        throw new WorkflowExceptions.MissingBeginStateException();
        //    else if (numberOfBeginStates > 1)
        //        throw new WorkflowExceptions.MissingBeginStateException();

        //    var beginState = statesProjector.GetBeginState(workflowId);

        //    if (beginStateIds.First() != beginState.StateId)
        //        throw new WorkflowExceptions.IncorrectBeginStateException();
        //}

        private void CheckEndState(Guid workflowId, WorkflowGraph workflowGraph)
        {
            var endStateIds = workflowGraph.FindAllEndStateIds();
            var numberOfEndStates = endStateIds.Count();

            if (numberOfEndStates == 0)
                throw new WorkflowExceptions.MissingEndStateException();
            else if (numberOfEndStates > 1)
                throw new WorkflowExceptions.MultipleEndStatesException();

            var endState = statesProjector.GetEndState(workflowId);

            if (endStateIds.First() != endState.StateId)
                throw new WorkflowExceptions.IncorrectEndStateException();
        }

        private void CheckResponsiblePartyFields(Guid workflowId, WorkflowGraph workflowGraph)
        {
            var paths = workflowGraph.FindAllPaths();
            var currentRequiredFields = new HashSet<Guid>();

            foreach (var path in paths)
            {
                foreach (var stateId in path)
                {
                    var responsiblePartyFieldIds = responsiblePartyProjector.FindAllFieldsOnState(stateId);

                    foreach (var id in responsiblePartyFieldIds)
                        if (currentRequiredFields.Contains(id) == false)
                            throw new WorkflowExceptions.FieldNeedsToBeRequiredException();

                    var fields = stateFieldProjector.FindAllOnState(stateId);
                    var requiredFieldIds = fields.Where(f => f.Editable && f.Required).Select(f => f.FieldId);

                    foreach (var id in requiredFieldIds)
                        currentRequiredFields.Add(id);

                    var optionalFieldIds = fields.Where(f => f.Editable && f.Required == false).Select(f => f.FieldId);

                    foreach (var id in optionalFieldIds)
                        currentRequiredFields.Remove(id);
                }
            }
        }
    }
}
