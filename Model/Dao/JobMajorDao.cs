using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class JobMajorDao
    {
        CareerWeb db = null;
        public JobMajorDao()
        {
            db = new CareerWeb();
        }
        public List<JobMajor> ListUsers()
        {
            try
            {
                return db.JobMajors.ToList();
            }
            catch
            {
                return null;
            }
        }
        public string NameJob(int id)
        {
            return db.JobMajors.Find(id).JobName;
        }
        public List<JobMajor> ListJobMain()
        {
            return db.JobMajors.Where(x => x.JobIDParent == null).ToList();
        }
        public List<JobMajor> ListJobSub()
        {
            return db.JobMajors.Where(x => x.JobIDParent != null).ToList();
        }
        public List<JobMajor> ListJobSubByUser(String userId)
        {
            // Loại bỏ những skill đã có
            var listJobMain = db.UserMajors.Where(x => x.UserID == userId && x.MajorParent == null).Select(x => x.MajorID).ToList();
            var presentSkill = db.UserMajors.Where(x => x.UserID == userId && x.MajorParent != null).Select(x => x.MajorID).ToList();
            return db.JobMajors.Where(x => x.JobIDParent != null && listJobMain.Contains(x.JobIDParent.Value) && !presentSkill.Contains(x.JobID)).ToList();
        }
        public List<JobMajor> ListJobMainByUser(String userId)
        {
            // Loại bỏ những major đã có
            var listJobMain = db.UserMajors.Where(x => x.UserID == userId && x.MajorParent == null).Select(x => x.MajorID).ToList();
            return db.JobMajors.Where(x => x.JobIDParent == null && !listJobMain.Contains(x.JobID)).ToList();
        }
        public int Insert(JobMajor ins)
        {
            try
            {
                db.JobMajors.Add(ins);
                db.SaveChanges();
                return ins.JobID;
            }
            catch(Exception e)
            {
                return -1;
            }
        }
        public bool ConfirmJob(int jobID)
        {
            try
            {
                var job = db.JobMajors.Find(jobID);
                job.Status = true;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool DeleteJob(int jobID)
        {
            try
            {
                var job = db.JobMajors.Find(jobID);
                db.JobMajors.Remove(job);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<JobMajor> ReturnParentListByUser(String userId)
        {
            try
            {
                var list = db.UserMajors.Where(x => x.UserID == userId && x.MajorParent == null)
                                        .Select(x => x.MajorID).ToList();
                var model = db.JobMajors.Where(x => list.Contains(x.JobID)).ToList();
                return model;
            }
            catch
            {
                return null;
            }
        }
    }
}
