using EFModel;
using InterfaceService.IUnitOfWord;
using MedicineService.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineService.UnitOfWord
{
    public class RoleInfo_R_RoleInfo_PowerInfo_UOW: BaseServices_UOW<RoleInfo,PowerInfo>,IRoleInfo_R_RoleInfo_PowerInfo_UOW
    {
        /// <summary>
        /// 删除角色信息，同时删除角色权限表中的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int RoleInfo_R_RoleInfo_PowerInfo_Delete(int id)
        {
            RoleInfo roleModel = RoleInfoService.Query(u => u.RoleID == id).FirstOrDefault();
            RoleInfoService.DeleteFlag(roleModel);
            List<R_RoleInfo_PowerInfo> r_RoleInfo_PowerInfoList = R_RoleInfo_PowerInfoService.Query(u => u.RoleID == id).ToList();
            if(r_RoleInfo_PowerInfoList.Count > 0)
            {
                foreach (var item in r_RoleInfo_PowerInfoList)
                {
                    R_RoleInfo_PowerInfoService.DeleteFlag(item);
                }
            }
            return db.SaveChanges();
        }
    }
}
