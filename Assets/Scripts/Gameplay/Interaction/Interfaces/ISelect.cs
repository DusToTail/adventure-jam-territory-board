namespace Gameplay.Interaction
{
    public interface ISelect
    {
        public ISelectable CurrentSelectable { get; }
        public ISelectable PreviousSelectable { get; }
        public void Select(ISelectable currentSelectable);
    }
}
