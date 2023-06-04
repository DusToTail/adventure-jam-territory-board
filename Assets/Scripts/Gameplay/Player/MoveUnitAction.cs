using TerritoryBoard;
using TerritoryBoard.TurnBasedSystem;
using TerritoryBoard.Mechanics;

namespace Gameplay
{
    public class MoveUnitAction : TurnBasedAction
    {
        private BasicUnit _unit;
        private HexagonTile _tile;

        public MoveUnitAction(BasicUnit unit, HexagonTile to, TurnBasedActor actor) : base(actor)
        {
            _unit = unit;
            _tile = to;
        }

        public override void ExecuteImpl()
        {
            _unit.ExecuteAction(new MoveAction(_tile.X, _tile.Y, _unit));
        }
    }
}
