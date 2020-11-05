using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWeb.Models
{
    public class InsertEnterpriseForm
    {
        public Guid Id { set; get;}
        public string Name { set; get; }
        public string Logo { set; get; }
        public int EstablishYear { set; get; }
        public string Email { set; get; }
        public int Size { set; get; }
        public int Type { set; get; }
        public string Mobile { set; get; }
        public int[] ListArea { set; get; }
        public int[] ListJobImp { set; get; }
        public int[] ListJobSub { set; get; }
        public string[] NewJobImp { set; get; }
        public string[] NewJobSub { set; get; }
    }
}