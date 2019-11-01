using BLL;
using Model.Base;
using Model.ViewModels.Grade;
using Model.ViewModels.Grade.Grading;
using System;
using System.Linq;
using System.Web.Mvc;
using WERC.Filters.ActionFilterAttributes;
using static Model.ApplicationDomainModels.ConstantObjects;

namespace WERC.Controllers
{
    [RoleBaseAuthorize(SystemRoles.Admin, SystemRoles.Judge)]
    public class GradeController : BaseController
    {
        [ActionName("ggddl")]
        public ActionResult GetGradeDropDownList()
        {
            var bsGrade = new BLGrade();

            var GradeList = bsGrade.GetGradeSelectListItem(0, int.MaxValue);

            return Json(GradeList, JsonRequestBehavior.AllowGet);
        }


        [ActionName("ggbf")]
        [HttpGet]
        public JsonResult GetGradeByFilter(string name = null)
        {
            var blGrade = new BLGrade();

            var teamList = blGrade.GetGradeList(name);
            return Json(teamList, JsonRequestBehavior.AllowGet);
        }

        [ActionName("lgtf")]
        [HttpGet]
        public ActionResult LoadGradingTeamForm()
        {
            var blTeamGradeDetail = new BLTeamGradeDetail();
            var gradingTypeList = blTeamGradeDetail.GetTeamGradeMetaData(CurrentUserId);

            return View("../Judge/OLD_GradingTeamForm", new VmGradingManagement
            {
                CurrentUserId = CurrentUserId,
                GradingTypeList = gradingTypeList
            });
        }

        [ActionName("lmsgtf")]
        [HttpGet]
        public ActionResult LoadManagementSingleTeamGradingForm(int id)
        {
            var bsTeam = new BLTeam();
            var teamInfo = bsTeam.GetTeamById(id);
            var teamName = teamInfo.Name;
            var bsGrade = new BLGrade();

            var GradeList = bsGrade.GetGradeSelectListItem(0, int.MaxValue);

            return View("../Judge/GradingTeamForm", new VmSingleTeamGradingManagement
            {
                CurrentUserId = CurrentUserId,
                GradeList = GradeList,
                TeamId = id,
                TeamName = teamName,
                Task = teamInfo.Task,
                University = teamInfo.University,
            });
        }

        [ActionName("l_s_gtf")]
        [HttpPost]
        public PartialViewResult LoadSingleTeamGradingForm(int teamId, int gradeId)
        {
            var blTeamGradeDetail = new BLTeamGradeDetail();
            var gradingTypeList = blTeamGradeDetail.GetSingleTeamGradeMetaData(CurrentUserId, gradeId, teamId);

            double? gradeTotalScore = null;

            if (gradingTypeList.TeamGrading.GradingDetailList.Count() > 0)
            {
                gradeTotalScore = gradingTypeList.TeamGrading.GradingDetailList.Sum(p => p.Point * p.Coefficient) ?? 0;
            }

            return PartialView("../Judge/_GradingTable", new VmSingleGradingType
            {
                CurrentUserId = CurrentUserId,
                GradeId = gradeId,
                TeamId = teamId,
                TeamName = gradingTypeList.TeamGrading.TeamName,
                TeamGrading = gradingTypeList.TeamGrading,
                TotalScore = blTeamGradeDetail.GetTotalScore(CurrentUserId, teamId),
                GradeTotalScore = gradeTotalScore ?? 0,

            });
        }

        [ActionName("gtswg")]
        [HttpPost]
        public JsonResult GetTotalScoreWithoutGrade(int teamId, int gradeId)
        {
            var blTeamGradeDetail = new BLTeamGradeDetail();

            var totalScoreData = blTeamGradeDetail.GetSingleTeamTotalScoreWithGradeWithoutCurrentJudge(CurrentUserId, gradeId, teamId);
            return Json(totalScoreData, JsonRequestBehavior.AllowGet);

        }
        //[ActionName("lctgf")]
        //[HttpGet]
        //public ActionResult LoadCreateTeamGradeForm(int id)
        //{
        //    var blTeamGradeDetail = new BLTeamGradeDetail();
        //    var teamGradeDetail = blTeamGradeDetail.GetTeamGradeMetaData(id)

