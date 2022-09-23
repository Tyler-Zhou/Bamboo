using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Reader.Client.Spider
{
    /// <summary>
    /// 爬虫基类
    /// </summary>
    public class BaseSpider
    {
        #region 成员(Member)
        /// <summary>
        /// 请求方法
        /// </summary>
        public string Method { get; set; } = "GET";
        /// <summary>
        /// 默认编码格式
        /// </summary>
        public string Charset { get; set; } = "utf-8";
        /// <summary>
        /// 接受类型
        /// </summary>
        public string Accept { get; set; } = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
        /// <summary>
        /// 接受编码
        /// </summary>
        public string AcceptEncoding { get; set; } = "gzip, deflate, br";
        /// <summary>
        /// 接受语言
        /// </summary>
        public string AcceptLanguage { get; set; } = "zh-CN,zh;q=0.9";
        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.25 Safari/537.36 Core/1.70.3641.400 QQBrowser/10.4.3284.400";
        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; } = "text/html; charset=utf-8";

        /// <summary>
        /// HTML 内容
        /// </summary>
        public string HtmlContent { get; set; }
        /// <summary>
        /// Post 参数
        /// </summary>
        public Dictionary<string, string> PostParams { get; set; }

        #endregion

        #region 方法(Methods)
        /// <summary>
        /// 构建Post字符串
        /// </summary>
        /// <returns></returns>
        public string BuildPostString()
        {
            string postData = string.Empty;
            if (PostParams != null)
            {
                foreach (string key in PostParams.Keys)
                {
                    postData += string.Format("{0}={1}&", key, PostParams[key]);
                }
                if (postData != string.Empty) postData = postData.Substring(0, postData.Length - 1);
            }
            return postData;
        }
        #endregion

        #region 虚拟方法(Virtual Methods)
        /// <summary>
        /// 获取HTML内容
        /// </summary>
        /// <returns></returns>
        public virtual void GetHtmlContent()
        {
            HtmlContent = string.Empty;
        }

        /// <summary>
        /// 获取HTML内容
        /// </summary>
        /// <returns></returns>
        public virtual void PostHtmlContent()
        {
            HtmlContent = string.Empty;
        }
        #endregion
    }

    /// <summary>
    /// Web Client 爬虫
    /// </summary>
    public class WebClientSpider : BaseSpider
    {
        #region 成员(Member)
        /// <summary>
        /// Web Client
        /// </summary>
        private readonly WebClient _WebClient = null;

        /// <summary>
        /// 请求URL
        /// </summary>
        public string Url { get; set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        public WebClientSpider()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public WebClientSpider(string url)
        {
            Url = url;
        }
        #endregion

        #region 方法(Methods)
        /// <summary>
        /// 初始化设置
        /// </summary>
        private void InitSetting()
        {
            GC.Collect();
            if (_WebClient == null)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
               | SecurityProtocolType.Tls11
               | SecurityProtocolType.Tls12
               | SecurityProtocolType.Ssl3;

                _WebClient.Encoding = Encoding.GetEncoding(Charset); // 设定网页编码
                                                                     //添加请求，响应头部信息
                _WebClient.Headers.Add("Accept", Accept);
                _WebClient.Headers.Add("Accept-Encoding", AcceptEncoding);
                _WebClient.Headers.Add("Accept-Language", AcceptLanguage);
                _WebClient.Headers.Add("UserAgent", UserAgent);
                _WebClient.Headers.Add("Content-Type", ContentType);
            }
        }
        #endregion

        #region 重写方法(Override Methods)
        /// <summary>
        /// 获取HTML内容
        /// </summary>
        /// <returns></returns>
        public override void GetHtmlContent()
        {
            try
            {
                InitSetting();
                HtmlContent = _WebClient.DownloadString(Url);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_WebClient != null)
                    _WebClient.Dispose();
                GC.Collect();
            }
        }

        /// <summary>
        /// 获取HTML内容
        /// </summary>
        /// <returns></returns>
        public override void PostHtmlContent()
        {
            try
            {
                ContentType = "application/x-www-form-urlencoded";
                InitSetting();
                string postData = BuildPostString();
                byte[] byteArray = Encoding.GetEncoding(Charset).GetBytes(postData);
                _WebClient.Headers.Add("ContentLength", byteArray.Length.ToString());
                byte[] recData = _WebClient.UploadData(Url, "POST", byteArray);
                HtmlContent = Encoding.GetEncoding(Charset).GetString(recData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_WebClient != null)
                    _WebClient.Dispose();
                GC.Collect();
            }
        }
        #endregion
    }

    /// <summary>
    /// HTTP Web Request 爬虫
    /// </summary>
    public class HttpWebRequestSpider : BaseSpider, IDisposable
    {
        #region 成员(Member)
        /// <summary>
        /// 请求对象
        /// </summary>
        HttpWebRequest _Request = null;
        /// <summary>
        /// 响应对象
        /// </summary>
        HttpWebResponse _Response = null;

        /// <summary>
        /// 请求URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 请求对象Cookies
        /// </summary>
        public CookieContainer RequestCookies { get; set; }

        /// <summary>
        /// 响应对象Cookies
        /// </summary>
        public CookieContainer ResponseCookies { get; set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        public HttpWebRequestSpider()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public HttpWebRequestSpider(string url)
        {
            Url = url;
        }
        #endregion

        #region 方法(Methods)
        /// <summary>
        /// 初始化设置
        /// </summary>
        private void InitSetting()
        {
            GC.Collect();
            if (string.IsNullOrWhiteSpace(Url))
                return;
            if (_Request == null)
            {
                _Request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Ssl3;
                System.Net.ServicePointManager.DefaultConnectionLimit = 50;
                _Request.AllowAutoRedirect = true;
                //添加请求，响应头部信息
                _Request.Accept = Accept;
                _Request.UserAgent = UserAgent;
                _Request.ContentType = ContentType;
                _Request.Headers.Add("Accept-Encoding", AcceptEncoding);
                _Request.Headers.Add("Accept-Language", AcceptLanguage);
                _Request.Method = Method;
                _Request.Timeout = 20 * 1000;
                _Request.KeepAlive = false;
                _Request.ServicePoint.Expect100Continue = false;
                if (RequestCookies == null)
                    RequestCookies = new CookieContainer();
                _Request.CookieContainer = RequestCookies;
            }
        }

        public virtual void Dispose()
        {
            if (_Response != null)
            {
                _Response.Close();
                _Response.Dispose();
                _Response = null;
            }
            if (_Request != null)
            {
                _Request.Connection.Clone();
                _Request.Abort();
                _Request = null;
            }
            GC.Collect();
        }
        #endregion

        #region 重写方法(Override Methods)
        /// <summary>
        /// 获取HTML内容
        /// </summary>
        /// <returns></returns>
        public override void GetHtmlContent()
        {
            try
            {
                InitSetting();
                _Response = _Request.GetResponse() as HttpWebResponse;
                using (Stream respStream = _Response.GetResponseStream())
                {
                    //如果包含GZIP,需要解压
                    if (!string.IsNullOrEmpty(_Response.ContentEncoding) && _Response.ContentEncoding.ToUpper().IndexOf("GZIP") > -1)
                    {
                        using (StreamReader sr = new StreamReader(new GZipStream(respStream, CompressionMode.Decompress), Encoding.Default))
                        {
                            HtmlContent = sr.ReadToEnd();

                        }
                    }
                    else
                    {
                        using (StreamReader respStreamReader = new StreamReader(respStream, Encoding.Default))
                        {
                            HtmlContent = respStreamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Dispose();
            }
        }

        /// <summary>
        /// 获取HTML内容
        /// </summary>
        /// <returns></returns>
        public override void PostHtmlContent()
        {
            try
            {
                ContentType = "application/x-www-form-urlencoded";
                InitSetting();
                string postData = BuildPostString();
                byte[] byteArray = Encoding.GetEncoding(Charset).GetBytes(postData);
                _Request.ContentLength = byteArray.Length;
                //写入参数
                using (Stream reqStream = _Request.GetRequestStream())
                {
                    reqStream.Write(byteArray, 0, byteArray.Length);
                }
                _Response = _Request.GetResponse() as HttpWebResponse;
                _Response.Cookies = _Request.CookieContainer.GetCookies(_Request.RequestUri);
                int cookies = _Response.Cookies.Count;
                if (ResponseCookies == null)
                    ResponseCookies = _Request.CookieContainer;
                using (Stream respStream = _Response.GetResponseStream())
                {
                    //如果包含GZIP,需要解压
                    if (!string.IsNullOrEmpty(_Response.ContentEncoding) && _Response.ContentEncoding.ToUpper().IndexOf("GZIP") > -1)
                    {
                        using (StreamReader sr = new StreamReader(new GZipStream(respStream, CompressionMode.Decompress), Encoding.GetEncoding(Charset)))
                        {
                            HtmlContent = sr.ReadToEnd();

                        }
                    }
                    else
                    {
                        using (StreamReader respStreamReader = new StreamReader(respStream, Encoding.GetEncoding(Charset)))
                        {
                            HtmlContent = respStreamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException webEx)
            {
                throw webEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Dispose();
            }
        }


        #endregion
    }
}
