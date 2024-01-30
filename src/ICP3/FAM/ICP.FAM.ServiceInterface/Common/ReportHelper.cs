using System.Data;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 报表金额转换
    /// </summary>
    public class ReportHelper
    {
        public static string GetFAMReportPath()
        {
            return System.Windows.Forms.Application.StartupPath + "\\Reports\\FAM\\";
        }

        public static string GetReportLOGOPath()
        {
            return System.Windows.Forms.Application.StartupPath + "\\Reports\\LOGO\\";
        }

        #region Convert Decimal To Text

        private static DataTable _DictTable;
        /// <summary>
        /// 字典表
        /// </summary>
        private static DataTable DictTable
        {
            get
            {
                if (_DictTable == null)
                {
                    _DictTable = new DataTable("dictTable");
                    _DictTable.Columns.Add(new System.Data.DataColumn("Key", typeof(string)));
                    _DictTable.Columns.Add(new System.Data.DataColumn("Value", typeof(decimal)));
                    AddDict(_DictTable);
                }
                return _DictTable;
            }
        }

        private static void AddDict(System.Data.DataTable dictTable)
        {
            dictTable.Rows.Add(new object[] { "billion", 1000000000 });
            dictTable.Rows.Add(new object[] { "million", 1000000 });
            dictTable.Rows.Add(new object[] { "thousand", 1000 });
            dictTable.Rows.Add(new object[] { "hundred", 100 });
            dictTable.Rows.Add(new object[] { "ninety", 90 });
            dictTable.Rows.Add(new object[] { "eighty", 80 });
            dictTable.Rows.Add(new object[] { "seventy", 70 });
            dictTable.Rows.Add(new object[] { "sixty", 60 });
            dictTable.Rows.Add(new object[] { "fifty", 50 });
            dictTable.Rows.Add(new object[] { "forty", 40 });
            dictTable.Rows.Add(new object[] { "thirty", 30 });
            dictTable.Rows.Add(new object[] { "twenty", 20 });
            dictTable.Rows.Add(new object[] { "nineteen", 19 });
            dictTable.Rows.Add(new object[] { "eighteen", 18 });
            dictTable.Rows.Add(new object[] { "seventeen", 17 });
            dictTable.Rows.Add(new object[] { "sixteen", 16 });
            dictTable.Rows.Add(new object[] { "fifteen", 15 });
            dictTable.Rows.Add(new object[] { "fourteen", 14 });
            dictTable.Rows.Add(new object[] { "thirteen", 13 });
            dictTable.Rows.Add(new object[] { "twelve", 12 });
            dictTable.Rows.Add(new object[] { "eleven", 11 });
            dictTable.Rows.Add(new object[] { "ten", 10 });
            dictTable.Rows.Add(new object[] { "nine", 9 });
            dictTable.Rows.Add(new object[] { "eight", 8 });
            dictTable.Rows.Add(new object[] { "seven", 7 });
            dictTable.Rows.Add(new object[] { "six", 6 });
            dictTable.Rows.Add(new object[] { "five", 5 });
            dictTable.Rows.Add(new object[] { "four", 4 });
            dictTable.Rows.Add(new object[] { "three", 3 });
            dictTable.Rows.Add(new object[] { "two", 2 });
            dictTable.Rows.Add(new object[] { "one", 1 });
            dictTable.Rows.Add(new object[] { "zero", 0 });
        }

        /// <summary>
        /// 转换到文本
        /// </summary>
        /// <returns></returns>
        public static string GetText(decimal amount)
        {
            return Translate(amount);
        }

        /// <summary>
        /// 转换到文本
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private static string Translate(decimal amount)
        {
            //定义
            string oText = string.Empty;
            //先取出小数部分取(*100表示取两位小数)--因为在运算过程中会把小数给去掉
            decimal decimalPart = (amount - (int)amount) * 100;

            //十亿
            if ((int)(amount / 1000000000) > 0)
            {
                oText = string.Format("{0} billion", ConvertDecimalToText((int)(amount / 1000000000)));
                amount = amount % 1000000000;
            }
            //百万
            if ((int)(amount / 1000000) > 0)
            {
                if (oText == string.Empty)
                    oText = string.Format("{0} million", ConvertDecimalToText((int)(amount / 1000000)));
                else
                    oText += string.Format(" {0} million", ConvertDecimalToText((int)(amount / 1000000)));
                amount = amount % 1000000;
            }
            //千
            if ((int)(amount / 1000) > 0)
            {
                if (oText == string.Empty)
                    oText = string.Format("{0} thousand", ConvertDecimalToText((int)(amount / 1000)));
                else
                    oText += string.Format(" {0} thousand", ConvertDecimalToText((int)(amount / 1000)));
                amount = amount % 1000;
            }

            if (oText == string.Empty)
                oText = string.Format("{0}", ConvertDecimalToText((int)(amount)));
            else
            {
                if (amount > 100 || amount == 0)
                    oText += string.Format(" {0}", ConvertDecimalToText((int)(amount)));
                else
                    oText += string.Format(" and {0}", ConvertDecimalToText((int)(amount)));

            }

            //小数部分用xx/%表示
            if (oText == string.Empty)
                oText = string.Format(" zero ", (int)decimalPart);
            else
                oText += string.Format(" and {0}/100", (int)decimalPart);

            return oText.ToUpper();
        }

        /// <summary>
        /// 千以内的转换
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="dictTable"></param>
        /// <returns></returns>
        private static string ConvertDecimalToText(decimal amount)
        {
            string oText = string.Empty;
            if ((int)(amount / 1000) > 0)
            {
                oText = string.Format("{0} thousand", getKeyByValue((int)(amount / 1000)));
                amount = amount % 1000;
            }
            if ((int)(amount / 100) > 0)
            {
                if (oText == string.Empty)
                    oText = string.Format("{0} hundred", getKeyByValue((int)(amount / 100)));
                else
                    oText += string.Format(" {0} hundred", getKeyByValue((int)(amount / 100)));
                amount = amount % 100;
            }

            if ((amount < 20 && amount != 0) || (amount % 10 == 0 && amount != 0))
            {
                if (oText == string.Empty)
                    oText = getKeyByValue(amount);
                else
                    oText += string.Format(" and {0}", getKeyByValue(amount));

            }
            else if (amount > 0)
            {
                if (oText == string.Empty)
                    oText = string.Format("{0}", getKeyByValue((int)(amount / 10) * 10));
                else
                    oText += string.Format(" and {0}", getKeyByValue((int)(amount / 10) * 10));
                amount = amount % 10;

                if (amount != 0)
                {
                    if (oText == string.Empty)
                        oText = string.Format("{0}", getKeyByValue((int)(amount / 1)));
                    else
                        oText += string.Format("-{0}", getKeyByValue((int)(amount / 1)));
                    amount = amount;
                }
            }

            return oText;
        }

        private static string getKeyByValue(decimal amount)
        {
            string oKey = string.Empty;
            for (int i = 0; i < DictTable.Rows.Count; i++)
            {
                if ((decimal)DictTable.Rows[i]["Value"] == amount)
                {
                    oKey = DictTable.Rows[i]["Key"].ToString();
                    break;
                }
            }
            return oKey;
        #endregion
        }
    }
}
