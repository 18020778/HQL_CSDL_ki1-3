namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EnterpriseArea")]
    public partial class EnterpriseArea
    {
        [Key]
        [Column(Order = 0)]
        public Guid EnterpriseId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AreaID { get; set; }

        public string DetailDescription { get; set; }
    }
}
