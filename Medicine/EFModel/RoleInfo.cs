namespace EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoleInfo")]
    public partial class RoleInfo
    {
        public int ID { get; set; }

        public int RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public DateTime AddTime { get; set; }

        public int DelFlag { get; set; }
    }
}
