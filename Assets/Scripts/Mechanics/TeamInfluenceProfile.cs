using System.Collections.Generic;

namespace Mechanics
{
    public class TeamInfluenceProfile
    {
        public List<TeamInfluence> Influences { get; private set; }

        public TeamInfluenceProfile()
        {
            Influences = new List<TeamInfluence>();
        }

        public void Add(ITeam team, IInfluenceSender[] senders)
        {
            var teamInfluence = Influences.Find(t => t.Team == team);
            if (teamInfluence == null)
            {
                Influences.Add(new TeamInfluence(team, senders));
            }
            else
            {
                teamInfluence.AddSenders(senders);
            }
        }

        public void Add(ITeam team, IInfluenceSender sender)
        {
            var teamInfluence = Influences.Find(t => t.Team == team);
            if (teamInfluence == null)
            {
                Influences.Add(new TeamInfluence(team, new IInfluenceSender[] { sender }));
            }
            else
            {
                teamInfluence.AddSender(sender);
            }
        }

        public void Add(ITeam team)
        {
            if (Influences.Find(t => t.Team == team) == null)
            {
                Influences.Add(new TeamInfluence(team));
            }
        }
    }
}
