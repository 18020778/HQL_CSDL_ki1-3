namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [StringLength(50)]
        public string EmployeeID { get; set; }

        public Guid EnterpriseID { get; set; }

        [Required]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDay { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Mobile { get; set; }

        public int Position { get; set; }

        [Required]
        [StringLength(50)]
        public string Sex { get; set; }
    }
}
