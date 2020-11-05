using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWeb.Models
{
    public class FileUploadModel
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}