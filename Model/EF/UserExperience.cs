namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserExperience")]
    public partial class UserExperience
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserID { get; set; }

        public int PositionID { get; set; }

        [Required]
        [StringLength(250)]
        public string EnterpriseName { get; set; }

        [Required]
        [StringLength(10)]
        public string StartTime { get; set; }

        [StringLength(10)]
        public string EndTime { get; set; }

        public string Description { get; set; }
    }
}
