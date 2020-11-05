using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OfferJobSkillDao
    {
        CareerWeb db = null;
        public OfferJobSkillDao()
        {
            db = new CareerWeb();
        }
        public List<OfferJobSkill> ReturnList()
        {
            return db.OfferJobSkills.ToList();
        }
        public bool Insert(OfferJobSkill of)
        {
            try
            {
                db.OfferJobSkills.Add(of);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool DeleteSkill(Guid offerID, int skillJob)
        {
            try
            {
                var off = db.OfferJobSkills.Find(offerID, skillJob);
                db.OfferJobSkills.Remove(off);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<OfferJobSkill> ListByOffer(Guid offerID)
        {
            return db.OfferJobSkills.Where(x => x.OfferID == offerID).ToList();
        }
    }
}
