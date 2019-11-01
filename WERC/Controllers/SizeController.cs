using BLL;
using System.Web.Mvc;

namespace WERC.Controllers
{
    public class SizeController : BaseController
    {
        [ActionName("gsddl")]
        public ActionResult GetSizeDropDownList()
        {
            var bsSize = new BLSize();

            var sizeList = bsSize.GetSizeSelectListItem(0, int.MaxValue);

            return Json(sizeList, JsonRequestBehavior.AllowGet);
        }
        [ActionName("gjsddl")]
        public ActionResult GetJacketSizeDropDownList()
        {
            var bsSize = new BLSize();

            var sizeList = bsSize.GetSizeSelectListItem(0, int.MaxValue);

            return Json(sizeList, JsonRequestBehavior.AllowGet);
        }
    }
}