using BLL.Base;
using Model;
using Model.ViewModels.TeamSafetyItem;
using Repository.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BLTeamSafetyItem : BLBase
    {
        public IEnumerable<VmTeamSafetyItem> GetTeamSafetyItemByTeamId(int teamId)
        {
            try
            {
                var safetyItemRepository = UnitOfWork.GetRepository<SafetyItemRepository>();

                var safetyItemList = safetyItemRepository.GetAllSafetyItems();

                var vmTeamSafetyItemList = (from tsi in safetyItemList
                                            select new VmTeamSafetyItem
                                            {
                                                TeamId = teamId,
                                                SafetyItemId = tsi.Id,
                                                LastContent = "",
                                                LastComment = "",
                                                ItemStatus = null,
                                                AttachedFileUrl = "",
                                                SafetyItemName = tsi.Name,
                                                Priority = tsi.Priority,
                                                Instruction = tsi.Instruction,
                                                AttachmentRequired = tsi.AttachmentRequired,
                                                TextRequired = tsi.TextRequired,
                                            }).ToList();

                var viewTeamSafetyItemRepository = UnitOfWork.GetRepository<ViewTeamSafetyItemRepository>();

                var teamSafetyItemAnswerList = viewTeamSafetyItemRepository.GetViewTeamSafetyItemByTeamId(teamId);

                if (teamSafetyItemAnswerList.Count() > 0)
                {
                    foreach (var item in vmTeamSafetyItemList)
                    {

                        var answer = teamSafetyItemAnswerList.Where(s => s.SafetyItemId == item.SafetyItemId).FirstOrDefault();

                        if (answer != null)
                        {
                            item.Id = answer.Id;
                            item.TeamId = answer.TeamId;
                            item.LastContent = answer.LastContent ?? "";
                            item.LastComment = answer.LastComment ?? "";
                            item.ItemStatus = answer.ItemStatus;
                            item.AttachedFileUrl = answer.AttachedFileUrl;
                        }
                    }
                }

                return vmTeamSafetyItemList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<VmTeamSafetyItem> GetSafetyAdminTeamSafetyItemByTeamId(int teamId)
        {
            try
            {

                var viewTeamSafetyItemRepository = UnitOfWork.GetRepository<ViewTeamSafetyItemRepository>();

                var viewTeamSafetyItemList = viewTeamSafetyItemRepository.GetViewTeamSafetyItemByTeamId(teamId);

                var vmTeamSafetyItemList = from t in viewTeamSafetyItemList
                                           select new VmTeamSafetyItem
                                           {
                                               Id = t.Id,
                                               TeamId = t.TeamId,
                                               LastContent = t.LastContent ?? "",
                                               LastComment = t.LastComment ?? "",
                                               ItemStatus = t.ItemStatus,
                                               AttachedFileUrl = t.AttachedFileUrl,
                                               Instruction = t.Instruction,
                                               Priority = t.Priority,
                                               SafetyItemId = t.SafetyItemId,
                                               SafetyItemName = t.SafetyItemName,
                                               AttachmentRequired = t.AttachmentRequired,
                                               TextRequired = t.TextRequired,
                                           };

                return vmTeamSafetyItemList.OrderBy(s => s.Priority);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateTeamSafetyItem(VmTeamSafetyItem vmTeamSafetyItem)
        {
            try
            {

                var teamSafetyItemRepository = UnitOfWork.GetRepository<TeamSafetyItemRepository>();

                var oldTeamSafety = teamSafetyItemRepository.GetTeamSafetyItem(vmTeamSafetyItem.TeamId, vmTeamSafetyItem.SafetyItemId);
                if (oldTeamSafety != null)
                {
                    teamSafetyItemRepository.UpdateTeamSafetyItem(
                       new TeamSafetyItem
                       {
                           Id = oldTeamSafety.Id,
                           TeamId = vmTeamSafetyItem.TeamId,
                           SafetyItemId = oldTeamSafety.SafetyItemId,
                           LastContent = vmTeamSafetyItem.LastContent ?? "",
                           LastComment = vmTeamSafetyItem.LastComment ?? "",
                           ItemStatus = vmTeamSafetyItem.ItemStatus,
                           AttachedFileUrl = vmTeamSafetyItem.AttachedFileUrl,

                       });

                    var teamSafetyItemLogRepository = UnitOfWork.GetRepository<TeamSafetyItemLogRepository>();
                    teamSafetyItemLogRepository.CreateTeamSafetyItemLog(
                                new TeamSafetyItemLog
                                {
                                    TeamSafetyItemId = oldTeamSafety.Id,
                                    AttachedFileUrl = vmTeamSafetyItem.AttachedFileUrl,
                                    Content = (vmTeamSafetyItem.Type == false) ? vmTeamSafetyItem.LastContent ?? "" : vmTeamSafetyItem.LastComment ?? "",
                                    DateTime = DateTime.Now,
                                    Type = vmTeamSafetyItem.Type,
                                    UserId = vmTeamSafetyItem.UserId,
                                });
                }
                else
                {
                    teamSafetyItemRepository.CreateTeamSafetyItem(
                        new TeamSafetyItem
                        {
                            TeamId = vmTeamSafetyItem.TeamId,
                            SafetyItemId = vmTeamSafetyItem.SafetyItemId,
                            LastContent = vmTeamSafetyItem.LastContent ?? "",
                            LastComment = vmTeamSafetyItem.LastComment ?? "",
                            ItemStatus = vmTeamSafetyItem.ItemStatus,
                            AttachedFileUrl = vmTeamSafetyItem.AttachedFileUrl,

                            TeamSafetyItemLogs = new List<TeamSafetyItemLog>()
                            {
                                new TeamSafetyItemLog{
                                     AttachedFileUrl = vmTeamSafetyItem.AttachedFileUrl,
                                     Content = (vmTeamSafetyItem.Type == false)? vmTeamSafetyItem.LastContent ?? "" : vmTeamSafetyItem.LastComment ?? "",
                                     DateTime = DateTime.Now,
                                     Type = vmTeamSafetyItem.Type,
                                     UserId = vmTeamSafetyItem.UserId,
                                }
                            }
                        });
                }
                UnitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckSavedTeamSafety(int teamId)
        {
            var teamSafetyItemRepository = UnitOfWork.GetRepository<TeamSafetyItemRepository>();

            var teamSafetyItemList = teamSafetyItemRepository.GetTeamSafetyItem(teamId);

            var safetyItemRepository = UnitOfWork.GetRepository<SafetyItemRepository>();

            var safetyItemCount = safetyItemRepository.GetSafetyItemsCount();

            if (safetyItemCount != teamSafetyItemList.Count())
            {
                return false;
            }
            foreach (var item in teamSafetyItemList)
            {
                if (item.ItemStatus == null || item.ItemStatus == 2)
                {
                    return false;
                }
            }


            return true;
        }
        public IEnumerable<VmTeamSafetyItem> GetApproveAllTeamSafetyIteam(int teamId)
        {
            var teamSafetyItemRepository = UnitOfWork.GetRepository<TeamSafetyItemRepository>();

            var teamSafetyItemList = teamSafetyItemRepository.GetTeamSafetyItem(teamId);

            var vmTeamSafetyItemList = from t in teamSafetyItemList
                                       select new VmTeamSafetyItem
                                       {
                                           Id = t.Id,
                                           TeamId = t.TeamId,
                                           LastContent = t.LastContent ?? "",
                                           LastComment = t.LastComment ?? "",
                                           ItemStatus = t.ItemStatus,
                                           AttachedFileUrl = t.AttachedFileUrl,
                                           SafetyItemId = t.SafetyItemId,
                                       };


            return vmTeamSafetyItemList;
        }

        public bool UpdateTeamSafetyItemStatus(int teamId, int temStatus)
        {
            var teamSafetyItemRepository = UnitOfWork.GetRepository<TeamSafetyItemRepository>();
            teamSafetyItemRepository.UpdateTeamSafetyItem(teamId, temStatus);

            var teamRepository = UnitOfWork.GetRepository<TeamRepository>();
            teamRepository.UpdateTeamSubmitStatus(teamId, true);

            return UnitOfWork.Commit();
        }
        public bool UpdateSubmitTeamSafetyItemStatus(int teamId, int temStatus)
        {
            var teamSafetyItemRepository = UnitOfWork.GetRepository<TeamSafetyItemRepository>();
            teamSafetyItemRepository.UpdateSubmitTeamSafetyItemStatus(teamId, temStatus);

            var teamRepository = UnitOfWork.GetRepository<TeamRepository>();
            teamRepository.UpdateTeamSubmitStatus(teamId, true);

            return UnitOfWork.Commit();
        }

        public bool UpdateTeamSafetyItemStatusAndComment(VmTeamSafetyItem vmTeamSafetyItem)
        {
            try
            {
                var teamSafetyItemRepository = UnitOfWork.GetRepository<TeamSafetyItemRepository>();

                var oldTeamSafety = teamSafetyItemRepository.GetTeamSafetyItem(vmTeamSafetyItem.TeamId, vmTeamSafetyItem.SafetyItemId);
                if (oldTeamSafety != null)
                {
                    teamSafetyItemRepository.UpdateTeamSafetyItem(
                       new TeamSafetyItem
                       {
                           Id = oldTeamSafety.Id,
                           TeamId = vmTeamSafetyItem.TeamId,
                           SafetyItemId = oldTeamSafety.SafetyItemId,
                           LastContent = vmTeamSafetyItem.LastContent ?? "",
                           LastComment = vmTeamSafetyItem.LastComment ?? "",
                           ItemStatus = vmTeamSafetyItem.ItemStatus,
                           AttachedFileUrl = vmTeamSafetyItem.AttachedFileUrl,

                       });

                    var teamSafetyItemLogRepository = UnitOfWork.GetRepository<TeamSafetyItemLogRepository>();
                    teamSafetyItemLogRepository.CreateTeamSafetyItemLog(
                                new TeamSafetyItemLog
                                {
                                    TeamSafetyItemId = oldTeamSafety.Id,
                                    AttachedFileUrl = vmTeamSafetyItem.AttachedFileUrl,
                                    Content = (vmTeamSafetyItem.Type == false) ? vmTeamSafetyItem.LastContent ?? "" : vmTeamSafetyItem.LastComment ?? "",
                                    DateTime = DateTime.Now,
                                    Type = vmTeamSafetyItem.Type,
                                    UserId = vmTeamSafetyItem.UserId,

                                });
                }

                UnitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}