namespace EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DosageType")]
    public partial class DosageType
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string DosageName { get; set; }

        public int FatheID { get; set; }
    }
}
