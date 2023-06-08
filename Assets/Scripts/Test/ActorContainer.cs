using UnityEngine;
using UnityEngine.InputSystem;
using TerritoryBoard.TurnController;

[DefaultExecutionOrder(int.MinValue)]
public class ActorContainer : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private int order;
    [SerializeField] private InputAction action;
    [SerializeField] private bool debugLogInput;
    public ITurnBasedActor Actor { get { return _actor; } }
    private TurnBasedActor _actor;

    private void Start()
    {
        _actor = new TurnBasedActor(id, order);
        action.performed += (ctx) =>
        {
            if (!_actor.IsTurnOwner) { Debug.LogWarning($"{gameObject.name} ({Actor.Id}): is not the turn owner! Please be patient"); return; }
            _actor.StartTurn();
            _actor.ExecuteAction(new DebugLog($"{gameObject.name} ({Actor.Id}): Hello World!", _actor));
            _actor.EndTurn();
        };
    }
    private void Update()
    {
        if (!_actor.IsTurnOwner) { return; }
        if(debugLogInput)
            Debug.Log($"{gameObject.name} ({Actor.Id}): Waiting for Input...");
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
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
