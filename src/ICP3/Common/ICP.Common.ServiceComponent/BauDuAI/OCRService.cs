using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ICP.Common.ServiceComponent
{
    partial class CommonService
    {
        public static BaiDuAIAccessToken AccessToken;
        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        public BaiDuAIAccessToken GetAccessToken(string clientId = "7hDwESqdMrjUEj8SzPIdfceh", string clientSecret = "GRbbDMpctek7d4jWbaZDvtzxMB8waCVQ")
        {
            if (AccessToken == null || (AccessToken != null && AccessToken.expiresTime < DateTime.Now))
            {
                string authHost = "https://aip.baidubce.com/oauth/2.0/token";
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = authHost + string.Format("?grant_type=client_credentials&client_id={0}&client_secret={1}", clientId, clientSecret),
                    Encoding = Encoding.UTF8,
                    Method = "GET",
                    ContentType = "application/x-www-form-urlencoded",
                    SecurityProtocolType = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072,
                };
                HttpResult result = http.GetHtml(item);
                AccessToken = JSONSerializerHelper.DeserializeFromJson<BaiDuAIAccessToken>(result.Html);
                AccessToken.expiresTime = DateTime.Now.AddSeconds(AccessToken.expires_in / 30);//一天
            }
            return AccessToken;
        }

        /// <summary>
        /// 识别数字
        /// </summary>
        /// <param name="token">token 字符串</param>
        /// <param name="body">数字图片 Base64字符串</param>
        /// <returns>数字字符串</returns>
        public string GetNumerResult(string tokenString, string base64StringBody)
        {
            string authHost = "https://aip.baidubce.com/rest/2.0/ocr/v1/numbers?access_token=" + tokenString;
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = authHost,
                Encoding = Encoding.UTF8,
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",
                SecurityProtocolType = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072,
                Postdata = "image=" + http.URLEncode(base64StringBody, Encoding.Default)
            };
            //得到新的HTML代码
            HttpResult result = http.GetHtml(item);
            return result.Html;
        }
    }
}
