using System;

namespace TerritoryBoard.TurnController
{
    public class TurnBasedActor : ITurnBasedActor
    {
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsTurnOwner { get; set; }

        string Utilities.IUniqueIdentifier.Id => _id;
        private string _id;
        private TurnController _turnController;

        public TurnBasedActor(string id, int order)
        {
            _id = id;
            Order = order;
        }
        public void BindController(TurnController turnController)
        {
            _turnController = turnController;

            _turnController.onStarted += () =>
            {
                IsActive = true;
                IsTurnOwner = false;
            };
            _turnController.onEnded += () =>
            {
                IsActive = false;
                IsTurnOwner = false;
            };
        }
        public void ExecuteAction(ITurnBasedAction action)
        {
            if(action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            action.Execute();
            _turnController.UpdateTurn(this, action);
        }
        public virtual void StartTurn()
        {
            _turnController.StartTurn(this);
        }
        public virtual void EndTurn()
        {
            _turnController.EndTurn(this);
        }
    }
}
