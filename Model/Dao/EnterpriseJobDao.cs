using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class EnterpriseJobDao
    {
        CareerWeb db = null;
        public EnterpriseJobDao()
        {
            db = new CareerWeb();
        }
        public List<EnterpriseJob> ListEnterpriseJob()
        {
            return db.EnterpriseJobs.ToList();
        }
        public bool Insert(EnterpriseJob ins)
        {
            try
            {
                db.EnterpriseJobs.Add(ins);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<EnterpriseJobFull> ListJob(Guid enterpriseID)
        {
            try
            {
                var model = (from a in db.JobMajors
                             join b in db.EnterpriseJobs
                             on a.JobID equals b.JobId
                             where a.Status == true && b.EnterpriseID == enterpriseID
                             select new
                             {
                                 JobID = a.JobID,
                                 JobName = a.JobName,
                                 Status = a.Status,
                                 EnterpriseID = b.EnterpriseID,
                             }).AsEnumerable().Select(x => new EnterpriseJobFull()
                             {
                                 JobName = x.JobName,
                                 JobID = x.JobID,
                                 Status = x.Status.Value,
                                 EnterpriseID = x.EnterpriseID,                              
                             });
                return model.ToList();
            }
            catch
            {
                return null;
            }
        }
        public List<EnterpriseJobFull> ListJobNew(Guid enterpriseID)
        {
            try
            {
                var model = (from a in db.JobMajors
                             join b in db.EnterpriseJobs
                             on a.JobID equals b.JobId
                             where a.Status == false && b.EnterpriseID == enterpriseID
                             select new
                             {
                                 JobID = a.JobID,
                                 JobName = a.JobName,
                                 Status = a.Status,
                                 EnterpriseID = b.EnterpriseID,                          
                             }).AsEnumerable().Select(x => new EnterpriseJobFull()
                             {
                                 JobName = x.JobName,
                                 JobID = x.JobID,
                                 Status = x.Status.Value,
                                 EnterpriseID = x.EnterpriseID,
                             });
                return model.ToList();
            }
            catch
            {
                return null;
            }
        }
        public bool RemoveEntJob(Guid id)
        {
            try
            {
                var listEntJob = db.EnterpriseJobs.Where(x => x.EnterpriseID == id).ToList();
                foreach(var item in listEntJob)
                {
                    db.EnterpriseJobs.Remove(item);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
