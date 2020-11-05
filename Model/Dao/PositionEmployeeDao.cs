using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class PositionEmployeeDao
    {
        CareerWeb db = null;
        public PositionEmployeeDao()
        {
            db = new CareerWeb();
        }
        public List<PositionEmployee> ReturnList()
        {
            return db.PositionEmployees.ToList();
        }
        public string NamePosition(int id)
        {
            return db.PositionEmployees.Find(id).NamePosition;
        }
    }
}
