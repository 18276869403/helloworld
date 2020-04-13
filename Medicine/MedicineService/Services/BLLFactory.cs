using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineService.Services
{
    public static class BLLFactory
    {
        private static ClassifyService _ClassifyService;
        public static ClassifyService ClassifyService
        {
            get
            {
                if (_ClassifyService == null)
                    _ClassifyService = new ClassifyService();
                return _ClassifyService;
            }
        }
        private static DosageTypeService _DosageTypeService;
        public static DosageTypeService DosageTypeService
        {
            get
            {
                if (_DosageTypeService == null)
                    _DosageTypeService = new DosageTypeService();
                return _DosageTypeService;
            }
        }
        private static EnterInfoService _EnterInfoService;
        public static EnterInfoService EnterInfoService
        {
            get
            {
                if (_EnterInfoService == null)
                    _EnterInfoService = new EnterInfoService();
                return _EnterInfoService;
            }
        }
        private static InventoryService _InventoryService;
        public static InventoryService InventoryService
        {
            get
            {
                if (_InventoryService == null)
                    _InventoryService = new InventoryService();
                return _InventoryService;
            }
        }
        private static MarketInfoService _MarketInfoService;
        public static MarketInfoService MarketInfoService
        {
            get
            {
                if (_MarketInfoService == null)
                    _MarketInfoService = new MarketInfoService();
                return _MarketInfoService;
            }
        }
        private static MedicineInfoService _MedicineInfoService;
        public static MedicineInfoService MedicineInfoService
        {
            get
            {
                if (_MedicineInfoService == null)
                    _MedicineInfoService = new MedicineInfoService();
                return _MedicineInfoService;
            }
        }
        private static PowerInfoService _PowerInfoService;
        public static PowerInfoService PowerInfoService
        {
            get
            {
                if (_PowerInfoService == null)
                    _PowerInfoService = new PowerInfoService();
                return _PowerInfoService;
            }
        }

        private static R_RoleInfo_PowerInfoService _R_RoleInfo_PowerInfoService;
        public static R_RoleInfo_PowerInfoService R_RoleInfo_PowerInfoService
        {
            get
            {
                if (_R_RoleInfo_PowerInfoService == null)
                    _R_RoleInfo_PowerInfoService = new R_RoleInfo_PowerInfoService();
                return _R_RoleInfo_PowerInfoService;
            }
        }
        private static R_UserInfo_RoleInfoService _R_UserInfo_RoleInfoService;
        public static R_UserInfo_RoleInfoService R_UserInfo_RoleInfoService
        {
            get
            {
                if (_R_UserInfo_RoleInfoService == null)
                    _R_UserInfo_RoleInfoService = new R_UserInfo_RoleInfoService();
                return _R_UserInfo_RoleInfoService;
            }
        }
        private static RepositService _RepositService;
        public static RepositService RepositService
        {
            get
            {
                if (_RepositService == null)
                    _RepositService = new RepositService();
                return _RepositService;
            }
        }
        private static RoleInfoService _RoleInfoService;
        public static RoleInfoService RoleInfoService
        {
            get
            {
                if (_RoleInfoService == null)
                    _RoleInfoService = new RoleInfoService();
                return _RoleInfoService;
            }
        }
        private static UserInfoService _UserInfoService;
        public static UserInfoService UserInfoService
        {
            get
            {
                if (_UserInfoService == null)
                    _UserInfoService = new UserInfoService();
                return _UserInfoService;
            }
        }
    }
}
