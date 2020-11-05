using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class AppliedCandidateInfo
    {
        public Guid EnterpriseID { get; set; }

        public String CandidateID { get; set; }
        public String CandidateName { get; set; }

        public Guid OfferID { get; set; }
        public String OfferName { get; set; }

        public String Salary { get; set; }
        public string Status { get; set; }

        public string CreateDate { get; set; }
    }
}
