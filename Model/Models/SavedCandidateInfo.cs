using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class SavedCandidateInfo
    {
        public Guid EnterpriseID { get; set; }

        public String CandidateID { get; set; }
        public String CandidateName { get; set; }
        public String DesiredJob { get; set; }
        public String Salary { get; set; }
        public string CreateDate { get; set; }
    }
}
