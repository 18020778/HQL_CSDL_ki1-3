namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OfferJobSkill")]
    public partial class OfferJobSkill
    {
        [Key]
        [Column(Order = 0)]
        public Guid OfferID { get; set; }

        public int ParentMajor { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChildMajor { get; set; }
    }
}
