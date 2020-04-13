
using EFModel;
using System;
using InterfaceService.IServices;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedicineService.Services
{
    public class PowerInfoService:BaseServices<PowerInfo>,IPowerInfoService
    {
        #region
        //DbContext db = EFContextFactory.GetDbContext();
        //public int Add(PowerInfo entity)
        //{
        //    db.Set<PowerInfo>().Add(entity);//给entity打上添加标记，等到调用saveChanges()时，才去操作数据库
        //    return db.SaveChanges();//调用SaveChanges()时，才统一的去操作数据库，内置事物
        //}
        //public int Update(PowerInfo entity)
        //{
        //    //必须先查再改
        //    //先查：=>读作go to （lambda表达式），匿名函数
        //    PowerInfo pEntity = db.Set<PowerInfo>().Where(p => p.ID == entity.ID).FirstOrDefault();
        //    pEntity.HttpMethod = entity.HttpMethod;
        //    db.Entry(pEntity).State = EntityState.Modified; //打上修改标记
        //    return db.SaveChanges();
        //}
        //public int Delete(PowerInfo entity)
        //{
        //    //必须先查再删
        //    //先查：=>读作go to (lambda表达式),匿名函数
        //    PowerInfo pEntity = db.Set<PowerInfo>().Where(p => p.ID == entity.ID).FirstOrDefault();
        //    db.Set<PowerInfo>().Attach(pEntity);
        //    db.Set<PowerInfo>().Remove(pEntity);//打上删除标记
        //    return db.SaveChanges();
        //}

        ///// <summary>
        ///// 查询所有
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<PowerInfo> Select(Expression<Func<PowerInfo, bool>> whereLambda)
        //{
        //    IQueryable<PowerInfo> IQPowerInfo = db.Set<PowerInfo>().Where(whereLambda);
        //    return IQPowerInfo;
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
        //public IQueryable<PowerInfo> PageLoadEntity<s>(int pageIndex, int pageSize, Expression<Func<PowerInfo, bool>> whereLambda, Expression<Func<PowerInfo, s>> orderby, bool isChecked)
        //{
        //    var temp = db.Set<PowerInfo>().Where(whereLambda);
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
