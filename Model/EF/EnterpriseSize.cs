namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EnterpriseSize")]
    public partial class EnterpriseSize
    {
        [Key]
        public int SizeID { get; set; }

        [StringLength(20)]
        public string AmountSize { get; set; }
    }
}
