using System.Collections;
using UnityEngine;

namespace Gameplay.Interaction
{
    [RequireComponent(typeof(Collider))]
    public abstract class BaseInteractable : MonoBehaviour, ISelectable, IHoverable
    {
        public bool Hovered { get; set; }
        public bool Selected { get; set; }

        public abstract void OnHoverEnter();
        public abstract void OnHoverExit();
        public abstract void OnSelectEnter();
        public abstract void OnSelectExit();
    }
}
