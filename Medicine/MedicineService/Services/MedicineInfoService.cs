
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
    public class MedicineInfoService:BaseServices<MedicineInfo>,IMedicineInfoService
    {
        #region
        //DbContext db = EFContextFactory.GetDbContext();
        //public int Add(MedicineInfo entity)
        //{
        //    db.Set<MedicineInfo>().Add(entity);//给entity打上添加标记，等到调用saveChanges()时，才去操作数据库
        //    return db.SaveChanges();//调用SaveChanges()时，才统一的去操作数据库，内置事物
        //}
        //public int Update(MedicineInfo entity)
        //{
        //    //必须先查再改
        //    //先查：=>读作go to （lambda表达式），匿名函数
        //    MedicineInfo mEntity = db.Set<MedicineInfo>().Where(m => m.ID == entity.ID).FirstOrDefault();
        //    mEntity.ChineseName = entity.ChineseName;
        //    db.Entry(mEntity).State = EntityState.Modified; //打上修改标记
        //    return db.SaveChanges();
        //}
        //public int Delete(MedicineInfo entity)
        //{
        //    //必须先查再删
        //    //先查：=>读作go to (lambda表达式),匿名函数
        //    MedicineInfo mEntity = db.Set<MedicineInfo>().Where(m => m.ID == entity.ID).FirstOrDefault();
        //    db.Set<MedicineInfo>().Attach(mEntity);
        //    db.Set<MedicineInfo>().Remove(mEntity);//打上删除标记
        //    return db.SaveChanges();
        //}

        ///// <summary>
        ///// 查询所有
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<MedicineInfo> Select()
        //{
        //    IQueryable<MedicineInfo> IQMedicineInfo = db.Set<MedicineInfo>().Where(m => m.ID > 0);
        //    return IQMedicineInfo;
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
        //public IQueryable<MedicineInfo> PageLoadEntity<s>(int pageIndex, int pageSize, Expression<Func<MedicineInfo, bool>> whereLambda, Expression<Func<MedicineInfo, s>> orderby, bool isChecked)
        //{
        //    var temp = db.Set<MedicineInfo>().Where(whereLambda);
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