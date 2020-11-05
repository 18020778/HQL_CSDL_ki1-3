namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeOfEnterprise")]
    public partial class TypeOfEnterprise
    {
        public int TypeOfEnterpriseID { get; set; }

        [Required]
        [StringLength(250)]
        public string NameOfEnterprise { get; set; }
    }
}
