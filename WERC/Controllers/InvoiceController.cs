using BLL;
using Model.ViewModels.Invoice;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WERC.Filters.CacheFilters;
using WERC.Models;

namespace WERC.Controllers
{
    public class InvoiceController : BaseController
    {

        [HttpGet]
        [ActionName("lif")]
        public PartialViewResult LoadInvoiceForm()
        {
            var blInvoice = new BLInvoice();

            var invoice = blInvoice.GetInvoiceByUserId(CurrentUserId, false);

            if (invoice != null)
            {
                int? lastOrderId = 0;

                var blShopCart = new BLShopCart();

                var lastOrderInfo = blShopCart.GetCheckoutStatus(CurrentUserId, invoice.Id, out lastOrderId);

                if (lastOrderInfo != null)
                {
                    blInvoice.UpdateInvoiceOrderStatus(lastOrderInfo, invoice.Id, true, lastOrderId.Value, true, true);

                    invoice = blInvoice.GetInvoiceFullInfoByUserId(CurrentUserId, false);
                }
            }

            var invoiceList = blInvoice.GetInvoiceFullInfoByUserId(CurrentUserId, false);

            if (invoiceList == null)
            {
                return PartialView("_EmptyReviewOrder", new VMHandleErrorInfo
                {
                    ErrorMessage = "There are no team(s) to pay." +
                    "Please, complete all team member(s) registration forms before you could proceed to payment"
                });

            }


            List<VmTeamSelection> teamSelectionList = new List<VmTeamSelection>();

            foreach (var item in invoiceList.InvoiceDetails)
            {
                teamSelectionList.Add(new VmTeamSelection
                {
                    Checked = item.IsChecked,
                    IsFirstTeam = item.IsFirstTeam,
                    TeamId = item.TeamId
                });
            }

            blInvoice.ProcessInvoice(CurrentUserId, invoiceList.LastCheckedId, teamSelectionList);
            invoiceList = blInvoice.GetInvoiceFullInfoByUserId(CurrentUserId, false);

            return PartialView("ReviewOrderManagement", invoiceList);
        }


        [HttpGet]
        [ActionName("lifem")]
        [NoCache]
        public PartialViewResult LoadExtraMemberInvoiceForm()
        {
            var blInvoice = new BLInvoice();

            var invoice = blInvoice.GetInvoiceByUserId(CurrentUserId, false);

            if (invoice != null)
            {
                int? lastOrderId = 0;

                var blShopCart = new BLShopCart();

                var lastOrderInfo = blShopCart.GetCheckoutStatus(CurrentUserId, invoice.Id, out lastOrderId);

                if (lastOrderInfo != null)
                {
                    blInvoice.UpdateInvoiceOrderStatus(lastOrderInfo, invoice.Id, true, lastOrderId.Value, true, true);

                }
            }

            var invoiceList = blInvoice.GetExtraMemberInvoiceFullInfoByUserId(CurrentUserId);
           
            if (invoiceList == null)
            {
                return PartialView("_EmptyReviewOrder", new VMHandleErrorInfo
                {
                    ErrorMessage = "There is no balance to pay for extra members."
                });
            }

            return PartialView("ExtraMemberReviewOrderManagement", invoiceList);
        }

        [HttpPost]
        [ActionName("pi")]
        public ActionResult ProcessInvoice(int currentTeamId, List<VmTeamSelection> teamSelectionList)
        {

            var blInvoice = new BLInvoice();
            var finishedInvoice = blInvoice.GetInvoiceByUserId(CurrentUserId, true);

            // if (finishedInvoice == null && teamSelectionList.Count(t => t.Checked == true) > 0 && teamSelectionList.Count(t => t.IsFirstTeam == true) != 1)
            if (blInvoice.GetPayedTeamCount(CurrentUserId) == 0 && teamSelectionList.Count(t => t.Checked == true) > 0 && teamSelectionList.Count(t => t.IsFirstTeam == true) != 1)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    hasError = true,
                    message = "One team must be as a first team",

                }, JsonRequestBehavior.DenyGet);
            }

            blInvoice.ProcessInvoice(CurrentUserId, currentTeamId, teamSelectionList);

            var invoice = blInvoice.GetInvoiceFullInfoByUserId(CurrentUserId, false);

            invoice.LastCheckedId = currentTeamId;

            return PartialView("~/Views/Invoice/_ReviewOrder.cshtml", invoice);
        }

    }
}