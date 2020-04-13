using DataModel.DataModels;
using System;
using EFModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using DataModel.EquelityComparer;
using System.Data;
using System.IO;
using Common.CSChef;
using Comman.ExcelHelperList;
using NPOI.SS.UserModel;
using Comman.FieldHelper;

namespace MVCMedicine.Controllers
{
    public class InventoryController : BaseController
    {
        // GET: Inventory
        /// <summary>
        /// 仓库信息视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult OutPutExcel(HttpPostedFileBase file)
        //{
        //    ExcelHelper.file = file;
        //    return View();
        //}

        /// <summary>
        /// 获取库存的查询操作
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            var EnterCompany = enterCompanyService.Query(u => u.ID > 0);
            var MedicineInfo = medicineInfoService.Query(u => u.ID > 0);
            var Inventory = inventoryService.Query(u => u.ID > 0);
            var EnterInfo = enterInfoService.Query(u => u.ID > 0);
            var Iquery = (from a in Inventory
                          join b in MedicineInfo on a.MedicineID equals b.MedicineID into C_Join
                          from c in C_Join.DefaultIfEmpty()
                          join d in EnterCompany on c.EnterCompanyID equals d.ID into E_Join
                          from e in E_Join.DefaultIfEmpty()
                          select new InventoryModels
                          {
                              ID = a.ID,
                              ChineseName = c.ChineseName,
                              ForeignName = c.ForeignName,
                              E_Name = e.E_Name,
                              Number = a.Number
                          }).ToList();

            //声明并实例化一个日期转换对象
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //设置转换日期的格式
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd hh':'mm':'ss";
            //把查询到的数据转换成Json格式的字符串
            string strJson = JsonConvert.SerializeObject(Iquery, Formatting.Indented, timeConverter);
            //把Json格式的字符串转换为layui符合条件的字符串
            string layuiStr = "{\"code\":0,\"msg\":\"\",\"data\":" + strJson + "}";
            //返回符合条件的字符串
            return layuiStr;
        }

        //public FileResult CreateWorkBook()
        //{
        //    var EnterCompany = enterCompanyService.Query(u => u.ID > 0);
        //    var MedicineInfo = medicineInfoService.Query(u => u.ID > 0);
        //    var Inventory = inventoryService.Query(u => u.ID > 0);
        //    var EnterInfo = enterInfoService.Query(u => u.ID > 0);
        //    var Iquery = (from a in Inventory
        //                  join b in MedicineInfo on a.MedicineID equals b.MedicineID into C_Join
        //                  from c in C_Join.DefaultIfEmpty()
        //                  join d in EnterCompany on c.EnterCompanyID equals d.ID into E_Join
        //                  from e in E_Join.DefaultIfEmpty()
        //                  select new InventoryModels
        //                  {
        //                      ID = a.ID,
        //                      ChineseName = c.ChineseName,
        //                      ForeignName = c.ForeignName,
        //                      E_Name = e.E_Name,
        //                      Number = a.Number
        //                  }).ToList();

        //    IWorkbook Workbook = ExcelHelper.CreateExcel(Iquery);

        //    // 写入到客户端 
        //    MemoryStream ms = new MemoryStream();
        //    Workbook.Write(ms);
        //    ms.Seek(0, SeekOrigin.Begin);
        //    DateTime dt = DateTime.Now;
        //    string dateTime = dt.ToString("yyMMddHHmmssfff");
        //    string fileName = "导出记录" + dateTime + ".xls";
        //    return File(ms, "application/vnd.ms-excel", fileName);
        //}

        public string GetExcel()
        {
            //实例化一个DataTable
            DataTable dt = new DataTable();

            //获取文件的信息
            HttpPostedFileBase file = HttpPostFieldHelper.file;
            //获取文件的名字，并进行合并
            string filePath = Path.Combine(Request.MapPath("~/SaveFile"), Path.GetFileName(file.FileName));
            //保存上传的文件
            file.SaveAs(filePath);
            //调用导入Excel表格的方法
            dt = ExcelHelper.ExcelToDataTable(filePath);

            //调用转换为List方法
            List<InventoryModels> list = InventoryHelper.GetList(dt);
            //声明并实例化一个日期转换对象
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //设置转换日期的格式
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd hh':'mm':'ss";
            //把查询到的数据转换成Json格式的字符串
            string strJson = JsonConvert.SerializeObject(list, Formatting.Indented, timeConverter);
            //把Json格式的字符串转换为layui符合条件的字符串
            string layuiStr = "{\"code\":0,\"msg\":\"\",\"data\":" + strJson + "}";
            //返回符合条件的字符串
            return layuiStr;
        }
    }
}