using CareerWeb.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerWeb.Controllers
{
    public class UserCertificateController : Controller
    {
        // GET: UserCertificate
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Insert(AddUserCertificateModel model)
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var ucInsert = new UserCertificate();
            ucInsert.UserId = acc.UserId;
            ucInsert.NameCertificate = model.Name;
            ucInsert.GetDate = model.Date;
            var file = model.ImageFile;
            if (file != null)
            {

                var fileName = Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath("/Assets/Client/Img/User/Certificate/" + fileName));
                var srcImage = "/Assets/Client/Img/User/Certificate/" + fileName;
                ucInsert.ImageCertificate = srcImage;
            }
            return Json(new
            {
                status = new UserCertificateDao().Insert(ucInsert)
            });
        }
        [HttpPost]
        public JsonResult Remove(int id)
        {
            var check = new UserCertificateDao().Remove(id);
            return Json(new
            {
                status = check
            });
        }
        [HttpPost]
        public JsonResult Modify(AddUserCertificateModel model)
        {
            var newModify = new UserCertificate();
            newModify.NameCertificate = model.Name;
            newModify.GetDate = model.Date;
            var file = model.ImageFile;
            if (file != null)
            {

                var fileName = Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath("/Assets/Client/Img/User/Certificate/" + fileName));
                var srcImage = "/Assets/Client/Img/User/Certificate/" + fileName;
                newModify.ImageCertificate = srcImage;
            }
            var check = new UserCertificateDao().Modify(model.ID, newModify);
            return Json(new
            {
                status = check
            });
        }
    }
}