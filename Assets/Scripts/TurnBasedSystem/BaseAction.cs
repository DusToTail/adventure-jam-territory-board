namespace TurnBasedSystem
{
    public class BaseAction : IAction
    {
        public IActor Actor => _actor;
        protected IActor _actor;
        public event IAction.OnActionFinished onActionFinished;

        public BaseAction(IActor actor)
        {
            _actor = actor;
        }

        public virtual void Execute()
        {
            onActionFinished?.Invoke(this);
        }
    }
}
