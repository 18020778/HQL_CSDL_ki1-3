using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class InterviewInfo
    {
        public int InterviewID { get; set; }

        public Guid EnterpriseID { get; set; }

        public String CandidateID { get; set; }

        public String CandidateName { get; set; }
        public Guid OfferID { get; set; }
        public String OfferName { get; set; }

        public string Time { get; set; }

        public string Date { get; set; }

        public String Address { get; set; }
 
    }
}
