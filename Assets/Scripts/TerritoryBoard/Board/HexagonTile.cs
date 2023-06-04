using UnityEngine;
using TerritoryBoard.Mechanics;

namespace TerritoryBoard
{
    public class HexagonTile : Tile
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private MeshCollider meshCollider;

        public Transform AttachTransform { get; private set; }
        public Vector3 DefaultWorldPosition { get; private set; }
        private HexagonBoard _board;
        private int _influenceReceiveRange;

        private void Start()
        {
            _board = FindObjectOfType<HexagonBoard>();

            meshFilter.sharedMesh = _board.CachedMesh;
            meshRenderer.sharedMaterial = _board.CachedMaterial;
            meshCollider.sharedMesh = _board.CachedMesh;

            _influenceReceiveRange = _board.tileConfig.influenceReceiveRange;

            AttachTransform = transform.Find("Attach");
        }

        public void SetDefaultWorldPosition(Vector3 pos)
        {
            DefaultWorldPosition = pos;
        }

        public TeamInfluenceProfile GetTeamInfluenceProfile()
        {
            var influenceProfile = new TeamInfluenceProfile();
            var tiles = _board.GetSurroundingTiles(X, Y, _influenceReceiveRange);
            foreach (var tile in tiles)
            {
                var struc = tile.Structure;
                if (struc != null && struc is IInfluenceSender)
                {
                    influenceProfile.Add(struc.Team, struc);
                }
            }

            return influenceProfile;
        }

        [System.Serializable]
        public struct Config
        {
            public enum Pivot
            {
                Bottom,
                Center,
                Top
            }
            public float size;
            public float height;
            public Pivot pivot;
            public int influenceReceiveRange;
        }
    }
}
