namespace TurnBasedSystem
{
    public class BaseActor : IActor
    {
        string Utilities.IUniqueIdentifier.Id => _id;
        public bool HasSentInput { get; set; }
        public bool CanSendInput { get; set; }
        public bool HasFinishedAction { get; set; }

        protected IAction _action;
        private string _id;
        protected TurnController _turnController;

        public BaseActor(string id, TurnController turnController)
        {
            _id = id;
            _turnController = turnController;
        }

        public virtual void ResetState()
        {
            CanSendInput = true;
            HasSentInput = false;
            HasFinishedAction = false;
        }
        public virtual IAction SubmitAction()
        {
            if (_action == null)
            {
                _action = new BaseAction(this);
            }
            HasSentInput = true;
            CanSendInput = false;
            _turnController.CurrentTurn.AddAction(_action);
            _action.onActionFinished += (x) =>
            {
                HasFinishedAction = true;
            };
            return _action;
        }
    }
}
