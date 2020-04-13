using EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.EquelityComparer
{
    public class PowerInfoComparer : IEqualityComparer<PowerInfo>
    {
        public bool Equals(PowerInfo x, PowerInfo y)
        {
            return x.PowerID == y.PowerID;
        }

        public int GetHashCode(PowerInfo obj)
        {
            return obj.GetHashCode();
        }
    }
}
