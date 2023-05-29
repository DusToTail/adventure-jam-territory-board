using UnityEngine;

namespace Board
{
    public class HexagonTile : Tile
    {
        private HexagonBoard _board;
        private Vector3 _defaultWorldPosition;

        public void SetDefaultWorldPosition(Vector3 pos)
        {
            _defaultWorldPosition = pos;
        }
        public void SetBoard(HexagonBoard board)
        {
            _board = board;
        }

        public override float GetInfluencedAmount()
        {
            if( _board == null )
            {
                return 0f;
            }

            float result = 0f;
            // TODO: depending on the range away from the tile

            return result;
        }
    }
}
