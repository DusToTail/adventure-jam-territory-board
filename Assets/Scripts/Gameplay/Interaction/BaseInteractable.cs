using System.Collections;
using UnityEngine;

namespace Gameplay.Interaction
{
    [RequireComponent(typeof(Collider))]
    public class BaseInteractable : MonoBehaviour, ISelectable, IHoverable
    {
        public bool isHovered { get; private set; }
        public bool isSelected { get; private set; }

        private Coroutine _staySelectCoroutine = null;
        private Coroutine _stayHoverCoroutine = null;

        public virtual void OnHoverEnter()
        {
            isHovered = true;
            _stayHoverCoroutine = StartCoroutine(OnHoverStayCoroutine());
        }
        public virtual void OnHoverExit()
        {
            if(_stayHoverCoroutine != null)
            {
                StopCoroutine(_stayHoverCoroutine);
                _stayHoverCoroutine = null;
            }
            isHovered = false;
        }
        public virtual void OnHoverStay()
        {
        }

        public virtual void OnSelectEnter()
        {
            isSelected = true;
            _staySelectCoroutine = StartCoroutine(OnSelectStayCoroutine());
        }
        public virtual void OnSelectExit()
        {
            if(_staySelectCoroutine != null)
            {
                StopCoroutine(_staySelectCoroutine);
                _staySelectCoroutine = null;
            }
            isSelected = false;
        }
        public virtual void OnSelectStay()
        {
        }

        private IEnumerator OnSelectStayCoroutine()
        {
            while(true)
            {
                OnSelectStay();
                yield return null;
            }
        }
        private IEnumerator OnHoverStayCoroutine()
        {
            while (true)
            {
                OnHoverStay();
                yield return null;
            }
        }
    }
}
