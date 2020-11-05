using CareerWeb.Models;
using iTextSharp.text.pdf.qrcode;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CareerWeb.Controllers
{
    public class OfferJobController : Controller
    {
        // GET: OfferJob
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult DeleteOffer(Guid offerID)
        {
            var check = new OfferJobDao().DeleteOffer(offerID);
            if (!check)
            {
                return Json(new
                {
                    status = false
                });
            }
            var offerJobSkill = new OfferJobSkillDao();
            var listOfferSkill = offerJobSkill.ListByOffer(offerID);
            foreach(var item in listOfferSkill)
            {
                var checkSecond = offerJobSkill.DeleteSkill(offerID, item.ChildMajor);
                if (!checkSecond)
                {
                    return Json(new
                    {
                        status = false
                    });
                }
            }
            return Json(new
            {
                status = true
            });
        }
        [HttpPost]
        public JsonResult DeleteSkillOffer(Guid offerID, int skillID)
        {
            var check = new OfferJobSkillDao().DeleteSkill(offerID, skillID);
            return Json(new
            {
                status = check
            });
        }
        [HttpPost]
        public ActionResult ReturnOffer(Guid offerID)
        {
            var jbDao = new JobMajorDao();
            var offer = new OfferJobDao().FindByID(offerID);
            var listSkill = new OfferJobSkillDao().ListByOffer(offerID);
            var listNameSkill = new List<string>();
            foreach(var item in listSkill)
            {
                listNameSkill.Add(jbDao.NameJob(item.ChildMajor));
            }
            if(offer == null)
            {
                return Json(new
                {
                    status = false
                });
            }
            return Json(new
            {
                offer = offer,
                limitDate = offer.OfferLimitDate.ToString("dd/MM/yyyy"),
                listSkill = listSkill,
                listNameSkill = listNameSkill,
                majorName = jbDao.NameJob(offer.OfferMajor),
                status = true
            });
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddOfferOfEmployee(FormOffer model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var emp = new EmployeeDao().FindById(acc.UserId);
            var offerJob = new OfferJob();
            if (model.typeAction == "add")
            {
                offerJob.OfferID = Guid.NewGuid();
                offerJob.OfferCreateDate = DateTime.Now;
                offerJob.Views = 0;
                offerJob.Applications = 0;
            }
            else
            {
                offerJob.OfferID = model.saveID;
            }
            offerJob.EmployeeID = acc.UserId;
            offerJob.EnterpriseID = emp.EnterpriseID;
            offerJob.OfferName = model.offerName;
            if (model.offerDescription != null && model.offerDescription != "")
            {
                offerJob.OfferDescription = model.offerDescription;
            }
            offerJob.OfferMajor = model.offerMajor;
            offerJob.OfferPosition = model.offerPosition;
            offerJob.OfferSalary = model.offerSalary;
            offerJob.Area = model.offerArea;
            offerJob.Amount = model.offerAmount;
            offerJob.ExperienceRequest = model.offerExperience;
            offerJob.ContactEmail = model.offerEmail;
            offerJob.LearningLevelRequest = model.offerLearning;
            offerJob.OfferLimitDate = DateTime.ParseExact(model.offerLimitDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if(model.offerGender != "none")
            {
                offerJob.Sex = model.offerGender;
            }
            offerJob.JobAddress = model.offerAddress;
            var file = model.offerImage;
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath("/Assets/Client/Img/Offer/" + fileName));
                var srcImage = "/Assets/Client/Img/Offer/" + fileName;
                offerJob.OfferImage = srcImage;
            }
            if (model.typeAction == "add")
            {
                var check = new OfferJobDao().Insert(offerJob);
            }
            else
            {
                var check = new OfferJobDao().Edit(offerJob);
            }
            if (model.offerListSkillId != null && model.offerListSkillId.Count > 0)
            {
                for (var i = 0; i < model.offerListSkillId.Count; i++)
                {
                    var offerSkill = new OfferJobSkill();
                    offerSkill.OfferID = offerJob.OfferID;
                    offerSkill.ParentMajor = model.offerListSkillParent[i];
                    offerSkill.ChildMajor = model.offerListSkillId[i];
                    var checkFalse = new OfferJobSkillDao().Insert(offerSkill);
                    if (!checkFalse) break;
                }
            }
            return RedirectToAction("ListAndCreateOffer", "Employee");
        }
    }
}