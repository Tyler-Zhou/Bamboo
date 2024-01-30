using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ICP.FCM.OceanExport.UI.Common
{
    public class EDIService
    {

        static EDIService _instance = null;
        public static EDIService Instance
        {
            get
            {
                if (_instance == null) _instance = new EDIService();

                return _instance;
            }
        }

        #region 外部服务

        public string[] ParseStringToXml(string valString, int rowLength, int maxRow)
        {
            string[] rows = SplitString(valString, rowLength, maxRow);
            return rows;
        }

        public string GetGoodsInfo(string val)
        {
            string[] deliveryTerms = new string[] { "CY-FREE OUT", "CY-DOOR","CY - DOOR", "FREE IN-FREE OUT", "AIRPORT-AIRPORT", 
                                                    "DOOR-CFS", "CY-LO", "CY-CY", "TACKLE-CY", "TACKLE-CFS", 
                                                    "RAMP-CY", "CFS-CFS", "CFS-DOOR", "DR-FREE OUT", "LINER IN-DR",
                                                    "LINER IN-CY","DOOR-DOOR","CY-TACKLE","CFS-CY","DR-LINER OUT",
                                                    "DOOR-CY","CY-CFS","CY-FO","FREE IN-DR","CY-LINER OUT","CY-RAMP","FREE IN-CY"};

            if (string.IsNullOrEmpty(val)) return string.Empty;
            foreach (string s in deliveryTerms)
            {
                if (val.IndexOf(s) >= 0)
                {
                    int i = val.IndexOf(s) + s.Length;
                    return val.Substring(i).TrimStart();
                }
            }

            return val;
        }

        #endregion

        #region 本地方法

        private bool IsEnterOrLine(string val)
        {
            if (string.IsNullOrEmpty(val)) return false;

            bool isTrue = false;
            char[] cs = val.ToCharArray();
            foreach (char c in cs)
            {
                int code = (int)c;
                if (code == 10 || code == 13)
                {
                    isTrue = true;

                    break;
                }
            }

            return isTrue;
        }

        private string[] SplitString(string val, int rowLength, int maxRow)
        {
            val = ToDBC(val);
            if (maxRow == 0) maxRow = 999;

            List<string> total = new List<string>();
            StringBuilder orgsb = new StringBuilder();
            string[] words = SplitStringToWord(val);
            foreach (string w in words)
            {
                if (IsEnterOrLine(w))
                {
                    // orgsb.Append(Environment.NewLine);
                    total.Add(orgsb.ToString());
                    orgsb = new StringBuilder();
                }

                if (orgsb.Length + w.Length > rowLength)
                {
                    if (total.Count < maxRow)
                    {
                        total.Add(orgsb.ToString());
                    }

                    orgsb = new StringBuilder();
                }
                if (IsEnterOrLine(w) == false)
                {
                    orgsb.Append(w);
                }

                if (w.Contains("\r\n\r\n"))
                {
                    orgsb.Append("");
                    total.Add(orgsb.ToString());
                }

            }

            if (total.Count < maxRow && orgsb.Length > 0 && orgsb.Equals(Environment.NewLine) == false)
            {
                total.Add(orgsb.ToString());
            }

            return total.ToArray();
        }

        public string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }


        public bool HasDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    return true;
                }
                if (c[i] > 65280 && c[i] < 65375)
                {
                    return true;
                }
            }

            return false;
        }

        private string[] SplitStringToWord(string val)
        {
            if (string.IsNullOrEmpty(val) == true) return new string[] { };
            String regex = "([\\w]+(\\'|\\-)*[\\w]+)|([\\w]+)|([-,，?？.:;·。；：~￥%…()|+{}$&*%#@=>/\\<!]+)|([\\s]*)|([\\r\\n]*)";
            Regex pattern = new Regex(regex, RegexOptions.Compiled);
            MatchCollection cs = pattern.Matches(val);
            List<string> vals = new List<string>();
            foreach (Match m in cs)
            {
                vals.Add(m.Value);
            }

            return vals.ToArray();
        }

        #endregion

    }
}
