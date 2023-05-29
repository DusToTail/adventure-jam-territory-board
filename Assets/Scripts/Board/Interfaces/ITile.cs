using Mechanics;

namespace Board
{
    public interface ITile : IGridEntity, IInfluenceReceiver
    {
        public IStructure Structure { get; set; }
        public IUnit Unit { get; set; }
    }
}
