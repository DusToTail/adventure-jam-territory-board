namespace Gameplay.Interaction
{
    public interface IHoverable
    {
        public bool Hovered { get; set; }
        public void OnHoverEnter();
        public void OnHoverExit();
    }
}
