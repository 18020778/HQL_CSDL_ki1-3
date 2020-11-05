using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class LevelLearningDao
    {
        CareerWeb db = null;
        public LevelLearningDao()
        {
            db = new CareerWeb();
        }
        public List<LevelLearning> ReturnList()
        {
            return db.LevelLearnings.ToList();
        }
    }
}
