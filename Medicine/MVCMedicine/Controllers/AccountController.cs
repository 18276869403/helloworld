using Common;
using EFModel;
using InterfaceService.IServices;
using MedicineService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMedicine.Controllers
{
    public class AccountController : Controller
    {
        public IUserInfoService UserinfoService { get; set; }
        public IR_UserInfo_RoleInfoService R_UserInfo_RoleInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        // GET: Account
        public ActionResult LoginIndex()
        {
            return View();
        }

        #region form表单提交数据
        /*使用MVC特性 面向切面编程技术，限制访问者的请求，只能使用post方式请求，防止CSRF攻击验证，CSRF表示“跨站请求伪造”，是一种攻击手段*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login()
        {
            //获取前台传递的值，密码使用了MD5技术进行加密
            string UserName = Request["UserName"];
            string UserPwd = MD5Helper.EncryptString(Request["UserPwd"]);

            //去数据库验证查询前台输入的UserName和UserPwd,并且筛选禁用（DelFlag）的用户
            List<UserInfo> Userlist = UserinfoService.Query(u => u.UserName == UserName && u.UserPwd == UserPwd && u.DelFlag == 0).ToList();
            //如果得到的数据超过0条，说明登录成功
            if (Userlist.Count > 0)
            {
                /*把需要用到的数据方如session中，因为其它地方可能会用到，先放入session中*/
                Session["UserName"] = Userlist[0].UserName; //用户名
                Session["UserID"] = Userlist[0].UserID; //用户编号
                Session["DepartmentID"] = Userlist[0].DepartmentID; //部门编号

                //把数据库查到的用户编号赋值给int变量
                int UserID = Userlist[0].UserID;
                /* 查询用户角色表，通过UserID查询，查询全部角色信息表 */
                var R_U_RoleIquery = R_UserInfo_RoleInfoService.Query(u => u.UserID == UserID); //用户角色表
                var RoleIquery = RoleInfoService.Query(u => u.ID > 0 && u.DelFlag == 0); //角色信息表

                /* 使用用户角色表 连接 角色信息表，筛选角色信息表启用的角色 */
                var r_roleIquery = (from a in R_U_RoleIquery
                                    join b in RoleIquery on a.RoleID equals b.RoleID into B_Join
                                    from c in B_Join.DefaultIfEmpty()
                                    select new
                                    {
                                        UserID = a.UserID,
                                        RoleID = a.RoleID,
                                        DelFlag = c.DelFlag
                                    }).Where(u => u.DelFlag == 0);

                //分组,通过p.UserID进行分组
                var GrpIquery = (from p in r_roleIquery
                                 group p by new
                                 {
                                     p.UserID
                                 }).AsEnumerable().Select(u =>
                                 new
                                 {
                                     UserID = u.Key.UserID,
                                     RoleID = String.Join(",", u.Select(x => x.RoleID).ToArray())
                                 }).ToList();

                //如果分组后的数据总数大于0
                if (GrpIquery.Count > 0)
                {
                    Session["RoleID"] = GrpIquery[0].RoleID; //之后做权限控制的时候使用所以先存起来
                }
                else
                {
                    Session["RoleID"] = 0; //如果没有，默认值为0
                }
                //页面跳转，Home控制器的Index方法 跳转到首页
                return RedirectToAction("Index", "Home");
            }
            //页面跳转，Account控制器的LoginIndex方法 跳转登录页
            return RedirectToAction("LoginIndex", "Account");
        }
        #endregion

        #region Ajax登录验证（未启用）
        //[HttpPost] //限制访问者的请求，只能使用post方式请求
        //[ValidateAntiForgeryToken] //CSRF攻击验证
        //public string AjaxLogin()
        //{
        //    //获取前台传递的值
        //    string userName = Request["userid"];
        //    string userPwd = MD5Helper.EncryptString(Request["pwd"]); //密码使用MD5加密

        //    //去数据库验证查询前台输入的值,并且筛选禁用的用户
        //    List<UserInfo> userlist = userinfoService.Select(u => u.UserName == userName && u.UserPwd == userPwd && u.DelFlag == 0).ToList();
        //    //如果得到的数据超过0条，说明登录成功
        //    if (userlist.Count > 0)
        //    {
        //        //把需要用到的数据方如session中，因为其它地方可能会用到，先放入session中
        //        Session["UserName"] = userlist[0].UserName;
        //        Session["UserID"] = userlist[0].UserID;
        //        Session["DepartmentID"] = userlist[0].DepartmentID;
        //        //把数据库查到的用户编号赋值给int变量
        //        int UserID = userlist[0].UserID;
        //        //查询用户角色表，通过UserID查询
        //        var r_u_roleIquery = r_UserInfo_RoleInfoService.Select(u => u.UserID == UserID);
        //        //查询全部角色信息表
        //        var roleIquery = roleInfoService.Select(u => u.ID > 0 && u.DelFlag == 0);
        //        //使用用户角色表连接角色信息表
        //        var r_roleIquery = (from a in r_u_roleIquery
        //                            join b in roleIquery on a.RoleID equals b.RoleID into b_join
        //                            from c in b_join.DefaultIfEmpty()
        //                            select new
        //                            {
        //                                UserID = a.UserID,
        //                                RoleID = a.RoleID,
        //                                DelFlag = c.DelFlag
        //                            }).Where(u => u.DelFlag == 0); //筛选角色信息表启用的角色
        //        //分组,通过p.UserID进行分组
        //        var grpIquery = (from p in r_roleIquery
        //                         group p by new
        //                         {
        //                             p.UserID
        //                         }).AsEnumerable().Select(u =>
        //                         new
        //                         {
        //                             UserID = u.Key.UserID,
        //                             RoleID = String.Join(",", u.Select(x => x.RoleID).ToArray())
        //                         }).ToList();
        //        //如果分组后的数据总数大于0
        //        if (grpIquery.Count > 0)
        //        {
        //            Session["RoleID"] = grpIquery[0].RoleID; //之后做权限控制的时候使用所以先存起来
        //        }
        //        else
        //        {
        //            Session["RoleID"] = 0; //如果没有，默认值为0
        //        }
        //        return "yes";
        //    }else
        //    {
        //        return "no";
        //    }
        //} 
        #endregion
    }
}