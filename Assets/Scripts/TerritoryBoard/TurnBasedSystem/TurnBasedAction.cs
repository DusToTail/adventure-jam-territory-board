namespace TerritoryBoard.TurnBasedSystem
{
    public class TurnBasedAction : ITurnBasedAction
    {
        public ITurnBasedActor Actor => _actor;
        protected ITurnBasedActor _actor;
        public event ITurnBasedAction.OnActionFinished onActionFinished;

        public TurnBasedAction(ITurnBasedActor actor)
        {
            _actor = actor;
        }
        public void Execute()
        {
            ExecuteImpl();
            onActionFinished?.Invoke(this);
        }
        public virtual void ExecuteImpl()
        {
        }
    }
}
