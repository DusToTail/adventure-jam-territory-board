using TerritoryBoard;
using TerritoryBoard.TurnController;
using UnityEngine;

namespace Gameplay
{
    public class CreateUnitAction : TurnBasedAction
    {
        public Unit Unit { get; private set; }
        private GameObject _prefab;
        private HexagonTile _tile;

        public CreateUnitAction(Unit origin, HexagonTile tile, TurnBasedActor actor) : base(actor)
        {
            _prefab = origin.gameObject;
            _tile = tile;
        }

        public override void ExecuteImpl()
        {
            Unit = GameObject.Instantiate(_prefab, _tile.AttachTransform).GetComponent<IUnit>() as Unit;
            Unit.OnCreate(_tile);
        }
    }
}
