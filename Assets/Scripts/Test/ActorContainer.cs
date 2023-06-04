using UnityEngine;
using UnityEngine.InputSystem;
using TerritoryBoard.TurnBasedSystem;

public class ActorContainer : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private bool debugLogInput;
    public ITurnBasedActor Actor { get { return _actor; } }

    private TurnBasedActor _actor;

    private void Start()
    {
        _actor = new SimpleActor(id, TurnBasedEngine.Instance.TurnController);
    }
    private void Update()
    {
        if (!_actor.CanSendInput || _actor.HasSentInput) { return; }
        if(debugLogInput)
            Debug.Log($"{gameObject.name} ({Actor.Id}): Waiting for Input...");
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (debugLogInput)
                Debug.LogWarning("Pressed Space Key!");
            _actor.SubmitAction();
        }
    }
}

public class SimpleActor : TurnBasedActor
{
    public SimpleActor(string id, TurnController turnController):base(id, turnController)
    {
    }
    public override ITurnBasedAction SubmitAction()
    {
        turnBasedAction = new DebugLog("Hello World!", this);
        return base.SubmitAction();
    }
}

public class DebugLog : TurnBasedAction
{
    private string _message;
    public DebugLog(string message, ITurnBasedActor actor):base(actor)
    {
        _message = message;
    }

    public override void ExecuteImpl()
    {
        Debug.Log(_message);
    }
}
