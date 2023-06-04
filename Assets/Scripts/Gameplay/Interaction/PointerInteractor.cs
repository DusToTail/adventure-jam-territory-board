using UnityEngine;
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
            var ray = raycastCamera.ScreenPointToRay(pos);
            int hitCount = Physics.RaycastNonAlloc(ray, _hits, raycastConfig.maxDistance, raycastConfig.layerMask, raycastConfig.queryTriggerInteraction);
            if(hitCount < 1)
            {
                Select(null);
#if UNITY_EDITOR
                Debug.DrawRay(ray.origin, ray.direction * raycastConfig.maxDistance, Color.red, 2f, true);
#endif
                return;
            }
            var col = _hits[0].collider;
            var selectable = col.GetComponent<ISelectable>();
            if (selectable != null)
            {
                Select(selectable);
#if UNITY_EDITOR
                Debug.DrawLine(ray.origin, ray.direction * raycastConfig.maxDistance, Color.green, 2f, true);
#endif
            }
        }

        public void OnHover(InputAction.CallbackContext context)
        {
            Vector2 pos = context.ReadValue<Vector2>();
            var ray = raycastCamera.ScreenPointToRay(pos);
            int hitCount = Physics.RaycastNonAlloc(ray, _hits, raycastConfig.maxDistance, raycastConfig.layerMask, raycastConfig.queryTriggerInteraction);
            if (hitCount < 1)
            {
                Hover(null);
#if UNITY_EDITOR
                Debug.DrawRay(ray.origin, ray.direction * raycastConfig.maxDistance, Color.red, 0.01f, true);
#endif
                return;
            }
            var col = _hits[0].collider;
            var hoverable = col.GetComponent<IHoverable>();
            if (hoverable != null && hoverable != CurrentHoverable)
            {
                Hover(hoverable);
#if UNITY_EDITOR
                Debug.DrawRay(ray.origin, ray.direction * raycastConfig.maxDistance, Color.green, 0.01f, true);
#endif
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
