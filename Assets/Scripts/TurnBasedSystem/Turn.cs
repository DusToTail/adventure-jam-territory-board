using System.Collections.Generic;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    public abstract class Turn : ITurn, Utilities.IUniqueIdentifier
    {
        public string Id { get { return _id; }}
        public List<IActor> Actors { get; protected set; }
        public List<IAction> Actions { get; protected set; }
        private string _id;

        public Turn(string id)
        {
            _id = id;
            Actions = new List<IAction>();
            Actors = new List<IActor>();
        }

        public abstract bool ReadyForExcution();
        public abstract bool HasFinishedExecution();
        public abstract Task Execute();
        public abstract void AddActor(IActor actor);
        public abstract void AddAction(IAction action);
    }
}
