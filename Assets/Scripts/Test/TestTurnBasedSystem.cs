using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using TurnBasedSystem;

namespace Test
{
    public class TestTurnBasedSystem : MonoBehaviour
    {
        public enum Mode
        {
            PerActor,
            PerCycle
        }
        [SerializeField] private Mode turnMode;
        [SerializeField] ActorContainer[] actorContainers;
        private TurnBasedEngine _engine;
        private TurnController _turnController;
        private ActorManager _actorManager;

        private void Start()
        {
            if(turnMode == Mode.PerActor)
            {
                _turnController = new PerActorTurnController();
            }
            else if(turnMode == Mode.PerCycle)
            {
                _turnController = new PerCycleTurnController();
            }
            _actorManager = new ActorManager();
            _engine = TurnBasedEngine.Instance;
            _engine.SetTurnController(_turnController);
            _engine.SetActorManager(_actorManager);
            _engine.Rebind();

            _engine.onStateChanged += (x) =>
            {
                Debug.Log("State:" + x);
            };
            _turnController.onPhaseChanged += (x) =>
            {
                Debug.Log("Phase:" + x);
            };
        }

        private async void Update()
        {
            if (_engine == null || _engine.State != TurnBasedEngine.EngineState.Standby) { return; }
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                foreach (var actor in actorContainers)
                {
                    _engine.RegisterActor(actor.Actor);
                }

                Debug.Log($">>>Turn No.{_engine.TurnController.Turns.Count}");
                await _engine.RunTurnAsync();
                Debug.Log($"Turn ID {_engine.TurnController.CurrentTurn.Id}<<<");

                foreach (var actor in actorContainers)
                {
                    _engine.UnregisterActor(actor.Actor);
                }
            }
        }
    }
}
