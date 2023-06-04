namespace TerritoryBoard.Mechanics
{
    public class MechanicsAction : IMechanicsAction
    {
        public IMechanicsActor Actor { get; set; }

        public event IMechanicsAction.OnEnter onEnter;
        public event IMechanicsAction.OnUpdate onUpdate;
        public event IMechanicsAction.OnExit onExit;

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
