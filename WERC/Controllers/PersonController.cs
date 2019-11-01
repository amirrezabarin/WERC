using BLL;
using CyberneticCode.Web.Mvc.Helpers;
using Model.ViewModels.Admin;
using Model.ViewModels.Person;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Model.ApplicationDomainModels.ConstantObjects;

namespace WERC.Controllers
{
    public class PersonController : BaseController
    {

        [HttpPost]
        [ActionName("gpi")]
        public PartialViewResult GetProfileInfo(string userId)
        {
            var blPerson = new BLPerson();
            var profile = blPerson.GetPersonByUserId(userId);

            //if (profile.RoleName == SystemRoles.Judge.ToString())
            //{
            //    profile.HideEmergency = true;
            //}

            return PartialView("_ProfileInfo", profile);
        }

        [HttpGet]
        [ActionName("gapbf")]
        public JsonResult GetAdvisorPersonMembersByFilter(VmApprovalReject filterItem = null)
        {
            var blPerson = new BLPerson();
            var vmApprovalRejectList = blPerson.GetUsersByFilterAndRoleNames(
                new string[] {
                    SystemRoles.Advisor.ToString(),
                },
                filterItem);

            return Json(vmApprovalRejectList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [ActionName("gjpbf")]
        public JsonResult GetJudgePersonMembersByFilter(VmApprovalReject filterItem = null)
        {
            var blPerson = new BLPerson();
            var vmApprovalRejectList = blPerson.GetUsersByFilterAndRoleNames(
                new string[] {
                    SystemRoles.Judge.ToString(),
                },
                filterItem);

            return Json(vmApprovalRejectList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("grbuebf")]
        public JsonResult GetRoleBaseUserEmailByFilter(VmPerson filter = null)
        {

            var blPerson = new BLPerson();

            var teamFullInfoList = blPerson.GetUsersByFilter(filter);

            return Json(teamFullInfoList, JsonRequestBehavior.AllowGet);
        }

        [ActionName("up")]
        [HttpPost]
        public async Task<ActionResult> UpdateProfile(VmPerson model)
        {
            var result = true;
            var user = UserManager.Users.FirstOrDefault(u => u.Id == model.UserId);

            if (
                string.IsNullOrWhiteSpace(model.FirstName) ||
                string.IsNullOrWhiteSpace(model.LastName) ||
                model.Agreement == null ||
                model.Agreement == false
                )
            {
                result = false;
            }
            else
            {
                model.CurrentUserId = CurrentUserId;

                var blPerson = new BLPerson();

                result = blPerson.UpdatePerson(model);

                if (result != false)
                {
                    user.PhoneNumber = model.PhoneNumber;

                    if (model.Email != null)
                    {
                        user.Email = model.Email;
                    }

                    var blUser = new BLUser();
                    blUser.UpdatePhoneUserNumber(user.Id, model.PhoneNumber);
                    //await UserManager.UpdateAsync(user);
               
                }
            }

            var message = "";

            if (result == false)
            {
                message = model.ActionMessageHandler.Message = "Operation has been failed...\n call system Admin";
            }
            else
            {
                message = model.ActionMessageHandler.Message = "Operation has been succeeded";
            }

            var returnUrl = "";

            if (CurrentUserRoles.Contains(SystemRoles.Admin.ToString()))
            {
                returnUrl = "/admin/index";
            }

            if (CurrentUserRoles.Contains("Advisor"))
            {
                if (user.EmailConfirmed == true)
                {
                    returnUrl = "/advisor/index";
                }
                else
                {
                    returnUrl = "/home/index";

                }
            }

            if (CurrentUserRoles.Contains(SystemRoles.Judge.ToString()))
            {
                if (user.EmailConfirmed == true)
                {
                    returnUrl = "/judge/index";
                }
                else
                {
                    returnUrl = "/home/index";

                }
            }

            if (CurrentUserRoles.Contains(SystemRoles.Student.ToString()))
            {
                returnUrl = "/student/index";
            }

            if (CurrentUserRoles.Contains(SystemRoles.Leader.ToString()))
            {
                returnUrl = "/leader/index";
            }
            if (CurrentUserRoles.Contains(SystemRoles.CoAdvisor.ToString()))
            {
                returnUrl = "/coadvisor/index";
            }

            if (CurrentUserRoles.Contains(SystemRoles.Lab.ToString()))
            {
                if (user.EmailConfirmed == true)
                {
                    returnUrl = "/lab/index";
                }
                else
                {
                    returnUrl = "/home/index";

                }
            }

            var jsonData = new
            {
                personId = model.Id,
                success = result,
                message,
                returnUrl,

            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return View("../Author/PersonEdit", model);
        }

        [ActionName("upp")]
        [HttpPost]
        public ActionResult UploadProfileImage(string oldProfilePictureUrl, HttpPostedFileBase uploadedProfilePicture)
        {
            var result = true;
            var blPerson = new BLPerson();
            string profilePictureUrl = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {

                    Image image = Image.FromStream(uploadedProfilePicture.InputStream);
                    Bitmap bitmap = new Bitmap(image);


                    ResizePicture(ref bitmap);

                    profilePictureUrl = UIHelper.UploadPictureFile(bitmap, uploadedProfilePicture.FileName,
                        uploadedProfilePicture.ContentLength, uploadedProfilePicture.ContentType,
                        "/Resources/Uploaded/Persons/Profile/" + CurrentUserId.Replace("-", ""));


                    result = blPerson.UploadProfileImage(CurrentUserId, profilePictureUrl);

                }
            }
            catch (Exception ex)
            {
                var jsonEx = JsonConvert.SerializeObject(ex, Formatting.Indented,
                               new JsonSerializerSettings
                               {
                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                               });

                var jsonException = new
                {
                    success = false,
                    message = jsonEx

                };

                return Json(jsonException, JsonRequestBehavior.AllowGet);
            }

            //if (result != false && !string.IsNullOrEmpty(profilePictureUrl))
            //{
            //    try
            //    {
            //        //UIHelper.DeleteFile(oldProfilePictureUrl);
            //    }
            //    catch (Exception ex)
            //    {
            //        var jsonEx = JsonConvert.SerializeObject(ex, Formatting.Indented,
            //                       new JsonSerializerSettings
            //                       {
            //                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //                       });

            //        var jsonException = new
            //        {
            //            success = false,
            //            message = jsonEx

            //        };

            //        return Json(jsonException, JsonRequestBehavior.AllowGet);
            //    }
            //}

            var jsonData = new
            {
                profilePictureUrl,
                success = result,
                message = "Your profile picture updated."
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return View("../Author/PersonEdit", model);
        }

        private static ImageCodecInfo GetEncoderInfo(string v)
        {
            throw new NotImplementedException();
        }

        [ActionName("uup")]
        [HttpPost]
        public ActionResult UploadUniversityImage(int universityId, string oldUniversityPictureUrl, HttpPostedFileBase uploadedUniversityPicture)
        {
            var result = true;
            var blUniversity = new BLUniversity();
            string universityPictureUrl = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {

                    Image image = Image.FromStream(uploadedUniversityPicture.InputStream);
                    Bitmap bitmap = new Bitmap(image);

                    ResizePicture(ref bitmap);

                    universityPictureUrl = UIHelper.UploadPictureFile(bitmap, uploadedUniversityPicture.FileName,
                        uploadedUniversityPicture.ContentLength, uploadedUniversityPicture.ContentType,
                        "/Resources/Uploaded/Universities/" + CurrentUserId.Replace("-", ""));

                    result = blUniversity.UploadUniversityImage(universityId, universityPictureUrl);

                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            //if (result != false && !string.IsNullOrEmpty(universityPictureUrl))
            //{
            //    UIHelper.DeleteFile(oldUniversityPictureUrl);
            //}

            var jsonData = new
            {
                universityPictureUrl,
                success = result,
                message = "Your University picture updated."
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return View("../Author/PersonEdit", model);
        }

        [ActionName("luup")]
        [HttpPost]
        public ActionResult LoadUniversityPictureUrl(int universityId)
        {
            var result = true;
            var blUniversity = new BLUniversity();

            var jsonData = new
            {
                universityPictureUrl = blUniversity.GetUniversityPictureUrl(universityId) ?? "",
                success = result,
                message = "Your University picture updated."
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return View("../Author/PersonEdit", model);
        }

        [ActionName("ur")]
        [HttpPost]
        public ActionResult UploadResume(string oldResumeUrl, HttpPostedFileBase UploadedResume)
        {
            var result = true;
            var blPerson = new BLPerson();
            string resumeUrl = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    resumeUrl = UIHelper.UploadFile(UploadedResume, "/Resources/Uploaded/Persons/Resume/" + CurrentUserId.Replace("-", ""));
                    if (string.IsNullOrWhiteSpace(resumeUrl) == false)
                    {
                        result = blPerson.UploadResume(CurrentUserId, resumeUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            //if (result != false && !string.IsNullOrEmpty(resumeUrl))
            //{
            //    UIHelper.DeleteFile(oldResumeUrl);
            //}

            var jsonData = new
            {
                resumeUrl,
                success = result,
                message = "Your resume uploaded."
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return View("../Author/PersonEdit", model);
        }

    }
}