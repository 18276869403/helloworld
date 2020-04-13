namespace EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(50)]
        public string DepartmentName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
    }
}
