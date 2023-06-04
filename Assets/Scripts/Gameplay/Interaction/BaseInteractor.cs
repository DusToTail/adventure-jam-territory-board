using UnityEngine;

namespace Gameplay.Interaction
{
    public class BaseInteractor : MonoBehaviour, ISelect, IHover
    {

        public ISelectable CurrentSelectable { get; protected set; }
        public ISelectable PreviousSelectable { get; protected set; }

        public void Select(ISelectable currentSelectable)
        {
            PreviousSelectable = CurrentSelectable;
            if(PreviousSelectable != null)
            {
                PreviousSelectable.OnSelectExit();
                PreviousSelectable.Selected = false;
            }

            if (currentSelectable != null)
            {
                CurrentSelectable = currentSelectable;
                CurrentSelectable.OnSelectEnter();
                CurrentSelectable.Selected = true;
            }
        }

        public IHoverable CurrentHoverable { get; protected set; }
        public IHoverable PreviousHoverable { get; protected set; }

        public void Hover(IHoverable currentHoverable)
        {
            PreviousHoverable = CurrentHoverable;
            if (PreviousHoverable != null)
            {
                PreviousHoverable.OnHoverExit();
                PreviousHoverable.Hovered = false;
            }

            if (currentHoverable != null)
            {
                CurrentHoverable = currentHoverable;
                CurrentHoverable.OnHoverEnter();
                CurrentHoverable.Hovered = true;
            }

        }
    }
}
