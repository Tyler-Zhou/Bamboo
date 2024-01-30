/*****
 *类说明:邮件模版操作类
 *创建者:王乐俊
 *创建时间: 2013-10-21
******/
using System;
using System.IO;
using System.Linq;
using System.Text;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// 邮件模版操作类
    /// </summary>
    public class EmailTemplateGetvaluation
    {

        /// <summary>
        /// 返回Message对象
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public ICP.Message.ServiceInterface.Message ReturnMessage(ICP.Message.ServiceInterface.Message mail, bool isEnglish, string itemKey, object[] values)
        {
            EmailTemplateItemData template = null;
            if (isEnglish)
            {
                template = Get("en-US", "Common", itemKey, values);
            }
            else
            {
                template = Get("zh-CN", "Common", itemKey, values);
            }
            if (template != null)
            {
                mail.Body = template.Body;
                mail.Subject = template.Subject;
            }
            else
            {
                mail = null;
            }
            return mail;
        }


        /// <summary>
        /// 获取指定语言指定段的值
        /// </summary>
        /// <param name="languageName"></param>
        /// <param name="sectionName"></param>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public EmailTemplateItemData Get(string languageName, string sectionName, string itemKey, object[] values)
        {
            EmailTemplateItemData template = EmailTemplateLoader.Current[languageName][sectionName][itemKey] as EmailTemplateItemData;
            if (template == null)
            {
                return GetHtml(languageName, sectionName, itemKey, values);
            }
            if (values != null && values.Any())
            {
                if (template != null)
                {
                    template.Subject = GetSubject(template.Subject, values).Trim();
                    template.Body = GetBody(template.Body, values).Trim().Replace("OE.Remark", string.Empty).Replace("OE.ContainerDescription", string.Empty).Replace("Notice", string.Empty);
                }
            }
            return template;
        }
        /// <summary>
        /// 提取HTML的模版内容
        /// </summary>
        /// <param name="languageName"></param>
        /// <param name="sectionName"></param>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public EmailTemplateItemData GetHtml(string languageName, string sectionName, string itemKey, object[] values)
        {
            if (string.IsNullOrEmpty(itemKey)) return null;
            EmailTemplateItemData templateItemData = new EmailTemplateItemData();
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            path = Path.Combine(path, "EmailTemplate" + "\\" + "EmailTemplateHTML");
            if (languageName == "en-US")
            {
                path = path + "\\" + itemKey + "EN.htm";
            }
            else
            {
                path = path + "\\" + itemKey + "CN.htm";
            }
            //根据路径读取的HTML文本的内容
            StreamReader streamReader = new StreamReader(path, System.Text.Encoding.Default);
            StringBuilder template = new StringBuilder();
            template.Append(streamReader.ReadToEnd());
            string[] templateHtml = template.ToString().Split('$');
            if (templateHtml.Any())
            {
                templateItemData.Subject = GetSubject(templateHtml[0], values).Trim();
                templateItemData.Body = GetBody(templateHtml[1], values).Trim()
                                                                        .Replace("OE.Remark", string.Empty)
                                                                        .Replace("OE.ContainerDescription", string.Empty)
                                                                        .Replace("OE.PlaceOfReceiptName", string.Empty)
                                                                        .Replace("OE.Commodity", string.Empty)
                                                                        .Replace("OE.OceanShippingOrderNo", string.Empty)
                                                                        .Replace("Notice", string.Empty);
            }
            //对此赋值完后关闭当前读取文件的流
            streamReader.Close();
            return templateItemData;
        }

        /// <summary>
        ///  邮件标题
        /// </summary>
        /// <param name="subject">截取的字符串</param>
        /// <param name="values">对象值</param>
        /// <returns></returns>
        public string GetSubject(string subject, object[] values)
        {
            string subJect = string.Empty;
            if (SetStrArray(subject).Length == 0)
            {
                subJect = subject;
            }
            else
            {
                foreach (object temp in values)
                {
                    #region 自定义实体
                    if (temp is ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanBookingInfo)
                    {
                        subJect = SetString(temp, !string.IsNullOrEmpty(subJect) ? subJect : subject, true);
                    }
                    else if (temp is ICP.FCM.AirExport.ServiceInterface.DataObjects.AirBookingInfo)
                    {
                        subJect = SetString(temp, !string.IsNullOrEmpty(subJect) ? subJect : subject, true);
                    }
                    else if (temp is ICP.FCM.OceanImport.ServiceInterface.MailOIBusinessDataObjects)
                    {
                        subJect = SetString(temp, !string.IsNullOrEmpty(subJect) ? subJect : subject, true);
                    }
                    else if (temp is ICP.FCM.AirImport.ServiceInterface.AirBusinessInfo)
                    {
                        subJect = SetString(temp, !string.IsNullOrEmpty(subJect) ? subJect : subject, true);
                    }
                    else if (temp is ICP.FRM.ServiceInterface.DataObjects.BaseInquireRate)
                    {
                        subJect = SetString(temp, !string.IsNullOrEmpty(subJect) ? subJect : subject, true);
                    }
                    //else if (temp is ICP.Business.Common.ServiceInterface.DataObjects.QuotedPriceOrderInfo)
                    //{
                    //    subJect = SetString(temp, !string.IsNullOrEmpty(subJect) ? subJect : subject, true);
                    //}
                    #endregion
                    //values包含多个对象
                    subject = string.IsNullOrEmpty(subJect) ? subject : subJect;
                }
            }
            return subject.Replace("&", string.Empty).Replace("%", string.Empty);
        }
        /// <summary>
        ///  邮件内容
        /// </summary>
        /// <param name="body">截取的字符串</param>
        /// <param name="values">对象值</param>
        /// <returns></returns>
        public string GetBody(string body, object[] values)
        {
            string boDy = string.Empty;
            if (SetStrArray(body).Length == 0)
            {
                boDy = body;
            }
            else
            {
                for (int item = 0; item < values.Length; item++)
                {
                    object temp = values[item];
                    #region 自定义实体及其自定义字符串替换
                    if (temp is ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanBookingInfo)
                    {
                        boDy = SetString(temp, !string.IsNullOrEmpty(boDy) ? boDy : body, true);
                    }
                    else if (temp is ICP.FCM.AirExport.ServiceInterface.DataObjects.AirBookingInfo)
                    {
                        boDy = SetString(temp, !string.IsNullOrEmpty(boDy) ? boDy : body, true);
                    }
                    else if (temp is ICP.FCM.OceanImport.ServiceInterface.MailOIBusinessDataObjects)
                    {
                        boDy = SetString(temp, !string.IsNullOrEmpty(boDy) ? boDy : body, true);
                    }
                    else if (temp is ICP.FCM.AirImport.ServiceInterface.AirBusinessInfo)
                    {
                        boDy = SetString(temp, !string.IsNullOrEmpty(boDy) ? boDy : body, true);
                    }
                    else if (temp is ICP.Framework.CommonLibrary.Common.EventObjects)
                    {
                        boDy = SetString(temp, !string.IsNullOrEmpty(boDy) ? boDy : body, true);
                    }
                    else if (temp is ICP.FRM.ServiceInterface.DataObjects.BaseInquireRate)
                    {
                        boDy = SetString(temp, !string.IsNullOrEmpty(boDy) ? boDy : body, true);
                    }
                    //else if (temp is ICP.Business.Common.ServiceInterface.DataObjects.QuotedPriceOrderInfo)
                    //{
                    //    boDy = SetString(temp, !string.IsNullOrEmpty(boDy) ? boDy : body, true);
                    //}
                    else
                    {
                        boDy = SetString(temp, !string.IsNullOrEmpty(boDy) ? boDy : body, false);
                    } 
                    #endregion
                    //values包含多个对象
                    body = string.IsNullOrEmpty(boDy) ? body : boDy;
                }
            }
            return body.Replace("&", string.Empty).Replace("%", string.Empty);
        }
        /// <summary>
        /// 根据字符提取参数
        /// </summary>
        /// <param name="strObject">字符串</param>
        /// <returns>返回分割以后的字符信息</returns>
        public string[] SetStrArray(string strObject)
        {
            string[] str = null;
            string[] strArray = strObject.Split(new char[] { '&', '%' });
            if (strArray.Length > 0)
            {
                str = strArray;
            }
            return str;
        }

        /// <summary>
        /// 返回已替换值的字符串
        /// </summary>
        /// <param name="values">对象</param>
        /// <param name="replaceStr">替换字符串</param>
        /// <returns></returns>
        public string SetString(object values, string replaceStr, bool isEntity)
        {
            string newStr = string.Empty;
            Type typeObject = values.GetType();
            if (values != null)
            {
                string[] strObject = SetStrArray(replaceStr);
                if (strObject.Length > 0)
                {
                    //客户端用户ID未取到值则为服务端调用
                    UserDetailInfo userDetail = null;
                    try
                    {
                        userDetail = ICP.Framework.CommonLibrary.Client.ServiceClient.GetService<IUserService>()
                                                   .GetUserDetailInfo(ICP.Framework.CommonLibrary.Client.LocalData.UserInfo.LoginID);
                    }
                    catch
                    {
                        //查找用户出现异常：
                        //  1.未找到用户;
                        //  2.当前为服务端之间调用，LoginID未获取到值
                        userDetail = null;
                    }
                    foreach (var item in strObject)
                    {
                        #region 循环替换变量字段或常量字符串
                        if (!string.IsNullOrEmpty(item))
                        {
                            if (item.Contains("Incept") && isEntity == false)
                            {
                                newStr = replaceStr.Replace(item, values.ToString());
                            }
                            else if (item.Contains("items") && isEntity == false)
                            {
                                newStr = replaceStr.Replace(item, values.ToString());
                            }
                            else if (item.Contains("OE") && isEntity == true)
                            {
                                newStr = SetNewString("OE", typeObject, item, values, replaceStr, newStr);
                            }
                            else if (item.Contains("OI") && isEntity == true)
                            {
                                newStr = SetNewString("OI", typeObject, item, values, replaceStr, newStr);
                            }
                            else if (item.Contains("AE") && isEntity == true)
                            {
                                newStr = SetNewString("AE", typeObject, item, values, replaceStr, newStr);
                            }
                            else if (item.Contains("AI") && isEntity == true)
                            {
                                newStr = SetNewString("AI", typeObject, item, values, replaceStr, newStr);
                            }
                            else if (item.Contains("EV") && isEntity == true)
                            {
                                newStr = SetNewString("EV", typeObject, item, values, replaceStr, newStr);
                            }
                            else if (item.Contains("IR") && isEntity == true)
                            {
                                newStr = SetNewString("IR", typeObject, item, values, replaceStr, newStr);
                            }
                            //else if (item.Contains("QP") && isEntity == true)
                            //{
                            //    newStr = SetNewString("QP", typeObject, item, values, replaceStr, newStr);
                            //}
                            else if (item.Contains("Present"))
                            {
                                if (userDetail != null) newStr = newStr.Replace(item, userDetail.EName);
                            }
                            else if (item.Contains("Tels"))
                            {
                                #region Tels
                                if (userDetail != null)
                                {
                                    Guid OperId = Guid.Empty;
                                    if (values is ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanBookingInfo)
                                    {
                                        var oceanBookingInfo = values as ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanBookingInfo;
                                        OperId = oceanBookingInfo.ID;
                                    }
                                    else if (values is ICP.FCM.AirExport.ServiceInterface.DataObjects.AirBookingInfo)
                                    {
                                        var airBookingInfo = values as ICP.FCM.AirExport.ServiceInterface.DataObjects.AirBookingInfo;
                                        OperId = airBookingInfo.ID;
                                    }
                                    else if (values is ICP.FCM.OceanImport.ServiceInterface.MailOIBusinessDataObjects)
                                    {
                                        var mailOiBusinessDataObjects = values as ICP.FCM.OceanImport.ServiceInterface.MailOIBusinessDataObjects;
                                        OperId = mailOiBusinessDataObjects.OIBookingID;
                                    }
                                    else if (values is ICP.FCM.AirImport.ServiceInterface.AirBusinessInfo)
                                    {
                                        var airBusinessInfo = values as ICP.FCM.AirImport.ServiceInterface.AirBusinessInfo;
                                        OperId = airBusinessInfo.ID;
                                    }
                                    UserInfo userInfo = ServiceClient.GetService<IOceanExportService>().GetBookingerId(OperId);
                                    newStr = newStr.Replace(item, userInfo == null ? userDetail.Tel : userInfo.Tel);
                                } 
                                #endregion
                            }
                            else if (item.Contains("Emails"))
                            {
                                if (userDetail != null)
                                {
                                    newStr = newStr.Replace(item, userDetail.EMail);
                                }
                            }
                            else if (item.Contains("Notice"))
                            {
                                System.Data.DataTable dt = ICP.Framework.CommonLibrary.Client.ServiceClient
                                    .GetService<ICP.FCM.OceanExport.ServiceInterface.IOceanExportService>()
                                    .GetEmailNotice(ICP.Framework.CommonLibrary.Client.LocalData.UserInfo.DefaultDepartmentID);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    newStr = newStr.Replace(item, dt.Rows[0][0].ToString());
                                }
                            }
                            else if (item.Contains("Company"))
                            {
                                //取当前用户的公司名
                                var firstOrDefault = ICP.Framework.CommonLibrary.Client.LocalData.UserInfo.UserOrganizationList.
                                    FirstOrDefault(n => n.Type == ICP.Framework.CommonLibrary.Server.LocalOrganizationType.Company && n.IsDefault == true);
                                if (firstOrDefault != null)
                                {
                                    string company = firstOrDefault.EShortName;
                                    //取当前用户的部门名
                                    var localOrganizationInfo = ICP.Framework.CommonLibrary.Client.LocalData.UserInfo.UserOrganizationList.
                                        FirstOrDefault(n => n.Type == ICP.Framework.CommonLibrary.Server.LocalOrganizationType.Department);
                                    if (localOrganizationInfo != null)
                                    {
                                        company = company + "/" + localOrganizationInfo.EShortName;
                                    }
                                    newStr = newStr.Replace(item, company + ".");
                                }
                            }
                            else if (item.Contains("Faxs"))
                            {
                                if (userDetail != null)
                                {
                                    newStr = newStr.Replace(item, userDetail.Fax);
                                }
                            }
                        } 
                        #endregion
                    }
                }
            }
            return newStr;
        }
        /// <summary>
        /// 根据条件对于replaceStr字符进行数据替换操作
        /// </summary>
        /// <param name="verdict">需要移除的字段名</param>
        /// <param name="typeObject">Type对象</param>
        /// <param name="chars">当前字符</param>
        /// <param name="values">替换的数据对象</param>
        /// <param name="replaceStr">需要替换的字符</param>
        /// <param name="newStr">新的字符串</param>
        /// <returns>返回一个新的字符</returns>
        public string SetNewString(string verdict, Type typeObject, string chars, object values, string replaceStr, string newStr)
        {
            string newValue = string.Empty;
            string newchars = chars.Replace(verdict + ".", string.Empty);
            string objectname = GetObjectName(verdict);
            string fullname = values.GetType().FullName;
            //如需要特殊处理的字段信息，请在字段后面加入?符号 如：（@%OE.DOCClosingDate?yyyy-mm-dd@%）
            if (newchars.Contains("?"))
            {
                string[] replachars = newchars.Split('?');
                if (replachars.Length > 0)
                {
                    string format = replachars[1];
                    string strValue = typeObject.GetProperty(replachars[0]).GetValue(values, null).ToString();
                    DateTime dateTime;
                    if (replachars.Length == 2)  
                    {
                        #region 旧模板格式

                        if (DateTime.TryParse(strValue, out dateTime))
                        {
                            DateTime datevalue = DateTime.Parse(strValue);
                            newValue = datevalue.ToString(format);
                        }
                        else
                        {
                            string strdecimal = typeObject.GetProperty(newchars).GetValue(values, null).ToString();
                            decimal decimals = 0;
                            if (decimal.TryParse(strdecimal, out decimals))
                            {
                                decimal decvalue = decimal.Parse(strdecimal);
                                newValue = decvalue.ToString(format);
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        #region 新模板格式
                        switch (format)
                        {
                            case "Boolean":     //Boolean格式化    Demo:value?Boolean?YesORNo
                                if (replachars[2].Equals("YesORNo"))
                                    newValue = Boolean.Parse(strValue) ? "Yes" : "NO";
                                else
                                    newValue = strValue;
                                break;
                            case "DateTime":    //DateTime格式化  Demo:value?DateTime?yyyy-mm-dd
                                DateTime.TryParse(strValue, out dateTime);
                                DateTime datevalue = DateTime.Parse(strValue);
                                newValue = datevalue.ToString(replachars[2]);
                                break;
                            case "Decimal":     //Decimal格式化  Demo:value?Decimal?0.00 保留两位小数
                                decimal decimals = 0;
                                if (decimal.TryParse(strValue, out decimals))
                                {
                                    decimal decvalue = decimal.Parse(strValue);
                                    newValue = decvalue.ToString(replachars[2]);
                                }
                                break;
                        }
                        #endregion
                    }
                }
            }
            //当前的对象的FullName是否包含对象的名称
            else if (fullname.Contains(objectname))
            {
                object obj = typeObject.GetProperty(newchars).GetValue(values, null);

                newValue = (obj == null||obj.Equals("")) ? " " : obj.ToString();
            }
            if (newStr == string.Empty && !string.IsNullOrEmpty(newValue))
            {
                newStr = replaceStr.Replace(chars, newValue);
            }
            else if (!string.IsNullOrEmpty(newValue))
            {
                newStr = newStr.Replace(chars, newValue);
            }
            return newStr;

        }

        ///<summary>
        /// 返回当前对象的名称
        ///</summary>
        ///<param name="verdict">简写</param>
        ///<returns></returns>
        public string GetObjectName(string verdict)
        {
            string objectname = string.Empty;
            switch (verdict)
            {
                case "OE":
                    objectname = "OceanBookingInfo";
                    break;
                case "OI":
                    objectname = "MailOIBusinessDataObjects";
                    break;
                case "AE":
                    objectname = "AirBookingInfo";
                    break;
                case "AI":
                    objectname = "AirBusinessInfo";
                    break;
                case "EV":
                    objectname = "EventObjects";
                    break;
                case "IR":
                    objectname = "BaseInquireRate";
                    break;
                //case "QP":
                //    objectname = "QuotedPriceOrderInfo";
                //    break;
            }
            return objectname;
        }
        //public string GetHtmlBody(ICP.Message.ServiceInterface.Message messageInfo, string content, bool isEnglish)
        //{
        //    string filePath = string.Format("{0}\\SendMailAppendContent{1}.htm", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplate" + "\\" + "EmailTemplateHTML"), (isEnglish ? "EN" : "CN"));

        //    string appendContent = string.Empty;
        //    using (StreamReader reader = new StreamReader(filePath))
        //    {
        //        appendContent = reader.ReadToEnd();
        //        reader.Close();
        //    }
        //    messageInfo.Body = ReplaceContent(appendContent, messageInfo, content) + messageInfo.Body;

        //    return messageInfo.Body;
        //}

        //private string ReplaceContent(string appentContent, ICP.Message.ServiceInterface.Message messageInfo, string content)
        //{
        //    string strContent = string.Empty;
        //    if (!string.IsNullOrEmpty(appentContent))
        //    {

        //        strContent = Replace(appentContent, "%Content%", content);
        //        strContent = Replace(strContent, "%Signature%", string.Empty);
        //        strContent = Replace(strContent, "%SenderName%", messageInfo.CreatorName);
        //        strContent = Replace(strContent, "%Email%", messageInfo.SendFrom);
        //        strContent = Replace(strContent, "%Sent%", messageInfo.CreateDate.ToString());
        //        strContent = Replace(strContent, "%Recepient%", messageInfo.SendTo);
        //        strContent = Replace(strContent, "%CC%", messageInfo.CC);
        //        strContent = string.Format(strContent, !string.IsNullOrEmpty(messageInfo.CC) ? "inherit" : "none");
        //        strContent = Replace(strContent, "%Subject%", messageInfo.Subject);
        //    }

        //    return strContent;
        //}

        //private string Replace(string content, string oldValue, string newValue)
        //{
        //    if (content.Contains(oldValue))
        //    {
        //        content = content.Replace(oldValue, newValue);
        //    }

        //    return content;

        //}
    }
}
