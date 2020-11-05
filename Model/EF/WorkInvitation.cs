namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkInvitation")]
    public partial class WorkInvitation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkInvitationID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserID { get; set; }

        public Guid OfferID { get; set; }

        [Required]
        [StringLength(50)]
        public string StartDay { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        public string Note { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        [StringLength(50)]
        public string Salary { get; set; }
    }
}
