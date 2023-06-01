using UnityEngine;
using TerritoryBoard.Mechanics;

namespace TerritoryBoard
{
    public abstract class Unit : MonoBehaviour, IUnit
    {
        public int X => _tile == null ? -1 : _tile.X;
        public int Y => _tile == null ? -1 : _tile.Y;

        public string Name { get; set; }

        private ITile _tile = null;

        public void SetTile(ITile tile)
        {
            _tile = tile;
        }

        public void ExecuteAction(IAction action)
        {
            action.EnterAction();
            action.UpdateAction();
            action.ExitAction();
        }
    }
}
