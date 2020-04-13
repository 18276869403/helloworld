namespace EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class R_UserInfo_RoleInfo
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int RoleID { get; set; }
    }
}