        //    return View("../Judge/CreateTeamGrade", teamGradeDetail)
        //}


        [ActionName("old_sg")]
        [HttpPost]
        public ActionResult old_SaveGrading(VmClientGrading[] clientGrading)
        {
            var result = true;
            var blTeamGradeDetail = new BLTeamGradeDetail();

            try
            {
                if (ModelState.IsValid)
                {
                    result = blTeamGradeDetail.UpdateTeamGradeDetail(CurrentUserId, clientGrading);
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

        [ActionName("sg")]
        [HttpPost]
        public ActionResult SaveGrading(VmClientGrading[] clientGrading)
        {
            var result = true;
            var blTeamGradeDetail = new BLTeamGradeDetail();

            try
            {

                result = blTeamGradeDetail.UpdateSingleTeamGradeDetail(CurrentUserId, clientGrading[0].GradeId, clientGrading[0].TeamId, clientGrading);

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

        [ActionName("ctg")]
        [HttpPost]
        public ActionResult CreateTeamGrade(VmTeamGradeDetail model)
        {
            var result = true;
            var blTeamGradeDetail = new BLTeamGradeDetail();

            model.CurrentUserId = CurrentUserId;

            try
            {
                if (ModelState.IsValid)
                {
                    result = blTeamGradeDetail.CreateTeamGradeDetail(model);
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            if (result == true)
            {
                return RedirectToAction("tl", "judge", new { activeItemId = result });
            }

            model.ActionMessageHandler.Message = "Operation has been failed...\n";

            return View("../Jadge/CreateTeamGrade", model);
        }

        [ActionName("lcgf")]
        [HttpGet]
        public ActionResult LoadCreateGradeForm()
        {
            var blPerson = new BLPerson();
            var person = blPerson.GetPersonByUserId(CurrentUserId);

            return View("../Admin/CreateGrade", new VmGrade());
        }

        [ActionName("cg")]
        [HttpPost]
        public ActionResult Create(VmGrade model)
        {
            var result = -1;
            var blGrade = new BLGrade();

            model.CurrentUserId = CurrentUserId;

            try
            {
                if (ModelState.IsValid)
                {
                    result = blGrade.CreateGrade(model);
                }
            }
            catch (Exception ex)
            {
                result = -1;
            }

            if (result != -1)
            {
                return RedirectToAction("gl", "Admin", new { activeItemId = result });
            }

            model.ActionMessageHandler.Message = "Operation has been failed...\n";

            return View("../Admin/CreateGrade", model);
        }

        [ActionName("eg")]
        [HttpPost]
        public ActionResult EditGrade(VmGrade model)
        {
            model.CurrentUserId = CurrentUserId;

            var result = true;
            var blGrade = new BLGrade();

            try
            {
                if (ModelState.IsValid)
                {
                    result = blGrade.UpdateGrade(model);
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            if (result == false)
            {
                model.ActionMessageHandler.Message = "Operation has been failed...\n call system Admin";
            }
            else
            {
                model.ActionMessageHandler.Message = "Operation has been succeeded";
            }

            var jsonData = new
            {
                success = result,
                message = model.ActionMessageHandler.Message
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        [ActionName("dg")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = true;

            var blGrade = new BLGrade();

            result = blGrade.DeleteGrade(id);

            string resultMessage = string.Empty;

            if (result == true)
            {
                resultMessage = new BaseViewModel()["Score Sheet Has been deleted successfuly."];
            }
            else
            {
                resultMessage = new BaseViewModel()["This grade has assigned to task. You can't delete it..."];

            }

            var jsonResult = new
            {
                success = result,
                message = resultMessage,
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

    }
}