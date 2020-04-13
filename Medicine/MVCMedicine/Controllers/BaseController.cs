using EFModel;
using MedicineService.Services;
using MedicineService.UnitOfWord;
using InterfaceService.IServices;
using InterfaceService.IUnitOfWord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMedicine.Controllers
{
    public class BaseController : Controller
    {
        #region 声明各个表的相关服务对象

        /// <summary>
        /// 进货信息表与库存表工作单元相关服务对象
        /// </summary>
        public IEnterInfo_Inventory_UOW EnterInfo_Inventory_UOW { get; set; }

        /// <summary>
        /// 价格表单元相关服务对象
        /// </summary>
        public IPriceServices PriceServices { get; set; }

        /// <summary>
        /// 销售与仓库表单元相关服务对象
        /// </summary>
        public IMarketInfo_Inventory_UOW MarketInfo_Inventory_UOW { get; set; }

        /// <summary>
        /// 角色表与角色权限表工作单元相关服务对象
        /// </summary>
        public IRoleInfo_R_RoleInfo_PowerInfo_UOW RoleInfo_R_RoleInfo_PowerInfo_UOW { get; set; }

        /// <summary>
        /// 权限信息表相关服务对象
        /// </summary>
        public IPowerInfoService powerInfoService { get; set; }

        /// <summary>
        /// 用户角色信息工作单元相关服务对象
        /// </summary>
        public IUserInfo_R_UserInfo_RoleInfo_UOW userInfo_R_UserInfo_RoleInfo_UOW { get; set; }

        /// <summary>
        /// 角色权限表相关服务对象
        /// </summary>
        public IR_RoleInfo_PowerInfoService r_RoleInfo_PowerInfoService { get; set; }

        /// <summary>
        /// 药品信息表相关服务对象
        /// </summary>
        public IMedicineInfoService medicineInfoService { get; set; }

        /// <summary>
        /// 用户信息表相关服务对象
        /// </summary>
        public IUserInfoService userInfoService { get; set; }

        /// <summary>
        /// 药品分类表相关服务对象
        /// </summary>
        public IClassifyService classifyService { get; set; }

        /// <summary>
        /// 部门信息表相关服务对象
        /// </summary>
        public IDepartmentService departmentService { get; set; }

        /// <summary>
        /// 药品形状类别表相关服务对象
        /// </summary>
        public IDosageTypeService dosageTypeService { get; set; }

        /// <summary>
        /// 供应商信息表相关服务对象
        /// </summary>
        public IEnterCompanyService enterCompanyService { get; set; }

        /// <summary>
        /// 进货信息表相关服务对象
        /// </summary>
        public IEnterInfoService enterInfoService { get; set; }

        /// <summary>
        /// 库存信息表相关服务对象
        /// </summary>
        public IInventoryService inventoryService { get; set; }

        /// <summary>
        /// 销售信息表相关服务对象
        /// </summary>
        public IMarketInfoService marketInfoService { get; set; }

        /// <summary>
        /// 角色信息表相关服务对象
        /// </summary>
        public IRoleInfoService roleInfoService { get; set; }

        /// <summary>
        /// 用户角色表相关服务对象
        /// </summary>
        public IR_UserInfo_RoleInfoService r_UserInfo_RoleInfoService { get; set; }

        /// <summary>
        /// 包装方式表相关服务对象
        /// </summary>
        public IPackTableService packTableService { get; set; }

        /// <summary>
        /// 贮藏方式表相关服务对象
        /// </summary>
        public IRepositService repositService { get; set; }
        #endregion

        #region 声明父类的属性
        /// <summary>
        /// 获取Excel的文件名字
        /// </summary>
        public static HttpPostedFileBase file { get; set; }

        /// <summary>
        /// 父类属性：用户编号(仅子类可访问)
        /// </summary>
        protected string UserID { get; set; }

        /// <summary>
        /// 父类属性：用户名称(仅子类可访问)
        /// </summary>
        protected string UserName { get; set; }

        /// <summary>
        /// 父类属性：部门编号(仅子类可访问)
        /// </summary>
        protected string DepartmentID { get; set; }

        /// <summary>
        /// 父类属性：角色编号（仅子类可访问）
        /// </summary>
        protected string AllRoleID { get; set; }
        #endregion
        
        /// <summary>
        /// 重写行为前过滤器，执行控制器中的方法之前先执行该方法，--注意：该验证为数据库权限信息验证，和特性验证二选其一，别搞乱了
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //【1】Session统一校验，保证用户必须登录
            if (Session["UserID"] != null)
            {
                //把session中的数据装进baseController控制器的属性中，可以使用ctrl+f查看
                UserID = Session["UserID"].ToString();
                UserName = Session["UserName"].ToString();
                DepartmentID = Session["DepartmentID"].ToString();
                AllRoleID = Session["RoleID"].ToString();
                //【2.1】拿到要访问的控制器和方法的地
                string url = Request.Url.AbsolutePath;
                //访问的地址是/home/index或者用户编号为1或者获取到的地址为“/”可以直接访问
                if (url == "/Home/Index" || UserID == "1"||url == "/")
                {
                    return; //主页和超级管理员只需要做登陆验证
                }
                //【2.2】查询该用户拥有的所有角色的角色权限表
                //思路1;先拿到这个人的所有的权限，在连表，假如连表的ActionUrl这一列的结果包含url，则说明这个人拥有访问这个地址的权限
                //思路2;先拿到这个url对应的powerid，再根据该用户拥有的所有的角色获取角色权限中间表的数据，如果权限中间表的PowerID这一列包含这个url对应的powerid，则有权限

                //先通过获取到的url去数据库对比，是否存在该链接的信息
                PowerInfo powerInfo = powerInfoService.Query(u => u.ActionUrl == url).FirstOrDefault();
                //再查询角色权限表的所有数据
                var Iquery = r_RoleInfo_PowerInfoService.Query(u => AllRoleID.Contains(u.RoleID.ToString())).ToList();
                //判断：如果角色权限表返回的总数大于0
                if(Iquery.Count > 0)
                {
                    //【2.3】查询角色权限表，看看该是否拥有此powerid权限
                    int count = Iquery.Where(u => u.PowerID == powerInfo.PowerID).Count();
                    if (count>0)
                    {
                        return; //说明有权限
                    }else
                    {
                        //跳入错误页
                        filterContext.Result = Redirect("/PowerError.html");
                    }
                }else
                {
                    //跳入错误页
                    filterContext.Result = Redirect("/PowerError.html");
                }
            }else
            {
                //返回登录页
                filterContext.Result = RedirectToRoute("Default", new { Controller = "Account", Action = "LoginIndex" });
            }
        }
    }
}