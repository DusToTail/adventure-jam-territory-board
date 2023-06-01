using System;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    public class TurnBasedEngine
    {
        public enum EngineState : int
        {
            Standby = 0,
            Executing = 1
        }
        public static TurnBasedEngine Instance {
            get
            {
                if(_instance == null)
                {
                    _instance = new TurnBasedEngine();
                    return _instance;
                }
                return _instance;
            }
        }
        public TurnController TurnController { get; private set; }
        public ActorManager ActorManager { get; private set; }

        public EngineState State { get { return _state; } set { _state = value; onStateChanged?.Invoke(_state); } }
        public delegate void OnStateChanged(EngineState newState);
        public event OnStateChanged onStateChanged;

        private static TurnBasedEngine _instance;
        private EngineState _state = EngineState.Standby;

        private TurnBasedEngine()
        {
        }
        public void SetTurnController(TurnController turnController)
        {
            TurnController = turnController;
        }
        public void SetActorManager(ActorManager actorManager)
        {
            ActorManager = actorManager;
        }
        public void Rebind()
        {
            ValidateComponent();
            TurnController.Initialize(ActorManager);
        }
        public async Task RunTurnAsync()
        {
            State = EngineState.Executing;

            ValidateComponent();
            await TurnController.RunAsync();

            State = EngineState.Standby;
        }
        public void RegisterActor(IActor actor) => ActorManager.RegisterActor(actor);
        public void UnregisterActor(IActor actor) => ActorManager.UnregisterActor(actor);
        private void ValidateComponent()
        {
            if (TurnController == null)
            {
                throw new InvalidOperationException("TurnBasedEngine: there is no TurnController attached!");
            }
            if (ActorManager == null)
            {
                throw new InvalidOperationException("TurnBasedEngine: there is no ActorManager attached!");
            }
        }
    }
}
