using TerritoryBoard;
using TerritoryBoard.TurnController;

namespace Gameplay
{
    public class DeleteUnitAction : TurnBasedAction
    {
        private HexagonTile _tile;

        public DeleteUnitAction(HexagonTile tile, TurnBasedActor actor) : base(actor)
        {
            _tile = tile;
        }

        public override void ExecuteImpl()
        {
            var unit = _tile.Unit as Unit;
            unit.OnDelete();
        }
    }
}
