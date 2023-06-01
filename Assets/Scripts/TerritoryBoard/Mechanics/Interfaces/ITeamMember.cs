namespace TerritoryBoard.Mechanics
{
    public interface ITeamMember
    {
        public delegate void OnTeamChanged(ITeamMember member, ITeam team);
        public event OnTeamChanged onTeamChanged;
        public ITeam Team { get; set; }
    }
}
