using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class EnterpriseAreaDao
    {
        CareerWeb db = null;
        public EnterpriseAreaDao()
        {
            db = new CareerWeb();
        }
        public List<EnterpriseArea> ListEnterpriseArea()
        {
            return db.EnterpriseAreas.ToList();
        }
        public bool Insert(EnterpriseArea ins)
        {
            try
            {
                db.EnterpriseAreas.Add(ins);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<EnterpriseAreaFull> ReturnList(Guid enterpriseID)
        {
            try
            {
                var model = (from a in db.Areas
                             join b in db.EnterpriseAreas
                             on a.AreaId equals b.AreaID
                             where  b.EnterpriseId == enterpriseID
                             select new
                             {
                                 AreaID = a.AreaId,
                                 EnterpriseID = b.EnterpriseId,
                                 AreaName = a.NameArea
                             }).AsEnumerable().Select(x => new EnterpriseAreaFull()
                             {
                                 AreaID = x.AreaID,
                                 AreaName = x.AreaName,
                                 EnterpriseID = x.EnterpriseID
                             });
                return model.ToList();
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public bool RemoveEntArea(Guid id)
        {
            try
            {
                var listEntArea = db.EnterpriseAreas.Where(x => x.EnterpriseId == id).ToList();
                foreach (var item in listEntArea)
                {
                    db.EnterpriseAreas.Remove(item);
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
