using System.Collections.Generic;

namespace TerritoryBoard.Mechanics
{
    public class TeamManager
    {
        public static TeamManager Instance { get
            {
                if (_instance == null)
                {
                    _instance = new TeamManager();
                    return _instance;
                }
                else
                {
                    return _instance;
                }
            }
        }
        public List<ITeam> Teams { get; private set; }

        private static TeamManager _instance;

        private TeamManager()
        {
            Teams = new List<ITeam>();
        }

        public void CreateTeam(string name)
        {
            Teams.Add(new Team(name));
        }
        public ITeam GetTeam(string name)
        {
            return Teams.Find(x => x.Name == name);
        }
        public void JoinTeam(ITeamMember member, ITeam team)
        {
            team.AddMember(member);
        }
        public void ExitTeam(ITeamMember member, ITeam team)
        {
            team.RemoveMember(member);
        }
        public void JoinTeam(ITeamMember member, string name)
        {
            var team = GetTeam(name);
            if(team == null) { return; }
            team.AddMember(member);
        }
        public void ExitTeam(ITeamMember member, string name)
        {
            var team = GetTeam(name);
            if (team == null) { return; }
            team.RemoveMember(member);
        }
    }
}
