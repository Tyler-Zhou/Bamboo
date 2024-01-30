using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml;

namespace ICP.Business.Common.UI
{
    /// <summary>
    /// WEB请求
    /// </summary>
    public class BaseTracking
    {
        protected string _CNo = "";
        /// <summary>
        /// 箱号
        /// </summary>
        public string CNo
        {
            get { return _CNo; }
            set { _CNo = value.Trim(); }
        }
        protected string _Url = "";
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        protected string _XmlFile = "";
        public string XmlFile
        {
            get { return _XmlFile; }
            set { _XmlFile = value; }
        }
        protected string _CarrierCode;
        /// <summary>
        /// 船东代码
        /// </summary>
        public string CarrierCode
        {
            get { return _CarrierCode; }
            set { _CarrierCode = value; }
        }
        protected string _TrackingResult = "";
        /// <summary>
        /// 箱的状态XML表示
        /// </summary>
        public string TrackingResult
        {
            get { return _TrackingResult; }
            set { _TrackingResult = value; }
        }
        protected string _InfoType = "";
        public string InfoType
        {
            get { return _InfoType; }
            set { _InfoType = value; }
        }
        protected string _OrderType = "";
        public string OrderType
        {
            get { return _OrderType; }
            set { _OrderType = value; }
        }
        protected string _LastState = "";
        /// <summary>
        /// 箱最新的状态XML表示
        /// </summary>
        public string LastState
        {
            get { return _LastState; }
            set { _LastState = value; }
        }

        protected string _SleepTime = "";
        public string SleepTime
        {
            get { return _SleepTime; }
            set { _SleepTime = value; }
        }

        protected string _LimitedSleepTime = "";
        public string LimitedSleepTime
        {
            get { return _LimitedSleepTime; }
            set { _LimitedSleepTime = value; }
        }

        public string MBLNO
        {
            get;
            set;
        }
        public string ETA
        {
            get;
            set;
        }

        protected string _DateType = "";
        /// <summary>
        /// 日期格式化字符串，用于日期字符串解析生成日期
        /// </summary>
        public string DateType
        {
            get { return _DateType; }
            set { _DateType = value; }
        }
        protected string _DateLocation = "";
        /// <summary>
        /// 日期所在的地点，用于日期字符串解析生成日期
        /// </summary>
        public string DateLocation
        {
            get { return _DateLocation; }
            set { _DateLocation = value; }
        }
        protected const string CNoName = "$CNo";
        protected string _Html = "";
        protected string _CoreHtml = "";
        protected string HtmlHash = "";
        protected SendHead oSendHead = new SendHead();
        protected Http oHttp = new Http();
        protected XmlDocument xmldoc = new XmlDocument();
        protected CARGOTRACKING_TYPE CargoType;
        protected int PostSleepTime = 0;
        protected string PostData = "";
        protected string xmlRoot = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><CargoTracking><Carrier></Carrier><ContainerHistory></ContainerHistory><RailHistory></RailHistory></CargoTracking>";
        protected XmlNode _CarrierNode;


        public BaseTracking()
        {
        }

