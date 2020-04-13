using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceService
{
    public interface IBaseServices<T> where T : class
    {
        #region 直接执行saveChanges
        int AddTo(T entity);
        int Modify(T entity);
        int Delete(T entity);
        #endregion

        #region 只是打上标记
        bool AddToFlag(T entity);
        bool ModifyFlag(T entity);
        bool DeleteFlag(T entity);

        #endregion

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Query(Expression<Func<T, bool>> lambda);

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
        IQueryable<T> PageLoadEntity<s>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderby, bool isChecked);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="listmodel"></param>
        /// <returns></returns>
        int BatchDelete(List<T> listmodel);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="listmodel"></param>
        /// <returns></returns>
        int BatchAdd(List<T> listmodel);
    }
}
