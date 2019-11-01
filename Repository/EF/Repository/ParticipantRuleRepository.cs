using System.Collections.Generic;
using Repository.EF.Base;
using System.Linq;
using Model;
using System;

namespace Repository.EF.Repository
{
    public class ParticipantRuleRepository : EFBaseRepository<ParticipantRule>
    {
        public IEnumerable<ParticipantRule> Select()
        {
            var ParticipantRuleList = from ParticipantRule in Context.ParticipantRules
                           select ParticipantRule;

            return ParticipantRuleList.ToArray();
        }
        public void CreateParticipantRule(ParticipantRule newParticipantRule)
        {
            Add(newParticipantRule);
        }
        public void UpdateParticipantRule(ParticipantRule updateableParticipantRule)
        {
            var oldParticipantRule = (from s in Context.ParticipantRules where s.Id == updateableParticipantRule.Id select s).FirstOrDefault();

            oldParticipantRule.FirstTeamMaxMember= updateableParticipantRule.FirstTeamMaxMember;
            oldParticipantRule.EachExtraTeamMaxMember = updateableParticipantRule.EachExtraTeamMaxMember;
            oldParticipantRule.ExtraParticipantFee = updateableParticipantRule.ExtraParticipantFee;

            Update(oldParticipantRule);
        }
        public bool DeleteParticipantRule(int participantRuleId)
        {
            var oldParticipantRule = (from s in Context.ParticipantRules where s.Id == participantRuleId select s).FirstOrDefault();

            Delete(oldParticipantRule);

            return true;
        }

        public ParticipantRule GetParticipantRule()
        {
            return Context.ParticipantRules.First();

        }
    }
}
