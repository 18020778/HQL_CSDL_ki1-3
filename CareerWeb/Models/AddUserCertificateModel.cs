using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWeb.Models
{
    public class AddUserCertificateModel
    {
        public HttpPostedFileBase ImageFile { get; set; }
        public int ID { set; get; }
        public string Name { set; get; }
        public string Date { set; get; }
    }
}