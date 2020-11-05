using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerWeb.Controllers
{
    public class UserMajorController : Controller
    {
        // GET: UserMajor
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddListUserSkill(List<int> listAdd, List<int> listParent)
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var check = true;
            var umDao = new UserMajorDao();
            for(var i = 0; i < listAdd.Count; i += 1)
            {
                var add = new UserMajor();
                add.UserID = acc.UserId;
                add.MajorID = listAdd[i];
                add.MajorParent = listParent[i];
                if (!umDao.InsertUserMajor(add))
                {
                    check = false;
                    break;
                }
            }
            return Json(new
            {
                status = check
            });
        }
        [HttpPost]
        public JsonResult DeleteUserSkill(int jobID)
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var check = new UserMajorDao().DeleteUserJob(acc.UserId, jobID);
            return Json(new
            {
                status = check
            });
        }
        [HttpPost]
        public JsonResult DeleteUserMajor(int jobID)
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var check = new UserMajorDao().DeleteUserJob(acc.UserId, jobID);
            if (!check)
            {
                return Json(new
                {
                    status = check
                });
            }
            var listSkillByUserAndParent = new UserMajorDao().ListSkillByUserAndMajor(acc.UserId, jobID);
            foreach(var item in listSkillByUserAndParent)
            {
                var checkSecondStep = new UserMajorDao().DeleteUserJob(acc.UserId, item.MajorID);
                if (!checkSecondStep)
                {
                    check = false;
                    break;
                }
            }
            return Json(new
            {
                status = check
            });
        }
    }
}