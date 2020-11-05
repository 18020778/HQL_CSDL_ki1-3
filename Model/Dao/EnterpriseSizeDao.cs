using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class EnterpriseSizeDao
    {
        CareerWeb db = null;
        public EnterpriseSizeDao()
        {
            db = new CareerWeb();
        }
        public List<EnterpriseSize> ReturnList()
        {
            return db.EnterpriseSizes.ToList();
        }
    }
}
