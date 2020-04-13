using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataModels
{
    public class UserInfoModels
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserPhone { get; set; }

        public int DepartmentID { get; set; }

        public int UserID { get; set; }

        public int Sex { get; set; }

        public DateTime AddTime { get; set; }

        public int DelFlag { get; set; }

        public string DepartmentName { get; set; }

        public int RoleID { get; set; }

        public string RoleName { get; set; }
    }
}
