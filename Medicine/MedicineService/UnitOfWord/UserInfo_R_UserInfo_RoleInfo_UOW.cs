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
    public class UserInfo_R_UserInfo_RoleInfo_UOW<T,S>:BaseServices_UOW<UserInfo,RoleInfo>,IUserInfo_R_UserInfo_RoleInfo_UOW
    {
        /// <summary>
        /// 删除用户同时删除用户角色表中的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UserInfo_R_UserInfo_RoleInfo_Delete(int id)
        {
            //1.先查询UserInfo表
            UserInfo userInfoEntity = UserInfoService.Query(u => u.UserID == id).FirstOrDefault();
            //再打上标记
            UserInfoService.DeleteFlag(userInfoEntity);
            //2.先查询
            List<R_UserInfo_RoleInfo> r_UserInfo_RoleInfoList = R_UserInfo_RoleInfoService.Query(u => u.UserID == userInfoEntity.UserID).ToList();
            if (r_UserInfo_RoleInfoList.Count > 0)
            {
                foreach (var item in r_UserInfo_RoleInfoList)
                {
                    R_UserInfo_RoleInfoService.DeleteFlag(item);
                }
            }
            //再打上删除标记
            return db.SaveChanges();
        }

        /// <summary>
        /// 功能暂未实现
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UserInfoAddAndDel(int num, UserInfo model)
        {
            //先查
            UserInfo entity = UserInfoService.Query(u => u.ID == num).FirstOrDefault();
            //打上删除标记
            UserInfoService.DeleteFlag(entity);
            //声明初始化器
            UserInfo entity1 = new UserInfo
            {
                //给属性赋值
                ID = model.ID,
                UserID = model.UserID,
            };
            //打上添加标记
            UserInfoService.AddTo(entity1);

            //调用SaveChanges()时，才统一的去操作数据库，内置事物
            return db.SaveChanges();
        }
    }
}
