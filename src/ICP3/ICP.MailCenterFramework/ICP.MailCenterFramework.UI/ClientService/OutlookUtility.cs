#region Comment

/*
 * 
 * FileName:    OutlookUtility.cs
 * CreatedOn:   2014/7/8 15:30:42
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->Outlook 工具类
 *      ->  1.获取当前OutLook
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System.Runtime.Serialization.Formatters.Binary;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.MailCenterFramework.ServiceInterface;
using ICP.Message.ServiceInterface;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Exception = System.Exception;

namespace ICP.MailCenterFramework.UI
{
    /// <summary>
    /// Outlook 工具类
    /// </summary>
    public class OutlookUtility
    {
        #region 成员变量

        /// <summary>
        /// OutLook Application
        /// </summary>
        private static Application _Application;

        /// <summary>
        /// Outlook Message-ID标签值
        /// </summary>
        public static string OutlookMessageIDTag = "http://schemas.microsoft.com/mapi/proptag/0x1035001F";

        /// <summary>
        /// 自定义邮件Message-ID标签值
        /// </summary>
        public static string CustomMessageIDTag =
            "http://schemas.microsoft.com/mapi/string/{00020386-0000-0000-C000-000000000046}/Message-ID";

        /// <summary>
        /// 自定义邮件业务上下文标签值
        /// </summary>
        public static string OperationContextTag =
            "http://schemas.microsoft.com/mapi/string/{00020386-0000-0000-C000-000000000046}/X-OperationContext";

        /// <summary>
        /// 邮件标头中Reference中的标签值
        /// </summary>
        public static string ReferenceTag = "http://schemas.microsoft.com/mapi/proptag/0x1039001F";

        public static string MimeTag = "http://schemas.microsoft.com/mapi/proptag/0x370E001E";

        /// <summary>
        /// 内嵌图片Tag
        /// </summary>
        public static string EmbeddedImageTag = "http://schemas.microsoft.com/mapi/proptag/0x3712001F";

        /// <summary>
        ///  邮件应用程序入口
        /// </summary>
        public static NameSpace OLNameSpace = null;

        /// <summary>
        ///关联邮件保存目录 
        /// </summary>
        public static string EmailRelationSaveFolder = IsEnglish ? "IsArchived" : "已归档";
        /// <summary>
        /// 关联文件夹
        /// </summary>
        public static string[] ArchivingFolders = IsEnglish ?
            new[] { "Filter", "IsArchived", "NoBusiness" } :
            new[] { "筛选", "已归档", "无业务" };


        /// <summary>
        /// 是否包含cityocean邮箱域名的邮件
        /// </summary>
        public static string CityoceanDomain = "@cityocean.com";

        #region Outlook Folders
        public static string[] FixedFolders =
        {
            "搜索文件夹", "草稿", "发件箱", "垃圾邮件", "收件箱", "已发送邮件", 
            "search folders", "drafts", "outbox", "junk e-mail", "inbox", "sent items"
        };

        public static string[] OtherFolders =
        {
            "rss 源","已删除邮件","日历","日记","任务","联系人","便笺"
            , "rss feeds", "deleted items", "calendar", "journal" ,  "tasks", "contacts", "notes"
        };

        public static string[] OutBoxFolder = { "发件箱", "Outbox" };
        public static string[] InBoxFolder = { "收件箱", "Inbox" };
        public static string[] JunE_MailFolder = { "垃圾邮件", "Junk e-mail" };
        public static string[] DraftsFolders = { "草稿", "Drafts" };
        public static string[] SentFolders = { "已发送邮件", "Sent Items" };
        public static string[] SearchFolders = { "搜索文件夹", "Search Folders" };
        #endregion

        #region 属性-OutLook Application
        /// <summary>
        /// 当前Outlook Application
        /// </summary>
        public static Application CurrentApplication
        {
            get
            {
                try
                {
                    #region Outlook Application
                    try
                    {
                        //获取当前运行的Outlook Application
                        if (_Application == null)
                            _Application = Marshal.GetActiveObject("Outlook.Application") as Application;
                    }
                    catch
                    {
                        try
                        {
                            //获取当前正在运行的OutLook
                            _Application = new Application();
                        }
                        catch (Exception ex)
                        {
                            ToolUtility.WriteLog("New Application", ex);
                            ToolUtility.KillProcess("Outlook");
                        }
                    }
                    #endregion

                    #region Outlook NameSpace
                    if (_Application != null && OLNameSpace == null)
                        OLNameSpace = _Application.Session;
                    #endregion

                    #region Outlook OLCultrue
                    #endregion
                }
                catch (Exception ex)
                {
                    ToolUtility.WriteLog("CurrentApplication", ex);
                    _Application = null;
                }

                return _Application;
            }
            set { _Application = value; }
        } 
        #endregion

        #region 属性-OutLook NameSpace
        /// <summary>
        /// 当前Outlook NameSpace
        /// </summary>
        public static NameSpace CurrentNameSpace
        {
            get
            {
                NameSpace temp = null;
                try
                {
                    temp = CurrentApplication.Session;
                }
                catch (Exception ex)
                {
                    ToolUtility.WriteLog("CurrentNameSpace", ex);
                    temp = null;
                }
                return temp;
            }
        }
        #endregion

        #region 属性-Outlook Attachment
        //Attachment
        private static List<AttachmentContent> _MailAttachments;
        /// <summary>
        /// 当前附件集合List(AttachmentContent)
        /// </summary>
        public static List<AttachmentContent> MailAttachments
        {
            get
            {
                return _MailAttachments ?? (_MailAttachments = new List<AttachmentContent>());
            }
            set
            {
                _MailAttachments = value;
            }
        }
        #endregion

        #region 属性-国际化
        /// <summary>
        /// 国际化
        /// </summary>
        private static string culture = string.Empty;
        /// <summary>
        /// 国际化
        /// </summary>
        public static string OLCultrue
        {
            get
            {
                if (!string.IsNullOrEmpty(culture)) return culture;
                try
                {
                    LanguageSettings languageSettings = CurrentApplication.LanguageSettings;
                    int lcid = languageSettings.LanguageID[MsoAppLanguageID.msoLanguageIDUI];
                    CultureInfo officeUICulture = new CultureInfo(lcid);
                    culture = officeUICulture.Name.ToLower();
                    languageSettings = null;
                }
                catch (Exception ex)
                {
                    ToolUtility.WriteLog("OLCultrue", ex);
                    culture = string.Empty;
                }
                return culture;
            }
        }

        /// <summary>
        /// 是否英文
        /// </summary>
        public static bool IsEnglish
        {
            get
            {
                if (string.IsNullOrEmpty(culture))
                {
                    try
                    {
                        LanguageSettings languageSettings = CurrentApplication.LanguageSettings;
                        int lcid = languageSettings.LanguageID[MsoAppLanguageID.msoLanguageIDUI];
                        CultureInfo officeUICulture = new CultureInfo(lcid);
                        culture = officeUICulture.Name.ToLower();
                        languageSettings = null;
                    }
                    catch (Exception ex)
                    {
                        ToolUtility.WriteLog("IsEnglish", ex);
                        return true;
                    }
                }
                return !culture.Equals("zh-cn");
            }
        } 
        #endregion

        #endregion

        #region MessageID

        /// <summary>
        /// 获取邮件的ID
        /// </summary>
        /// <param name="pa">属性访问器</param>
        /// <param name="tagName">属性名称</param>
        /// <returns></returns>
        private static string GetMessageId(PropertyAccessor pa, string tagName)
        {
            string messageId = string.Empty;
            object messageIdProperty = null;
            try
            {
                //自定义Message-ID属性可能不存在，需要捕获此异常
                messageIdProperty = pa.GetProperty(tagName);
            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// 设置邮件的MessageID
        /// </summary>
        /// <param name="item">邮件对象</param>
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
                    pa.SetProperty(CustomMessageIDTag, GetMessageID());
                }
            }
        }

        /// <summary>
        /// 查询MessageID
        /// </summary>
        /// <param name="item">邮件对象</param>
        /// <returns></returns>
        public static string SearchMessageID(object item)
        {
            string createBy = string.Empty;
            return SearchMessageID(item,out createBy);
        }

        /// <summary>
        /// 查询MessageID
        /// </summary>
        /// <param name="item">邮件对象</param>
        /// <param name="createBy">创建人</param>
        /// <returns></returns>
        public static string SearchMessageID(object item, out string createBy)
        {
            createBy = "UnKnown";
            string strMessageID = string.Empty;
            object messageId = null;
            PropertyAccessor pa = null;
            try
            {
                if (item is _MailItem)
                {
                    _MailItem mailItem = item as _MailItem;
                    pa = mailItem.PropertyAccessor;
                }
                else if (item is ReportItem)
                {
                    ReportItem riItem = item as ReportItem;
                    pa = riItem.PropertyAccessor;
                }

                //使用mail head来获取邮件信息
                try
                {
                    if (pa != null)
                        messageId = pa.GetProperty(OutlookMessageIDTag);
                }
                catch
                {
                    messageId = null;
                }
                if (messageId != null && !string.IsNullOrEmpty(messageId.ToString()))
                {
                    strMessageID = messageId.ToString().ToLower().Trim();
                    createBy = "Auto";
                }
                else
                {
                    messageId = pa.GetProperty(CustomMessageIDTag);
                    if (messageId != null && !string.IsNullOrEmpty(messageId.ToString()))
                    {
                        strMessageID = messageId.ToString().ToLower().Trim();
                        createBy = "Custom";
                    }
                }
            }
            catch (System.Exception)
            {
                strMessageID = "";
            }
            finally
            {
                pa = null;
            }
            return strMessageID;
        }

        /// <summary>
        /// 获取MessageID
        /// </summary>
        /// <returns></returns>
        private static string GetMessageID()
        {
            string hostName = "@cityocean.com";
            try
            {
                int index = LocalData.UserInfo.EmailAddress.IndexOf("@", StringComparison.Ordinal);
                if (index >= 0)
                {
                    hostName = LocalData.UserInfo.EmailAddress.Substring(index);
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Get MessageID", ex);
            }
            return string.Format("<custom{0}>", string.Format("{0}{1}", Guid.NewGuid().ToString(), hostName).ToLower());
        }

        #endregion

        #region Check Relation

        /// <summary>
        /// 判断邮件是否关联业务
        /// </summary>
        /// <param name="olItem">邮件</param>
        /// <returns>Boolean:是否关联</returns>
        public static bool IsRelationOperation(object olItem)
        {
            string strMessage = string.Empty;
            return (IsRelationOperation(olItem, out strMessage));
        }
        /// <summary>
        /// 判断邮件是否关联业务
        /// </summary>
        /// <param name="olItem">邮件</param>
        /// <param name="strMessageID">MessageID</param>
        /// <returns>Boolean:是否关联</returns>
        public static bool IsRelationOperation(object olItem,out string strMessageID)
        {
            bool returnValue = true;
            strMessageID = string.Empty;
            try
            {
                //获取MessageID
                strMessageID = SearchMessageID(olItem);
                //从缓存文件查找关联信息
                OperationMessageController omc = new OperationMessageController();
                //根据Mail MessageID 查询
                List<OperationMessageRelation> dtResult = omc.GetOperationMessageRelationByMessageIdAndReference(strMessageID, string.Empty);
                if (dtResult == null || dtResult.Count <= 0) //未找到则根据Mail Reference查询
                {
                    PropertyAccessor propertyAccessor = null;
                    if (olItem is _MailItem)
                    {
                        _MailItem mailItem = olItem as _MailItem;
                        propertyAccessor = mailItem.PropertyAccessor;
                    }
                    else if (olItem is ReportItem)
                    {
                        ReportItem riItem = olItem as ReportItem;
                        propertyAccessor = riItem.PropertyAccessor;
                    }
                    if (propertyAccessor != null)
                    {
                        MessageUserPropertiesObject mupo=GetUserPropertiesOjbect(propertyAccessor);
                        if (!string.IsNullOrEmpty(mupo.Reference))
                        {
                            dtResult = omc.GetOperationMessageRelationByMessageIdAndReference(string.Empty, mupo.Reference);
                        }
                    }
                }
                if (dtResult == null || dtResult.Count <= 0)
                {
                    returnValue = false;
                }
            }
            catch (Exception ex)
            {
                returnValue = false;
            }
            return returnValue;
        }
        #endregion

        #region Save MailItem
        /// <summary>
        /// 以指定路径保存MailItem对象到磁盘MSG文件
        /// </summary>
        /// <param name="mailItem"></param>
        /// <param name="savePath"></param>
        public static void SaveMailItemAs(object mailItem, string savePath)
        {
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            MailItem _mailItem = mailItem as MailItem;
            if (_mailItem != null)
            {
                _mailItem.SaveAs(savePath, OlSaveAsType.olMSG);
            }
        }
        #endregion

        #region MessageUserPropertiesObject
        /// <summary>
        /// 获取邮件自定义信息
        /// </summary>
        /// <param name="pa">属性访问器</param>
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
            catch (Exception ex)
            {
            }
            finally
            {
                //获取邮件Reference
                object objReference = pa.GetProperty(ReferenceTag);
                if (objReference != null && (string)objReference != "")
                {
                    if (propertiesObject == null)
                        propertiesObject = new MessageUserPropertiesObject() { OperationId = Guid.Empty, OperationType = OperationType.Unknown };
                    string[] references = objReference.ToString().Split(new char[1] { '>' });
                    if (references.Length > 0)
                    {
                        propertiesObject.Reference = string.Format("{0}>", references[0]);
                    }
                }

                pa = null;
            }

            return propertiesObject;
        } 
        #endregion

        #region Mail Folder
        /// <summary>
        /// 设置文件夹显示图标
        /// </summary>
        /// <param name="lower_FolderName"></param>
        /// <returns></returns>
        public static int GetImageIndex(string lower_FolderName)
        {
            if (OtherFolders.Any(item => item.ToLower().Equals(lower_FolderName)))
                return -1;
            else if (FixedFolders.Any(item => item.ToLower().Equals(lower_FolderName)))
            {
                if (InBoxFolder.Any(item => item.ToLower().Equals(lower_FolderName))) //收件箱
                {
                    return 1;
                }
                else if (DraftsFolders.Any(item => item.ToLower().Equals(lower_FolderName))) //草稿箱
                {
                    return 2;
                }
                else if (OutBoxFolder.Any(item => item.ToLower().Equals(lower_FolderName))) //已发送邮件
                {
                    return 3;
                }
                else if (SentFolders.Any(item => item.ToLower().Equals(lower_FolderName))) //发件箱
                {
                    return 5;
                }
                else if (JunE_MailFolder.Any(item => item.ToLower().Equals(lower_FolderName))) //垃圾邮件
                {
                    return 6;
                }
            }
            else
            {
                //用户文件夹:根路径或新建文件夹
                if (CurrentApplication.Session.DefaultStore.GetRootFolder().Name.ToLower().Equals(lower_FolderName))
                    return 0;
            }
            return 7;
        }

        /// <summary>
        /// 获取归档文件夹目录：不存在则添加
        /// </summary>
        /// <param name="selectStore">选中数据源</param>
        /// <param name="archivingType">归档类型</param>
        /// <param name="isSend">是否发送</param>
        /// <param name="isDefaultStore">是否默认Store</param>
        /// <returns>返回值：outlook.MAPIFolder;1.新增成功返回新增的文件夹对象2.失败返回null</returns>
        public static MAPIFolder GetArchivingFolder(Store selectStore, ArchivingType archivingType, bool isSend, bool isDefaultStore)
        {
            if (selectStore == null)
                return null;

            MAPIFolder newFolder = null;  //关联文件夹
            MAPIFolder findFolder = null; //关联邮件保存文件夹上级目录
            string newFolderName = isSend ? (IsEnglish ? SentFolders[1] : SentFolders[0]) : (IsEnglish ? InBoxFolder[1] : InBoxFolder[0]);  //关联文件夹名称
            try
            {
                if (isDefaultStore)
                {
                    //默认收件箱
                    findFolder = selectStore.Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                    if (isSend) //发件箱
                    {
                        //获取发件箱
                        findFolder = selectStore.Session.GetDefaultFolder(OlDefaultFolders.olFolderSentMail);
                    }
                }
                else
                {
                    //获取根文件夹
                    MAPIFolder RootFolder = selectStore.GetRootFolder();
                    //关联文件夹上级目录
                    findFolder = selectStore.GetRootFolder().Folders.OfType<MAPIFolder>()
                            .SingleOrDefault(item => item.Name.Equals(newFolderName)) ??
                        RootFolder.Folders.Add(newFolderName, Type.Missing) as Folder;
                }
                //关联文件夹上级目录存在
                if (findFolder != null)
                {
                    newFolder =
                        findFolder.Folders.OfType<MAPIFolder>()
                            .SingleOrDefault(item => item.Name.Equals(ArchivingFolders[archivingType.GetHashCode()])) ??
                        findFolder.Folders.Add(ArchivingFolders[archivingType.GetHashCode()], Type.Missing) as Folder;
                }
            }
            catch
            {
                newFolder = null;
                findFolder = null;
                newFolderName = string.Empty;
            }
            return newFolder;
        }

        #endregion

        #region Mail Body

        /// <summary>
        /// 解析邮件的主体
        /// 1.将邮件中的图片保存到本地，并将邮件中的图片路径替换为本地路径
        /// 2.抽取邮件中真正的附件，剔除邮件背景图片附件
        /// </summary>
        /// <param name="attachments"></param>
        /// <param name="htmlBody"></param>
        /// <returns></returns>
        public static string FixHtmlBody(Attachments attachments, string htmlBody)
        {
            string body = htmlBody;
            Attachments _attachments = attachments;
            foreach (Attachment att in _attachments)
            {
                string fileName = GetAttachmentName(att);

                //http://schemas.microsoft.com/mapi/proptag/0x3712001F
                //http://schemas.microsoft.com/mapi/proptag/0x3704001F
                //http://schemas.microsoft.com/mapi/proptag/0x3707001F                
                //查询图片contentID
                string emCID = att.PropertyAccessor.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x3712001F").ToString();

                if (!string.IsNullOrEmpty(emCID))
                {
                    //当可以查询到图片contentID，却没有要替换htmlBody中的字符，则该文件就是附件
                    bool isReplaceSign = false;
                    string fullFilePath = GetFileFullPath(fileName);
                    SaveAsLocalFile(att, fullFilePath);
                    //将平铺的背景图片替换
                    body = ReplaceHtmlBodyBackgroundImage(body, emCID, ref isReplaceSign);
                    //将图片路径图换掉
                    body = ReplaceHtmlBodyImagePathWithLocalPath(emCID, body, fullFilePath, ref isReplaceSign);

                    if (!isReplaceSign)
                    {
                        AttachmentContent attachmentInfo = AddAttachmentToList(att, body);
                        if (attachmentInfo != null)
                        {
                            MailAttachments.Add(attachmentInfo);
                        }
                    }
                }
                else
                {
                    //特殊处理： 把Attachment为Image的文件类型去判断HtmlBody中是否包含该图片，如果不包含，则添加到集合;否则表示是真正的附件
                    AttachmentContent attachmentInfo = AddAttachmentToList(att, body);
                    if (attachmentInfo != null)
                    {
                        MailAttachments.Add(attachmentInfo);
                    }
                }
            }
            ToolUtility.ReleaseComObject(_attachments);

            return body;
        }

        /// <summary>
        /// 获取附件名称
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        private static string GetAttachmentName(Attachment attachment)
        {
            string fileName = string.Empty;
            if (attachment.Type.Equals(OlAttachmentType.olOLE))
                fileName = attachment.DisplayName;
            else
                fileName = attachment.FileName;
            return fileName;
        }

        /// <summary>
        /// 得到文件完整路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string GetFileFullPath(string fileName)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempPath);
            return Path.Combine(tempPath, fileName);
        }

        /// <summary>
        /// 将附件保存到本地磁盘
        /// </summary>
        /// <param name="att"></param>
        /// <param name="fullFilePath"></param>
        /// <returns></returns>
        private static string SaveAsLocalFile(Attachment att, string fullFilePath)
        {
            try
            {
                //将附件保存到本地
                att.SaveAsFile(fullFilePath);
            }
            //如果出现 “尝试读取或写入受保护的内存。这通常指示其他内存已损坏”异常，则文件名称重命名为 FileName(1).extension
            catch (System.AccessViolationException ave)
            {
                try
                {
                    string extension = Path.GetExtension(fullFilePath);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullFilePath);

                    string fileName = string.Format("{0}(1){1}", fileNameWithoutExtension, extension);
                    fullFilePath = Path.Combine(Path.GetDirectoryName(fullFilePath), fileName);
                    att.SaveAsFile(fullFilePath);
                }
                catch (System.Exception ex)
                {
                    throw;
                }
            }

            return fullFilePath;
        }

        /// <summary>
        /// 替换当前HTMLBody背景图片
        /// </summary>
        /// <param name="htmlBody"></param>
        /// <param name="contentID"></param>
        /// <param name="isReplaceSign"></param>
        /// <returns></returns>
        private static string ReplaceHtmlBodyBackgroundImage(string htmlBody, string contentID, ref bool isReplaceSign)
        {
            string body = htmlBody;
            // “background=” 代表背景图片
            if (body.Contains("background="))
            {
                string pattern = string.Format("background=(\")?(cid:)?{0}(\")?", contentID);
                body = Regex.Replace(htmlBody, pattern, "", RegexOptions.IgnoreCase);
                isReplaceSign = true;
            }
            return body;
        }

        /// <summary>
        /// 将htmlbody中图片路径替换
        /// </summary>
        /// <param name="contentID"></param>
        /// <param name="htmlBody"></param>
        /// <param name="fullFilePath"></param>
        /// <param name="isReplaceSign"></param>
        /// <returns></returns>
        private static string ReplaceHtmlBodyImagePathWithLocalPath(string contentID, string htmlBody, string fullFilePath, ref bool isReplaceSign)
        {
            if (htmlBody.Contains(contentID))
            {
                string pattern = string.Format("cid:(\")?{0}(\")?", contentID);
                htmlBody = Regex.Replace(htmlBody, pattern, fullFilePath + "\"", RegexOptions.IgnoreCase);
                isReplaceSign = true;
            }
            return htmlBody;
        }

        /// <summary>
        /// 添加附件到列表
        /// </summary>
        /// <param name="att"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private static AttachmentContent AddAttachmentToList(Attachment att, string body)
        {
            object objMime = att.PropertyAccessor.GetProperty(MimeTag);

            string emMime = objMime == null ? "" : objMime.ToString();
            if (emMime.ToLower().Contains("image"))
            {
                if (!body.Contains(att.FileName))
                {
                    return CreateAttachmentContentInfo(att);
                }
            }
            else
            {
                //将真正的附件添加到集合中
                return CreateAttachmentContentInfo(att);
            }

            return null;
        }
        /// <summary>
        /// 将附件转换成实体类
        /// </summary>
        /// <param name="att"></param>
        /// <returns></returns>
        public static AttachmentContent CreateAttachmentContentInfo(Attachment att)
        {
            return new AttachmentContent()
            {
                OLAttachment = att,
                Name = GetFileName(att),
                DisplayName = att.DisplayName ?? string.Empty,
                Size = att.Size,
                UploadTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified)
            };
        }
        private static string GetFileName(Attachment att)
        {
            string fileName = string.Empty;
            try
            {
                fileName = att.FileName;
            }
            catch (System.Exception ex)
            {
                fileName = att.DisplayName;
            }

            return fileName;
        }
        #endregion

        #region Mail Attachment

        /// <summary>
        /// 判断是否包含附件
        /// </summary>
        /// <param name="pAttachments">附件集合</param>
        /// <returns>bool:true(包含)false(无)</returns>
        public static bool IsContainAttachments(Attachments pAttachments)
        {
            bool isContain = false;
            try
            {
                if (pAttachments != null)
                {
                    //非内嵌图片则为包含附件
                    if ((from Attachment att in pAttachments
                         select att.PropertyAccessor.GetProperty(EmbeddedImageTag).ToString()).Any(string.IsNullOrEmpty))
                    {
                        isContain = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("IsContainAttachments", ex);
            }
            return isContain;
        }

        /// <summary>
        /// 获取附件
        /// </summary>
        /// <param name="pAttachments">附件集合</param>
        /// <returns></returns>
        public static List<AttachmentContent> GetAttachmentContents(Attachments pAttachments)
        {
            List<AttachmentContent> contents = new List<AttachmentContent>();

            DateTime now = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            for (int i = 1; i <= pAttachments.Count; i++)
            {
                Attachment currentAttachment = pAttachments[i];
                //非内嵌图片
                string contentID = currentAttachment.PropertyAccessor.GetProperty(EmbeddedImageTag).ToString();
                if (string.IsNullOrEmpty(contentID))
                {
                    string fullFilePath =ICPPathUtility.TempPathFileName(currentAttachment.FileName);
                    try
                    {
                        if (File.Exists(fullFilePath))
                        {
                            File.Delete(fullFilePath);
                        }
                        currentAttachment.SaveAsFile(fullFilePath);
                        ToolUtility.WriteLog("Utility GetAttachmentContents(Attachments)", new Exception(fullFilePath));
                    }
                    catch(Exception ex)
                    {
                        ToolUtility.WriteLog("Utility GetAttachmentContents(Attachments)", ex);
                    }
                    finally
                    {
                        contents.Add(new AttachmentContent { Name = currentAttachment.FileName, Content = ToolUtility.ReadFileContentFromDisk(fullFilePath), UploadTime = now, ClientPath = fullFilePath });
                        currentAttachment = null;
                    }
                }
            }
            if (pAttachments.Count > 0 && contents.Count <= 0)
            {
                //WriteLog("Utility GetAttachmentContents(Attachments)", new Exception("Null Attachment"));
                return new List<AttachmentContent>();
            }
            pAttachments = null;
            return contents;
        }

        /// <summary>
        /// 获取附件
        /// </summary>
        /// <param name="mail">邮件对象</param>
        /// <param name="isRename">是否重命名</param>
        /// <returns></returns>
        public static List<AttachmentContent> GetAttachmentContentsByMailItem(_MailItem mail,bool isRename=true)
        {
            if (mail == null)
                return new List<AttachmentContent>();
            List<AttachmentContent> contents = new List<AttachmentContent>();
            Attachments mailAttachments = mail.Attachments;

            DateTime now = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            for (int i = 1; i <= mailAttachments.Count; i++)
            {
                Attachment currentAttachment = mailAttachments[i];
                bool IsAttachment = false;
                string contentID = currentAttachment.PropertyAccessor.GetProperty(EmbeddedImageTag).ToString();
                if (string.IsNullOrEmpty(contentID))
                {
                    IsAttachment = true;
                }
                else
                {
                    if ((!mail.HTMLBody.Contains("background=")) && (!mail.HTMLBody.Contains(contentID)))
                    {
                        object objMime = currentAttachment.PropertyAccessor.GetProperty(MimeTag);
                        string emMime = objMime == null ? "" : objMime.ToString();

                        if (emMime.ToLower().Contains("image"))
                        {
                            if (!mail.HTMLBody.Contains(currentAttachment.FileName))
                            {
                                IsAttachment = true;
                            }
                        }
                        else
                            IsAttachment = true;
                    }
                }
                if (!IsAttachment) continue;
                string attachmentName = currentAttachment.FileName;
                if (isRename)
                {
                    attachmentName = Path.GetFileNameWithoutExtension(attachmentName).ReplaceSpecialCharacters("_")+attachmentName.GetExtension();
                }
                string fullFilePath = ICPPathUtility.TempPathFileName(attachmentName);
                try
                {
                    if (File.Exists(fullFilePath))
                    {
                        File.Delete(fullFilePath);
                    }
                    currentAttachment.SaveAsFile(fullFilePath);
                }
                catch (Exception ex)
                {
                    ToolUtility.WriteLog("Utility GetAttachmentContents(_MailItem,bool)", ex);
                }
                finally
                {
                    contents.Add(new AttachmentContent
                    {
                        Name = attachmentName,
                        Content = ToolUtility.ReadFileContentFromDisk(fullFilePath),
                        UploadTime = now,
                        ClientPath = fullFilePath
                    });
                    currentAttachment = null;
                }
            }
            if (mailAttachments != null && mailAttachments.Count > 0 && contents.Count <= 0)
            {
                return new List<AttachmentContent>();
            }
            mailAttachments = null;
            
            return contents;
        }

        

        #endregion

        #region Clone Message Entry(克隆Message实体)
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
            return ToolUtility.Clone(messageInfo);
        }
        #endregion

        #region Mail Item To Message

        /// <summary>
        /// 将Mail Item转换为Message
        /// </summary>
        /// <param name="mail">Mail Item</param>
        /// <returns></returns>
        public static Message.ServiceInterface.Message ConvertMailItemToMessageInfo(MailItem mail)
        {
            return ConvertMailItemInfo(mail);
        }
        /// <summary>
        /// 将MailItem转换成Message实体对象
        /// </summary>
        /// <param name="mail">Mail Item</param>
        /// <returns></returns>
        private static Message.ServiceInterface.Message ConvertMailItemInfo(MailItem mail)
        {
            Message.ServiceInterface.Message entry = Message.ServiceInterface.Message.CreateInstance();
            try
            {
                //未发送邮件及其草稿箱邮件不转换成Message，即不可关联
                if (!mail.Sent || "4501/1/1 0:00:00".Equals("" + mail.SentOn))
                    entry = null;
                else
                {
                    entry.Id = Guid.NewGuid();
                    entry.IsAssociated = false;
                    entry.Body = mail.HTMLBody ?? "";
                    entry.BodyFormat = BodyFormat.olFormatHTML;
                    bool useOriginalSenderAddress = false;
                    Account _account = null;
                    try
                    {
                        _account = mail.SendUsingAccount;
                    }
                    catch
                    {
                        _account = null;
                    }
                    if (_account != null)
                        useOriginalSenderAddress = mail.SenderEmailAddress.Equals(_account.SmtpAddress);
                    if (useOriginalSenderAddress)
                    {
                        //账户邮件地址
                        entry.SendFrom = _account.SmtpAddress;
                        entry.FromName = _account.UserName;
                        entry.Way = MessageWay.Send;
                    }
                    else
                    {
                        entry.SendFrom = mail.SenderEmailAddress;
                        if (string.IsNullOrEmpty(mail.SenderName))
                        {
                            entry.FromName = entry.SendFrom.Contains('@') ? entry.SendFrom.Substring(0, entry.SendFrom.IndexOf('@')) : entry.SendFrom;
                        }
                        else
                            entry.FromName = mail.SenderName;
                        entry.ToName = mail.ReceivedByName;
                        entry.Way = MessageWay.Receive;
                    }
                    
                    entry.State = mail.Sent ? MessageState.Success : MessageState.Draft;
                    entry.Type = MessageType.Email;
                    entry.Subject = mail.Subject ?? "";
                    PropertyAccessor propertyAccessor = mail.PropertyAccessor;
                    entry.MessageId = GetMessageId(propertyAccessor, OutlookMessageIDTag);
                    ////if (string.IsNullOrEmpty(entry.MessageId))
                    ////    throw new Exception("MessageID is null\r\nSubject:" + entry.Subject);
                    entry.UserProperties = GetUserPropertiesOjbect(propertyAccessor);

                    entry.CreateBy = LocalData.UserInfo.LoginID;
                    entry.CreatorName = mail.SenderName;
                    entry.ReceivingTime = mail.ReceivedTime;
                    entry.Sendtime = mail.SentOn;
                    entry.SentDateTimeZone = ConvertDateTimeOffset("SentOn", mail.SentOn);
                    if (entry.SentDateTimeZone == DateTimeOffset.MinValue)
                        entry.SentDateTimeZone = ConvertDateTimeOffset("Sendtime", entry.Sendtime.Value);
                    entry.EntryID = mail.EntryID;
                    entry.Size = mail.Size;
                    //保存消息到数据库，需要获取真正的邮件地址
                    string toNameList = string.Empty;
                    entry.SendTo = ConvertRecipientsToString(mail.Recipients, (int)OlMailRecipientType.olTo, out toNameList);
                    entry.ToName = toNameList;

                    string ccNameList = string.Empty;
                    entry.CC = ConvertRecipientsToString(mail.Recipients, (int)OlMailRecipientType.olCC, out ccNameList);
                    entry.CCName = ccNameList;

                    string bccNameList = string.Empty;
                    entry.BCC = ConvertRecipientsToString(mail.Recipients, (int)OlMailRecipientType.olBCC, out bccNameList);
                    entry.BCCName = bccNameList;
                    entry.HasAttachment = IsContainAttachments(mail.Attachments);
                    //转换邮件临时实体类
                    entry.CreateDate = mail.ReceivedTime;
                    //需备份邮件帐户
                    //包含cityocean.com的邮件在国内服务器已备份
                    entry.BackupMailState = string.Format("{0};{1};{2}", entry.SendFrom, entry.SendTo, entry.CC).Contains("cityocean.com");
                }
            }
            catch (Exception ex)
            {
                entry = null;
            }
            return entry;
        }

        public static DateTimeOffset ConvertDateTimeOffset(string FeildName,DateTime dtTime)
        {
            DateTimeOffset dtos = new DateTimeOffset();
            try
            {
                dtos = dtTime.ToDateTimeOffset();
            }
            catch (Exception ex)
            {
                dtos = DateTimeOffset.MinValue;
            }
            return dtos;
        }

        /// <summary>
        /// 将邮件接收人转换成string字符串，以 ‘;’分割
        /// </summary>
        /// <param name="olRecipients"></param>
        /// <param name="type">类型:发件人为0，接收人为1，CC为2</param>
        /// <param name="nameList">发件人或接收人的名称</param>
        /// <returns></returns>
        public static string ConvertRecipientsToString(Recipients olRecipients, int type, out string nameList)
        {
            StringBuilder strAddress = new StringBuilder();
            StringBuilder strNames = new StringBuilder();
            try
            {
                int count = olRecipients.Count;
                for (int i = 1; i <= count; i++)
                {
                    if (olRecipients[i].Type == type)
                    {
                        string address = !string.IsNullOrEmpty(olRecipients[i].Address) ? olRecipients[i].Address : olRecipients[i].Name;
                        strAddress.Append(string.Format("{0};", address));
                        strNames.Append(string.Format("{0};", olRecipients[i].Name));
                    }
                }
            }
            finally
            {
                ToolUtility.ReleaseComObject(olRecipients);
            }


            if (strAddress.Length > 0)
            {
                nameList = strNames.ToString(0, strNames.Length - 1);
                return strAddress.ToString(0, strAddress.Length - 1);
            }
            else
            {
                nameList = string.Empty;
                return string.Empty;
            }
        }
        /// <summary>
        /// 是否使用原始发件人地址
        /// </summary>
        /// <param name="mailItem">邮件项</param>
        /// <returns>Boolean:true为发件;false为收件</returns>
        public static bool IsUseOriginalSenderAddress(MailItem mailItem)
        {
            bool returnValue = false;
            Account _account = null;
            try
            {
                _account = mailItem.SendUsingAccount;
            }
            catch
            {
                _account = null;
            }
            if (_account != null)
                returnValue = mailItem.SenderEmailAddress.Equals(_account.SmtpAddress);
            return returnValue;
        }
        #endregion

        #region Report Item To Message
        /// <summary>
        /// 将ReportItem（回执）转换成Message实体对象
        /// </summary>
        /// <param name="reportItem"></param>
        /// <returns></returns>
        public static Message.ServiceInterface.Message ConvertReportItemToMessageInfo(_ReportItem reportItem)
        {
            Message.ServiceInterface.Message info; string arrTo = string.Empty, arrCC = string.Empty, senderName = string.Empty, senderEmailAddress = string.Empty; DateTime sentOn;

            try
            {
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
                info = new Message.ServiceInterface.Message()
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
                info.SentDateTimeZone = sentOn.ToDateTimeOffset();
                info.EntryID = reportItem.EntryID;

                ToolUtility.ReleaseComObject(olPA);
            }
            catch (Exception ex)
            {
                info = null;
                ToolUtility.WriteLog("ConvertReportItemToMessageInfo", ex);
            }

            return info;
        } 
        #endregion

        #region 将Message实体中一些不需要的属性值过滤，减少网络带宽
        /// <summary>
        /// 将Message实体中一些不需要的属性值过滤，减少网络带宽
        /// </summary>
        /// <param name="messageInfo">Message(Entry)</param>
        /// <returns></returns>
        public static Message.ServiceInterface.Message GetMessageInfo(Message.ServiceInterface.Message messageInfo)
        {
            if (messageInfo != null)
            {
                //不需要保存事件代码了，通过邮件模板发送邮件的时候就记录了该邮件关联的业务的事件代码
                if (messageInfo.UserProperties != null && messageInfo.UserProperties.EventInfo != null)
                { messageInfo.UserProperties.EventInfo.Code = "Nothing"; }
                //默认保存邮件的状态是发送成功的
                messageInfo.State = MessageState.Success;
            }
            return null;
        } 
        #endregion

        #region 是否包含Cityocean域名的邮件
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
        #endregion

        #region 将选择的邮件的每个附件都映射本地两个路径
        /// <summary>
        /// 将选择的邮件的每个附件都映射本地两个路径（LocalPath，PreviewPath）
        /// </summary>
        /// <returns></returns>
        public static List<AttachmentContent> GetRealAttachmentList()
        {
            for (int i = 0; i < MailAttachments.Count; i++)
            {
                AttachmentContent attachmentInfo = MailAttachments[i];
                if (attachmentInfo != null && attachmentInfo.OLAttachment != null)
                {
                    string fullPath = GetFileFullPath(attachmentInfo.Name);
                    SaveAsLocalFile(attachmentInfo.OLAttachment as Attachment, fullPath);
                    attachmentInfo.ClientPath = fullPath;

                    if (string.IsNullOrEmpty(attachmentInfo.PreviewPath))
                    {
                        string previewPath = GetFileFullPath(attachmentInfo.Name);
                        SaveAsLocalFile(attachmentInfo.OLAttachment as Attachment, previewPath);
                        attachmentInfo.PreviewPath = previewPath;
                    }

                    attachmentInfo.OLAttachment = null;
                }
            }
            return MailAttachments;
        } 
        #endregion

        #region 从邮件主题获取单号
        /// <summary>
        /// 从邮件主题获取单号
        /// </summary>
        public static string regexExpression = "[a-zA-Z0-9]{8,30}";
        public static List<string> MatchArray(string arrText)
        {
            return arrText.MatchUseRegex(regexExpression, true);
        }
        #endregion

        #region 保存Message
        public static void SaveMessage(Message.ServiceInterface.Message messageItem)
        {
            ServiceClient.GetClientService<IClientBusinessOperationService>().SaveMessage(messageItem);
        } 
        #endregion

        #region 写日志
        public static long GetObjectSize(object o)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, o);
                using (var fileStream = new FileStream(@"D:\test.txt", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var buffer = stream.ToArray();
                    fileStream.Write(buffer, 0, buffer.Length);
                    fileStream.Flush();
                }

                return stream.Length;
            }
        }
        
        #endregion  
    }
}
