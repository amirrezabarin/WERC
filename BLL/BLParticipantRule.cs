using BLL.Base;
using Model;
using Model.ViewModels.Admin;
using Model.ViewModels.ParticipantRule;
using Repository.EF.Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using static Model.ApplicationDomainModels.ConstantObjects;
using Model.ViewModels.Judge;

namespace BLL
{
    public class BLParticipantRule : BLBase
    {
        public VmParticipantRule GetParticipantRule()
        {
            try
            {
                var participantRuleRepository = UnitOfWork.GetRepository<ParticipantRuleRepository>();

                var participantRule = participantRuleRepository.GetParticipantRule();

                var vwParticipantRule = new VmParticipantRule
                {
                    Id = participantRule.Id,
                    FirstTeamMaxMember = participantRule.FirstTeamMaxMember,
                    EachExtraTeamMaxMember = participantRule.EachExtraTeamMaxMember,
                    ExtraParticipantFee = participantRule.ExtraParticipantFee,
                    UIExtraParticipantFee = participantRule.ExtraParticipantFee.ToString(),
                };

                return vwParticipantRule;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool UpdateParticipantRule(VmParticipantRule vmParticipantRule)
        {
            var participantRuleRepository = UnitOfWork.GetRepository<ParticipantRuleRepository>();

            var participantRule = new ParticipantRule
            {
                Id = vmParticipantRule.Id,
                FirstTeamMaxMember = vmParticipantRule.FirstTeamMaxMember,
                EachExtraTeamMaxMember = vmParticipantRule.EachExtraTeamMaxMember,
                ExtraParticipantFee = vmParticipantRule.ExtraParticipantFee,
            };

            participantRuleRepository.UpdateParticipantRule(participantRule);

            return UnitOfWork.Commit();
        }

    }
}