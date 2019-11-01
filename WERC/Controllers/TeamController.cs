using BLL;
using CyberneticCode.Web.Mvc.Helpers;
using Model.Base;
using Model.ViewModels.Invoice;
using Model.ViewModels.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WERC.Filters.ActionFilterAttributes;
using static Model.ApplicationDomainModels.ConstantObjects;

namespace WERC.Controllers
{
    [RoleBaseAuthorize(SystemRoles.Admin, SystemRoles.Advisor, SystemRoles.CoAdvisor, SystemRoles.Leader, SystemRoles.SafetyAdmin)]
    public class TeamController : BaseController
    {
        // GET: Team
        public void RefreshTeam()
        {
            //var blInvoice = new BLInvoice();

            //var invoiceIds = blInvoice.GetInvoiceIds(false);

            //if (invoiceIds != null)
            //{
            //    foreach (var item in invoiceIds.Ids)
            //    {


            //        int? lastOrderId = 0;

            //        var blShopCart = new BLShopCart();

            //        var lastOrderInfo = blShopCart.GetCheckoutStatus(CurrentUserId, item, out lastOrderId);

            //        if (lastOrderInfo != null)
            //        {
            //            blInvoice.UpdateInvoiceOrderStatus(lastOrderInfo, item, true, lastOrderId.Value, true, true);

            //            invoiceIds = blInvoice.GetInvoiceFullInfoByUserId(CurrentUserId, false);
            //        }
            //    }
            //}

            //var invoiceList = blInvoice.GetInvoiceFullInfoByUserId(CurrentUserId, false);

            //RedirectToAction("tfim", "admin");
        }
        public ActionResult TeamList()
        {
            var bsTeam = new BLTeam();

            return View(new VmTeamCollection() { TeamList = bsTeam.GetAdvisorTeams(CurrentUserId) });
        }

