using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CareerWeb.Controllers
{
    public class UserExperienceController : Controller
    {
        // GET: UserExperience
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Insert(int position, string nameEnterprise, string timeStart, string timeEnd, string description)
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var usInsert = new UserExperience();
            usInsert.UserID = acc.UserId;
            usInsert.PositionID = position;
            usInsert.EnterpriseName = nameEnterprise;
            usInsert.StartTime = timeStart;
            if (timeEnd != null && timeEnd != "")
            {
                usInsert.EndTime = timeEnd;
            }
            if (description != null && description != "")
            {
                usInsert.Description = description;
            }
            return Json(new
            {
                status = new UserExperienceDao().Insert(usInsert)
            });
        }
        [HttpPost]
        public JsonResult Remove(int id)
        {
            var check = new UserExperienceDao().Remove(id);
            return Json(new
            {
                status = check
            });
        }
        [HttpPost]
        public JsonResult Modify(int id, int position, string nameEnterprise, string timeStart, string timeEnd, string description)
        {
            var newModify = new UserExperience();
            newModify.PositionID = position;
            newModify.EnterpriseName = nameEnterprise;
            newModify.StartTime = timeStart;
            if (timeEnd != null && timeEnd != "")
            {
                newModify.EndTime = timeEnd;
            }
            if (description != null && description != "")
            {
                newModify.Description = description;
            }
            var check = new UserExperienceDao().Modify(id, newModify);
            return Json(new
            {
                status = check
            });
        }
    }
}