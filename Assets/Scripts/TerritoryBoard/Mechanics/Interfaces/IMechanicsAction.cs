namespace TerritoryBoard.Mechanics
{
    public interface IMechanicsAction
    {
        public delegate void OnEnter(IMechanicsAction action);
        public delegate void OnUpdate(IMechanicsAction action);
        public delegate void OnExit(IMechanicsAction action);
        public event OnEnter onEnter;
        public event OnUpdate onUpdate;
        public event OnExit onExit;
        public IMechanicsActor Actor { get; set; }
        public void Execute();
        public void EnterAction();
        public void UpdateAction();
        public void ExitAction();
    }
}
