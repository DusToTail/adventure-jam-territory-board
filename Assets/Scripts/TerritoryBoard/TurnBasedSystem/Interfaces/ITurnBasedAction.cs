namespace TerritoryBoard.TurnBasedSystem
{
    public interface ITurnBasedAction
    {
        public delegate void OnActionFinished(ITurnBasedAction action);
        public event OnActionFinished onActionFinished;
        public ITurnBasedActor Actor { get; }
        public void Execute();
    }
}
