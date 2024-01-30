#region Comment

/*
 * 
 * FileName:    ClientInquireRateService.cs
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ICP.Common.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.FRM.ServiceInterface;
using ICP.FRM.UI.InquireRates;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenter.ServiceInterface;
using ICP.Message.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;
using FormType = ICP.Framework.CommonLibrary.Common.FormType;

namespace ICP.FRM.UI
{
    /// <summary>
    /// 询价客户端服务实现
    /// </summary>
    public class ClientInquireRateService : IClientInquireRateService
    {
        #region  服务
        [ServiceDependency]
        public WorkItem workItem
        {
            get;
            set;

        }
        /// <summary>
        /// 获取用户信息的接口
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        public IOutLookService OutLookService
        {
            get
            {
                ClientHelper.EnsureEmailCenterAppStarted();
                if (LocalData.ApplicationType ==
                    ApplicationType.EmailCenter)
                {
                    return ServiceClient.GetClientService<IOutLookService>();
                }
                else
                {
                    return ServiceClient.GetService<IOutLookService>();
                }
            }
        }
        /// <summary>
        /// 询价的接口
        /// </summary>
        public IInquireRatesService IiInquireRatesService
        {
            get
            {
                return ServiceClient.GetService<IInquireRatesService>();
            }
        }
        /// <summary>
        /// 邮件发送接口
        /// </summary>
        public IMailCenterTemplateService MailCenterTemplateService
        {
            get
            {
                ClientHelper.EnsureEmailCenterAppStarted();

                return ServiceClient.GetService<IMailCenterTemplateService>();
            }
        }
        #endregion

        /// <summary>
        /// 显示新增海运询价面板
        /// </summary>
        public void InquireOceanRate()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //1.设置标题
                //string titleNo = LocalData.IsEnglish ? "New Inquire Ocean" : "新增海运出口询价";
                //2.显示面板
                PartLoader.ShowEditPart<NewInquireOceanRatePart>(workItem, null, "New Inquire Ocean Rate", null);
            }
        }
        /// <summary>
        /// 显示新增海运询价面板
        /// </summary>
        /// <param name="inquieroceanrate">海运询价实体对象</param>
        /// <param name="editPartSaved"></param>
        public void InquireOceanRate(InquierOceanRate inquieroceanrate, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //string titleNo = LocalData.IsEnglish ? "New Inquire Ocean" : "新增海运出口询价";
                PartLoader.ShowEditPart<NewInquireOceanRatePart>(workItem, inquieroceanrate, "New Inquire Ocean Rate", editPartSaved);
            }
        }
        /// <summary>
        /// 显示海运询价列表面板
        /// </summary>
        public void OpenOceanInquireRateListPart()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                workItem.Commands[FunctionConstants.FRM_InquireRates].Execute();
            }
        }
        /// <summary>
        /// 返回当前询价的实体
        /// </summary>
        /// <param name="inquerireid">询价ID</param>
        /// <returns></returns>
        public InquierOceanRate ReturnInquierOceanRate(Guid inquerireid)
        {
            return IiInquireRatesService.GetInquierOceanRateInfoForInquireBy(inquerireid, LocalData.UserInfo.LoginID);
        }
        /// <summary>
        /// 返回当前人员的详细信息
        /// </summary>
        /// <param name="userId">人员ID</param>
        /// <returns></returns>
        public UserInfo ReturnUserInfo(Guid userId)
        {
            return UserService.GetUserInfo(userId);
        }

        /// <summary>
        /// 根据询价ID查询出当前询问人，并新建一份空白的邮件
        /// </summary>
        /// <param name="inquerireid">询价的ID</param>
        public void EmailMailtoAskpeople(Guid inquerireid)
        {
            //询问人的邮件地址
            string emailAddress = string.Empty;
            var inquierOceanRate = ReturnInquierOceanRate(inquerireid);
            if (inquierOceanRate != null)
            {
                if (inquierOceanRate.InquireBysList.Any())
                {
                    foreach (var item in inquierOceanRate.InquireBysList)
                    {
                        UserInfo userInfo = ReturnUserInfo(item.InquireByID);
                        if (userInfo != null)
                        {
                            //第一次执行
                            if (string.IsNullOrEmpty(emailAddress))
                            {
                                emailAddress = userInfo.EMail;
                            }
                            //多个地址进行累加
                            else if (!string.IsNullOrEmpty(emailAddress) && emailAddress.Contains(userInfo.EMail) == false)
                            {
                                emailAddress = emailAddress + ";" + userInfo.EMail;
                            }
                        }

                    }
                }
            }
            var message = new Message.ServiceInterface.Message
            {
                Type = MessageType.Email,
                Way = MessageWay.Send,
                SendTo = string.IsNullOrEmpty(emailAddress) ? null : emailAddress,
                SendFrom = LocalData.UserInfo.EmailAddress,
                UserProperties = new MessageUserPropertiesObject { Action = string.Empty },
                Body = string.Empty,
                Subject = string.Empty
            };
            OutLookService.Send(message);
        }



        /// <summary>
        /// 邮件发给承运人
        /// </summary>
        /// <param name="inquerireid">询价的ID</param>
        public void EmailtoCarrier(Guid inquerireid)
        {
            var inquierOceanRate = ReturnInquierOceanRate(inquerireid);

            if (inquierOceanRate == null) return;


            Message.ServiceInterface.Message message = CreateMessageInfo(MessageType.Email, MessageWay.Send, string.Empty, LocalData.UserInfo.EmailAddress,
                                                       FormType.Booking, OperationType.InquireRate,
                                                       inquierOceanRate.ID, Guid.Empty,
                                                       string.Empty, string.Empty, LocalData.UserInfo.EmailAddress, string.Empty, null, null);

            /***********
             * 转换对象的原因，客户端发送邮件是需要通过服务端与邮件中心进行通信的，
             * 目前方法只针对于客户端，如需要改服务端发送 则需要把服务修改为服务端方法
             * *****************/
            OceanBookingInfo ocean = new OceanBookingInfo();
            ocean.POLName = inquierOceanRate.POLName;
            ocean.PODName = inquierOceanRate.PODName;
            ocean.Commodity = inquierOceanRate.Commodity;
            ocean.CustomerName = inquierOceanRate.CustomerName;
            ocean.PlaceOfDeliveryName = inquierOceanRate.PlaceOfDeliveryName;
            ocean.TransportClauseName = inquierOceanRate.TransportClauseName;
            MailCenterTemplateService.SendMailWithTemplate(message, true, "MailtoCarrier", new object[] { ocean });
        }
        public Message.ServiceInterface.Message CreateMessageInfo(MessageType type,
           MessageWay way, string sendTo, string sendFrom, FormType formType,
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

            message.UserProperties = new MessageUserPropertiesObject
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
