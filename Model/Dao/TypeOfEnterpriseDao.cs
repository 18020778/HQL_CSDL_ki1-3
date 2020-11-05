using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class TypeOfEnterpriseDao
    {
        CareerWeb db = null;
        public TypeOfEnterpriseDao()
        {
            db = new CareerWeb();
        }
        public List<TypeOfEnterprise> ReturnList()
        {
            return db.TypeOfEnterprises.ToList();
        }
    }
}
