namespace Gameplay.Interaction
{
    public interface ISelectable
    {
        public bool Selected { get; set; }
        public void OnSelectEnter();
        public void OnSelectExit();
    }
}
