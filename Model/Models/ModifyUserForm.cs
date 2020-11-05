using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ModifyUserForm
    {
        public string userName { set; get; }
        public string userDob { set; get; }
        public string userGender { set; get; }
        public string userEmail { set; get; }
        public string userMobile { set; get; }
        public int userArea { set; get; }
        public string userAddress { set; get; }
    }
}
