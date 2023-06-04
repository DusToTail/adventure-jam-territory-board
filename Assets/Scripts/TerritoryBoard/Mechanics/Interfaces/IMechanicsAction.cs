namespace TerritoryBoard.Mechanics
{
    public interface IMechanicsAction
    {
        public IMechanicsActor Actor { get; set; }
        public void Execute();
    }
}
