using BLL;
using CyberneticCode.Web.Mvc.Helpers;
using Model.ViewModels.Lab;
using Model.ViewModels.Team;
using Model.ViewModels.Test;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using WERC.Filters.ActionFilterAttributes;
using static Model.ApplicationDomainModels.ConstantObjects;

namespace WERC.Controllers.Lab
{
    [RoleBaseAuthorize(SystemRoles.Lab, SystemRoles.Admin)]
    public class LabController : BaseController
    {
        // GET: Lab
        public ActionResult Index()
        {
            return View(new VmLab());
        }

        #region Team Test
        [ActionName("lttmf")]
        [HttpGet]
        public ActionResult LoadTeamTestManagementForm()
        {
            return View("TeamTestManagementForm", new VmTeamTestManagement
            {
                CurrentUserId = CurrentUserId,
            });
        }

        [ActionName("lttf")]
        [HttpPost]
        public PartialViewResult LoadTeamTestTableForm(int taskId)
        {
            var blTest = new BLTest();
            var teamTestCollection = blTest.GetTeamTaskTest(CurrentUserId, taskId);

            return PartialView("_TeamTestTable", teamTestCollection);
        }

        [ActionName("st")]
        [HttpPost]
        public ActionResult SaveTest(VmTeamTestResult[] clientTest)
        {
            var result = true;
            var blTest = new BLTest();

            try
            {
                if (ModelState.IsValid)
                {
                    result = blTest.UpdateTest(CurrentUserId, clientTest);
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            var jsonData = new
            {
                success = result,
                message = "",
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        #endregion Team Test

        [ActionName("sjat")]
        [HttpPost]
        public async Task<JsonResult> SubmitLabsAssignedTasks(string[] userIdList)
        {
            //var domainName = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host;
            var domainName = "29th WERC Environmental Design Contest 2019";

            var subject = "Submitted your Assigned Tasks";
            var body = "<h1>" + domainName + "</h1>" +
                "<h2>Submitted your Assigned Tasks</h2>";

            foreach (var userId in userIdList)
            {
                await UserManager.SendEmailAsync(userId, subject, body);

                emailHelper = new EmailHelper()
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                    EmailList = new string[] { specialEmail }
                };

                emailHelper.Send();
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ActionName("tl")]
        public ActionResult TeamList(int activeItemId = -1)
        {
            var bsTeam = new BLTeam();

            return View("TeamList", new VmTeamCollection
            {
                HtmlControlId = "Lab_TeamList",
                DataAction = "jts",
                DataController = "Lab",
                AllowDownlaod = false,
                AllowEdit = false,
                AllowDelete = false,
                ActiveItemId = activeItemId,
                Draggable = false,
                ShowSearchBox = true,
                ParentHtmlControlId = "TeamList_ParentHtmlControlId",
                OnItemSelected = "",
                TeamList = bsTeam.GetLabTeams(CurrentUserId)
            });
        }

        [HttpPost]
        [ActionName("jts")]
        public PartialViewResult SearchTeam(
            bool draggable,
            string OnItemSelected,
            bool allowDownlaod,
            bool allowEdit,
            bool allowReject,
            string onItemRejecting,
            bool allowAccept,
            string onItemAccepting,
            bool allowDelete,
            string htmlControlId,
            string dataAction,
            string dataController,
            string ParentHtmlControlId, string onItemDragged,
            string teamName = "")
        {
            var bsTeam = new BLTeam();
            var teamList = bsTeam.GetLabTeams(CurrentUserId, teamName);

            return PartialView("_TeamList",
                new VmTeamCollection
                {
                    DataAction = dataAction,
                    DataController = dataController,
                    AllowDownlaod = allowDownlaod,
                    AllowEdit = allowEdit,
                    AllowReject = allowReject,
                    OnItemRejecting = onItemRejecting,
                    AllowAccept = allowAccept,
                    OnItemAccepting = onItemAccepting,
                    AllowDelete = allowDelete,
                    Draggable = draggable,
                    ShowSearchBox = true,
                    SearchText = teamName,
                    TeamList = teamList,
                    HtmlControlId = htmlControlId,
                    ParentHtmlControlId = ParentHtmlControlId,
                    OnItemSelected = OnItemSelected,
                    OnItemDragged = onItemDragged
                });
        }
        [HttpGet]
        [ActionName("lupf")]
        public ActionResult LoadUpdateProfileForm()
        {
            var blPerson = new BLPerson();
            var vmPerson = blPerson.GetPersonByUserId(CurrentUserId);

            vmPerson.HideEmergency = false;
            vmPerson.OnActionSuccess = "loadLabPanel";

            return View("UpdateProfile", vmPerson);
        }
    }
}
