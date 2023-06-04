using System.Collections.Generic;

namespace TerritoryBoard.Mechanics
{
    public class InfluenceLevelController
    {
        public Stack<InfluenceLevel> Levels { get; private set; }

        public delegate void OnLevelChanged(InfluenceLevel level);
        public event OnLevelChanged onLevelChanged;

        public InfluenceLevel Current { get { return _current; }
            set
            {
                _current = value;
                onLevelChanged?.Invoke(value);
            }
        }
        private InfluenceLevel _current;
        private IPopulation _sender;

        public InfluenceLevelController(Config config, IPopulation sender)
        {
            Levels = new Stack<InfluenceLevel>();
            foreach(var level in config.levels)
            {
                Levels.Push(level);
            }

            sender.onPopulationChanged += Reevaluate;
            _sender = sender;
        }

        public void Push(string name, int populationThreshold, int perPopulation, float influence)
        {
            Levels.Push(new InfluenceLevel(name, populationThreshold, perPopulation, influence));
        }

        public InfluenceLevel Pop()
        {
            return Levels.Pop();
        }

        private void Reevaluate(IPopulation population)
        {
            float total = population.CurrentPopulation;
            var levels = Levels.ToArray();
            if(levels.Length == 0)
            {
                Current = default(InfluenceLevel);
            }

            for (int i = levels.Length - 1; i >= 0; i--)
            {
                var level = levels[i];
                if(total >= level.populationThreshold)
                {
                    Current = level;
                    break;
                }
            }
        }
        ~InfluenceLevelController()
        {
            _sender.onPopulationChanged -= Reevaluate;
        }

        [System.Serializable]
        public struct Config
        {
            public InfluenceLevel[] levels;
        }
    }
}
