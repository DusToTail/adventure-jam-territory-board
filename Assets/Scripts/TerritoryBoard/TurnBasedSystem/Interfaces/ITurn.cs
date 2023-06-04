using System.Collections.Generic;
using System.Threading.Tasks;

namespace TerritoryBoard.TurnBasedSystem
{
    public interface ITurn
    {
        public string Id { get; }
        public List<ITurnBasedActor> Actors { get;}
        public List<ITurnBasedAction> Actions { get;}

        public bool ReadyForExcution();
        public bool HasFinishedExecution();
        public Task Execute();
        public void AddActor(ITurnBasedActor actor);
        public void AddAction(ITurnBasedAction action);
    }
}
