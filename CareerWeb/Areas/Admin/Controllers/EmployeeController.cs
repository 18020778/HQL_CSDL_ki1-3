using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CareerWeb.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Admin/Employee
        public ActionResult Index()
        {
            var model = new EmployeeDao().ListEmployees();
            return View(model);
        }
        public ActionResult DeleteEmployee(String id)
        {
            new EmployeeDao().Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult DetailEmployee(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = new EmployeeDao().FindById(id);
            return View(employee);
        }
    }
}