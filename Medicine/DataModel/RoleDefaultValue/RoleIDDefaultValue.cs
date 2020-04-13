using EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.RoleDefaultValue
{
    public class RoleIDDefaultValue
    {
        public int GetRoleID(R_RoleInfo_PowerInfo entity)
        {
            if (entity.RoleID.ToString() == null)
            {
                return entity.RoleID = 0;
            }else
            {
                return entity.RoleID;
            }
        }
    }
}
