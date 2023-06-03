using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Gameplay.Interaction
{
    public class PointerInteractor : BaseInteractor
    {
        [SerializeField] private InputAction selectAction;
        [SerializeField] private InputAction hoverAction;
        [SerializeField] private Camera raycastCamera;
        [SerializeField] private RaycastConfig raycastConfig;

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
            hoverAction.performed += ctx => { OnHover(ctx); };
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            Vector2 pos = _pointer.position.ReadValue();
            Debug.Log($"OnSelect at {pos}");
            var ray = raycastCamera.ScreenPointToRay(pos);
            int hitCount = Physics.RaycastNonAlloc(ray, _hits, raycastConfig.maxDistance, raycastConfig.layerMask, raycastConfig.queryTriggerInteraction);
            if(hitCount < 1)
            {
                Select(null);
                return;
            }
            var col = _hits[0].collider;
            var selectable = col.GetComponent<ISelectable>();
            if (selectable != null)
            {
                Select(selectable);
            }
        }

        public void OnHover(InputAction.CallbackContext context)
        {
            Vector2 pos = context.ReadValue<Vector2>();
            Debug.Log($"OnHover at {pos}");
            var ray = raycastCamera.ScreenPointToRay(pos);
            int hitCount = Physics.RaycastNonAlloc(ray, _hits, raycastConfig.maxDistance, raycastConfig.layerMask, raycastConfig.queryTriggerInteraction);
            if (hitCount < 1)
            {
                Hover(null);
                return;
            }
            var col = _hits[0].collider;
            var hoverable = col.GetComponent<IHoverable>();
            if (hoverable != null)
            {
                Hover(hoverable);
            }
        }

        private void OnEnable()
        {
            selectAction.Enable();
            hoverAction.Enable();
        }

        private void OnDisable()
        {
            selectAction.Disable();
            hoverAction.Disable();
        }

        [System.Serializable]
        public struct RaycastConfig
        {
            public float maxDistance;
            public LayerMask layerMask;
            public QueryTriggerInteraction queryTriggerInteraction;
        }
    }
}
