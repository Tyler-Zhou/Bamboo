using ICP.EDI.PluginInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;

namespace NB.CLP
{
    /// <summary>
    /// 无纸化
    /// </summary>
    public class CLPAPI : IEDIPluginService
    {
        public void BuildData(EDIPluginInput source, EDIPluginOut target)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(source.DataSetXML);


            XmlElement root = xmldoc.DocumentElement;
            XmlNodeList listNodes = root.SelectNodes("BillInfo");
            XmlNodeList listCargoInfo = root.SelectNodes("cargoList");
            List<BillInfo> billList = new List<BillInfo>();
            foreach (XmlNode node01 in listNodes)
            {
                BillInfo bi = new BillInfo();
                bi.cargoList = new List<CargoInfo>();
                foreach (XmlNode node0101 in node01.ChildNodes)
                {
                    if (node0101.InnerText.IsNullOrEmpty())
                        continue;
                    bi.SetPropertyValue(node0101.Name, node0101.InnerText);
                }
                billList.Add(bi);
            }
            foreach (var item in billList)
            {
                foreach (XmlNode node02 in listCargoInfo)
                {
                    CargoInfo ci = new CargoInfo();
                    foreach (XmlNode node0201 in node02.ChildNodes)
                    {
                        if (node0201.InnerText.IsNullOrEmpty())
                            continue;
                        ci.SetPropertyValue(node0201.Name, node0201.InnerText);
                    }
                    item.cargoList.Add(ci);
                }
            }
            JsonSerializerSettings mJsonSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            target.EDIData = JsonConvert.SerializeObject(billList, Newtonsoft.Json.Formatting.None, mJsonSettings);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="values"></param>
        public void SendData(IDictionary<string, object> values)
        {
            if (values == null)
                return;
            string api_url = values["API_URL"].ToString();
            string api_user_id = values["API_User_ID"].ToString();
            string api_key = values["API_Key"].ToString();
            string api_Data = values["API_Data"].ToString();
            string api_method = values["API_Method"].ToString();
            //日期格式化
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //按照ASCII排序参数
            string sign = string.Format("data={0}&format=json&method={1}&timestamp={2}&user_id={3}&version=2.0&key={4}", api_Data, api_method, timestamp, api_user_id, api_key);
            string postData= string.Format("user_id={0}&version=2.0&format=json&timestamp={1}&sign={2}&method={3}&data={4}", api_user_id, timestamp, Cryptography.EncryptByMD532Bit(sign), api_method, HttpUtility.UrlEncode(api_Data));

            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = api_url,
                Encoding = Encoding.UTF8,
                Method = "post",
                ContentType = "application/x-www-form-urlencoded",
                Postdata = postData,
            };
            //得到新的HTML代码
            HttpResult result = http.GetHtml(item);
            Result clpResult = JsonConvert.DeserializeObject<Result>(result.Html);
            if ("F".Equals(clpResult.data.result))
            {
                throw new Exception(clpResult.data.errorInfo);
            }
        }
    }
}
