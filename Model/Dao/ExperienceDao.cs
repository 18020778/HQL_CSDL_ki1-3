using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ExperienceDao
    {
        CareerWeb db = null;
        public ExperienceDao()
        {
            db = new CareerWeb();
        }
        public List<Experience> ListExperiences()
        {
            return db.Experiences.ToList();
        }
    }
}
