
using DataModel.EquelityComparer;
using EFModel;
using MedicineService;
using MedicineService.Services;
using MVCMedicine.FilterAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMedicine.Controllers
{
    public class HomeController : BaseController
    {
      // GET: Home
        //[AllowAnonymous]
        public ActionResult Index()
        {
            //获取到uesrid和userName
            ViewBag.userID = UserID;
            ViewBag.userName = UserName;

            if (UserID == "1")
            {
                //只要是UserID == 1 的就可以不用进行权限验证
                ViewBag.Menu = powerInfoService.Query(u => u.IsMenu == 1).ToList();
            }
            else
            {
                //根据角色编号获取角色权限表中的数据,和权限表中的所有数据
                var iquery = r_RoleInfo_PowerInfoService.Query(u => AllRoleID.Contains(u.RoleID.ToString()));
                var iquerypower = powerInfoService.Query(u => u.ID > 0);

                //把角色表和权限表，并筛选出菜单的权限
                var listMenu = (from a in iquery
                                join b in iquerypower on a.PowerID equals b.PowerID into b_join
                                from c in b_join.DefaultIfEmpty()
                                select c).Where(u => u.IsMenu == 1).ToList();

                //数组去重
                ViewBag.Menu = listMenu.Distinct(new PowerInfoComparer());
                #region 数组去重的思路
                //数组去重的思路 【1】先创建一个新的List集合
                //List<PowerInfo> listModel = new List<PowerInfo>();
                //【2】先遍历循环查询到的数据
                //foreach (var item in listMenu)
                //{
                //判断创建的新的List集合是否有数据，如果没有数据，则进入if代码块
                //    if (listModel.Count==0)
                //    {
                //向新的List集合中添加数据
                //        listModel.Add(item);
                //    }
                //声明一个变量，作用是：当作一个标记
                //    int count = 0;
                //循环变量新的List集合
                //    foreach (var item1 in listModel)
                //    {
                //判断：新的List集合中的PowerID包含查到List集合的PowerID，如果包含，进入if代码块中修改标记
                //        if (item1.PowerID.ToString().Contains(item.PowerID.ToString()))
                //        {
                //修改标记
                //            count = 1;
                //        }
                //    }
                //判断：如果标记没有被修改，也就是没有重复，进入代码块中添加数据
                //    if (count == 0)
                //    {
                //向新的List集合中添加数据
                //        listModel.Add(item);
                //    }
                //}
                //把去除重复得到的数据复制ViewBag.Menu
                //ViewBag.Menu = listModel;
                #endregion
            }
            return View();
        }
    }
}