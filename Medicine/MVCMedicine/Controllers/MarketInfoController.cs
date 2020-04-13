
using Comman.DateHelpers;
using Comman.ExcelHelperList;
using Comman.FieldHelper;
using Common.CSChef;
using DataModel.DataModels;
using EFModel;
using MedicineService.Services;
using MVCMedicine.FilterAttribute;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NPOI.SS.UserModel;
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
    //[MyPowerFilter(RoleID = "1,2,5")]
    public class MarketInfoController : BaseController
    {
        // GET: MarketInfo
        /// <summary>
        /// 销售信息首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 售药信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddMarketInfo()
        {
            string ID = Request["id"];
            MarketFieldHelper.ID = Convert.ToInt32(ID);
            string medicineID = Request["MedicineID"];
            MarketFieldHelper.MedicineID = medicineID;
            int intUserID = Convert.ToInt32(UserID);
            ViewBag.userInfo = userInfoService.Query(u => u.UserID > intUserID).FirstOrDefault();
            ViewBag.medicineModel = medicineInfoService.Query(u => u.MedicineID == medicineID).FirstOrDefault();
            ViewBag.Price = PriceServices.Query(u => u.MedicineID == medicineID).FirstOrDefault();
            return View();
        }

        public ActionResult ModifyMarketInfo()
        {
            int ID = Convert.ToInt32(Request["id"]);
            int UserID = Convert.ToInt32(Request["UserID"]);
            string medicineID = Request["MedicineID"];

            if (ID != MarketFieldHelper.ID||UserID != MarketFieldHelper.UserID||MarketFieldHelper.MedicineID != medicineID)
            {
                MarketFieldHelper.ID = 0;
                MarketFieldHelper.UserID = 0;
                MarketFieldHelper.MedicineID = "";
            }
            if(MarketFieldHelper.MedicineID == null||MarketFieldHelper.MedicineID == "")
            {
                MarketFieldHelper.MedicineID = medicineID;
            }
            if (MarketFieldHelper.ID == 0 || MarketFieldHelper.ID.ToString() == "" || MarketFieldHelper.ID.ToString() == null)
            {
                MarketFieldHelper.ID = ID;
            }
            if (MarketFieldHelper.UserID == 0 || MarketFieldHelper.ID.ToString() == ""|| MarketFieldHelper.UserID.ToString() == null)
            {
                MarketFieldHelper.UserID = UserID;
            }
            ViewBag.UserInfo = userInfoService.Query(u => u.UserID == UserID).FirstOrDefault();
            ViewBag.medicineModel = medicineInfoService.Query(u => u.MedicineID == medicineID).FirstOrDefault();
            ViewBag.MarketInfo = marketInfoService.Query(u => u.ID == ID).FirstOrDefault();
            ViewBag.Price = PriceServices.Query(u => u.MedicineID == medicineID).FirstOrDefault();
            return View();
        }

        /// <summary>
        /// 查询药品信息以及保质期
        /// </summary>
        /// <returns></returns>
        public string GetInventoryJson()
        {
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["limit"]);

            string medicine = Request["MedicineID"];
            IQueryable<MedicineInfo> data = null;
            int count = 0;

            if (medicine != "" && medicine != null)
            {
                data = medicineInfoService.PageLoadEntity(pageIndex, pageSize, u => u.MedicineID.Contains(medicine), u=>u.MedicineID, true);
                count = medicineInfoService.Query(u => u.MedicineID.Contains(medicine)).Count() ;
            }
            else
            {
                data = medicineInfoService.PageLoadEntity(pageIndex, pageSize, u => u.ID >0 , u => u.MedicineID, true);
                count = medicineInfoService.Query(u => u.ID > 0).Count();
            }

            IQueryable<Inventory> Inventory = inventoryService.Query(u => u.ID > 0);
            IQueryable<EnterCompany> EnterCompany = enterCompanyService.Query(u => u.ID > 0);
            IQueryable<EnterInfo> EnterInfo = enterInfoService.Query(u => u.ID > 0);
            IQueryable<PriceInfo> Price = PriceServices.Query(u => u.ID > 0);

            IQueryable<InventoryModels> InventoryModel = (from a in data
                                                        join b in Inventory on a.MedicineID equals b.MedicineID into b_join
                                                        from c in b_join.DefaultIfEmpty()
                                                        join d in EnterCompany on a.EnterCompanyID equals d.ID into c_join
                                                        from e in c_join.DefaultIfEmpty()
                                                        join f in Price on a.MedicineID equals f.MedicineID into d_join
                                                        from g in d_join.DefaultIfEmpty()
                                                        join h in EnterInfo on a.MedicineID equals h.MedicineID into e_join
                                                        from i in e_join.DefaultIfEmpty()
                                                        select new InventoryModels
                                                        {
                                                            ID = a.ID,
                                                            MedicineID = a.MedicineID,
                                                            ChineseName = a.ChineseName,
                                                            ForeignName = a.ForeignName,
                                                            EnterDate = i.EnterDate,
                                                            EndDate = i.EndDate,
                                                            Number = c.Number,
                                                            PriceID = g.ID,
                                                            Price = g.Price,
                                                        });
            List<InventoryModels> list = new List<InventoryModels>();
            foreach(var item in InventoryModel.ToList())
            {
                InventoryModels model = new InventoryModels();
                model.MedicineID = item.MedicineID;
                model.ID = item.ID;
                model.ChineseName = item.ChineseName;
                model.ForeignName = item.ForeignName;
                model.EnterDate = item.EnterDate;
                model.EndDate = item.EndDate;
                model.Number = item.Number;
                MarketFieldHelper.PriceID = item.PriceID;
                model.Price = item.Price;
                model.Guarantee = DateHelper.DateDiff(item.EnterDate, item.EndDate);
                list.Add(model);                                                                           
            }
            IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverter();
            datetimeConverter.DateTimeFormat = "yyyy'-'MM'-'dd hh':'mm':'ss";

            string strJson = JsonConvert.SerializeObject(list, Formatting.Indented, datetimeConverter);

            string layuiStr = "{\"code\":0,\"msg\":\"\",\"count\":" + count + ",\"data\":" + strJson + "}";
            return layuiStr;
        }

        public string ModifyMarketAndInventory(MarketInfoModels model)
        {
            if(model != null)
            {
                model.ID = MarketFieldHelper.ID;
                model.UserID = MarketFieldHelper.UserID;
                model.MedicineID = MarketFieldHelper.MedicineID;
                MarketInfo numModel = marketInfoService.Query(u => u.ID == model.ID).FirstOrDefault();
                if(model.MarketNumber <= 0)
                {
                    return "<script>alert('请填写大于0的退货数据！！！');window.location.href='../MarketInfo/ModifyMarketInfo?MedicineID=" + model.MedicineID + "&id=" + model.ID + "&UserID="+ model.UserID +"'</script>";
                }
                if (model.MarketNumber > numModel.MarketNumber)
                {
                    return "<script>alert('退货数量不能超过购买数量！！！');window.location.href='../MarketInfo/ModifyMarketInfo?MedicineID=" + model.MedicineID + "&id=" + model.ID + "&UserID=" + model.UserID + " '</script>";
                }
                MarketFieldHelper.Market.Add(model);

                if (MarketInfo_Inventory_UOW.BacthModify(model) > 0) {
                    return "<script>alert('修改成功！！！');window.location.href='../MarketInfo/ModifyMarketInfo?MedicineID=" + model.MedicineID + "&id=" + model.ID + "&UserID=" + model.UserID + " '</script>";
                }else
                {
                    return "<script>alert('修改失败！！！');window.location.href='../MarketInfo/ModifyMarketInfo?MedicineID=" + model.MedicineID + "&id=" + model.ID + "&UserID=" + model.UserID + " '</script>";
                }
            }else
            {
                return "<script>alert('数据不能为空！！！');window.location.href='../MarketInfo/ModifyMarketInfo?MedicineID=" + model.MedicineID + "&id=" + model.ID + "&UserID=" + model.UserID + " '</script>";
            }
        }

        #region MyRegion
        public ActionResult CreateWork()
        {
            IWorkbook Workbook = ExcelHelper.CreateExcel(MarketFieldHelper.Market);

            // 写入到客户端 
            MemoryStream ms = new MemoryStream();
            Workbook.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            DateTime dt = DateTime.Now;
            string dateTime = dt.ToString("yyMMddHHmmssfff");
            string fileName = "导出退货记录" + dateTime + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
        }

        //public FileResult CreateWorkBook()
        //{
        //    #region MyRegion
        //    //var EnterCompany = enterCompanyService.Query(u => u.ID > 0);
        //    //var MedicineInfo = medicineInfoService.Query(u => u.ID > 0);
        //    //var Inventory = inventoryService.Query(u => u.ID > 0);
        //    //var EnterInfo = enterInfoService.Query(u => u.ID > 0);
        //    //var Iquery = (from a in Inventory
        //    //              join b in MedicineInfo on a.MedicineID equals b.MedicineID into C_Join
        //    //              from c in C_Join.DefaultIfEmpty()
        //    //              join d in EnterCompany on c.EnterCompanyID equals d.ID into E_Join
        //    //              from e in E_Join.DefaultIfEmpty()
        //    //              select new InventoryModels
        //    //              {
        //    //                  ID = a.ID,
        //    //                  ChineseName = c.ChineseName,
        //    //                  ForeignName = c.ForeignName,
        //    //                  E_Name = e.E_Name,
        //    //                  Number = a.Number
        //    //              }).ToList();
        //    #endregion

        //    //IWorkbook Workbook = ExcelHelper.CreateExcel(Iquery);

        //    // 写入到客户端 
        //    MemoryStream ms = new MemoryStream();
        //    Workbook.Write(ms);
        //    ms.Seek(0, SeekOrigin.Begin);
        //    DateTime dt = DateTime.Now;
        //    string dateTime = dt.ToString("yyMMddHHmmssfff");
        //    string fileName = "导出退货记录" + dateTime + ".xls";
        //    return File(ms, "application/vnd.ms-excel", fileName);
        //}
        #endregion

        /// <summary>
        /// 添加销售信息功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddToModify(MarketInfo model)
        {
            if (model!= null)
            {
                model.UserID = int.Parse(UserID);
                model.PriceID = MarketFieldHelper.PriceID;
                model.MedicineID = MarketFieldHelper.MedicineID;
                Inventory numModel = inventoryService.Query(u => u.MedicineID == model.MedicineID).FirstOrDefault();
                if(numModel.Number <= 0)
                {
                    return "<script>alert('库存不足，购买失败！！！');window.location.href='../MarketInfo/AddMarketInfo?MedicineID=" + model.MedicineID+"&id"+ model.ID + " '</script>";
                }
                if(numModel.Number < model.MarketNumber)
                {
                    return "<script>alert('购买数量超出库存，购买失败！！！');window.location.href='../MarketInfo/AddMarketInfo?MedicineID=" + model.MedicineID + "&id" + model.ID + " '</script>";
                }
                if (MarketInfo_Inventory_UOW.BacthAddToModify(model) > 0)
                {
                    return "<script>alert('购买成功！！！');window.location.href='../MarketInfo/AddMarketInfo?MedicineID=" + model.MedicineID+"&id"+ model.ID + " '</script>";
                }else
                {
                    return "<script>alert('购买失败！！！');window.location.href='../MarketInfo/AddMarketInfo?MedicineID=" + model.MedicineID + "&id" + model.ID + " '</script>";
                }
            }else
            {
                return "<script>alert('数据不能为空！！！');window.location.href='../MarketInfo/AddMarketInfo?MedicineID=" + model.MedicineID + "&id" + model.ID + " '</script>";
            }
        }

        /// <summary>
        /// 查询销售信息
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["limit"]);
            IQueryable<MarketInfo> data = null;
            int count = 0;

            try
            {
                int UserID = Convert.ToInt32(Request["UserID"]);
                if (UserID.ToString() == null || UserID.ToString() == "" || UserID == 0)
                {
                    data = marketInfoService.PageLoadEntity(pageIndex, pageSize, u => u.ID > 0, u => u.ID, true);
                    count = marketInfoService.Query(u => u.ID > 0).Count();
                }
                else
                {
                    data = marketInfoService.PageLoadEntity(pageIndex, pageSize, u => u.UserID == UserID, u => u.ID, true);
                    count = marketInfoService.Query(u => u.UserID == UserID).Count();
                }

            }
            catch (Exception)
            {
                data = marketInfoService.PageLoadEntity(pageIndex, pageSize, u => u.ID > 0, u => u.ID, true);
                count = marketInfoService.Query(u => u.ID > 0).Count();
            }

            IQueryable<MedicineInfo> medicineModel = medicineInfoService.Query(u => u.ID > 0);
            IQueryable<UserInfo> userModel = userInfoService.Query(u => u.ID > 0);
            IQueryable<PriceInfo> Price = PriceServices.Query(u => u.ID > 0);

            IQueryable<MarketInfoModels> marketModel = (from a in data
                                                  join b in medicineModel on a.MedicineID equals b.MedicineID into b_join
                                                  from c in b_join.DefaultIfEmpty()
                                                  join d in userModel on a.UserID equals d.UserID into c_join
                                                  from e in c_join.DefaultIfEmpty()
                                                  join f in Price on a.MedicineID equals f.MedicineID into d_join
                                                  from g in d_join.DefaultIfEmpty()
                                                  select new MarketInfoModels
                                                  {
                                                      ID =a.ID,
                                                      MedicineID = c.MedicineID,
                                                      ChineseName = c.ChineseName,
                                                      UserID = e.UserID,
                                                      UserName = e.UserName,
                                                      SellTime = a.SellTime,
                                                      ForeignName = c.ForeignName,
                                                      MarketNumber = a.MarketNumber,
                                                      Price = g.Price
                                                  });

            IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverter();
            datetimeConverter.DateTimeFormat = "yyyt'-'MM'-'dd hh':'mm':'ss";

            string strJson = JsonConvert.SerializeObject(marketModel, Formatting.Indented, datetimeConverter);

            string layuiStr = "{\"code\":0,\"msg\":\"\",\"count\":" + count + ",\"data\":" + strJson + "}";
            return layuiStr;
        }

        /// <summary>
        /// 删除销售信息
        /// </summary>
        /// <returns></returns>
        public string DelMarketInfoByID()
        {
            int ID = Convert.ToInt32(Request["ID"]);
            MarketInfo entity = marketInfoService.Query(u => u.ID == ID).FirstOrDefault();

            if(entity!= null)
            {
                if (marketInfoService.Delete(entity) > 0)
                {
                    return "删除成功!";
                }
            }
            return "删除失败!";
        }
    }
}