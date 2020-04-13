namespace EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EnterInfo")]
    public partial class EnterInfo
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string MedicineID { get; set; }

        public int MarketNumber { get; set; }

        public int EnterCompanyID { get; set; }

        public DateTime EnterDate { get; set; }

        public DateTime EndDate { get; set; }

        [Column(TypeName = "money")]
        public decimal EnterPrice { get; set; }
    }
}
