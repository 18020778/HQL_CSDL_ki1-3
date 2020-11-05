using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserExperienceDao
    {
        CareerWeb db = null;
        public UserExperienceDao()
        {
            db = new CareerWeb();
        }
        public int Insert(UserExperience us)
        {
            try
            {
                db.UserExperiences.Add(us);
                db.SaveChanges();
                return us.ID;
            }
            catch(Exception e)
            {
                return -1;
            }
        }
        public bool Remove(int id)
        {
            try
            {
                var removeUs = db.UserExperiences.Find(id);
                db.UserExperiences.Remove(removeUs);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool Modify(int id, UserExperience newUs)
        {
            try
            {
                var modifyUs = db.UserExperiences.Find(id);
                modifyUs.PositionID = newUs.PositionID;
                modifyUs.EnterpriseName = newUs.EnterpriseName;
                modifyUs.StartTime = newUs.StartTime;
                modifyUs.EndTime = newUs.EndTime;
                modifyUs.Description = newUs.Description;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<UserExperience> ListByUser(String userID)
        {
            return db.UserExperiences.Where(x => x.UserID == userID).ToList();
        }
    }
}
