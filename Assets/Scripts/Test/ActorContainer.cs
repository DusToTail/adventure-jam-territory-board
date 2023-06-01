using UnityEngine;
using UnityEngine.InputSystem;
using TurnBasedSystem;

public class ActorContainer : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private bool debugLogInput;
    public IActor Actor { get { return _actor; } }

    private BaseActor _actor;

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

public class SimpleActor : BaseActor
{
    public SimpleActor(string id, TurnController turnController):base(id, turnController)
    {
    }
    public override IAction SubmitAction()
    {
        _action = new DebugLog("Hello World!", this);
        return base.SubmitAction();
    }
}

public class DebugLog : BaseAction
{
    private string _message;
    public DebugLog(string message, IActor actor):base(actor)
    {
        _message = message;
    }

    public override void Execute()
    {
        Debug.Log(_message);
        base.Execute();
    }
}
