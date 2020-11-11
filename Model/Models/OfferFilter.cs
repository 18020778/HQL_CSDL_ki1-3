using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class OfferFilter
    {
        public Guid OfferID { get; set; }
        public string OfferName { get; set; }
        public Guid EnterpriseID { get; set; }
        public string EnterpriseName { get; set; }
        public string JobAddress { get; set; }
        public int OfferSalary { get; set; }
        public string AmountSalary { get; set; }
        public DateTime OfferLimitDate { get; set; }
        public string Bonus { get; set; }
        public string OfferImage { get; set; }
    }
}