        public BaseTracking(string xmlfile, string carriercode, string cno)
        {
            Init(xmlfile, carriercode);
        }
        public virtual void Init()
        {
            if (string.IsNullOrEmpty(XmlFile) || string.IsNullOrEmpty(CarrierCode)) throw new Exception("缺少参数");
            Init(XmlFile, CarrierCode);
        }
        /// <summary>
        /// 加载xml配置
        /// </summary>
        /// <param name="xmlfile"></param>
        /// <param name="carriercode"></param>
        public virtual void Init(string xmlfile, string carriercode)
        {
            try
            {
                _CarrierCode = carriercode;
                xmldoc.Load(xmlfile);
                string xmlPath = "//LongWin/CargoTracking/Carriers/Carrier[Code='" + carriercode.ToUpper() + "']";
                _CarrierNode = xmldoc.DocumentElement.SelectSingleNode(xmlPath);
                if (_CarrierNode != null)
                {
                    Url = _CarrierNode.SelectSingleNode("CargoTrackingUrl").InnerText.Trim();
                    CargoType = (CARGOTRACKING_TYPE)Enum.Parse(typeof(CARGOTRACKING_TYPE), _CarrierNode.SelectSingleNode("CargoTrackingType").InnerText.Trim());

                    PostData = _CarrierNode.SelectSingleNode("CargoTrackingData") == null ? string.Empty : _CarrierNode.SelectSingleNode("CargoTrackingData").InnerText.Trim();

                    InfoType = _CarrierNode.SelectSingleNode("CargoTrackingInfo/Type") == null ? string.Empty : _CarrierNode.SelectSingleNode("CargoTrackingInfo/Type").InnerText.Trim();
                    OrderType = _CarrierNode.SelectSingleNode("CargoTrackingInfo/OrderType") == null ? "desc" : _CarrierNode.SelectSingleNode("CargoTrackingInfo/OrderType").InnerText.Trim();
                    _DateType = _CarrierNode.SelectSingleNode("CargoTrackingInfo/DateType") == null ? string.Empty : _CarrierNode.SelectSingleNode("CargoTrackingInfo/DateType").InnerText.Trim();
                    _DateLocation = _CarrierNode.SelectSingleNode("CargoTrackingInfo/DateLocation") == null ? "en-us" : _CarrierNode.SelectSingleNode("CargoTrackingInfo/DateLocation").InnerText.Trim();
                    _SleepTime = _CarrierNode.SelectSingleNode("SleepTime") == null ? string.Empty : _CarrierNode.SelectSingleNode("SleepTime").InnerText.Trim();
                    _LimitedSleepTime = _CarrierNode.SelectSingleNode("LimitedSleepTime") == null ? string.Empty : _CarrierNode.SelectSingleNode("LimitedSleepTime").InnerText.Trim();
                    //this.CNo = _CarrierNode.SelectSingleNode("CNoExample") == null ? string.Empty : _CarrierNode.SelectSingleNode("CNoExample").InnerText.Trim();
                }
                else
                {
                    throw new Exception("Can not find carrier info in config file:" + xmlfile);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.GetType().ToString() + " :" + ex.Message);
            }
        }
        /// <summary>
        /// 向船东查询网站发送Cargo Tracking请求，获取Web请求返回文本
        /// </summary>
        /// <returns></returns>
        public virtual string GetHtml()
        {
            return GetHtml(this.Url);
        }
        /// <summary>
        /// 转换请求地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public virtual string ReplaceURL(string url)
        {
            return url.Replace(CNoName, this.CNo).Replace("$MBLNo", this.MBLNO);
        }
        /// <summary>
        /// 向船东查询网站发送Cargo Tracking请求，获取Web请求返回文本
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public virtual string GetHtml(string url)
        {
            oSendHead.Cookies = null;
            url = this.ReplaceURL(url);
            Uri actionUrl = new Uri(url);
            oSendHead.Method = "GET";
            oSendHead.Referer = url;
            oSendHead.Host = "http://" + actionUrl.Host;//主机
            if (url.IndexOf("https://") >= 0) //网址
                oSendHead.Host = "https://" + actionUrl.Host;
            if (actionUrl.Port != 80) //主机+端口
                oSendHead.Host = oSendHead.Host + ":" + actionUrl.Port;
            oSendHead.Action = actionUrl.PathAndQuery;//
            oSendHead.AcceptLanguage = "en-us";
            oHttp.Send(ref oSendHead);
            oSendHead.Cookies = oHttp.Cookies;
            if (CargoType == CARGOTRACKING_TYPE.POST)
            {
                if (PostSleepTime != 0)
                    System.Threading.Thread.Sleep(PostSleepTime);//mol需要挂起线程，否则得不到数据
                oSendHead.Method = "post";
                oSendHead.PostData = ReplacePostData();
                oHttp.Send(ref oSendHead);
            }
            return oSendHead.Html;
        }
        public virtual string ReplacePostData()
        {
            return this.PostData.Replace(CNoName, this.CNo);
        }

