using EFModel;
using InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceService.IUnitOfWord
{
    public interface IRoleInfo_R_RoleInfo_PowerInfo_UOW:IBaseServices_UOW<RoleInfo,PowerInfo>
    {
        int RoleInfo_R_RoleInfo_PowerInfo_Delete(int id);
    }
}
