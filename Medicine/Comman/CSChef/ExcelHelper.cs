using DataModel.DataModels;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common.CSChef
{
    /// <summary>
    /// 关于excel表格导入导出帮助类
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>读取Excel表格到DataTable中
        /// 默认第一行为表头，导入第一个工作表
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string strFileName)
        {
            DataTable dt = new DataTable();
            FileStream file = null;
            IWorkbook Workbook = null;

            try
            {
                using(file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
                {
                    //判断表格的后缀名
                    if (strFileName.IndexOf(".xlsx") > 0)
                        //把xlsx文件中的数据写入Workbook中
                        Workbook = new XSSFWorkbook(file);
                    else if (strFileName.IndexOf(".xls") > 0)
                        //把xls文件中的数据写入Workbook中
                        Workbook = new HSSFWorkbook();

                    if(Workbook != null)
                    {
                        //读取第一个sheet表单
                        ISheet Sheet = Workbook.GetSheetAt(0);
                        IEnumerator Rows = Sheet.GetRowEnumerator();
                        //得到Excel工作表的行
                        IRow HeaderRow = Sheet.GetRow(0);
                        int RowCount = Sheet.LastRowNum;
                        //得到Excel工作表的总列数
                        int CellCount = HeaderRow.LastCellNum;

                        //填充标题行
                        for (int j = 0; j < CellCount; j++)
                        {
                            //得到Excel工作表指定行的单元格
                            ICell Cell = HeaderRow.GetCell(j);
                            //填充到DataTable的行中
                            dt.Columns.Add(Cell.ToString());
                        }

                        //填充内容行
                        for (int i = (Sheet.FirstRowNum+1); i < RowCount+1; i++)
                        {
                            IRow Row = Sheet.GetRow(i);
                            DataRow DataRow = dt.NewRow();

                            for (int j = Row.FirstCellNum; j < CellCount; j++)
                            {
                                if (Row.GetCell(j) != null)
                                    DataRow[j] = Row.GetCell(j).ToString();
                            }
                            dt.Rows.Add(DataRow);
                        }
                    }
                    return dt;
                }
            }
            catch (Exception)
            {
                if (file != null)
                    file.Close(); //关闭当前流并释放资源
                return null;
            }
        }

        /// <summary>
        /// 从Excel中获取数据到DataTable
        /// </summary>
        /// <param name="strFileName">Excel文件全路径（服务器路径）</param>
        /// <param name="SheetName">要获取数据的工作表名称</param>
        /// <param name="HeaderRowIndex">工作表标题行所在行号（从0开始）</param>
        /// <returns></returns>
        public static DataTable RenderDataTableFromExcel(string strFileName,string SheetName,int HeaderRowIndex)
        {
            IWorkbook Workbook = null;
            using(FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                if (strFileName.IndexOf(".xlsx") > 0)
                    Workbook = new XSSFWorkbook(file);
                else if (strFileName.IndexOf(".xls") > 0)
                    Workbook = new HSSFWorkbook(file);

                ISheet Sheet = Workbook.GetSheet(SheetName);
                return RenderDataTableFromExcel(Workbook, SheetName, HeaderRowIndex);
            }
        }
        
        /// <summary>
        /// 从Excel中获取数据到DataTable
        /// </summary>
        /// <param name="Workbook">要处理的工作簿</param>
        /// <param name="SheetName">要获取数据的工作表名称</param>
        /// <param name="HeaderRowIndex">工作表标题所在行号（从0开始）</param>
        /// <returns></returns>
        public static DataTable RenderDataTableFromExcel(IWorkbook Workbook,string SheetName,int HeaderRowIndex)
        {
            ISheet Sheet = Workbook.GetSheet(SheetName); 
            DataTable table = new DataTable();
            try
            {
                IRow HeaderRow = Sheet.GetRow(HeaderRowIndex);
                int CellCount = HeaderRow.LastCellNum;

                for (int i = HeaderRow.FirstCellNum; i < CellCount; i++)
                {
                    DataColumn Column = new DataColumn(HeaderRow.GetCell(i).StringCellValue);
                    table.Columns.Add(Column);
                }

                int RowCount = Sheet.LastRowNum;

                #region 循环各行各列，写入数据到DataTable中
                //for (int i = (Sheet.FirstRowNum + 1); i < RowCount + 1; i++)
                for (int i = (Sheet.FirstRowNum + 1); i < RowCount + 1; i++)
                {
                    IRow Row = Sheet.GetRow(i);
                    DataRow DataRow = table.NewRow();
                    for (int j = Row.FirstCellNum; j < CellCount; j++)
                    {
                        ICell Cell = Row.GetCell(j);
                        if(Cell == null)
                        {
                            DataRow[j] = null;
                        }else
                        {
                            switch (Cell.CellType)
                            {
                                case CellType.Blank:
                                    DataRow[j] = null;
                                    break;
                                case CellType.Boolean:
                                    DataRow[j] = Cell.BooleanCellValue;
                                    break;
                                case CellType.Numeric:
                                    DataRow[j] = Cell.ToString();
                                    break;
                                case CellType.String:
                                    DataRow[j] = Cell.StringCellValue;
                                    break;
                                case CellType.Error:
                                    DataRow[j] = Cell.ErrorCellValue;
                                    break;
                                default:
                                    DataRow[j] = "=" + Cell.CellFormula;
                                    break;
                            }
                        }     
                    }
                    table.Rows.Add(DataRow);
                }
                #endregion

            }
            catch (Exception ex)
            {
                table.Clear();
                table.Columns.Clear();
                table.Columns.Add("出错了");
                DataRow dr = table.NewRow();
                dr[0] = ex.Message;
                table.Rows.Add(dr);
                return table;
            }
            finally
            {
                Workbook = null;
                Sheet = null;
            }

            #region 清除最后的空行
            for (int i = table.Rows.Count-1; i >0; i--)
            {
                bool isnull = true;
                for(int j = 0; j < table.Columns.Count; j++)
                {
                    if(table.Rows[i][j].ToString()!= null)
                    {
                        if (table.Rows[i][j].ToString() != "")
                        {
                            isnull = false;
                            break;
                        }
                    }
                }
                if (isnull)
                {
                    table.Rows[i].Delete();
                }
            }
            #endregion
            return table;
        }

        public static IWorkbook CreateExcel(List<MarketInfoModels> List)
        {
            //创建excel工作簿
            IWorkbook Workbook = new HSSFWorkbook();

            //创建表
            ISheet Sheet = Workbook.CreateSheet("测试表");

            //创建一行
            IRow Row0 = Sheet.CreateRow(0);

            //给第一单元格添加内容
            Row0.CreateCell(0).SetCellValue("序号");
            Row0.CreateCell(1).SetCellValue("药品编号");
            Row0.CreateCell(2).SetCellValue("用户编号");
            Row0.CreateCell(3).SetCellValue("退货数量");
            Row0.CreateCell(4).SetCellValue("价格");

            //将数据逐步写入sheet1各个行
            for (int i = 0; i < List.Count; i++)
            {
                IRow Row = Sheet.CreateRow(i + 1);
                Row.CreateCell(0).SetCellValue(List[i].ID.ToString());
                Row.CreateCell(1).SetCellValue(List[i].MedicineID.ToString());
                Row.CreateCell(2).SetCellValue(List[i].UserID.ToString());
                Row.CreateCell(3).SetCellValue(List[i].MarketNumber.ToString());
                Row.CreateCell(4).SetCellValue(List[i].Price.ToString());
                //....N行
            }
            return Workbook;
        }
    }
}
