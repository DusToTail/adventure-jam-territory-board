namespace TerritoryBoard.Interaction
{
    public interface ISelector
    {
        public ISelectable CurrentSelectable { get; }
        public ISelectable PreviousSelectable { get; }
        public void Select(ISelectable currentSelectable);
    }
}
