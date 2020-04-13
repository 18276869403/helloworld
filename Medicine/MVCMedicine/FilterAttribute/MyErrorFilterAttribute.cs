using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMedicine.FilterAttribute
{
    public class MyErrorFilterAttribute:HandleErrorAttribute
    {
        public static Queue<Exception> ExceptionQueue = new Queue<Exception>(); //创建一个队列

        /// <summary>
        /// 异常处理过滤器
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            if (!filterContext.ExceptionHandled)
            {
                //注意：在跳转到错误页之前，应该把报错信息记录到日志中，供开发人员检查bug
                ExceptionQueue.Enqueue(filterContext.Exception);//入队
                filterContext.Result = new RedirectResult("/Error.html");
                //异常处理后，要将ExceptionHandled设置为true，否则仍然会继续抛出错误
                filterContext.ExceptionHandled = true;
            }
        }
    }
}