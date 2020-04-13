using DataModel.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.EquelityComparer
{
    public class InventoryComparer : IEqualityComparer<InventoryModels>
    {
        public bool Equals(InventoryModels x, InventoryModels y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(InventoryModels obj)
        {
            return obj.GetHashCode();
        }
    }
}
