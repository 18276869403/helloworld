using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CSChef
{
    public static class CExcel
    {
        /// <summary>
        /// 生成Excel2003连接串
        /// </summary>
        /// <param name="xlsFileName"></param>
        /// <param name="hdr"></param>
        /// <returns></returns>
        public static string GetCnnstr2003(string xlsFileName,bool hdr = false)
        {
            StringBuilder sb = new StringBuilder(200);
            sb.AppendFormat(@"Provider=Microsoft.Jet.OLEDB.4.0;
            Data Source={0};
            Extended Properties='Excel 8.0;HDR={1};IMEX=2;';", xlsFileName, (hdr ? "Yes" : "No"));
            return sb.ToString();
        }

        public static string GetCnnstr2007(string xlsxFileName,bool hdr = false)
        {
            StringBuilder sb = new StringBuilder(200);
            sb.AppendFormat(@"Provider=Microsoft.ACE.OLEDB.12.0;
            Data Source={0};
            Extended Properties='Excel 12.0;HDR={1};IMEX=2;';", xlsxFileName, (hdr ? "Yes" : "No"));
            return sb.ToString();
        }
    }
}
