using System.Collections.Generic;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    public class PerCycleTurn : Turn
    {
        public List<IActor> Actors { get; private set; }
        public List<IAction> Actions { get; private set; }

        public PerCycleTurn(string id) : base(id)
        {
            Actors = new List<IActor>();
            Actions = new List<IAction>();
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
        public override bool FinishedExcution()
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

        public override void AddActor(IActor actor)
        {
            Actors.Add(actor);
        }
        public override void AddAction(IAction action)
        {
            Actions.Add(action);
        }
    }
}
