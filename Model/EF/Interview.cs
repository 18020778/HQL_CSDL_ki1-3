namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Interview")]
    public partial class Interview
    {
        public int InterviewID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserID { get; set; }

        public Guid OfferID { get; set; }

        [Required]
        [StringLength(50)]
        public string Time { get; set; }

        [Required]
        [StringLength(50)]
        public string Date { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        [StringLength(50)]
        public string EmployeeID { get; set; }

        public string Address { get; set; }

        public string Note { get; set; }
    }
}
