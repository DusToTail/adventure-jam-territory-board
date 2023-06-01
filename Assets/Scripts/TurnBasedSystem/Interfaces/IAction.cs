namespace TurnBasedSystem
{
    public interface IAction
    {
        public delegate void OnActionFinished(IAction action);
        public event OnActionFinished onActionFinished;
        public IActor Actor { get; }
        public void Execute();
    }
}
