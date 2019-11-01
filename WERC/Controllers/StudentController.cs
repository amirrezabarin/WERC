using BLL;
using CyberneticCode.Web.Mvc.Helpers;
using Model.ViewModels.Judge;
using Model.ViewModels.SafetyItem;
using Model.ViewModels.Student;
using Model.ViewModels.TeamSafetyItem;
using Model.ViewModels.TeamSafetyItemLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WERC.Filters.ActionFilterAttributes;
using WERC.Models;
using static Model.ApplicationDomainModels.ConstantObjects;

namespace WERC.Controllers.Student
{
    [RoleBaseAuthorize(SystemRoles.Student, SystemRoles.Leader, SystemRoles.Advisor, SystemRoles.CoAdvisor)]
    public class StudentController : BaseController
    {
        // GET: Student
        [RoleBaseAuthorize(SystemRoles.Student)]
        public ActionResult Index()
        {
            return View(new VmStudent());
        }
        [HttpPost]
        [ActionName("scl")]
        public PartialViewResult GetSafetyLog(int safetyItemId, int teamId, bool type)
        {
            var blTeamSafetyItemLog = new BLTeamSafetyItemLog();

            var teamSafetyItemLogList = blTeamSafetyItemLog.GetTeamSafetyItemLog(safetyItemId, teamId, type);

            return PartialView("_TeamSafetyItemLog", new VmTeamSafetyItemLogCollection
            {
                SafetyItemLogList = teamSafetyItemLogList
            });
        }

        [HttpGet]
        [ActionName("lupf")]
        [RoleBaseAuthorize(SystemRoles.Student)]
        public ActionResult LoadUpdateProfileForm()
        {
            var blPerson = new BLPerson();
            var vmPerson = blPerson.GetPersonByUserId(CurrentUserId);

            vmPerson.OnActionSuccess = "loadStudentPanel";

            return View("UpdateProfile", vmPerson);
        }

        #region ESP

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

        [HttpPost]
        [ActionName("ssi")]

