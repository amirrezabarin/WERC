using BLL;
using CyberneticCode.Web.Mvc.Helpers;
using Model.Base;
using Model.ViewModels.Task;
using Model.ViewModels.Test;
using System;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;

namespace WERC.Controllers
{
    public class TestController : BaseController
    {
        [ActionName("gtddl")]
        public ActionResult GetTestDropDownList()
        {
            var bsTest = new BLTest();

            var testList = bsTest.GetTestSelectListItem(0, int.MaxValue);

            return Json(testList, JsonRequestBehavior.AllowGet);
        }
        [ActionName("gtddlwd")]
        public ActionResult GetTestDropDownListWithDescription()
        {
            var bsTest = new BLTest();

            var testList = bsTest.GetTestSelectListItemWithDescription(0, int.MaxValue);

            return Json(testList, JsonRequestBehavior.AllowGet);
        }

        [ActionName("lctf")]
        [HttpGet]
        public ActionResult LoadCreateTestForm()
        {
            return View("../Admin/CreateTest", new VmTest());
        }

        [ActionName("ct")]
        [HttpPost]
        public ActionResult Create(VmTest model)
        {
            var result = -1;
            var blTest = new BLTest();

            try
            {
                
                result = blTest.CreateTest(model);
            }
            catch (Exception ex)
            {
                result = -1;
            }


            if (result != -1)
            {
                return RedirectToAction("tl", "Admin", new { activeItemId = result });
            }

            model.ActionMessageHandler.Message = "Operation has been failed...\n";

            return View("../Admin/CreateTest", model);
        }

        [ActionName("et")]
        [HttpPost]
        public ActionResult EditTest(VmTest model)
        {
            model.CurrentUserId = CurrentUserId;

            
            var result = true;
            var blTest = new BLTest();

            try
            {
                 
                result = blTest.UpdateTest(model);
            }
            catch (Exception ex)
            {
                result = false;
            }

            
            if (result == false)
            {
                model.ActionMessageHandler.Message = "Operation has been failed...\n call system Admin";
            }

            var jsonData = new
            {
                TestTitle = model.Name,
               
                TestId = model.Id,
                success = result,
                message = model.ActionMessageHandler.Message = "Operation has been succeeded"

            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return View("../Author/TestEdit", model);
        }

        // GET: Test/Delete/5
        [ActionName("dt")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = true;

            var blTest = new BLTest();

            result = blTest.DeleteTest(id);

            string resultMessage = string.Empty;

            if (result == true)
            {
                resultMessage = new BaseViewModel()["Test Has been deleted successfuly."];
            }
            else
            {
                resultMessage = new BaseViewModel()["This test has assignd to judge. You can't delete it..."];
            }

            var jsonResult = new
            {
                success = result,
                message = resultMessage,
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
    }
}