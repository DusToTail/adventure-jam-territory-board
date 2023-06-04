using UnityEngine;
using TerritoryBoard.Mechanics;

namespace TerritoryBoard
{
    public class BasicStructure : Structure
    {
        [SerializeField] private bool initializeOnStart;
        [SerializeField] private InfluenceLevelController.Config config;
        private InfluenceLevelController _influenceController;

        private void Start()
        {
            if (initializeOnStart)
                InitializeInluenceController(config);
        }

        public void InitializeInluenceController(InfluenceLevelController.Config config)
        {
            _influenceController = new InfluenceLevelController(config, this);
            _influenceController.onLevelChanged += ChangeInfluenceOutput;
        }

        public void ChangeInfluenceOutput(InfluenceLevel level)
        {
            _influenceOutput = (int)(CurrentPopulation / level.perPopulation) * level.perPopulationInfluence;
        }

        private void OnDestroy()
        {
            _influenceController.onLevelChanged -= ChangeInfluenceOutput;
        }

    }
}
