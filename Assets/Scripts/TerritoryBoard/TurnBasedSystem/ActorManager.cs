using System;
using TerritoryBoard.Utilities;

namespace TerritoryBoard.TurnController
{
    public class ActorManager : BaseManager<ITurnBasedActor>
    {
        public void RegisterActor(ITurnBasedActor actor)
        {
            if (actor == null)
            {
                throw new ArgumentNullException("TurnBasedEngine: can not register null!");
            }
            if (Get(actor.Id) != null)
            {
                throw new InvalidOperationException($"TurnBasedEngine: actor {actor.Id} already registered!");
            }
            Add(actor);
        }

        public void UnregisterActor(ITurnBasedActor actor)
        {
            if (actor == null)
            {
                throw new ArgumentNullException("TurnBasedEngine: can not unregister null!");
            }
            if (Get(actor.Id) == null)
            {
                throw new InvalidOperationException($"TurnBasedEngine: actor {actor.Id} does not exist!");
            }
            Remove(actor);
        }
    }
}
