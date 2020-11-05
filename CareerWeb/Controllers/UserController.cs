using CareerWeb.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using Model.Dao;
using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Text;
using HtmlAgilityPack;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.html;
using System.Web.UI.WebControls;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.html.head;
using Rotativa;
using System.Data.Entity.Core.Metadata.Edm;
using PagedList;
using PagedList.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CareerWeb.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult SearchCompanyForUser(int? page, string EName = "", int EArea = 0, int ECareer = 0, int ESize = 0)
        {
            var jobMajorDao = new JobMajorDao();
            ViewBag.ListEnterpriseSize = new EnterpriseSizeDao().ReturnList();
            ViewBag.ListJobMain = jobMajorDao.ListJobMain();
            ViewBag.ListArea = new AreaDao().ListArea();

            var ListEnterpriseContainer = new EnterpriseDao().ReturnFilterList(EName, EArea, ECareer, ESize).ToPagedList(page ?? 1, 5);
            var nameJob = new List<string>();
            foreach (var item in ListEnterpriseContainer)
            {
                var saveName = "";
                for (var i = 0; i < item.listJobId.Count; i++)
                {
                    if (i == 0)
                    {
                        saveName += jobMajorDao.NameJob(item.listJobId[i]);
                        continue;
                    }
                    saveName += ", " + jobMajorDao.NameJob(item.listJobId[i]);
                }
                nameJob.Add(saveName);
            }
            ViewBag.ListFullJobName = nameJob;
            return View(ListEnterpriseContainer);
        }


        public ActionResult ResultForSearchJob(Guid OfferID)
        {
            ViewBag.ListEnterpriseName = new EnterpriseDao().ReturnList();
            ViewBag.ListJobMain = new EnterpriseJobDao().ListEnterpriseJob();
            ViewBag.ListArea = new AreaDao().ListArea();
            ViewBag.ListOfferJob = new OfferJobDao().ReturnFilterList();

            var jobMajorDao = new JobMajorDao();
            var saveName = "";
            var ShowDetail = new OfferJobDao().ShowDetail(OfferID);
            foreach (var item in ShowDetail)
            {
                for (var i = 0; i < item.listJobId.Count; i += 1)
                {
                    saveName += jobMajorDao.NameJob(item.listJobId[i]) + ", ";
                }
                saveName.Remove(saveName.Length - 1);
            }
            ViewBag.ListFullJobName = saveName;
            return View(ShowDetail);
        }
       
        public ActionResult SearchJobForUser(int? page, string OfferName = "", int Area = 0, int OfferMajor = 0, int OfferSalary = 0, int PositionJobID = 0, string Sex = "0", int ExperienceRequest = 0, int LearningLevelRequest = 0, string OfferCreateDate = "")
        {
            ViewBag.ListJobMain = new JobMajorDao().ListJobMain();
            ViewBag.ListArea = new AreaDao().ListArea();
            ViewBag.ListExperience = new ExperienceDao().ListExperiences();
            ViewBag.ListSalary = new SalaryDao().ListSalary();
            ViewBag.ListPositionEmployee = new PositionEmployeeDao().ReturnList();
            ViewBag.ListLevelLearning = new LevelLearningDao().ReturnList();

            var ListJobContainer = new OfferJobDao().ReturnFilterList(OfferName, Area, OfferMajor, OfferSalary, PositionJobID, Sex, ExperienceRequest, LearningLevelRequest).ToPagedList(page ?? 1, 2);
            return View(ListJobContainer);
        }
        public ActionResult Index()
            
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserHome");
            }
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var user = new UserDao().FindById(acc.UserId);
            ViewBag.ListUserJobParent = new JobMajorDao().ReturnParentListByUser(user.UserId);
            ViewBag.ListLevelLearning = new LevelLearningDao().ReturnList();
            ViewBag.ListPostion = new PositionEmployeeDao().ReturnList();
            ViewBag.ListLanguage = new LanguageDao().ReturnList();
            ViewBag.ListJobSub = new JobMajorDao().ListJobSubByUser(user.UserId);
            ViewBag.ListEnterprise = new EnterpriseDao().ReturnList();
            ViewBag.ListArea = new AreaDao().ListArea();
            ViewBag.ListUserExperience = new UserExperienceDao().ListByUser(acc.UserId);
            ViewBag.ListUserForeignLanguage = new UserForeignLanguageDao().ListByUser(acc.UserId);
            ViewBag.ListUserCertificate = new UserCertificateDao().ListByUser(acc.UserId);
            ViewBag.ListSkill = new UserMajorDao().ListSkillByUserId(acc.UserId);
            ViewBag.ListMajorUser = new UserMajorDao().ListUserMajor(acc.UserId);
            ViewBag.ListMajorCanChoose = new JobMajorDao().ListJobMainByUser(acc.UserId);
            ViewBag.ListSalary = new SalaryDao().ListSalary();
            return View(user);

        }

        //[HttpPost]
        //public ActionResult IndexVideo(HttpPostedFileBase videoFile)
        //{
        //    if (videoFile != null)
        //    {
        //        string fileName = Path.GetFileName(videoFile.FileName);
        //        if (videoFile.ContentLength < 104857600)
        //        {
        //            videoFile.SaveAs(Server.MapPath("/UserVideo/" + fileName));
        //            string mainconn = ConfigurationManager.ConnectionStrings["CareerWeb"].ConnectionString;
        //            SqlConnection sqlconn = new SqlConnection(mainconn);
        //            string sqlquery = "insert into [dbo].[User] values(@VideoProfile)";
        //            SqlCommand sqlcomn = new SqlCommand(sqlquery, sqlconn);
        //            sqlconn.Open();
        //            sqlcomn.Parameters.AddWithValue("@VideoProfile", "/UserVideo/" + fileName);
        //            sqlcomn.ExecuteNonQuery();
        //            sqlconn.Close();
        //            ViewData["Message"] = "Sucess";
        //        }
        //    }
        //    List<UploadClass> videoList = new List<UploadClass>();
        //    string mainconn = ConfigurationManager.ConnectionStrings["CareeerWeb"].ConnectionString;
        //    SqlConnection sqlconn = new SqlConnection(mainconn);
        //    string sqlquery = "select  from ";
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public JsonResult CreateAccountInfor(String userID, string email, string name, string mobile, string dateBirth, string sex, string atSchool, int area, List<int> listJob)
        {
            var userInfor = new User();
            userInfor.UserId = userID;
            userInfor.UserName = name;
            userInfor.UserEmail = email;
            userInfor.UserMobile = mobile;
            userInfor.AtSchool = (atSchool == "in") ? true : false;
            userInfor.Sex = sex;
            userInfor.UserArea = area;
            string[] splitDate = dateBirth.Split('-');
            userInfor.UserBirthDay = new DateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), int.Parse(splitDate[2]));
            userInfor.CompleteProfile = 1;
            var checkInsertUser = new UserDao().InsertUser(userInfor);
            var checkInsertUserMajor = true;
            foreach (int x in listJob)
            {
                var userMajor = new UserMajor();
                userMajor.UserID = userID;
                userMajor.MajorID = x;
                var ck = new UserMajorDao().InsertUserMajor(userMajor);
                if (!ck) checkInsertUserMajor = ck;
            }
            if (checkInsertUser && checkInsertUserMajor)
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
        public JsonResult ModifyUser(ModifyUserForm user)
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var check = new UserDao().ModifyUserBasic(acc.UserId, user);
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
        public JsonResult ImageUpload(FileUploadModel model)
        {
            var file = model.ImageFile;
            if (file != null)
            {

                var fileName = Path.GetFileName(file.FileName);
                var extention = Path.GetExtension(file.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);
                file.SaveAs(Server.MapPath("/Assets/Client/Img/User/ImageProfile/" + fileName));
                var srcImage = "/Assets/Client/Img/User/ImageProfile/" + fileName;
                var accID = int.Parse(User.Identity.Name);
                var acc = new AccountDao().FindAccountById(accID);
                var check = new UserDao().UploadImage(acc.UserId, srcImage);
                if (check)
                {
                    return Json(new
                    {
                        status = true,
                    });
                }
            }
            return Json(new
            {
                status = false
            });
        }
        [HttpPost]
        public JsonResult ModifyInforJob(string nameJob, int salaryID, int positionId, List<int> idList)
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var userNew = new User();
            userNew.DesiredJob = nameJob;
            userNew.Salary = salaryID;
            userNew.PositionApply = positionId;
            var check = new UserDao().ModifyUserJob(acc.UserId, userNew);
            if (!check)
            {
                return Json(new
                {
                    status = false
                });
            }
            for(var i = 0; i < idList.Count; i++)
            {
                var newUserMajor = new UserMajor();
                newUserMajor.UserID = acc.UserId;
                newUserMajor.MajorID = idList[i];
                var checkStepSecond = new UserMajorDao().InsertUserMajor(newUserMajor);
                if (!checkStepSecond)
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
        public ActionResult ViewCV(string template = "1")
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            ViewBag.Template = template;
            return View();
        }
        public ActionResult Check(string template, bool isPdf = false )
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var user = new UserDao().FindById(acc.UserId);
            ViewBag.ListUserExperience = new UserExperienceDao().ListByUser(acc.UserId);
            ViewBag.ListUserForeignLanguage = new UserForeignLanguageDao().ListByUser(acc.UserId);
            ViewBag.ListUserCertificate = new UserCertificateDao().ListByUser(acc.UserId);
            ViewBag.ListSkill = new UserMajorDao().ListSkillByUserId(acc.UserId);
            ViewBag.ListMajorUser = new UserMajorDao().ListUserMajor(acc.UserId);
            ViewBag.Link = Server.MapPath(user.UserImage);
            if (!isPdf)
            {
                return PartialView(template, user);
            }
            return new PartialViewAsPdf(template, user);
        }
        public ActionResult TemplateCV_1()
        {
            return PartialView();
        }
        public ActionResult UserHome()
        {
            ViewBag.ListJob = new JobMajorDao().ListJobMain();
            ViewBag.ListArea = new AreaDao().ListArea();
            ViewBag.ListExperience = new ExperienceDao().ListExperiences();
            ViewBag.ListSalary = new SalaryDao().ListSalary();
            ViewBag.ListPositionEmployee = new PositionEmployeeDao().ReturnList();
            ViewBag.ListLevelLearning = new LevelLearningDao().ReturnList();
            ViewBag.ListOffer = new OfferJobDao().ListOfferJob();
            ViewBag.ListJobMain = new JobMajorDao().ListJobMain();
            return View();
        }
        public ActionResult ResultForSearchCompany(Guid EnterpriseID)
        {
            var ShowEnterprise = new EnterpriseDao().ShowEnterprise(EnterpriseID);
            var jobMajorDao = new JobMajorDao();
            var saveName = "";
            foreach (var item in ShowEnterprise)
            {
                for (var i = 0; i < item.listJobId.Count; i += 1)
                {
                    saveName += jobMajorDao.NameJob(item.listJobId[i]) + ", ";
                }
                saveName.Remove(saveName.Length - 1);
            }
            ViewBag.ListFullJobName = saveName;
            ViewBag.ShowContainer = new OfferJobDao().ShowContainer(EnterpriseID);
            return View(ShowEnterprise);
        }
    }
}