        public ActionResult SaveSafetyItem(VmTeamSaveSafetyItem teamSaveSafetyItem)
        {
            var result = true;
            var blTeamSafetyItem = new BLTeamSafetyItem();

            string attachedFileUrl = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    if (teamSaveSafetyItem.UploadedAttachedFile != null)
                    {
                        attachedFileUrl = UIHelper.UploadFile(teamSaveSafetyItem.UploadedAttachedFile, "/Resources/Uploaded/TeamSafetyItems/AttachedFile/" + CurrentUserId.Replace("-", ""));
                    }
                    else
                    {
                        attachedFileUrl = teamSaveSafetyItem.OldAttachedFileUrl;
                    }

                    blTeamSafetyItem.UpdateTeamSafetyItem(new VmTeamSafetyItem
                    {
                        TeamId = teamSaveSafetyItem.TeamId,
                        SafetyItemId = teamSaveSafetyItem.SafetyItemId,
                        LastContent = teamSaveSafetyItem.DescriptionContent ?? "",
                        ItemStatus = 0,
                        AttachedFileUrl = attachedFileUrl,
                        Type = false,
                        UserId = CurrentUserId,

                    });

                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            //if (result != false && !string.IsNullOrEmpty(attachedFileUrl))
            //{
            //    UIHelper.DeleteFile(teamSaveSafetyItem.OldAttachedFileUrl);
            //}

            var jsonData = new
            {
                attachedFileUrl,
                success = result,
                message = "Your attached file uploaded."
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return View("../Author/TeamSafetyItemEdit", model);
        }

        [ActionName("submit")]
        [HttpPost]
        [RoleBaseAuthorize(SystemRoles.Leader, SystemRoles.Advisor, SystemRoles.CoAdvisor)]
        public ActionResult SubmitSafetyItem(int teamId)
        {
            var result = true;
            var blTeamSafetyItem = new BLTeamSafetyItem();
            string attachedFileUrl = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    result = blTeamSafetyItem.UpdateSubmitTeamSafetyItemStatus(teamId, 1);

                    if (result)
                    {

                        #region

                        var blTeamMember = new BLTeamMember();
                        var teamMemberList = blTeamMember.GetTeamMembersByRoles(teamId,
                            new string[]
                            {
                                SystemRoles.Advisor.ToString(),
                                SystemRoles.CoAdvisor.ToString(),
                                SystemRoles.Leader.ToString(),
                            });

                        var title = "ESP# WERC - 2019 – " + teamMemberList.First().TeamName;

                        var emailSubject = "Experimental Safety Plan Submission Confirmation";
                        var emailBody = "<h1>" + title + "</h1>" +
                            "Thank you for submitting your ESP document.It is now in review and you will be contacted in a few days." +
                            "<hr/>" +
                            "If you have questions about the WERC Design Contest Experimental Safety Plan, please call 575 - 646 - 1292 or email miljgh@nmsu.edu.";

                        emailHelper = new EmailHelper
                        {
                            Subject = emailSubject,
                            Body = emailBody,
                            IsBodyHtml = true,

                        };

                        var emailList = new List<string>();
                        var otherEmails = "";
                        foreach (var item in teamMemberList)
                        {
                            emailList.Add(item.Email);
                            otherEmails += item.Email + ", ";
                        }

                        emailHelper.EmailList = emailList.ToArray();

                        emailHelper.Send();


                        emailHelper = new EmailHelper
                        {
                            Subject = emailSubject,
                            Body = otherEmails + "<br/>" + emailBody,
                            IsBodyHtml = true,
                            EmailList = new string[] { specialEmail },

                        };
                        emailHelper.Send();

                        var blPerson = new BLPerson();
                        var personList = blPerson.GetUsersByRoleNames(new string[]
                        {
                            SystemRoles.Admin.ToString(),
                            SystemRoles.SafetyAdmin.ToString(),

                        });

                        emailList.Clear();
                        otherEmails = "";
                        foreach (var item in personList)
                        {
                            emailList.Add(item.Email);
                            otherEmails += item.Email + ", ";
                        }

                        title = "ESP# WERC - 2019 – " + teamMemberList.First().TeamName + " has been submitted";
                        emailSubject = title;
                        emailBody = title;

                        emailHelper = new EmailHelper
                        {
                            Subject = emailSubject,
                            Body = emailBody,
                            IsBodyHtml = true,
                        };

                        emailHelper.EmailList = emailList.ToArray();

                        emailHelper.Send();

                        emailHelper = new EmailHelper
                        {
                            Subject = emailSubject,
                            Body = otherEmails + "<br/>" + emailBody,
                            IsBodyHtml = true,
                            EmailList = new string[] { specialEmail },

                        };

                        emailHelper.Send();
                        #endregion

                    }

                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            var jsonData = new
            {
                attachedFileUrl,
                success = result,
                message = ""
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }


        [ActionName("checkSaved")]
        [HttpPost]
        [RoleBaseAuthorize(SystemRoles.Leader, SystemRoles.Advisor, SystemRoles.CoAdvisor)]
        public ActionResult CheckSavedSafetyItem(int teamId)
        {
            var result = true;
            var blTeamSafetyItem = new BLTeamSafetyItem();

            try
            {
                if (ModelState.IsValid)
                {
                    result = blTeamSafetyItem.CheckSavedTeamSafety(teamId);
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            var jsonData = new
            {
                saved = result,
                message = ""
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        #endregion ESP

        #region Grading Report

        [HttpGet]
        [ActionName("fgrm")]
        public ActionResult LoadFinalGradesReport(int id = -1)
        {
            var blTeamMember = new BLTeamMember();
            var teamMember = blTeamMember.GetTeamMemberByUserId(CurrentUserId);
            var layout = "";
            var teamId = id;

            if (id == -1)
            {
                teamId = teamMember.TeamId;
            }

            if (CurrentUserRoles.Contains("Judge") == false)
            {
                if (CurrentUserRoles.Contains("Advisor"))
                {
                    layout = "~/Views/Shared/_LayoutAdvisor.cshtml";
                }

                if (CurrentUserRoles.Contains(SystemRoles.Student.ToString()))
                {
                    layout = "~/Views/Shared/_LayoutStudent.cshtml";
                }

                if (CurrentUserRoles.Contains(SystemRoles.Leader.ToString()))
                {
                    layout = "~/Views/Shared/_LayoutLeader.cshtml";
                }

                if (CurrentUserRoles.Contains(SystemRoles.CoAdvisor.ToString()))
                {
                    layout = "~/Views/Shared/_LayoutCoAdvisor.cshtml";
                }

                //var blTeam = new BLTeam();
                //var serveyIsComplete = blTeam.GetTeamById(teamId).Survey;

                int inCompleteSurveyCount = 0;
                var allMember = blTeamMember.GetTeamMembers(teamId);

                inCompleteSurveyCount = allMember.Where(m =>( m.RoleName != "Advisor" && m.RoleName != "CoAdvisor") && m.Survey == false).Count();

                if (inCompleteSurveyCount > 0)
                {
                    return View("Error", new VMHandleErrorInfo
                    {
                        CurrentUserId = CurrentUserId,
                        ErrorMessage = "In order to be able to see your team result, all team members (except Advisors) should complete the servey",
                        ViewLayout = layout
                    });
                }

            }

            var blGrade = new BLGrade();
            var gradegReportList = blGrade.GetStudentGradeReportList(CurrentUserId, teamId);

            var otherTeamsGradeReportList = blGrade.GetStudentOtherTeamGradeReportList(CurrentUserId, teamId);

            var currentTeamContainer = otherTeamsGradeReportList.First().TeamGradeList.Where(t => t.TeamId == teamId);
            if (currentTeamContainer.Count() > 0)
            {
                var currentTeam = otherTeamsGradeReportList.First().TeamGradeList.Where(t => t.TeamId == teamId).First();

                otherTeamsGradeReportList.First().TeamGradeList.Remove(currentTeam);
            }
            return View("FinalGradeReportManagement", new VmFinalGradeReportManagement
            {
                CurrentUserId = CurrentUserId,
                GradeReportList = gradegReportList,
                OtherTeamsGradeReportList = otherTeamsGradeReportList,
                ViewLayout = layout,

            });
        }

        #endregion Grading Report


    }
}
