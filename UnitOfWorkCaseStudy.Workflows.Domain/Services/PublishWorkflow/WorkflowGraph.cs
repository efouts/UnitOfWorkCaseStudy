using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkCaseStudy.Workflows.Api.Projections;
using UnitOfWorkCaseStudy.Workflows.Api.Projectors;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Services.PublishWorkflow
{
    public interface IWorkflowGraph
    {
        IEnumerable<Guid> FindAllBeginStateIds();
        IEnumerable<Guid> FindAllEndStateIds();
        IEnumerable<IEnumerable<Guid>> FindAllPaths();
        IEnumerable<Guid> GetOrphanedStates();
    }

    public class WorkflowGraph : IWorkflowGraph
    {
        private HashSet<Guid> stateMap;
        private Dictionary<Guid, List<Guid>> adjacentStatesMap;
        private List<GraphTransition> transitions;
        private List<List<Guid>> paths;

        public WorkflowGraph()
        {
            stateMap = new HashSet<Guid>();
            adjacentStatesMap = new Dictionary<Guid, List<Guid>>();
            transitions = new List<GraphTransition>();
        }

        public void AddState(Guid stateId)
        {
            stateMap.Add(stateId);
        }

        public void AddTransition(Guid fromStateId, Guid toStateId) 
        {
            var transition = new GraphTransition() { FromStateId = fromStateId, ToStateId = toStateId };
            transitions.Add(transition);
        }

        public IEnumerable<Guid> FindAllBeginStateIds()
        {
            var fromStateIds = transitions.Select(t => t.FromStateId);
            var toStateIds = transitions.Select(t => t.ToStateId);
            return fromStateIds.Except(toStateIds);
        }

        public IEnumerable<Guid> FindAllEndStateIds()
        {
            var fromStateIds = transitions.Select(t => t.FromStateId);
            var toStateIds = transitions.Select(t => t.ToStateId);
            return toStateIds.Except(fromStateIds);
        }
  
        public IEnumerable<Guid> GetOrphanedStates()
        {
            var fromStateIds = transitions.Select(t => t.FromStateId);
            var toStateIds = transitions.Select(t => t.ToStateId);
            return stateMap.Except(fromStateIds).Except(toStateIds);
        }

        public IEnumerable<IEnumerable<Guid>> FindAllPaths()
        {
            paths = new List<List<Guid>>();

            var adjacentStatesMap = BuildAdjacentStatesMap();
            var beginStateIds = FindAllBeginStateIds();

            foreach (var stateId in beginStateIds)
                FindAllWalksHelper(new List<Guid>() { stateId }, adjacentStatesMap);

            return paths;
        }

        private Dictionary<Guid, List<Guid>> BuildAdjacentStatesMap()
        {
            var adjacentStatesMap = new Dictionary<Guid, List<Guid>>();

            foreach (var transition in transitions)
            {
                if (adjacentStatesMap.ContainsKey(transition.FromStateId) == false)
                    adjacentStatesMap.Add(transition.FromStateId, new List<Guid>());

                adjacentStatesMap[transition.FromStateId].Add(transition.ToStateId);
            }

            return adjacentStatesMap;
        }

        private void FindAllWalksHelper(List<Guid> path, Dictionary<Guid, List<Guid>> adjacentStatesMap)
        {
            if (PathContainsRepeatedCycle(path))
                return;

            var lastStateId = path.Last();
            var adjacentStates = adjacentStatesMap[lastStateId];

            if (adjacentStates.Any() == false)
            {
                paths.Add(path.ToList());
                return;
            }

            foreach (var state in adjacentStates)
            {
                var newPath = path.ToList();
                newPath.Add(state);
                FindAllWalksHelper(newPath, adjacentStatesMap);
            }
        }

        private Boolean PathContainsRepeatedCycle(List<Guid> path)
        {
            for (var i = path.Count - 1; i > (path.Count - 1) / 2; i--)
            {
                var tailVertices = path.GetRange(i, path.Count - i);
                var tailVerticesOfRemainder = path.GetRange(i - tailVertices.Count, tailVertices.Count);

                if (Enumerable.SequenceEqual(tailVerticesOfRemainder, tailVertices))
                    return true;
            }

            return false;
        }  

        private class GraphTransition
        {
            public Guid FromStateId { get; set; }
            public Guid ToStateId { get; set; }
        }
    }
}
