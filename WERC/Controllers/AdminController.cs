using BLL;
using BLL.SystemTools;
using CyberneticCode.Web.Mvc.Helpers;
using Model.ViewModels.Admin;
using Model.ViewModels.Grade;
using Model.ViewModels.Judge;
using Model.ViewModels.Person;
using Model.ViewModels.Task;
using Model.ViewModels.Team;
using Model.ViewModels.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WERC.Filters.ActionFilterAttributes;
using static Model.ApplicationDomainModels.ConstantObjects;

namespace WERC.Controllers
{
    [RoleBaseAuthorize(SystemRoles.Admin, SystemRoles.SafetyAdmin)]
    public class AdminController : BaseController
    {

        BLPerson blPerson = new BLPerson();
        VmPerson person = null;

        // GET: Admin
        public ActionResult test()
        {
           
            return View();
        }
        public ActionResult Index()
        {
            var UserRoles = TempData["UserRoles"] as IEnumerable<string>;

            return View(new VmAdmin() { CurrentUserRoles = UserRoles });
        }

        [HttpGet]
        [ActionName("lupf")]
        public ActionResult LoadUpdateProfileForm()
        {
            var blPerson = new BLPerson();
            var vmPerson = blPerson.GetPersonByUserId(CurrentUserId);

            vmPerson.OnActionSuccess = "loadAdminPanel";

            return View("UpdateProfile", vmPerson);
        }

        [ActionName("arm")]
        public ActionResult ApprovalRejectManagement()
        {
            return View("ApprovalRejectManagement", new VmApprovalRejectManagement());
        }

        [ActionName("prm")]
        public ActionResult PaymentRulesManagement()
        {
            var blParticipantRule = new BLParticipantRule();

            return View("PaymentRulesManagement", new VmPaymentRulesManagement()
            {
                ParticipantRule = blParticipantRule.GetParticipantRule()
            });
        }

        [HttpGet]
        [ActionName("tfim")]
        public ActionResult TeamFullInfoManagement()
        {

            return View("TeamFullInfoManagement", new VmTeamFullInfoManagement());
        }

        [HttpGet]
        [ActionName("tam")]
        public ActionResult TeamActivationManagement()
        {

            return View("TeamActivationManagement", new VmTeamFullInfoManagement());
        }

