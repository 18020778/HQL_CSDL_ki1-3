using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerWeb.Controllers
{
    public class UserForeignLanguageController : Controller
    {
        // GET: UserForeignLanguage
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Insert(int languegeId, string level, string description)
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var ufInsert = new UserForeignLanguage();
            ufInsert.UserID = acc.UserId;
            ufInsert.LanguageID = languegeId;
            ufInsert.LanguageLevel = level;
            if (description != null && description != "")
            {
                ufInsert.Description = description;
            }
            return Json(new
            {
                status = new UserForeignLanguageDao().Insert(ufInsert)
            });
        }
        [HttpPost]
        public JsonResult Remove(int id)
        {
            var check = new UserForeignLanguageDao().Remove(id);
            return Json(new
            {
                status = check
            });
        }
        [HttpPost]
        public JsonResult Modify(int id, int languegeId, string level, string description)
        {
            var ufInsert = new UserForeignLanguage();
            ufInsert.LanguageID = languegeId;
            ufInsert.LanguageLevel = level;
            if (description != null && description != "")
            {
                ufInsert.Description = description;
            }
            var check = new UserForeignLanguageDao().Modify(id, ufInsert);
            return Json(new
            {
                status = check
            });
        }
    }
}