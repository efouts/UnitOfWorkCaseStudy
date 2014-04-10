using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Models
{
    public class WorkflowPath
    {
        private List<Transition> transitions;
        public Guid Tail { get { return transitions.Last().ToStateId; } }

        public static WorkflowPath CreateFrom(WorkflowPath path)
        {
            return new WorkflowPath(path.transitions);
        }

        public WorkflowPath(IEnumerable<Transition> transitions)
        {
            this.transitions = transitions.ToList();
        }

        public void AddTransition(Transition transition)
        {
            transitions.Add(transition);
        }

        public Boolean ContainsRepeatedCycle()
        {
            var path = GetStates();

            for (var i = path.Count - 1; i > (path.Count - 1) / 2; i--)
            {
                var tailVertices = path.GetRange(i, path.Count - i);
                var tailVerticesOfRemainder = path.GetRange(i - tailVertices.Count, tailVertices.Count);

                if (Enumerable.SequenceEqual(tailVerticesOfRemainder, tailVertices))
                    return true;
            }

            return false;
        }

        public List<Guid> GetStates()
        {
            var states = new List<Guid>();
            states.Add(transitions.First().FromStateId);
            states.AddRange(transitions.Select(e => e.ToStateId));
            return states;            
        }
    }
}
