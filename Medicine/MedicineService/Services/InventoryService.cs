
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
    public class InventoryService : BaseServices<Inventory>,IInventoryService
    {
        #region
        //DbContext db = EFContextFactory.GetDbContext();
        //public int Add(Inventory entity)
        //{
        //    db.Set<Inventory>().Add(entity);//给entity打上添加标记，等到调用saveChanges()时，才去操作数据库
        //    return db.SaveChanges();//调用SaveChanges()时，才统一的去操作数据库，内置事物
        //}
        //public int Update(Inventory entity)
        //{
        //    //必须先查再改
        //    //先查：=>读作go to （lambda表达式），匿名函数
        //    Inventory iEntity = db.Set<Inventory>().Where(i => i.ID == entity.ID).FirstOrDefault();
        //    iEntity.Number = entity.Number;
        //    db.Entry(iEntity).State = EntityState.Modified; //打上修改标记
        //    return db.SaveChanges();
        //}
        //public int Delete(Inventory entity)
        //{
        //    //必须先查再删
        //    //先查：=>读作go to (lambda表达式),匿名函数
        //    Inventory iEntity = db.Set<Inventory>().Where(i => i.ID == entity.ID).FirstOrDefault();
        //    db.Set<Inventory>().Attach(iEntity);
        //    db.Set<Inventory>().Remove(iEntity);//打上删除标记
        //    return db.SaveChanges();
        //}

        ///// <summary>
        ///// 查询所有
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<Inventory> Select(Expression<Func<Inventory, bool>> whereLambda)
        //{
        //    IQueryable<Inventory> IQInventory = db.Set<Inventory>().Where(whereLambda);
        //    return IQInventory;
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
        //public IQueryable<Inventory> PageLoadEntity<s>(int pageIndex, int pageSize, Expression<Func<Inventory, bool>> whereLambda, Expression<Func<Inventory, s>> orderby, bool isChecked)
        //{
        //    var temp = db.Set<Inventory>().Where(whereLambda);
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
