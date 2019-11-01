using BLL;
using CyberneticCode.Web.Mvc.Helpers;
using Model.ViewModels.Admin;
using Model.ViewModels.Person;
using Model.ViewModels.Reference;
using Model.ViewModels.SafetyItem;
using Model.ViewModels.Team;
using Model.ViewModels.TeamSafetyItem;
using Model.ViewModels.TeamSafetyItemLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WERC.Filters.ActionFilterAttributes;
using static Model.ApplicationDomainModels.ConstantObjects;

namespace WERC.Controllers
{
    [RoleBaseAuthorize(SystemRoles.SafetyAdmin, SystemRoles.Admin)]
    public class SafetyAdminController : BaseController
    {

        BLPerson blPerson = new BLPerson();
        VmPerson person = null;

        // GET: Admin
        public ActionResult Index()
        {
            var UserRoles = TempData["UserRoles"] as IEnumerable<string>;

            return View(new VmAdmin() { CurrentUserRoles = UserRoles });
        }

        #region admin menus

        [HttpGet]
        [ActionName("tfim")]
        public ActionResult TeamFullInfoManagement()
        {
            return View("TeamFullInfoManagement", new VmTeamFullInfoManagement());
        }

        [HttpGet]
        [ActionName("tem")]
        public ActionResult TeamEmailManagement()
        {
            var blTeamMember = new BLTeamMember();

            var memberUserIds = blTeamMember.GetAllTeamMembersUserIds();

            return View("TeamEmailManagement", new VmTeamFullInfoManagement()
            {
                MemberUserIds = memberUserIds
            });
        }

        [HttpGet]
        [ActionName("ruem")]
        public ActionResult RoleBaseUserEmailManagement()
        {
            return View("RoleBaseUserEmailManagement", new VmRoleBaseUserEmailManagement());
        }

        #endregion

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
        public ActionResult LoadUpdateProfileForm()
        {
            var blPerson = new BLPerson();
            var vmPerson = blPerson.GetPersonByUserId(CurrentUserId);

            vmPerson.OnActionSuccess = "loadSafetyAdminPanel";

            return View("UpdateProfile", vmPerson);
        }