        public virtual string CNoTracking(string cno, string carriercode)
        {
            _TrackingResult = "";
            _LastState = "";
            _Html = GetHtml();

            return _TrackingResult;
        }
        /// <summary>
        /// 获取呈现的文本
        /// </summary>
        /// <param name="innerText"></param>
        /// <returns></returns>
        public virtual string GetAppearText(string innerText)
        {
            string text = innerText;
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Contains("&nbsp;"))  //空格
                    text = text.Replace("&nbsp;", " ").Trim();
                if (text.Contains("&amp;"))   // &符
                    text = text.Replace("&amp;", "&").Trim();
                if (text.Contains("&lt;"))    //少于号
                    text = text.Replace("&lt;", "<").Trim();
                if (text.Contains("&gt;"))    //大于号
                    text = text.Replace("&gt;", ">").Trim();
                if (text.Contains("&qpos;"))  //单字符
                    text = text.Replace("&qpos;", "'").Trim();
                if (text.Contains("\t"))
                    text = text.Replace("\t", "").Trim();
                if (text.Contains("\n"))
                    text = text.Replace("\n", "").Trim();
                if (text.Contains("\r"))
                    text = text.Replace("\r", "").Trim();

            }
            return text;
        }
    }

    public class Helper
    {
        public Helper()
        {
        }
        /// <summary>
        /// 用英文单引号替换中文单引号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DealSpeciaStr(string str)
        {
            return str.Replace("'", "’").Trim();
        }
        public static void CopyData(SendHead source, ref SendHead target)
        {
            target.AcceptLanguage = source.AcceptLanguage;
            target.Action = source.Action;
            target.ContentType = source.ContentType;
            target.Cookies = source.Cookies;
            target.EncodeName = source.EncodeName;
            target.Host = source.Host;
            target.Method = source.Method;
            target.Port = source.Port;
            target.PostData = source.PostData;
            target.Referer = source.Referer;

        }
        public static string ConcateNote(string vesselName, string vesselNo)
        {
            return ConcateNote(vesselName, vesselNo, "/");
        }
        public static string ConcateNote(string vesselName, string vesselNo, string separator)
        {
            if (string.IsNullOrEmpty(vesselName) && string.IsNullOrEmpty(vesselNo))
            {
                return string.Empty;
            }
            else if (string.IsNullOrEmpty(vesselName))
            {
                return vesselNo;
            }
            else if (string.IsNullOrEmpty(vesselNo))
            {
                return vesselName;
            }
            else
            {
                return string.Format("{0}{1}{2}", vesselName, separator, vesselNo);
            }

        }
        /// <summary>
        /// 获取节点下匹配节点名称的内容
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string D(XmlNode node, string name)
        {
            string strResult = node.SelectSingleNode(name) == null ? string.Empty : node.SelectSingleNode(name).InnerText.Trim();
            return strResult;
        }
        /// <summary>
        /// 创建节点，并追加到指定父节点下
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodeName"></param>
        /// <param name="nodeValue"></param>
        /// <param name="parentNode"></param>
        public static void CreateXmlNode(XmlDocument xmlDoc, string nodeName, string nodeValue, ref XmlNode parentNode)
        {
            XmlNode tmpNode = xmlDoc.CreateNode(XmlNodeType.Element, nodeName, "");
            if (!string.IsNullOrEmpty(nodeValue)) tmpNode.InnerText = nodeValue;
            parentNode.AppendChild(tmpNode);
        }
        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodeName"></param>
        /// <param name="nodeValue"></param>
        /// <returns></returns>
        public static XmlNode CreateXmlNode(XmlDocument xmlDoc, string nodeName, string nodeValue)
        {
            XmlNode tmpNode = xmlDoc.CreateNode(XmlNodeType.Element, nodeName, "");
            if (!string.IsNullOrEmpty(nodeValue)) tmpNode.InnerText = nodeValue;
            return tmpNode;
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(str))).ToLower().Replace("-", "");
        }
        /// <summary>
        /// 获取目标字符串中在两个字符串参数中间的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static string Filter2(string str, string s1, string s2)
        {
            string str2 = "";
            int num = str.LastIndexOf(s1);//123abc345dd67
            int num2 = str.LastIndexOf(s2);
            if ((num > 0) && (num2 > 0))
            {
                str2 = str.Substring(num + s1.Length, (num2 - num) - s1.Length);
            }
            else
            {
                str2 = str;
            }
            return str2;
        }
        /// <summary>
        /// 去除字符串中的换行符和标记符号，用方括号替换半圆括号
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string TripHtml(string strHtml)
        {
            strHtml = strHtml.Replace("\r\n", "").Replace("\n", "").Replace("(", "[").Replace(")", "]");
            Regex regex = new Regex("<.+?>", RegexOptions.IgnoreCase);
            return regex.Replace(strHtml, "");
        }
        /// <summary>
        /// 将内容追加写入c:\Service.log"文件
        /// </summary>
        /// <param name="content"></param>
        public static void WriteFile(string content)
        {
            WriteFile(@"c:\Service.log", content, Encoding.Default);
        }

        /// <summary>
        /// 将字符串转换成格式为MM/dd/yyyy格式的时间字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Convert2EDate(string str)
        {

            DateTime _date;
            string strDate = "";
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    if (DateTime.TryParse(str, out _date) == true) strDate = _date.ToString("MM/dd/yyyy", new System.Globalization.CultureInfo("en-us", true));
                }
                catch
                {
                    strDate = str;
                }
            }
            else
            {
                strDate = str;
            }
            return strDate;
        }
        /// <summary>
        /// 采用默认编码，将内容追加写入指定路径的文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void WriteFile(string path, string content)
        {
            WriteFile(path, content, Encoding.Default);
        }
        /// <summary>
        /// 采用指定编码，将内容追加写入指定路径的文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <param name="enc"></param>
        public static void WriteFile(string path, string content, Encoding enc)
        {
            try
            {
                File.AppendAllText(path, content + "\r\n", enc);
            }
            catch (System.Exception ex)
            {
                try
                {
                    File.AppendAllText(path, content + "\r\n", enc);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// 指定控制台前景色为绿色，并向控制台写入带当前时间字符串的文本
        /// </summary>
        /// <param name="str"></param>
        public static void Cl(string str)
        {
            Cl(str, ConsoleColor.Green);
        }
        /// <summary>
        /// 设置控制台前景色，并向控制台写入带当前时间字符串的文本
        /// </summary>
        /// <param name="str"></param>
        /// <param name="color"></param>
        public static void Cl(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write("" + str + "    " + DateTime.Now.ToString() + " \r\n");
        }
        /// <summary>
        /// 返回孔子付出的MD5编码字符串
        /// </summary>
        /// <returns></returns>
        public static string EmptyHash()
        {
            return GetMD5("");
        }
        /// <summary>
        /// key表示未在配置文件中配置的Code，value表示与key中Code相同的船公司
        /// </summary>
        private static Dictionary<string, string> dicCode = new Dictionary<string, string>();
        /// <summary>
        /// 通过反射获取Cargo Tracking的具体处理程序实例
        /// 类名规则为:类名=参数trackingModule开头
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static BaseTracking GetTracking(string trackingModule)
        {
            trackingModule = trackingModule.ToUpper();
            //if (dicCode.ContainsKey(trackingModule))
            //{
            //    trackingModule = dicCode[trackingModule];
            //}
            BaseTracking tracker = null;
            Type trackingType = trackingTypes.Find(type => type.Name.StartsWith(trackingModule, StringComparison.CurrentCultureIgnoreCase));
            if (trackingType == null)
            {
                tracker = new BaseTracking();
                return tracker;
            }
            tracker = Activator.CreateInstance(trackingType) as BaseTracking;
            return tracker;
        }
        public static string GetDate(string originalDate, string dateType, string dateLocation)
        {
            string strDate = originalDate.Replace("Local", "").Trim();

            try
            {
                if (!string.IsNullOrEmpty(strDate))
                {
                    if (dateType == "")
                    {
                        strDate = DateTime.Parse(strDate, new System.Globalization.CultureInfo(dateLocation, true)).ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        strDate = DateTime.ParseExact(strDate, dateType, new System.Globalization.CultureInfo(dateLocation, true)).ToString("yyyy-MM-dd HH:mm");

                    }
                }
                return strDate;
            }
            catch (Exception ex)
            {
                return strDate;
            }
        }

        public static string SplitTextToString(string htmlText, string keyWord, string firstCharText, string secondCharText)
        {
            string arrText = string.Empty;
            string[] splitText = Regex.Split(htmlText, keyWord);
            if (splitText != null && splitText.Length > 1)
            {
                splitText = Regex.Split(splitText[1].Trim(), firstCharText);
                if (splitText != null && splitText.Length > 0)
                {
                    arrText = splitText[0].Replace(secondCharText, "").Trim();
                }
            }

            return arrText;
        }

        static Helper()
        {
            dicCode.Add("SENATOR", "HANJIN");
            dicCode.Add("CNC", "CMA");
            dicCode.Add("ANL", "CMA");
            dicCode.Add("LLT", "EVERGREEN");
            dicCode.Add("UNIGLORY", "EVERGREEN");
            dicCode.Add("P&O", "OOCL");
            trackingTypes = Assembly.GetExecutingAssembly().GetExportedTypes().Where(type => type.BaseType == typeof(BaseTracking)).ToList();
        }
        static List<Type> trackingTypes;

    }
    /// <summary>
    /// 自定义的Web请求头部信息类
    /// </summary>
    public struct SendHead
    {
        public string Host { get; set; }
        public string Referer { get; set; }
        public CookieCollection Cookies { get; set; }
        public string Action { get; set; }
        public string PostData { get; set; }
        public string Method { get; set; }
        public string ContentType { get; set; }
        public string Html { get; set; }

        public string AcceptLanguage
        {
            get;
            set;
        }
        public string EncodeName { get; set; }
        public string Port { get; set; }
        public bool IsSecure { get; set; }
    }
    /// <summary>
    /// 发送Web请求辅助类
    /// </summary>
    public class Http
    {
        public CookieCollection Cookies = null;
        /// <summary>
        /// 发送web请求，并返回请求结果文本
        /// </summary>
        /// <param name="oHead"></param>
        /// <returns></returns>
        public string Send(ref SendHead oHead)
        {
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            for (int i = 0; i < 3; i++)
            {
                request = (HttpWebRequest)WebRequest.Create(oHead.Host + oHead.Action);
                request.ProtocolVersion = new Version("1.1");
                request.Referer = oHead.Referer;



                if (!string.IsNullOrEmpty(oHead.AcceptLanguage))
                {
                    request.Headers.Add("Accept-Language", oHead.AcceptLanguage);
                }
                request.Headers.Add("UA-CPU", "x86");
                request.Accept = " image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/xaml+xml, application/vnd.ms-xpsdocument, application/x-ms-xbap, application/x-ms-application, */*";
                request.CookieContainer = new CookieContainer();
                request.Timeout = 0xafc8;
                if (oHead.Cookies != null)
                {
                    string cookieHeader = "";
                    foreach (Cookie cookie in oHead.Cookies)
                    {
                        string str5 = cookieHeader;
                        cookieHeader = str5 + cookie.Name + "=" + cookie.Value + ",";
                    }
                    request.CookieContainer.SetCookies(new Uri(oHead.Host), cookieHeader);
                }
                //response.ContentEncoding



                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; CIBA; .NET CLR 2.0.50727; .NET CLR 3.0.04506.590; .NET CLR 3.5.20706)";

                request.Credentials = CredentialCache.DefaultCredentials;
                if (oHead.IsSecure)
                {

                }
                request.IfModifiedSince = DateTime.MinValue;
                if (oHead.Method.ToLower() == "post")
                {
                    byte[] bytes;
                    request.Method = "POST";
                    if ((oHead.EncodeName == null) || (oHead.EncodeName.Length == 0))
                    {
                        bytes = Encoding.ASCII.GetBytes(oHead.PostData);
                    }
                    else
                    {
                        bytes = Encoding.GetEncoding(oHead.EncodeName).GetBytes(oHead.PostData);
                    }
                    if (string.IsNullOrEmpty(oHead.ContentType))
                    {
                        request.ContentType = "application/x-www-form-urlencoded";
                    }
                    else
                    {
                        request.ContentType = oHead.ContentType;
                    }
                    request.ContentLength = bytes.Length;
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                    break;
                }
                catch (WebException exception)
                {
                    if (exception.Response != null)
                    {
                        response = (HttpWebResponse)exception.Response;
                        break;
                    }
                    if (i == 2)
                    {
                        throw exception;
                    }
                }
            }
            CookieCollection cookies = request.CookieContainer.GetCookies(request.RequestUri);
            foreach (Cookie cookie in cookies)
            {
                if (response.Cookies[cookie.Name] == null)
                {
                    response.Cookies.Add(cookie);
                }
            }
            Stream responseStream = response.GetResponseStream();
            string characterSet = response.CharacterSet;
            if ((characterSet == null) || (characterSet.Length == 0))
            {
                characterSet = "gb2312";
            }
            if (characterSet.ToLower() == "iso-8859-1" || characterSet.ToUpper() == "MS949")
            {
                characterSet = "gb2312";
            }
            if (!string.IsNullOrEmpty(oHead.EncodeName))
            {
                characterSet = oHead.EncodeName;
            }
            StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding(characterSet));
            string str3 = reader.ReadToEnd();
            this.Cookies = response.Cookies;
            oHead.Cookies = response.Cookies;
            reader.Close();
            responseStream.Close();
            response.Close();
            return (oHead.Html = str3);
        }
    }
    /// <summary>
    /// Cargo Tracking web内容获取方式(get,post等)
    /// </summary>
    public enum CARGOTRACKING_TYPE
    {
        GET,    //直接访问
        POST,   //需要程序解析访问
        PARSE,  //保留枚举，以供以后分析所用
        OTHERS = 9,//
    }
}
