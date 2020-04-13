
using EFModel;
using MedicineService;
using InterfaceService.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedicineService.Services
{
    public class EnterInfoService:BaseServices<EnterInfo>,IEnterInfoService
    {
        #region
        //DbContext db = EFContextFactory.GetDbContext();
        //public int Add(EnterInfo entity)
        //{

        //    db.Set<EnterInfo>().Add(entity);//给entity打上添加标记，等到调用saveChanges()时，才去操作数据库
        //    return db.SaveChanges();//调用SaveChanges()时，才统一的去操作数据库，内置事物

        //}
        //public int Update(EnterInfo entity)
        //{

        //    //先查：=>读作go to （lambda表达式），匿名函数
        //    EnterInfo eEntity = db.Set<EnterInfo>().Where(e => e.ID == entity.ID).FirstOrDefault();
        //    eEntity.MarketNumber = entity.MarketNumber;
        //    db.Entry(eEntity).State = EntityState.Modified; //打上修改标记
        //    return db.SaveChanges();

        //}
        //public int Delete(EnterInfo entity)
        //{
        //    //必须先查再删
        //    //先查：=>读作go to (lambda表达式),匿名函数
        //    EnterInfo eEntity = db.Set<EnterInfo>().Where(e => e.ID == entity.ID).FirstOrDefault();
        //    db.Set<EnterInfo>().Attach(eEntity);
        //    db.Set<EnterInfo>().Remove(eEntity);//打上删除标记
        //    return db.SaveChanges();
        //}

        ///// <summary>
        ///// 查询所有
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<EnterInfo> Select(Expression<Func<EnterInfo, bool>> whereLambda)
        //{
        //    IQueryable<EnterInfo> IQEnterInfo = db.Set<EnterInfo>().Where(whereLambda);
        //    return IQEnterInfo;
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
        //public IQueryable<EnterInfo> PageLoadEntity<s>(int pageIndex, int pageSize, Expression<Func<EnterInfo, bool>> whereLambda, Expression<Func<EnterInfo, s>> orderby, bool isChecked)
        //{
        //    var temp = db.Set<EnterInfo>().Where(whereLambda);
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
