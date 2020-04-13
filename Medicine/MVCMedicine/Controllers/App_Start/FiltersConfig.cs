using MVCMedicine.FilterAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMedicine.App_Start
{
    public class FiltersConfig
    {
        /// <summary>
        /// 静态全局异常处理器
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyErrorFilterAttribute()); //注册自定义异常处理过滤器
        }
    }
}