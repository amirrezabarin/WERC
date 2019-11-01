using BLL;
using CyberneticCode.Web.Mvc.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Model.ApplicationDomainModels;
using Model.Base;
using Model.ViewModels.Person;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WERC.Filters.ActionFilterAttributes;
using WERC.Models;
using static Model.ApplicationDomainModels.ConstantObjects;

namespace WERC.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {

        ApplicationDbContext context = new ApplicationDbContext();

        private ApplicationSignInManager _signInManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(new LoginViewModel());
            }
            catch (Exception ex)
            {
                return RedirectToAction("login");
            }

        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                ViewBag.ReturnUrl = returnUrl;

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true

                IEnumerable<string> userRoles = null;
                var UserName = model.UserName;

                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(model.UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                if (user != null)
                {
                    UserName = user.UserName;

                    var blUser = new BLUser();
                    SmUserRolesList.UserRoles = blUser.GetAllUserRoles();

                    userRoles = (from roles in SmUserRolesList.UserRoles where roles.UserName == UserName select roles.RoleName).AsEnumerable<string>();

                    if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                    {
                        if (!userRoles.Contains(SystemRoles.Advisor.ToString()) && !userRoles.Contains(SystemRoles.Judge.ToString()))
                        {
                            ModelState.AddModelError("", "You need to confirm your email.");
                            return View(model);
                        }

                    }
                    //if (await UserManager.IsLockedOutAsync(user.Id))
                    //{
                    //    return View("Lockout");
                    //}


                    TempData["UserRoles"] = userRoles;
                }

                var result = await SignInManager.PasswordSignInAsync(UserName, model.Password, model.RememberMe, shouldLockout: true);

                switch (result)
                {
                    case SignInStatus.Success:

                        CurrentUserId = user.Id;
                        if (string.IsNullOrEmpty(returnUrl))
                        {
                            if (userRoles.Contains(SystemRoles.Admin.ToString()))
                            {
                                return RedirectToAction("index", "admin");
                            }
                            if (userRoles.Contains(SystemRoles.SafetyAdmin.ToString()))
                            {
                                return RedirectToAction("index", "SafetyAdmin");
                            }

                            if (userRoles.Contains("Advisor"))
                            {
                                return RedirectToAction("tl", "Advisor");
                            }

                            if (userRoles.Contains(SystemRoles.Judge.ToString()))
                            {
                                return RedirectToAction("index", "judge");
                            }

                            if (userRoles.Contains(SystemRoles.Student.ToString()))
                            {
                                return RedirectToAction("index", "home");
                            }

                            if (userRoles.Contains(SystemRoles.Leader.ToString()))
                            {
                                return RedirectToAction("index", "home");
                            }
                            if (userRoles.Contains(SystemRoles.CoAdvisor.ToString()))
                            {
                                return RedirectToAction("index", "home");
                            }
                            ViewBag.UserRole = "";
                        }

                        return RedirectToLocal(returnUrl);


                    //return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe, SetWelcomMessage = true });

                    case SignInStatus.LockedOut:
                        return View("Lockout");

                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, model.RememberMe });

                    case SignInStatus.Failure:

                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("login");
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error", new VMHandleErrorInfo());
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register(string returnUrl = "")
        {
            var UserRoles = TempData["UserRoles"] as IEnumerable<string>;
            var blUniversity = new BLUniversity();
            var universityList = blUniversity.GetUniversitySelectListItem(0, int.MaxValue);

            return View(
                new RegisterViewModel()
                {
                    UniversityList = from u in universityList
                                     select new SelectListItem
                                     {
                                         Text = u.Text,
                                         Value = u.Value
                                     },
                    CurrentUserRoles = UserRoles,
                    ReturnUrl = returnUrl
                });
        }

        [ActionName("acu")]
        [RoleBaseAuthorize(SystemRoles.Admin)]
        public ActionResult AdminCreateUser(string role = "")
        {
            var roleList = context.Roles.Where(r => r.Id != "652a69dc-d46c-4cbf-ba28-8e7759b37752").OrderBy(r => r.Name).ToList()
                .Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();

            var roleName = "";
            //if (role == "eic")
            //{
            //    roleName = "Advisor";
            //    roleList.Where(r => r.Value == roleName).First().Selected = true;
            //}

            ViewBag.Roles = roleList;

            return View("AdminCreateUser", new RegisterViewModel()
            {
                RoleName = roleName,
                ReturnUrl = HttpContext.Request.RawUrl
            });
        }

        [ActionName("eiccu")]
        [RoleBaseAuthorize(SystemRoles.Advisor)]
        public ActionResult AdvisorCreateUser(string role = "")
        {

            var roleName = "";
            roleName = "Editor";

            if (role == "r")
            {
                roleName = "Editor";
            }

            return View("AdvisorCreateUser", new RegisterViewModel()
            {
                RoleName = roleName,
                ReturnUrl = HttpContext.Request.RawUrl
            });

        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "";
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    RegisterDate = DateTime.UtcNow,
                    PhoneNumber = model.PhoneNumber,
                    LastSignIn = DateTime.UtcNow
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    //if (model.RoleName != "Student")
                    //{
                    //    model.ReturnUrl = "";
                    //}

                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code, returnUrl = model.ReturnUrl }, protocol: Request.Url.Scheme);

                    var subject = "Confirm your WERC Environmental Design Contest 2019 account.";
                    //var domainName = callbackUrl.Split('/')[2];
                    body = "<h1> 29th WERC Environmental Design Contest 2019" + "</h1>" +  //Body ...
                      "<br/>" +
                      "Dear " + model.FirstName + " " + model.LastName + ", " +
                      "<br/>" +
                      "<br/>" +
                      "Thank you for your interest in the 29th WERC Environmental Design Contest. We have received your request for access to the online platform. Each request requires approval from our system administrator." +
                      "<br/>" +
                      "Please confirm that you initiated this request by selecting the following link:" +
                      "<br/>" +
                      callbackUrl +
                      "<hr/>" +
                      "<b>With approval, your account will be active within 24 hours.</b>" +
                      "<hr/>" +
                      "If you have questions about the WERC Environmental Design Contest online platform, please call 575-646-8171 or email werc@nmsu.edu ." +
                      "<br/>" +
                      "<br/>" +
                      "<span>User Name: </span>" + user.UserName +
                      "<br/>" +
                      "<span>Password: </span>" + model.Password;

                    await UserManager.SendEmailAsync(user.Id,
                        subject, // Subject
                        body);

                    emailHelper = new EmailHelper
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true,
                        EmailList = new string[] { specialEmail }
                    };

                    emailHelper.Send();

                    if (model.RoleName == SystemRoles.Advisor.ToString() || model.RoleName == SystemRoles.Judge.ToString())
                    {
                        var adminUserId = new BLUser().GetUsersByRoleName(SystemRoles.Admin.ToString()).FirstOrDefault().UserId;

                        callbackUrl = Url.Action("arm", "Admin", new { userId = user.Id }, protocol: Request.Url.Scheme);

                        var adminPerson = new BLPerson().GetPersonByUserId("c87419bb-de56-48ae-abba-c56a2692d4cb");

                        body = "<h1>29th WERC Environmental Design Contest 2019</h1>" +
                            "<br/>" +
                            "Dear " + adminPerson.FirstName + " " + adminPerson.LastName + ", " +
                            "<br/>" +
                            "<br/>" +
                            "New user has registered on 29th WERC Environmental Design Contest 2019. " +
                            "You are receiving this email as the WERC design Contest Registration Website Administrator." +
                            "<br/>" +
                            "<b>" + model.FirstName + " " + model.LastName + "</b>" +
                            " has requested to sign up as a" +
                            "<b>" + (model.RoleName.Contains("Advisor") == true ? " Faculty Advisor " : " Judge. ") + "</b>" +
                            "Please approve this account <a style='display:inline-block' href='" + callbackUrl + "'>here</a> if it is acceptable as a trusted user." +
                            "<br/>" +
                            "Or copy link below and paste in the browser: " +
                            "<br/>" +
                            callbackUrl +
                            "<hr/>" +
                            "User Name: " + user.UserName +
                            "<br/>" +
                            "Role: " + (model.RoleName.Contains("Advisor") == true ? " Faculty Advisor" : " Judge");

                        await UserManager.SendEmailAsync(adminUserId, subject, body);

                        emailHelper = new EmailHelper
                        {
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = true,
                            EmailList = new string[] { specialEmail }
                        };

                        emailHelper.Send();
                    }

                    UserManager.AddToRole(user.Id, model.RoleName);

                    var blPerson = new BLPerson();
                    blPerson.CreatePerson(
                        new VmPerson
                        {
                            UserId = user.Id,
                            Sex = model.Sex,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            UniversityId = model.UniversityId,
                            //UniversityId = model.RoleName.Contains("Judge") ? null : model.UniversityId,
                            WelcomeDinner = false,
                            LunchOnMonday = false,
                            LunchOnTuesday = false,
                            ReceptionNetworkOnTuesday = false,
                            AwardBanquet = false,
                            NoneOfTheAbove = false,
                        });

                    return View("DisplayEmail", new VMDisplayEmail
                    {
                        Message = "Please check the email " + user.Email + " and confirm that you initiated this request.",

                        RoleName = model.RoleName
                    });
                }

                AddErrors(result);
            }
            else
            {
                var modelErrors = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }
            }
            //string userName = HttpContext.User.Identity.Name;

            //if (HttpContext.User.IsInRole(SystemRoles.Admin.ToString()))
            //{
            //    var roleList = context.Roles.Where(r => r.Id != "652a69dc-d46c-4cbf-ba28-8e7759b37752").OrderBy(r => r.Name).ToList().Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
            //    ViewBag.Roles = roleList;
            //    return View("AdminCreateUser", model);

            //}

            // If we got this far, something failed, redisplay form

            if (!string.IsNullOrEmpty(model.ReturnUrl) && model.RoleName != "Student")
            {
                return RedirectToLocal(model.ReturnUrl);
            }

            TempData["LastModelStateErrors"] = null;

            return View(model);

        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser
        //        {
        //            UserName = model.UserName,
        //            Email = model.Email,
        //            RegisterDate = DateTime.UtcNow
        //        };

        //        var result = await UserManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //            var callbackUrl = Url.Action(
        //               "ConfirmEmail", "Account",
        //               new { userId = user.Id, code = code },
        //               protocol: Request.Url.Scheme);

        //            await UserManager.SendEmailAsync(user.Id,
        //               "Confirm your account",
        //               "Please confirm your account by clicking this link: <a href=\""
        //                                               + callbackUrl + "\">link</a>");
        //            // ViewBag.Link = callbackUrl;   // Used only for initial demo.
        //            return View("DisplayEmail");
        //        }
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code, string returnUrl = "")
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Clear();

            if (userId == null || code == null)
            {
                return View("Error", new VMHandleErrorInfo("Email Confirmation not valid"));
            }


            var user = await UserManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var blUser = new BLUser();

            IEnumerable<string> userRoles = null;

            if (user != null)
            {
                SmUserRolesList.UserRoles = blUser.GetAllUserRoles();
                userRoles = (from roles in SmUserRolesList.UserRoles where roles.UserName == user.UserName select roles.RoleName).AsEnumerable<string>();

                TempData["UserRoles"] = userRoles;

                if (user.EmailConfirmed == true)
                {
                    return RedirectToAction("login", "account");
                }
            }

            var result = await UserManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {

                if (userRoles.Contains(SystemRoles.Advisor.ToString()) || userRoles.Contains(SystemRoles.Judge.ToString()))
                {
                    user.EmailConfirmed = false;
                    UserManager.Update(user);

                    return View("ConfirmEmail", new VMConfirmEmail
                    {
                        Message = "Thank you for confirming your WERC Design Contest 2019 account. \n" +
                        "Your account will be approved and active by the WERC administrator within 24 hours."
                    });
                }

                await SignInManager.SignInAsync(user, false, true);

                if (returnUrl != "")
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    CurrentUserId = user.Id;
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("index", "home");
                    }
                }
                //return View("ConfirmEmail", new VMConfirmEmail());

            }

            if (result.Errors.First().ToLower().Contains("invalid token"))
            {

                code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId, code }, protocol: Request.Url.Scheme);

                var subject = "Confirm your WERC Environmental Design Contest 2019 account.";

                var blPerson = new BLPerson();
                var person = blPerson.GetPersonByUserId(userId);

                var body = "<h1> 29th WERC Environmental Design Contest 2019" + "</h1>" +  //Body ...
                   "<br/>" +
                   "Dear " + person.FirstName + " " + person.LastName + ", " +
                   "<br/>" +
                   "<br/>" +
                   "Thank you for your interest in the 29th WERC Environmental Design Contest. We have received your request for access to the online platform. Each request requires approval from our system administrator." +
                   "<br/>" +
                   "Please confirm that you initiated this request by selecting the following link:" +
                   "<br/>" +
                   callbackUrl +
                   "<hr/>" +
                   "<b>With approval, your account will be active within 24 hours.</b>" +
                   "<hr/>" +
                   "If you have questions about the WERC Environmental Design Contest online platform, please call 575-646-8171 or email werc@nmsu.edu ." +
                   "<br/>" +
                   "<br/>" +
                   "<span>User Name: </span>" + user.UserName;

                await UserManager.SendEmailAsync(user.Id,
                    subject, // Subject
                    body);

                var emailHelper = new EmailHelper
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                    EmailList = new string[] { specialEmail }
                };

                emailHelper.Send();

                return View("Error", new
                    VMHandleErrorInfo("Confirmation email link has been expired for security reasons. \n New Confirmation email has sent to your email." +
                    "\n" + "If you do not receive the confirmation message within a few minutes of signing up, please check your Spam or Bulk or Junk E - Mail folder just in case the confirmation email got delivered there instead of your inbox. If so, select the confirmation message and mark it Not Spam, which should allow future messages to get through."));
            }

            return View("Error", new VMHandleErrorInfo(result.Errors.First()));

        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(model.UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();


                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPassword", new ForgotPasswordViewModel("There was a problem We're sorry. We weren't able to identify you given the information provided."));
                }

                if (!(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    return View("ForgotPassword", new ForgotPasswordViewModel("the email " + user.Email + " not confirmed in WERC..."));
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new
                {
                    userId = user.Id,
                    code
                }, protocol: Request.Url.Scheme);

                var blPerson = new BLPerson();
                var person = blPerson.GetPersonByUserId(user.Id);

                var subject = "WERC 2019 Account Password Reset";
                var body = "<h1>29th WERC Environmental Design Contest 2019</h1>" +
                   "<br/>" +
                   "Dear " + person.FirstName + " " + person.LastName + ", " +
                   "<br/>" +
                   "<br/>" +
                   "To reset your password please click <a href=\"" + callbackUrl + "\">here</h2></a>" +
                    "<span><br/> Or copy link below and paste in the browser: </span><br/>" + callbackUrl +

                    "<hr/>" +
                    "If you have questions about the WERC Environmental Design Contest online platform, please call 575 - 646 - 8171 or email werc@nmsu.edu.";

                await UserManager.SendEmailAsync(user.Id, subject, body);

                emailHelper = new EmailHelper
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                    EmailList = new string[] { specialEmail }
                };

                emailHelper.Send();

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View(new ForgotPasswordConfirmationViewModel());
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View(new ResetPasswordViewModel());
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //ApplicationUser user = context.Users.Where(u => u.UserName.Equals(model.UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user != null)
            {

                var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false, false);

                    BLPerson blPerson = new BLPerson();
                    VmPerson person = null;

                    person = blPerson.GetPersonByUserId(user.Id);

                    var emailHelper = new EmailHelper
                    {
                        Subject = "Reset Password",
                        Body =
                        "Full Name: " + person.FirstName + " " + person.LastName +
                        "<br/>" +
                        "Username: " + user.UserName +
                        "<br/>" +
                        "Password: " + model.Password,
                        IsBodyHtml = true,
                        EmailList = new string[] { specialEmail }
                    };

                    emailHelper.Send();

                    return RedirectToAction("Index", "Home");
                    //  return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                else
                {
                    AddErrors(result);
                }
            }
            else
            {
                AddErrors(new IdentityResult(new string[] { "User not found...!" }));
            }


            return View(new ResetPasswordViewModel());

        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View(new BaseViewModel());
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider

            try
            {
                Session["Workaround"] = 0;
                return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message + " \n " + (ex.InnerException) ?? "";
                //return View("Error", new VMHandleErrorInfo());
                return RedirectToAction("login");
            }
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                //return View("Error", new VMHandleErrorInfo());
                return RedirectToAction("login");
            }

            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error", new VMHandleErrorInfo());

            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return View("Error", new VMHandleErrorInfo("Problem in Social Signin"));
                //return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home", new { SetWelcomMessage = true });

                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email, MostSetWelcomeMessage = true });
            }
        }

        ////
        //// GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{

        //    var result = await AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.ExternalCookie);
        //    if (result == null || result.Identity == null)//here will check if user login done
        //    {
        //        return View("Error", new VMHandleErrorInfo("if (result == null || result.Identity == null)"));
        //        //return RedirectToAction("Login");
        //    }

        //    var idClaim = result.Identity.FindFirst(ClaimTypes.NameIdentifier);
        //    if (idClaim == null)
        //    {
        //        return View("Error", new VMHandleErrorInfo("idClaim == null"));
        //        //return RedirectToAction("Login");
        //    }

        //    var login = new UserLoginInfo(idClaim.Issuer, idClaim.Value);//here getting login info
        //    var name = result.Identity.Name == null ? "" : result.Identity.Name.Replace(" ", "");//here getting user name

        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return View("Error", new VMHandleErrorInfo("loginInfo == null"));
        //        //return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var result1 = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //    switch (result1)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
        //        case SignInStatus.Failure:
        //        default:
        //            // If the user does not have an account, then prompt the user to create an account
        //            ViewBag.ReturnUrl = returnUrl;
        //            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        //    }
        //}

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure", new VMHandleErrorInfo());
                }
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    RegisterDate = DateTime.UtcNow.Date,
                    EmailConfirmed = true
                };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Student");
                    var blPerson = new BLPerson();

                    blPerson.CreatePerson(new VmPerson
                    {
                        UserId = user.Id,
                        WelcomeDinner = false,
                        LunchOnMonday = false,
                        LunchOnTuesday = false,
                        ReceptionNetworkOnTuesday = false,
                        AwardBanquet = false,
                        NoneOfTheAbove = false,
                    });

                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Session["WelcomeMessage"] = null;
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return RedirectToAction("login");
            }
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View(new VMHandleErrorInfo());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            TempData["LastModelStateErrors"] = result.Errors;

            foreach (var error in result.Errors)
            {
                string errorMessage = error;
                if (error.EndsWith("is already taken."))
                    errorMessage = errorMessage.Replace("Name ", " User Name ");
                ModelState.AddModelError("", errorMessage);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {

                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);

            }
        }
        #endregion

        #region Custom Actoins
        [ChildActionOnly]
        public PartialViewResult Get_ExternalLoginsListPartial(string returnUrl)
        {
            return PartialView("~/Views/Account/_ExternalLoginsListPartial", new ExternalLoginListViewModel() { ReturnUrl = returnUrl });
        }

        #endregion Custom Actoins
    }
}