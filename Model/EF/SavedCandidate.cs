namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SavedCandidate")]
    public partial class SavedCandidate
    {
        [Key]
        [Column(Order = 0)]
        public Guid EnterpriseID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string UserID { get; set; }

        [StringLength(50)]
        public string CreateDate { get; set; }
    }
}
