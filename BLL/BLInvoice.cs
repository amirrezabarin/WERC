using BLL.Base;
using Model;
using Model.ViewModels.Invoice;
using Repository.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BLInvoice : BLBase
    {
        public int GetPayedTeamCount(string userId)
        {
            var viewInvoiceRepository = UnitOfWork.GetRepository<ViewInvoiceRepository>();

            return viewInvoiceRepository.GetPayedTeamCount(userId);
        }

        #region Extra Member      
        public VmInvoice GetExtraMemberInvoiceFullInfoByUserId(string userId)
        {
            var viewInvoiceRepository = UnitOfWork.GetRepository<ViewInvoiceExtraMemberRepository>();

            var viewInvoice = viewInvoiceRepository.GetViewInvoiceExtraMemberByUserId(userId, true, true);

            if (viewInvoice.Count() == 0)
            {
                return null;//At first, Please pay for the team by the peyment menu
            }

            var teamIds = viewInvoice.Select(t => t.TeamId).Distinct(); // Grouped Teams

            var invoiceDetailRepository = UnitOfWork.GetRepository<InvoiceDetailRepository>();

            var blparticipantRule = new BLParticipantRule();

            var blTeamMember = new BLTeamMember();

            var participantRule = blparticipantRule.GetParticipantRule();

            var extraMemberCount = 0;

            #region Temprorary Calculation


            foreach (var teamId in teamIds)
            {
                var teamExtraParticipantSum = viewInvoice.Where(i => i.TeamId == teamId).Sum(t => t.ExtraParticipantCount);

                var team = viewInvoice.Where(i => i.TeamId == teamId).First();

                var teamMemberCount = blTeamMember.GetTeamMembersCount(team.TeamId);

                extraMemberCount = 0;

                if (team.IsFirstTeam.Value)
                {

                    extraMemberCount = (teamMemberCount - participantRule.FirstTeamMaxMember - teamExtraParticipantSum).Value;

                }
                else
                {
                    extraMemberCount = (teamMemberCount - participantRule.EachExtraTeamMaxMember - teamExtraParticipantSum).Value;

                }

                if (extraMemberCount > 0)
                {
                    break;
                }

            }

            if (extraMemberCount <= 0)
            {
                return null;// There is no balance to pay for extra members.
            }

            #endregion Temprorary Calculation

            #region Create/Edit Invoice

            VmInvoice vmInvoice = null;

            if (extraMemberCount > 0) // there is one payable extra member at least
            {

                var viewInvoiceUnFinished = viewInvoiceRepository.GetViewInvoiceExtraMemberByUserId(userId, true, false);

                if (viewInvoiceUnFinished == null || viewInvoiceUnFinished.Count() == 0)
                {
                    vmInvoice = new VmInvoice
                    {
                        UserId = userId,
                        DateOfIssue = DateTime.Now.Date,
                        InvoiceTotal = 0,
                        Subtotal = 0,
                        Tax = 0,
                        Total = 0,
                        AmountDue = 0,
                        ConventionalFee = 2.5M,
                        Finished = false,
                    };

                    vmInvoice.Id = CreateInvoice(vmInvoice);

                }
                else
                {
                    var blInvoice = new BLInvoice();
                    vmInvoice = blInvoice.GetInvoiceById(viewInvoiceUnFinished.First().InvoiceId.Value);

                    invoiceDetailRepository.DeleteInvoiceDetailsByInvoiceId(viewInvoiceUnFinished.First().InvoiceId.Value);
                    UnitOfWork.Commit();

                }

                vmInvoice.Subtotal = 0;
            }

            #endregion Create/Edit Invoice

            #region Calculation

            foreach (var teamId in teamIds)
            {

                var teamExtraParticipantSum = viewInvoice.Where(i => i.TeamId == teamId).Sum(t => t.ExtraParticipantCount);

                var team = viewInvoice.Where(i => i.TeamId == teamId).First();

                var teamMemberCount = blTeamMember.GetTeamMembersCount(team.TeamId);

                extraMemberCount = 0;

                if (team.IsFirstTeam.Value)
                {

                    extraMemberCount = (teamMemberCount - participantRule.FirstTeamMaxMember - teamExtraParticipantSum).Value;

                }
                else
                {
                    extraMemberCount = (teamMemberCount - participantRule.EachExtraTeamMaxMember - teamExtraParticipantSum).Value;

                }

                if (extraMemberCount > 0) // has payable extra member
                {

                    var invoiceDetail = new InvoiceDetail
                    {
                        TeamId = team.TeamId,
                        InvoiceId = vmInvoice.Id,
                        PaymentRuleId = team.PaymentRuleId.Value,
                        IsFirstTeam = team.IsFirstTeam.Value,
                        ConventionalFee = vmInvoice.ConventionalFee,
                        ExtraTeamDiscount = 0
                    };

                    invoiceDetail.ExtraParticipantCount = extraMemberCount;

                    invoiceDetail.TeamUnitCost = 0;

                    invoiceDetail.ExtraParticipantUnitCost = participantRule.ExtraParticipantFee;

                    invoiceDetail.ExtraParticipantAmount = invoiceDetail.ExtraParticipantCount * invoiceDetail.ExtraParticipantUnitCost;

                    invoiceDetail.Amount = invoiceDetail.ExtraParticipantAmount;

                    invoiceDetailRepository.CreateInvoiceDetail(invoiceDetail);

                    vmInvoice.Subtotal += invoiceDetail.Amount;

                    vmInvoice.TotalConventionalFee += vmInvoice.ConventionalFee * invoiceDetail.Amount / 100;
                }

            }

            #endregion Calculation

            UnitOfWork.Commit();

            vmInvoice.Total = vmInvoice.Subtotal + vmInvoice.TotalConventionalFee;
            vmInvoice.AmountDue = vmInvoice.Total;
            vmInvoice.InvoiceTotal = vmInvoice.AmountDue * vmInvoice.ConventionalFee / 100;

            UpdateInvoice(vmInvoice);

            viewInvoice = viewInvoiceRepository.GetViewInvoiceExtraMemberByUserId(userId, true, false);

            var viewDataInvoice = viewInvoiceRepository.GetViewInvoiceExtraMemberById(vmInvoice.Id);

            var invoice = viewDataInvoice.OrderByDescending(i => i.Total).First();

            var allowCheckFisrtTeam = false;

            if (GetPayedTeamCount(userId) == 0)
            {
                allowCheckFisrtTeam = true;
            }

            vmInvoice.Id = invoice.InvoiceId ?? -1;
            vmInvoice.University = invoice.University;
            vmInvoice.Address = invoice.StreetLine1 + " " + invoice.StreetLine2 + " " + invoice.City + invoice.State + " " + invoice.ZipCode;
            vmInvoice.AccountOwner = invoice.FirstName + " " + invoice.LastName;
            vmInvoice.DateOfIssue = invoice.DateOfIssue ?? DateTime.Now.Date;
            vmInvoice.InvoiceNumber = invoice.InvoiceNumber ?? 0;
            vmInvoice.InvoiceTotal = invoice.InvoiceTotal ?? 0;
            vmInvoice.Title = invoice.Title ?? "";
            vmInvoice.UserId = invoice.UserId;
            vmInvoice.Subtotal = invoice.Subtotal ?? 0;
            vmInvoice.Tax = invoice.Tax ?? 0;
            vmInvoice.Total = invoice.Total ?? 0;
            vmInvoice.AmountDue = invoice.AmountDue ?? 0;
            vmInvoice.AllowCheckFisrtTeam = allowCheckFisrtTeam;

            vmInvoice.InvoiceDetails =

                (from i in viewInvoice
                 select new VmInvoiceDetail
                 {
                     Amount = i.Amount ?? 0,
                     ExtraParticipantAmount = i.ExtraParticipantAmount ?? 0,
                     ExtraParticipantCount = i.ExtraParticipantCount ?? 0,
                     ExtraParticipantUnitCost = i.ExtraParticipantUnitCost ?? 0,
                     ExtraTeamDiscount = i.ExtraTeamDiscount ?? 0,
                     Id = i.InvoiceId ?? -1,
                     InvoiceId = invoice.InvoiceId ?? -1,
                     IsFirstTeam = i.IsFirstTeam ?? false,
                     FirstTeamOrExtraTeam =
                           (allowCheckFisrtTeam == true) ?
                                               (i.IsFirstTeam == null || i.IsFirstTeam == false) ? "Extra" : "First"
                                               : "Extra",

                     PaymentRuleId = i.PaymentRuleId ?? -1,
                     PayStatus = i.PayStatus,
                     TeamId = i.TeamId,
                     TeamName = i.TeamName,
                     TeamUnitCost = (i.IsFirstTeam == null || i.IsFirstTeam == false) ?
                           (i.TeamUnitCost - i.ExtraTeamDiscount) ?? 0 : i.TeamUnitCost ?? 0,
                     TypeOfRegistration = i.TypeOfRegistration,
                     DueDate = i.DueDate ?? DateTime.Now.Date,
                     IsChecked = i.Amount > 0 ? true : false,
                     ConventionalFee = i.Amount * i.ConventionalFee / 100,
                     Task = i.Task,

                 }).ToList();

            vmInvoice.TotalConventionalFee = vmInvoice.InvoiceDetails.Sum(i => i.ConventionalFee).Value;

            return vmInvoice;
        }
        #endregion Extra Member 

        public VmInvoice GetInvoiceFullInfoByUserId(string userId, bool payStatus = false)
        {
            var viewInvoiceRepository = UnitOfWork.GetRepository<ViewInvoiceRepository>();

            var viewInvoice = viewInvoiceRepository.GetViewInvoiceByUserId(userId, payStatus);

            if (viewInvoice.Count() == 0)
            {
                return null;
            }

            var invoice = viewInvoice.OrderByDescending(i => i.Total).First();

            var allowCheckFisrtTeam = false;

            if (GetPayedTeamCount(userId) == 0)
            {
                allowCheckFisrtTeam = true;
            }

            var vmInvoice = new VmInvoice
            {
                Id = invoice.InvoiceId ?? -1,
                University = invoice.University,

                Address = invoice.StreetLine1 + " " +
                                invoice.StreetLine2 + " " + invoice.City + invoice.State + " " + invoice.ZipCode,
                AccountOwner = invoice.FirstName + " " + invoice.LastName,
                DateOfIssue = invoice.DateOfIssue ?? DateTime.Now.Date,
                InvoiceNumber = invoice.InvoiceNumber ?? 0,
                InvoiceTotal = invoice.InvoiceTotal ?? 0,
                Title = invoice.Title ?? "",
                UserId = invoice.UserId,
                Subtotal = invoice.Subtotal ?? 0,
                Tax = invoice.Tax ?? 0,
                Total = invoice.Total ?? 0,
                AmountDue = invoice.AmountDue ?? 0,
                AllowCheckFisrtTeam = allowCheckFisrtTeam,
                InvoiceDetails = (from i in viewInvoice
                                  select new VmInvoiceDetail
                                  {
                                      Amount = i.Amount ?? 0,
                                      ExtraParticipantAmount = i.ExtraParticipantAmount ?? 0,
                                      ExtraParticipantCount = i.ExtraParticipantCount ?? 0,
                                      ExtraParticipantUnitCost = i.ExtraParticipantUnitCost ?? 0,
                                      ExtraTeamDiscount = i.ExtraTeamDiscount ?? 0,
                                      Id = i.InvoiceId ?? -1,
                                      InvoiceId = invoice.InvoiceId ?? -1,
                                      IsFirstTeam = i.IsFirstTeam ?? false,
                                      FirstTeamOrExtraTeam =
                                            (allowCheckFisrtTeam == true) ?
                                                                (i.IsFirstTeam == null || i.IsFirstTeam == false) ? "Extra" : "First"
                                                                : "Extra",

                                      PaymentRuleId = i.PaymentRuleId ?? -1,
                                      PayStatus = i.PayStatus,
                                      TeamId = i.TeamId,
                                      TeamName = i.TeamName,
                                      TeamUnitCost = (i.IsFirstTeam == null || i.IsFirstTeam == false) ?
                                            (i.TeamUnitCost - i.ExtraTeamDiscount) ?? 0 : i.TeamUnitCost ?? 0,
                                      TypeOfRegistration = i.TypeOfRegistration,
                                      DueDate = i.DueDate ?? DateTime.Now.Date,
                                      IsChecked = i.Amount > 0 ? true : false,
                                      ConventionalFee = i.Amount * i.ConventionalFee / 100,
                                      Task = i.Task,

                                  }).ToList(),
            };

            vmInvoice.TotalConventionalFee = vmInvoice.InvoiceDetails.Sum(i => i.ConventionalFee).Value;
            return vmInvoice;
        }
        public VmInvoice GetInvoiceFullInfoById(int invoiceId)
        {
            var viewInvoiceRepository = UnitOfWork.GetRepository<ViewInvoiceRepository>();

            var viewInvoice = viewInvoiceRepository.GetViewInvoiceById(invoiceId);

            if (viewInvoice.Count() == 0)
            {
                return null;
            }

            var invoice = viewInvoice.OrderByDescending(i => i.Total).First();


            var vmInvoice = new VmInvoice
            {
                Id = invoice.InvoiceId ?? -1,
                University = invoice.University,

                Address = invoice.StreetLine1 + " " +
                                invoice.StreetLine2 + " " + invoice.City + invoice.State + " " + invoice.ZipCode,
                AccountOwner = invoice.FirstName + " " + invoice.LastName,
                DateOfIssue = invoice.DateOfIssue ?? DateTime.Now.Date,
                InvoiceNumber = invoice.InvoiceNumber ?? 0,
                InvoiceTotal = invoice.InvoiceTotal ?? 0,
                Title = invoice.Title ?? "",
                UserId = invoice.UserId,
                Subtotal = invoice.Subtotal ?? 0,
                Tax = invoice.Tax ?? 0,
                Total = invoice.Total ?? 0,
                AmountDue = invoice.AmountDue ?? 0,
                InvoiceDetails = (from i in viewInvoice
                                  select new VmInvoiceDetail
                                  {
                                      Amount = i.Amount ?? 0,
                                      ExtraParticipantAmount = i.ExtraParticipantAmount ?? 0,
                                      ExtraParticipantCount = i.ExtraParticipantCount ?? 0,
                                      ExtraParticipantUnitCost = i.ExtraParticipantUnitCost ?? 0,
                                      ExtraTeamDiscount = i.ExtraTeamDiscount ?? 0,
                                      Id = i.InvoiceId ?? -1,
                                      InvoiceId = invoice.InvoiceId ?? -1,
                                      IsFirstTeam = i.IsFirstTeam ?? false,
                                      FirstTeamOrExtraTeam = (i.IsFirstTeam == null || i.IsFirstTeam == false) ? "Extra" : "First",

                                      PaymentRuleId = i.PaymentRuleId ?? -1,
                                      PayStatus = i.PayStatus,
                                      TeamId = i.TeamId,
                                      TeamName = i.TeamName,
                                      TeamUnitCost = (i.IsFirstTeam == null || i.IsFirstTeam == false) ?
                                            (i.TeamUnitCost - i.ExtraTeamDiscount) ?? 0 : i.TeamUnitCost ?? 0,
                                      TypeOfRegistration = i.TypeOfRegistration,
                                      DueDate = i.DueDate ?? DateTime.Now.Date,
                                      IsChecked = i.Amount > 0 ? true : false,
                                      ConventionalFee = i.Amount * i.ConventionalFee / 100,
                                      Task = i.Task,

                                  }).ToList(),
            };

            vmInvoice.TotalConventionalFee = vmInvoice.InvoiceDetails.Sum(i => i.ConventionalFee).Value;
            return vmInvoice;
        }
        public VmInvoiceIds GetInvoiceFullInfoByUserAndStatus(string userId, bool status)
        {
            var invoiceRepository = UnitOfWork.GetRepository<InvoiceRepository>();

            var invoiceList = invoiceRepository.GetInvoiceIdsByUserId(userId, status);

            if (invoiceList.Count() == 0)
            {
                return null;
            }

            var ids = new VmInvoiceIds() { Ids = invoiceList.Select(i => i.Id).ToArray() };

            return ids;
        }
        public VmInvoiceIds GetInvoiceIds(bool status)
        {
            var invoiceRepository = UnitOfWork.GetRepository<InvoiceRepository>();

            var invoiceList = invoiceRepository.GetInvoiceIds(status);

            if (invoiceList.Count() == 0)
            {
                return null;
            }

            var ids = new VmInvoiceIds() { Ids = invoiceList.Select(i => i.Id).ToArray() };

            return ids;
        }
        public VmInvoice GetInvoiceById(int id)
        {
            var invoiceRepository = UnitOfWork.GetRepository<InvoiceRepository>();

            var invoice = invoiceRepository.GetInvoiceById(id);
            var vmInvoice = new VmInvoice
            {
                Id = invoice.Id,
                UserId = invoice.UserId,
                Title = invoice.Title,
                DateOfIssue = invoice.DateOfIssue,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceTotal = invoice.InvoiceTotal,
                Subtotal = invoice.Subtotal,
                Tax = invoice.Tax,
                Total = invoice.Total,
                AmountDue = invoice.AmountDue,
                ConventionalFee = invoice.ConventionalFee.Value,
                Finished = invoice.Finished,
            };

            return vmInvoice;
        }
        public VmInvoice GetInvoiceByUserId(string userId, bool finished)
        {
            var invoiceRepository = UnitOfWork.GetRepository<InvoiceRepository>();

            var invoice = invoiceRepository.GetInvoiceByUserId(userId, finished);

            if (invoice == null)
            {
                return null;
            }

            var vmInvoice = new VmInvoice
            {
                Id = invoice.Id,
                UserId = invoice.UserId,
                Title = invoice.Title,
                DateOfIssue = invoice.DateOfIssue,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceTotal = invoice.InvoiceTotal,
                Subtotal = invoice.Subtotal,
                Tax = invoice.Tax,
                Total = invoice.Total,
                AmountDue = invoice.AmountDue,
                ConventionalFee = invoice.ConventionalFee.Value,
                Finished = invoice.Finished,

            };

            return vmInvoice;
        }
        public IEnumerable<VmInvoiceDetail> GetInvoiceDetailByInvoiceId(int invoiceId)
        {
            var invoiceRepository = UnitOfWork.GetRepository<InvoiceDetailRepository>();

            var invoiceDetailList = invoiceRepository.GetInvoiceDetailByInvoiceId(invoiceId);

            var vmInvoiceDetails = from invoiceDetail in invoiceDetailList
                                   select new VmInvoiceDetail
                                   {
                                       Id = invoiceDetail.Id,
                                       IsFirstTeam = invoiceDetail.IsFirstTeam,
                                       PaymentRuleId = invoiceDetail.PaymentRuleId,
                                       TeamId = invoiceDetail.TeamId,
                                       TeamUnitCost = invoiceDetail.TeamUnitCost,
                                       ExtraTeamDiscount = invoiceDetail.ExtraTeamDiscount,
                                       ExtraParticipantUnitCost = invoiceDetail.ExtraParticipantUnitCost,
                                       ExtraParticipantCount = invoiceDetail.ExtraParticipantCount,
                                       ExtraParticipantAmount = invoiceDetail.ExtraParticipantAmount,
                                       Amount = invoiceDetail.Amount,
                                       ConventionalFee = invoiceDetail.ConventionalFee,
                                       IsChecked = invoiceDetail.Amount > 0 ? true : false,
                                   };

            return vmInvoiceDetails;
        }
        public void ProcessInvoice(string currentUserId, int currentTeamId, List<VmTeamSelection> teamSelectionList)
        {
            var invoiceDetailRepository = UnitOfWork.GetRepository<InvoiceDetailRepository>();

            var blPaymentRule = new BLPaymentRule();
            var blparticipantRule = new BLParticipantRule();
            var blTeamMember = new BLTeamMember();

            var paymentRule = blPaymentRule.GetPaymentRuleBySuitableDueDate(DateTime.Now.Date);

            var participantRule = blparticipantRule.GetParticipantRule();

            if (paymentRule != null)
            {
                var vmInvoice = GetInvoiceByUserId(currentUserId, false);

                if (vmInvoice == null)
                {
                    vmInvoice = new VmInvoice
                    {
                        UserId = currentUserId,
                        DateOfIssue = DateTime.Now.Date,
                        InvoiceTotal = 0,
                        Subtotal = 0,
                        Tax = 0,
                        Total = 0,
                        AmountDue = 0,
                        ConventionalFee = 2.5M,
                        Finished = false,
                    };

                    vmInvoice.Id = CreateInvoice(vmInvoice);
                }

                vmInvoice.Subtotal = 0;

                invoiceDetailRepository.DeleteInvoiceDetailsByInvoiceId(vmInvoice.Id);

                var finishedInvoice = GetInvoiceByUserId(currentUserId, true);

                var allowCheckFisrtTeam = false;

                if (finishedInvoice == null)
                {
                    allowCheckFisrtTeam = true;
                }

                foreach (var item in teamSelectionList)
                {
                    var teamMemberCount = blTeamMember.GetTeamMembersCount(item.TeamId);

                    var invoiceDetail = new InvoiceDetail();

                    if (item.Checked == true)
                    {
                        invoiceDetail.TeamId = item.TeamId;
                        invoiceDetail.InvoiceId = vmInvoice.Id;
                        invoiceDetail.PaymentRuleId = paymentRule.Id;
                        invoiceDetail.IsFirstTeam = item.IsFirstTeam;
                        invoiceDetail.ConventionalFee = vmInvoice.ConventionalFee;

                        if (item.IsFirstTeam && allowCheckFisrtTeam == true)
                        {
                            invoiceDetail.ExtraTeamDiscount = 0;
                            invoiceDetail.ExtraParticipantCount = (teamMemberCount - participantRule.FirstTeamMaxMember) < 0 ? 0 : teamMemberCount - participantRule.FirstTeamMaxMember;

                        }
                        else
                        {
                            invoiceDetail.ExtraTeamDiscount = paymentRule.ExtraTeamDiscount;
                            invoiceDetail.ExtraParticipantCount = (teamMemberCount - participantRule.EachExtraTeamMaxMember) < 0 ? 0 : teamMemberCount - participantRule.EachExtraTeamMaxMember;
                            invoiceDetail.TeamUnitCost = paymentRule.FirstTeamFee - paymentRule.ExtraTeamDiscount;//Team Unit Cost

                        }

                        invoiceDetail.TeamUnitCost = paymentRule.FirstTeamFee;//Team Unit Cost
                        invoiceDetail.ExtraParticipantUnitCost = participantRule.ExtraParticipantFee;
                        invoiceDetail.ExtraParticipantAmount = invoiceDetail.ExtraParticipantCount * invoiceDetail.ExtraParticipantUnitCost;
                        invoiceDetail.Amount = invoiceDetail.ExtraParticipantAmount + invoiceDetail.TeamUnitCost - invoiceDetail.ExtraTeamDiscount;

                        invoiceDetailRepository.CreateInvoiceDetail(invoiceDetail);

                        vmInvoice.Subtotal += invoiceDetail.Amount;
                        vmInvoice.TotalConventionalFee += vmInvoice.ConventionalFee * invoiceDetail.Amount / 100;
                    }
                }

                UnitOfWork.Commit();

                vmInvoice.Total = vmInvoice.Subtotal + vmInvoice.TotalConventionalFee;
                vmInvoice.AmountDue = vmInvoice.Total;
                vmInvoice.InvoiceTotal = vmInvoice.AmountDue * vmInvoice.ConventionalFee / 100;

                UpdateInvoice(vmInvoice);

            }

        }

        public int CreateInvoice(VmInvoice vmInvoice)
        {
            var result = -1;
            try
            {
                var invoiceRepository = UnitOfWork.GetRepository<InvoiceRepository>();

                var newInvoice = new Invoice
                {
                    Id = vmInvoice.Id,
                    UserId = vmInvoice.UserId,
                    Title = vmInvoice.Title,
                    DateOfIssue = vmInvoice.DateOfIssue,
                    InvoiceNumber = vmInvoice.InvoiceNumber,
                    InvoiceTotal = vmInvoice.InvoiceTotal,
                    Subtotal = vmInvoice.Subtotal,
                    Tax = vmInvoice.Tax,
                    Total = vmInvoice.Total,
                    AmountDue = vmInvoice.AmountDue,
                    ConventionalFee = vmInvoice.ConventionalFee,
                    Finished = vmInvoice.Finished,
                };

                invoiceRepository.CreateInvoice(newInvoice);

                UnitOfWork.Commit();

                result = newInvoice.Id;

            }
            catch (Exception ex)
            {
                result = -1;
            }

            return result;
        }
        public bool UpdateInvoice(VmInvoice vmInvoice)
        {
            try
            {
                var invoiceRepository = UnitOfWork.GetRepository<InvoiceRepository>();

                var updateableInvoice = new Invoice
                {
                    Id = vmInvoice.Id,
                    UserId = vmInvoice.UserId,
                    Title = vmInvoice.Title,
                    DateOfIssue = vmInvoice.DateOfIssue,
                    InvoiceNumber = vmInvoice.InvoiceNumber,
                    InvoiceTotal = vmInvoice.InvoiceTotal,
                    Subtotal = vmInvoice.Subtotal,
                    Tax = vmInvoice.Tax,
                    Total = vmInvoice.Total,
                    AmountDue = vmInvoice.AmountDue,
                    ConventionalFee = vmInvoice.ConventionalFee,
                    Finished = vmInvoice.Finished,
                };

                invoiceRepository.UpdateInvoice(updateableInvoice);

                return UnitOfWork.Commit();
            }
            catch
            {
                return false;
            }

        }
        public bool UpdateInvoiceStatus(int invoiceId, bool finished)
        {
            try
            {
                var invoiceRepository = UnitOfWork.GetRepository<InvoiceRepository>();

                invoiceRepository.UpdateInvoiceStatus(invoiceId, finished);

                return UnitOfWork.Commit();
            }
            catch
            {
                return false;
            }

        }
        public bool UpdateInvoiceOrderStatus(Model.ShopCart.Order.OrderInfo.Result lastOrderInfo, int invoiceId, bool finished, int orderId, bool complete, bool teamPayStatus)
        {
            try
            {
                var invoiceRepository = UnitOfWork.GetRepository<InvoiceRepository>();

                invoiceRepository.UpdateInvoiceStatus(invoiceId, finished);

                var orderRepository = UnitOfWork.GetRepository<OrderRepository>();

                orderRepository.UpdateOrderStatus(lastOrderInfo.Order.Received, lastOrderInfo.Order.Tx, orderId, complete);

                var invoiceDetailRepository = UnitOfWork.GetRepository<InvoiceDetailRepository>();

                var invoiceDetailList = invoiceDetailRepository.GetInvoiceDetailByInvoiceId(invoiceId);

                var teamIdList = invoiceDetailList.Select(i => i.TeamId).ToArray<int>();

                var teamRepository = UnitOfWork.GetRepository<TeamRepository>();

                teamRepository.UpdatePayStatus(teamIdList, teamPayStatus);

                return UnitOfWork.Commit();
            }
            catch
            {
                return false;
            }

        }
        public bool DeleteInvoice(int invoiceId)
        {
            try
            {
                var invoiceRepository = UnitOfWork.GetRepository<InvoiceRepository>();

                invoiceRepository.DeleteInvoice(invoiceId);

                return UnitOfWork.Commit();
            }
            catch
            {
                return false;
            }

        }
    }
}
