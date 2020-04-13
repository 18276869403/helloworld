namespace EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inventory")]
    public partial class Inventory
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string MedicineID { get; set; }

        public int Number { get; set; }
    }
}
