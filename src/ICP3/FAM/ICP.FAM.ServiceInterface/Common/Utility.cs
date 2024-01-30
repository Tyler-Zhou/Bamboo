using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FAM.ServiceInterface.Common
{
    public class Utility
    {
        #region 将数字类型转化为中文大写(打印用)
        /// <summary>
        /// 将数字类型转化为中文大写
        /// </summary>
        /// <param name="source">需要转换的数字内容</param>
        /// <returns>转换后的中文内容</returns>
        public static string MoneyToString(decimal source)
        {
            string returnVal = "";
            string CNChar = "";
            string bit = "万仟佰拾亿仟佰拾万仟佰拾元角分";
            string num = "壹贰叁肆伍陆柒捌玖";
            int moneyMax = bit.Length + 1;

            string moneyString = source.ToString("###########.00");
            int length = moneyString.Length - 1;

            moneyString = moneyString.Replace(".", "");//去除小数点


            for (int i = moneyString.Length; i > 0; i--)
            {
                moneyMax = moneyMax - 1;
                CNChar = bit.Substring(moneyMax - 1, 1);
                string strNumber = moneyString.Substring(i - 1, 1);
                if (strNumber == "-")
                {
                    continue;
                }
                int number = Convert.ToInt16(moneyString.Substring(i - 1, 1));
                if (number == 0)
                {
                    switch (CNChar)
                    {
                        case "元":
                            returnVal = CNChar + returnVal;
                            break;
                        case "万":
                            returnVal = CNChar + returnVal;
                            break;
                        case "亿":
                            returnVal = CNChar + returnVal;
                            break;
                        case "分":
                            returnVal = "整";
                            break;
                        case "角":
                            if (returnVal != "整") returnVal = "零" + returnVal;
                            break;
                        default:
                            if ((returnVal.Substring(0, 1) != "万") && (returnVal.Substring(0, 1) != "亿") && (returnVal.Substring(0, 1) != "元") && (returnVal.Substring(0, 1) != "零")) returnVal = "零" + returnVal;
                            break;
                    }
                }
                else
                {
                    returnVal = num.Substring(number - 1, 1) + CNChar + returnVal;
                }
            }

            if (source < 0)
            {
                returnVal = "负" + returnVal;
            }

            return returnVal;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numValue"></param>
        /// <returns></returns>
        public static decimal GetDecimal(decimal? numValue)
        {
            if (numValue == null)
            {
                return 0;
            }
            else
            {
                return numValue.Value;
            }
        }
    }
}
