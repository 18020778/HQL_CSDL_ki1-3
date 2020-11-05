using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ShowInfoCandidate
    {
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public DateTime UserBirthDay { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public string UserMobile { get; set; }
        public List<string> listJob { get; set; }
        public string Amount { get; set; }
        public string NamePosition { get; set; }
        public string UserArea { get; set; }
        public string StudyLevel { get; set; }
        public string SchoolName { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string JobName { get; set; }
    }
}
