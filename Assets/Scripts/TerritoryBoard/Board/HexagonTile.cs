using UnityEngine;
using TerritoryBoard.Mechanics;

namespace TerritoryBoard
{
    public class HexagonTile : Tile
    {
        public Vector3 defaultWorldPosition { get; private set; }
        private HexagonBoard _board;
        private int _influenceReceiveRange;

        private void Start()
        {
            _board = FindObjectOfType<HexagonBoard>();

            var meshFilter = GetComponent<MeshFilter>();
            meshFilter.sharedMesh = _board.CachedMesh;

            var renderer = GetComponent<MeshRenderer>();
            renderer.sharedMaterial = _board.CachedMaterial;

            _influenceReceiveRange = _board.tileConfig.influenceReceiveRange;
        }

        public void SetDefaultWorldPosition(Vector3 pos)
        {
            defaultWorldPosition = pos;
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
