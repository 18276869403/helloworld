namespace EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceInfo")]
    public partial class PriceInfo
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string MedicineID { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
    }
}
