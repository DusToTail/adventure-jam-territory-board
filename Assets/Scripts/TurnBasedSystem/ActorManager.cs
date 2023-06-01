using System;
using Utilities;

namespace TurnBasedSystem
{
    public class ActorManager : BaseManager<IActor>
    {
        public void RegisterActor(IActor actor)
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

        public void UnregisterActor(IActor actor)
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
