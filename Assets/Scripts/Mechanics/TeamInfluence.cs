using System.Collections.Generic;
using System.Linq;

namespace Mechanics
{
    public class TeamInfluence : ITeamInfluence
    {
        public ITeam Team { get { return _team; } }
        public List<IInfluenceSender> Senders { get { return _senders; } }
        public float TotalInfluence 
        { 
            get 
            { 
                float result=0;
                foreach(var s in Senders)
                {
                    result += s.TotalInfluenceOutput;
                }
                return result;
            } 
        }

        private ITeam _team;
        private List<IInfluenceSender> _senders;

        public TeamInfluence(ITeam team)
        {
            _team = team;
            _senders = new List<IInfluenceSender>();
        }
        public TeamInfluence(ITeam team, IInfluenceSender[] senders)
        {
            _team = team;
            _senders = new List<IInfluenceSender>();
            if(senders != null)
            {
                _senders.AddRange(senders.Where(x=>!_senders.Contains(x)));
            }
        }
        public void AddSender(IInfluenceSender sender)
        {
            if(sender == null || _senders.Contains(sender)) { return; }
            _senders.Add(sender);
        }
        public void AddSenders(IInfluenceSender[] senders)
        {
            if (senders != null)
            {
                _senders.AddRange(senders.Where(x => !_senders.Contains(x)));
            }
        }
    }
}
