using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class LanguageDao
    {
        CareerWeb db = null;
        public LanguageDao()
        {
            db = new CareerWeb();
        }
        public List<Language> ReturnList()
        {
            return db.Languages.ToList();
        }
        public string NameLanguage(int id)
        {
            return db.Languages.Find(id).LanguageName;
        }
    }
}