        #region reference
        [HttpPost]
        [ActionName("urf")]
        public ActionResult UploadReferenceFile(HttpPostedFileBase uploadedReferenceFile, string title)
        {
            var result = true;
            var blReference = new BLReference();
            var referenceFileUrl = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (uploadedReferenceFile != null)
                    {
                        referenceFileUrl = UIHelper.UploadFile(uploadedReferenceFile, "/Resources/Uploaded/TeamSafetyItems/ReferenceFile/" + CurrentUserId.Replace("-", ""));
                    }

                    blReference.CreateReference(
                            new VmReference
                            {
                                ReferenceFileUrl = referenceFileUrl,
                                Title = title,

                            });
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            var jsonData = new
            {

                success = result,
                message = "Your reference file uploaded."
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return View("../Author/TeamSafetyItemEdit", model);
        }

        [HttpGet]
        [ActionName("larf")]
        public ActionResult LoadAddReferenceForm()
        {
            var blReference = new BLReference();

            return View("ReferenceUpload", blReference.GetAllReference());
        }

        [HttpPost]
        [ActionName("drf")]
        public ActionResult DeleteReferenceFile(int id)
        {
            var blReference = new BLReference();

            var reference = blReference.GetReferenceById(id);

            //if (reference != null)
            //{
            //    UIHelper.DeleteFile(reference.ReferenceFileUrl);
            //}

            blReference.DeleteReference(id);

            return View("ReferenceUpload", blReference.GetAllReference());
        }
        #endregion reference


        [HttpGet]
        [ActionName("sa_tfim")]
        public ActionResult SafetyAdminTeamFullInfoManagement()
        {

            return View("SafetyAdminTeamFullInfoManagement", new VmTeamFullInfoManagement());
        }

        [HttpGet]
        [ActionName("sa_tfim_esp_r")]
        public ActionResult SafetyAdminTeamFullInfoESPReportManagement()
        {

            return View("SafetyAdminTeamFullInfoESPReportManagement", new VmTeamFullInfoManagement());
        }

        [HttpGet]
        [ActionName("gesp")]
        public ActionResult GetESP(int teamId)
        {

            var blTeam = new BLTeam();

            var teamFullInfoList = blTeam.GetTeamFullInfoByFilter(new VmTeamFullInfo()).ToList();

            var teamName = teamFullInfoList.Where(t => t.Id == teamId).Select(t => t.Name).First();
            var taskName = teamFullInfoList.Where(t => t.Id == teamId).Select(t => t.TaskName).First();
            var facultyAdvisor = teamFullInfoList.Where(t => t.Id == teamId).Select(t => t.Advisor).First();
            var university = teamFullInfoList.Where(t => t.Id == teamId).Select(t => t.University).First();

            var blReference = new BLReference();

            var blTeamSafetyItem = new BLTeamSafetyItem();
            var vmTeamSafetyItemList = blTeamSafetyItem.GetSafetyAdminTeamSafetyItemByTeamId(teamId);

            return View("SafetyAdminExperimentalSafetyPlan",
                new VmTeamSafetyItemCollection
                {
                    TeamSafetyItemList = vmTeamSafetyItemList,
                    ReferenceFiles = blReference.GetAllReference(),
                    TeamName = teamName,
                    TaskName = taskName,
                    Advisor = facultyAdvisor,
                    University = university,
                });
        }

        [HttpGet]
        [ActionName("gesprp")]
        public ActionResult GetESPReportPage(int teamId)
        {

            var blTeam = new BLTeam();

            var teamFullInfoList = blTeam.GetTeamFullInfoByFilter(new VmTeamFullInfo()).ToList();

            var teamName = teamFullInfoList.Where(t => t.Id == teamId).Select(t => t.Name).First();
            var taskName = teamFullInfoList.Where(t => t.Id == teamId).Select(t => t.TaskName).First();
            var facultyAdvisor = teamFullInfoList.Where(t => t.Id == teamId).Select(t => t.Advisor).First();
            var university = teamFullInfoList.Where(t => t.Id == teamId).Select(t => t.University).First();

            var blTeamMember = new BLTeamMember();
            var teamMemberList = blTeamMember.GetTeamMembers(teamId);

            return View("SafetyAdminESPReportPage",
                new VmTeamSafetyItemCollection
                {
                    TeamMemberList = teamMemberList,
                    TeamName = teamName,
                    TaskName = taskName,
                    Advisor = facultyAdvisor,
                    University = university,

                });
        }

        [ActionName("ssi")]
        [HttpPost]
        public ActionResult SaveSaftyItem(VmSaveSafetyItemAdmin saveSafetyItemAdmin)
        {
            var result = true;
            var blTeamSafetyItem = new BLTeamSafetyItem();

            try
            {
                if (ModelState.IsValid)
                {

                    result = blTeamSafetyItem.UpdateTeamSafetyItemStatusAndComment(new VmTeamSafetyItem
                    {
                        TeamId = saveSafetyItemAdmin.TeamId,
                        SafetyItemId = saveSafetyItemAdmin.SafetyItemId,
                        LastComment = saveSafetyItemAdmin.Comment ?? "",
                        LastContent = saveSafetyItemAdmin.LastContent ?? "",
                        ItemStatus = saveSafetyItemAdmin.ItemStatus,
                        Type = true,
                        UserId = CurrentUserId,
                        AttachedFileUrl = saveSafetyItemAdmin.AttachedFileUrl,

                    });


                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            var jsonData = new
            {
                success = result,
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        [ActionName("espa")]
        [HttpPost]
        public ActionResult ESPApproveSaftyItem(int teamId, int itemStatus)
        {
            var result = true;
            var blTeamSafetyItem = new BLTeamSafetyItem();
            var allowInReview = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var blTeamMember = new BLTeamMember();
                    if (result)
                    {

                        #region

                        if (itemStatus == 2) // In review
                        {
                            var inReviewTeamSafetyItemList = blTeamSafetyItem.GetApproveAllTeamSafetyIteam(teamId);

                            foreach (var item in inReviewTeamSafetyItemList)
                            {
                                if (item.ItemStatus == 2)
                                {
                                    allowInReview = true;
                                }
                            }
                        }

                        if (itemStatus == 2 && allowInReview == false)
                        {
                            var jsonDataInreview = new
                            {
                                success = result,
                                allowInReview,
                            };

                            return Json(jsonDataInreview, JsonRequestBehavior.AllowGet);
                        }

                        var blTeam = new BLTeam();
                        if (itemStatus == 3) // approve
                        {
                            var approvalTeamSafetyItemList = blTeamSafetyItem.GetApproveAllTeamSafetyIteam(teamId);

                            foreach (var saveSafetyItemAdmin in approvalTeamSafetyItemList)
                            {
                                blTeamSafetyItem.UpdateTeamSafetyItemStatusAndComment(new VmTeamSafetyItem
                                {
                                    TeamId = saveSafetyItemAdmin.TeamId,
                                    SafetyItemId = saveSafetyItemAdmin.SafetyItemId,
                                    LastComment = saveSafetyItemAdmin.LastComment ?? "",
                                    LastContent = saveSafetyItemAdmin.LastContent ?? "",
                                    ItemStatus = 3,
                                    Type = true,
                                    UserId = CurrentUserId,
                                    AttachedFileUrl = saveSafetyItemAdmin.AttachedFileUrl,

                                });
                            }
                        }

                        var teamMemberList = blTeamMember.GetTeamMembersByRoles(teamId,
                            new string[]
                            {
                                    SystemRoles.Advisor.ToString(),
                                    SystemRoles.CoAdvisor.ToString(),
                                    SystemRoles.Leader.ToString(),
                                    SystemRoles.Student.ToString(),
                            });

                        var title = "ESP# WERC - 2019 – " + teamMemberList.First().TeamName;

                        var emailSubject = "An in review Comment For Your WERC 2019 ESP";

                        var emailBody = "<h1>" + title + "</h1>" +
                            "You ESP has been reviewed in the WERC Design Contest System. It is included questions, comments and/or requests for changes regarding the safety of your experiment." +
                            "<br/>" +
                            "Please login to the WERC Design Contest System and respond to these in an understanding and timely manner." +
                            "<hr/>" +
                            "If you have questions about the WERC Design Contest Experimental Safety Plan, please call 575 - 646 - 1292 or email miljgh@nmsu.edu.";

                        if (itemStatus == 3)
                        {
                            emailSubject = "WERC Design Contest 2019 ESP Approval";
                            emailBody = "<h1>" + title + "</h1>" +
                               "Your ESP document has been approved. The final phase of approval will happen at the event when myself, or one of my safety team, " +
                               "will compare this document with your bench scale setup, including any chemicals and materials have on hand." +
                               "If you foresee any changes before you arrive at the event, please request a revision to your ESP document so it can be re-approved. " +
                               "After your bench scale setup is approved, you will be issued a run permit and be allowed to collect any starting water/samples needed for your work. " +
                               "Please remember that the bench scale area at this event is considered to be a laboratory area.  And, as such, everyone will be required to wear safety glasses, " +
                               "long pants or leg coverings, and close toe shoes at all times.  Thank you for your understanding with this process." +
                               "<hr/>" +
                               "If you have questions about the WERC Design Contest Experimental Safety Plan, please call 575 - 646 - 1292 or email miljgh@nmsu.edu.";

                        }

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
                            Body = otherEmails + "<br>" + emailBody,
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
                success = result,
                allowInReview,
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
    }
}