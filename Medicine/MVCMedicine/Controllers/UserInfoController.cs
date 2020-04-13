using Common;
using EFModel;
using MedicineService.Services;
using MedicineService.UnitOfWord;
using MVCMedicine.FilterAttribute;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicineService;
using DataModel.DataModels;
using Comman.FieldHelper;

namespace MVCMedicine.Controllers
{
    //[MyPowerFilter(RoleID = "1")]
    /// <summary>
    /// 继承BaseController控制器，BaseController拥有对权限的控制
    /// </summary>
    public class UserInfoController : BaseController
    {
        // GET: UserInfo
        /// <summary>
        /// 用户信息首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 分配权限页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AllotRole()
        {
            return View();
        }

        public ActionResult ModifyPassword()
        {
            UserFieldHelper.UserID = Convert.ToInt32(base.UserID);
            ViewBag.UserName = base.UserName;
            return View();
        }

        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <returns></returns>
        public string GetRoleJson()
        {
            //获取到网页传递的值
            int pageIndex = Convert.ToInt32(Request["page"]); //分页
            int pageSize = Convert.ToInt32(Request["limit"]); //每页多少条

            //把查询到的数据装进entity
            IQueryable<RoleInfo> entity = roleInfoService.PageLoadEntity(pageIndex, pageSize, u => u.ID > 0, u => u.ID, true);
            int count = roleInfoService.Query(u => u.ID > 0).Count();

            //时间转换格式
            IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverter();
            datetimeConverter.DateTimeFormat = "yyyy'-'MM'-'dd hh':'mm':'ss";

            //把得到的数据转换成Json格式的数据
            string strJson = JsonConvert.SerializeObject(entity, Formatting.Indented, datetimeConverter);
            //把转换好的Json格式的数据再转换layui绑定数据的格式
            string layuiStr = "{\"code\":0,\"msg\":\"\",\"count\":" + count + ",\"data\":" + strJson + "}";
            return layuiStr;
        }

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <returns></returns>
        public string AddRole()
        {
            //获取到用户需要修改的角色编号  把Request["RoleInfo"].Split(',');根据逗号拆分并装进arr数组中
            string[] arr = Request["RoleInfo"].Split(',');

            int UserID = Convert.ToInt32(Request["UserID"]); //获取到网页传递的

            //【1】根据用户编号去查询用户角色表，把查询到的数据装进list集合中
            List<R_UserInfo_RoleInfo> list = r_UserInfo_RoleInfoService.Query(u => u.UserID == UserID).ToList();
            List<R_UserInfo_RoleInfo> listmodel = new List<R_UserInfo_RoleInfo>();     //实例化一个新的list集合

            //【2】判断是否查询到数据
            if (list.Count > 0)  
            {
                //【3】先把查询到的数据批量删除   BatchDelete这是封装好的批量删除的方法 可以用F12查看
                if (r_UserInfo_RoleInfoService.BatchDelete(list) > 0)
                {
                    //【4】for循环获取arr里面的值
                    for (int i = 0; i < arr.Length - 1; i++)
                    {
                        //【5】实例化一个新的models并把数据装进models中  arr[i]根据i来获取arr里面的值
                        R_UserInfo_RoleInfo models = new R_UserInfo_RoleInfo();
                        models.UserID = UserID;
                        models.RoleID = Convert.ToInt32(arr[i]);

                        //【6】把models添加到listmodel集合中
                        listmodel.Add(models);
                    }
                    //【7】调用封装好的批量添加的方法 (可以用F12查看) 并且把已经有数据的listmodel集合当成参数传给方法
                    if (r_UserInfo_RoleInfoService.BatchAdd(listmodel) > 0)
                    {
                        return "yes";
                    }
                    else
                    {
                        return "no";
                    }
                }
                else
                {
                    return "no";
                }
            }
            else
            {
                //【1】当根据用户编号去数据库未查询到数据的时候走下面的代码
                //【1.1】for循环获取arr里面的值
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    //【1.2】实例化一个新的models并把数据装进models中  arr[i]根据i来获取arr里面的值
                    R_UserInfo_RoleInfo models = new R_UserInfo_RoleInfo();
                    models.UserID = UserID;
                    models.RoleID = Convert.ToInt32(arr[i]);

                    //【1.3】把models添加到listmodel集合中
                    listmodel.Add(models);
                }
                //【1.4】调用封装好的批量添加的方法 (可以用F12查看) 并且把已经有数据的listmodel集合当成参数传给方法
                if (r_UserInfo_RoleInfoService.BatchAdd(listmodel) > 0)
                {
                    return "yes";
                }
                else
                {
                    return "no";
                }
            }
        }

