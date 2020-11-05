using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserCertificateDao
    {
        CareerWeb db = null;
        public UserCertificateDao()
        {
            db = new CareerWeb();
        }
        public int Insert(UserCertificate uc)
        {
            try
            {
                db.UserCertificates.Add(uc);
                db.SaveChanges();
                return uc.ID;
            }
            catch
            {
                return -1;
            }
        }
        public bool Remove(int id)
        {
            try
            {
                var removeUc = db.UserCertificates.Find(id);
                db.UserCertificates.Remove(removeUc);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Modify(int id, UserCertificate newUc)
        {
            try
            {
                var modifyUc = db.UserCertificates.Find(id);
                modifyUc.NameCertificate = newUc.NameCertificate;
                if (newUc.ImageCertificate != null)
                {
                    modifyUc.ImageCertificate = newUc.ImageCertificate;
                }
                modifyUc.GetDate = newUc.GetDate;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<UserCertificate> ListByUser(String userID)
        {
            return db.UserCertificates.Where(x => x.UserId == userID).ToList();
        }
    }
}
