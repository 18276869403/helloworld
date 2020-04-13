
using EFModel;
using MedicineService;
using System;
using System.Collections.Generic;
using InterfaceService.IServices;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedicineService.Services
{
    public class MarketInfoService : BaseServices<MarketInfo>,IMarketInfoService
    {
        #region
        //DbContext db = EFContextFactory.GetDbContext();
        //public int Add(MarketInfo entity)
        //{
        //    db.Set<MarketInfo>().Add(entity);//给entity打上添加标记，等到调用saveChanges()时，才去操作数据库
        //    return db.SaveChanges();//调用SaveChanges()时，才统一的去操作数据库，内置事物
        //}
        //public int Update(MarketInfo entity)
        //{
        //    //必须先查再改
        //    //先查：=>读作go to （lambda表达式），匿名函数
        //    MarketInfo mEntity = db.Set<MarketInfo>().Where(m => m.ID == entity.ID).FirstOrDefault();
        //    mEntity.MarketNumber = entity.MarketNumber;
        //    db.Entry(mEntity).State = EntityState.Modified; //打上修改标记
        //    return db.SaveChanges();
        //}
        //public int Delete(MarketInfo entity)
        //{
        //    //必须先查再删
        //    //先查：=>读作go to (lambda表达式),匿名函数
        //    MarketInfo mEntity = db.Set<MarketInfo>().Where(m => m.ID == entity.ID).FirstOrDefault();
        //    db.Set<MarketInfo>().Attach(mEntity);
        //    db.Set<MarketInfo>().Remove(mEntity);//打上删除标记
        //    return db.SaveChanges();
        //}

        ///// <summary>
        ///// 查询所有
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<MarketInfo> Select(Expression<Func<MarketInfo, bool>> whereLambda)
        //{
        //    IQueryable<MarketInfo> IQMarketInfo = db.Set<MarketInfo>().Where(whereLambda);
        //    return IQMarketInfo;
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
        //public IQueryable<MarketInfo> PageLoadEntity<s>(int pageIndex, int pageSize, Expression<Func<MarketInfo, bool>> whereLambda, Expression<Func<MarketInfo, s>> orderby, bool isChecked)
        //{
        //    var temp = db.Set<MarketInfo>().Where(whereLambda);
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
