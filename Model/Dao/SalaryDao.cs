using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SalaryDao
    {
        CareerWeb db = null;
        public SalaryDao()
        {
            db = new CareerWeb();
        }
        public List<Salary> ListSalary()
        {
            return db.Salaries.ToList();
        }
        public string NameSalary(int id)
        {
            return db.Salaries.Find(id).Amount;
        }
        public string AmountSalary(int id = 0)
        {
            if (id == 0)
            {
                return "Thỏa thuận";
            }
            return db.Salaries.Find(id).Amount;
        }
    }
}
