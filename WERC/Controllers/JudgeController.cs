
using BLL;
using CyberneticCode.Web.Mvc.Helpers;
using Model.ViewModels.Judge;
using Model.ViewModels.Team;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WERC.Filters.ActionFilterAttributes;
using static Model.ApplicationDomainModels.ConstantObjects;

namespace WERC.Controllers.Judge
{
    [RoleBaseAuthorize(SystemRoles.Judge, SystemRoles.Admin)]
    public class JudgeController : BaseController
    {
        // GET: Judge
        public ActionResult Index()
        {
            return View(new VmJudge());
        }

        [ActionName("lttf")]
        [HttpPost]
        public PartialViewResult LoadTeamTestTableForm(int teamId)
        {
            var blTest = new BLTest();
            var teamTestCollection = blTest.GetTeamTaskTestByTeam(teamId);

            return PartialView("_TeamTestTableJudge", teamTestCollection);
        }

        [ActionName("gjfibf")]
        [HttpGet]
        public JsonResult GetJudgeFullInfoByFilter(VmJudgeFullInfo filterItem = null)
        {
            var blPerson = new BLPerson();

            var judgeFullInfoList = blPerson.GetJudgeFullInfoByFilter(filterItem).ToList();

            return Json(judgeFullInfoList, JsonRequestBehavior.AllowGet);
        }

        [ActionName("sjat")]
        [HttpPost]
        public async Task<JsonResult> SubmitJudgesAssignedTasks(string[] userIdList)
        { //var domainName = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host;
            var taskList = "";
            var blUserTask = new BLUserTask();
            var userTaskList = blUserTask.GetUserTasksByUsers(userIdList);

            var domainName = "29th WERC Environmental Design Contest 2019";

            var subject = "Task Assignment for the 29th WERC Design Contest";
            var body = "";

            foreach (var userId in userIdList)
            {
                var usertaskList = from u in userTaskList where u.UserId == userId select u;
                var fullName = usertaskList.First().Name;

                taskList = string.Join(",", usertaskList.Select(t => t.TaskName));

                body = "<h1>" + domainName + "</h1>" +
                "Dear " + fullName + ",<br/>" +
                "<br/>" +

                "I would like to thank you in advance for your time and effort that you will put into this design contest.<br/>" +

                "Tasks has been assigned to judges.We've worked so hard trying to accommodate as much as we could your preferences.<br/> " +

                "You will be assigned to judge teams in the following task(s): <strong>" + taskList + "</strong><br/>" +

                "Each team must submit a single PDF of the complete report(including the audits) no later than March 25th, 2019.Late reports will be penalized at a rate of 25 points per day.<br/>" +

                "The guidelines sent to teams about the criteria of their written report can be seen " +
                "<a href='https://iee.nmsu.edu/outreach/events/international-environmental-design-contest/guidelines/written-report/'>here</a>.<br/>" +

                "Another email will be sent shortly to discuss in detail the scoring process.<br/>" +

                "Once again, thank you.<br/>" +
                "<br/>" +
                "Kind Regards, <br/>" +
                "WERC team <br/>" +

                "<br/>" +
                "Environmental Design Contest <br/>" +

                "<br/>" +
                "Phone: 575-646-8171<br/>" +
                "Fax:     575-646-5440<br/>" +

                "<br/>" +
                "College of Engineering<br/>" +
                "1025 Stewart Street<br/>" +
                "MSC Eng NM<br/>" +
                "New Mexico State University<br/>" +
                "P.O. Box 30001<br/>" +
                "Las Cruces, NM 88003-8001<br/>" +

                "<br/>" +
                "<h5 style='color:#8c0b42'>BE BOLD. Shape the Future.</h5>" +
                "<strong style='color:#8c0b42'>New Mexico State University</strong><br/>";

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
                HtmlControlId = "Judge_TeamList",
                DataAction = "jts",
                DataController = "Judge",
                AllowDownlaod = false,
                AllowEdit = false,
                AllowDelete = false,
                ActiveItemId = activeItemId,
                Draggable = false,
                ShowSearchBox = true,
                ParentHtmlControlId = "TeamList_ParentHtmlControlId",
                OnItemSelected = "",
                TeamList = bsTeam.GetJudgeTeams(CurrentUserId)
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
            var teamList = bsTeam.GetJudgeTeams(CurrentUserId, teamName);

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
            vmPerson.OnActionSuccess = "loadJudgePanel";

            return View("UpdateProfile", vmPerson);
        }

        #region Grading Report

        [HttpGet]
        [ActionName("fgrm")]
        public ActionResult LoadFinalGradesReport()
        {
            var blGrade = new BLGrade();
            var gradegReportList = blGrade.GetGradeReportList(CurrentUserId);

            return View("FinalGradeReportManagement", new VmFinalGradeReportManagement
            {
                CurrentUserId = CurrentUserId,
                GradeReportList = gradegReportList
            });
        }

        [HttpPost]
        [ActionName("lged")]
        [RoleBaseAuthorize(SystemRoles.Judge, SystemRoles.Admin, SystemRoles.Student, SystemRoles.Advisor, SystemRoles.CoAdvisor, SystemRoles.Leader)]
        public PartialViewResult LoadGradingEvaluationDetail(int taskId, int teamId, int gradeId)
        {
            var blTeamGradeDetail = new BLTeamGradeDetail();
            var teamGradeDetailList = blTeamGradeDetail.GetGradingEvaluationDetail(taskId, teamId, gradeId).ToList();

            return PartialView("_FinalGradesReportDetailTable", new VmGradeDetailManagement
            {
                CurrentUserId = CurrentUserId,
                GradeDetailList = teamGradeDetailList
            });
        }

        #endregion Grading Report
    }
}
