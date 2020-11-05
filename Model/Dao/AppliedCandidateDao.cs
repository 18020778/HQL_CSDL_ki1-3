using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AppliedCandidateDao
    {
        CareerWeb db = null;
        public AppliedCandidateDao()
        {
            db = new CareerWeb();
        }

        public List<AppliedCandidate> ListCandidate()
        {
            return db.AppliedCandidates.ToList();
        }

        public List<AppliedCandidate> ListCandidateApply(Guid enterpriseId)
        {
            return db.AppliedCandidates.Where(x => x.EnterpriseID == enterpriseId).ToList();
        }

        public AppliedCandidate findCandidate(String userId, Guid offerId)
        {
            return db.AppliedCandidates.SingleOrDefault(x => x.UserID == userId && x.OfferID == offerId);
        }

        public bool UpdateStatus(String userId, Guid offerId, String status)
        {
            try
            {
                var candidate = findCandidate(userId, offerId);
                candidate.Status = status;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<AppliedCandidate> ListByUser(String userId)
        {
            var workList = ListCandidate();
            var result = from appliedCandidate in workList
                         where appliedCandidate.UserID == userId
                         select appliedCandidate;
            return result.ToList();
        }

        public List<AppliedCandidateInfo> AppliedCandidateList(Guid enterpriseId)
        {
            var appliedCandidateList = ListCandidateApply(enterpriseId);
            var result = (from AppliedCandidate in appliedCandidateList
                          where AppliedCandidate.EnterpriseID == enterpriseId
                          select new
                          {
                              EnterpriseID = AppliedCandidate.EnterpriseID,
                              CandidateID = AppliedCandidate.UserID,
                              CandidateName = new UserDao().FindById(AppliedCandidate.UserID).UserName,
                              OfferID = AppliedCandidate.OfferID,
                              OfferName = new OfferJobDao().FindByID(AppliedCandidate.OfferID).OfferName,
                              Salary = (new UserDao().FindById(AppliedCandidate.UserID).Salary != null) ? new UserDao().FindById(AppliedCandidate.UserID).Salary : 0,
                              Status = AppliedCandidate.Status,
                              CreateDate = AppliedCandidate.CreateDate
                          }).AsEnumerable().Select(x => new AppliedCandidateInfo()
                          {
                              EnterpriseID = x.EnterpriseID,
                              CandidateID = x.CandidateID,
                              CandidateName = x.CandidateName,
                              OfferID = x.OfferID,
                              OfferName = x.OfferName,
                              Salary = new SalaryDao().AmountSalary(x.Salary.Value),
                              Status = x.Status,
                              CreateDate = x.CreateDate
                          });
            return result.ToList();
        }


    }


}
