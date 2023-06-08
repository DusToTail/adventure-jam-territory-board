using UnityEngine;
using UnityEngine.InputSystem;
using TerritoryBoard.TurnController;

[DefaultExecutionOrder(int.MaxValue)]
public class TestTurnBasedSystem : MonoBehaviour
{
    [SerializeField] private TurnController.Config config;
    [SerializeField] ActorContainer[] actorContainers;
    public static TurnController turnController;

    private void Start()
    {
        turnController = new TurnController();

        ITurnBasedActor[] actors = new ITurnBasedActor[actorContainers.Length];
        for (int i = 0; i < actors.Length; i++)
        {
            actors[i] = actorContainers[i].Actor;
            actors[i].BindController(turnController);
        }

        turnController.Initialize(actors, config);
        turnController.onStarted += () => { Debug.Log("TurnController Started!"); };
        turnController.onEnded += () => { Debug.Log("TurnController Ended!"); };
    }

    private void Update()
    {
        if (turnController.HasStarted) { return; }
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            turnController.StartGame();
        }
    }

    private void OnDestroy()
    {
        turnController.Shutdown();
    }
}
