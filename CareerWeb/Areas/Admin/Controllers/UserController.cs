using Model.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CareerWeb.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            var model = new UserDao().ListUsers();
            return View(model);
        }
        [HttpDelete]
        public ActionResult DeleteUser(String id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult DetailUser(String id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = new UserDao().FindById(id);
            return View(user);
        }
    }
}