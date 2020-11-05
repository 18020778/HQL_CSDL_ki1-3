using Microsoft.Ajax.Utilities;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerWeb.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InterviewSchedule()
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var user = new UserDao().FindById(acc.UserId);
            var userId = user.UserId;
            var interview = new InterviewDao().ListInterviewByUser(userId).ToList();

            return View(interview);
        }
        public ActionResult WorkList()
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var user = new UserDao().FindById(acc.UserId);
            var userId = user.UserId;
            var workList = new AppliedCandidateDao().ListByUser(userId);
            return View(workList);
        }
        
        public ActionResult InterviewLetter(Int32? interviewId, Guid? offerId)
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var user = new UserDao().FindById(acc.UserId);
            var userId = user.UserId;
            if (interviewId != null && offerId == null)
            {
                var interview = new InterviewDao().findById((int)interviewId);
                ViewBag.Offer = new OfferJobDao().FindByID(interview.OfferID);
                ViewBag.Employee = new EmployeeDao().FindById(interview.EmployeeID);
                return View(interview);
            }
            if (interviewId == null && offerId != null)
            {
                var interview = new InterviewDao().findInterview(userId, (Guid)offerId);
                ViewBag.Offer = new OfferJobDao().FindByID(interview.OfferID);
                ViewBag.Employee = new EmployeeDao().FindById(interview.EmployeeID);
                return View(interview);
            }

            return View();
        }
        public ActionResult InviteWork(Guid offerId)
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var user = new UserDao().FindById(acc.UserId);
            var userId = user.UserId;
            var workinvitation = new WorkInvitationDao().findWorkInvitation(userId, offerId);
            ViewBag.Offer = new OfferJobDao().FindByID(offerId);
            return View(workinvitation);
        }
        public ActionResult Result(Guid offerId)
        {

            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var user = new UserDao().FindById(acc.UserId);
            var userId = user.UserId;
            var workinvitation = new WorkInvitationDao().findWorkInvitation(userId, offerId);
            ViewBag.Offer = new OfferJobDao().FindByID(offerId);
            return View(workinvitation);
        }

        [HttpPost]
        public JsonResult InterViewInvitationRep(Guid offerId, bool accept)
        {

            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var user = new UserDao().FindById(acc.UserId);
            var userId = user.UserId;
            bool checkUpdateInterview;
            if (accept == true)
            {
                checkUpdateInterview = new InterviewDao().UpdateStatus(userId, offerId, "accept");
            }
            else
            {
                checkUpdateInterview = new InterviewDao().UpdateStatus(userId, offerId, "deny");
            }
            return Json(new
            {
                status = checkUpdateInterview
            });
        }

        [HttpPost]
        public JsonResult checkStatusInterview(Guid offerId)
        {

            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var user = new UserDao().FindById(acc.UserId);
            var userId = user.UserId;
           var interview = new InterviewDao().findInterview(userId, offerId);
            var applied = new AppliedCandidateDao().findCandidate(userId, offerId);
            return Json(new
            {
                interviewStatus = interview.Status,
                workStatus = applied.Status
            });
        }


        [HttpPost]
        public JsonResult WorkInvitationRep(Guid offerId, bool accept)
        {

            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var user = new UserDao().FindById(acc.UserId);
            var userId = user.UserId;
            bool checkUpdateWorkInvitation = true;
            if (accept == true)
            {
                checkUpdateWorkInvitation = new WorkInvitationDao().UpdateStatus(userId, offerId, "accept");
                var checkUpdateStatus = new AppliedCandidateDao().UpdateStatus(userId, offerId, "Hoàn tất tuyển dụng");
            }
            else
            {
                checkUpdateWorkInvitation = new WorkInvitationDao().UpdateStatus(userId, offerId, "deny");
            }
            return Json(new
            {
                status = checkUpdateWorkInvitation
            });
        }

        [HttpPost]
        public JsonResult checkStatusWorkInvitation(Guid offerId)
        {

            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var user = new UserDao().FindById(acc.UserId);
            var userId = user.UserId;
            var workinvitation = new WorkInvitationDao().findWorkInvitation(userId, offerId);
            return Json(new
            {
                workInvitationStatus = workinvitation.Status
            });
        }
    }
}