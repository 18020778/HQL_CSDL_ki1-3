using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserForeignLanguageDao
    {
        CareerWeb db = null;
        public UserForeignLanguageDao()
        {
            db = new CareerWeb();
        }
        public int Insert(UserForeignLanguage uf)
        {
            try
            {
                db.UserForeignLanguages.Add(uf);
                db.SaveChanges();
                return uf.ID;
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
                var removeUf = db.UserForeignLanguages.Find(id);
                db.UserForeignLanguages.Remove(removeUf);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Modify(int id, UserForeignLanguage newUf)
        {
            try
            {
                var modifyUf = db.UserForeignLanguages.Find(id);
                modifyUf.LanguageID = newUf.LanguageID;
                modifyUf.LanguageLevel = newUf.LanguageLevel;
                modifyUf.Description = newUf.Description;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<UserForeignLanguage> ListByUser(String userID)
        {
            return db.UserForeignLanguages.Where(x => x.UserID == userID).ToList();
        }
        public List<UserForeignLanguage> ReturnList()
        {
            return db.UserForeignLanguages.ToList();
        }

    }
}
