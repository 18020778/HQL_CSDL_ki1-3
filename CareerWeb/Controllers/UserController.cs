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
       
        public ActionResult SearchJobForUser(int? page, string OfferName = "", int Area = 0, int OfferMajor = 0, int OfferSalary = 0, int PositionJobID = 0,string sex = "",  int ExperienceRequest = 0, int LearningLevelRequest = 0)
        {
            ViewBag.ListJobMain = new JobMajorDao().ListJobMain();
            ViewBag.ListArea = new AreaDao().ListArea();
            ViewBag.ListExperience = new ExperienceDao().ListExperiences();
            ViewBag.ListSalary = new SalaryDao().ListSalary();
            ViewBag.ListPositionEmployee = new PositionEmployeeDao().ReturnList();
            ViewBag.ListLevelLearning = new LevelLearningDao().ReturnList();

            var ListJobContainer = new OfferJobDao().ReturnFilterList(OfferName, Area, OfferMajor, OfferSalary, PositionJobID, sex, ExperienceRequest, LearningLevelRequest).ToPagedList(page ?? 1, 5);
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

        [HttpPost]
        public JsonResult CreateAccountInfor(String userID, string email, string name, string mobile, string dateBirth, string sex, string atSchool, int area, List<int> listJob)
        {
            var userInfor = new Model.EF.User();
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
        public JsonResult FileUpload(FileUploadModel model)
        {
            var file = model.UploadFile;
            if (file != null)
            {

                var fileName = Path.GetFileName(file.FileName);
                var extention = Path.GetExtension(file.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);
                var accID = int.Parse(User.Identity.Name);
                var acc = new AccountDao().FindAccountById(accID); 
                if(model.type == "image")
                {
                    file.SaveAs(Server.MapPath("/Assets/Client/Img/User/ImageProfile/" + fileName));
                }
                else
                {
                    file.SaveAs(Server.MapPath("/Assets/Client/Img/User/UserVideo/" + fileName));
                }
                var srcFile =  (model.type == "image") ? "/Assets/Client/Img/User/ImageProfile/" + fileName : "/Assets/Client/Img/User/UserVideo/" + fileName;         
                var check = (model.type == "image") ? new UserDao().UploadImage(acc.UserId, srcFile) : new UserDao().UploadVideo(acc.UserId, srcFile);
                if (check)
                {
                    if(model.type != "image")
                    {
                        return Json(new
                        {
                            status = true,
                            linkVideo = srcFile
                        });
                    }
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
            var stringURL = ShowEnterprise[0].UrlAddress;
            var indexFirst = -1; /// Dấu @
            for(var i = 0; i < stringURL.Length; i += 1)
            {
                if(stringURL[i] == '@')
                {
                    indexFirst = i;
                    break;
                }
            }
            var indexSecond = -1;
            var indexThird = -1;
            for(var i = indexFirst + 1; i < stringURL.Length; i += 1)
            {
                if(stringURL[i] == ',')
                {
                    if (indexSecond == -1) indexSecond = i;
                    else
                    {
                        indexThird = i;
                        break;
                    }
                }
            }
            var xPoint = stringURL.Substring(indexFirst + 1, indexSecond - indexFirst - 1);
            var yPoint = stringURL.Substring(indexSecond + 1, indexThird - indexSecond - 1);
            ViewBag.xPoint = xPoint;
            ViewBag.yPoint = yPoint;
            ViewBag.ListFullJobName = saveName;
            ViewBag.ShowContainer = new OfferJobDao().ShowContainer(EnterpriseID);
            return View(ShowEnterprise);
        }
    }
}