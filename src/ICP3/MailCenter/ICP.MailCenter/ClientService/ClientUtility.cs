using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using ICP.Business.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.ServiceInterface;
using ICP.Message.ServiceInterface;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;
using outlook = Microsoft.Office.Interop.Outlook;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientUtility
    {
        #region 属性-OutLook Application
        /// <summary>
        /// 
        /// </summary>
        public static outlook.Application _Application;
        /// <summary>
        /// 当前Outlook Application
        /// </summary>
        public static Application OLApplication
        {
            get
            {
                #region Outlook Application

                try
                {
                    //获取当前运行的Outlook Application
                    _Application = Marshal.GetActiveObject("Outlook.Application") as Application;
                }
                catch
                {
                    try
                    {
                        //获取当前正在运行的OutLook
                        _Application = new Application();
                    }
                    catch (System.Exception ex)
                    {
                        MailUtility.KillProcess("Outlook");
                    }
                }

                #endregion
                return _Application;
            }
            set { _Application = value; }
        }
        #endregion

        #region 属性-OutLook NameSpace
        /// <summary>
        ///  邮件应用程序入口
        /// </summary>
        public static NameSpace OLNameSpace
        {
            get { return OLApplication.Session; }
        }
        #endregion

        /// <summary>
        /// 得到邮件存储临时路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileFullPath(string fileName)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), "MailTemp");
            Directory.CreateDirectory(tempPath);
            return Path.Combine(tempPath, fileName);
        }

        private static readonly object objSync = new object();
        //private static EmailFolderPart _folderPart;
        //public static EmailFolderPart folderPart
        //{
        //    get
        //    {
        //        if (_folderPart != null)
        //        {
        //            return _folderPart;
        //        }
        //        else
        //        {
        //            _folderPart = ServiceClient.GetClientService<Microsoft.Practices.CompositeUI.WorkItem>()
        //                           .SmartParts.Get<EmailFolderPart>(MailCenterWorkSpaceConstants.EmailFolderPart);

        //            return _folderPart;
        //        }
        //    }
        //}
        public static object synObj = new object();
        /// <summary>
        ///  邮件应用程序入口
        /// </summary>
        /// 
        public static outlook.NameSpace OlNS = null;


        public static outlook.Application OlApp
        {
            get
            {
                return GetOutlookAddIn();
            }
            set { _Application = value; }
        }

        /// <summary>
        /// 是否包含cityocean邮箱域名的邮件
        /// </summary>
        public static string CityoceanDomain = "@cityocean.com";

        /// <summary>
        ///关联邮件保存目录 
        /// </summary>
        public static string EmailRelationSaveFolder = "已关联/IsRelation";

        /// <summary>
        /// 自定义邮件Message-ID标签值
        /// </summary>
        public static string CustomMessageIDTag = "http://schemas.microsoft.com/mapi/string/{00020386-0000-0000-C000-000000000046}/Message-ID";
        /// <summary>
        /// 自定义邮件业务上下文标签值
        /// </summary>
        public static string OperationContextTag = "http://schemas.microsoft.com/mapi/string/{00020386-0000-0000-C000-000000000046}/X-OperationContext";
        /// <summary>
        /// Outlook Message-ID标签值
        /// </summary>
        public static string OutlookMessageIDTag = "http://schemas.microsoft.com/mapi/proptag/0x1035001F";
        /// <summary>
        /// 邮件标头中Reference中的标签值
        /// </summary>
        public static string ReferenceTag = "http://schemas.microsoft.com/mapi/proptag/0x1039001F";
        /// <summary>
        /// outlook应用程序入口
        /// </summary>
        /// <returns></returns>
        private static int index = 0;
        public static outlook.Application GetOutlookAddIn()
        {
            //打开一个outlook全局应用程序入口
            try
            {
                if (_Application == null)
                    _Application = Marshal.GetActiveObject("Outlook.Application") as outlook.Application;
                if (_Application != null)
                    OlNS = _Application.Session;

                _Application.NewMailEx += OnApplicationNewMailEx;
            }
            catch
            {
                try
                {
                    _Application = new outlook.Application();
                    OlNS = _Application.Session;
                    //_Application.NewMailEx += OnApplicationNewMailEx;
                }
                catch (System.Exception ex)
                {
                    //ICP.Framework.CommonLibrary.Logger.Log.Error((LocalData.IsEnglish ? "Faulure to using MailCenter! " : "未能正常使用邮件中心!") + CommonHelper.BuildExceptionString(ex));
                    //if (index == 2)
                    //{
                    //    index = 0;
                    //    MailListPresenter.ShowReminderForm(LocalData.IsEnglish ? "Failure to use Microsoft Outlook, please contact the administrator!" : "未能正常使用Microsoft Outlook，请于管理员联系！", true);
                    //}
                    //MailUtility.KillProcess("Outlook");
                    GetOutlookNewNameSpace();
                    index++;
                }
            }

            #region invalidate
            ////outlook security manager
            //if (securityManager == null || securityManager.DisableOOMWarnings == false || StartFlag == true)
            //{
            //    //屏蔽安全对话框
            //    securityManager = new AddinExpress.Outlook.SecurityManager();
            //    securityManager.ConnectTo(_Application);
            //    try
            //    {
            //        securityManager.DisableOOMWarnings = true;   //设置组件安全管理出错时，需要重新启动outlook
            //    }
            //    catch
            //    {
            //        //关闭线程
            //        try
            //        {
            //            securityManager.Disconnect(_Application);
            //            foreach (var p in process)
            //            {
            //                p.Kill();
            //            }
            //        }
            //        catch { }
            //        finally
            //        {
            //            //重新启动outlook
            //            MailUtility.StartProcess();
            //            try
            //            {
            //                _Application = new outlook.Application();
            //            }catch{}

            //            securityManager = new AddinExpress.Outlook.SecurityManager();
            //            securityManager.ConnectTo(_Application);
            //            securityManager.DisableOOMWarnings = true;
            //        }
            //    }
            //}
            //else if (securityManager.DisableOOMWarnings == false)
            //{
            //    securityManager = new AddinExpress.Outlook.SecurityManager();

            //    securityManager.ConnectTo(_Application);
            //    securityManager.DisableOOMWarnings = true;
            //}

            //olInspectors = _Application.Inspectors;
            //olInspectors.NewInspector += new InspectorsEvents_NewInspectorEventHandler(insperctors_NewInspector);

            //_Application.ItemSend -= new Microsoft.Office.Interop.Outlook.ApplicationEvents_11_ItemSendEventHandler(_Application_ItemSend);
            //_Application.ItemSend += new Microsoft.Office.Interop.Outlook.ApplicationEvents_11_ItemSendEventHandler(_Application_ItemSend);
            #endregion

            return _Application;
        }

        public static void LogOff()
        {
            OlNS.Logoff();
        }

        private static string folderEntryID = string.Empty;
        private static string mailItemEntryID = string.Empty;
        private static void OnApplicationNewMailEx(string EntryIDCollection)
        {
            if (string.Compare(mailItemEntryID, EntryIDCollection) == 0)
                return;

            ClientProperties.IsReceiveNewMail = true;
            mailItemEntryID = EntryIDCollection;
            _MailItem olMailItem = MailListPresenter.GetItemByEntryID(mailItemEntryID) as MailItem;
            //收到新邮件
            if (olMailItem != null)
            {
                SetNewMailCount(olMailItem.Parent);
            }
            //收到新回执
            else
            {
                _ReportItem olReportItem = MailListPresenter.GetItemByEntryID(mailItemEntryID) as ReportItem;
                if (olReportItem != null)
                {
                    SetNewMailCount(olReportItem.Parent);
                }
            }
        }

        private static void SetNewMailCount(object objFolder)
        {
            MAPIFolder olFolder = (MAPIFolder)objFolder;
            if (olFolder == null)
                return;

            //收到新邮件后，如果是单个文件夹收到邮件，直接累加，如果两个文件夹先后收到新邮件，以后面收到新邮件的文件夹为主
            if (string.Compare(folderEntryID, olFolder.EntryID) == 0)
            {
                ClientProperties.UnReadCount++;
            }
            else
            {
                ClientProperties.UnReadCount = 0;
                ClientProperties.UnReadCount++;
                folderEntryID = olFolder.EntryID;
            }

            ClientProperties.newMail_UnRead = LocalData.IsEnglish
                                                  ? string.Format("\"{0}\" Received {1} New Mail(s)",
                                                                  olFolder.FullFolderPath,
                                                                  ClientProperties.UnReadCount)
                                                  : string.Format("\"{0}\" 收到{1}封新邮件", olFolder.FullFolderPath,
                                                                  ClientProperties.UnReadCount);


            //folderPart.Expand(olFolder);

            MailUtility.ReleaseComObject(olFolder);
        }


        /// <summary>
        /// 重新获取outlook命名空间
        /// </summary>
        public static void GetOutlookNewNameSpace()
        {
            GetOutlookAddIn();
        }
        /// <summary>
        /// 生成一个outlook NameSapce实例
        /// </summary>
        /// <returns></returns>
        public static NameSpace CreateOutlookNameSpaceInstance()
        {
            GetOutlookAddIn();
            return OlNS;
        }

        /// <summary>
        /// 获取Outlook版本号
        /// </summary>
        private static int _olVersion = 0;
        public static int olVersion
        {
            get
            {
                if (_olVersion == 0)
                {
                    Int32.TryParse(ClientUtility.OlApp.Version.Substring(0, 2), out _olVersion);
                }
                return _olVersion;
            }
        }


        /// <summary>
        /// outlook语言(zh-cn | en-us)
        /// </summary>
        static string culture = string.Empty;
        public static string olCultrue
        {
            get
            {
                if (string.IsNullOrEmpty(culture))
                {
                    LanguageSettings languageSettings = OlApp.LanguageSettings;
                    int lcid = languageSettings.get_LanguageID(MsoAppLanguageID.msoLanguageIDUI);
                    CultureInfo officeUICulture = new CultureInfo(lcid);
                    culture = officeUICulture.Name.ToLower();
                    MailUtility.ReleaseComObject(languageSettings);
                }

                return culture;
            }
        }

        /// <summary>
        /// 邮件类别名称
        /// </summary>
        private static string _GreenCategory = string.Empty;
        public static string GreenCategory
        {
            get
            {
                //判断是否为空
                if (string.IsNullOrEmpty(_GreenCategory))
                {
                    Microsoft.Office.Interop.Outlook.Categories categories = _Application.Session.Categories;
                    //循环所有颜色类别
                    foreach (Microsoft.Office.Interop.Outlook.Category category in categories)
                    {
                        //为绿色标识时返回
                        if (category.Name.ToLower().Equals("green category") || category.Name.ToLower().Equals("绿色类别"))
                        {
                            _GreenCategory = category.Name;
                            break;
                        }
                    }
                }
                return _GreenCategory;
            }
        }

        /// <summary>
        /// 文件类型后缀和对应的文件图标缓冲字典
        /// </summary>
        /// <summary>
        /// 缓存文件图标集合
        /// </summary>
        public static Dictionary<string, Image> _FileIcons;
        public static Dictionary<string, Image> FileIcons
        {
            get
            {
                return _FileIcons ?? (_FileIcons = new Dictionary<string, Image>());
            }
            set { _FileIcons = value; }
        }

        public static Image GetAttachmentIcon(string fileName, string fileExtension)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            Image imgIcon = null;
            if (FileIcons.ContainsKey(fileExtension))
            {
                FileIcons.TryGetValue(fileExtension, out imgIcon);
            }
            else
            {
                imgIcon = SystemFileIcon.Current.GetIconByFileName(fileName).ToBitmap();
                FileIcons.Add(fileExtension, imgIcon);
            }

            return imgIcon;
        }



        public static void SaveCurrentMail(string savePath)
        {

            ClientProperties.CurrentMailItem.SaveAs(savePath, OlSaveAsType.olMSG);

            ClientProperties.CurrentMailItem.Close(OlInspectorClose.olDiscard);
        }
        public static string GetCurrentMailSubject()
        {
            string subject = ClientProperties.CurrentMailItem.Subject ?? "Temp";
            subject = subject.Trim();
            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();
            foreach (char invalidChar in invalidChars)
            {
                subject = subject.Replace(invalidChar.ToString(), "");
            }
            return subject;
        }


        public static void SaveMessage(Message.ServiceInterface.Message messageItem)
        {
            ServiceClient.GetClientService<ICP.DataCache.ServiceInterface.IClientBusinessOperationService>().SaveMessage(messageItem);
        }

        public static Message.ServiceInterface.Message ConvertMailItemToMessageInfo(MailItem mail, bool useOriginalSenderAddress, bool isNeedConvertAttachmentContent)
        {
            return ConvertMailItemInfo(mail, useOriginalSenderAddress, isNeedConvertAttachmentContent);
        }
        public static Message.ServiceInterface.Message ConvertMailItemToMessageInfo(MailItem mail, bool useOriginalSenderAddress)
        {
            return ConvertMailItemInfo(mail, useOriginalSenderAddress, true);
        }

        /// <summary>
        /// 判断邮件是否关联业务
        /// </summary>
        /// <param name="olItem"></param>
        /// <returns></returns>
        public static bool IsRelationOperation(_MailItem olItem)
        {
            bool isRelation = false;
            PropertyAccessor olPA = olItem.PropertyAccessor;
            try
            {
                object objUserProperties = olPA.GetProperty(OperationContextTag);
                if (objUserProperties != null)
                {
                    if (MessageUserPropertiesObject.Convert(objUserProperties.ToString()) != null)
                    {
                        isRelation = true;
                    }
                }
            }
            catch (System.Exception ex) { }
            finally
            {
                MailUtility.ReleaseComObject(olPA);
            }

            return isRelation;
        }
        /// <summary>
        /// 获取邮件的ID
        /// </summary>
        /// <param name="pa"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static string GetMessageId(PropertyAccessor pa, string tagName)
        {
            string messageId = string.Empty;
            object messageIdProperty = null;
            try
            {
                //自定义Message-ID属性可能不存在，需要捕获此异常
                messageIdProperty = pa.GetProperty(tagName);
            }
            catch (System.Exception ex)
            {
                // ICP.Framework.CommonLibrary.Logger.Log.Error(ex.Message);
                return string.Empty;
            }

            if (messageIdProperty != null && !string.IsNullOrEmpty(messageIdProperty.ToString()))
                messageId = messageIdProperty.ToString().ToLower().Trim();
            else
            {
                string messageIdTag = CustomMessageIDTag;
                messageId = GetMessageId(pa, messageIdTag);
            }
            return messageId;
        }
        public static void SetMessageID(_MailItem item)
        {
            PropertyAccessor pa = null;
            //使用mail head来获取邮件信息
            pa = item.PropertyAccessor;

            object messageId = pa.GetProperty(OutlookMessageIDTag);
            if (messageId == null || string.IsNullOrEmpty(messageId.ToString()))
            {
                try
                {
                    messageId = pa.GetProperty(CustomMessageIDTag);
                }
                catch
                {
                    messageId = null;
                }
                if (messageId == null || string.IsNullOrEmpty(messageId.ToString()))
                {
                    //新增邮件，设置邮件的Message-ID
                    pa.SetProperty(ClientUtility.CustomMessageIDTag, GetMessageID());
                }
            }
        }

        /// <summary>
        /// 获取MessageID
        /// </summary>
        /// <param name="hostName"></param>
        /// <returns></returns>
        public static string GetMessageID()
        {
            string hostName = "@cityocean.com";
            int index = LocalData.UserInfo.EmailAddress.IndexOf("@");
            if (index >= 0)
            {
                hostName = LocalData.UserInfo.EmailAddress.Substring(index);
            }
            return string.Format("<mailcenter{0}>", string.Format("{0}{1}", Guid.NewGuid().ToString(), hostName).ToLower());
        }

        /// <summary>
        /// 将Message实体中一些不需要的属性值过滤，减少网络带宽
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <returns></returns>
        public static Message.ServiceInterface.Message GetMessageInfo(Message.ServiceInterface.Message messageInfo)
        {
            if (messageInfo != null)
            {
                //不需要保存事件代码了，通过邮件模板发送邮件的时候就记录了该邮件关联的业务的事件代码
                if (messageInfo.UserProperties != null && messageInfo.UserProperties.EventInfo != null)
                    messageInfo.UserProperties.EventInfo.Code = "Nothing";
                //默认保存邮件的状态是发送成功的
                messageInfo.State = MessageState.Success;

                if (HasCityoceanDomainMail(messageInfo))
                {
                    //多线程控制并发
                    lock (objSync)
                    {
                        //不能将预览邮件的原封装实体的附件清空，会造成邮件附件全部消失，需要克隆实体后将其清空
                        if (messageInfo.Attachments.Count > 0)
                        {
                            messageInfo.Attachments = GetRealAttachmentList();
                        }
                        Message.ServiceInterface.Message cloneMessage = CloneMessageInfo(messageInfo);
                        cloneMessage.Attachments.Clear();
                        return cloneMessage;
                    }
                }
                else
                {
                    lock (objSync)
                    {
                        if (messageInfo.Attachments.Count > 0)
                            messageInfo.Attachments = GetRealAttachmentList();

                        return messageInfo;
                    }
                }
            }

            return messageInfo;
        }
        /// <summary>
        /// 克隆Message 实体
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <returns></returns>
        public static Message.ServiceInterface.Message CloneMessageInfo(Message.ServiceInterface.Message messageInfo)
        {
            if (messageInfo.Attachments.Count > 0)
            {
                if (messageInfo.Attachments.Any(item => item.OLAttachment != null))
                {
                    messageInfo.Attachments = GetRealAttachmentList();
                }
            }
            return ICP.Framework.ClientComponents.Controls.Utility.Clone(messageInfo);
        }
        /// <summary>
        /// 是否包含Cityocean域名的邮件
        /// </summary>
        /// <returns></returns>
        public static bool HasCityoceanDomainMail(Message.ServiceInterface.Message messageInfo)
        {
            bool[] cityOceanDomains = new bool[4];
            cityOceanDomains[0] = !string.IsNullOrEmpty(messageInfo.SendFrom) && messageInfo.SendFrom.ToLower().Contains(CityoceanDomain);
            cityOceanDomains[1] = !string.IsNullOrEmpty(messageInfo.SendTo) && messageInfo.SendTo.ToLower().Contains(CityoceanDomain);
            cityOceanDomains[2] = !string.IsNullOrEmpty(messageInfo.CC) && messageInfo.CC.ToLower().Contains(CityoceanDomain);
            cityOceanDomains[3] = !string.IsNullOrEmpty(messageInfo.BCC) && messageInfo.BCC.ToLower().Contains(CityoceanDomain);
            return cityOceanDomains.Any(item => item == true);
        }


        /// <summary>
        /// 将MailItem转换成Message实体对象
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="useOriginalSenderAddress"></param>
        /// <param name="isNeedConvertAttachmentContent"></param>
        /// <returns></returns>
        private static Message.ServiceInterface.Message ConvertMailItemInfo(MailItem mail, bool useOriginalSenderAddress, bool isNeedConvertAttachmentContent)
        {
            Message.ServiceInterface.Message entry = Message.ServiceInterface.Message.CreateInstance();
            entry.Id = Guid.NewGuid();
            entry.Body = mail.HTMLBody == null ? "" : mail.HTMLBody;
            entry.BodyFormat = BodyFormat.olFormatHTML;
            if (!useOriginalSenderAddress)
            {
                if (mail.SendUsingAccount != null)
                {
                    entry.SendFrom = mail.SendUsingAccount.SmtpAddress; //账户邮件地址
                }
                else
                {
                    entry.SendFrom = mail.SenderEmailAddress;
                }

            }
            else
            {
                entry.SendFrom = mail.SenderEmailAddress;
            }

            entry.State = mail.Sent ? MessageState.Success : MessageState.Draft;
            entry.Type = MessageType.Email;
            entry.Subject = mail.Subject == null ? "" : mail.Subject;
            PropertyAccessor propertyAccessor = mail.PropertyAccessor;
            entry.MessageId = GetMessageId(propertyAccessor, OutlookMessageIDTag);
            entry.CreateBy = LocalData.UserInfo.LoginID;
            entry.UserProperties = GetUserPropertiesOjbect(propertyAccessor);
            entry.CreatorName = mail.SenderName;
            entry.ReceivingTime = mail.ReceivedTime;
            entry.Sendtime = mail.SentOn;
            entry.EntryID = mail.EntryID;
            entry.Size = mail.Size;
            //保存消息到数据库，需要获取真正的邮件地址
            string toNameList = string.Empty;
            entry.SendTo = MailListPresenter.ConvertRecipientsToString(mail.Recipients, (int)OlMailRecipientType.olTo, out toNameList);
            entry.ToName = toNameList;

            string ccNameList = string.Empty;
            entry.CC = MailListPresenter.ConvertRecipientsToString(mail.Recipients, (int)OlMailRecipientType.olCC, out ccNameList);
            entry.CCName = ccNameList;

            string bccNameList = string.Empty;
            entry.BCC = MailListPresenter.ConvertRecipientsToString(mail.Recipients, (int)OlMailRecipientType.olBCC, out bccNameList);
            entry.BCCName = bccNameList;
            if (isNeedConvertAttachmentContent)
            {
                entry.HasAttachment = mail.Attachments != null && mail.Attachments.Count > 0;
                entry.Attachments.AddRange(GetAttachmentContents(mail));

                entry.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

            }
            else
            {
                //转换邮件临时实体类
                entry.CreateDate = mail.ReceivedTime;


            }

            return entry;
        }
        /// <summary>
        /// 将ReportItem（回执）转换成Message实体对象
        /// </summary>
        /// <param name="reportItem"></param>
        /// <returns></returns>
        public static Message.ServiceInterface.Message ConvertReportItemToMessageInfo(_ReportItem reportItem)
        {
            Message.ServiceInterface.Message info; string arrTo = string.Empty, arrCC = string.Empty, senderName = string.Empty, senderEmailAddress = string.Empty; DateTime sentOn;

            PropertyAccessor olPA = reportItem.PropertyAccessor;
            Object objRecipient = olPA.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x0E04001F");
            object objCC = olPA.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x0E03001F");
            object objEmailAddress = olPA.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x0C1F001F");
            object objSenderName = olPA.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x0C1A001F");
            object objSentOn = olPA.GetProperty("urn:schemas:httpmail:date");

            string toName = string.Empty;
            if (objRecipient == null)
                arrTo = string.Empty;
            else
            {
                arrTo = objRecipient.ToString();
                toName = objRecipient.ToString();
            }

            string ccName = string.Empty;
            if (objCC == null)
                arrCC = string.Empty;
            else
            {
                arrCC = objCC.ToString();
                ccName = objCC.ToString();
            }

            senderEmailAddress = objEmailAddress == null ? "" : objEmailAddress.ToString();
            senderName = objSenderName == null ? "" : objSenderName.ToString();
            sentOn = objSentOn == null ? Convert.ToDateTime("4501-1-1 0:00:00") : Convert.ToDateTime(objSentOn.ToString());
            info = new ICP.Message.ServiceInterface.Message()
            {
                Body = reportItem.Body,
                CC = arrCC,
                SendTo = arrTo,
                Subject = reportItem.Subject,
                CreatorName = senderName,
                SendFrom = senderEmailAddress,
                CreateDate = sentOn,
                ToName = toName,
                CCName = ccName,
                State = MessageState.Transmitted
            };
            info.Sendtime = sentOn;
            info.EntryID = reportItem.EntryID;

            MailUtility.ReleaseComObject(olPA);

            return info;
        }
        /// <summary>
        /// 将选择的邮件的每个附件都映射本地两个路径（LocalPath，PreviewPath）
        /// </summary>
        /// <returns></returns>
        public static List<AttachmentContent> GetRealAttachmentList()
        {
            for (int i = 0; i < ClientProperties.Attachments.Count; i++)
            {
                AttachmentContent attachmentInfo = ClientProperties.Attachments[i];
                if (attachmentInfo != null && attachmentInfo.OLAttachment != null)
                {
                    string fullPath = MailListPresenter.GetFileFullPath(attachmentInfo.Name);
                    MailListPresenter.SaveAsLocalFile(attachmentInfo.OLAttachment as Attachment, fullPath);
                    attachmentInfo.ClientPath = fullPath;

                    if (string.IsNullOrEmpty(attachmentInfo.PreviewPath))
                    {
                        string previewPath = MailListPresenter.GetFileFullPath(attachmentInfo.Name);
                        MailListPresenter.SaveAsLocalFile(attachmentInfo.OLAttachment as Attachment, previewPath);
                        attachmentInfo.PreviewPath = previewPath;
                    }

                    attachmentInfo.OLAttachment = null;
                }
            }

            return ClientProperties.Attachments;
        }


        public static List<AttachmentContent> GetAttachmentContents(_MailItem mail)
        {
            List<AttachmentContent> contents = new List<AttachmentContent>();
            Attachments mailAttachments = mail.Attachments;

            DateTime now = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            for (int i = 1; i <= mailAttachments.Count; i++)
            {
                Attachment currentAttachment = mailAttachments[i];

                string contentID = currentAttachment.PropertyAccessor.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x3712001F").ToString();
                if (string.IsNullOrEmpty(contentID))
                {
                    string fullFilePath = Path.Combine(ClientProperties.TempPath, currentAttachment.FileName);
                    try
                    {
                        if (File.Exists(fullFilePath)) { File.Delete(fullFilePath); }
                        currentAttachment.SaveAsFile(fullFilePath);
                    }
                    catch
                    { }
                    finally
                    {
                        contents.Add(new AttachmentContent { Name = currentAttachment.FileName, Content = MailUtility.ReadFileContentFromDisk(fullFilePath), UploadTime = now, ClientPath = fullFilePath });
                        MailUtility.ReleaseComObject(currentAttachment);
                    }
                }
            }
            MailUtility.ReleaseComObject(mailAttachments);

            return contents;

        }
        /// <summary>
        /// 获取邮件自定义信息
        /// </summary>
        /// <param name="pa"></param>
        /// <returns></returns>
        public static MessageUserPropertiesObject GetUserPropertiesOjbect(PropertyAccessor pa)
        {
            MessageUserPropertiesObject propertiesObject = null;
            try
            {
                //获取邮件自定义信息
                object objUserProperties = pa.GetProperty(OperationContextTag);

                if (objUserProperties != null && objUserProperties != "")
                {
                    propertiesObject = MessageUserPropertiesObject.Convert(objUserProperties.ToString());
                }
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                //获取邮件Reference
                object objReference = pa.GetProperty(ReferenceTag);
                if (objReference != null && objReference != "")
                {
                    if (propertiesObject == null)
                        propertiesObject = new MessageUserPropertiesObject() { OperationId = Guid.Empty, OperationType = OperationType.Unknown };
                    string[] references = objReference.ToString().Split(new char[1] { '>' });
                    if (references != null && references.Length > 0)
                    {
                        propertiesObject.Reference = string.Format("{0}>", references[0]);
                    }
                }

                MailUtility.ReleaseComObject(pa);
            }

            return propertiesObject;
        }

        private static Guid GetFolderId(_MailItem mail)
        {
            throw new NotImplementedException();
        }

        #region OutLook Folder Manage
        /// <summary>
        /// 添加文件夹目录
        /// </summary>
        /// <param name="selectFolder">选中文件夹</param>
        /// <param name="newFolderName">新文件夹名称</param>
        /// <returns>返回值：outlook.MAPIFolder新增成功返回新增的文件夹对象否则返回null</returns>
        public static outlook.MAPIFolder AddFolder(outlook.MAPIFolder selectFolder, string newFolderName, ref bool isExsitsFolder)
        {
            if (selectFolder == null)
                return null;

            outlook.MAPIFolder newFolder = null;
            try
            {
                //查找目录
                newFolder = MailUtility.FindMAPIFolderbyName(selectFolder, newFolderName);
                //未找到需创建的文件夹
                if (newFolder == null)
                {
                    newFolder = selectFolder.Folders.Add(
                        EmailRelationSaveFolder, Type.Missing)
                        as outlook.Folder;
                    isExsitsFolder = false;
                }
                else
                    isExsitsFolder = true;
            }
            catch
            {
                newFolder = null;
                isExsitsFolder = false;
            }
            return newFolder;
        }

        /// <summary>
        /// 从文件夹列表中获取关联邮件保存文件夹对象
        /// </summary>
        /// <param name="folders">文件夹集合对象</param>
        /// <returns>返回值：文件夹对象</returns>
        public static outlook.MAPIFolder GeteRelationEmailFolder(Store storeItem)
        {
            outlook.MAPIFolder resultValue = null;
            try
            {
                outlook.Folder inboxTemp = GetInboxFolder(storeItem);
                //循环当前文件夹下的子文件夹
                foreach (outlook.Folder item in inboxTemp.Folders)
                {
                    if (item.Name.Equals(EmailRelationSaveFolder)) //如果文件夹名相同则返回
                    {
                        resultValue = item;
                        break;
                    }
                }
            }
            catch { resultValue = null; }
            return resultValue;
        }

        /// <summary>
        /// 从文件夹列表中获取关联邮件保存文件夹对象
        /// </summary>
        /// <param name="folders">文件夹集合对象</param>
        /// <returns>返回值：文件夹对象</returns>
        public static outlook.Folder GetInboxFolder(Store storeItem)
        {
            outlook.Folder resultValue = null;
            try
            {
                foreach (outlook.Folder item in storeItem.GetRootFolder().Folders)
                {
                    if (item.Name.Equals("收件箱") || item.Name.ToLower().Equals("Inbox")) //如果文件夹名相同则返回
                    {
                        resultValue = item;
                        break;
                    }
                }
            }
            catch { resultValue = null; }
            return resultValue;
        }
        #endregion



        /// <summary>
        /// 保存邮件的事件和邮件
        /// </summary>
        /// <param name="item">邮件实体</param>
        public static void NewSeveMeassage(MailItem item)
        {
            Message.ServiceInterface.Message message = ClientUtility.ConvertMailItemToMessageInfo(item, false);
            if (string.IsNullOrEmpty(message.SendFrom))
            {
                message.SendFrom = LocalData.UserInfo.EmailAddress;
            }
            ServiceClient.GetService<IICPCommonOperationService>().SaveMessage(message);
        }


    }


}
