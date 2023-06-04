using System.Collections;
using UnityEngine;
using TerritoryBoard;

namespace Gameplay.Interaction
{
    public class TileInteractable : BaseInteractable
    {
        [SerializeField] private OnHoverConfig hoverConfig;
        [SerializeField] private OnSelectConfig selectConfig;
        [SerializeField] private AnimationCurve hoverCurve;
        [SerializeField] private Transform model;

        // TODO: Use coroutine for flexibility or use animation of performance or job system

        private HexagonTile _tile;
        //private Vector3 _defaultPosition;
        //private float _hoverTime;
        private Material _material;
        //private Coroutine _hoverEnterCoroutine;
        //private Coroutine _hoverExitCoroutine;

        private void Start()
        {
            _tile = GetComponentInParent<HexagonTile>();
            //_defaultPosition = _tile.defaultWorldPosition;
            _material = model.GetComponent<Renderer>().material;
        }

        public override void OnHoverEnter()
        {
            if (Selected) { return; }
            //StopAllCoroutines();
            //_hoverEnterCoroutine = StartCoroutine(OnHoverEnterCoroutine());
            _material.color = hoverConfig.hoveredColor;

        }
        public override void OnHoverExit()
        {
            if (Selected) { return; }
            //StopAllCoroutines();
            //_hoverExitCoroutine = StartCoroutine(OnHoverExitCoroutine());
            _material.color = hoverConfig.defaultColor;
        }

        public override void OnSelectEnter()
        {
            _material.color = selectConfig.selectedColor;
        }
        public override void OnSelectExit()
        {
            _material.color = selectConfig.defaultColor;
        }

        //private IEnumerator OnHoverEnterCoroutine()
        //{
        //    while(_hoverTime < 1)
        //    {
        //        float depth = hoverConfig.depth * hoverCurve.Evaluate(_hoverTime);
        //        model.transform.position = _defaultPosition + new Vector3(0, depth, 0);
        //        _hoverTime += Time.deltaTime / hoverConfig.duration;
        //        yield return null;
        //    }
        //    while (_hoverTime > 0)
        //    {
        //        float depth = hoverConfig.depth * hoverCurve.Evaluate(_hoverTime);
        //        model.transform.position = _defaultPosition + new Vector3(0, depth, 0);
        //        _hoverTime -= Time.deltaTime / hoverConfig.duration;
        //        yield return null;
        //    }
        //    model.transform.position = _defaultPosition;
        //    _hoverTime = 0;
        //}
        //private IEnumerator OnHoverExitCoroutine()
        //{
        //    while(_hoverTime > 0)
        //    {
        //        float depth = hoverConfig.depth * hoverCurve.Evaluate(_hoverTime);
        //        model.transform.position = _defaultPosition + new Vector3(0, depth, 0);
        //        _hoverTime -= Time.deltaTime / hoverConfig.duration;
        //        yield return null;
        //    }
        //    model.transform.position = _defaultPosition;
        //    _hoverTime = 0;
        //}

        [System.Serializable]
        private struct OnHoverConfig
        {
            public Color defaultColor;
            public Color hoveredColor;
        }

        [System.Serializable]
        private struct OnSelectConfig
        {
            public Color defaultColor;
            public Color selectedColor;
        }
    }
}
