
using DataModel.DataModels;
using EFModel;
using MedicineService.Services;
using MVCMedicine.FilterAttribute;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMedicine.Controllers
{
    //[MyPowerFilter(RoleID = "1")]
    public class MedicineController : BaseController
    {
        // GET: Medicine
        /// <summary>
        /// 药品信息管理首页
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            //List<MedicineInfo> list = MedicineInfoService.Select(u => u.ID > 0).ToList();
            //ViewBag.mylist = list;
            return View();
        }
        
        /// <summary>
        /// 添加药品信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddMedicineInfo()
        {
            ViewBag.ClassifyInfo = classifyService.Query(u => u.ID > 0).ToList();
            ViewBag.DosageInfo = dosageTypeService.Query(u => u.ID > 10).ToList();
            ViewBag.RepositInfo = repositService.Query(u => u.ID > 0).ToList();
            ViewBag.EnterCompanyInfo = enterCompanyService.Query(u => u.ID > 0).ToList();
            ViewBag.PackInfo = packTableService.Query(u => u.ID > 0).ToList();
            return View();
        }

        /// <summary>
        /// 修改药品信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyMedicineInfo()
        {
            string medicineID = Request["id"];
            if(medicineID == "" || medicineID == null)
            {
                return View("<script>alert('药品编号错误！！！');window.location.href='../Medicine/Index'</script>"); 
            }
            MedicineInfo model = medicineInfoService.Query(u => u.MedicineID == medicineID).FirstOrDefault();
            ViewBag.ClassifyInfo = classifyService.Query(u => u.ID > 0).ToList();
            ViewBag.DosageInfo = dosageTypeService.Query(u => u.ID > 10).ToList();
            ViewBag.RepositInfo = repositService.Query(u => u.ID > 0).ToList();
            ViewBag.EnterCompanyInfo = enterCompanyService.Query(u => u.ID > 0).ToList();
            ViewBag.PackInfo = packTableService.Query(u => u.ID > 0).ToList();
            ViewBag.model = model;
            return View();
        }

        /// <summary>
        /// 添加药品信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(MedicineInfo entity)
        {
            if(medicineInfoService.AddTo(entity) > 0)
            {
                return "<script>alert('添加成功！！！');window.location.href='../Medicine/AddMedicineInfo'</script>";
            }else
            {
                return "<script>alert('添加失败！！！');window.location.href='../Medicine/AddMedicineInfo'</script>";
            }
        }

        /// <summary>
        /// 修改药品信息
        /// </summary>
        /// <returns></returns>
        public string Modify(MedicineInfo model)
        {
            MedicineInfo entity = medicineInfoService.Query(u => u.MedicineID == model.MedicineID).FirstOrDefault();

            if(entity == null)
            {
                return "<script>alert('并未查询到需要修改的信息，修改失败！！！');window.location.href='../Medicine/Index'</script>";
            }else
            {
                entity.ChineseName = model.ChineseName;
                entity.ForeignName = model.ForeignName;
                entity.ClassifyID = model.ClassifyID;
                entity.DosageID = model.DosageID;
                entity.EnterCompanyID = model.EnterCompanyID;
                entity.Etalon = model.Etalon;
                entity.Ingredient = model.Ingredient;
                entity.M_Function = model.M_Function;
                entity.Notes = model.Notes;
                entity.PackID = model.PackID;
                entity.Pharmacology = model.Pharmacology;
                entity.RepositID = model.RepositID;
                entity.Taboo = model.Taboo;
                if (medicineInfoService.Modify(entity) > 0)
                {
                    return "<script>alert('修改成功！！！');window.location.href='../Medicine/ModifyMedicineInfo?id=" + entity.MedicineID + "'</script>";
                }else
                {
                    return "<script>alert('修改失败，信息不准确！！！');window.location.href='../Medicine/ModifyMedicineInfo?id=" + entity.MedicineID + "'</script>";
                }
            }
        }

        /// <summary>
        /// 查询是否存在药品编号
        /// </summary>
        /// <returns></returns>
        public string isRepeat()
        {
            string medicineID = Request["value"];
            if(medicineInfoService.Query(u => u.MedicineID == medicineID).Count() > 0)
            {
                return "no";
            }
            else
            {
                return "yes";
            }
        }

        /// <summary>
        /// 删除药品
        /// </summary>
        /// <returns></returns>
        public string DelMedicineInfoByID()
        {
            string MedicineID = Request["DelID"];
            MedicineInfo entity = medicineInfoService.Query(u => u.MedicineID == MedicineID).FirstOrDefault();
            if (entity != null)
            {
                if (medicineInfoService.Delete(entity) > 0)
                {
                    return "删除成功！！！'";
                }
            }
            return "删除成功！！！";
        }

        /// <summary>
        /// 查询+搜索
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            //获取前台传递的多少页的数据和每页有多少条数据
            int PageIndex = Convert.ToInt32(Request["page"]);
            int PageSize = Convert.ToInt32(Request["limit"]);

            string medicineID = Request["medicineID"];
            
            IQueryable<MedicineInfo> data = null;
            int count = 0;
            if (medicineID == null || medicineID == "")
            {
                //分页查询
                data = medicineInfoService.PageLoadEntity(PageIndex, PageSize, u => u.ID > 0, u => u.MedicineID, true);
                count = medicineInfoService.Query(u => u.ID > 0).Count(); //查询数据库返回的总行数
            }
            else
            {
                //分页查询
                data = medicineInfoService.PageLoadEntity(PageIndex, PageSize, u => u.MedicineID.Contains(medicineID), u => u.MedicineID, true);
                count = medicineInfoService.Query(u => u.MedicineID.Contains(medicineID)).Count(); //查询数据库返回的总行数
            }

            IQueryable<DosageType> dtModel = dosageTypeService.Query(u => u.ID > 10);
            IQueryable<Inventory> iModel = inventoryService.Query(u => u.ID > 0);
            IQueryable<Classify> cModel = classifyService.Query(u => u.ID > 0);
            IQueryable<EnterCompany> eModel = enterCompanyService.Query(u => u.ID > 0);
            IQueryable<PackTable> ptModel = packTableService.Query(u => u.ID > 0);
            IQueryable<Reposit> rModel = repositService.Query(u => u.ID > 0);

            IQueryable<MedicineInfoModels> linqModel = (from a in data
                                                        join b in iModel on a.MedicineID equals b.MedicineID into b_join
                                                        from c in b_join.DefaultIfEmpty()
                                                        join d in dtModel on a.DosageID equals d.ID into c_join
                                                        from e in c_join.DefaultIfEmpty()
                                                        join f in cModel on a.ClassifyID equals f.ID into d_join
                                                        from g in d_join.DefaultIfEmpty()
                                                        join h in eModel on a.EnterCompanyID equals h.ID into e_join
                                                        from i in e_join.DefaultIfEmpty()
                                                        join j in ptModel on a.PackID equals j.ID into f_join
                                                        from k in f_join.DefaultIfEmpty()
                                                        join h in rModel on a.RepositID equals h.ID into j_join
                                                        from m in j_join.DefaultIfEmpty()
                                                        select new MedicineInfoModels
                                                        {
                                                            ChineseName = a.ChineseName,
                                                            ForeignName = a.ForeignName,
                                                            MedicineID = a.MedicineID,
                                                            ClassifyName = g.ClassifyName,
                                                            DosageName = e.DosageName,
                                                            E_Name = i.E_Name,
                                                            RepositName = m.RepositName,
                                                            Etalon = a.Etalon,
                                                            PackName = k.PackName,
                                                            Pharmacology = a.Pharmacology,
                                                            Ingredient = a.Ingredient,
                                                            M_Function = a.M_Function,
                                                            Notes = a.Notes,
                                                            Taboo = a.Taboo
                                                        });

            //设置时间的格式
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter(); 
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd hh':'mm':'ss";

            //序列化data转换为Json格式
            string strJson = JsonConvert.SerializeObject(linqModel, Formatting.Indented, timeConverter);
            //把序列化的Json字符串拼接成为layui数据表格能够接收的数据格式
            string layuiStr = "{\"code\":0,\"msg\":\"\",\"count\":" + count + ",\"data\": " + strJson + "}";

            return layuiStr;//返回layuiStr
        }
    }
}