        [HttpGet]
        [ActionName("ups_tfim")]
        public ActionResult UpdatePayStatusTeamFullInfoManagement()
        {
            var blInvoice = new BLInvoice();

            var invoiceList = blInvoice.GetInvoiceIds(false);

            if (invoiceList != null)
            {
                foreach (var invoiceId in invoiceList.Ids)
                {

                    int? lastOrderId = 0;

                    var blShopCart = new BLShopCart();
                    var lastOrderInfo = blShopCart.GetCheckoutStatus(invoiceId, out lastOrderId);

                    if (lastOrderInfo != null)
                    {
                        //Update all data in one transaction
                        blInvoice.UpdateInvoiceOrderStatus(lastOrderInfo, invoiceId, true, lastOrderId.Value, true, true);
                    }
                }
            }

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

        [HttpGet]
        [ActionName("jtfim")]
        public ActionResult JudgeTaskFullInfoManagement()
        {
            return View("JudgeTaskFullInfoManagement", new VmJudgeTaskFullInfoManagement());
        }

        [HttpPost]
        public async Task<ActionResult> Edit(VmApprovalReject model)
        {
            var result = true;
            var user = UserManager.Users.FirstOrDefault(u => u.Id == model.UserId);
            string returnUrlLink = string.Empty;

            person = blPerson.GetPersonByUserId(model.UserId);


            //returnUrlLink = "/person/up/" + model.UserId;// Update Profile
            returnUrlLink = "";// Update Profile

            var callbackUrl = Url.Action("Login", "Account", new { returnUrl = returnUrlLink }, protocol: Request.Url.Scheme);

            var emailTitle = "29th WERC Environmental Design Contest 2019";

            var body = "<h2>" + emailTitle + "</h2>" +
            "<br/>" +
            "Dear " + person.FirstName + " " + person.LastName + ", " +
            "<br/>" +
            "<br/>" +
            "<h4>" +
            "Your 29th WERC Environmental Design Contest 2019 account approved by the WERC administrator. Please sign in to system by clicking " +
            "<a href=\"" + callbackUrl + "\">here </a><span>or copy link below and paste in the browser: </span>" +
            callbackUrl +
            "</h4>" +
            "<hr/>" +
            "<span>User Name: </span>" + user.UserName +
            "<hr/>" +
            "If you have questions about the WERC Environmental Design Contest online platform, please call 575 - 646 - 8171 or email werc@nmsu.edu.";


            var subject = "2019 WERC Design Contest Account Approval";

            if (model.Approval == (int)Approval.Reject)
            {
                model.EmailConfirmed = false;
                model.LockoutEnabled = true;

                body = "<h1>" + emailTitle + "</h1>" +
                "<br/>" +
                "Dear " + person.FirstName + " " + person.LastName + ", " +
                "<br/>" +
                "<br/>" +
                "<h2>Your account has been rejected by administrator." +
                "<br/><br/><span>User Name: </span>" + user.UserName;
                subject = "Account Has Been Rejected";
            }
            else
                if (model.Approval == (int)Approval.Approve)
            {
                model.EmailConfirmed = true;
                model.LockoutEnabled = false;
            }
            else
                if (model.Approval == (int)Approval.Pending)
            {
                model.EmailConfirmed = false;
                model.LockoutEnabled = false;


                body = "<h1>" + emailTitle + "</h1>" +
                "<br/>" +
                "Dear " + person.FirstName + " " + person.LastName + ", " +
                "<br/>" +
                "<br/>" +
                "<h2>Your Account has been Set to pending to Approval by Administrator." +
                "<br/><br/><span>User Name: </span>" + user.UserName;

                subject = "Pending for Approval Account";
            }

            user.EmailConfirmed = model.EmailConfirmed;
            user.LockoutEnabled = model.LockoutEnabled;

            await UserManager.UpdateAsync(user);

            await UserManager.SendEmailAsync(user.Id, subject, body);

            emailHelper = new EmailHelper()
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                EmailList = new string[] { specialEmail }
            };

            emailHelper.Send();

            var jsonResult = new
            {
                success = result,
                message = "",
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }


        #region Assining tasks to Judge

        [HttpGet]
        [ActionName("attjm")]
        public ActionResult AssignTaskToJudgeManagement()
        {
            var bsTask = new BLTask();
            var bsPerson = new BLPerson();

            var tasks = bsTask.GetAllTask();
            var judges = bsPerson.GetUsersByRoleNames(new string[] { SystemRoles.Judge.ToString() }).Select(r => new SelectListItem
            {
                Value = r.UserId.ToString(),
                Text = r.Name
            }).ToList();

            return View("AssigningTaskToJudge", new VmAssignTaskToJudgeManagement()
            {
                Tasks = tasks,
                Judges = judges
            });
        }
         [HttpGet]
        [ActionName("attlm")]
        public ActionResult AssignTaskToLabManagement()
        {
            var bsTask = new BLTask();
            var bsPerson = new BLPerson();

            var tasks = bsTask.GetAllTask();
            var labs = bsPerson.GetUsersByRoleNames(new string[] { SystemRoles.Lab.ToString() }).Select(r => new SelectListItem
            {
                Value = r.UserId.ToString(),
                Text = r.Name
            }).ToList();

            return View("AssigningTaskToLab", new VmAssignTaskToLabManagement()
            {
                Tasks = tasks,
                Labs = labs
            });
        }

        [HttpPost]
        [ActionName("gjt")]
        public PartialViewResult GetUserTasksByUser(string userId)
        {
            var blUserTask = new BLUserTask();

            var tasks = blUserTask.GetUserTasksByUser(userId);

            return PartialView("_DropedTaskList", new VmTaskCollection
            {
                TaskList = tasks
            });
        }
         [HttpPost]
        [ActionName("glt")]
        public PartialViewResult GetLabUserTasksByUser(string userId)
        {
            var blUserTask = new BLUserTask();

            var tasks = blUserTask.GetUserTasksByUser(userId);

            return PartialView("_DropedTaskList", new VmTaskCollection
            {
                TaskList = tasks
            });
        }

        [HttpPost]
        [ActionName("attj")]
        public ActionResult AssignTasksToJudge(string userId, int[] taskIds)
        {
            var blUserTask = new BLUserTask();

            var result = blUserTask.AssignTasksToUser(userId, taskIds);

            var jsonResult = new
            {
                success = result,
                message = "",
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
          [HttpPost]
        [ActionName("attl")]
        public ActionResult AssignTasksToLab(string userId, int[] taskIds)
        {
            var blUserTask = new BLUserTask();

            var result = blUserTask.AssignTasksToUser(userId, taskIds);

            var jsonResult = new
            {
                success = result,
                message = "",
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        #endregion Assing tasks to Judge


        [HttpGet]
        [ActionName("gl")]
        public ActionResult GradeList(int activeItemId = -1)
        {
            var bsGrade = new BLGrade();

            return View("GradeList", new VmGradeCollection
            {
                HtmlControlId = "Admin_GradeList",
                DataAction = "gs",
                DataController = "admin",
                AllowEdit = true,
                AllowDelete = true,
                ActiveItemId = activeItemId,
                Draggable = false,
                ShowSearchBox = true,
                ParentHtmlControlId = "GradeList_ParentHtmlControlId",
                OnItemSelected = "",
                GradeList = bsGrade.GetAllGrade()
            });
        }

        [HttpPost]
        [ActionName("gs")]
        public PartialViewResult SearchGrade(
            bool draggable,
            string OnItemSelected,
            bool allowEdit,
            bool allowDelete,
            string htmlControlId,
            string dataAction,
            string dataController,
            string ParentHtmlControlId, string onItemDragged,
            string gradeName = "")
        {
            var bsGrade = new BLGrade();
            var gradeList = bsGrade.GetGradeList(gradeName);

            return PartialView("_GradeList",
                new VmGradeCollection
                {
                    DataAction = dataAction,
                    DataController = dataController,
                    AllowEdit = allowEdit,
                    AllowDelete = allowDelete,
                    Draggable = draggable,
                    ShowSearchBox = true,
                    SearchText = gradeName,
                    GradeList = gradeList,
                    HtmlControlId = htmlControlId,
                    ParentHtmlControlId = ParentHtmlControlId,
                    OnItemSelected = OnItemSelected,
                    OnItemDragged = onItemDragged
                });
        }

        [HttpGet]
        [ActionName("tl")]
        public ActionResult TaskList(int activeItemId = -1)
        {
            var bsTask = new BLTask();

            return View("TaskList", new VmTaskCollection
            {
                HtmlControlId = "Admin_TaskList",
                DataAction = "ts",
                DataController = "admin",
                AllowEdit = true,
                AllowDelete = true,
                ActiveItemId = activeItemId,
                Draggable = false,
                ShowSearchBox = false,
                ParentHtmlControlId = "TaskList_ParentHtmlControlId",
                OnItemSelected = "",
                TaskList = bsTask.GetAllTask()
            });
        }

        [HttpGet]
        [ActionName("ltef")]
        public ActionResult LoadTaskEditForm(int id)
        {
            var blTask = new BLTask();
            var task = blTask.GetTaskById(id);
            task.OnActionSuccess = "loadTaskList";

            return View("EditTask", task);
        }

        [HttpGet]
        [ActionName("lgef")]
        public ActionResult LoadGradeEditForm(int id)
        {
            var blGrade = new BLGrade();
            var grade = blGrade.GetGradeWithDetailsById(id);
            grade.OnActionSuccess = "loadGradeList";

            return View("EditGrade", grade);
        }

        [HttpPost]
        [ActionName("ts")]
        public PartialViewResult SearchTask(
            bool draggable,
            string OnItemSelected,
            bool allowEdit,
            bool showSearchBox,
            bool allowDelete,
            string htmlControlId,
            string dataAction,
            string dataController,
            string ParentHtmlControlId, string onItemDragged,
            string taskName = "")
        {
            var bsTask = new BLTask();
            var taskList = bsTask.GetTaskList(taskName);

            return PartialView("_TaskList",
                new VmTaskCollection
                {
                    DataAction = dataAction,
                    DataController = dataController,
                    AllowEdit = allowEdit,
                    AllowDelete = allowDelete,
                    Draggable = draggable,
                    ShowSearchBox = showSearchBox,
                    SearchText = taskName,
                    TaskList = taskList,
                    HtmlControlId = htmlControlId,
                    ParentHtmlControlId = ParentHtmlControlId,
                    OnItemSelected = OnItemSelected,
                    OnItemDragged = onItemDragged
                });
        }

        [HttpPost]
        [ActionName("se")]
        [RoleBaseAuthorize(SystemRoles.Admin, SystemRoles.SafetyAdmin)]
        public ActionResult SendEmail(VmEmail email)
        {
            var result = true;
            var message = "Operation succeeded";

            if (email.AdditionalEmails != null)
            {
                if (email.AdditionalEmails.Length == 1 && email.AdditionalEmails[0] == "")
                {
                    email.AdditionalEmails = null;
                }
            }

            List<string> allEmails = new List<string>();
            if (email.UserIds != null && email.UserIds.Length > 0)
            {
                BLPerson blPerson = new BLPerson();
                var emails = blPerson.GetEmailsByUserIds(email.UserIds);
                allEmails.AddRange(emails);
            }

            if (email.AdditionalEmails != null)
            {
                allEmails.AddRange(email.AdditionalEmails);
            }

            if (allEmails.Count > 0)
            {

                emailHelper = new EmailHelper
                {
                    Subject = email.EmailSubject,
                    Body = email.EmailBody,
                    IsBodyHtml = true,
                    EmailList = allEmails.ToArray()
                };

                result = emailHelper.Send();
            }
            else
            {
                result = false;
                message = "Users not selected";
            }
            var jsonResult = new
            {
                result,
                success = result,
                message,
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        #region Grading Report

        [HttpGet]
        [ActionName("fgrm")]
        public ActionResult LoadFinalGradesReport()
        {
            var blGrade = new BLGrade();
            var gradegReportList = blGrade.GetGradeReportList("");

            return View("FinalGradeReportManagement", new VmFinalGradeReportManagement
            {
                CurrentUserId = CurrentUserId,
                GradeReportList = gradegReportList
            });
        }

        #endregion Grading Report

        #region Languages
        public ActionResult UploadLanguages()
        {
            UpdateLanguage();
            return View(new VmAdmin());
        }

        public string UpdateLanguage()
        {
            BLDBTools.ImportDataFromExcel(Server.MapPath(@"~/Documents/Dictionary/dictionary.xls"));

            return "Succeed";
        }
        #endregion
    }
}