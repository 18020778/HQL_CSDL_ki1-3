namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserForeignLanguage")]
    public partial class UserForeignLanguage
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserID { get; set; }

        public int LanguageID { get; set; }

        [Required]
        [StringLength(50)]
        public string LanguageLevel { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
    }
}
