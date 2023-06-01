using System.Collections.Generic;

namespace TerritoryBoard.Mechanics
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

        /// <summary>
        /// Use TeamManager class for edit team members
        /// </summary>
        /// <param name="member"></param>
        public void AddMember(ITeamMember member)
        {
            if(member == null || _members.Contains(member)) { return; }
            _members.Add(member);
            member.Team = this;
            onMemberAdded?.Invoke(member);
        }

        /// <summary>
        /// Use TeamManager class for edit team members
        /// </summary>
        /// <param name="member"></param>
        public void RemoveMember(ITeamMember member)
        {
            if (member == null || !_members.Contains(member)) { return; }
            _members.Remove(member);
            member.Team = this;
            onMemberRemoved?.Invoke(member);
        }
    }
}
