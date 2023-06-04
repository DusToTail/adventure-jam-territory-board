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

        public void ExecuteAction(IMechanicsAction action)
        {
            action.Execute();
        }

        public void SetTile(ITile tile)
        {
            var hexTile = tile as HexagonTile;
            _tile = tile;
            transform.parent = hexTile.AttachTransform;
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            hexTile.Unit = this;
        }

        public virtual void OnCreate(ITile tile)
        {
            SetTile(tile);
            
        }
        public virtual void OnDelete()
        {
            Destroy(gameObject);

            _tile.Unit = null;
        }
    }
}
