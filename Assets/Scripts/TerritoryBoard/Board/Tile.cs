using UnityEngine;
using TerritoryBoard.Grid2D;
using TerritoryBoard.Mechanics;

namespace TerritoryBoard
{
    public abstract class Tile : MonoBehaviour, ITile
    {
        public IStructure Structure 
        {
            get 
            { 
                return _structure; 
            } 
            set 
            {
                if(value == null)
                {
                    if(_structure is IOnTileExit exit)
                    {
                        exit.OnTileExit(this);
                    }
                    _structure = value;
                }
                else
                {
                    _structure = value;
                    if (_structure is IOnTileEnter enter)
                    {
                        enter.OnTileEnter(this);
                    }
                }
                onStructureChanged?.Invoke(value); 
            }
        }
        public IUnit Unit 
        { 
            get 
            { 
                return _unit; 
            } 
            set 
            {
                if (value == null)
                {
                    if (_unit is IOnTileExit exit)
                    {
                        exit.OnTileExit(this);
                    }
                    _unit = value;
                }
                else
                {
                    _unit = value;
                    if (_unit is IOnTileEnter enter)
                    {
                        enter.OnTileEnter(this);
                    }
                }
                onUnitChanged?.Invoke(value); 
            } 
        }

        public delegate void OnStructureChanged(IStructure structure);
        public delegate void OnUnitChanged(IUnit unit);
        public event OnStructureChanged onStructureChanged;
        public event OnUnitChanged onUnitChanged;

        public int X => _cell == null ? -1 : _cell.X;
        public int Y => _cell == null ? -1 : _cell.Y;

        public string Name { get; set; }

        public float TotalInfluenceInput { get { return InfluenceInput * InfluenceInputFactor; } }
        public float InfluenceInput => _influenceInput;
        public float InfluenceInputFactor
        {
            get
            {
                return _influenceInputFactor;
            }
            set
            {
                _influenceInputFactor = value;
            }
        }

        public ITeam Team { get { return _team; } set { _team = value; onTeamChanged?.Invoke(this, _team); } }
        public event ITeamMember.OnTeamChanged onTeamChanged;

        private Cell _cell;
        private IStructure _structure = null;
        private IUnit _unit = null;
        private ITeam _team = null;
        protected float _influenceInput;
        protected float _influenceInputFactor = 1f;

        public void SetCell(Cell cell)
        {
            _cell = cell;
            Name = $"({X},{Y})";
        }
    }
}
