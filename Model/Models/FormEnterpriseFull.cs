using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class FormEnterpriseFull
    {
        public Guid EnterpriseID { set; get; }
        public String EnterpriseName { set; get; }
        public String ImageLogo { set; get; }
        public String NameArea { set; get; }
        public List<int> listJobId { set; get; }
    }
}