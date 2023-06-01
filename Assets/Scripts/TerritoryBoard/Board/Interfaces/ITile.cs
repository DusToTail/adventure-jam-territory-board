using TerritoryBoard.Mechanics;

namespace TerritoryBoard
{
    public interface ITile : IGridEntity, IInfluenceReceiver, ITeamMember
    {
        public IStructure Structure { get; set; }
        public IUnit Unit { get; set; }
    }
}
