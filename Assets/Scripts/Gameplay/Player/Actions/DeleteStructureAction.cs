using TerritoryBoard;
using TerritoryBoard.TurnController;

namespace Gameplay
{
    public class DeleteStructureAction : TurnBasedAction
    {
        private HexagonTile _tile;

        public DeleteStructureAction(HexagonTile tile, TurnBasedActor actor) : base(actor)
        {
            _tile = tile;
        }

        public override void ExecuteImpl()
        {
            var structure = _tile.Structure as Structure;
            structure.OnDelete();
        }
    }
}
