using UnityEngine;
using Mechanics;

namespace Board
{
    public class BasicStructure : Structure
    {
#if UNITY_EDITOR
        [SerializeField] private bool initializeOnStart;
        [SerializeField] private InfluenceLevelController.Config config;

        private void Start()
        {
            if (initializeOnStart)
                InitializeInluenceController(config);
        }
#endif
        private InfluenceLevelController _influenceController;

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
