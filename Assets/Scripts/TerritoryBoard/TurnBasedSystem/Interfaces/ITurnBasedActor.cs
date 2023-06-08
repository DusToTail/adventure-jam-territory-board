namespace TerritoryBoard.TurnController
{
    public interface ITurnBasedActor : Utilities.IUniqueIdentifier
    {
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsTurnOwner { get; set; }
        public void BindController(TurnController controller);
        public void ExecuteAction(ITurnBasedAction action);
        public void StartTurn();
        public void EndTurn();
    }
}
