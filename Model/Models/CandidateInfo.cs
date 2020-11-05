using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class CandidateInfo
    {
        public String UserId { get; set; }

        public String UserName { get; set; }

        public String UserImage { get; set; }

        public String UserExperience { get; set; }

        public String UserSalary { get; set; }

        public String UserArea { get; set; }

        public List<String> UserMajorName { get; set; }
    }
}
