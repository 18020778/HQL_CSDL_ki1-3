using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserLearningDao
    {
        CareerWeb db = null;
        public UserLearningDao()
        {
            db = new CareerWeb();
        }
        public List<UserLearning> ListUserLearning()
        {
            return db.UserLearnings.ToList();
        }
    }
}
