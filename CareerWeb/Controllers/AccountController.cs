using CareerWeb.Common;
using Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CareerWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegisterCommon()
        {
            return View();
        }
        public ActionResult RegisterForUser()
        {
          
            return View();
        }
        public ActionResult RegisterForEnterprise()
        {
            ViewBag.ListTypeOfEnterprise = new TypeOfEnterpriseDao().ReturnList();
            ViewBag.ListEnterpriseSize = new EnterpriseSizeDao().ReturnList();
            return View();
        }
        public ActionResult RegisterForUniversity()
        {
            return View();
        }
        public ActionResult RegisterForEmployee()
        {
            ViewBag.ListPositionEmployee = new PositionEmployeeDao().ReturnList();
            ViewBag.ListEnterprise = new EnterpriseDao().ReturnList();
            return View();
        }
        private string RandomCode()
        {
            Random rd = new Random();
            string code = "";    
            for(int i = 0; i < 6; i++)
            {
                int mod = rd.Next(0, 100);
                code += ((rd.Next(0, 9) + mod) % 10) + "";
            }
            return code;
        }
        [HttpPost]
        public JsonResult SendVertify(string email, string pass)
        {
            var checkEmail = new AccountDao().checkEmailExsit(email);
            if (checkEmail)
            {
                return Json(new
                {
                    status = false,
                    checkEmail = false
                });
            }
            try
            {
                var code = RandomCode();
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/Template/SendVertify.html"));
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Code}}", code);
                new MailHelper().SendMail(email, "Người đăng ký mới ở CareerWeb", content);
                return Json(new
                {
                    status = true,
                    checkEmail = true,
                    code = code
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = false,
                    checkEmail = true
                });
            }
        }
        [HttpPost]
        public JsonResult CreateAccount(string accountName, string accountPass, string typeOfAccount)
        {
            var checkCreate = new AccountDao().createAccount(accountName, Encryptor.MD5Hash(accountPass), typeOfAccount);
            var listArea = new AreaDao().ListArea();
            var listJob = new JobMajorDao().ListJobMain();
            return Json(new
            {
                userId = checkCreate,
                listArea = listArea,
                listJob = listJob
            });
        }
        [HttpPost]
        public JsonResult Login(string accName, string passWord)
        {
            int userId = 0;
            var acc = new AccountDao().CheckLogin(accName, Encryptor.MD5Hash(passWord), out userId);
            if(acc == -1 || acc == -2)
            {
                return Json(new
                {
                    status = false,
                    error = acc
                });
            }
            FormsAuthentication.SetAuthCookie(userId.ToString(), true);
            return Json(new
            {
                status = true,
                type = acc
            });
        }
        public ActionResult Logout()
        {
            var accID = int.Parse(User.Identity.Name);
            FormsAuthentication.SignOut();
            return RedirectToAction("UserHome" ,"User");
        }
    }
}