namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Enterprise")]
    public partial class Enterprise
    {
        public Guid EnterpriseID { get; set; }

        [Required]
        [StringLength(250)]
        public string EnterpriseName { get; set; }

        [Required]
        public string ImageLogo { get; set; }

        public int EstablishYear { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public int EnterpriseSize { get; set; }

        public int TypeOfEnterprise { get; set; }

        [Required]
        [StringLength(20)]
        public string Mobile { get; set; }

        public bool? Status { get; set; }

        [StringLength(10)]
        public string Code { get; set; }
    }
}
