using System.Collections.Generic;
using System.Threading.Tasks;

namespace TerritoryBoard.TurnController
{
    internal interface ITurn
    {
        public string Id { get; }
        public Dictionary<ITurnBasedActor, List<ITurnBasedAction>> ActorsDictionary { get;}
        public void AddActor(ITurnBasedActor actor);
        public void AddAction(ITurnBasedAction action, ITurnBasedActor actor);
    }
}
