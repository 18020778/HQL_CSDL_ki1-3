namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Area")]
    public partial class Area
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AreaId { get; set; }

        [Required]
        [StringLength(50)]
        public string NameArea { get; set; }
    }
}
