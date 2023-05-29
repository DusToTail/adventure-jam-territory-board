using Mechanics;

namespace Board
{
    public interface IUnit : IGridEntity
    {
        public void ExecuteAction();
        public void EnterAction();
        public void UpdateAction();
        public void ExitAction();
    }
}
