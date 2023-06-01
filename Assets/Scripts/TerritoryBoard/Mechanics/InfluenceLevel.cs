namespace TerritoryBoard.Mechanics
{
    public struct InfluenceLevel
    {
        public string name;
        public int populationThreshold;
        public int perPopulation;
        public float perPopulationInfluence;

        public InfluenceLevel(string name, int populationThreshold, int perPopulation, float influence)
        {
            this.name = name;
            this.populationThreshold = populationThreshold;
            this.perPopulation = perPopulation;
            this.perPopulationInfluence = influence;
        }
    }
}