        /// <summary>
        /// 添加用户的视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUserInfo()
        {
            //查询部门表的所有信息，并装进ViewBag.model容器中   这里的ViewBag.model用来在网页上面绑定下拉框
            //可以去视图了解具体的方法
            ViewBag.model = departmentService.Query(u => u.DepartmentID > 0).ToList();
            return View();
        }

        /// <summary>
        /// 修改的视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyUserInfo()
        {
            /* 当用户点击编辑用户信息按钮的时候获取用户编号 
             * 并弹出修改用户信息窗体同时把获取到的用户编号通过地址栏传递到该窗口*/
            
            //获取点击按钮时传递过来的用户编号
            int UserID = Convert.ToInt32(Request["UserID"]);

            //【1】首页查询部门表的所有信息，用来绑定下拉列表  这里的ViewBag.depmodel用来在网页上面绑定下拉框
            ViewBag.depmodel = departmentService.Query(u => u.DepartmentID > 0).ToList();
            //【2】根据用户编号查询到该用户编号的所有信息并装进ViewBag.model中
            //这里ViewBag.model的用处是在网页弹出来的时候就把该用户编号所有的信息绑定到网页的控件里面
            ViewBag.model = userInfoService.Query(u => u.UserID == UserID).FirstOrDefault();
            return View();
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Modify(UserInfo model)
        {
            /*这里修改用户信息使用的是html的Form表单提交数据的方式，所以提交的数据能够直接传递到方法里，
             * 所以我们的方法中声明了UserInfo model 就是用来接收form表单提交的数据*/

            //修改数据的步骤  先查再改
            //【1】先根据接收到的用户编号查询数据库
            UserInfo entity = userInfoService.Query(u => u.UserID == model.UserID).FirstOrDefault();
            //判断：是否查询到数据 如果entity为空 表示没有查询到数据
            if(entity!= null)
            {
                //当查询到数据时进入
                //【2】把从表单获取到的数据（model）填充进entity中，注：只需要填充 需要修改的数据就行
                entity.UserName = model.UserName;
                entity.UserPhone = model.UserPhone;
                entity.Sex = model.Sex;
                entity.DelFlag = model.DelFlag;

                //【3】调用修改的方法 可以用F12查看
                if (userInfoService.Modify(entity) > 0)
                {
                    //修改成功后使用javascript技术实现页面跳转，并把用户编号传递到跳转的页面
                    return "<script>alert('修改成功！！！');window.location.href='../UserInfo/ModifyUserInfo?UserID="+ entity.UserID +"'</script>";
                }else
                {
                    //修改失败后使用javascript技术实现页面跳转，并把用户编号传递到跳转的页面
                    return "<script>alert('修改失败！！！');window.location.href='../UserInfo/ModifyUserInfo?UserID=" + entity.UserID + "'</script>";
                }
            }else
            {
                //为查询到该用户编号的信息 使用javascript技术实现页面跳转，并把用户编号传递到跳转的页面
                return "<script>alert('未查询到信息！！！');window.location.href='../UserInfo/ModifyUserInfo?UserID=" + entity.UserID + "'</script>";
            }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        public string Add(UserInfo model)
        {
            /*这里修改用户信息使用的是html的Form表单提交数据的方式，所以提交的数据能够直接传递到方法里，
             * 所以我们的方法中声明了UserInfo model 就是用来接收form表单提交的数据*/

            //【1】首先，先判断model里面是否接收到的form表单里面的数据
            if (model != null)
            {
                //【2】声明一个初始化器 就是在实例化一个容器的时候同时给属性赋值
                UserInfo entity = new UserInfo
                {
                    //把从表单获取到数据的model里面的数据再赋值给entity
                    UserID = model.UserID,
                    UserName = model.UserName,
                    UserPwd = MD5Helper.EncryptString(model.UserPwd),
                    DepartmentID = model.DepartmentID,
                    Sex = model.Sex,
                    UserPhone = model.UserPhone,
                    DelFlag = model.DelFlag == 1 ? 1 : 0, //三元表达式
                    AddTime = DateTime.Now
                };
                //调用添加数据的方法 可以使用F12查看
                if (userInfoService.AddTo(entity) > 0)
                {
                    //添加成功后 实现页面跳转
                    return "<script>alert('添加成功！！！');window.location.href='../UserInfo/AddUserInfo'</script>";
                }
                else
                {
                    //添加失败后 实现页面跳转
                    return "<script>alert('添加失败！！！');window.location.href='../UserInfo/AddUserInfo'</script>";
                }
            }
            else
            {
                //当获取的数据为空的时候 实现页面跳转
                return "<script>alert('数据不能为空！！！');window.location.href='../UserInfo/AddUserInfo'</script>";
            }
        }

        /// <summary>
        /// 判断编号是否存在
        /// </summary>
        /// <returns></returns>
        public string isRepeat()
        {
            //获取控件里面的值
            int value = Convert.ToInt32(Request["value"]);

            //对value做数据验证
            if (value.ToString() != ""||value.ToString() != null||value != 0)
            {
                //调用查询的方法根据value去查
                if (userInfoService.Query(u=>u.UserID == value).Count()>0)
                {
                    //返回
                    return "yes";
                }
                else
                {
                    //返回
                    return "no";
                }
            }
            else
            {
                //返回
                return "no";
            }
        }

        //[AllowAnonymous]
        /// <summary>
        /// 查询+分页
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            //获取到前台传递的数据 通过Request关键字获取
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["limit"]);
            IQueryable<UserInfo> data = null; //声明一个装数据的容器data，数据类型必须是IQueryable<UserInfo>
            int count = 0; //初始化标记 查询到的数据有多少条
            try
            {
                int UserID = Convert.ToInt32(Request["UserID"]);//获取网页传递过来的用户编号

                if (UserID.ToString() == null || UserID.ToString() == "" || UserID == 0)
                {
                    //把查询到的数据装入data容器中
                    data = userInfoService.PageLoadEntity(pageIndex, pageSize, u => u.ID > 0, u => u.UserID, true);
                    count = userInfoService.Query(u => u.ID > 0).Count(); //把查询到多少条数据赋值给初始化标记 count
                }
                else
                {
                    //把查询到的数据装入data容器中
                    data = userInfoService.PageLoadEntity(pageIndex, pageSize, u => u.UserID == UserID, u => u.UserID, true);
                    count = userInfoService.Query(u => u.UserID == UserID).Count(); //把查询到多少条数据赋值给初始化标记 count
                }
            }
            catch 
            {
                //把查询到的数据装入data容器中
                data = userInfoService.PageLoadEntity(pageIndex, pageSize, u => u.ID > 0, u => u.UserID, true);
                count = userInfoService.Query(u => u.ID > 0).Count(); //把查询到多少条数据赋值给初始化标记 count
            }
            
            //查询所需要的用到的表的所有数据
            IQueryable<Department> departmentIquery = departmentService.Query(u => u.DepartmentID > 0); //Department部门表
            IQueryable<RoleInfo> roleIquery = roleInfoService.Query(u => u.ID > 0); //RoleInfo角色表
            IQueryable<R_UserInfo_RoleInfo> r_u_rIquery = r_UserInfo_RoleInfoService.Query(u => u.ID > 0); //R_UserInfo_RoleInfo用户角色表
            //linq连表查询
            IQueryable<UserInfoModels> linq = (from a in data //（from从...、来自） a (别名) in(在) data(表) 
                                              join b in departmentIquery on a.DepartmentID equals b.DepartmentID into b_join //on(连表关键字) a.DepartmentID(用户表部门编号) equals(关键字) b.DepartmentID(部门表部门编号) into(把结果装进...)
                                              from c in b_join.DefaultIfEmpty()
                                              join d in r_u_rIquery on a.UserID equals d.UserID into c_join
                                              from e in c_join.DefaultIfEmpty()
                                              join f in roleIquery on e.RoleID equals f.RoleID into d_join
                                              from g in d_join.DefaultIfEmpty()
                                              select new UserInfoModels
                                              {
                                                  UserID = a.UserID,
                                                  UserName = a.UserName,
                                                  UserPhone = a.UserPhone,
                                                  AddTime = a.AddTime,
                                                  Sex = a.Sex,
                                                  DelFlag = a.DelFlag,
                                                  DepartmentID = c.DepartmentID,
                                                  DepartmentName = c.DepartmentName,
                                                  RoleName = g.RoleName
                                              });
            //把查询到的数据进行分组
            var grouplinq = (from p in linq
                             group p by new
                             {
                                 p.UserID,
                                 p.UserName,
                                 p.UserPhone,
                                 p.AddTime,
                                 p.Sex,
                                 p.DelFlag,
                                 p.DepartmentID,
                                 p.DepartmentName,
                             }).AsEnumerable().Select(grp => 
                             new UserInfoModels
                             {
                                 UserID = grp.Key.UserID,
                                 UserName = grp.Key.UserName,
                                 UserPhone = grp.Key.UserPhone,
                                 AddTime = grp.Key.AddTime,
                                 Sex = grp.Key.Sex,
                                 DelFlag = grp.Key.DelFlag,
                                 DepartmentName = grp.Key.DepartmentName,
                                 RoleName = String.Join(",", grp.Select(x => x.RoleName).ToArray())
                             }).OrderBy(u => u.DepartmentName);    //orderby排序

            #region   校验数据
            //try //抓取异常，主要用来抓取UserID 如果前台传递UserID不是int类型，就抓取异常
            //{
            //    if (Request["UserID"] == null || Request["UserID"] == ""|| Convert.ToInt32(Request["UserID"]) == 0)
            //    {
            //        //把查询到的数据装入data容器中
            //        data = userInfoService.PageLoadEntity(pageIndex, pageSize, u => u.ID > 0, u => u.UserID, true);
            //        count = userInfoService.Select(u => u.ID > 0).Count(); //把查询到多少条数据赋值给初始化标记 count
            //    }
            //    else
            //    {
            //        //获取网页传递过来的用户编号
            //        int UserID = Convert.ToInt32(Request["UserID"]);
            //        //把查询到的数据装入data容器中
            //        data = userInfoService.PageLoadEntity(pageIndex, pageSize, u => u.UserID == UserID, u => u.UserID, true);
            //        count = userInfoService.Select(u => u.UserID == UserID).Count(); //把查询到多少条数据赋值给初始化标记 count
            //    }
            //}
            //catch //抓取异常，主要用来抓取UserID 如果前台传递UserID不是int类型，就进入下面代码块
            //{ 
            //    //把查询到的数据装入data容器中
            //    data = userInfoService.PageLoadEntity(pageIndex, pageSize, u => u.ID > 0, u => u.UserID, true);
            //    count = userInfoService.Select(u => u.ID > 0).Count(); //把查询到多少条数据赋值给初始化标记 count
            //}
            #endregion

            //转换日期的格式
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd hh':'mm':'ss";

            //把查询到的数据转换为Json格式的字符串
            string strJson = JsonConvert.SerializeObject(grouplinq, Formatting.Indented, timeConverter);

            //把Json格式的字符串改成符合layui绑定数据的格式
            string layuiStr = "{\"code\":0,\"msg\":\"\",\"count\":" + count + ",\"data\": " + strJson + "}";
            return layuiStr;
        }

