using WERC.Filters.ActionFilterAttributes;
using Model.Base;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Model.ApplicationDomainModels.ConstantObjects;
using Model.ViewModels.Advisor;
using BLL;
using Model.ViewModels.Team;
using Model.ViewModels.TeamSafetyItem;

namespace WERC.Controllers.Advisor
{
    [RoleBaseAuthorize(SystemRoles.Advisor,SystemRoles.CoAdvisor, SystemRoles.Leader)]
    public class AdvisorController : BaseController
    {
        // GET: Advisor
        public ActionResult Index()
        {
            return View(new VmAdvisor());
        }

        [HttpGet]
        [ActionName("gesp")]
        public ActionResult GetESP(int id)
        {
            var blTeamMember = new BLTeamMember();
            var teamId = id;
            var blTeamSafetyItem = new BLTeamSafetyItem();
            var vmTeamSafetyItemList = blTeamSafetyItem.GetTeamSafetyItemByTeamId(teamId);
            var blReference = new BLReference();

            return View("ExperimentalSafetyPlan",
                new VmTeamSafetyItemManagement
                {
                    TeamSafetyItemCollection = new VmTeamSafetyItemCollection
                    {
                        TeamSafetyItemList = vmTeamSafetyItemList,
                        ReferenceFiles = blReference.GetAllReference(),
                        CurrentUserRoles = CurrentUserRoles
    
                    }
                });
        }

        [HttpGet]
        [ActionName("tl")]
        public ActionResult TeamList(int activeItemId = -1)
        {
            var bsTeam = new BLTeam();

            return View("TeamList", new VmTeamCollection
            {
                HtmlControlId = "Advisor_TeamList",
                DataAction = "ats",
                DataController = "Advisor",
                AllowDownlaod = true,
                AllowEdit = true,
                AllowDelete = true,
                ActiveItemId = activeItemId,
                Draggable = false,
                ShowSearchBox = false,
                ParentHtmlControlId = "TeamList_ParentHtmlControlId",
                OnItemSelected = "",
                TeamList = bsTeam.GetAdvisorTeams(CurrentUserId)
            });
        }

        [HttpGet]
        [ActionName("lupf")]
        public ActionResult LoadUpdateProfileForm()
        {
            var blPerson = new BLPerson();
            var vmPerson = blPerson.GetPersonByUserId(CurrentUserId);

            vmPerson.OnActionSuccess = "loadAdvisorPanel";

            return View("UpdateProfile", vmPerson);
        }

        [HttpGet]
        [ActionName("ltef")]
        public ActionResult LoadTeamEditForm(int id)
        {
            var blTeam = new BLTeam();
            var team = blTeam.GetTeamById(id);

            team.OnActionSuccess = "loadTeamList";

            return View("EditTeam", team);
        }

        [HttpGet]
        [ActionName("tmm")]
        public ActionResult TeamMemberManagement(int id = -1)
        {
            var blTeam = new BLTeam();
            var team = blTeam.GetTeamById(id);

            return View("TeamMemberManagement",
                new VmTeamMemberManagement
                {
                    TeamId = id,
                    TeamName = team.Name,
                    WrittenReportUrl = team.WrittenReportUrl,
                });
        }

        [HttpPost]
        [ActionName("ats")]
        public PartialViewResult SearchTeam(
            bool draggable,
            string OnItemSelected,
            bool allowDownlaod,
            bool allowEdit,
            bool showSearchBox,
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
            var teamList = bsTeam.GetAdvisorTeams(CurrentUserId, teamName);

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
                    ShowSearchBox = showSearchBox,
                    SearchText = teamName,
                    TeamList = teamList,
                    HtmlControlId = htmlControlId,
                    ParentHtmlControlId = ParentHtmlControlId,
                    OnItemSelected = OnItemSelected,
                    OnItemDragged = onItemDragged
                });
        }
    }
}
