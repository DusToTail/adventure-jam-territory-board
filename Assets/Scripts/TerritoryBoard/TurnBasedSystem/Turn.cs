using System.Collections.Generic;
using System;

namespace TerritoryBoard.TurnController
{
    internal class Turn : ITurn, Utilities.IUniqueIdentifier
    {
        public string Id { get { return _id; }}
        public Dictionary<ITurnBasedActor, List<ITurnBasedAction>> ActorsDictionary => _actorsDictionary;

        private string _id;
        private Dictionary<ITurnBasedActor, List<ITurnBasedAction>> _actorsDictionary;

        internal Turn(string id)
        {
            _id = id;
            _actorsDictionary = new Dictionary<ITurnBasedActor, List<ITurnBasedAction>>();
        }

        public void AddActor(ITurnBasedActor actor)
        {
            if(actor == null)
            {
                throw new ArgumentNullException(nameof(actor));
            }
            if(_actorsDictionary.ContainsKey(actor))
            {
                throw new ArgumentException($"Actor ({actor.Id}) is already registered in turn {Id}");
            }
            _actorsDictionary.TryAdd(actor, new List<ITurnBasedAction>());
        }
        public void AddAction(ITurnBasedAction action, ITurnBasedActor actor)
        {
            if (actor == null)
            {
                throw new ArgumentNullException(nameof(actor));
            }
            if (!_actorsDictionary.ContainsKey(actor))
            {
                throw new ArgumentException($"Actor ({actor.Id}) does not exist in turn {Id}");
            }
            if(action == null)
            {
                throw new ArgumentNullException(nameof(actor));
            }
            var actions = _actorsDictionary[actor];
            actions.Add(action);
        }
    }
}
