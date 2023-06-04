namespace TerritoryBoard.TurnBasedSystem
{
    public class TurnBasedActor : ITurnBasedActor
    {
        string Utilities.IUniqueIdentifier.Id => id;
        public bool HasSentInput { get; set; }
        public bool CanSendInput { get; set; }
        public bool HasFinishedAction { get; set; }

        protected ITurnBasedAction turnBasedAction;
        protected string id;
        protected TurnController turnController;

        public TurnBasedActor(string id, TurnController turnController)
        {
            this.id = id;
            this.turnController = turnController;
        }

        public virtual void ResetState()
        {
            CanSendInput = true;
            HasSentInput = false;
            HasFinishedAction = false;
        }
        public virtual ITurnBasedAction SubmitAction()
        {
            if (turnBasedAction == null)
            {
                turnBasedAction = new TurnBasedAction(this);
            }
            HasSentInput = true;
            CanSendInput = false;
            turnController.CurrentTurn.AddAction(turnBasedAction);
            turnBasedAction.onActionFinished += (x) =>
            {
                HasFinishedAction = true;
            };
            return turnBasedAction;
        }
    }
}
