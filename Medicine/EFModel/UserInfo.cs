namespace EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserInfo")]
    public partial class UserInfo
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(32)]
        public string UserPwd { get; set; }

        [Required]
        [StringLength(50)]
        public string UserPhone { get; set; }

        public int DepartmentID { get; set; }

        public int UserID { get; set; }

        public int Sex { get; set; }

        public DateTime AddTime { get; set; }

        public int DelFlag { get; set; }
    }
}
