using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace CareerWeb.Areas.Admin.Controllers
{
    public class JobMajorController : Controller
    {
        // GET: Admin/JobMajor
        public ActionResult Index()
        {
            var model = new JobMajorDao().ListUsers();
            return View(model);
        }
        [HttpPost]
        public JsonResult ConfirmNewJob(int jobID)
        {
            var check = new JobMajorDao().ConfirmJob(jobID);
            if (check)
            {
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
        [HttpPost]
        public JsonResult DeleteNewJob(int jobID)
        {
            var check = new JobMajorDao().DeleteJob(jobID);
            if (check)
            {
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
    }
}