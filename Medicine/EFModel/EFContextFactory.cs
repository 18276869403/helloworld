using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EFModel
{
    /// <summary>
    /// 负责创建EF数据操作上下文实例，保证线程内唯一
    /// </summary>
    public class EFContextFactory
    {
        /// <summary>
        /// 线程槽==父类DBContext
        /// </summary>
        /// <returns></returns>
        public static DbContext GetDbContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("dbContext");//去命名为dbContext的线程数据槽中找，看看有没有DbContext
            if (dbContext == null)//如果没有，就创建
            {
                dbContext = new EFContext();//创建dbContext
                CallContext.SetData("dbContext", dbContext);//把它方法哦命名为dbContext的线程数据槽
            }
            return dbContext;
        }
    }

}
