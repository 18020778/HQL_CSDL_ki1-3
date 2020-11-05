using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class EmployeeDao
    {
        CareerWeb db = null;
        public EmployeeDao()
        {
            db = new CareerWeb();
        }
        public bool InsertEmployee(Employee employee)
        {
            try
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<Employee> ListEmployees()
        {
            try
            {
                return db.Employees.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<Employee> ListEmployee(Guid enterpriseId)
        {
            return db.Employees.Where(x => x.EnterpriseID == enterpriseId).ToList();
        }
        public Employee FindById(String userID)
        {
            return db.Employees.Find(userID);
        }
        public bool Delete(String id)
        {
            try
            {
                var employee = db.Employees.Find(id);
                db.Employees.Remove(employee);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
