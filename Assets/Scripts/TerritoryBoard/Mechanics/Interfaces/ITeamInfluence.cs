using System.Collections.Generic;

namespace TerritoryBoard.Mechanics
{
    public interface ITeamInfluence
    {
        public ITeam Team { get; }
        public List<IInfluenceSender> Senders { get; }
        public float TotalInfluence { get; }
    }
}
