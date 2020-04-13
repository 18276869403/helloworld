using InterfaceService.IUnitOfWord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModel;
using System.Data.Entity;
using MedicineService.Services;

namespace MedicineService.UnitOfWord
{
    public class EnterInfo_Inventory_UOW : BaseServices_UOW<EnterInfo, Inventory>,IEnterInfo_Inventory_UOW
    {
        public int AddToModify(EnterInfo EnterInfo)
        {
            db.Set<EnterInfo>().Add(EnterInfo);
            Inventory Inventory = InventoryService.Query(u => u.MedicineID == EnterInfo.MedicineID).FirstOrDefault();
            if(EnterInfo.MarketNumber > 0)
            {
                Inventory.Number += EnterInfo.MarketNumber;
                db.Entry(Inventory).State = EntityState.Modified;

                return db.SaveChanges();
            }else
            {
                return 0;
            }
        }

        public int ModifyToInventory(EnterInfo EnterInfo)
        {
            EnterInfo entity = EnterInfoService.Query(u => u.ID > EnterInfo.ID).FirstOrDefault();
            Inventory Inventory = InventoryService.Query(u => u.MedicineID == EnterInfo.MedicineID).FirstOrDefault();
            if (EnterInfo.MarketNumber > Inventory.Number)
            {
                return 0;
            }
            Inventory.Number -= entity.MarketNumber;
            Inventory.Number += EnterInfo.MarketNumber;
            db.Entry(Inventory).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int BacthAddToModify(List<EnterInfo> model)
        {
            foreach (var item in model)
            {
                if (item.MarketNumber > 0)
                {
                    EnterInfo entity = new EnterInfo
                    {
                        MedicineID = item.MedicineID,
                        MarketNumber = item.MarketNumber,
                        EnterCompanyID = item.EnterCompanyID,
                        EnterPrice = item.EnterPrice,
                        EnterDate = item.EnterDate,
                        EndDate = item.EnterDate.AddYears(1)
                    };
                    db.Set<EnterInfo>().Add(entity);//给entity打上添加标记，等到调用saveChanges()时，才去操作数据库
                    Inventory Inventory = InventoryService.Query(u => u.MedicineID == entity.MedicineID).FirstOrDefault();
                    Inventory.Number += entity.MarketNumber;
                    db.Entry(Inventory).State = EntityState.Modified;
                }
                else
                {
                    return 0;
                }
            }
            return db.SaveChanges();
        }
    }
}
