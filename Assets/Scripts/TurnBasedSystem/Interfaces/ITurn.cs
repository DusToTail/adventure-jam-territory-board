using System.Collections.Generic;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    public interface ITurn
    {
        public string Id { get; }
        public List<IActor> Actors { get;}
        public List<IAction> Actions { get;}

        public bool ReadyForExcution();
        public bool HasFinishedExecution();
        public Task Execute();
        public void AddActor(IActor actor);
        public void AddAction(IAction action);
    }
}
