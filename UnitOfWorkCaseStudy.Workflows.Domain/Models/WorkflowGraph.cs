using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Models
{
    //public class WorkflowGraph
    //{
    //    //private readonly HashSet<Guid> states;
    //    //private readonly List<Transition> transitions;
    //    //private readonly HashSet<Guid> fromStates;
    //    //private readonly HashSet<Guid> toStates;
    //    //private List<WorkflowPath> paths;

    //    //public WorkflowGraph()
    //    //{
    //    //    states = new HashSet<Guid>();
    //    //    transitions = new List<Transition>();
    //    //}

    //    //public void AddState(Guid state)
    //    //{
    //    //    states.Add(state);
    //    //}

    //    //public void AddTransition(Guid from, Guid to)
    //    //{
    //    //    fromStates.Add(from);
    //    //    toStates.Add(to);
    //    //    transitions.Add(new Transition() { From = from, To = to });
    //    //}
        
    //    //public Boolean HasOneBeginState()
    //    //{
    //    //    return fromStates.Except(toStates).Count() == 1;
    //    //}

    //    //public Boolean IsBeginState(Guid stateId)
    //    //{
    //    //    return fromStates.Except(toStates).Contains(stateId);
    //    //}

    //    //public Boolean HasOneEndState()
    //    //{
    //    //    return toStates.Except(fromStates).Count() == 1;
    //    //}

    //    //public Boolean IsEndState(Guid stateId)
    //    //{
    //    //    return toStates.Except(fromStates).Contains(stateId);
    //    //}

    //    //public Boolean HasOrphanedStates()
    //    //{
    //    //    return states.Except(fromStates).Except(toStates).Any();
    //    //}

    //    //public IEnumerable<WorkflowPath> FindAllPaths()
    //    //{
    //    //    if (HasOneEndState() == false)
    //    //        throw new WorkflowExceptions.InfiniteCycleException();

    //    //    paths = new List<WorkflowPath>();

    //    //    var beginStates = fromStates.Except(toStates);

    //    //    foreach (var state in beginStates)
    //    //        foreach (var transition in GetTransitionsFrom(state))
    //    //            FindAllWalksHelper(new WorkflowPath(new[] { transition }));

    //    //    return paths;
    //    //}

    //    //private void FindAllWalksHelper(WorkflowPath path)
    //    //{
    //    //    if (path.ContainsRepeatedCycle())
    //    //        return;

    //    //    var tail = path.Tail;
    //    //    var transitionsFromTail = GetTransitionsFrom(tail);

    //    //    if (transitionsFromTail.Any() == false)
    //    //    {
    //    //        paths.Add(WorkflowPath.CreateFrom(path));
    //    //        return;
    //    //    }

    //    //    foreach (var transition in transitionsFromTail)
    //    //    {
    //    //        var newPath = WorkflowPath.CreateFrom(path);
    //    //        newPath.AddTransition(transition);
    //    //        FindAllWalksHelper(newPath);
    //    //    }
    //    //}

    //    //private IEnumerable<Transition> GetTransitionsFrom(Guid stateId)
    //    //{
    //    //    return transitions.Where(t => t.FromStateId.Equals(stateId));
    //    //}

    //    //private class Transition
    //    //{
    //    //    public Guid From { get; set; }
    //    //    public Guid To { get; set; }
    //    //}
    //}
}