using System;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.MailCenter.ServiceInterface;
using outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Interop.Outlook;
using System.Windows.Forms;

namespace ICP.MailCenter.UI.UC
{
    public class CommonDataService : Controller
    {
        #region Sevice
        /// <summary>
        /// 根WorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        IOutLookService _outlookService;
        public IOutLookService outLookService
        {
            get
            {
                if (_outlookService == null)
                    _outlookService = ServiceClient.GetClientService<IOutLookService>();
                return _outlookService;
            }
        }
        //[ServiceDependency]
        //public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }

        //[ServiceDependency]
        //public IClientFileService clientFileService { get; set; }
        #endregion

        #region Constants
        MailItem item;
        #endregion

        #region  attribute

        #endregion


        /// <summary>
        /// 新增关联业务的邮件
        /// </summary>
        void SendRelatedMail()
        {
            //if (CurrentRow != null)
            //{
            //    ICP.Message.ServiceInterface.Message mail = CreateMailInfo();
            //    outLookService.Send(mail);
            //}
        }
        //ICP.Message.ServiceInterface.Message CreateMailInfo()
        //{
        //    ICP.Message.ServiceInterface.Message mail = new ICP.Message.ServiceInterface.Message();
        //    mail.Id = Guid.NewGuid();
        //    //mail.OperationID = CurrentRow.ID;
        //    mail.BodyFormat = BodyFormat.olFormatHTML;
        //    mail.CC = string.Empty;
        //    //mail.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
        //    mail.Body = "";
        //    //mail.OperationType = CurrentRow.OperationType;
        //    // mail.SendTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
        //    mail.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
        //    mail.UserProperties.OperationId = CurrentRow.ID;
        //    mail.UserProperties.OperationType = CurrentRow.OperationType;
        //    //mail.UserProperties = new MailUserPropertiesObject(
        //    //                                                 true,
        //    //                                                 CurrentRow.ID,
        //    //                                                 mail.Id,
        //    //                                                 ICP.Framework.CommonLibrary.Common.FormType.Booking,
        //    //                                                 CurrentRow.OperationType,
        //    //                                                 CurrentRow.CustomerID,
        //    //                                                 CurrentRow.ContactID,
        //    //                                                 CurrentRow.OperationNO,
        //    //                                                 MailRegionType.COD);
        //    return mail;
        //}
        //ICP.Message.ServiceInterface.Message CreateMailInfo(_MailItem mailItem, MessageUserPropertiesObject info)
        //{
        //    Mail mail = new Mail();
        //    mail.Id = info.MailID;
        //    mail.Body = mailItem.Body;
        //    mail.BodyFormat = BodyFormat.olFormatHTML;
        //    mail.HtmlBody = mailItem.HTMLBody;
        //    mail.OperationType = info.OperationType;
        //    mail.FormType = info.FormType;
        //    mail.OperationID = info.OperationID;
        //    mail.CC = mailItem.CC;
        //    mail.Body = mailItem.Body;
        //    mail.HtmlBody = mailItem.HTMLBody;
        //    mail.Subject = mailItem.Subject;
        //    List<MailAttachment> list = new List<MailAttachment>();
        //    MailAttachment mailAttachment;
        //    foreach (string att in mailItem.Attachments)
        //    {
        //        mailAttachment = new MailAttachment();
        //        mailAttachment.FilePath = Path.Combine(System.IO.Path.GetTempPath(), att);
        //        mailAttachment.DisplayName = att;
        //        list.Add(mailAttachment);
        //    }
        //    mail.Attachments = list;

        //    return mail;
        //}

