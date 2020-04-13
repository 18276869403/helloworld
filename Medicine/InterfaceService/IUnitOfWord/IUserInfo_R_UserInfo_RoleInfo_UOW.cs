using EFModel;
using InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceService.IUnitOfWord
{
    public interface IUserInfo_R_UserInfo_RoleInfo_UOW:IBaseServices_UOW<UserInfo,RoleInfo>
    {
        /// <summary>
        /// 删除用户同时删除用户角色表中的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int UserInfo_R_UserInfo_RoleInfo_Delete(int id);

        /// <summary>
        /// 功能暂未使用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int UserInfoAddAndDel(int num, UserInfo model);
    }
}
