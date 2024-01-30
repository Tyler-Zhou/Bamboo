using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Aspose.Pdf.Generator;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Helper;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Office.Interop.Outlook;
using ICP.Message.ServiceInterface;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using ICP.Common.ServiceInterface;
using Aspose.Email.Mail;
using Aspose.Email.Outlook;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using System.Collections.Generic;
using System.Linq;
using ICP.MailCenterFramework.ServiceInterface;
using ICP.MailCenterFramework.UI;
using ApplicationContext = ICP.Framework.CommonLibrary.Common.ApplicationContext;
using Attachment = Aspose.Email.Mail.Attachment;
using Exception = System.Exception;
using Logger = ICP.Framework.CommonLibrary.Logger;
using ICP.FileSystem.ServiceInterface;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// Outlook服务类
    /// </summary>
    /// 
    public class OutlookService : IOutLookService
    {
        #region 成员变量
        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        }
        public IMessageService MessageService
        {
            get
            {
                return ServiceClient.GetService<IMessageService>();
            }
        }

        /// <summary>
        /// FCM公共服务
        /// </summary>
        public IFCMCommonService FcmCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        /// <summary>
        /// 业务查询接口
        /// </summary>
        public IBusinessQueryService BusinessQueryServices
        {
            get
            {
                return ServiceClient.GetService<IBusinessQueryService>();
            }
        }

        private IOutlookOperateService _OutlookOperateService;
        /// <summary>
        /// Outlook Operation Service
        /// </summary>
        public IOutlookOperateService OutlookOperateService
        {
            get
            {
                if (_OutlookOperateService != null)
                    return _OutlookOperateService;
                _OutlookOperateService = ServiceClient.GetService<IOutlookOperateService>() ?? new OutlookOperateService();
                return _OutlookOperateService;
            }
        }
        #endregion

        /// <summary>
        /// 打开邮件
        /// </summary>
        /// <param name="item">邮件对象</param>
        public void Open(object item)
        {
            if (item is MailItem)
            {
                MailItem mailItem = item as MailItem;
                mailItem.Display(false);
            }
        }

        /// <summary>
        /// 如果是没有发送出去的邮件，则直接打开原编辑窗口，否则就回复
        /// </summary>
        /// <param name="item"></param>
        public void ReplyToSender(object item)
        {
            if (item is MailItem)
            {
                MailItem mailItem = item as MailItem;
                if (mailItem.Sent)
                {
                    MailItem _Item = mailItem.Reply();
                    ClientUtility.SetMessageID(_Item);
                    _Item.Display(false);
                    if (string.IsNullOrEmpty(_Item.Body) || _Item.Body == " ")
                    {
                        _Item.Close(OlInspectorClose.olDiscard);
                        _Item.Display(false);
                    }
                }
                else
                    mailItem.Display(false);
            }
        }

        /// <summary>
        /// 如果是没有发送出去的邮件，则直接打开原编辑窗口，否则就回复所有人
        /// </summary>
        /// <param name="item"></param>
        public void ReplyToAll(object item)
        {
            if (item is MailItem)
            {
                MailItem mailItem = item as MailItem;
                if (mailItem.Sent)
                {
                    MailItem _Item = mailItem.ReplyAll();
                    ClientUtility.SetMessageID(_Item);
                    _Item.Display(false);
                    if (string.IsNullOrEmpty(_Item.Body) || _Item.Body == " ")
                    {
                        _Item.Close(OlInspectorClose.olDiscard);
                        _Item.Display(false);
                    }
                }
                else
                    mailItem.Display(false);
            }
        }

        /// <summary>
        /// 如果是没有发送出去的邮件，则直接打开原编辑窗口，否则就转发
        /// </summary>
        /// <param name="item"></param>
        public void Forward(object item)
        {
            if (item is MailItem)
            {
                MailItem mailItem = item as MailItem;
                if (mailItem.Sent)
                {
                    MailItem _Item = mailItem.Forward();
                    ClientUtility.SetMessageID(_Item);
                    _Item.Display(false);
                    if (string.IsNullOrEmpty(_Item.Body) || _Item.Body == " ")
                    {
                        _Item.Close(OlInspectorClose.olDiscard);
                        _Item.Display(false);
                    }
                }
                else
                    mailItem.Display(false);
            }
        }

        /// <summary>
        /// 通过文件路径查找邮件转换成MailItem对象
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>MailItem对象</returns>
        public object GetMailItemByFilePath(string filePath)
        {
            object mailItem = null;
            try
            {
                mailItem = ClientUtility.OLNameSpace.OpenSharedItem(filePath);
            }
            catch (Exception ex)
            {
                mailItem = null;
                Logger.Log.Info(ex.StackTrace);
            }
            finally
            {
                ClientUtility.OLApplication = null;
            }
            return mailItem;
        }

        /// <summary>
        /// 转换Message到MailItem对象
        /// </summary>
        /// <param name="message">Message对象</param>
        /// <returns>MailItem对象</returns>
        public string GetMailItemByMessage(Message.ServiceInterface.Message message)
        {
            string mFilePath = string.Empty;
            try
            {
                mFilePath = ConvertMessage2Msg(message, false);
            }
            catch (Exception ex)
            {
                mFilePath = string.Empty;
                Logger.Log.Info(ex.StackTrace);
            }
            finally
            {
                ClientUtility.OLApplication = null;
            }
            return mFilePath;
        }

        /// <summary>
        /// 通过EntryID获取MailItem对象
        /// </summary>
        /// <param name="paramEntryID">EntryID</param>
        /// <returns>MailItem对象：未找到邮件返回空</returns>
        public object GetMailItemByEntryID(string paramEntryID)
        {
            object mailItem = null;
            try
            {
                if (!string.IsNullOrEmpty(paramEntryID))
                {
                    mailItem = ClientUtility.OLNameSpace.GetItemFromID(paramEntryID);
                }
            }
            catch (Exception ex)
            {
                mailItem = null;
            }
            return mailItem;
        }

        /// <summary>
        /// 通过MessageID查询邮件
        /// </summary>
        /// <param name="strMessageID">MessageID</param>
        /// <returns>MailItem</returns>
        public MailItem GetMailItemByMessageID(string strMessageID)
        {
            MailItem mailItem = null;
            try
            {
                OutlookAdvancedSearch oas = new OutlookAdvancedSearch(strMessageID);
                oas.RunAdvancedSearch();
                if (oas.SearchResults != null && oas.SearchResults.Count > 0)
                {
                    mailItem = oas.SearchResults[0] as MailItem;
                }
                oas = null;
            }
            catch (Exception ex)
            {
                Logger.Log.Info(ex.StackTrace);
            }
            return mailItem;
        }

        /// <summary>
        /// 通过MessageID查询并转换成Byte[]
        /// </summary>
        /// <param name="strEntryID">Mail EntryID(GUID)</param>
        /// <param name="strMessageID">MessageID</param>
        /// <param name="strIMessageID">IMessageID</param>
        /// <returns>byte[]</returns>
        public byte[] GetByteByMessageID(string strEntryID, string strMessageID, string strIMessageID)
        {
            byte[] mailByte = null;
            MailItem mailItem = null;
            string saveAsPath = ClientUtility.GetFileFullPath((string.IsNullOrEmpty(strEntryID) ? strIMessageID : strEntryID).ToLower() + ".msg");
            try
            {
                if (!File.Exists(saveAsPath))
                {
                    //确保Outlook启动
                    if (!IsAppExists("OUTLOOK.EXE"))
                        ClientHelper.EnsureEmailCenterAppStarted();
                    if (!string.IsNullOrEmpty(strEntryID))
                        mailItem = GetMailItemByEntryID(strEntryID) as MailItem;
                    if (mailItem == null && !string.IsNullOrEmpty(strMessageID))
                        mailItem = GetMailItemByMessageID(strMessageID);
                    if (mailItem != null)
                        MailUtility.SaveMailItemAs(mailItem, saveAsPath);
                }
                if (File.Exists(saveAsPath))
                    mailByte = MailUtility.ReadFileContentFromDisk(saveAsPath);
                
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog(CommonHelper.BuildExceptionString(ex));
            }
            finally
            {
                DeleteMsgFile(saveAsPath);
            }
            return mailByte;
        }

        /// <summary>
        /// 获取邮件另存目录
        /// </summary>
        /// <param name="strEntryID">邮件EntryID</param>
        /// <param name="strMessageID">邮件MessageID</param>
        /// <param name="strIMessageID">消息Guid</param>
        /// <returns>另存目录</returns>
        public string GetMailItemSaveAsPath(string strEntryID, string strMessageID, string strIMessageID)
        {
            string saveAsPath = ICPPathUtility.CombineDirectory4FileName(ICPPathUtility.TempPathMailEntity(), (string.IsNullOrEmpty(strEntryID) ? strIMessageID : strEntryID).ToLower() + ".msg");
            if (!File.Exists(saveAsPath))
            {
                return OutlookOperateService.GetMailItemSaveAsPath(strEntryID, strMessageID, strIMessageID);
            }
            return saveAsPath;
        }

        private void DeleteMsgFile(string strPath)
        {
            try
            {
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 通过进程打开文件
        /// </summary>
        /// <param name="strPath">文件路径</param>
        public void OpenMailByProcess(string strPath)
        {
            if (!File.Exists(strPath))
                return;
            using (Process proc = Process.Start(strPath))
            {
                //当某个文件被引用程序打开过后不关闭，再次打开proc就为NULL
                if (proc != null)
                    proc.Dispose();
            }
        }

        /// <summary>
        /// 是否存在进程
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns> 
        public bool IsAppExists(string appName)
        {
            string processName = System.IO.Path.GetFileNameWithoutExtension(appName);
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }

        /// <summary>
        /// 新建一封可以输入的邮件
        /// </summary>
        /// <param name="emailAddress"></param>
        public void AddNewEmail(Message.ServiceInterface.Message mail)
        {
            MailItem item = ClientUtility.GetOutlookAddIn().CreateItem(OlItemType.olMailItem) as MailItem;
            string emailAddress = mail.SendTo;
            if (!String.IsNullOrEmpty(emailAddress))
            {
                String addr = String.Empty;
                if (emailAddress.StartsWith("["))
                {
                    String[] arrAddress = emailAddress.Split('[');
                    addr = arrAddress[1].Substring(0, arrAddress[1].Length - 1);
                }
                else if (emailAddress.StartsWith("'") && emailAddress.EndsWith("'"))
                {
                    addr = emailAddress.Substring(1, emailAddress.Length - 2);
                }
                else if (emailAddress.StartsWith("<"))
                {
                    String[] arrAddress = emailAddress.Split('<');
                    addr = arrAddress[1].Substring(0, arrAddress[1].Length - 1);
                }
                else
                {
                    addr = emailAddress;
                }

                if (ClientUtility.olVersion == 11)
                {
                    item.To = addr;
                }
                else
                {
                    item.Recipients.Add(addr);
                }
            }

            //item.Save();
            item.Display(false);
            ClientProperties.isSaveMail = false;
            MailUtility.ReleaseComObject(item);
            //MailListPresenter.InvalidateEmailListRegion();
        }

        public void AddNewEmail(string sendTo)
        {
            if (string.IsNullOrEmpty(sendTo))
                return;
            Message.ServiceInterface.Message messageInfo = new Message.ServiceInterface.Message();
            messageInfo.SendTo = sendTo;
            AddNewEmail(messageInfo);
            messageInfo = null;
        }

        /// <summary>
        /// 如果是没有发送出去的邮件，则直接打开原编辑窗口，否则就回复所有人
        /// </summary>
        /// <param name="item">答复邮件</param>
        public void ReplyToAllContainsAttachment(object item)
        {
            if (item == null)
                return;
            MailItem _item = item as MailItem;
            if (_item == null)
                return;
            MailItem _relyAllItem = null;
            if (_item.Sent)
            {
                _relyAllItem = _item.ReplyAll();
                //当前答复邮件是否包含附件
                if (_relyAllItem.Attachments != null)
                {
                    List<AttachmentContent> attachments = ClientUtility.GetAttachmentContents(_item);
                    for (int index = 0; index < attachments.Count; index++)
                    {
                        _relyAllItem.Attachments.Add(attachments[index].ClientPath
                            , OlAttachmentType.olByValue, index + 1, attachments[index].DisplayName);
                    }
                    //清空邮件列表集合对象
                    attachments.Clear();
                    attachments = null;
                }
                ClientUtility.SetMessageID(_relyAllItem);
                _relyAllItem.Display(false);
                if (string.IsNullOrEmpty(_relyAllItem.Body) || _relyAllItem.Body == " ")
                {
                    _relyAllItem.Close(OlInspectorClose.olDiscard);
                    _item.Display(false);
                }
            }
            else
                _item.Display(false);
            MailUtility.ReleaseComObject(_relyAllItem);
            _item = null;
            //MailListPresenter.InvalidateEmailListRegion();
        }

        public void Open(Message.ServiceInterface.Message mail)
        {
            MailItem item = GetMailItem(mail);
            item.Save();
            item.Display(false);
            MailUtility.ReleaseComObject(item);
            //MailListPresenter.InvalidateEmailListRegion();
        }

        public void ReplyAll(Message.ServiceInterface.Message mail)
        {
            _MailItem item = GetMailItemReply(mail);
            item.Save();
            item.Display(false);
            MailUtility.ReleaseComObject(item);
            //MailListPresenter.InvalidateEmailListRegion();
        }

        public void Reply(Message.ServiceInterface.Message mail)
        {
            MailItem item = GetMailItemReply(mail);
            item.Save();
            item.Display(false);
            MailUtility.ReleaseComObject(item);
            mail = null;
            //MailListPresenter.InvalidateEmailListRegion();
        }


        static string entryID = string.Empty;
        /// <summary>
        /// 客户端发送邮件
        /// </summary>
        /// <param name="mail">邮件实体</param>
        public void Send(Message.ServiceInterface.Message mail)
        {
            Stopwatch stopwatch = StopwatchHelper.StartStopwatch();
            MailItem item = GetMailItem(mail);
            item.Save();
            if (mail.Attachments.Count > 0)
            {
                SaveAttachmentContent(mail, mail.Attachments[0].DisplayName, mail.Attachments[0].ClientPath);
            }
            // entryID = item.EntryID;
            //SaveMessage(item);
            SaveEventandState(mail, ClientUtility.GetMessageId(item.PropertyAccessor, ClientUtility.OutlookMessageIDTag));
            item.Display(false);
            MailUtility.ReleaseComObject(item);
            item = null; mail = null;
            //MailListPresenter.InvalidateEmailListRegion();
            //刷新任务中心列表
            ServiceClient.GetClientService<WorkItem>().Commands["Command_ListRefresh"].Execute();
            MethodBase method = MethodBase.GetCurrentMethod();
            StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName,"SEND-EMAIL" ,"客户端发送邮件新建邮件窗口");
        }



        /// <summary>
        /// 保存邮件实体中事件和修改事件状态
        /// </summary>
        public void SaveEventandState(Message.ServiceInterface.Message mail, string mailMsgId)
        {
            if (mail.UserProperties == null) return;
            if (mail.UserProperties.EventInfo == null) return;
            if (mail.UserProperties != null && mail.UserProperties.OperationId != Guid.Empty)
            {
                if (mail.UserProperties.EventInfo != null && !mail.UserProperties.EventInfo.Code.Equals("Nothing"))
                {
                    EventObjects info = mail.UserProperties.EventInfo;
                    info.MessageID = Guid.Empty;
                    info.MailMsgID = mailMsgId;
                    FcmCommonService.SaveMemoInfo(info);
                }
                if (!string.IsNullOrEmpty(mail.UserProperties.Action))
                {
                    List<BusinessSaveParameter> listBusinessParameter = new List<BusinessSaveParameter>();
                    BusinessSaveParameter parameter = new BusinessSaveParameter();
                    parameter["OceanBookingID"] = mail.UserProperties.OperationId;
                    if (mail.UserProperties.FormId != Guid.Empty)
                    {
                        parameter["BLID"] = mail.UserProperties.FormId;
                    }
                    parameter["OperationType"] = (int)mail.UserProperties.OperationType;
                    parameter[mail.UserProperties.Action] = 1;
                    listBusinessParameter.Add(parameter);
                    BusinessQueryServices.Save(listBusinessParameter);
                }
            }
        }


        /// <summary>
        /// 附件上传到文档列表
        /// </summary>
        /// <param name="message">邮件实体</param>
        /// <param name="attachmentName"></param>
        /// <param name="exportedPath"></param>
        public void SaveAttachmentContent(ICP.Message.ServiceInterface.Message message, string attachmentName, string exportedPath)
        {
            DocumentInfo document = new DocumentInfo();
            if (message.UserProperties == null) return;
            if (message.UserProperties.EventInfo == null) return;
            if (message.UserProperties.EventInfo.Code == "ANSC")
            {
                document.DocumentType = DocumentType.AN;
            }
            else
            {
                return;
            }
            document.Id = Guid.NewGuid();
            document.FileSources = FileSource.FDocument;
            document.OperationID = message.UserProperties.OperationId;
            document.CreateBy = LocalData.UserInfo.LoginID;
            document.CreateByName = LocalData.UserInfo.UserName;
            document.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            document.FormType = message.UserProperties.FormType;
            document.Name = attachmentName;
            document.Type = message.UserProperties.OperationType;
            try
            {
                ServiceClient.GetClientService<IClientFileService>().Upload(new DocumentInfo[] { document }, new string[] { exportedPath });
            }
            catch (Exception ex)
            {
                string strMessage = LocalData.IsEnglish ? "Upload attachments, Failure" + ex.Message : "上传附件,失败 " + ex.Message;
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, strMessage);
            }
        }
        /// <summary>
        /// 保存邮件中的事件信息，当前的邮件对象
        /// </summary>
        /// <param name="item">邮件信息</param>
        public void SaveMessage(MailItem item)
        {
            try
            {
                ClientUtility.NewSeveMeassage(item);
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog(CommonHelper.BuildExceptionString(ex));
            }
        }
        public void AutoSend(Message.ServiceInterface.Message mail)
        {
            InnerSend(mail);
            mail = null;
        }
        private void InnerSend(Message.ServiceInterface.Message mail)
        {
            MailItem item = GetMailItem(mail);
            item.Save();
            item.Send();
            MailUtility.ReleaseComObject(item);
            //MailListPresenter.InvalidateEmailListRegion();
        }

        public void Resend(Message.ServiceInterface.Message mail)
        {
            InnerSend(mail);

        }

        private MailItem GetMailItem(Message.ServiceInterface.Message mail)
        {

            PropertyAccessor pa = null;
            MailItem item = null;
            try
            {
                ClientProperties.isSaveMail = true;
                item = ClientUtility.GetOutlookAddIn().CreateItem(OlItemType.olMailItem) as MailItem;

                item.CC = mail.CC;
                item.To = mail.SendTo;
                item.Subject = mail.Subject;
                // OlBodyFormat bodyFormat = (OlBodyFormat)Enum.Parse(typeof(OlBodyFormat), mail.BodyFormat.ToString());

                //item.BodyFormat = bodyFormat;
                item.BodyFormat = OlBodyFormat.olFormatHTML;

                item.HTMLBody = mail.Body;


                //使用mail head来获取邮件信息
                pa = item.PropertyAccessor;
                if (mail.UserProperties != null)
                {
                    pa.SetProperty(ClientUtility.OperationContextTag, mail.UserProperties.ToString());

                }
                ClientUtility.SetMessageID(item);



                if (mail.Attachments != null)
                {
                    int count = mail.Attachments.Count;
                    for (int i = 0; i < count; i++)
                    {
                        item.Attachments.Add(mail.Attachments[i].ClientPath, OlAttachmentType.olByValue, i + 1, mail.Attachments[i].DisplayName);
                    }
                }
            }
            catch (ThreadAbortException e)
            {
            }
            catch (ObjectDisposedException ode)
            {
            }
            catch (AccessViolationException ave)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MailUtility.ReleaseComObject(pa);
            }

            return item;
        }

        /// <summary>
        /// 构建回复邮件对象
        /// </summary>
        /// <param name="mail">原始邮件</param>
        /// <returns>回复邮件对象</returns>
        private MailItem GetMailItemReply(Message.ServiceInterface.Message mail)
        {

            PropertyAccessor pa = null;
            MailItem item = null;
            try
            {
                ClientProperties.isSaveMail = true;

                item = ClientUtility.GetOutlookAddIn().CreateItem(OlItemType.olMailItem) as MailItem;
                item.CC = mail.CC;
                item.To = mail.SendFrom;
                item.Subject = (ClientUtility.olCultrue == "zh-cn" ? "答复：" : "RE:") + mail.Subject;
                item.BodyFormat = OlBodyFormat.olFormatHTML;
                string strReplyHead = "<br /><hr />";
                strReplyHead += "<b>" + (ClientUtility.olCultrue == "zh-cn" ? "发件人：" : "Form:") + "</b>" + mail.SendFrom + "<br />";
                if (mail.Sendtime != null)
                    strReplyHead += "<b>" + (ClientUtility.olCultrue == "zh-cn" ? "发送时间：" : "Sent:") + "</b>" + Convert.ToDateTime(mail.Sendtime).ToString("yyyy年mm月dd日 hh:MM") + "<br />";
                if (!string.IsNullOrEmpty(mail.CC))
                    strReplyHead += "<b>" + (ClientUtility.olCultrue == "zh-cn" ? "抄送：" : "Cc:") + "</b>" + mail.CC + "<br />";
                strReplyHead += "<b>" + (ClientUtility.olCultrue == "zh-cn" ? "收件人：" : "To:") + "</b>" + mail.SendTo + "<br />";
                strReplyHead += "<b>" + (ClientUtility.olCultrue == "zh-cn" ? "主题：" : "Subject:") + "</b>" + mail.Subject + "<br />";
                strReplyHead += "<br />";
                item.HTMLBody = strReplyHead + mail.Body;


                //使用mail head来获取邮件信息
                pa = item.PropertyAccessor;
                if (mail.UserProperties != null)
                {
                    pa.SetProperty(ClientUtility.OperationContextTag, mail.UserProperties.ToString());

                }

                if (mail.Attachments != null)
                {
                    int count = mail.Attachments.Count;
                    for (int i = 0; i < count; i++)
                    {
                        item.Attachments.Add(mail.Attachments[i].ClientPath, OlAttachmentType.olByValue, i + 1, mail.Attachments[i].DisplayName);
                    }
                }

                ClientUtility.SetMessageID(item);
            }
            catch (ThreadAbortException e)
            {
            }
            catch (ObjectDisposedException ode)
            {
            }
            catch (AccessViolationException ave)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MailUtility.ReleaseComObject(pa);
            }

            return item;
        }

        public void Forward(Message.ServiceInterface.Message mail)
        {
            MailItem item = GetMailItem(mail);
            item.Save();
            item.Display(false);
            MailUtility.ReleaseComObject(item);
        }

        /// <summary>
        /// 通过outlook打开msg文件
        /// </summary>
        /// <param name="path"></param>
        public void Open(string filePath)
        {
            OpenMail(filePath, false);
        }

        /// <summary>
        /// 使用Outlook打开Msg文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isServerDownLoad"></param>
        private void OpenMail(string filePath, bool isServerDownLoad)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    if (filePath.ToLower().EndsWith(".msg"))
                    {
                        IntPtr vHandle = WindowsExtension._lopen(filePath, SWP.OF_READWRITE | SWP.OF_SHARE_DENY_NONE);
                        //表示文件正在被使用
                        if (vHandle == new IntPtr(-1))
                        {
                            throw new ICPException(LocalData.IsEnglish ? "it has opened in outlook program!" : "已在Outlook程序打开!");
                            return;
                        }
                        else
                            WindowsExtension.CloseHandle(vHandle);

                        _MailItem item =
                            ClientUtility.CreateOutlookNameSpaceInstance().OpenSharedItem(filePath) as MailItem;
                        if (item != null)
                        {
                            //服务端下载的邮件，现已设置了关联邮件的绿色类别
                            if (isServerDownLoad)
                            {
                                //item.Categories = ClientUtility.GreenCategory;
                                //item.Save();
                            }
                            item.Display(false);
                            MailUtility.ReleaseComObject(item);
                            //MailListPresenter.InvalidateEmailListRegion();
                        }
                    }
                    else
                    {
                        using (Process proc = Process.Start(filePath))
                        {
                            //当某个文件被引用程序打开过后不关闭，再次打开proc就为NULL
                            if (proc != null)
                                proc.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.SaveLog(
                        CommonHelper.BuildExceptionString(ex));
                    throw ex;
                }
            }
            else
            {
                throw new ICPException("the file is not exsit.");
            }
        }

        public string ConvertMailToPDF(string mailFile)
        {

            string outputFilePath = string.Empty;
            MailItem mail = ClientUtility.CreateOutlookNameSpaceInstance().OpenSharedItem(mailFile) as MailItem;
            string htmlBody = mail.HTMLBody;
            // write html to file      
            outputFilePath = GetHtmlTargetFilePath(mailFile);

            using (FileStream fs = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(htmlBody);
                fs.Write(data, 0, data.Length);
                fs.Close();
            }

            string pdfFile = ExportHtml2PDF(outputFilePath);
            return pdfFile;
        }
        private static string GetHtmlTargetFilePath(string inputFile)
        {
            return GetTargetFilePath(inputFile, ".html");
        }
        private static string GetPDFTargetFilePath(string inputFile)
        {
            return GetTargetFilePath(inputFile, ".pdf");
        }
        private static String GetTargetFilePath(String inputFile, string fileExtension)
        {
            string rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, IOHelper.HtmlTempPath);
            return Path.Combine(rootPath, Path.GetFileNameWithoutExtension(inputFile) + fileExtension);

        }
        /// <summary>
        /// 将html转换为pdf文件
        /// </summary>
        /// <param name="inputFile">html文件</param>
        /// <param name="outPutFile">转换后文件的地址</param>
        /// <returns></returns>
        public string ExportHtml2PDF(string inputFile)
        {

            string outputFilePath = string.Empty;
            try
            {
                // Instantiate an object PDF class
                Pdf pdf = new Pdf();
                // add the section to PDF document sections collection
                Section section = pdf.Sections.Add();
                // Read the contents of HTML file into StreamReader object
                using (StreamReader reader = File.OpenText(inputFile))
                {
                    //Create text paragraphs containing HTML text
                    Text text2 = new Text(section, reader.ReadToEnd());
                    // enable the property to display HTML contents within their own formatting
                    text2.IsHtmlTagSupported = true;
                    //Add the text paragraphs containing HTML text to the section
                    section.Paragraphs.Add(text2);
                    pdf.IsAutoFontAdjusted = true;
                    // embed the font subset in resultant PDF
                    pdf.SetUnicode();
                    // Specify the URL which serves as images database
                    outputFilePath = GetPDFTargetFilePath(inputFile);
                    //pdf.HtmlInfo.ImgUrl = string.Format(@"{0}\", Path.GetDirectoryName(outputFilePath));
                    //Save the pdf document
                    pdf.Save(outputFilePath);

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return outputFilePath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string ConvertMessageToMsg(Message.ServiceInterface.Message message)
        {
            return ConvertMessage2Msg(message, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isOpen"></param>
        /// <returns></returns>
        public string ConvertMessage2Msg(Message.ServiceInterface.Message message, bool isOpen)
        {
            if (message == null) return "";

            MailMessage mailMsg = new MailMessage();
            mailMsg.From = message.SendFrom;
            if (!string.IsNullOrEmpty(message.SendTo))
            {
                FillEmailAddress(mailMsg.To, message.SendTo);
            }
            mailMsg.Subject = message.Subject.Replace('\r', ' ').Replace('\n', ' ');
            //mailMsg.Date = message.Sendtime.HasValue ? message.Sendtime.Value : message.CreateDate;
            mailMsg.Date = message.SentDateTimeZone == DateTimeOffset.MinValue
                ? message.Sendtime.HasValue ? message.Sendtime.Value
                : message.CreateDate : message.SentDateTimeZone.ToLocalDateTime();
            if (!string.IsNullOrEmpty(message.CC))
            {
                FillEmailAddress(mailMsg.CC, message.CC);
            }

            mailMsg.HtmlBody = message.Body.DecodeSpecialCharacter();
            //判断MessageID是否为空
            mailMsg.MessageId = string.IsNullOrEmpty(message.MessageId) ? ClientUtility.GetMessageID() : message.MessageId;

            foreach (AttachmentContent content in message.Attachments)
                mailMsg.Attachments.Add(new Attachment(content.ClientPath));

            MapiMessage outlookMsg = MapiMessage.FromMailMessage(mailMsg, OutlookMessageFormat.Unicode);

            string windowTitle = LocalData.IsEnglish ? string.Format("{0} - Message (HTML) ", message.Id) : string.Format("{0} - 邮件 (纯文本) ", message.Id);
            string strMsgFile = Path.Combine(Path.GetTempPath(), string.Format("{0}.msg", windowTitle));
            if (File.Exists(strMsgFile))
            {
                try
                {
                    File.Delete(strMsgFile);
                }
                catch
                {
                    strMsgFile = Path.Combine(Path.GetTempPath(), string.Format("{0}{1}.msg", windowTitle, Guid.NewGuid()));
                }
            }

            outlookMsg.Save(strMsgFile);
            if (isOpen)
                OpenMail(strMsgFile, true);
            return strMsgFile;
        }

        private void FillEmailAddress(MailAddressCollection mailAddress, string email)
        {
            List<string> lstEmails = email.Split(';').ToList();
            foreach (string item in lstEmails)
            {
                if (item.IsValidEmail())
                {
                    mailAddress.Add(item);
                }
            }
        }



    }
}
