namespace TerritoryBoard.Mechanics
{
    public interface IPopulation
    {
        public delegate void OnChanged(IPopulation population);
        public event OnChanged onPopulationChanged;
        public int CurrentPopulation { get; set; }
        public int MinPopulation { get; set; }
        public int MaxPopulation { get; set; }
    }
}
