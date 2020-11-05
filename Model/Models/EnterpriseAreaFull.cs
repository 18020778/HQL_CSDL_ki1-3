using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class EnterpriseAreaFull
    {
        public Guid EnterpriseID { get; set; }
        public int AreaID { get; set; }
        public string AreaName { get; set; }
    }
}
