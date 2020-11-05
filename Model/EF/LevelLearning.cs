namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LevelLearning")]
    public partial class LevelLearning
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string NameLevel { get; set; }
    }
}
