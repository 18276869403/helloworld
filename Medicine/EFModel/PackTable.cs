namespace EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PackTable")]
    public partial class PackTable
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string PackName { get; set; }
    }
}
