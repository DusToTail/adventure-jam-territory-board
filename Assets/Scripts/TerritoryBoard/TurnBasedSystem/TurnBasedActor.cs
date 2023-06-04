namespace TerritoryBoard.TurnBasedSystem
{
    public class TurnBasedActor : ITurnBasedActor
    {
        string Utilities.IUniqueIdentifier.Id => _id;
        public bool HasSentInput { get; set; }
        public bool CanSendInput { get; set; }
        public bool HasFinishedAction { get; set; }

        protected ITurnBasedAction _action;
        private string _id;
        protected TurnController _turnController;

        public TurnBasedActor(string id, TurnController turnController)
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
        public virtual ITurnBasedAction SubmitAction()
        {
            if (_action == null)
            {
                _action = new TurnBasedAction(this);
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
