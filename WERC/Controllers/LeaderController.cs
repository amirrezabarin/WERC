using BLL;
using Model.ViewModels.Leader;
using Model.ViewModels.Team;
using Model.ViewModels.TeamSafetyItem;
using System.Web.Mvc;
using WERC.Filters.ActionFilterAttributes;
using static Model.ApplicationDomainModels.ConstantObjects;

namespace WERC.Controllers.Leader
{
    [RoleBaseAuthorize(SystemRoles.Leader)]
    public class LeaderController : BaseController
    {
        // GET: Leader
        public ActionResult Index()
        {
            return View(new VmLeader());
        }

        [HttpGet]
        [ActionName("gesp")]
        public ActionResult GetESP()
        {
            var blTeamMember = new BLTeamMember();
            var teamId = blTeamMember.GetTeamMemberByUserId(CurrentUserId).TeamId;
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

            return View("../Leader/TeamList", new VmTeamCollection
            {
                HtmlControlId = "Leader_TeamList",
                DataAction = "ats",
                DataController = "Leader",
                AllowDownlaod = true,
                AllowEdit = true,
                AllowDelete = true,
                ActiveItemId = activeItemId,
                Draggable = false,
                ShowSearchBox = false,
                ParentHtmlControlId = "TeamList_ParentHtmlControlId",
                OnItemSelected = "",
                TeamList = bsTeam.GetTeamsByLeader(CurrentUserId)
            });
        }

        [HttpGet]
        [ActionName("tmm")]
        public ActionResult TeamMemberManagement()
        {

            var blTeam = new BLTeam();
            int id = blTeam.GetLeaderTeam(CurrentUserId);

            var team = blTeam.GetTeamById(id);

            return View("../Leader/TeamMemberManagement",
                new VmTeamMemberManagement
                {
                    TeamId = id,
                    TeamName = team.Name,
                    WrittenReportUrl = team.WrittenReportUrl,
                    CurrentUserId = CurrentUserId,
                });

        }
        [HttpGet]
        [ActionName("lupf")]
        public ActionResult LoadUpdateProfileForm()
        {
            var blPerson = new BLPerson();
            var vmPerson = blPerson.GetPersonByUserId(CurrentUserId);

            vmPerson.OnActionSuccess = "loadLeaderPanel";

            return View("UpdateProfile", vmPerson);
        }

    }
}
