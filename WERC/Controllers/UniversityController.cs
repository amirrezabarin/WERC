using BLL;
using System.Web.Mvc;

namespace WERC.Controllers
{
    public class UniversityController : BaseController
    {
        [ActionName("guddl")]
        public ActionResult GetUniversityDropDownList()
        {
            var bsUniversity = new BLUniversity();

            var universityList = bsUniversity.GetUniversitySelectListItem(0, int.MaxValue);

            return Json(universityList, JsonRequestBehavior.AllowGet);
        }
    }
}