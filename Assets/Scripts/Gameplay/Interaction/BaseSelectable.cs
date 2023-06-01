using System.Collections;
using UnityEngine;

namespace Gameplay.Interaction
{
    [RequireComponent(typeof(Collider))]
    public class BaseSelectable : MonoBehaviour, ISelectable
    {
        private Coroutine _stayCoroutine = null;

        public virtual void OnSelectEnter()
        {
            _stayCoroutine = StartCoroutine(OnSelectStayCoroutine());
        }

        public virtual void OnSelectExit()
        {
            StopCoroutine(_stayCoroutine);
            _stayCoroutine = null;
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
    }
}
