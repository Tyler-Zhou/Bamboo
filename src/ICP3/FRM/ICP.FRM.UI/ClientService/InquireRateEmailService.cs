#region Comment

/*
 * 
 * FileName:    InquireRateEmailService.cs
 * CreatedOn:   
 * CreatedBy:   
 * 
 * 
 * Description：
 *      ->询价客户端服务实现
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System.Threading;
using ICP.Business.Common.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Message.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FormType = ICP.Framework.CommonLibrary.Common.FormType;

namespace ICP.FRM.UI
{
    public class InquireRateEmailService : IInquireRateEmailService
    {
        #region 服务
        /// <summary>
        /// 用户服务
        /// </summary>
        private IUserService UserService
        {
            get
            {
                return ServiceClient.GetClientService<IUserService>();
            }
        }

        /// <summary>
        /// 邮件中心模板操作类
        /// </summary>
        public IMainCenterEmailTemplateGetter MainCenterEmailTemplateGetter
        {
            get { return ServiceClient.GetClientService<IMainCenterEmailTemplateGetter>(); }
        }

        /// <summary>
        /// 消息服务(发送邮件)
        /// </summary>
        public IMessageService MessageService
        {
            get { return ServiceClient.GetService<IMessageService>(); }
        }

        public IOperationMessageRelationService OperationMessageRelationService
        {
            get { return ServiceClient.GetService<IOperationMessageRelationService>(); }
        }

        /// <summary>
        /// 海出业务员单
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        #endregion


        /// <summary>
        /// 发送询价询问信息到RespondBy
        /// </summary>
        /// <param name="paramInquireObj">传入对象</param>
        public void SendEmailToRespondBy(BaseInquireRate paramInquireObj)
        {
            //询价对象为空则返回
            if (paramInquireObj == null)
                return;
            //邮件对象
            Message.ServiceInterface.Message message = null;
            //邮件模板Key
            string itemKey = string.Empty;
            try
            {
                //设置模板Key
                itemKey = "InquireRateRequest";

                //无询价人则返回 
                if (paramInquireObj.InquireBysList == null)
                    return;
                //设置邮件主题中显示的用户名(English Name)
                paramInquireObj.InquireByName = LocalData.UserInfo.UserEname;
                #region Build Message
                message = CreateMessageInfo(MessageType.Email,
                            MessageWay.Send,
                            GetEmailAddress(paramInquireObj.RespondByID), //"taylorzhou@cityocean.com",
                            "ICPservice@cityocean.com",
                            FormType.Unknown,
                            OperationType.InquireRate,
                            paramInquireObj.ID, Guid.Empty, string.Empty, string.Empty,
                            LocalData.UserInfo.EmailAddress,  //"taylorzhou@cityocean.com",
                            string.Empty,null, null);
                object[] values = { paramInquireObj };
                message = MainCenterEmailTemplateGetter.ReturnMessage(message, true, itemKey, values);
                #endregion

                #region Send Message
                message.BodyFormat = BodyFormat.olFormatHTML;
                message.State = MessageState.Success;
                MessageService.Send(message);
                #endregion
            }
            finally
            {
                paramInquireObj = null;
                message = null;
            }
        }

        /// <summary>
        /// 发送询价结果到InquireBy
        /// </summary>
        /// <param name="inquireRateList">传入集合</param>
        public void SendEmailToInquireBy(List<BaseInquireRate> inquireRateList)
        {
            //主询价
            BaseInquireRate inquireObj;
            //询价人集合
            string strInquireBys = string.Empty;
            //询价人邮件集合
            string strInquireByEmails = string.Empty;
            //询价明细
            StringBuilder items = null;
            //消息
            Message.ServiceInterface.Message message = null;
            //未处理询价人集合
            List<InquirePriceInquireBys> unHandledInquierBys = null;
            //邮件模板Key
            string itemKey = string.Empty;
            try
            {
                itemKey = "InquireRateResult";
                //获取主询价
                inquireObj = inquireRateList.Find(p => p.MainRecordID == null);
                if (inquireObj == null)
                    return;
                //无询价人则返回 
                if (inquireObj.InquireBysList == null)
                    return;
                //获取未处理询价人集合AND邮箱地址
                unHandledInquierBys = inquireObj.InquireBysList.Where(item => !item.Handled).ToList();
                //无未处理询价人
                if (unHandledInquierBys.Count <= 0)
                    return;

                #region Build InquierBy

                //初始化
                strInquireBys = "";
                strInquireByEmails = "";
                //获取未处理询价人集合 AND 邮箱地址
                foreach (InquirePriceInquireBys item in unHandledInquierBys)
                {
                    //使用逗号拼接询价人名称
                    strInquireBys += item.InquireByCName + ",";
                    strInquireByEmails += GetEmailAddress(item.InquireByID) + ";";
                }
                //移除最后询价人字符串
                strInquireBys = strInquireBys.Substring(0, strInquireBys.Length - 1);
                inquireObj.InquireByName = strInquireBys;
                #endregion

                #region Build Items
                items = new StringBuilder();
                for (var index = 0; index < inquireRateList.Count; index++)
                {
                    BaseInquireRate item = inquireRateList[index];
                    if (item.IsMain)
                        continue;
                    //使用逗号拼接箱型
                    string _strUnitRate = item.UnitRates.Aggregate("",
                        (current, itemUnit) => current + (itemUnit.UnitName + "=" + itemUnit.Rate.ToString() + " , "));
                    //移除最后逗号
                    _strUnitRate = _strUnitRate.Substring(0, _strUnitRate.Length - 1);
                    items.Append("    ");
                    items.AppendFormat(
                        "Item{0}: Carrier={1}, Currency={2}, {3}, Commodity={4}, Term={5}, Surcharge={6}, Duration={7} <br />"
                        , index + 1                       //项索引
                        , item.CarrierName              //承运人名称
                        , item.CurrencyName             //币种
                        , _strUnitRate                  //集装箱尺寸集合
                        , item.Commodity                //Commodity
                        , item.TransportClauseName      //TransportClauseName
                        , item.TotalSurcharge           //总附加费
                        ,                               //DurationFrom - DurationTo
                        (item.DurationFrom.HasValue ? Convert.ToDateTime(item.DurationFrom).ToString("dd/MM/yyyy") : "") +
                        "-"
                        + (item.DurationTo.HasValue ? Convert.ToDateTime(item.DurationTo).ToString("dd/MM/yyyy") : "")
                        );
                }

                #endregion

                #region Build Message
                message = CreateMessageInfo(MessageType.Email,MessageWay.Send,
                                                            strInquireByEmails, //"taylorzhou@cityocean.com",
                                                            "ICPservice@cityocean.com",//系统发送
                                                            FormType.Unknown,
                                                            OperationType.InquireRate,
                                                            inquireObj.ID, Guid.Empty,
                                                           string.Empty, string.Empty,
                                                             LocalData.UserInfo.EmailAddress, //"taylorzhou@cityocean.com",
                                                            string.Empty,null, null);
                object[] values = { inquireObj, items };
                message = MainCenterEmailTemplateGetter.ReturnMessage(message, true, itemKey, values);
                #endregion

                #region Send Email
                message.BodyFormat = BodyFormat.olFormatHTML;
                message.State = MessageState.Success;
                MessageService.Send(message);
                #endregion
            }
            finally
            {
                if (inquireRateList != null)
                {
                    inquireRateList.Clear();
                    inquireRateList = null;
                }
                if (unHandledInquierBys != null)
                {
                    unHandledInquierBys.Clear();
                    unHandledInquierBys = null;
                }
                inquireObj = null;
                items = null;
                strInquireBys = string.Empty;
                strInquireByEmails = string.Empty;
                message = null;
            }
        }

        /// <summary>
        /// 确认后通知
        /// </summary>
        /// <param name="BookingID">业务ID</param>
        /// <param name="paramInquireObj">询价对象</param>
        public void MailBookingConfirm(Guid BookingID, BaseInquireRate paramInquireObj)
        {
            WaitCallback fire = (notifyInfo) =>
            {
                //获取海出业务信息
                OceanBookingInfo oceanbookinginfo = null;
                //消息
                Message.ServiceInterface.Message message = null;
                //事件代码
                EventCode eventCodes = null;
                //询价箱型明细
                string _strUnitRate = string.Empty;
                //邮件模板Key
                string itemKey = string.Empty;
                //发送至邮箱
                string sentTo = string.Empty;
                try
                {
                    //获取海出业务信息
                    oceanbookinginfo = OceanExportService.GetOceanBookingInfo(BookingID);
                    if (oceanbookinginfo == null) return;
                    //获取订舱员邮件地址
                    sentTo = GetEmailAddress(oceanbookinginfo.BookingByID) + ";";
                    //获取客服员邮件地址
                    sentTo += GetEmailAddress(oceanbookinginfo.BookingerID) + ";";
                    //获取文件员邮件地址
                    sentTo += GetEmailAddress(oceanbookinginfo.FilerId) + ";";
                    itemKey = "InquireRateBookingConfirm";

                    //获取SOCCD事件
                    eventCodes = EventCodeList("SOCCD");

                    #region Build Unit Rate

                    _strUnitRate = string.IsNullOrEmpty(paramInquireObj.UnitRateString) ? "" : paramInquireObj.UnitRateString;

                    #endregion

                    #region Build Event

                    var eventObjects = new EventObjects
                    {
                        OperationID = oceanbookinginfo.ID,
                        OperationType = OperationType.OceanExport,
                        FormID = oceanbookinginfo.ID,
                        FormType = FormType.Unknown,
                        Code = eventCodes.Code,
                        Description = eventCodes.Subject,
                        Subject = eventCodes.Subject,
                        Priority = MemoPriority.Normal,
                        UpdateDate = DateTime.Now,
                        Owner = LocalData.UserInfo.LoginName,
                        UpdateBy = LocalData.UserInfo.LoginID,
                        CategoryName = eventCodes.Category,
                        IsShowAgent = true,
                        IsShowCustomer = true,
                        Type = MemoType.EmailLog
                    };

                    #endregion

                    #region Build Message

                    message = CreateMessageInfo(MessageType.Email, MessageWay.Send,
                        sentTo,
                        LocalData.UserInfo.EmailAddress,
                        FormType.Unknown,
                        OperationType.InquireRate,
                        oceanbookinginfo.ID, Guid.Empty,
                        string.Empty, string.Empty,
                        LocalData.UserInfo.EmailAddress,
                        string.Empty, eventObjects, null);
                    object[] values = {oceanbookinginfo, paramInquireObj, _strUnitRate};
                    message = MainCenterEmailTemplateGetter.ReturnMessage(message, true, itemKey, values);

                    #endregion

                    #region Send Email

                    message.BodyFormat = BodyFormat.olFormatHTML;
                    message.State = MessageState.Success;
                    MessageService.Send(message);

                    #endregion

                    #region Relation Ocean Booking

                    List<OperationMessageRelation> messageRelations = new List<OperationMessageRelation>();
                    OperationMessageRelation relation = new OperationMessageRelation();
                    relation.HasData = true;
                    relation.OperationID = oceanbookinginfo.ID;
                    relation.OperationType = OperationType.OceanExport;
                    relation.MessageId = message.MessageId;
                    relation.FormType = message.UserProperties.FormType;
                    relation.ID = oceanbookinginfo.ID;

                    if (message.UserProperties.ContainsKey("UpdateDate"))
                    {
                        relation.UpdateDate = (DateTime?) message.UserProperties["UpdateDate"];
                    }

                    if (message.UserProperties.ContainsKey("ContactStage"))
                    {
                        object objContactStage = message.UserProperties["ContactStage"];
                        if (objContactStage == null || objContactStage == "")
                            relation.ContactStage = ContactStage.Unknown;
                        else
                            relation.ContactStage =
                                (ContactStage?) Enum.Parse(typeof (ContactStage), objContactStage.ToString());
                    }
                    else
                        relation.ContactStage = ContactStage.Unknown;
                    messageRelations.Add(relation);
                    if (OperationMessageRelationService != null)
                        OperationMessageRelationService.SaveOperationMailMessage(messageRelations.ToArray());

                    #endregion
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _strUnitRate = string.Empty;
                    message = null;
                    eventCodes = null;
                    oceanbookinginfo = null;
                    itemKey = string.Empty;
                }
            };

            ThreadPool.QueueUserWorkItem(fire);
        }

        /// <summary>
        /// 获取Email Address
        ///     通过用户ID获取用户邮件地址，未找到用户则返回空文本
        /// </summary>
        /// <param name="ID">用户ID</param>
        /// <returns>Email Address:未找到则返回空文本</returns>
        private string GetEmailAddress(Guid? ID)
        {
            //获取邮箱地址
            string strSendEmailAddress = "";
            if (ID != null)
            {
                //获取用户详细信息
                UserInfo inquireByInfo = UserService.GetUserInfo(new Guid(ID.ToString()));
                if (inquireByInfo != null)
                    strSendEmailAddress = inquireByInfo.EMail;
            }
            strSendEmailAddress = strSendEmailAddress == string.Empty ? "" : strSendEmailAddress;
            return strSendEmailAddress;
        }

        /// <summary>
        /// 返回消息实体类
        /// </summary>
        /// <param name="type">发送类型</param>
        /// <param name="way">发送方向</param>
        /// <param name="sendTo">接收人邮箱</param>
        /// <param name="sendFrom">发送人邮箱</param>
        /// <param name="operationType">操作类型</param>
        /// <param name="operationId">操作ID</param>
        /// <param name="formId">表单ID</param>
        /// <param name="body">发送内容</param>
        /// <param name="subject">主题</param>
        /// <param name="cc">邮件抄送地址</param>
        /// <param name="action">操作动作</param>
        /// <param name="eventObjects">事件对象</param>
        /// <param name="attachmentContents">邮件附件信息</param>
        /// <returns></returns>
        public Message.ServiceInterface.Message
            CreateMessageInfo(MessageType type,
            MessageWay way, string sendTo, string sendFrom, FormType formType,
            OperationType operationType, Guid operationId, Guid formId, string body, string subject, string cc,
            string action, EventObjects eventObjects, List<AttachmentContent> attachmentContents)
        {
            // 邮件发送的消息实体
            var message = new Message.ServiceInterface.Message();

            message.Type = type;                //消息类型
            message.Way = way;                  //消息方向
            message.SendTo = sendTo;            //收件人邮箱地址
            message.SendFrom = sendFrom;        //发件人邮箱地址
            message.CC = cc;                    //邮件抄送地址

            message.UserProperties = new MessageUserPropertiesObject
            {
                FormType = formType,
                OperationType = operationType,
                OperationId = operationId,
                FormId = formId
            };
            if (!string.IsNullOrEmpty(action))
            {
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

        private List<EventCode> eventCodeList = new List<EventCode>();
        /// <summary>
        /// 返回当前CODE的事件详细信息
        /// </summary>
        /// <returns></returns>
        public EventCode EventCodeList(string code)
        {
            if (eventCodeList.Any() == false)
            {
                eventCodeList = FCMCommonService.GetEventCodeList(OperationType.OceanExport);
            }
            return eventCodeList.FirstOrDefault(n => n.Code == code);
        }
    }
}
