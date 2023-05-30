namespace Mechanics
{
    public class BaseAction : IAction
    {
        public IActor Actor { get; set; }

        public event IAction.OnEnter onEnter;
        public event IAction.OnUpdate onUpdate;
        public event IAction.OnExit onExit;

        public BaseAction(IActor actor)
        {
            Actor = actor;
        }

        public virtual void EnterAction()
        {
            onEnter?.Invoke(this);
        }

        public virtual void UpdateAction()
        {
            onUpdate?.Invoke(this);
        }

        public virtual void ExitAction()
        {
            onExit?.Invoke(this);
        }
    }

}
