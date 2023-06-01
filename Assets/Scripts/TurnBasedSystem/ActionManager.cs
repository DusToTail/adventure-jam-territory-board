using System.Collections.Generic;

namespace TurnBasedSystem
{
    //public class ActionManager
    //{
    //    public List<IAction> Actions { get; private set; }
    //    public int Count { get { return Actions == null ? -1 : Actions.Count; } }
    //    public int Capacity { get { return Actions == null ? -1 : Actions.Capacity; } }
    //    public ActionManager()
    //    {
    //        Actions = new List<IAction>();
    //    }
    //    internal void RegisterAction(IAction action)
    //    {
    //        Actions.Add(action);
    //    }
    //    internal void UnregisterAction(IAction action)
    //    {
    //        Actions.Remove(action);
    //    }
    //    internal void Sort(IComparer<IAction> comparer)
    //    {
    //        Actions.Sort(comparer);
    //    }
    //    public IAction GetActionFromID(long id)
    //    {
    //        return Actions.Find(x => x.Id == id);
    //    }
    //    public string[] GetActionsInfo()
    //    { return System.Array.ConvertAll(Actions.ToArray(), new System.Converter<IAction, string>(IActionToString)); }
    //    private string IActionToString(IAction action)
    //    { return action.GetActionInfo().ToString(); }
    //}
}
