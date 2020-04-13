using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataModels
{
    public class Role_Power_Models
    {
        public string RoleID { get; set; }

        public int DelFlag { get; set; }

        [Required]
        [StringLength(50)]
        public string roleDescription { get; set; }

        public int PowerID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [Required]
        [StringLength(50)]
        public string powerDescription { get; set; }

        [Required]
        [StringLength(50)]
        public string ActionUrl { get; set; }

        [Required]
        [StringLength(50)]
        public string MenuName { get; set; }
    }
}
