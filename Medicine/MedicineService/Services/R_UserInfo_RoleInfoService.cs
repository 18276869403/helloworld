
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
    public class R_UserInfo_RoleInfoService : BaseServices<R_UserInfo_RoleInfo>,IR_UserInfo_RoleInfoService
    {
        #region
        //DbContext db = EFContextFactory.GetDbContext();
        //public int Add(R_UserInfo_RoleInfo entity)
        //{
        //    db.Set<R_UserInfo_RoleInfo>().Add(entity);//给entity打上添加标记，等到调用saveChanges()时，才去操作数据库
        //    return db.SaveChanges();//调用SaveChanges()时，才统一的去操作数据库，内置事物
        //}
        //public int Update(R_UserInfo_RoleInfo entity)
        //{
        //    //必须先查再改
        //    //先查：=>读作go to （lambda表达式），匿名函数
        //    R_UserInfo_RoleInfo rurEntity = db.Set<R_UserInfo_RoleInfo>().Where(rur => rur.ID == entity.ID).FirstOrDefault();
        //    rurEntity.RoleID= entity.RoleID;
        //    db.Entry(rurEntity).State = EntityState.Modified; //打上修改标记
        //    return db.SaveChanges();
        //}
        //public int Delete(R_UserInfo_RoleInfo entity)
        //{
        //    //必须先查再删
        //    //先查：=>读作go to (lambda表达式),匿名函数
        //    R_UserInfo_RoleInfo rurEntity = db.Set<R_UserInfo_RoleInfo>().Where(rur => rur.ID == entity.ID).FirstOrDefault();
        //    db.Set<R_UserInfo_RoleInfo>().Attach(rurEntity);
        //    db.Set<R_UserInfo_RoleInfo>().Remove(rurEntity);//打上删除标记
        //    return db.SaveChanges();
        //}

        ///// <summary>
        ///// 查询所有
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<R_UserInfo_RoleInfo> Select(Expression<Func<R_UserInfo_RoleInfo, bool>> whereLambda)
        //{
        //    IQueryable<R_UserInfo_RoleInfo> IQR_UserInfo_RoleInfo = db.Set<R_UserInfo_RoleInfo>().Where(whereLambda);
        //    return IQR_UserInfo_RoleInfo;
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
        //public IQueryable<R_UserInfo_RoleInfo> PageLoadEntity<s>(int pageIndex, int pageSize, Expression<Func<R_UserInfo_RoleInfo, bool>> whereLambda, Expression<Func<R_UserInfo_RoleInfo, s>> orderby, bool isChecked)
        //{
        //    var temp = db.Set<R_UserInfo_RoleInfo>().Where(whereLambda);
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
