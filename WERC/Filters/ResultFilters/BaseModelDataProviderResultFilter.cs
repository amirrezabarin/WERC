using WERC.AppDomainHelper;
using BLL;
using Model.ApplicationDomainModels;
using Model.Base;
using System;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using Model.ViewModels;
using WERC.Controllers;
using Microsoft.AspNet.Identity.Owin;

namespace WERC.Filters.ResultFilters
{
    public class BaseModelDataProviderResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model;
            var layoutModel = model as BaseViewModel;

            if (layoutModel != null)
            {
                var layout = new LayoutViewModel() { SiteName = "WERC" };

                layoutModel.Layout = layout;

                var languageDictionary = new Dictionary<string, string>();// PreLoadData.LoadLanguage(Thread.CurrentThread.CurrentCulture.Name);
                 
                layoutModel.CurrentCultureName = Thread.CurrentThread.CurrentCulture.Name;

                layoutModel.LanguageDictionary = languageDictionary;

                layoutModel.activeLanguageList = new List<VmActiveLanguage>();// new BLLanguage().GetActiveLanguages();
                layoutModel.activeLanguageCommaSepatated = new BLLanguage().GetActiveLanguagesCommaSeparated(layoutModel.activeLanguageList);

                if (layoutModel.MostSetWelcomeMessage == false)
                {
                    if (filterContext.HttpContext.Session["WelcomeMessage"] != null)
                        layoutModel.WelcomeMessage = filterContext.HttpContext.Session["WelcomeMessage"].ToString();
                }

                if (layoutModel.MostSetWelcomeMessage)
                {
                    Random random = new Random();

                    int index = random.Next(layoutModel.WelcomeMessageList.Length);
                    filterContext.HttpContext.Session["WelcomeMessage"] = layoutModel.WelcomeMessage = layoutModel.WelcomeMessageList[index];
                }

                if (filterContext.HttpContext.Request.IsAuthenticated)
                {
                    var userManager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var user = userManager.Users.First(u => u.UserName == System.Web.HttpContext.Current.User.Identity.Name);
                    layoutModel.UserEmailConfirmed = user.EmailConfirmed;

                    string userName = filterContext.HttpContext.User.Identity.Name;
                    if (SmUserRolesList.UserRoles == null)
                    {
                        var blUser = new BLUser();
                        SmUserRolesList.UserRoles = blUser.GetAllUserRoles();
                    }
                    var controller = (filterContext.Controller as BaseController);

                    layoutModel.CurrentUserId = controller.CurrentUserId;
                    layoutModel.CurrentUserRoles = (from roles in SmUserRolesList.UserRoles where roles.UserName == userName select roles.RoleName).AsEnumerable();
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }
    }
}