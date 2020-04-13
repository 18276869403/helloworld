using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCMedicine.FilterAttribute
{
    /// <summary>
    /// 权限验证过滤器
    /// </summary>
    public class MyPowerFilterAttribute : ActionFilterAttribute //继承ActionFilterAttribute过滤器
    {
        public string RoleID { get; set; }

        /// <summary>
        /// 重写（override） 行为前过滤器 Action（行为） filter（过滤器）Execut (执行) ing（正在进行时）
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //调用base.OnActionExecuting(filtertext)这个后，才会执行后续的ActionFilter，如果你确定只有一个，或是不想执行后续的话，那么可以不用调用该语句。
            //同时，filterContext.Result = xxx;会导致转向其它视图，后续的ActionFilter也是执行不了的。
            base.OnActionExecuting(filterContext);

            //判断Session中是否存在用户编号
            if (filterContext.HttpContext.Session["UserID"] != null)
            {
                //判断权限编号是否为空
                if (RoleID != null)
                {
                    //判断是否跳过授权过滤器[AllowAnonymous]
                    if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                        || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                    {
                        return;
                    }
                    else
                    {
                        string[] p = RoleID.Split(',');
                        foreach (var item in p)
                        {
                            //判断权限是否包含
                            if (filterContext.HttpContext.Session["RoleID"].ToString().Contains(item))
                            {
                                break;
                            }else
                            {
                                //不包含权限的话跳转到该页面
                                filterContext.Result = new RedirectResult("/PowerError.html");
                            }
                        }
                        return;
                    }
                }
                else
                {
                    //返回
                    return;
                }
            }
            else
            {
                //返回登录页面或者警告权限不足
                filterContext.Result = new RedirectResult("/Account/LoginIndex");
            }
        }

        ///// <summary>
        ///// 重写（override） 行为后过滤器  Action（行为）filter（过滤器） Execut（执行） ed（过去式）
        ///// </summary>
        ///// <param name="filterContext"></param>
        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    base.OnActionExecuted(filterContext);
        //    //HttpContext.Current.Response.Write("<p>action执行之后先执行此方法;");
        //}

        ///// <summary>
        ///// 重写（override） 结果后过滤器 Result（结果） filter（过滤器） Execut（执行） ing（正在进行时）
        ///// </summary>
        ///// <param name="filterContext"></param>
        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    base.OnResultExecuting(filterContext);
        //    //HttpContext.Current.Response.Write("<p>action返回结果之前先执行此方法;");
        //}

        ///// <summary>
        ///// 重写（override） 结果后过滤器 Result（结果） filter（过滤器） Execut（执行） ed（过去式）
        ///// </summary>
        ///// <param name="filterContext"></param>
        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    base.OnResultExecuted(filterContext);
        //    //HttpContext.Current.Response.Write("<p>action返回结果之后执行此方法;");
        //}
    }
}