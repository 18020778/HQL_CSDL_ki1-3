using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ShowFullJob
    {
        public Guid EnterpriseID { get; set; }
        public string OfferName { get; set; }
        public string OfferDescription { get; set; }
        public string JobAddress { get; set; }
        public string Amount { get; set; }
        public string Bonus { get; set; }
        public int AmountNum { get; set; }
        public string Sex { get; set; }
        public string NameArea { get; set; }
        public DateTime OfferLimitDate { get; set; }
        public int? Applications { get; set; }
        public string Time { get; set; }
        public string NameLevel { get; set; }
        public string NamePosition { get; set; }
        public string EnterpriseName { get; set; }
        public string AmountSize { get; set; }
        public string ImageLogo { get; set; }
        public string NameOfEnterprise { get; set; }
        public List<int> listJobId { get; set; }
    }
}
