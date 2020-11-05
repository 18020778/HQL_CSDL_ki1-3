using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class InterviewDao
    {
        CareerWeb db = null;
        public InterviewDao()
        {
            db = new CareerWeb();
        }

        public Interview findById(int interviewId)
        {
            return db.Interviews.Find(interviewId);
        }

        public Interview findInterview (String userId, Guid offerId)
        {
            return db.Interviews.SingleOrDefault(x => x.UserID == userId && x.OfferID == offerId);

        }

        public List<InterviewInfo> ListInterviewByUser(String userId)
        {
            var interviewList = db.Interviews.ToList();
            var result = (from interview in interviewList
                          where interview.UserID == userId
                          select new
                          {
                              InterviewID = interview.InterviewID,
                              EnterpriseID = new EmployeeDao().FindById(interview.EmployeeID).EnterpriseID,
                              CandidateID = interview.UserID,
                              CandidateName = new UserDao().FindById(interview.UserID).UserName,
                              OfferID = interview.OfferID,
                              OfferName = new OfferJobDao().FindByID(interview.OfferID).OfferName,
                              Time = interview.Time,
                              Date = interview.Date,
                              Address = interview.Address

                          }).AsEnumerable().Select(x => new InterviewInfo()
                          {
                              InterviewID = x.InterviewID,
                              EnterpriseID = x.EnterpriseID,
                              CandidateID = x.CandidateID,
                              CandidateName = x.CandidateName,
                              OfferID = x.OfferID,
                              OfferName = x.OfferName,
                              Time = x.Time,
                              Date = x.Date,
                              Address = x.Address
                          });

            return result.ToList();
        }

        public List<InterviewInfo> ListInterviewByEnterprise(Guid enterpriseId)
        {
            var interviewList = db.Interviews.ToList();
            var result = (from interview in interviewList
                          where (new EmployeeDao().FindById(interview.EmployeeID).EnterpriseID == enterpriseId)
                          select new
                          {
                              EnterpriseID = new EmployeeDao().FindById(interview.EmployeeID).EnterpriseID,
                              CandidateID = interview.UserID,
                              CandidateName = new UserDao().FindById(interview.UserID).UserName,
                              OfferID = interview.OfferID,
                              OfferName = new OfferJobDao().FindByID(interview.OfferID).OfferName,
                              Time = interview.Time,
                              Date = interview.Date

                          }).AsEnumerable().Select(x => new InterviewInfo()
                          {
                              EnterpriseID = x.EnterpriseID,
                              CandidateID = x.CandidateID,
                              CandidateName = x.CandidateName,
                              OfferID = x.OfferID,
                              OfferName = x.OfferName,
                              Time = x.Time,
                              Date = x.Date

                          });

            return result.ToList();
        }

        public bool InsertInterview(Interview interview)
        {
            try
            {
                db.Interviews.Add(interview);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateStatus(String userId, Guid offerId, String status)
        {
            try
            {
                var interview = findInterview(userId, offerId);
                interview.Status = status;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