        /// <summary>
        /// 删除数据,(可弃功能)
        /// </summary>
        /// <returns></returns>
        public string DelUserInfoByID()
        {
            //获取到用户编号
            int userID = Convert.ToInt32(Request["UserID"]);
            //调用方法
            if (userInfo_R_UserInfo_RoleInfo_UOW.UserInfo_R_UserInfo_RoleInfo_Delete(userID) > 0)
            {
                return "删除成功!";
            }else
            {
                return "删除失败!";
            }
        }

        /// <summary>
        /// 开启用户
        /// </summary>
        /// <returns></returns>
        public string ModifyDelFlag()
        {
            //【1】获取用户编号
            int id = Convert.ToInt32(Request["ID"]);
            //【2】根据用户编号去数据库查询数据
            UserInfo entity = userInfoService.Query(u => u.UserID == id).FirstOrDefault();
            //Request["FlagID"] == "true" 是获取网页开启用户按钮的状态（是否开启） 如果开启，delflag=0，否则deflag=1
            entity.DelFlag = Request["FlagID"] == "true" ? 0 : 1; //三元表达式
            //【3】调用修改的方法并把entity传递给方法 
            if (userInfoService.Modify(entity)>0)
            {
                //返回
                return "设置成功";
            }else
            {
                //返回
                return "错误";
            }
        }


        //public string AddAndDel()
        //{
        //    int num = 2;
        //    UserInfo model = new UserInfo();
        //    if (userInfo_R_UserInfo_RoleInfo_UOW.UserInfoAddAndDel(num,model)>0)
        //    {
        //        return "操作成功";
        //    }else
        //    {
        //        return "失败";
        //    }
        //}
    }
}