using System.Collections.Generic;

namespace TerritoryBoard.Mechanics
{
    public interface ITeam
    {
        public string Name { get; set; }
        public IReadOnlyCollection<ITeamMember> Members { get;}
        public void AddMember(ITeamMember member);
        public void RemoveMember(ITeamMember member);
    }
}