        [ActionName("etps")]
        [HttpPost]
        public ActionResult EditPayStatus(List<VmTeamSelection> teamSelectionList)
        {
            var result = true;
            var blTeam = new BLTeam();
            string checkoutURL = "";
            try
            {

                var blShopCart = new BLShopCart();
                var checkoutResult = blShopCart.HandelCheckout(teamSelectionList, CurrentUserId);

                if (checkoutResult != null && checkoutResult.Error != "0")
                {
                    // Display Error 
                }
                else
                {
                    checkoutURL = checkoutResult.Url;
                }
                //var newOrder = blShopCart.CreateOrder();

                //var allOrders = blShopCart.GetAllOrders();
                //var allOrdersVerbose = blShopCart.GetAllOrdersVerbose();
                //var orderInfo = blShopCart.GetOrderInfo("149331");
                //var orderInfoVerbose = blShopCart.GetOrderInfoVerbose("149331");

                //if (newOrder != null)
                //{
                //    result = blTeam.UpdatePayStatus(teamSelectionList, true);
                //}
            }
            catch (Exception ex)
            {
                result = false;
            }
            var jsonData = new
            {
                success = result,
                message = "Operation has been succeeded",
                redirectCheckoutURL = checkoutURL

            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return View("../Author/TeamEdit", model);
        }


        [ActionName("etpsem")]
        [HttpPost]
        public ActionResult EditPayStatusExtraMember(int invoiceId)
        {
            var result = true;
            var blTeam = new BLTeam();
            string checkoutURL = "";
            try
            {

                var blShopCart = new BLShopCart();
                var checkoutResult = blShopCart.HandelCheckoutExtraMember(invoiceId, CurrentUserId);

                if (checkoutResult != null && checkoutResult.Error != "0")
                {
                    // Display Error 
                }
                else
                {
                    checkoutURL = checkoutResult.Url;
                }
                //var newOrder = blShopCart.CreateOrder();

                //var allOrders = blShopCart.GetAllOrders();
                //var allOrdersVerbose = blShopCart.GetAllOrdersVerbose();
                //var orderInfo = blShopCart.GetOrderInfo("149331");
                //var orderInfoVerbose = blShopCart.GetOrderInfoVerbose("149331");

                //if (newOrder != null)
                //{
                //    result = blTeam.UpdatePayStatus(teamSelectionList, true);
                //}
            }
            catch (Exception ex)
            {
                result = false;
            }
            var jsonData = new
            {
                success = result,
                message = "Operation has been succeeded",
                redirectCheckoutURL = checkoutURL

            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return View("../Author/TeamEdit", model);
        }

        [ActionName("gtfibf")]
        [HttpPost]
        public JsonResult GetTeamFullInfoByFilter(VmTeamFullInfo filter = null)
        {

            var blTeam = new BLTeam();

            var teamFullInfoList = blTeam.GetTeamFullInfoByFilter(filter).ToList();

            teamFullInfoList.First().LabResultUrl = "/Resources/Uploaded/Teams/97fda227f835461d8b39a4f3bf1fa9fc/20180816004448favicon.png?CT=application_vnd.oasis.opendocument.spreadsheet.png," +
                "/Resources/Uploaded/Teams/6a1436ffcab9434aad8fed02f32f2411/20180810151905favicon.png?CT=application_pdf.png," +
                "/Resources/Uploaded/Teams/97fda227f835461d8b39a4f3bf1fa9fc/20180816005047favicon.png?CT=application_pkcs7_mime.png";
            return Json(teamFullInfoList, JsonRequestBehavior.AllowGet);
        }
        [ActionName("gta")]
        [HttpGet]
        public JsonResult GetTeamActivation(VmTeam filter = null)
        {

            var blTeam = new BLTeam();

            var teamFullInfoList = blTeam.GetTeamList();

            teamFullInfoList.First().LabResultUrl = "/Resources/Uploaded/Teams/97fda227f835461d8b39a4f3bf1fa9fc/20180816004448favicon.png?CT=application_vnd.oasis.opendocument.spreadsheet.png," +
                "/Resources/Uploaded/Teams/6a1436ffcab9434aad8fed02f32f2411/20180810151905favicon.png?CT=application_pdf.png," +
                "/Resources/Uploaded/Teams/97fda227f835461d8b39a4f3bf1fa9fc/20180816005047favicon.png?CT=application_pkcs7_mime.png";
            return Json(teamFullInfoList, JsonRequestBehavior.AllowGet);
        }


        [ActionName("gtebf")]
        [HttpPost]
        public JsonResult GetTeamEmailByFilter(VmTeamFullInfo filter = null)
        {

            var blTeam = new BLTeam();

            var teamFullInfoList = blTeam.GetTeamFullInfoByFilter(filter).ToList();

            teamFullInfoList.First().LabResultUrl = "/Resources/Uploaded/Teams/97fda227f835461d8b39a4f3bf1fa9fc/20180816004448favicon.png?CT=application_vnd.oasis.opendocument.spreadsheet.png," +
                "/Resources/Uploaded/Teams/6a1436ffcab9434aad8fed02f32f2411/20180810151905favicon.png?CT=application_pdf.png," +
                "/Resources/Uploaded/Teams/97fda227f835461d8b39a4f3bf1fa9fc/20180816005047favicon.png?CT=application_pkcs7_mime.png";
            return Json(teamFullInfoList, JsonRequestBehavior.AllowGet);
        }

        [ActionName("gtfibfad")]
        [HttpGet]
        public JsonResult GetTeamFullInfoByFilterByAdvisor(VmTeamFullInfo filterItem = null)
        {
            var blTeam = new BLTeam();

            var teamFullInfoList = blTeam.GetTeamFullInfoByFilterByAdvisor(CurrentUserId, filterItem).ToList();
            teamFullInfoList.First().LabResultUrl = "/Resources/Uploaded/Teams/97fda227f835461d8b39a4f3bf1fa9fc/20180816004448favicon.png?CT=application_vnd.oasis.opendocument.spreadsheet.png," +
                "/Resources/Uploaded/Teams/6a1436ffcab9434aad8fed02f32f2411/20180810151905favicon.png?CT=application_pdf.png," +
                "/Resources/Uploaded/Teams/97fda227f835461d8b39a4f3bf1fa9fc/20180816005047favicon.png?CT=application_pkcs7_mime.png";
            return Json(teamFullInfoList, JsonRequestBehavior.AllowGet);
        }

        [ActionName("lctf")]
        [HttpGet]
        [RoleBaseAuthorize(SystemRoles.Advisor)]
        public ActionResult LoadCreateTeamForm()
        {
            var blPerson = new BLPerson();
            var person = blPerson.GetPersonByUserId(CurrentUserId);

            var bsTeam = new BLTeam();
            var teamCount = bsTeam.GetAdvisorTeams(CurrentUserId).Count();

            return View("../Advisor/CreateTeam", new VmTeam
            {
                University = person.University,
                TeamCount = teamCount,
                Name = person.University.Substring(0, 3) + "-" + teamCount
            });
        }

        [ActionName("ct")]
        [HttpPost]
        public ActionResult Create(VmTeam model)
        {
            var result = -1;
            var blTeam = new BLTeam();

            model.CurrentUserId = CurrentUserId;

            try
            {
                if (ModelState.IsValid)
                {
                    if (model.UploadedDocument != null)
                    {
                        model.TeamImageUrl = UIHelper.UploadFile(model.UploadedDocument, "/Resources/Uploaded/Teams/" + CurrentUserId.Replace("-", ""));
                    }
                }

                result = blTeam.CreateTeam(model);
            }

            catch (Exception ex)
            {
                result = -1;
            }


            if (result != -1)
            {
                return RedirectToAction("tl", "Advisor", new { activeItemId = result });
            }

            model.ActionMessageHandler.Message = "Operation has been failed...\n";

            return View("../Advisor/CreateTeam", model);
        }

        [ActionName("et")]
        [HttpPost]
        public ActionResult EditTeam(VmTeam model)
        {
            model.CurrentUserId = CurrentUserId;

            var oldUrl = model.TeamImageUrl;
            var result = true;
            var blTeam = new BLTeam();

            try
            {
                if (ModelState.IsValid)
                {
                    if (model.UploadedDocument != null)
                    {
                        model.TeamImageUrl = UIHelper.UploadFile(model.UploadedDocument, "/Resources/Uploaded/Teams/" + CurrentUserId.Replace("-", ""));
                    }
                }
                result = blTeam.UpdateTeam(model);
            }
            catch (Exception ex)
            {
                result = false;
            }

            //if (result != false && !string.IsNullOrEmpty(model.TeamImageUrl))
            //{
            //    UIHelper.DeleteFile(oldUrl);
            //}

            if (result == false)
            {
                model.ActionMessageHandler.Message = "Operation has been failed...\n call system Admin";
            }

            var jsonData = new
            {
                teamTitle = model.Name,
                teamIconUrl = model.TeamImageUrl,
                teamId = model.Id,
                success = result,
                message = model.ActionMessageHandler.Message = "Operation has been succeeded"

            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return View("../Author/TeamEdit", model);
        }

        [ActionName("urr")]
        [HttpPost]
        public ActionResult UploadWrittenReport(int teamId, string oldWrittenReportUrl, HttpPostedFileBase UploadedWrittenReport)
        {
            var result = true;
            var blTeam = new BLTeam();
            string writtenReportUrl = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    writtenReportUrl = UIHelper.UploadFile(UploadedWrittenReport, "/Resources/Uploaded/Teams/WrittenReport/" + CurrentUserId.Replace("-", ""));

                    result = blTeam.UploadWrittenReport(teamId, writtenReportUrl);

                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            //if (result != false && !string.IsNullOrEmpty(writtenReportUrl))
            //{
            //    UIHelper.DeleteFile(oldWrittenReportUrl);
            //}

            var jsonData = new
            {
                writtenReportUrl = HttpUtility.HtmlDecode(writtenReportUrl),
                writtenReportFileName = UploadedWrittenReport.FileName,
                writtenReportUrlIcon = "/Resources/Images/Mimetypes128x128/" + writtenReportUrl.Split(new string[] { "?CT=" }, StringSplitOptions.RemoveEmptyEntries)[1],
                success = result,
                message = "Your written report successfully has been uploaded."
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return View("../Author/TeamEdit", model);
        }

        [ActionName("uta")]
        [HttpPost]
        public ActionResult UpdateTeamActivation(int teamId)
        {
            var blTeam = new BLTeam();
            var lastStatus = blTeam.ReverseTeamActivation(teamId);
            var jsonData = new
            {
                lastStatus,
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return View("../Author/TeamEdit", model);
        }

        [ActionName("utss")]
        [HttpPost]
        public ActionResult UpdateTeamSuppressScoring(int teamId)
        {
            var blTeam = new BLTeam();
            var lastStatus = blTeam.ReverseTeamSuppressScoring(teamId);
            var jsonData = new
            {
                lastStatus,
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return View("../Author/TeamEdit", model);
        }

        [ActionName("dt")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = true;

            var blTeam = new BLTeam();

            result = blTeam.DeleteTeam(id);

            string resultMessage = string.Empty;

            if (result == true)
            {
                resultMessage = new BaseViewModel()["Team Has been deleted successfuly."];
            }
            else
            {
                resultMessage = new BaseViewModel()["This team has members. You can't delete it..."];

            }

            var jsonResult = new
            {
                success = result,
                message = resultMessage,
                teamId = id,
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

    }
}