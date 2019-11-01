using BLL;
using Model.ViewModels;
using System.Web.Mvc;
using static Model.ApplicationDomainModels.ConstantObjects;
using static WERC.AppDomainHelper.StaticObjects;

namespace WERC.Controllers.SkilledWorkers
{
    public class HomeController : BaseController
    {
         
        [HttpPost]
        public ActionResult GetActiveUsers()
        {

            var jsonResult = new
            {
                activeUserCount = ActiveUsers.Count,
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult tabClosed()
        {

            var jsonResult = new
            {
                activeUserCount = ActiveUsers.Count,
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {

            var SetWelcomMessage = Request.QueryString["SetWelcomMessage"] != null ? bool.Parse(Request.QueryString["SetWelcomMessage"]) : false;

            var blImage = new BLImage();

            if (SetWelcomMessage == true)
            {
                return View("Home", new VmHome()
                {
                    MostSetWelcomeMessage = false,
                    Images = blImage.GetImagesByType(ImageType.FirstPageImage)

                });

            }
            return View("Home", new VmHome()
            {
                Images = blImage.GetImagesByType(ImageType.FirstPageImage)
            });
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
