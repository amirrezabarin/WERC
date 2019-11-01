using BLL;
using Model.ViewModels.CoAdvisor;
using Model.ViewModels.Team;
using Model.ViewModels.TeamSafetyItem;
using System.Web.Mvc;
using WERC.Filters.ActionFilterAttributes;
using static Model.ApplicationDomainModels.ConstantObjects;

namespace WERC.Controllers.CoAdvisor
{
    [RoleBaseAuthorize(SystemRoles.CoAdvisor)]
    public class CoAdvisorController : BaseController
    {
        // GET: CoAdvisor
        public ActionResult Index()
        {
            return View(new VmCoAdvisor());
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

            return View("../CoAdvisor/TeamList", new VmTeamCollection
            {
                HtmlControlId = "CoAdvisor_TeamList",
                DataAction = "ats",
                DataController = "CoAdvisor",
                AllowDownlaod = true,
                AllowEdit = true,
                AllowDelete = true,
                ActiveItemId = activeItemId,
                Draggable = false,
                ShowSearchBox = false,
                ParentHtmlControlId = "TeamList_ParentHtmlControlId",
                OnItemSelected = "",
                TeamList = bsTeam.GetTeamsByCoAdvisor(CurrentUserId)
            });
        }

        [HttpGet]
        [ActionName("tmm")]
        public ActionResult TeamMemberManagement()
        {

            var blTeam = new BLTeam();
            int id = blTeam.GetCoAdvisorTeam(CurrentUserId);

            var team = blTeam.GetTeamById(id);

            return View("../CoAdvisor/TeamMemberManagement",
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

            vmPerson.OnActionSuccess = "loadCoAdvisorPanel";

            return View("UpdateProfile", vmPerson);
        }

    }
}
