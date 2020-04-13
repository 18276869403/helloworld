namespace EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EnterCompany")]
    public partial class EnterCompany
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string E_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        public int E_Zip { get; set; }

        [Required]
        [StringLength(50)]
        public string E_Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string E_Fax { get; set; }

        [Required]
        [StringLength(50)]
        public string Register_Address { get; set; }

        [Required]
        [StringLength(50)]
        public string E_Url { get; set; }
    }
}
