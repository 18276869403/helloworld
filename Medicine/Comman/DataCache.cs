using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DataCache<T> where T:class
    {
        public static List<T> _powerCache
        {
            get
            {
                return _powerCache;
            }
            set
            {
                _powerCache = value;
            }
        }
        public static List<T> powerCache(List<T> list)
        {
            if (_powerCache != null)
            {

            }else
            {
                
            }
            return list;
        }
    }
}
