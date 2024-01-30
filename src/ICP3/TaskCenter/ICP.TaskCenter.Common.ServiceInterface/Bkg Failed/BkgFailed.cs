using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.Interfaces;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.Business.ServiceInterface;
using ICP.Message.ServiceInterface;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.TaskCenter.Common.ServiceInterface.Bkg_Failed
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class BkgFailed : BaseEditPart
    {
        [ServiceDependencyAttribute]
        public WorkItem workItem { get; set; }


        /// <summary>
        /// 邮件发送注入的服务
        /// </summary>
        public ICP.MailCenter.ServiceInterface.IMailCenterTemplateService MailCenter
        {
            get
            {
                ClientHelper.EnsureEmailCenterAppStarted();
                return ServiceClient.GetService<ICP.MailCenter.ServiceInterface.IMailCenterTemplateService>();
            }

        }
        /// <summary>
        /// 当前的业务处理面板
        /// </summary>
        //[State]
        public BaseBusinessPart CurrentBaseBusinessPart
        {
            get { return workItem.State["CurrentBaseBusinessPart"] as BaseBusinessPart; }
            set { workItem.State["CurrentBaseBusinessPart"] = value; }
        }
        public BkgFailed()
        {
            InitializeComponent();
            simpleClose.Click += delegate { this.FindForm().Close(); };
        }

        private void simpleOk_Click(object sender, EventArgs e)
        {
            var eventObjects = new ICP.FCM.Common.ServiceInterface.DataObjects.EventObjects();

            var oceanbookinginfo = ServiceClient.GetService<IOceanExportService>().GetOceanBookingInfo(CurrentBaseBusinessPart.ID);
            if (memoDescription.EditValue!=null)
            {
                eventObjects.Description = memoDescription.EditValue.ToString();
            }
            else
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null,
                                                            LocalData.IsEnglish
                                                                ? "Please enter a description."
                                                                : "请输入描述信息.");
                //MessageBox.Show(LocalData.IsEnglish ? "Please enter a description." : "请输入描述信息.");
                return;
            }
            oceanbookinginfo.POLCheck = true;
            oceanbookinginfo.PODCheck = true;
            oceanbookinginfo.ContainerCheck = true;
            string top = ServiceClient.GetService<IICPCommonOperationService>().JudgmentTip(oceanbookinginfo);
            if (!string.IsNullOrEmpty(top))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, top);
            }
            else
            {
                string eMail = string.Empty;
                string template = string.Empty;
                var userInfo = ServiceClient.GetService<IUserService>().GetUserInfo((Guid)oceanbookinginfo.BookingerID);
                var salesName = ServiceClient.GetService<IUserService>().GetUserInfo((Guid)oceanbookinginfo.SalesID);
                if (checkCustomer.Checked)
                {
                    eMail = userInfo.EMail;
                    template = "ConfirBkgFailedC";
                }
                if (checkSales.Checked)
                {
                    eMail = salesName.EMail;
                    template = "ConfirBkgFailedS";
                }
                if (checkSales.Checked && checkCustomer.Checked)
                {
                    eMail = userInfo.EMail + ";" + salesName.EMail;
                    template = "ConfirBkgFailed";
                }
                // 邮件发送的消息实体
                var message = CreateMessageInfo(
                    MessageType.Email,
                    MessageWay.Send,
                    eMail, LocalData.UserInfo.EmailAddress,
                    FormType.Booking, OperationType.OceanExport,
                    oceanbookinginfo.ID, oceanbookinginfo.ID, string.Empty, string.Empty, string.Empty, "SOF");

                eventObjects.OperationID = oceanbookinginfo.ID;
                eventObjects.OperationType = OperationType.OceanExport;
                eventObjects.EventID = new Guid("F7A53B6F-2321-48D8-9312-45CF849299D3");
                eventObjects.Id = Guid.Empty;
                eventObjects.FormID = oceanbookinginfo.ID;
                eventObjects.FormType = FormType.Unknown;
                eventObjects.IsShowAgent = false;
                eventObjects.IsShowCustomer = true;
                eventObjects.Subject = "F7A53B6F-2321-48D8-9312-45CF849299D3";
                eventObjects.Priority = MemoPriority.Normal;
                eventObjects.Type = MessageType.Email;
                eventObjects.UpdateDate = DateTime.Now;
                eventObjects.UpdateBy = LocalData.UserInfo.LoginID;
                object[] values = { oceanbookinginfo, eventObjects };

                MailCenter.SendMailWithTemplate(message, true, template, values);

                eventObjects.Subject = message.Subject;
                eventObjects.Description = "SO is Failed";
                ServiceClient.GetService<IICPCommonOperationService>().SaveEventInfo(eventObjects);
            }
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
        /// <returns></returns>
        public Message.ServiceInterface.Message CreateMessageInfo(Message.ServiceInterface.MessageType type,
            Message.ServiceInterface.MessageWay way, string sendTo, string sendFrom, FormType formType,
            OperationType operationType, Guid operationId, Guid formId, string body, string subject, string cc,
            string action)
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
            if (!string.IsNullOrEmpty(action))
            {
                //MessageUserPropertiesObject(消息自定义属性类)
                message.UserProperties = new Message.ServiceInterface.MessageUserPropertiesObject();
                message.UserProperties.FormType = formType;
                message.UserProperties.OperationType = operationType;
                message.UserProperties.OperationId = operationId;
                message.UserProperties.FormId = formId;
                //Message.UserProperties.Action = Action;
            }
            if (!string.IsNullOrEmpty(body) && !string.IsNullOrEmpty(subject))
            {
                message.Body = body;
                message.Subject = subject;
            }
            return message;

        }
    }
}
