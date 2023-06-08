namespace TerritoryBoard.TurnController
{
    public interface ITurnBasedAction
    {
        public delegate void OnActionFinished(ITurnBasedAction action);
        public event OnActionFinished onActionFinished;
        public void Execute();
    }
}
