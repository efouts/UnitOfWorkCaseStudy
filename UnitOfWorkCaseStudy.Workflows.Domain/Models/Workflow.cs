using System;
using System.Linq;
using System.Collections.Generic;
using UnitOfWorkCaseStudy.Workflows.Infrastructure.Events;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Models
{
    public class Workflow : AggregateRoot
    {
        internal HashSet<Guid> fields;
        internal Dictionary<Guid, State> states;
        internal IList<Transition> transitions;
        private Guid beginStateId;
        private Guid endStateId;
        
        internal Workflow() 
        {
            fields = new HashSet<Guid>();
            states = new Dictionary<Guid, State>();
            transitions = new List<Transition>();
        }

        internal void SetId(Guid id)
        {
            Id = id;
        }

        public void ChangeTitle(String title)
        {
            LogEvent(new TitleChangedEvent(Id, title));
        }

        public void AddState(Guid stateId, String stateTitle)
        {
            states.Add(stateId, new State());
            LogEvent(new StateAddedEvent(Id, stateId, stateTitle));
        }

        public void AddFieldTo(Guid stateId, Guid fieldId)
        {
            states[stateId].Fields.Add(fieldId);
            LogEvent(new StateFieldAddedEvent(Id, stateId, fieldId));
        }

        public void AddUserAsResponsiblePartyTo(Guid stateId, Guid userId)
        {
            LogEvent(new UserAddedAsResponsiblePartyEvent(Id, stateId, userId));
        }

        public void AddFieldAsResponsiblePartyTo(Guid stateId, Guid fieldId)
        {
            if (!fields.Contains(fieldId))
                throw new InvalidOperationException();

            states[stateId].ResponsiblePartyFields.Add(fieldId);
            LogEvent(new FieldAddedAsResponsiblePartyEvent(Id, stateId, fieldId));
        }

        public void AddTransition(Guid fromStateId, Guid toStateId, String name)
        {
            if (!states.ContainsKey(fromStateId))
                throw new WorkflowExceptions.MissingFromStateException();

            if (!states.ContainsKey(toStateId))
                throw new WorkflowExceptions.MissingToStateException();

            var transitionId = Guid.NewGuid();
            var transition = new Transition() { TransitionId = transitionId, FromStateId = fromStateId, ToStateId = toStateId, Name = name };
            LogEvent(new TransitionCreatedEvent(Id, transitionId, fromStateId, toStateId, name));

            transitions.Add(transition);
        }

        public void AddField(Guid fieldId)
        {
            if (fields.Contains(fieldId))
                return;

            fields.Add(fieldId);            
            LogEvent(new FieldAddedEvent(Id, fieldId));
        }

        public void MakeFieldEditableOn(Guid stateId, Guid fieldId)
        {
            if (states[stateId].Fields.Contains(fieldId) == false)
                throw new InvalidOperationException();

            states[stateId].EditableFields.Add(fieldId);
            LogEvent(new FieldMadeEditableEvent(fieldId, Id));
        }

        public void MakeFieldRequiredOn(Guid stateId, Guid fieldId)
        {
            var state = states[stateId];

            if (state.Fields.Contains(fieldId) == false || state.EditableFields.Contains(fieldId) == false)
                throw new InvalidOperationException();

            states[stateId].RequiredFields.Add(fieldId);
            LogEvent(new FieldMadeRequiredEvent(fieldId, Id));
        }

        public void SetBeginState(Guid stateId)
        {
            beginStateId = stateId;
            LogEvent(new BeginStateSetEvent(Id, stateId));
        }

        public void SetEndState(Guid stateId)
        {
            endStateId = stateId;
            LogEvent(new EndStateSetEvent(Id, stateId));            
        }

        public void Publish()
        {
            LogEvent(new WorkflowPutOnlineEvent(this.Id));
        }

        public void PutOnline()
        {
            //if (beginStateId == Guid.Empty)
            //    throw new WorkflowExceptions.MissingBeginStateException();

            //if (endStateId == Guid.Empty)
            //    throw new WorkflowExceptions.MissingEndStateException();

            //var graph = new WorkflowGraph(states.Keys, transitions);

            //if (graph.HasOneBeginState() == false)
            //    throw new WorkflowExceptions.MultipleBeginStatesException();

            //if (graph.IsBeginState(beginStateId) == false)
            //    throw new WorkflowExceptions.IncorrectBeginStateException();

            //if (graph.HasOneEndState() == false)
            //    throw new WorkflowExceptions.MultipleEndStatesException();

            //if (graph.IsEndState(endStateId) == false)
            //    throw new WorkflowExceptions.IncorrectEndStateException();

            //if (graph.HasOrphanedStates())
            //    throw new WorkflowExceptions.OrphanedStateException();

            //var workflowPaths = graph.FindAllPaths();
            //var requiredFields = new HashSet<Guid>();

            //foreach (var path in workflowPaths)
            //{
            //    foreach (var stateId in path.GetStates())
            //    {
            //        var state = states[stateId];

            //        foreach (var field in state.ResponsiblePartyFields)
            //            if (requiredFields.Contains(field) == false)
            //                throw new WorkflowExceptions.FieldNeedsToBeRequiredException();

            //        var requiredFieldsOnState = state.RequiredFields;
            //        foreach (var field in requiredFieldsOnState)
            //            requiredFields.Add(field);

            //        var optionalFields = state.EditableFields.Except(requiredFieldsOnState);
            //        foreach (var field in optionalFields)
            //            requiredFields.Remove(field);
            //    }
            //}

            LogEvent(new WorkflowPutOnlineEvent(this.Id));
        }
    }    
}