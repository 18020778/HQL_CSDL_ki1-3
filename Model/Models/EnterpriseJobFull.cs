using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class EnterpriseJobFull
    {
        public int JobID { get; set; }
        public string JobName { get; set; }
        public bool Status { get; set; }
        public Guid EnterpriseID { get; set; }
    }
}
