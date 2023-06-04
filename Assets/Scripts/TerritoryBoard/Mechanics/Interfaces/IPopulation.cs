namespace TerritoryBoard.Mechanics
{
    public interface IPopulation
    {
        public delegate void OnPopulationChanged(IPopulation population);
        public event OnPopulationChanged onPopulationChanged;
        public int CurrentPopulation { get; set; }
        public int MinPopulation { get; set; }
        public int MaxPopulation { get; set; }
    }
}
