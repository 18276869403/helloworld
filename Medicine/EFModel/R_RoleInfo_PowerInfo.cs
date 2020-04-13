namespace EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class R_RoleInfo_PowerInfo
    {
        public int ID { get; set; }

        public int PowerID { get; set; }

        public int RoleID { get; set; }
    }
}
