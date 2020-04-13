using DataModel.DataModels;
using EFModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comman.ExcelHelperList
{
    public class InventoryHelper
    {
        public static List<InventoryModels> GetList(DataTable excelTable)
        {
            List<InventoryModels> list = new List<InventoryModels>();
            foreach (DataRow row in excelTable.Rows)
            {
                InventoryModels model = new InventoryModels();
                var key = row.ItemArray;
                for (int i = 0; i < key.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            model.ChineseName = row[i].ToString();
                            continue;
                        case 1:
                            model.Number = Convert.ToInt32(row[i]);
                            continue;
                        case 2:
                            model.ForeignName = row[i].ToString();
                            continue;
                        case 3:
                            model.E_Name = row[i].ToString();
                            continue;
                        case 4:
                            model.M_Function = row[i].ToString();
                            continue;
                        case 5:
                            model.Number = Convert.ToInt32(row[i]);
                            continue;
                    }
                }
                list.Add(model);
            }
            return list;
        }
    }
}
