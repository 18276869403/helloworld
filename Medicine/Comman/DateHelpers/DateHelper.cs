using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comman.DateHelpers
{
   public class DateHelper
    {
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;

            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days.ToString();

            return dateDiff;
        }
    }
}
