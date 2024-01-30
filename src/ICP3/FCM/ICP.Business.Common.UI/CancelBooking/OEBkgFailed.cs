/*****
 * 描述：取消订舱和订舱失败统一界面 
 * 创建时间:2014-04-23
 * 创建人: wlj
  *****/
using System;
using System.Collections.Generic;
using System.Linq;
using ICP.Business.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.ServiceInterface;
using ICP.MailCenter.UI;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;

namespace ICP.Business.Common.UI
{
    public partial class OEBkgFailed : PopupWindow
    {
        public WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }
        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// 业务ID 
        /// </summary>
        public Guid OperationId { get; set; }

        public FailureBooking FailureBooking { get; set; }

        public CancelBooking CancelBooking { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OceanBookingInfo OceanBookingInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IMessageService MessageService
        {
            get { return ServiceClient.GetService<IMessageService>(); }
        }

        public IMailCenterTemplateService MailCenterTemplateService
        {
            get
            {
                ClientHelper.EnsureEmailCenterAppStarted();

                return new MailCenterTemplateService();
            }
        }
        /// <summary>
        /// 获取业务的信息信息
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        public ICP.Sys.ServiceInterface.IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<ICP.Sys.ServiceInterface.IUserService>();
            }
        }
        /// <summary>
        /// 事件列表保存是修改缓存表的指定字段
        /// </summary>
        public IBusinessQueryService BusinessQueryService
        {
            get { return ServiceClient.GetService<IBusinessQueryService>(); }
        }


        public IMainCenterEmailTemplateGetter MainCenterEmailTemplateGetter
        {
            get { return ServiceClient.GetClientService<IMainCenterEmailTemplateGetter>(); }
        }
        /// <summary>
        /// 揽货人的信息
        /// </summary>
        ICP.Sys.ServiceInterface.DataObjects.UserInfo _salesuserInfo = null;
        /// <summary>
        /// 客服人员的信息
        /// </summary>
        ICP.Sys.ServiceInterface.DataObjects.UserInfo _bookingeruserInfo = null;
        /// <summary>
        /// 订舱员信息
        /// </summary>
        ICP.Sys.ServiceInterface.DataObjects.UserInfo _bookingByuserInfo = null;
        public OEBkgFailed()
        {
            InitializeComponent();
            simpleClose.Click += delegate { this.FindForm().Close(); };
            this.Disposed += delegate
            {


                _salesuserInfo = null;
                _bookingeruserInfo = null;
                _bookingByuserInfo = null;
            };
            this.Load += new EventHandler(BkgFailed_Load);
        }
        void BkgFailed_Load(object sender, EventArgs e)
        {
            OceanBookingInfo = OceanExportService.GetOceanBookingInfo(OperationId);
            if (OceanBookingInfo == null) return;
            bool flg = true;
            simpleOk.Text = LocalData.IsEnglish ? "Send" : "发送";
            simpleClose.Text = LocalData.IsEnglish ? "Close" : "关闭";
            groupBox2.Text = LocalData.IsEnglish ? "Description" : "描述";
            groupBox1.Text = LocalData.IsEnglish ? "Recipient" : "接收";
            if (OceanBookingInfo.SalesID != null)
            {
                _salesuserInfo = UserService.GetUserInfo((Guid)OceanBookingInfo.SalesID);
                checkSales.Text = LocalData.IsEnglish
                                      ? "Mail to Sales:" + _salesuserInfo.Code
                                      : "邮件发给揽货人:" + _salesuserInfo.Code;
            }
            else
            {
                flg = false;
            }
            if (FailureBooking != FailureBooking.Unknown && OceanBookingInfo.BookingerID != null)
            {
                _bookingeruserInfo = UserService.GetUserInfo((Guid)OceanBookingInfo.BookingerID);
                checkinternal.Text = LocalData.IsEnglish ? "Mail to Customer Service:" + _bookingeruserInfo.Code : "邮件发给客服:" + _bookingeruserInfo.Code;
            }
            if (CancelBooking != CancelBooking.Unknown && OceanBookingInfo.BookingByID != null)
            {
                _bookingByuserInfo = UserService.GetUserInfo((Guid)OceanBookingInfo.BookingByID);
                checkinternal.Text = LocalData.IsEnglish
                                         ? "Mail to Bookinger:" + _bookingByuserInfo.Code
                                         : "邮件发给订舱员:" + _bookingByuserInfo.Code;
            }
            if (flg == false)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null,
                                                      LocalData.IsEnglish
                                                          ? "The mail of cancellation notice could not be sent due to the field Sales is required....."
                                                          : "揽货人信息为空，无法发送邮件.....");
                this.FindForm().Close();
            }
        }

        private void simpleOk_Click(object sender, EventArgs e)
        {
            if (memoDescription.EditValue == null)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null,
                                                       LocalData.IsEnglish
                                                           ? "Please enter a description."
                                                           : "请输入描述信息.");
                return;
            }
            var eventObjects = new EventObjects
            {
                OperationID = OceanBookingInfo.ID,
                OperationType = OperationType.OceanExport,
                Code = FailureBooking == FailureBooking.Unknown ? "SOC" : "SOF",
                Id = Guid.Empty,
                FormID = OceanBookingInfo.ID,
                FormType = FormType.Unknown,
                IsShowAgent = false,
                IsShowCustomer = true,
                Subject = Theme,
                Description = Theme,
                Priority = MemoPriority.Normal,
                Type = MemoType.EmailLog,
                UpdateDate = DateTime.Now,
                UpdateBy = LocalData.UserInfo.LoginID
            };
            #region 邮件接收人
            string sendTo = string.Empty;
            if (checkinternal.Checked && _bookingeruserInfo != null)
            {
                sendTo = _bookingeruserInfo.EMail;
            }
            if (checkSales.Checked && _salesuserInfo != null)
            {
                if (string.IsNullOrEmpty(sendTo))
                {
                    sendTo = _salesuserInfo.EMail;
                }
                else
                {
                    sendTo = sendTo + ";" + _salesuserInfo.EMail;
                }
            }
            #endregion



            string body = memoDescription.EditValue.ToString().Trim();

            var message = CreateMessageInfo(MessageType.Email,
                                            MessageWay.Send, sendTo, LocalData.UserInfo.EmailAddress,
                                            FormType.Booking, OperationType.OceanExport,
                                            OceanBookingInfo.ID, Guid.Empty,
                                            body, Theme, string.Empty, eventObjects.Code, eventObjects, null);
            //MailCenterTemplateService.SendMailWithTemplate(message, LocalData.IsEnglish, string.Empty, null);
            message.BodyFormat = BodyFormat.olFormatHTML;
            message.State = MessageState.Success;
            MessageService.Send(message);
            var business = new List<BusinessSaveParameter>();
            var dictionary = new Dictionary<string, object>
                            {
                                {"OceanBookingID", eventObjects.OperationID},
                                {eventObjects.Code,"1"},
                                {"OperationType",OperationType.OceanExport}
                            };
            var businessSave = new BusinessSaveParameter { items = dictionary };
            business.Add(businessSave);
            BusinessQueryService.Save(business);
            OceanExportService.UpdateOceanBookingsState(LocalData.UserInfo.LoginID, OperationId, OEOrderState.CancelBooking);
            RootWorkItem.State["Indicates"] = true;
            this.FindForm().Close();

        }
        /// <summary>
        /// 返回消息实体类
        /// </summary>
        /// <param name="type">发送类型</param>
        /// <param name="way">发送方向</param>
        /// <param name="sendTo">接收人邮箱</param>
        /// <param name="sendFrom">发送人邮箱</param>
        /// <param name="formType">表单类型</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="operationId">操作ID</param>
        /// <param name="formId">表单ID</param>
        /// <param name="body">发送内容</param>
        /// <param name="subject">主题</param>
        /// <param name="cc">邮件抄送地址</param>
        /// <param name="action">操作动作</param>
        /// <param name="eventObjects">事件列表实体</param>
        /// <param name="attachmentContents">邮件附件信息</param>
        /// <returns></returns>
        public Message.ServiceInterface.Message
            CreateMessageInfo(Message.ServiceInterface.MessageType type,
            Message.ServiceInterface.MessageWay way, string sendTo, string sendFrom, FormType formType,
            OperationType operationType, Guid operationId, Guid formId, string body, string subject, string cc,
            string action, EventObjects eventObjects, List<AttachmentContent> attachmentContents)
        {
            // 邮件发送的消息实体
            var message = new Message.ServiceInterface.Message();

            message.Type = type;
            message.Way = way;
            //收件人邮箱地址
            message.SendTo = sendTo;
            //发件人邮箱地址
            message.SendFrom = sendFrom;
            //邮件抄送地址
            message.CC = cc;

            message.UserProperties = new Message.ServiceInterface.MessageUserPropertiesObject
            {
                FormType = formType,
                OperationType = operationType,
                OperationId = operationId,
                FormId = formId
            };
            if (!string.IsNullOrEmpty(action))
            {
                //MessageUserPropertiesObject(消息自定义属性类)

                message.UserProperties.Action = action;
            }
            if (!string.IsNullOrEmpty(body) && !string.IsNullOrEmpty(subject))
            {
                message.Body = body;
                message.Subject = subject;
            }
            if (attachmentContents != null && attachmentContents.Any())
            {
                message.Attachments = attachmentContents;
            }
            if (eventObjects != null)
                message.UserProperties.EventInfo = eventObjects;

            return message;

        }

    }
}
