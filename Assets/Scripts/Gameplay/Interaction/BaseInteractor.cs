using UnityEngine;

namespace Gameplay.Interaction
{
    public class BaseInteractor : MonoBehaviour, ISelect, IHover
    {

        public ISelectable CurrentSelectable { get; protected set; }
        public ISelectable PreviousSelectable { get; protected set; }

        public void Select(ISelectable currentSelectable)
        {
            PreviousSelectable?.OnSelectExit();
            CurrentSelectable = currentSelectable;
            CurrentSelectable?.OnSelectExit();
        }

        public IHoverable CurrentHoverable { get; protected set; }
        public IHoverable PreviousHoverable { get; protected set; }

        public void Hover(IHoverable currentHoverable)
        {
            PreviousHoverable?.OnHoverExit();
            CurrentHoverable = currentHoverable;
            CurrentHoverable?.OnHoverExit();
        }
    }
}
