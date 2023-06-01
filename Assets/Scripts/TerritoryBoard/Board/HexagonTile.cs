using UnityEngine;
using TerritoryBoard.Mechanics;

namespace TerritoryBoard
{
    public class HexagonTile : Tile
    {
        [SerializeField] private Config config;
        private HexagonBoard _board;
        private Vector3 _defaultWorldPosition;

        private void Start()
        {
            _board = FindObjectOfType<HexagonBoard>();
        }

        public void SetDefaultWorldPosition(Vector3 pos)
        {
            _defaultWorldPosition = pos;
        }

        public TeamInfluenceProfile GetTeamInfluenceProfile()
        {
            if( _board == null )
            {
                return null;
            }

            var influenceProfile = new TeamInfluenceProfile();
            var tiles = _board.GetSurroundingTiles(X, Y, config.influenceReceiveRange);
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
            public int influenceReceiveRange;
        }
    }
}
