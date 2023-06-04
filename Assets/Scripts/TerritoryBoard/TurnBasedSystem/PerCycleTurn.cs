using System.Collections.Generic;
using System.Threading.Tasks;

namespace TerritoryBoard.TurnBasedSystem
{
    public class PerCycleTurn : Turn
    {
        public PerCycleTurn(string id) : base(id)
        {
            Actors = new List<ITurnBasedActor>();
            Actions = new List<ITurnBasedAction>();
        }

        public override bool ReadyForExcution()
        {
            foreach (var actor in Actors)
            {
                if (!actor.HasSentInput)
                {
                    return false;
                }
            }
            return true;
        }
        public override bool HasFinishedExecution()
        {
            foreach (var actor in Actors)
            {
                if (!actor.HasFinishedAction)
                {
                    return false;
                }
            }
            return true;
        }

        public override Task Execute()
        {
            List<Task> tasks = new List<Task>();
            foreach (var action in Actions)
            {
                tasks.Add(Task.Run(() =>
                {
                    action.Execute();
                }));
            }
            return Task.WhenAll(tasks);
        }

        public override void AddActor(ITurnBasedActor actor)
        {
            Actors.Add(actor);
        }
        public override void AddAction(ITurnBasedAction action)
        {
            Actions.Add(action);
        }
    }
}
