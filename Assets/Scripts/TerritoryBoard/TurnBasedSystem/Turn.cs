using System.Collections.Generic;
using System.Threading.Tasks;

namespace TerritoryBoard.TurnBasedSystem
{
    public abstract class Turn : ITurn, Utilities.IUniqueIdentifier
    {
        public string Id { get { return _id; }}
        public List<ITurnBasedActor> Actors { get; protected set; }
        public List<ITurnBasedAction> Actions { get; protected set; }
        private string _id;

        public Turn(string id)
        {
            _id = id;
            Actions = new List<ITurnBasedAction>();
            Actors = new List<ITurnBasedActor>();
        }

        public abstract bool ReadyForExcution();
        public abstract bool HasFinishedExecution();
        public abstract Task Execute();
        public abstract void AddActor(ITurnBasedActor actor);
        public abstract void AddAction(ITurnBasedAction action);
    }
}
