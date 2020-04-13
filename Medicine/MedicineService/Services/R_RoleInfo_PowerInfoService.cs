
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
    public class R_RoleInfo_PowerInfoService : BaseServices<R_RoleInfo_PowerInfo>,IR_RoleInfo_PowerInfoService
    {
        #region
        //DbContext db = EFContextFactory.GetDbContext();
        //public int Add(R_RoleInfo_PowerInfo entity)
        //{
        //    db.Set<R_RoleInfo_PowerInfo>().Add(entity);//给entity打上添加标记，等到调用saveChanges()时，才去操作数据库
        //    return db.SaveChanges();//调用SaveChanges()时，才统一的去操作数据库，内置事物
        //}
        //public int Update(R_RoleInfo_PowerInfo entity)
        //{
        //    //必须先查再改
        //    //先查：=>读作go to （lambda表达式），匿名函数
        //    R_RoleInfo_PowerInfo ripEntity = db.Set<R_RoleInfo_PowerInfo>().Where(rip => rip.ID == entity.ID).FirstOrDefault();
        //    ripEntity.PowerID = entity.PowerID;
        //    db.Entry(ripEntity).State = EntityState.Modified; //打上修改标记
        //    return db.SaveChanges();
        //}
        //public int Delete(R_RoleInfo_PowerInfo entity)
        //{
        //    //必须先查再删
        //    //先查：=>读作go to (lambda表达式),匿名函数
        //    R_RoleInfo_PowerInfo ripEntity = db.Set<R_RoleInfo_PowerInfo>().Where(rip => rip.ID == entity.ID).FirstOrDefault();
        //    db.Set<R_RoleInfo_PowerInfo>().Attach(ripEntity);
        //    db.Set<R_RoleInfo_PowerInfo>().Remove(ripEntity);//打上删除标记
        //    return db.SaveChanges();
        //}

        ///// <summary>
        ///// 查询所有
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<R_RoleInfo_PowerInfo> Select(Expression<Func<R_RoleInfo_PowerInfo, bool>> whereLambda)
        //{
        //    IQueryable<R_RoleInfo_PowerInfo> IQR_RoleInfo_PowerInfo = db.Set<R_RoleInfo_PowerInfo>().Where(whereLambda);
        //    return IQR_RoleInfo_PowerInfo;
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
        //public IQueryable<R_RoleInfo_PowerInfo> PageLoadEntity<s>(int pageIndex, int pageSize, Expression<Func<R_RoleInfo_PowerInfo, bool>> whereLambda, Expression<Func<R_RoleInfo_PowerInfo, s>> orderby, bool isChecked)
        //{
        //    var temp = db.Set<R_RoleInfo_PowerInfo>().Where(whereLambda);
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
