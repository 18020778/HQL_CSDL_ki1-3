namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserLearning")]
    public partial class UserLearning
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserID { get; set; }

        public int StudyLevel { get; set; }

        public int Major { get; set; }

        [Required]
        [StringLength(10)]
        public string TimeStart { get; set; }

        [StringLength(10)]
        public string TimeEnd { get; set; }

        public Guid? SchoolID { get; set; }

        [StringLength(100)]
        public string SchoolName { get; set; }
    }
}
