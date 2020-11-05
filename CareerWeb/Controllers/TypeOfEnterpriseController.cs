using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerWeb.Controllers
{
    public class TypeOfEnterpriseController : Controller
    {
        // GET: TypeOfEnterprise
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ReturnListType(string name)
        {
            var listType = new TypeOfEnterpriseDao().ReturnList();
            return Json(new
            {
                status = true,
                listType = listType
            });
        }

    }
}