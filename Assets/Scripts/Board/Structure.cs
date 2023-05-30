using UnityEngine;
using Mechanics;

namespace Board
{
    public abstract class Structure : MonoBehaviour, IStructure
    {
        public int X => _tile == null ? -1 : _tile.X;
        public int Y => _tile == null ? -1 : _tile.Y;

        public string Name { get; set; }

        public int CurrentPopulation { get { return _currentPopulation; }
            set 
            { 
                value = value > MaxPopulation ? MaxPopulation : value;
                value = value < MinPopulation ? MinPopulation : value;
                _currentPopulation = value;
                onPopulationChanged?.Invoke(this);
            }
        }
        public int MinPopulation
        {
            get { return _minPopulation; }
            set
            {
                _minPopulation = value;
                onPopulationChanged?.Invoke(this);
            }
        }
        public int MaxPopulation
        {
            get { return _maxPopulation; }
            set
            {
                _maxPopulation = value;
                onPopulationChanged?.Invoke(this);
            }
        }

        public float TotalInfluenceOutput { get { return InfluenceOutput * InfluenceOutputFactor; } }
        public float InfluenceOutput => _influenceOutput;
        public float InfluenceOutputFactor
        {
            get
            {
                return _influenceOutputFactor;
            }
            set
            {
                _influenceOutputFactor = value;
                onInfluenceSenderChanged?.Invoke(this);
            }
        }
        public event IInfluenceSender.OnChanged onInfluenceSenderChanged;
        public event IPopulation.OnChanged onPopulationChanged;

        public ITeam Team { get { return _team; } set { _team = value; onTeamChanged?.Invoke(this, _team); } }
        public event ITeamMember.OnTeamChanged onTeamChanged;

        protected ITile _tile = null;
        protected ITeam _team = null;
        protected int _currentPopulation;
        protected int _minPopulation;
        protected int _maxPopulation;
        protected float _influenceOutput;
        protected float _influenceOutputFactor = 1f;

        public void SetTile(ITile tile)
        {
            _tile = tile;
        }
    }
}
