using UnityEngine;
using Grid2D;

namespace Board
{
    public abstract class Tile : MonoBehaviour, ITile
    {
        public IStructure Structure { get { return _structure; } set { _structure = value; onStructureChanged?.Invoke(value); } }
        public IUnit Unit { get { return _unit; } set { _unit = value; onUnitChanged?.Invoke(value); } }

        public delegate void OnStructureChanged(IStructure structure);
        public delegate void OnUnitChanged(IUnit unit);
        public event OnStructureChanged onStructureChanged;
        public event OnUnitChanged onUnitChanged;

        public int X => _cell == null ? -1 : _cell.X;
        public int Y => _cell == null ? -1 : _cell.Y;

        public string Name { get; set; }

        public float Total { get { return Amount * Factor; } }
        public float Amount => GetInfluencedAmount();
        public float Factor { get; set; } = 1f;

        private Cell _cell;
        private IStructure _structure = null;
        private IUnit _unit = null;

        public void SetCell(Cell cell)
        {
            _cell = cell;
            Name = $"({X},{Y})";
        }

        public abstract float GetInfluencedAmount();
    }
}
