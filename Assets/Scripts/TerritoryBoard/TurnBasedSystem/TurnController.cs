using System;
using System.Linq;
using System.Collections.Generic;

namespace TerritoryBoard.TurnController
{
    public class TurnController
    {
        public enum TurnMode
        {
            OneTurnOneActor,
            OneTurnAllActors
        }
        public int TurnCount { get { return _turns != null ? _turns.Count : -1; } }
        internal ITurn CurrentTurn { get { return _turns != null && _turns.Count > 0 ? _turns[_turns.Count - 1] : null; } }
        internal List<ITurn> Turns { get { return _turns; } }

        public ActorManager ActorManager { get; private set; }
        public int MaxTurnCount { get; private set; }
        public TurnMode Mode { get; private set; }
        public bool IsInitialized { get; private set; }
        public bool HasStarted { get; private set; }
        public bool HasEnded { get; private set; }

        public delegate void Started();
        public delegate void Ended();
        public event Started onStarted;
        public event Ended onEnded;

        private IEnumerator<ITurnBasedActor> _actorsEnumerator;
        private IEnumerable<ITurnBasedActor> _actorsEnumerable;
        private List<ITurn> _turns;

        public TurnController()
        {
            ActorManager = new ActorManager();
            _turns = new List<ITurn>();
            IsInitialized = false;
            HasStarted = false;
            HasEnded = false;
        }
        public void Initialize(ITurnBasedActor[] actors, Config config)
        {
            MaxTurnCount = config.maxTurnCount;
            Mode = config.mode;
            foreach(var actor in actors)
            {
                ActorManager.RegisterActor(actor);
            }
            _actorsEnumerable = ActorManager.Dictionary.Values.OrderBy(x => x.Order);
            _actorsEnumerator = _actorsEnumerable.GetEnumerator();
            IsInitialized = true;
        }
        public void Shutdown()
        {
            foreach (var actor in ActorManager.Dictionary.Values.ToArray())
            {
                ActorManager.UnregisterActor(actor);
            }
            _actorsEnumerator.Dispose();
        }
        public void StartGame()
        {
            onStarted?.Invoke();
            HasStarted = true;

            var head = _actorsEnumerable.First((x) => x.IsActive);
            head.IsTurnOwner = true;
        }
        public void EndGame()
        {
            onEnded?.Invoke();
            HasEnded = true;
        }
        internal void StartTurn(ITurnBasedActor actor)
        {
            if (Mode == TurnMode.OneTurnOneActor)
            {
                Turn turn = new Turn((_turns.Count).ToString());
                turn.AddActor(actor);
                _turns.Add(turn);

                while (_actorsEnumerator.Current != actor)
                {
                    bool nextIsInBound = _actorsEnumerator.MoveNext();
                    if (!nextIsInBound)
                    {
                        _actorsEnumerator = _actorsEnumerable.GetEnumerator();
                    }
                }
            }
            else if (Mode == TurnMode.OneTurnAllActors)
            {
                var head = _actorsEnumerable.First((x)=>x.IsActive);
                if(actor == head)
                {
                    Turn turn = new Turn((_turns.Count).ToString());
                    _actorsEnumerator = _actorsEnumerable.GetEnumerator();
                    while (_actorsEnumerator.MoveNext())
                    {
                        turn.AddActor(_actorsEnumerator.Current);
                    }
                    _turns.Add(turn);
                    _actorsEnumerator = _actorsEnumerable.GetEnumerator();
                }

                while (_actorsEnumerator.Current != actor)
                {
                    bool nextIsInBound = _actorsEnumerator.MoveNext();
                    if (!nextIsInBound)
                    {
                        _actorsEnumerator = _actorsEnumerable.GetEnumerator();
                    }
                }
            }
        }
        internal void EndTurn(ITurnBasedActor actor)
        {
            if(_turns.Count > MaxTurnCount)
            {
                EndGame();
                return;
            }

            while (true)
            {
                bool nextIsInBound = _actorsEnumerator.MoveNext();
                if (nextIsInBound && _actorsEnumerator.Current.IsActive)
                {
                    break;
                }
                else if (!nextIsInBound)
                {
                    _actorsEnumerator = _actorsEnumerable.GetEnumerator();
                }
            }
            actor.IsTurnOwner = false;
            _actorsEnumerator.Current.IsTurnOwner = true;
        }
        internal void UpdateTurn(ITurnBasedActor actor, ITurnBasedAction action)
        {
            if (actor == null)
            {
                throw new ArgumentNullException(nameof(actor));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            var turn = CurrentTurn;

            if (!turn.ActorsDictionary.ContainsKey(actor))
            {
                throw new ArgumentException($"Actor {actor.Id} does not exist in turn {turn.Id}");
            }
            turn.AddAction(action, actor);
        }

        [Serializable]
        public struct Config
        {
            public TurnMode mode;
            public int maxTurnCount;
        }
    }
}