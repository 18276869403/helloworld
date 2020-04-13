namespace EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MarketInfo")]
    public partial class MarketInfo
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string MedicineID { get; set; }

        public int? MarketNumber { get; set; }

        public int? UserID { get; set; }

        public DateTime? SellTime { get; set; }

        public int PriceID { get; set; }
    }
}
