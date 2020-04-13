using DataModel.DataModels;
using EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceService.IUnitOfWord
{
    public interface IMarketInfo_Inventory_UOW:IBaseServices_UOW<MarketInfo, Inventory>
    {
        int BacthAddToModify(MarketInfo model);

        int BacthModify(MarketInfoModels model);
    }
}