        void DoAction(MailAction action)
        {
            try
            {
                _MailItem mailItem = this.RootWorkItem.State[MailCenterCommandConstants.CurrentSelection] as MailItem;
                if (mailItem == null)
                {
                    _ReportItem _reportItem =
                        this.RootWorkItem.State[MailCenterCommandConstants.CurrentSelection] as ReportItem;
                    if (_reportItem != null)
                    {
                        if (action == MailAction.Forword)
                        {

                        }
                    }
                }
                else
                {
                    //表示选中了邮件
                    if (ClientProperties.IsEnableToolBar == true)
                    {
                        WithoutRelatedBusinessActionCondition(action, mailItem);
                    }
                    if (ClientProperties.IsEnableToolBar == false && action == MailAction.AddNew) //只能新增邮件
                    {
                        WithoutRelatedBusinessActionCondition(action, mailItem);
                    }
                }
                //if (mailItem != null)
                //{
                //    //UserProperty userProperties = mailItem.UserProperties["ICPAction"];
                //    //if (userProperties != null && userProperties.Value != null)
                //    //{
                //    //    if (userProperties.Value.ToString().GetType() == typeof(MailUserPropertiesObject))
                //    //    {
                //    //        MailUserPropertiesObject info = (MailUserPropertiesObject)userProperties.Value;
                //    //        Mail mail = this.CreateMailInfo(mailItem, info);
                //    RelatedBusinessActionCondition(action, mailItem);
                //    //}
                //}
                //else
                //{


                //}
            }
            catch (System.Exception ex)
            {
                ICP.Framework.CommonLibrary.Logger.Log.Error(
                                        ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 没有关联业务的邮件
        /// </summary>
        /// <param name="action"></param>
        /// <param name="mailItem"></param>
        void WithoutRelatedBusinessActionCondition(MailAction action, object mailItem)
        {
            switch (action)
            {
                case MailAction.AddNew:
                    outLookService.AddNewEmail(CreateMessageInfo());//调用Outlook服务接口
                    break;
                case MailAction.Forword:
                    SetMailRead(mailItem);
                    outLookService.Forward(mailItem);
                    break;
                case MailAction.Reply:
                    SetMailRead(mailItem);
                    outLookService.ReplyToSender(mailItem);
                    break;
                case MailAction.ReplyAll:
                    SetMailRead(mailItem);
                    outLookService.ReplyToAll(mailItem);
                    break;
                case MailAction.ReplyAllContainsAttachment:
                    outLookService.ReplyToAllContainsAttachment(mailItem);
                    break;
            }
        }

        /// <summary>
        /// 将邮件设置成已读
        /// </summary>
        /// <param name="objMailItem"></param>
        private void SetMailRead(object objMailItem)
        {
            _MailItem item = objMailItem as MailItem;
            if (item != null)
            {
                if (item.UnRead)
                {
                    try
                    {
                        item.UnRead = false;
                        item.Save();
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
        }

        ICP.Message.ServiceInterface.Message CreateMessageInfo()
        {
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.SendTo = string.Empty;

            return message;
        }
        /// <summary>
        /// 已关联业务的邮件
        /// </summary>
        /// <param name="action"></param>
        /// <param name="mail"></param>
        /// <param name="mailItem"></param>
        void RelatedBusinessActionCondition(MailAction action, object mailItem)
        {
            switch (action)
            {
                case MailAction.AddNew:
                    break;
                case MailAction.Forword:
                    outLookService.Forward(mailItem);
                    break;
                case MailAction.Reply:
                    outLookService.ReplyToSender(mailItem);
                    break;
                case MailAction.ReplyAll:
                    outLookService.ReplyToAll(mailItem);
                    break;
            }
        }

        #region 新增一封邮件
        [CommandHandler(MailCenterCommandConstants.Command_AddNewMail)]
        public void Command_AddNewMail(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                DoAction(MailAction.AddNew);
            }
            //if (CurrentRow == null)
            //{
            //    ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            //    outLookService.AddNewEmail(message);
            //}
            //else
            //{
            //  SendRelatedMail();
            //}
        }

        #endregion
        #region 回复邮件
        [CommandHandler(MailCenterCommandConstants.Command_ReplyMail)]
        public void Command_ReplyMail(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                DoAction(MailAction.Reply);
            }
        }
        #endregion

        #region 答复所有人
        [CommandHandler(MailCenterCommandConstants.Command_ReplyALL)]
        public void Command_ReplyALL(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                DoAction(MailAction.ReplyAll);
            }
        }
        #endregion

        #region 答复所有人(包含附件)
        [CommandHandler("Command_ReplyALLContainsAttachment")]
        public void Command_ReplyALLContainsAttachment(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                DoAction(MailAction.ReplyAllContainsAttachment);
            }
        }
        #endregion

        #region 转发邮件
        [CommandHandler(MailCenterCommandConstants.Command_Forward)]
        public void Command_Forward(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                DoAction(MailAction.Forword);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 保存备注信息
        /// </summary>
        /// <param name="commonMenoList"></param>
        /// <param name="formId"></param>
        /// <param name="formType"></param>
        /// <param name="operationId"></param>
        /// <param name="type"></param>
        ///// <returns></returns>
        //public bool SaveMemo(
        //    CommonMemoList commonMenoList,
        //    Guid?[] formId,
        //    ICP.Framework.CommonLibrary.Common.FormType[] formType,
        //    Guid operationId,
        //    ICP.Framework.CommonLibrary.Common.OperationType type)
        //{
        //    ManyResultData mans = this.fcmCommonService.SaveMemoList(
        //      operationId,
        //      type,
        //      new Guid?[1] { commonMenoList.ID },
        //      formId,
        //      formType,
        //      new bool[1] { false },
        //      new bool[1] { false },
        //      new String[1] { commonMenoList.Subject },
        //      new String[1] { commonMenoList.Content },
        //      new MemoType[1] { MemoType.Memo },
        //      new MemoPriority[1] { MemoPriority.Normal },
        //      LocalData.UserInfo.LoginID,
        //      new DateTime?[1] { commonMenoList.UpdateDate });

        //    if (mans == null || mans.ChildResults == null || mans.ChildResults.Count == 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        //public String DownLoadFile(Guid id)
        //{
        //    try
        //    {
        //        if (id == Guid.Empty) { return ""; }
        //        ContentInfo contentInfo = clientFileService.GetDocumentContent(id);
        //        return DataCacheUtility.SaveFileContentToDisk(contentInfo);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw new System.Exception(ex.Message);
        //    }
        //}
        #endregion
    }
}
