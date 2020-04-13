using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comman.CSChef
{
    class ICellHelper
    {
        private static object checkVlaue(ICell cell)
        {
            object returnValue = "";
            if (cell == null)
            {
                returnValue = "";
            }
            else
            {
                //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)***
                switch (cell.CellType)
                {
                    case CellType.Blank:
                        returnValue = "";
                        break;
                    case CellType.Numeric:
                        short format = cell.CellStyle.DataFormat;
                        //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理***
                        if (format == 14 || format == 31 || format == 57 || format == 58 || format == 20)
                            returnValue = cell.DateCellValue.ToString(" HH:mm");
                        else
                            returnValue = cell.NumericCellValue;
                        break;
                    case CellType.String:
                        returnValue = cell.StringCellValue;
                        break;
                }
            }
            return returnValue;
        }
    }
}
