namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OfferJob")]
    public partial class OfferJob
    {
        [Key]
        public Guid OfferID { get; set; }

        [Required]
        [StringLength(50)]
        public string EmployeeID { get; set; }

        public Guid EnterpriseID { get; set; }

        [Required]
        [StringLength(50)]
        public string OfferName { get; set; }

        [Required]
        public string OfferDescription { get; set; }

        [StringLength(250)]
        public string OfferImage { get; set; }

        public int Area { get; set; }

        [Required]
        public string JobAddress { get; set; }

        public int OfferSalary { get; set; }

        [StringLength(50)]
        public string Sex { get; set; }

        public int Amount { get; set; }

        [StringLength(50)]
        public string Bonus { get; set; }

        [Column(TypeName = "date")]
        public DateTime OfferCreateDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime OfferLimitDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ContactEmail { get; set; }

        public int? Applications { get; set; }

        public int? Views { get; set; }

        public int ExperienceRequest { get; set; }

        public int? LearningLevelRequest { get; set; }

        public int OfferMajor { get; set; }

        public int OfferPosition { get; set; }
    }
}
