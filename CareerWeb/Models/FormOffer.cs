using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWeb.Models
{
    public class FormOffer
    {
        public string typeAction { set; get; }
        public Guid saveID { set; get; }
        public string offerName { set; get; }
        public int offerMajor { set; get; }
        public int offerPosition { set; get; }
        public int offerSalary { set; get; }
        public int offerArea { set; get; }
        public string offerEmail { set; get; }
        public int offerExperience { set; get; }
        public int offerLearning { set; get; }
        public int offerAmount { set; get; }
        public string offerLimitDate { set; get; }
        public string offerDescription { set; get; }
        public string offerGender { set; get; }
        public string offerAddress { set; get; }
        public HttpPostedFileBase offerImage { get; set; }
        public List<int> offerListSkillId { set; get; }
        public List<int> offerListSkillParent { set; get; }
    }
}