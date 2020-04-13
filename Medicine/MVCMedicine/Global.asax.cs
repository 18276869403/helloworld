using log4net;
using MVCMedicine.App_Start;
using MVCMedicine.FilterAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCMedicine
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();//Log4Net
            
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes); //注册路由器规则
            FiltersConfig.RegisterFilters(GlobalFilters.Filters);  //在global(全球性,全局)文件中注册该方法
            AutofacConfig.MyAutofacConfig(); //注册Autofac

            #region Log4Net
            //开辟一个线程池
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (true)
                {
                    if (MyErrorFilterAttribute.ExceptionQueue.Count > 0)//判断队列里是否有数据
                    {
                        Exception ex = MyErrorFilterAttribute.ExceptionQueue.Dequeue(); //出队
                        if(ex != null)
                        {
                            ILog logger = LogManager.GetLogger("testError");
                            logger.Error(ex.ToString()); //将异常信息写入Log4Net中
                        }else
                        {
                            Thread.Sleep(3000); //线程休眠3000毫秒
                        }
                    }else
                    {
                        Thread.Sleep(3000); //线程休眠300毫秒
                    }
                }
            });
            #endregion
        }
    }
}
