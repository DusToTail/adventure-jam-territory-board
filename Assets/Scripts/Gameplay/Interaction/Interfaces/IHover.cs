namespace Gameplay.Interaction
{
    public interface IHover
    {
        public IHoverable CurrentHoverable { get; }
        public IHoverable PreviousHoverable { get; }
        public void Hover(IHoverable currentHoverable);
    }
}
