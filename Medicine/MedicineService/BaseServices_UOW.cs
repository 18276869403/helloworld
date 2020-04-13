using EFModel;
using InterfaceService;
using InterfaceService.IServices;
using MedicineService.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineService
{
    public class BaseServices_UOW<T,S>:IBaseServices_UOW<T,S>where T:class
    {
        /// <summary>
        /// 声明dbContext的线程数据槽
        /// </summary>
        public DbContext db = EFContextFactory.GetDbContext();

        public IEnterInfoService EnterInfoService { get; set; }

        public IInventoryService InventoryService { get; set;}
        public IUserInfoService UserInfoService { get; set; }

        public IR_UserInfo_RoleInfoService R_UserInfo_RoleInfoService { get; set; }

        public IRoleInfoService RoleInfoService { get; set; }

        public IR_RoleInfo_PowerInfoService R_RoleInfo_PowerInfoService { get; set; }
        public IMarketInfoService MarketInfoService { get; set; }
    }
}
