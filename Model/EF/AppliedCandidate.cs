namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppliedCandidate")]
    public partial class AppliedCandidate
    {
        [Key]
        [Column(Order = 0)]
        public Guid EnterpriseID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string UserID { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid OfferID { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(10)]
        public string CreateDate { get; set; }

        [StringLength(250)]
        public string LinkCV { get; set; }
    }
}
