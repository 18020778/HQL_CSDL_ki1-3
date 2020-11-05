namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JobMajor")]
    public partial class JobMajor
    {
        [Key]
        public int JobID { get; set; }

        [StringLength(50)]
        public string JobName { get; set; }

        public int? JobIDParent { get; set; }

        public bool? Status { get; set; }
    }
}
