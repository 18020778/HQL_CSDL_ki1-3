using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AreaDao
    {
        CareerWeb db = null;
        public AreaDao()
        {
            db = new CareerWeb();
        }
        public List<Area> ListArea()
        {
            return db.Areas.ToList();
        }

        public string FindNameArea(int areaID)
        {
            return db.Areas.Find(areaID).NameArea;
        }
    }
}
