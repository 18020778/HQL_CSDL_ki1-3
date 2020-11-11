using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class FormEnterpriseFull
    {
        public Guid EnterpriseID { set; get; }
        public string EnterpriseName { set; get; }
        public string ImageLogo { set; get; }
        public string NameArea { set; get; }
        public string listJob { set; get; }
    }
}