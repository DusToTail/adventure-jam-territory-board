using System.Collections.Generic;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    public abstract class TurnController
    {
        public enum TurnPhase : int
        {
            Beginning,
            Waiting,
            Executing,
            Ending
        }

        public Turn CurrentTurn { get { return _turns != null && _turns.Count > 0 ? _turns[_turns.Count - 1] : null; } }
        public List<Turn> Turns { get { return _turns; } }

        

        public TurnPhase Phase { get { return _phase; } protected set { _phase = value; onPhaseChanged?.Invoke(_phase); } }
        public delegate void OnTurnPhaseChanged(TurnPhase newState);
        public event OnTurnPhaseChanged onPhaseChanged;

        public int MaxTurnCount { get; set; } = 10;

        protected ActorManager _actorManager;
        
        private TurnPhase _phase;
        private List<Turn> _turns;

        public void Initialize(ActorManager actorManager)
        {
            _turns = new List<Turn>();
            _actorManager = actorManager;
        }
        internal async Task RunAsync()
        {
            {
                Phase = TurnPhase.Beginning;
                BeginTurn();
            }

            {
                Phase = TurnPhase.Waiting;
                await WaitForActionAsync();
            }

            {
                Phase = TurnPhase.Executing;
                await ExecuteAsync();
            }

            {
                Phase = TurnPhase.Ending;
                EndTurn();
            }
        }
        public abstract void BeginTurn();
        public async virtual Task WaitForActionAsync()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    if (CurrentTurn.ReadyForExcution()) { break; }
                    await Task.Delay(200);
                }
            });
        }
        public async virtual Task ExecuteAsync()
        {
            await Task.Run(async () => await CurrentTurn.Execute());
        }
        public virtual void EndTurn()
        {
        }
    }
}