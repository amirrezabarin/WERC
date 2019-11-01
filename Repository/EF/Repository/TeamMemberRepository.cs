using Repository.EF.Base;
using System.Linq;
using Model;
using System;

namespace Repository.EF.Repository
{
    public class TeamMemberRepository : EFBaseRepository<TeamMember>
    {

        public void CreateTeamMember(TeamMember newTeamMember)
        {
            Add(newTeamMember);
        }
        public void UpdateTeamMember(TeamMember updateableTeamMember)
        {
            var oldTeamMember = (from s in Context.TeamMembers where s.Id == updateableTeamMember.Id select s).FirstOrDefault();

            oldTeamMember.TeamId = updateableTeamMember.TeamId;
            oldTeamMember.MemberUserId = updateableTeamMember.MemberUserId;
            oldTeamMember.RegistrationStatus = updateableTeamMember.RegistrationStatus;
            oldTeamMember.Survey = updateableTeamMember.Survey;

            Update(oldTeamMember);
        }
        public void UpdateTeamMemberRegistrationStatusByUserId(TeamMember updateableTeamMember)
        {
            var oldTeamMembers = (from s in Context.TeamMembers where s.MemberUserId == updateableTeamMember.MemberUserId select s);

            if (oldTeamMembers != null)
            {
                foreach (var item in oldTeamMembers)
                {
                    item.RegistrationStatus = updateableTeamMember.RegistrationStatus;

                    Update(item);
                }

            }
        }
        public void UpdateTeamMemberSurveyByUserId(string memberUserId, bool survey)
        {
            var oldTeamMember = (from s in Context.TeamMembers where s.MemberUserId == memberUserId select s).FirstOrDefault();

            oldTeamMember.Survey = survey;

            Update(oldTeamMember);
        }
        public void DeleteTeamMember(int teamMemberId)
        {
            var oldTeamMember = (from s in Context.TeamMembers where s.Id == teamMemberId select s).FirstOrDefault();
            Delete(oldTeamMember);
        }


    }
}
