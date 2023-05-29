using System.Collections.Generic;

namespace Mechanics
{
    public class Team : ITeam
    {
        public string Name { get; set; }
        public IReadOnlyCollection<ITeamMember> Members { get => _members; }
        public delegate void OnMemberAdded(ITeamMember member);
        public delegate void OnMemberRemoved(ITeamMember member);
        public event OnMemberAdded onMemberAdded;
        public event OnMemberAdded onMemberRemoved;

        private List<ITeamMember> _members;

        public Team(string name)
        {
            Name = name;
            _members = new List<ITeamMember>();
        }

        public void AddMember(ITeamMember member)
        {
            if(member == null || _members.Contains(member)) { return; }
            _members.Add(member);
            member.Team = this;
            onMemberAdded?.Invoke(member);
        }

        public void RemoveMember(ITeamMember member)
        {
            if (member == null || !_members.Contains(member)) { return; }
            _members.Remove(member);
            member.Team = this;
            onMemberRemoved?.Invoke(member);
        }
    }
}
