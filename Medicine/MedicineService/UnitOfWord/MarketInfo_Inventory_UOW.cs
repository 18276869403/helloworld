using EFModel;
using InterfaceService.IUnitOfWord;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.DataModels;

namespace MedicineService.UnitOfWord
{
    public class MarketInfo_Inventory_UOW:BaseServices_UOW<MarketInfo,Inventory>,IMarketInfo_Inventory_UOW
    {
        public int BacthAddToModify(MarketInfo model)
        {
            if (model != null)
            {
                Inventory Inventory = InventoryService.Query(u => u.MedicineID == model.MedicineID).FirstOrDefault();

                if (Inventory == null)
                {
                    return 0;
                }
                if(Inventory.Number >= model.MarketNumber)
                {
                    Inventory.Number -= (int)model.MarketNumber;
                    MarketInfo entity = new MarketInfo
                    {
                        MedicineID = model.MedicineID,
                        UserID = model.UserID,
                        MarketNumber = model.MarketNumber,
                        PriceID = model.PriceID,
                        SellTime = DateTime.Now
                    };
                    db.Set<MarketInfo>().Add(entity);
                    db.Entry(Inventory).State = EntityState.Modified;

                    return db.SaveChanges();
                }
                else
                {
                    return 0;
                }
            }else
            {
                return 0;
            }
        }

        public int BacthModify(MarketInfoModels model)
        {
            if(model!= null)
            {
                MarketInfo Market = MarketInfoService.Query(u => u.ID == model.ID).FirstOrDefault();
                Inventory Inventory = InventoryService.Query(u => u.MedicineID == model.MedicineID).FirstOrDefault();
                if (Market != null&&Inventory!= null)
                {
                    if(model.MarketNumber > Market.MarketNumber)
                    {
                        return 0;
                    }
                    Market.MarketNumber = model.MarketNumber;
                    db.Entry(Market).State = EntityState.Modified;

                    Inventory.Number += (int)model.MarketNumber;
                    db.Entry(Inventory).State = EntityState.Modified;

                    return db.SaveChanges();
                }else
                {
                    return 0;
                }
            }else
            {
                return 0;
            }
        }
    }
}
