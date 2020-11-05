using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ShowFullEnterprise
    {
        public Guid EnterpriseID { get; set; }
        public string EnterpriseName {get; set;}
        public string ImageLogo { get; set; }
        public string AmountSize { get; set; }
        public string NameOfEnterprise { get; set; }
        public int EstablishYear { get; set; }
        public string NameArea { get; set; }
        public List<int> listJobId { set; get; }
        public List<OfferJob> listContainerJob { set; get; }
        public string Description { set; get; }
    }
}
