using TerritoryBoard;
using TerritoryBoard.TurnController;
using UnityEngine;

namespace Gameplay
{
    public class CreateStructureAction : TurnBasedAction
    {
        public Structure Structure { get; private set; }
        private GameObject _prefab;
        private HexagonTile _tile;

        public CreateStructureAction(Structure origin, HexagonTile tile, TurnBasedActor actor) : base(actor)
        {
            _prefab = origin.gameObject;
            _tile = tile;
        }

        public override void ExecuteImpl()
        {
            Structure = GameObject.Instantiate(_prefab, _tile.AttachTransform).GetComponent<IStructure>() as Structure;
            Structure.OnCreate(_tile);
        }
    }
}
