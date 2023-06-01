namespace TerritoryBoard.Mechanics
{
    public interface IAction
    {
        public delegate void OnEnter(IAction action);
        public delegate void OnUpdate(IAction action);
        public delegate void OnExit(IAction action);
        public event OnEnter onEnter;
        public event OnUpdate onUpdate;
        public event OnExit onExit;
        public IActor Actor { get; set; }
        public void EnterAction();
        public void UpdateAction();
        public void ExitAction();
    }
}
