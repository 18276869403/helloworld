using DataModel.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comman.FieldHelper
{
    public class MarketFieldHelper
    {
        public static string MedicineID { get; set; }

        public static int ID { get; set; }
        
        public static int UserID { get; set; }

        public static int PriceID { get; set; }

        public static List<MarketInfoModels> Market = new List<MarketInfoModels>();
    }
}
