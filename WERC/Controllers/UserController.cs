using WERC.Filters.ActionFilterAttributes;
using BLL;
using Model.ApplicationDomainModels;
using System.Web.Mvc;
using static Model.ApplicationDomainModels.ConstantObjects;

namespace WERC.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        [ActionName("ul")]
        [RoleBaseAuthorize(SystemRoles.Admin)]
        public ActionResult UserList()
        {
            var blUser = new BLUser();

            return View("UserList", blUser.GetAllUsers());
        }

        [HttpGet]
        [ActionName("su")]
        [RoleBaseAuthorize(SystemRoles.Admin)]
        public ActionResult SearchUser(
          string searchText = "")
        {
            var blUser = new BLUser();

            return View("UserList", blUser.GetUserByFiler(searchText));

        }
    }
}