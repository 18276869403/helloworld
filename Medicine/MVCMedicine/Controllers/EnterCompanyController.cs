
using Comman.RegexHelpers;
using EFModel;
using MedicineService.Services;
using MVCMedicine.FilterAttribute;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCMedicine.Controllers
{
    public class EnterCompanyController : BaseController
    {
        // GET: EnterCompany
        /// <summary>
        /// 供应商首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(); //返回Index视图给Index方法
        }

        /// <summary>
        /// 添加供应商信息的视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AddEnterCompany()
        {
            return View(); //返回AddEnterCompany视图给AddEnterCompany()方法
        }

        /// <summary>
        /// 修改供应商信息的视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyEnterCompany()
        {
            //获取网页传递的供应商编号，通过"id"键获取值并转化为int类型
            int enterCompanyID = Convert.ToInt32(Request["id"]); 
            //判断：如果供应商得到的值为 (""、null、0) 
            if (enterCompanyID.ToString() == "" || enterCompanyID.ToString() == null || enterCompanyID == 0)
            {
                //如果条件成立，返回index视图
                return View(Index());
            }else
            {
                //通过获得的供应商编号去查询数据库，看看数据库是否存在记录，并且赋值给viewbag.enter，
                //在修改网页初始化的时候需要使用viewbag.enter给控件赋初始值
                ViewBag.enter = enterCompanyService.Query(u => u.ID == enterCompanyID).FirstOrDefault();
                return View(); //返回ModifyEnterCompany视图
            }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(EnterCompany entity)
        {
            /*前台通过form表单提交数据技术，前台在点击提交按钮的时候，可以直接在调用方法的时候
            获取参数《Add(EnterCompany entity)》这里获取到前台控件里面的数据并装进EnterCompany实体里面*/
            //调用enterCompanyService中的add方法，并把entity传递给方法，实现添加数据
            if (!RegexHelper.IsHandset(entity.E_Phone))
            {
                return "<script>alert('请输入13|14|15|17|18开头的11位数的手机号码！！！');window.location.href='../EnterCompany/AddEnterCompany'</script>";
            }
            if (!RegexHelper.IsPostalcode(entity.E_Zip.ToString()))
            {
                return "<script>alert('请输入由数字组成长度为6的邮编！！！');window.location.href='../EnterCompany/AddEnterCompany'</script>";
            }
            if (enterCompanyService.AddTo(entity) > 0)
            {
                //条件为真，弹出提示框“添加成功”,并跳转到'../EnterCompany/AddEnterCompany'视图
                return "<script>alert('添加成功！！！');window.location.href='../EnterCompany/AddEnterCompany'</script>";
            }
            else
            {
                //条件为假，弹出提示框“添加失败”,同样跳转到'../EnterCompany/AddEnterCompany'视图
                return "<script>alert('添加失败！！！');window.location.href='../EnterCompany/AddEnterCompany'</script>";
            }
        }

        /// <summary>
        /// 查询是否存在药品编号
        /// </summary>
        /// <returns></returns>
        public string isRepeat()
        {
            //获取前端传递的值，通过value键获取值并赋值给medicineID
            int medicineID = Convert.ToInt32(Request["value"]);
            //判断：通过前台获取到的药品编号去数据库查询是否在该编号，如果存在，条件为真
            if (enterCompanyService.Query(u => u.ID == medicineID).Count() > 0)
            {
                //条件为真，返回yes,代表该编号以及存在
                return "yes";
            }
            else
            {
                //条件为真，返回yes,代表查询不到该编号
                return "no";
            }
        }

        /// <summary>
        /// 修改供应商信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Modify(EnterCompany model)
        {
            /*前台通过form表单提交数据技术，前台在点击提交按钮的时候，可以直接在他方法的时候
            获取参数《Modify(EnterCompany model)》这里获取到前台控件里面的数据并装进EnterCompany实体里面*/
            //先查，通过前台传递过来的model.ID查询数据库
            if (!RegexHelper.IsHandset(model.E_Phone))
            {
                return "<script>alert('请输入13|15|17|18开头的11位数的手机号码！！！');window.location.href='../EnterCompany/AddEnterCompany'</script>";
            }
            if (!RegexHelper.IsPostalcode(model.E_Zip.ToString()))
            {
                return "<script>alert('请输入由数字组成长度为6的邮编！！！');window.location.href='../EnterCompany/AddEnterCompany'</script>";
            }
            EnterCompany entity = enterCompanyService.Query(u => u.ID == model.ID).FirstOrDefault();
            //判断：如果查询结果为空
            if (entity == null)
            {
                //条件为真，跳出提示框，（并查询到需要修改的信息，修改失败！！！）
                return "<script>alert('并查询到需要修改的信息，修改失败！！！');window.location.href='../EnterCompany/Index'</script>";
            }
            else
            {
                //对entity对象需要修改的属性进行赋值
                entity.ID = model.ID;
                entity.E_Name = model.E_Name;
                entity.Address = model.Address;
                entity.E_Zip = model.E_Zip;
                entity.E_Phone = model.E_Phone;
                entity.E_Fax = model.E_Fax;
                entity.Register_Address = model.Register_Address;
                entity.E_Url = model.E_Url;
                //判断：调用EnterCompanyService对象的Update方法并把entity当成参数传递给方法，条件是方法返回的大于0
                if (enterCompanyService.Modify(entity) > 0)
                {
                    //条件为真，跳出提示框，提示修改成功，并进行页面跳转--window.location.href='../EnterCompany/ModifyEnterCompany?id=" + entity.ID + "'</script>--
                    return "<script>alert('修改成功！！！');window.location.href='../EnterCompany/ModifyEnterCompany?id=" + entity.ID + "'</script>";
                }
                else
                {
                    //条件为假，跳出提示框，提示修改失败，并进行页面跳转--window.location.href='../EnterCompany/ModifyEnterCompany?id=" + entity.ID + "'</script>--
                    return "<script>alert('修改失败，信息不准确！！！');window.location.href='../EnterCompany/ModifyEnterCompany?id=" + entity.ID + "'</script>";
                }
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public string DelEnterCompanyByID()
        {
            //获取前端传递的值，通过ID键获取值并赋值给ID
            int ID = Convert.ToInt32(Request["ID"]);
            //根据获取到的编号去数据查询数据
            EnterCompany entity = enterCompanyService.Query(u => u.ID == ID).FirstOrDefault();
            //判断：查询到的结果是否存在
            if(entity != null)
            {
                //条件为真
                //判断：调用enterCompanyService.Delete(entity)方法并传参，返回值大于0
                if (enterCompanyService.Delete(entity) > 0)
                {

                    return "删除成功!";
                }else
                {
                    return "删除失败!";
                }
            }else
            {
                return "未查询到您需要删除的信息";
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            //获取前端传递的分页索引
            int pageIndex = Convert.ToInt32(Request["page"]);
            //获取每一页要查询多少条数据
            int pageSize = Convert.ToInt32(Request["limit"]);

            //实例化一个空的IQueryable<EnterCompany>对象
            IQueryable<EnterCompany> data = null;
            int count = 0;  //声明一个标记
            try
            {
                //获取前端传递的供应商编号
                int CompanyID = Convert.ToInt32(Request["companyID"]);
                //判断：供应商编号是否符合条件，只要符合任何一项，就为真
                if(CompanyID.ToString()==null||CompanyID.ToString() == ""|| CompanyID==0)
                {
                    //为真，默认查询所有
                    data = enterCompanyService.PageLoadEntity(pageIndex, pageSize, u => u.ID > 0, u => u.E_Name, true);
                    //获取在数据库中查询到的数据返回的条数
                    count = enterCompanyService.Query(u => u.ID > 0).Count();
                }else
                {
                    //为假，根据供应商编号查询数据
                    data = enterCompanyService.PageLoadEntity(pageIndex, pageSize, u => u.ID == CompanyID, u => u.E_Name, true);
                    //获取在数据库中查询到的数据返回的条数
                    count = enterCompanyService.Query(u => u.ID == CompanyID).Count();
                }
            }
            catch
            {
                //抓取异常，当供应商编号获取异常时，默认查询所有
                data = enterCompanyService.PageLoadEntity(pageIndex, pageSize, u => u.ID > 0, u => u.E_Name, true);
                //获取在数据库中查询到的数据返回的条数
                count = enterCompanyService.Query(u => u.ID > 0).Count();
            }
            //声明并实例化一个日期转换对象
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd hh':'mm':'ss"; //设置转换的格式

            //把查询到的数据转换成Json格式的字符串
            string strJson = JsonConvert.SerializeObject(data, Formatting.Indented, timeConverter);
            //把Json格式的字符串转换为layui符合条件的字符串
            string layuiStr = "{\"code\":0,\"msg\":\"\",\"count\":" + count + ",\"data\":" + strJson + "}";
            return layuiStr;    //返回符合条件的字符串
        }
    }
}