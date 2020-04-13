using Comman.RegexHelpers;
using EFModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comman.ExcelHelperList
{
    public class EnterInfoHelper
    {
        public static List<EnterInfo> GetList(DataTable excelTable)
        {
            List<EnterInfo> list = new List<EnterInfo>();
            foreach (DataRow row in excelTable.Rows)
            {
                EnterInfo model = new EnterInfo();
                var key = row.ItemArray;
                for (int i = 0; i < key.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            model.MedicineID = row[i].ToString();
                            continue;
                        case 1:
                            if (!RegexHelper.IsNumber(row[i].ToString()))
                            {
                                //ErrorHelper.ErrorInfo();
                                break;
                            }
                            model.MarketNumber = Convert.ToInt32(row[i]);
                            continue;
                        case 2:
                            model.EnterCompanyID = Convert.ToInt32(row[i]);
                            continue;
                        case 3:
                            model.EnterDate = Convert.ToDateTime(row[i]);
                            continue;
                        case 4:
                            model.EnterPrice = Convert.ToInt32(row[i]);
                            continue;
                    }
                }
                list.Add(model);
            }
            return list;
        }
    }
}
