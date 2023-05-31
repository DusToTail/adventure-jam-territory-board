using UnityEngine;

namespace Interaction
{
    public class BaseSelector : MonoBehaviour, ISelector
    {
        public ISelectable CurrentSelectable => _cur;
        public ISelectable PreviousSelectable => _prev;

        protected ISelectable _cur = null;
        protected ISelectable _prev = null;

        public void Select(ISelectable currentSelectable)
        {
            _prev?.OnSelectExit();
            _cur = currentSelectable;
            _cur.OnSelectExit();
        }
    }
}
