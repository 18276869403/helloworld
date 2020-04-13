
using DataModel.DataModels;
using EFModel;
using MedicineService.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMedicine.Controllers
{
    public class RoleInfoController : BaseController
    {
        // GET: RoleInfo
        /// <summary>
        /// 角色信息首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加角色信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddRoleInfo()
        {
            return View();
        }

        /// <summary>
        /// 修改角色信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyRoleInfo()
        {
            int roleID = Convert.ToInt32(Request["RoleID"]);
            ViewBag.model = roleInfoService.Query(u => u.RoleID == roleID).FirstOrDefault();
            return View();
        }

        /// <summary>
        /// 分配角色权限页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AllotPower()
        {
            return View();
        }

        /// <summary>
        /// 分配权限信息
        /// </summary>
        /// <returns></returns>
        public string GetPowerAllotJson()
        {
            int RoleID = Convert.ToInt32(Request["RoleID"]);
            IQueryable<PowerInfo> powerModel = powerInfoService.Query(u => u.ID > 0);
            IQueryable<R_RoleInfo_PowerInfo> rrpModel = r_RoleInfo_PowerInfoService.Query(u => u.RoleID == RoleID);
            var linqModel = (from a in powerModel
                             join b in rrpModel on a.PowerID equals b.PowerID into b_join
                             from c in b_join.DefaultIfEmpty()
                             select new Role_Power_Models
                             {
                                 powerDescription = a.Description,
                                 PowerID = a.PowerID,
                                 ActionUrl = a.ActionUrl,
                                 RoleID = c.RoleID.ToString()
                             });

            IsoDateTimeConverter dateTimeConverter = new IsoDateTimeConverter();
            dateTimeConverter.DateTimeFormat = "yyyy'-'MM'-'dd hh':'mm':'ss";
            string strJson = JsonConvert.SerializeObject(linqModel, Formatting.Indented, dateTimeConverter);
            string layuiStr = "{\"code\":0,\"msg\":\"\",\"data\":" + strJson + "}";
            return layuiStr;
        }

        /// <summary>
        /// 分配权限
        /// </summary>
        /// <returns></returns>
        public string ModifyAllot()
        {
            int roleID = Convert.ToInt32(Request["RoleID"]);
            int powerID = Convert.ToInt32(Request["PowerID"]);
            string check = Request["check"];
            if (check == "true")
            {
                R_RoleInfo_PowerInfo entity = new R_RoleInfo_PowerInfo
                {
                    RoleID = roleID,
                    PowerID = powerID
                };
                if (r_RoleInfo_PowerInfoService.AddTo(entity)>0)
                {
                    return "授权成功";
                }else
                {
                    return "授权失败";
                }
            }else
            {
                R_RoleInfo_PowerInfo entity = r_RoleInfo_PowerInfoService.Query(u => u.RoleID == roleID && u.PowerID == powerID).FirstOrDefault();
                if (r_RoleInfo_PowerInfoService.Delete(entity) > 0)
                {
                    return "关闭成功";
                }else
                {
                    return "关闭失败";
                }
            }
        }

        /// <summary>
        /// 修改角色信息功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Modify(RoleInfo model)
        {
            if (model!= null)
            {
                RoleInfo entity = roleInfoService.Query(u => u.RoleID == model.RoleID).FirstOrDefault();
                entity.RoleName = model.RoleName;
                entity.Description = model.Description;
                entity.DelFlag = model.DelFlag;
                if (roleInfoService.Modify(entity) > 0)
                {
                    return "<script>alert('修改成功');window.location.href='../RoleInfo/ModifyRoleInfo?RoleID="+ model.RoleID +"'</script>";
                }else
                {
                    return "<script>alert('修改失败');window.location.href='../RoleInfo/ModifyRoleInfo?RoleID=" + model.RoleID + "'</script>";
                }
            }else
            {
                return "<script>alert('数据不能为空');window.location.href='../RoleInfo/ModifyRoleInfo?RoleID=" + model.RoleID + "'</script>";
            }
        }

        /// <summary>
        /// 查询角色信息页
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["limit"]);

            IQueryable<RoleInfo> entity = roleInfoService.PageLoadEntity(pageIndex, pageSize, u => u.ID > 0, u => u.ID, true);
            int count = roleInfoService.Query(u => u.ID > 0).Count();

            IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverter();
            datetimeConverter.DateTimeFormat = "yyyy'-'MM'-'dd hh':'mm':'ss";

            string strJson = JsonConvert.SerializeObject(entity, Formatting.Indented, datetimeConverter);
            string layuiStr = "{\"code\":0,\"msg\":\"\",\"count\":" + count + ",\"data\":" + strJson + "}";
            return layuiStr;
        }

        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <returns></returns>
        public string Add(RoleInfo model)
        {
            if (model != null)
            {
                RoleInfo entity = new RoleInfo
                {
                    RoleID = model.RoleID,
                    RoleName = model.RoleName,
                    Description = model.Description,
                    AddTime = DateTime.Now,
                    DelFlag = model.DelFlag
                };
                if (roleInfoService.AddTo(entity)>0)
                {
                    return "<script>alert('添加成功');window.location.href='../RoleInfo/AddRoleInfo'</script>";
                }
                else
                {
                    return "<script>alert('添加失败');window.location.href='../RoleInfo/AddRoleInfo'</script>";
                }
            }else
            {
                return "<script>alert('数据不能为空');window.loaction.href='../RoleInfo/AddRoleInfo'</script>";
            }
        }

        /// <summary>
        /// 锁定按钮监听修改角色状态方法
        /// </summary>
        /// <returns></returns>
        public string ModifyDelFlag()
        {
            int roleID = Convert.ToInt32(Request["ID"]);
            RoleInfo entity = roleInfoService.Query(u => u.RoleID == roleID).FirstOrDefault();
            entity.DelFlag = Request["DelFlag"] == "true" ? 0 : 1; 

            if (roleInfoService.Modify(entity) > 0)
            {
                return "设置成功!!!";
            }
            else
            {
                return "设置失败!!!";
            }
            
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <returns></returns>
        public string Delete()
        {
            int roleID = Convert.ToInt32(Request["RoleID"]);
            if (RoleInfo_R_RoleInfo_PowerInfo_UOW.RoleInfo_R_RoleInfo_PowerInfo_Delete(roleID)>0)
            {
                return "删除成功";
            }else
            {
                return "删除失败";
            }
        }
    }
}