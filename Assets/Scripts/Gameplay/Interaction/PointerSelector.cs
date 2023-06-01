using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Gameplay.Interaction
{
    public class PointerSelector : BaseSelector
    {
        [SerializeField] private InputAction selectAction;
        [SerializeField] private Camera raycastCamera;
        [SerializeField] private float raycastMaxDistance;
        [SerializeField] private LayerMask raycastLayerMask;
        [SerializeField] private QueryTriggerInteraction raycastQueryTriggerInteraction;
        [SerializeField] private UnityAction<Vector2> onNoHit;
        [SerializeField] private UnityAction<Vector2, ISelectable> onHit;

        private Pointer _pointer;
        private RaycastHit[] _hits = new RaycastHit[1];

        private void Awake()
        {
            if(raycastCamera == null)
            {
                raycastCamera = Camera.main;
            }
            _pointer = Pointer.current;
            selectAction.performed += ctx => { OnSelect(ctx); };
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            Vector2 pos = _pointer.position.ReadValue();
            var ray = raycastCamera.ScreenPointToRay(pos);
            int hitCount = Physics.RaycastNonAlloc(ray, _hits, raycastMaxDistance, raycastLayerMask, raycastQueryTriggerInteraction);
            if(hitCount < 1)
            {
                onNoHit?.Invoke(pos);
                return;
            }
            var col = _hits[0].collider;
            var selectable = col.GetComponent<ISelectable>();
            if (selectable != null)
            {
                Select(selectable);
                onHit?.Invoke(pos, selectable);
            }
        }

        private void OnEnable()
        {
            selectAction.Enable();
        }

        private void OnDisable()
        {
            selectAction.Disable();
        }
    }
}
