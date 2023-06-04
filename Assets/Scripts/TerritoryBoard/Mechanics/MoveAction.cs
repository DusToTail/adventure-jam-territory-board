namespace TerritoryBoard.Mechanics
{
    public class MoveAction : MechanicsAction
    {
        private int newX;
        private int newY;
        private IMovableActor _actor;

        public MoveAction(int newX, int newY, IMovableActor actor) : base(actor)
        {
            this.newX = newX;
            this.newY = newY;
            _actor = actor;
        }

        public override void EnterAction()
        {
            base.EnterAction();
        }

        public override void UpdateAction()
        {
            _actor.MoveToPosition(newX, newY);
            base.UpdateAction();
        }

        public override void ExitAction()
        {
            base.ExitAction();
        }
    }
}
