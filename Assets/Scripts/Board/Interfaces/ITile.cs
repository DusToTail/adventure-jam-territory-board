using Mechanics;

namespace Board
{
    public interface ITile : IGridEntity, IInfluenceReceiver, ITeamMember
    {
        public IStructure Structure { get; set; }
        public IUnit Unit { get; set; }
    }
}
