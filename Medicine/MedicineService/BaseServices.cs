
using EFModel;
using System;
using System.Collections.Generic;
using InterfaceService;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedicineService
{
    public class BaseServices<T> :IBaseServices<T>where T:class
    {
        private DbContext db = EFContextFactory.GetDbContext();

        #region 直接执行saveChanges
        public int AddTo(T entity)
        {
            db.Set<T>().Add(entity);//给entity打上添加标记，等到调用saveChanges()时，才去操作数据库
            return db.SaveChanges();//调用SaveChanges()时，才统一的去操作数据库，内置事物
        }
        public int Modify(T entity)
        {
            db.Entry(entity).State = EntityState.Modified; //打上修改标记
            return db.SaveChanges();
        }
        public int Delete(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Set<T>().Remove(entity);//打上删除标记
            return db.SaveChanges();
        }
        #endregion

        #region 只是打上标记
        public bool AddToFlag(T entity)
        {
            db.Set<T>().Add(entity);//给entity打上添加标记，等到调用saveChanges()时，才去操作数据库
            return true;//调用SaveChanges()时，才统一的去操作数据库，内置事物
        }
        public bool ModifyFlag(T entity)
        {
            db.Entry(entity).State = EntityState.Modified; //打上修改标记
            return true;
        }
        public bool DeleteFlag(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Set<T>().Remove(entity);//打上删除标记
            return true;
        }

        #endregion

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Query(Expression<Func<T, bool>> lambda)
        {
            IQueryable<T> IQT = db.Set<T>().Where(lambda);
            return IQT;
        }

        /// <summary>
        /// 分页排序
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex">页码数</param>
        /// <param name="pageSize">每页显示多少条数据</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderby">排序条件</param>
        /// <param name="isChecked">是否升序排序</param>
        /// <returns></returns>
        public IQueryable<T> PageLoadEntity<s>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderby, bool isChecked)
        {
            var temp = db.Set<T>().Where(whereLambda);
            if (isChecked)
            {
                temp = temp.OrderBy(orderby).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending(orderby).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            return temp;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="listmodel"></param>
        /// <returns></returns>
        public int BatchDelete(List<T> listmodel)
        {
            foreach (var model in listmodel)
            {
                db.Set<T>().Attach(model);
                db.Set<T>().Remove(model);//打上删除标记
            }
            return db.SaveChanges();
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="listmodel"></param>
        /// <returns></returns>
        public int BatchAdd(List<T> listmodel)
        {
            foreach (var model in listmodel)
            {
                db.Set<T>().Add(model);//给entity打上添加标记，等到调用saveChanges()时，才去操作数据库
            }
            return db.SaveChanges();//调用SaveChanges()时，才统一的去操作数据库，内置事物
        }
    }
}
