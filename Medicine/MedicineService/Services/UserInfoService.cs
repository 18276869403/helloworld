﻿
using EFModel;
using InterfaceService.IServices;
using MedicineService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedicineService.Services
{
    public class UserInfoService:BaseServices<UserInfo>,IUserInfoService
    {
        #region 源代码
        //DbContext db = EFContextFactory.GetDbContext();
        //public int Add(UserInfo entity)
        //{
        //    db.Set<UserInfo>().Add(entity);//给entity打上添加标记，等到调用saveChanges()时，才去操作数据库
        //    return db.SaveChanges();//调用SaveChanges()时，才统一的去操作数据库，内置事物
        //}
        //public int Update(UserInfo entity)
        //{
        //    //必须先查再改
        //    //先查：=>读作go to （lambda表达式），匿名函数
        //    UserInfo uEntity = db.Set<UserInfo>().Where(u => u.ID == entity.ID).FirstOrDefault();
        //    uEntity.UserName = entity.UserName;
        //    db.Entry(uEntity).State = EntityState.Modified; //打上修改标记
        //    return db.SaveChanges();
        //}
        //public int Delete(UserInfo entity)
        //{
        //    //必须先查再删
        //    //先查：=>读作go to (lambda表达式),匿名函数
        //    UserInfo uEntity = db.Set<UserInfo>().Where(u => u.ID == entity.ID).FirstOrDefault();
        //    db.Set<UserInfo>().Attach(uEntity);
        //    db.Set<UserInfo>().Remove(uEntity);//打上删除标记
        //    return db.SaveChanges();
        //}

        ///// <summary>
        ///// 查询所有
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<UserInfo> Select(Expression<Func<UserInfo, bool>> lambdaWhere)
        //{
        //    IQueryable<UserInfo> IQUserInfo = db.Set<UserInfo>().Where(lambdaWhere);
        //    return IQUserInfo;
        //}

        ///// <summary>
        ///// 分页排序
        ///// </summary>
        ///// <typeparam name="s"></typeparam>
        ///// <param name="pageIndex">页码数</param>
        ///// <param name="pageSize">每页显示多少条数据</param>
        ///// <param name="whereLambda">条件</param>
        ///// <param name="orderby">排序条件</param>
        ///// <param name="isChecked">是否升序排序</param>
        ///// <returns></returns>
        //public IQueryable<UserInfo> PageLoadEntity<s>(int pageIndex, int pageSize, Expression<Func<UserInfo, bool>> whereLambda, Expression<Func<UserInfo, s>> orderby, bool isChecked)
        //{
        //    var temp = db.Set<UserInfo>().Where(whereLambda);
        //    if (isChecked)
        //    {
        //        temp = temp.OrderBy(orderby).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //    }
        //    else
        //    {
        //        temp = temp.OrderByDescending(orderby).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //    }
        //    return temp;
        //}
        #endregion
    }
}
