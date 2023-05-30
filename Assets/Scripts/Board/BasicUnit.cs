using Mechanics;

namespace Board
{
    public class BasicUnit : Unit, IMovableActor
    {
        public int currentXCoordinate => X;
        public int currentYCoordinate => Y;

        private HexagonBoard _board;

        private void Start()
        {
            _board = FindObjectOfType<HexagonBoard>();
        }

        public void MoveToPosition(int x, int y)
        {
            // Validate tile
            var tile = _board.GetTileAtPosition(x, y);
            if(tile == null)
            {
                // TODO: Handle exception
                return;
            }

            // TODO: Animation
            SetTile(tile);
        }
    }
}
