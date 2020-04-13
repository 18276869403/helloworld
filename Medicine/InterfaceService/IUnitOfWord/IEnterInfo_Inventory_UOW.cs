using EFModel;
using InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceService.IUnitOfWord
{
    public interface IEnterInfo_Inventory_UOW:IBaseServices_UOW<EnterInfo,Inventory>
    {
        int AddToModify(EnterInfo EnterInfo);
        int ModifyToInventory(EnterInfo EnterInfo);
        int BacthAddToModify(List<EnterInfo> model);
    }
}
