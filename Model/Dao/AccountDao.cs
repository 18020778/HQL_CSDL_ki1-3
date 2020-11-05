using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AccountDao
    {
        CareerWeb db = null;
        public AccountDao()
        {
            db = new CareerWeb();
        }
        public bool checkEmailExsit(string email)
        {
            var count = db.Accounts.Count(x => x.AccountName == email);
            if(count > 0)
            {
                return true;
            }
            return false;
        }
        public String createAccount(string email, string pass, string typeOfAccount)
        {
            var newAccount = new Account();
            newAccount.AccountName = email;
            newAccount.AccountPassword = pass;
            newAccount.CreateDate = DateTime.Now;
            newAccount.UserId = Guid.NewGuid().ToString();
            newAccount.VisitFirstTime = true;
            newAccount.TypeOfAccount = typeOfAccount;
            if(typeOfAccount == "User" || typeOfAccount == "Employee")
            {
                newAccount.Status = "Complete";
            }
            else
            {
                newAccount.Status = "Request";
            }
            db.Accounts.Add(newAccount);
            db.SaveChanges();
            return newAccount.UserId;
        }
        public Account FindAccountByUserId(String userId)
        {
            try
            {
                return db.Accounts.SingleOrDefault(x => x.UserId == userId);
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public Account FindAccountById(int accountId)
        {
            try
            {
                return db.Accounts.Find(accountId);
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public bool RemoveAccountByUserID(String id)
        {
            try
            {
                var rmAcc = db.Accounts.SingleOrDefault(x => x.UserId == id);
                db.Accounts.Remove(rmAcc);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool ChangeStatus(String id)
        {
            try{
                var acc = db.Accounts.SingleOrDefault(x => x.UserId == id);
                acc.Status = "Complete";
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public int CheckLogin(string userName, string passWord, out int userId)
        {
            var acc = db.Accounts.SingleOrDefault(x => x.AccountName == userName);
            userId = -1;
            if (acc == null)
            {
                return -1;
            }
            if (acc.AccountPassword != passWord)
            {
                return -2;
            }
            userId = acc.AccountId;
            if (acc.TypeOfAccount == "User") return 1;
            if (acc.TypeOfAccount == "Enterprise") return 2;
            if (acc.TypeOfAccount == "Employee") return 3;
            return 4;
        }
        public string GetName(int accountId)
        {
            var acc = db.Accounts.Find(accountId);
            if (acc.TypeOfAccount == "User")
            {
                return db.Users.SingleOrDefault(x => x.UserId == acc.UserId).UserName;
            }
            if(acc.TypeOfAccount == "Employee")
            {
                return db.Employees.SingleOrDefault(x => x.EmployeeID == acc.UserId).EmployeeName;
            }
            return "";
        }
       
    }
}
