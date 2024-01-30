using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.ComponentModel;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;


namespace ICP.EDI.WebService
{
    /// <summary>
    /// Summary description for EDI
    /// </summary>
    [WebService(Namespace = "http://www.CityOcean.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class ICP3EDIService : System.Web.Services.WebService
    {
        #region 外部服务

        [WebMethod(Description = "解析字符串到XML格式")]
        public string[] ICP3ParseStringToXml(string valString, int rowLength, int maxRow, string style)
        {
            if (string.IsNullOrEmpty(style) == false && style == "ZH")
            {
                string[] rows = SplitStringForZH(valString, rowLength, maxRow);
                return rows;
            }
            else
            {
                string[] rows = SplitString(valString, rowLength, maxRow);
                return rows;
            }

        }

        [WebMethod(Description = "解析字符串到XML格式不去空行")]
        public string[] ICP3ParseStringToXmlNew(string valString, int rowLength, int maxRow, string style)
        {
            if (string.IsNullOrEmpty(style) == false && style == "ZH")
            {
                string[] rows = SplitStringForZH(valString, rowLength, maxRow, "N");
                return rows;
            }
            else
            {
                string[] rows = SplitString(valString, rowLength, maxRow);
                return rows;
            }

        }



        [WebMethod(Description = "截取字符串")]
        public string ICP3ParseStringToString(string valString, int length)
        {
            if (string.IsNullOrEmpty(valString))
            {
                return string.Empty;
            }
            else
            {
                if (valString.Length <= length)
                {
                    return valString;
                }
                else
                {
                    return valString.Substring(0, length);
                }

            }

        }


        /// <summary>
        /// 把传入的港口信息数据转化成对象XML
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [WebMethod(Description = "把传入的港口信息数据转化成对象,")]
        public PortInfo ICP3ParsePortString(string value)
        {
            PortInfo port = new PortInfo();
            if (string.IsNullOrEmpty(value))
            {
                value = @"&lt;PortInfo&gt;
                        &lt;PortCode&gt;&lt;/PortCode&gt;
                        &lt;Qualifier&gt;&lt;/Qualifier&gt;
                        &lt;Date&gt;&lt;/Date&gt;
                        &lt;Time&gt;&lt;/Time&gt;
                        &lt;ActualDate&gt;&lt;/ActualDate&gt;
                        &lt;ActualTime&gt;&lt;/ActualTime&gt;
                        &lt;/PortInfo&gt;";
            }

            try
            {
                System.Xml.Serialization.XmlSerializer info = new System.Xml.Serialization.XmlSerializer(typeof(PortInfo));
                XmlDocument responseXmlDocument = new XmlDocument();
                responseXmlDocument.LoadXml((XMLEncode(value)));

                XmlNode xn = responseXmlDocument.DocumentElement.ParentNode;
                port = info.Deserialize(new XmlNodeReader(xn)) as PortInfo;
            }
            catch (Exception ex)
            {
                port.PortCode = ex.Message;
            }
            return port;
        }

        /// <summary>
        /// 把传入的客户信息数据转化成对象X
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [WebMethod(Description = "把传入的客户信息数据转化成对象,")]
        public CustomerDescription ICP3ParseCustomerToCustomerInfo(string value)
        {
            return ICP3GetCustomer(value);
        }

        [WebMethod(Description = "转换为ams所需要的格式")]
        public AMSCustomer ParseToAMSCustomer(string value)
        {
            return GetAMSCustomer(value);
        }

        /// <summary>
        /// 获得客户资料
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private CustomerDescription ICP3GetCustomer(string value)
        {
            CustomerDescription customer = new CustomerDescription();
            if (string.IsNullOrEmpty(value))
            {
                customer.Name = "Null";
            }


            try
            {
                System.Xml.Serialization.XmlSerializer info = new System.Xml.Serialization.XmlSerializer(typeof(CustomerDescription));
                XmlDocument responseXmlDocument = new XmlDocument();
                responseXmlDocument.LoadXml(value);

                XmlNode xn = responseXmlDocument.DocumentElement.ParentNode;
                customer = info.Deserialize(new XmlNodeReader(xn)) as CustomerDescription;

                if (!string.IsNullOrEmpty(customer.Tel) && customer.Tel.Length > 12)
                {
                    string tel = customer.Tel.Replace("86-", "");
                    customer.Tel = tel.Replace("-", "");
                }

                if (!string.IsNullOrEmpty(customer.Fax) && customer.Fax.Length > 12)
                {
                    string fax = customer.Fax.Replace("86-", "");
                    customer.Fax = fax.Replace("-", "");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(value);

                customer.Name = ex.Message;
            }
            return customer;

        }

        /// <summary>
        /// AMS专用客户资料
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private AMSCustomer GetAMSCustomer(string value)
        {
            AMSCustomer customer = new AMSCustomer();
            if (string.IsNullOrEmpty(value))
            {
                customer.Name = "Null";
            }


            try
            {
                //将xml转换为AMSCustomer
                System.Xml.Serialization.XmlSerializer info = new System.Xml.Serialization.XmlSerializer(typeof(AMSCustomer));
                XmlDocument responseXmlDocument = new XmlDocument();
                responseXmlDocument.LoadXml(value);

                XmlNode xn = responseXmlDocument.DocumentElement.ParentNode;
                customer = info.Deserialize(new XmlNodeReader(xn)) as AMSCustomer;

            }
            catch (Exception ex)
            {
                throw new Exception(value);
                customer.Name = ex.Message;
            }
            return customer;
        }

        /// <summary>
        /// 把传入的客户信息数据转化成字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [WebMethod(Description = "把传入的客户信息数据转化成字符串")]
        public string ICP3ParseCustomerToString(string value)
        {
            CustomerDescription customer = ICP3GetCustomer(value);

            return customer.ToString();
        }

        /// <summary>
        /// 把传入的客户信息数据转化成字符数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [WebMethod(Description = "把传入的客户信息数据转化成字符串数组")]
        public string[] ICP3ParseCustomerToList(string value, int rowLength, int maxRow, string style)
        {
            CustomerDescription customer = ICP3GetCustomer(value);

            return ICP3ParseStringToXml(customer.ToString(), rowLength, maxRow, style);
        }


        /// <summary>
        /// 转换中海客户字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [WebMethod(Description = "转换中海客户字符串")]
        public string[] ICP3ParseCustomerToCSCL(string value, int rowLength, int maxRow, string style)
        {
            CustomerDescription customer = ICP3GetCustomer(value);
            string[] rows = ICP3ParseStringToXml(customer.ToCSCL(), rowLength, maxRow, style);

            if (rows != null)
            {
                //第一行如果是空的，则加入一空格
                if (rows.Length >= 1 && string.IsNullOrEmpty(rows[0]))
                {
                    rows[0] = "C&#$";
                }
                //第二行如果是空的，则加入一空格
                if (rows.Length >= 2 && string.IsNullOrEmpty(rows[1]))
                {
                    rows[1] = "C&#$";
                }
                //第三行如果是空的，则加入一空格
                if (rows.Length >= 3 && string.IsNullOrEmpty(rows[2]))
                {
                    rows[2] = "C&#$";
                }
                //第四行如果是空的，则加入一空格
                if (rows.Length >= 4 && string.IsNullOrEmpty(rows[3]))
                {
                    rows[3] = "C&#$";
                }
                //第五行如果是空的，则加入一空格
                if (rows.Length >= 5 && string.IsNullOrEmpty(rows[4]))
                {
                    rows[4] = "C&#$";
                }
            }
            else
            {
                rows = new string[0];
            }
            return rows;
        }


        [WebMethod(Description = "切取货物有用的信息,")]
        public string ICP3GetGoodsInfo(string val)
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



        /// <summary>
        /// 中远转换客户名称(超过35个字符截取成两个字段)
        /// </summary>
        /// <param name="sText"></param>
        /// <returns></returns>
        [WebMethod(Description = "中远转换客户名称(超过35个字符截取成两个字段),")]
        public CustomerName SeparationCustomerNmae(string val)
        {
            CustomerName cus = new CustomerName();

            if (string.IsNullOrEmpty(val))
            {
                return cus;
            }

            string[] cust = SplitString(val, 35, 0);

            if (cust.Count() <= 1)
            {
                cus.Name1 = cust[0];
            }
            else
            {
                cus.Name1 = cust[0];
                cus.Nmae2 = cust[1];
            }

            return cus;
        }

        [WebMethod(Description = "中远转换客户地址(超过35个字符截取成两个字段),")]
        public CustomerAddress SeparationCustomerAddress(string val)
        {
            CustomerAddress cus = new CustomerAddress();

            if (string.IsNullOrEmpty(val))
            {
                return cus;
            }

            string[] cust = SplitString(val, 35, 0);

            if (cust.Count() == 1)
            {
                cus.Address1 = cust[0];
            }
            else if (cust.Count() == 2)
            {
                cus.Address1 = cust[0];
                cus.Address2 = cust[1];
            }
            else if (cust.Count() == 3)
            {
                cus.Address1 = cust[0];
                cus.Address2 = cust[1];
                cus.Address3 = cust[2];
            }
            else if (cust.Count() == 4)
            {
                cus.Address1 = cust[0];
                cus.Address2 = cust[1];
                cus.Address3 = cust[2];
                cus.Address4 = cust[3];
            }
            else 
            {
                cus.Address1 = "";
            }         
            return cus;
        }



        /// <summary>
        /// 将货描和唛头拼接成报文
        /// </summary>
        /// <param name="goods"></param>
        /// <param name="marks"></param>
        /// <returns></returns>
        [WebMethod(Description = "将货描和唛头拼接成报文-中远")]
        public string[] ICP3SpliceMarksAndGoods(string goodsstr, string marksstr)
        {
            List<string> re = new List<string>();

            string[] goods = SplitString(goodsstr, 35, 999);
            string[] marks = SplitString(marksstr, 35, 999);

            if (marks == null || marks.Length == 0 || marks[0] == "N/M")
            {
                foreach (string item in goods)
                {
                    re.Add(item + "******");
                }
                return re.ToArray();
            }

            if (goods.Length >= marks.Length)
            {
                for (int i = 0; i < goods.Length - 1; i++)
                {
                    if (marks.Length - 1 < i)
                    {
                        re.Add(goods[i] + "******");
                    }
                    else
                    {
                        re.Add(goods[i] + "****" + marks[i] + "L");
                    }
                }
            }
            else
            {
                for (int i = 0; i < marks.Length - 1; i++)
                {
                    if (goods.Length - 1 < i)
                    {
                        re.Add("*****" + marks[i] + "L");
                    }
                    else
                    {
                        re.Add(goods[i] + "****" + marks[i] + "L");
                    }
                }
            }

            return re.ToArray();
        }

        #endregion

        #region 本地方法

        private string XMLEncode(string sText)
        {
            sText = sText.Replace("&amp;", "&");
            sText = sText.Replace("&lt;", "<");
            sText = sText.Replace("&gt;", ">");
            sText = sText.Replace("&apos;", "&apos;");
            sText = sText.Replace(@"&quot;", "&quot;");
            return sText;
        }

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

        [WebMethod(Description = "字条串转换为数组")]
        public string[] SplitString(string val, int rowLength, int maxRow)
        {
            if (maxRow == 0) maxRow = 999;

            List<string> total = new List<string>();
            StringBuilder orgsb = new StringBuilder();
            string[] words = SplitStringToWord(val);
            foreach (string w in words)
            {
                if (IsEnterOrLine(w))
                {
                    // orgsb.Append(Environment.NewLine);
                    total.Add(orgsb.ToString().Trim());
                    orgsb = new StringBuilder();
                }

                if (orgsb.Length + w.Length > rowLength)
                {
                    if (total.Count < maxRow)
                    {
                        total.Add(orgsb.ToString().Trim());
                    }

                    orgsb = new StringBuilder();
                }
                if (IsEnterOrLine(w) == false)
                {
                    orgsb.Append(w);
                }

                if (w.Contains("\r\n\r\n"))
                {
                    orgsb.AppendLine();
                }

            }

            if (total.Count < maxRow && orgsb.Length > 0)
            {
                total.Add(orgsb.ToString().Trim());
            }

            return total.ToArray();
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

        /// <summary>
        /// 11-10-9LM新增(仅针对中海)
        /// </summary>
        private string[] SplitStringForZH(string val, int rowLength, int maxRow, string isRemoveBlankLines)
        {

            //&amp;amp;这个是DataSet转为XML对于&;的解析,在EDI处理中会忽略掉;符号.所以要替换一下
            //val = val.Replace("&amp;amp;", "&amp;;");

            if (maxRow == 0) maxRow = 999;

            List<string> total = new List<string>();
            StringBuilder orgsb = new StringBuilder();
            int orgsbLength = 0;
            string[] words = SplitStringToWordForZH(val);
            foreach (string w in words)
            {
                if (IsEnterOrLine(w))
                {
                    // orgsb.Append(Environment.NewLine);
                    total.Add(orgsb.ToString());
                    orgsb = new StringBuilder();
                    orgsbLength = 0;
                }

                int wordLength = w.Length;

                //如果是xml的特殊字符,计算为一个字符
                if (Regex.IsMatch(w, "&amp;amp;|&amp;|&quot;|&apos;|&lt;|&gt")) wordLength = 1;

                if (orgsbLength + wordLength > rowLength)
                {
                    if (total.Count < maxRow)
                    {
                        total.Add(orgsb.ToString());
                    }

                    orgsb = new StringBuilder();
                    orgsbLength = 0;
                }
                if (IsEnterOrLine(w) == false)
                {
                    orgsb.Append(w);
                    orgsbLength += wordLength;
                }

                if (w.Contains("\r\n\r\n"))
                {
                    orgsb.AppendLine();
                }

            }

            if (orgsb.Length > 0) total.Add(orgsb.ToString());

            List<string> results = new List<string>();
            #region 去除空行

            foreach (var item in total)
            {
                if (isRemoveBlankLines == "Y")
                {
                    if (string.IsNullOrEmpty(item.Trim())) continue;
                }
                results.Add(item.Trim());
                if (results.Count >= maxRow) break;
            }
            #endregion

            return results.ToArray();
        }

        /// <summary>
        /// 11-10-9LM新增(仅针对中海)
        /// </summary>
        private string[] SplitStringForZH(string val, int rowLength, int maxRow)
        {
            return SplitStringForZH(val, rowLength, maxRow, "N");
        }

        /// <summary>
        /// 11-10-9LM新增(仅针对中海)
        /// </summary>
        private string[] SplitStringToWordForZH(string val)
        {
            if (string.IsNullOrEmpty(val) == true) return new string[] { };
            //String regex = "([\\w]*(\\'|\\-)*[\\w]*)|([\\w]+)|([-,，?？.:;·。；：~￥%…()|+{}$&*%#@=>/\\<!]+)|([\\s]*)|([\\r\\n]*)";
            //String regex = "(&amp;|&quot;|&apos;|&lt;|&gt)|(\\'|\\-)|([\\w]+(\\'|\\-|&amp;|&quot;|&apos;|&lt;|&gt)*[\\w]+)|([\\w]+)|([,?.:;，。？；：(){} $&*%#@=+>/\\<!]+)|([\\-]*)|([\\s]*)";

            String regex = "(\\'|\\-|\r|\n|\r\n)|([\\w]+(\\'|\\-)*[\\w]+)|([\\w]+)|([\",?.:;，。？；：(){} $&*%#@=+>/\\<!]+)|([\\-]*)|([\\s]*)";

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

    [Serializable]
    public class AMSCustomer
    {
        public AMSCustomer()
        {
            this.Name = string.Empty;
            this.Address = string.Empty;
            this.Country = string.Empty;
            this.City = string.Empty;
            this.Tel = string.Empty;
            this.Fax = string.Empty;
            this.Remark = string.Empty;
            this.Contact = string.Empty;
            CountryCode = string.Empty;
            Address2 = string.Empty;
            Zip = string.Empty;
        }
        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement("Address")]
        public string Address { get; set; }

        /// <summary>
        /// 地址2
        /// </summary>
        [XmlElement("Address2")]
        public string Address2 { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [XmlElement("Country")]
        public string Country { get; set; }

        /// <summary>
        /// 国家代码
        /// </summary>
        [XmlElement("CountryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [XmlElement("Zip")]
        public string Zip { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [XmlElement("City")]
        public string City { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [XmlElement("Tel")]
        public string Tel { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [XmlElement("Fax")]
        public string Fax { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [XmlElement("Contact")]
        public string Contact { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("Remark")]
        public string Remark { get; set; }
    }


    [Serializable]
    public class PortInfo
    {
        string portCode;
        [System.Xml.Serialization.XmlElement("PortCode")]
        public string PortCode
        {
            get { return portCode; }
            set { portCode = value; }
        }
        string qualifier;
        [System.Xml.Serialization.XmlElement("Qualifier")]
        public string Qualifier
        {
            get { return qualifier; }
            set { qualifier = value; }
        }
        string date;
        [System.Xml.Serialization.XmlElement("Date")]
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        string time;
        [System.Xml.Serialization.XmlElement("Time")]
        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        string actualDate;
        [System.Xml.Serialization.XmlElement("ActualDate")]
        public string ActualDate
        {
            get { return actualDate; }
            set { actualDate = value; }
        }
        string actualTime;
        [System.Xml.Serialization.XmlElement("ActualTime")]
        public string ActualTime
        {
            get { return actualTime; }
            set { actualTime = value; }
        }
    }



    /// <summary>
    /// 客户描述信息
    /// </summary>
    [Serializable]
    public class CustomerDescription
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerDescription()
        {
            this.Name = string.Empty;
            this.Address = string.Empty;
            this.Country = string.Empty;
            this.City = string.Empty;
            this.Tel = string.Empty;
            this.Fax = string.Empty;
            this.Remark = string.Empty;
            this.Contact = string.Empty;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement("Address")]
        public string Address { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [XmlElement("Country")]
        public string Country { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [XmlElement("City")]
        public string City { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [XmlElement("Tel")]
        public string Tel { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [XmlElement("Fax")]
        public string Fax { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [XmlElement("Contact")]
        public string Contact { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("Remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 转换为字符串描述
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        /// <returns>返回字符串描述</returns>
        public string ToString(bool isEnglish)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(this.Name) && this.Name.Trim().Length > 0)
            {
                sb.AppendLine(this.Name);
            }
            if (!string.IsNullOrEmpty(this.Address) && this.Address.Trim().Length > 0)
            {
                sb.AppendLine(this.Address);
            }
            if (!string.IsNullOrEmpty(this.Tel) && this.Tel.Trim().Length > 0)
            {
                sb.AppendLine("TEL:" + this.Tel.Replace(System.Environment.NewLine, ""));
            }
            if (!string.IsNullOrEmpty(this.Fax) && this.Fax.Trim().Length > 0)
            {
                sb.AppendLine("FAX:" + this.Fax.Replace(System.Environment.NewLine, ""));
            }
            if (!string.IsNullOrEmpty(this.Remark) && this.Remark.Trim().Length > 0)
            {
                sb.Append(this.Remark);
            }


            return sb.ToString();
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>返回字符串描述</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(this.Name) && this.Name.Trim().Length > 0)
            {
                sb.AppendLine(this.Name);
            }


            #region 地点加入国家城市
            string adderssInfo = this.Address;

            if (!string.IsNullOrEmpty(this.City) && this.City.Trim().Length > 0)
            {
                adderssInfo = adderssInfo + "," + this.City;
            }
            if (!string.IsNullOrEmpty(this.Country) && this.Country.Trim().Length > 0)
            {
                adderssInfo = adderssInfo + "," + this.Country;
            }


            if (!string.IsNullOrEmpty(adderssInfo) && adderssInfo.Trim().Length > 0)
            {
                sb.AppendLine(adderssInfo);
            }
            #endregion


            string tel = string.Empty;
            if (!string.IsNullOrEmpty(this.Tel) && this.Tel.Trim().Length > 0)
            {
                tel = "TEL:" + this.Tel.Replace(System.Environment.NewLine, "");
            }
            if (!string.IsNullOrEmpty(this.Fax) && this.Fax.Trim().Length > 0)
            {
                if (string.IsNullOrEmpty(tel))
                {
                    tel = "FAX:" + this.Fax.Replace(System.Environment.NewLine, "");
                }
                else
                {
                    tel = tel + " FAX:" + this.Fax.Replace(System.Environment.NewLine, "");
                }
            }
            if (!string.IsNullOrEmpty(tel) && tel.Trim().Length > 0)
            {
                sb.AppendLine(tel);
            }

            if (!string.IsNullOrEmpty(this.Remark) && this.Remark.Trim().Length > 0)
            {
                sb.Append(this.Remark);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 转换为中海字符格式
        /// </summary>
        /// <returns>返回字符串描述</returns>
        public string ToCSCL()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(this.Name) && this.Name.Trim().Length > 0)
            {
                sb.AppendLine(this.Name);
            }


            #region 地点加入国家城市
            string adderssAndTelFax = this.Address;

            if (!string.IsNullOrEmpty(this.City) && this.City.Trim().Length > 0)
            {
                adderssAndTelFax = adderssAndTelFax + "," + this.City;
            }
            if (!string.IsNullOrEmpty(this.Country) && this.Country.Trim().Length > 0)
            {
                adderssAndTelFax = adderssAndTelFax + "," + this.Country;
            }

            #endregion


            string telFax = string.Empty;
            if (!string.IsNullOrEmpty(this.Tel) && this.Tel.Trim().Length > 0)
            {
                telFax = "TEL:" + this.Tel.Replace(System.Environment.NewLine, "");
            }
            if (!string.IsNullOrEmpty(this.Fax) && this.Fax.Trim().Length > 0)
            {
                if (string.IsNullOrEmpty(telFax))
                {
                    telFax = "FAX:" + this.Fax.Replace(System.Environment.NewLine, "");
                }
                else
                {
                    telFax = telFax + " FAX:" + this.Fax.Replace(System.Environment.NewLine, "");
                }
            }
            if (!string.IsNullOrEmpty(telFax) && telFax.Trim().Length > 0)
            {
                adderssAndTelFax = adderssAndTelFax + " " + telFax;
            }

            if (!string.IsNullOrEmpty(adderssAndTelFax) && adderssAndTelFax.Trim().Length > 0)
            {
                sb.AppendLine(adderssAndTelFax);
            }

            if (!string.IsNullOrEmpty(this.Remark) && this.Remark.Trim().Length > 0)
            {
                sb.Append(this.Remark);
            }

            return sb.ToString();
        }




        #region IConvertible 成员


        /// <summary>
        /// 分四行显示名称、地址、电话和传真
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public string ToString(IFormatProvider provider)
        {
            if (this == null)
            {
                return string.Empty;
            }
            else
            {
                return this.ToString();
            }
        }


        #endregion
    }



    /// <summary>
    /// 中远客户名称使用
    /// </summary>
    [Serializable]
    public class CustomerName
    {
        public CustomerName()
        {
            this.Name1 = string.Empty;
            this.Nmae2 = string.Empty;
        }
        public string Name1 { get; set; }

        public string Nmae2 { get; set; }
    }

    /// <summary>
    /// 中远客户地址使用
    /// </summary>
    public class CustomerAddress
    {
        public CustomerAddress()
        {
            this.Address1 = string.Empty;
            this.Address2 = string.Empty;
            this.Address3 = string.Empty;
            this.Address4 = string.Empty;
        }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
    }
}
