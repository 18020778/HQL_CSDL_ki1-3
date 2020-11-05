using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SavedCandidateDao
    {
        CareerWeb db = null;
        public SavedCandidateDao()
        {
            db = new CareerWeb();
        }

        public List<SavedCandidate> ListSavedCandidate(Guid enterpriseId)
        {
            return db.SavedCandidates.Where(x => x.EnterpriseID == enterpriseId).ToList();
        }

        public List<SavedCandidateInfo> ListCandidateSave(Guid enterpriseId)
        {
            var listCandidate = ListSavedCandidate(enterpriseId);
            var result = (from SavedCandidate in listCandidate
                          where SavedCandidate.EnterpriseID == enterpriseId
                          select new
                          {
                              EnterpriseID = SavedCandidate.EnterpriseID,
                              CandidateID = SavedCandidate.UserID,
                              CandidateName = new UserDao().FindById(SavedCandidate.UserID).UserName,
                              Salary = (new UserDao().FindById(SavedCandidate.UserID).Salary != null) ? new UserDao().FindById(SavedCandidate.UserID).Salary : 0,
                              DesiredJob = (new UserDao().FindById(SavedCandidate.UserID).DesiredJob != null) ? new UserDao().FindById(SavedCandidate.UserID).DesiredJob : "   ",
                              CreateDate = SavedCandidate.CreateDate,
                          }).AsEnumerable().Select(x => new SavedCandidateInfo()
                          {
                              EnterpriseID = x.EnterpriseID,
                              CandidateID = x.CandidateID,
                              CandidateName = x.CandidateName,
                              Salary = new SalaryDao().AmountSalary(x.Salary.Value),
                              DesiredJob = x.DesiredJob,
                              CreateDate = x.CreateDate
                          });

            return result.ToList();
        }

        public SavedCandidate findCandidate(String userId, Guid enterpriseId)
        {
            return db.SavedCandidates.SingleOrDefault(x => x.UserID == userId && x.EnterpriseID == enterpriseId);
        }
        public bool InsertCandidate(SavedCandidate savedCandidate)
        {
            try
            {
                db.SavedCandidates.Add(savedCandidate);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool DeleteCandidate(SavedCandidate savedCandidate)
        {
            try
            {
                db.SavedCandidates.Attach(savedCandidate);
                db.SavedCandidates.Remove(savedCandidate);
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
