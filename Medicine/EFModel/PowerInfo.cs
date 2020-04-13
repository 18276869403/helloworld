namespace EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PowerInfo")]
    public partial class PowerInfo
    {
        public int ID { get; set; }

        public int PowerID { get; set; }

        [Required]
        [StringLength(50)]
        public string MenuName { get; set; }

        [Required]
        [StringLength(50)]
        public string ActionUrl { get; set; }

        public int IsMenu { get; set; }

        public int ParentID { get; set; }

        [Required]
        [StringLength(50)]
        public string MenuIconUrl { get; set; }

        public int HttpMethod { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
