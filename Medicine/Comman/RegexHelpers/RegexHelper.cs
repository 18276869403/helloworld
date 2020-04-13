using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comman.RegexHelpers
{
    public class RegexHelper
    {
        /// <summary>
        /// 验证数字
        /// </summary>
        /// <param name="str_number"></param>
        /// <returns></returns>
        public static bool IsNumber(string str_number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_number, @"^[0-9]*$");
        }

        /// <summary>
        /// 验证手机
        /// </summary>
        /// <param name="str_handset"></param>
        /// <returns></returns>
        public static bool IsHandset(string str_handset)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^[1]+[3,5,7,8]+\d{9}");
        }

        /// <summary>
        /// 验证邮编
        /// </summary>
        /// <param name="str_postalcode"></param>
        /// <returns></returns>
        public static bool IsPostalcode(string str_postalcode)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_postalcode, @"^\d{6}$");
        }
    }
}
