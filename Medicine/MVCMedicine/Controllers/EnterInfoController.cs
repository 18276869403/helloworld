
using Comman.ExcelHelperList;
using Comman.FieldHelper;
using Comman.RegexHelpers;
using Common.CSChef;
using DataModel.DataModels;
using EFModel;
using MedicineService.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMedicine.Controllers
{
    public class EnterInfoController : BaseController
    {
        // GET: EnterInfo
        /// <summary>
        /// 进货信息首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //返回Index视图
            return View();
        }

        /// <summary>
        /// 添加进货信息的视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AddEnterInfo()
        {
            //返回AddEnterInfo视图
            ViewBag.Medicine = medicineInfoService.Query(u => u.ID > 0);
            ViewBag.EnterCompany = enterCompanyService.Query(u => u.ID > 0);
            return View();
        }

        /// <summary>
        /// 修改进货信息的视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyEnterInfo()
        {
            //获取到网页传递的编号并赋值
            int ID = Convert.ToInt32(Request["ID"]);
            string MedicineID = Request["MedicineID"];
            EnterFieldHelper.MedicineID = MedicineID;
            //根据编号去数据查询数据，并赋值给ViewBag.enter，访问修改进货信息的视图需要使用ViewBag.enter给控件绑定初始值
            EnterInfo enter = enterInfoService.Query(u => u.ID == ID).FirstOrDefault();
            int EnterID = enter.ID;
            EnterFieldHelper.E_ID = EnterID;
            ViewBag.enter = enter;
            ViewBag.Company = enterCompanyService.Query(u => u.ID == EnterID).FirstOrDefault();
            ViewBag.Medicine = medicineInfoService.Query(u => u.MedicineID == MedicineID).FirstOrDefault();
            //返回ModifyEnterInfo视图给 ModifyEnterInfo() 方法
            return View();
        }

        /// <summary>
        /// 跳转导入Excel表格批量添加页
        /// </summary>
        /// <param name="file">获取到文件路径</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddToEnterInfo(HttpPostedFileBase file)
        {
            HttpPostFieldHelper.file = file;
            return View();
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <returns></returns>
        public string BacthAddTo()
        {
            DataTable dt = new DataTable();
            HttpPostedFileBase file = HttpPostFieldHelper.file;
            string path = Path.GetFileName(file.FileName);
            string strPath = Path.Combine(Request.MapPath("~/SaveFile"),path);
            file.SaveAs(strPath);
            dt = ExcelHelper.RenderDataTableFromExcel(strPath, "Sheet1",0);

            //调用转换为List方法
            List<EnterInfo> list = EnterInfoHelper.GetList(dt);

            if (EnterInfo_Inventory_UOW.BacthAddToModify(list)>0)
            {
                //跳出提示框，并跳转页面
                return "yes";
            }else
            {
                //跳出提示框，并跳转页面
                return "no";
            }
            
        }

        /// <summary>
        /// 查询库存信息
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            //获取前端传递的分页索引
            int pageIndex = Convert.ToInt32(Request["page"]);
            //获取每一页要查询多少条数据
            int pageSize = Convert.ToInt32(Request["limit"]);
            
            //获取前端传递的药品编号
            string medicineID = Request["ID"];
            //实例化一个空的IQueryable<EnterInfo>对象
            IQueryable<EnterInfo> data = null;
            int count = 0;

            //判断：药品编号是否符合条件，符合为真，否则为假
            if(medicineID == null||medicineID == "" )
            {
                //条件为真，默认查询所有
                data = enterInfoService.PageLoadEntity(pageIndex,pageSize,u => u.ID > 0,u=> u.MedicineID,true );
                count = enterInfoService.Query(u => u.ID > 0).Count();
            }else
            {
                //条件为假，根据药品编号查询
                data = enterInfoService.PageLoadEntity(pageIndex, pageSize, u => u.MedicineID.Contains(medicineID), u => u.MedicineID, true);
                count = enterInfoService.Query(u => u.MedicineID.Contains(medicineID)).Count();
            }

            var EnterCompany = enterCompanyService.Query(u => u.ID > 0);
            var MedicineInfo = medicineInfoService.Query(u => u.ID > 0);
            var Inventory = inventoryService.Query(u => u.ID > 0);
            var Iquery = (from a in data
                          join b in MedicineInfo on a.MedicineID equals b.MedicineID into C_Join
                          from c in C_Join.DefaultIfEmpty()
                          join d in EnterCompany on c.EnterCompanyID equals d.ID into E_Join
                          from e in E_Join.DefaultIfEmpty()
                          join f in Inventory on a.MedicineID equals f.MedicineID into F_Join
                          from g in F_Join.DefaultIfEmpty()
                          select new InventoryModels
                          {
                              ID = a.ID,
                              ChineseName = c.ChineseName,
                              ForeignName = c.ForeignName,
                              MedicineID = c.MedicineID,
                              E_Name = e.E_Name,
                              MarketNumber = a.MarketNumber,
                              EnterDate = a.EnterDate,
                              Price = a.EnterPrice
                          }).ToList();


            //声明并实例化一个日期转换对象
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //设置转换日期的格式
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd hh':'mm':'ss";
            //把查询到的数据转换成Json格式的字符串
            string strJson = JsonConvert.SerializeObject(Iquery, Formatting.Indented, timeConverter);
            //把Json格式的字符串转换为layui符合条件的字符串
            string layuiStr = "{\"code\":0,\"msg\":\"\",\"count\":" + count + ",\"data\":" + strJson + "}";
            //返回符合条件的字符串
            return layuiStr;
        }

        /// <summary>
        /// 删除库存信息
        /// </summary>
        /// <returns></returns>
        public string DelEnterInfoByID()
        {
            if (!RegexHelper.IsNumber(Request["ID"]))
            {
                return "请提供正确的进货编号";
            }
            else
            {
                //获取前端传递的药品编号
                int ID = Convert.ToInt32(Request["ID"]);

                //先查再删
                //根据获取到的药品编号去数据库查询数据
                EnterInfo entity = enterInfoService.Query(u => u.ID == ID).FirstOrDefault();
                if (entity != null) //判断：查询的数据不为空
                {
                    //条件为真，调用删除数据的方法并传参
                    if (enterInfoService.Delete(entity) > 0)
                    {
                        //返回结果
                        return "删除成功!";
                    }
                    else
                    {
                        //返回结果
                        return "删除失败!";
                    }
                }
                else
                {
                    //返回结果
                    return "未查询到符合的数据";
                }
            }
        }

        /// <summary>
        /// 添加进货信息同时对库存表增加进货的数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(EnterInfo Model)
        {
            if (!RegexHelper.IsNumber(Model.MarketNumber.ToString()))
            {
                return "<script>alert('数量必须为数字！！！');window.location.href='../EnterInfo/AddEnterInfo'</script>";
            }
            //判断从前端获取的数据是否存在,当数据不存在，条件为真
            if (Model == null)
            {
                //跳出提示框，并跳转页面
                return "<script>alert('数据不能为空！！！');window.location.href='../EnterInfo/AddEnterInfo'</script>";
            }else
            {
                EnterInfo Entity = new EnterInfo
                {
                    MedicineID = Model.MedicineID,
                    EnterCompanyID = Model.EnterCompanyID,
                    EnterPrice = Model.EnterPrice,
                    EnterDate = DateTime.Now,
                    MarketNumber = Model.MarketNumber,
                    EndDate = DateTime.Now.AddYears(1)
                };
                if (Model.MarketNumber <= 0)
                {
                    return "<script>alert('数量必须大于0！！！');window.location.href='../EnterInfo/AddEnterInfo'</script>";
                }
                if(Model.EnterPrice <= 1)
                {
                    return "<script>alert('价格不能小于1！！！');window.location.href='../EnterInfo/AddEnterInfo'</script>";
                }
                //判断：调用添加方法并传参，方法返回值大于0的时候，条件为真
                if (EnterInfo_Inventory_UOW.AddToModify(Entity) >0)
                {
                    //返回结果，弹窗并跳转界面
                    return "<script>alert('添加成功！！！');window.location.href='../EnterInfo/AddEnterInfo'</script>";
                }
                else
                {
                    //返回结果，弹窗并跳转界面
                    return "<script>alert('添加失败！！！');window.location.href='../EnterInfo/AddEnterInfo'</script>";
                }
            }
        }

        /// <summary>
        /// 查询是否存在药品编号
        /// </summary>
        /// <returns></returns>
        public string isRepeatMedicineID()
        {
            //获取前端传递的药品编号
            string medicineID = Request["value"];
            //判断：根据药品编号调用查询方法，方法返回值大于0，条件为真
            if (enterInfoService.Query(u => u.MedicineID == medicineID).Count() > 0)
            {
                //返回结果
                return "yes";
            }
            else
            {
                //返回结果
                return "no";
            }
        }

        /// <summary>
        /// 查询供应商是否存在
        /// </summary>
        /// <returns></returns>
        public string isRepeatCompanyID()
        {
            //获取前端传递的供应商编号
            if (!RegexHelper.IsNumber(Request["value"]))
            {
                return "请输入正确的供应商编号！！！";
            }
            int enterCompanyID = Convert.ToInt32(Request["value"]);
            //判断：根据供应商的编号，调用查询方法，方法返回值大于0，条件为真
            if (enterInfoService.Query(u => u.EnterCompanyID == enterCompanyID).Count() > 0)
            {
                //返回结果
                return "yes";
            }
            else
            {
                //返回结果
                return "该供应商编号已存在！！！";
            }
        }

        /// <summary>
        /// 修改进货信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Modify(EnterInfo model)
        {
            //先查再删
            /*前台通过form表单提交数据技术，前台在点击提交按钮的时候，可以直接在他方法的时候
            获取参数《Modify(EnterInfo model)》这里获取到前台控件里面的数据并装进EnterInfo实体里面*/
            //先查，通过前台传递过来的model.ID查询数据库，并赋值给EnterInfo实体
            if (!RegexHelper.IsNumber(model.MarketNumber.ToString()))
            {
                return "<script>alert('供应商必须为数字！！！');window.location.href='../EnterInfo/ModifyEnterInfo?ID=" + model.ID + "&MedicineID=" + EnterFieldHelper.MedicineID + "'</script>";
            }
            EnterInfo entity = enterInfoService.Query(u => u.ID == model.ID).FirstOrDefault();
            if (entity == null)
            {
                return "<script>alert('未查询到需要修改的信息，修改失败！！！');window.location.href='../EnterInfo/ModifyEnterInfo?ID=" + model.ID + "&MedicineID=" + EnterFieldHelper.MedicineID + "'</script>";
            }
            else
            {
                entity.MedicineID = EnterFieldHelper.MedicineID;
                entity.MarketNumber = model.MarketNumber;
                entity.EnterCompanyID = EnterFieldHelper.E_ID ;
                entity.EnterDate = model.EnterDate;
                entity.EnterPrice = model.EnterPrice;
                if(model.MarketNumber < 0)
                {
                    return "<script>alert('数量不能小于或等于0');window.location.href='../EnterInfo/ModifyEnterInfo?ID=" + model.ID + "&MedicineID=" + EnterFieldHelper.MedicineID + "'</script>";
                }

                if (EnterInfo_Inventory_UOW.ModifyToInventory(entity) > 0)
                {
                    return "<script>alert('修改成功！！！');window.location.href='../EnterInfo/ModifyEnterInfo?ID=" + model.ID + "&MedicineID=" + EnterFieldHelper.MedicineID + "'</script>";
                }
                else
                {
                    return "<script>alert('修改失败，数量不能大于库存数量！！！');window.location.href='../EnterInfo/ModifyEnterInfo?ID=" + model.ID + "&MedicineID=" + EnterFieldHelper.MedicineID + "'</script>";
                }
            }
        }
    }
}