namespace TerritoryBoard.Mechanics
{
    public class MechanicsAction : IMechanicsAction
    {
        public IMechanicsActor Actor { get; set; }

        public delegate void OnEnter(IMechanicsAction action);
        public delegate void OnUpdate(IMechanicsAction action);
        public delegate void OnExit(IMechanicsAction action);
        public event OnEnter onEnter;
        public event OnUpdate onUpdate;
        public event OnExit onExit;

        public MechanicsAction(IMechanicsActor actor)
        {
            Actor = actor;
        }
        public void Execute()
        {
            EnterAction();
            onEnter?.Invoke(this);
            UpdateAction();
            onUpdate?.Invoke(this);
            ExitAction();
            onExit?.Invoke(this);
        }
        public virtual void EnterAction()
        {
        }

        public virtual void UpdateAction()
        {
        }

        public virtual void ExitAction()
        {
        }
    }

}
