namespace EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MedicineInfo")]
    public partial class MedicineInfo
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ChineseName { get; set; }

        [Required]
        [StringLength(50)]
        public string ForeignName { get; set; }

        [Required]
        [StringLength(50)]
        public string MedicineID { get; set; }

        [Required]
        [StringLength(255)]
        public string M_Function { get; set; }

        [Required]
        [StringLength(255)]
        public string Ingredient { get; set; }

        public int ClassifyID { get; set; }

        public int DosageID { get; set; }

        [StringLength(50)]
        public string Etalon { get; set; }

        [Required]
        [StringLength(255)]
        public string Taboo { get; set; }

        [Required]
        [StringLength(255)]
        public string Notes { get; set; }

        [StringLength(255)]
        public string Pharmacology { get; set; }

        public int RepositID { get; set; }

        public int PackID { get; set; }

        public int? EnterCompanyID { get; set; }
    }
}
