#region Comment

/*
 * 
 * FileName:    DataCacheOperationService.cs
 * CreatedOn:   2015/5/21 16:38:51
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;

namespace ICP.DataCache.LocalOperation
{
    /// <summary>
    /// 缓存数据库操作服务
    /// </summary>
    public class DataCacheOperationService : IDataCacheOperationService
    {
        #region Servies
        /// <summary>
        /// 关联数据服务
        /// </summary>
        public IOperationMessageRelationService OperationMessageRelationService
        {
            get { return ServiceClient.GetService<IOperationMessageRelationService>(); }
        }
        /// <summary>
        /// 业务查询服务
        /// </summary>
        public IBusinessQueryService BusinessQueryService
        {
            get { return ServiceClient.GetService<IBusinessQueryService>(); }
        }

        /// <summary>
        /// 缓存文件操作服务
        /// </summary>
        public ILocalBusinessCacheDataOperation LocalBusinessCacheDataOperation
        {
            get { return ServiceClient.GetClientService<ILocalBusinessCacheDataOperation>(); }
        }

        /// <summary>
        /// FCN公用服务
        /// </summary>
        public IFCMCommonService FcmCommonService
        {
            get { return ServiceClient.GetService<IFCMCommonService>(); }
        }

        public ILocalBusinessContactService ClientBusinessOperationService
        {
            get
            {
                return new LocalBusinessContactService();
            }
        }
        public IBusinessContactService BusinessContactService
        {
            get
            {
                return ServiceClient.GetService<IBusinessContactService>();
            }
        }
        #endregion

        /// <summary>
        /// 获取所有联系人
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllContact()
        {
            return LocalBusinessCacheDataOperation.GetAllContact();
        }

        /// <summary>
        /// 获取邮件关联信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllOperationMessageRelation()
        {
            return LocalBusinessCacheDataOperation.GetAllOperationMessageRelation();
        }

        /// <summary>
        /// 获取所有本地缓存业务数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllOperationViewInfo()
        {
            return LocalBusinessCacheDataOperation.GetAllOperationViewInfo();
        }

        /// <summary>
        /// 通过OperationID、OperationType获取本地缓存业务数据
        /// </summary>
        /// <param name="operationIDs">OperationID集合</param>
        /// <returns></returns>
        public DataTable GetOperationViewInfoByOperationIDs(Guid[] operationIDs)
        {
            return LocalBusinessCacheDataOperation.GetOperationViewInfoByOperationIDs(operationIDs);
        }

        /// <summary>
        /// 获取邮件关联信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public List<OperationMessageRelation> GetOperationMessageRelationByMessageIdAndReference(string messageId, string reference)
        {
            List<OperationMessageRelation> messageRelation = GetLocalOperationMessageRelationByMessageIdAndReference(messageId, reference);
            if (messageRelation != null && messageRelation.Count > 0)
                return messageRelation;
            try
            {
                messageRelation = OperationMessageRelationService.GetByMessageIdAndReference(messageId, reference);
                if (messageRelation != null && messageRelation.Count > 0)
                    LocalBusinessCacheDataOperation.SaveOperationMessageRelation(messageRelation.ToArray());
            }
            catch
            {
                messageRelation = null;
            }
            return messageRelation;
        }

        /// <summary>
        /// 获取邮件关联信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public List<OperationMessageRelation> GetLocalOperationMessageRelationByMessageIdAndReference(string messageId, string reference)
        {
            return LocalBusinessCacheDataOperation.GetOperationMessageRelationByMessageIdAndReference(messageId, reference);
        }

        /// <summary>
        /// 根据邮件ID，获取关联信息，返回Datatable
        /// </summary>
        /// <param name="messageID">邮件MessageID</param>
        /// <returns>DataTable</returns>
        public DataTable GetOperationMessageRelationDataTableByMessageID(string messageID)
        {
            return LocalBusinessCacheDataOperation.GetOperationMessageRelationByMessageId(messageID);
        }

        /// <summary>
        /// 根据邮件ID，获取关联信息，返回单个对象
        /// </summary>
        /// <param name="messageID">邮件MessageID</param>
        /// <returns>OperationMessageRelation</returns>
        public OperationMessageRelation GetOperationMessageRelationByMessageID(string messageID)
        {
            return LocalBusinessCacheDataOperation.GetOperationMessageRelationByMessageID(messageID);
        }


        /// <summary>
        /// 查找业务集合：固定SQL
        /// </summary>
        /// <param name="criteria">业务查询信息实体类</param>
        /// <rereturns>DataTable</rereturns>
        public DataTable GetOperationViewListFixed(BusinessQueryCriteria criteria)
        {
            return LocalBusinessCacheDataOperation.GetOperationViewListFixed(criteria);
        }

        /// <summary>
        /// 联系人是否存在缓存
        /// </summary>
        /// <param name="ExternalEmails">联系人集合</param>
        /// <returns>是否存在:True存在；False不存在；</returns>
        public bool IsAllContactExsitCache(List<string> ExternalEmails)
        {
            return LocalBusinessCacheDataOperation.IsAllContactExsitCache(ExternalEmails);
        }

        /// <summary>
        /// 保存本地缓存邮件关联业务信息
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        public void SaveLocalOperationMessageRelation(OperationMessageRelation[] relation)
        {
            LocalBusinessCacheDataOperation.SaveOperationMessageRelation(relation);
        }
        /// <summary>
        /// 保存邮件与业务的关联信息
        /// </summary>
        /// <param name="relationMessages"></param>
        /// <returns></returns>
        public ManyResult SaveOperationMessageRelation(OperationMessageRelation[] relationMessages)
        {
            foreach (var item in relationMessages)
            {
                
                if (item.OperationID != Guid.Empty)
                {
                    List<Guid> operationIds = new List<Guid> {item.OperationID};
                    //判断业务信息是否存在
                    if (!LocalBusinessCacheDataOperation.IsShipmentExists(item.OperationID, item.OperationType))
                    {
                        DataTable temp = BusinessQueryService.GetOperationInfo(operationIds, item.OperationType);
                        LocalBusinessCacheDataOperation.UpdateLocalBusinessData(operationIds, item.OperationType, temp);
                    }
                    else
                        LocalBusinessCacheDataOperation.PushBusinessDataToOutlook(operationIds.ToArray());
                    
                }
            }
            return SaveAndSyncOperationMessageRelation(relationMessages);
        }

        /// <summary>
        /// 保存联系人
        /// </summary>
        /// <param name="ContactParameters">联系人集合</param>
        public void SaveLocalOperationContactMail(List<OperationContactParameters> ContactParameters)
        {
            LocalBusinessCacheDataOperation.SaveOperationContactMail(ContactParameters);
        }

        /// <summary>
        /// 删除服务端数据库关联信息和本地缓存关联信息
        /// </summary>
        /// <param name="messageRelationIds">关联信息ID集合</param>
        /// <param name="updateDates">更新时间</param>
        public void RemoveAndSyncOperationMessageRelations(Guid[] messageRelationIds, DateTime?[] updateDates)
        {
            OperationMessageRelationService.RemoveOperationMessagesByIDs(messageRelationIds, updateDates);
            LocalBusinessCacheDataOperation.RemoveOperationMessageRelations(messageRelationIds);
        }

        /// <summary>
        /// 根据邮件地址，获取联系人类型
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns>如果本地不存在，则返回null</returns>
        public int? GetContactPersonType(string emailAddress)
        {
            return LocalBusinessCacheDataOperation.GetContactPersonType(emailAddress);
        }

        /// <summary>
        /// 关联的方法
        /// </summary>
        /// <param name="contactType">联系人类型</param>
        /// <param name="bussOperationContexts">需要关联的业务集合</param>
        /// <param name="message">Message对象</param>
        public void SaveContactList(ContactType contactType
            , List<BusinessOperationContext> bussOperationContexts, Message.ServiceInterface.Message message)
        {
            if (bussOperationContexts != null && bussOperationContexts.Count > 0)
            {
                List<MailContactInfo> mailContactList = MailContactInfo.GetAllExternalContacts(message);
                List<CustomerCarrierObjects> currentShipmentCustomerCarriers = new List<CustomerCarrierObjects>();

                //外部联系人
                for (int index = 0; index < bussOperationContexts.Count; index++)
                {
                    BusinessOperationContext bocItem = bussOperationContexts[index];
                    currentShipmentCustomerCarriers.Clear();
                    //转换当前业务联系人
                    currentShipmentCustomerCarriers.AddRange(mailContactList.Select(mailContactInfo => CreateEntity(bocItem.OperationID, mailContactInfo, contactType)));

                    //找到当前业务联系人
                    List<CustomerCarrierObjects> searchShipmentCustomerCarriers = FcmCommonService.GetContactList(bocItem.OperationID, bocItem.OperationType).CustomerCarrier;

                    if (searchShipmentCustomerCarriers != null && searchShipmentCustomerCarriers.Count > 0)
                    {
                        //找到第一票业务联系人
                        List<CustomerCarrierObjects> firstShipmentCustomerCarriers =
                            Framework.ClientComponents.Controls.Utility.Clone(currentShipmentCustomerCarriers);
                        if (firstShipmentCustomerCarriers != null && firstShipmentCustomerCarriers.Count > 0)
                        {
                            List<CustomerCarrierObjects> newCustomerCarriers =
                                ReplaceCustomerCarriers(searchShipmentCustomerCarriers, firstShipmentCustomerCarriers, bocItem.OperationID, bocItem.OperationType);
                            FcmCommonService.SaveContactList(newCustomerCarriers);
                        }
                    }
                    else
                    {
                        List<CustomerCarrierObjects> firstShipmentCustomerCarriers =
                           Framework.ClientComponents.Controls.Utility.Clone(currentShipmentCustomerCarriers);
                        foreach (var item in firstShipmentCustomerCarriers)
                        {
                            item.OceanBookingID = bocItem.OperationID;
                            item.OperationType = bocItem.OperationType;
                            item.Id = Guid.NewGuid();
                            item.UpdateDate = null;
                        }

                        FcmCommonService.SaveContactList(firstShipmentCustomerCarriers);
                    }
                    List<string> emailList = mailContactList.Select(item => item.EmailAddress).ToList();
                    List<OperationContactInfo> contactList = BusinessContactService.GetOperationContactByEmails(emailList);
                    ClientBusinessOperationService.LocalSaveOperationContacts(contactList);
                    List<Guid> operationIds = new List<Guid>() {bocItem.OperationID};
                    DataTable temp = BusinessQueryService.GetOperationInfo(operationIds, bocItem.OperationType);
                    LocalBusinessCacheDataOperation.UpdateLocalBusinessData(operationIds, bocItem.OperationType, temp);
                }
            }
        }

        /// <summary>
        /// 写入操作日志
        /// </summary>
        /// <param name="AssemblyNames">组件名称</param>
        /// <param name="ExecuteType">操作类型</param>
        /// <param name="ExecuteDescription">操作内容</param>
        /// <param name="Content">操作时长(ms)</param>
        public void WriteStopwatchLog(string AssemblyNames, string ExecuteType, string ExecuteDescription, string Content)
        {
            LocalBusinessCacheDataOperation.AddOperationLog(Guid.Empty,DateTime.Now, AssemblyNames, ExecuteType, ExecuteDescription, Content);
        }

        /// <summary>
        /// 服务端保存和同步客户端关联信息
        /// </summary>
        /// <param name="messageRelations"></param>
        /// <returns></returns>
        private ManyResult SaveAndSyncOperationMessageRelation(OperationMessageRelation[] messageRelations)
        {
            ManyResult results = OperationMessageRelationService.SaveOperationMailMessage(messageRelations);//保存消息关联操作日志
            if (results != null)
            {
                int count = results.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    messageRelations[i].ID = results.Items[i].GetValue<Guid>("ID");
                    messageRelations[i].UpdateDate = results.Items[i].GetValue<DateTime?>("UpdateDate");
                    messageRelations[i].XmlMessageInfo = null;
                    messageRelations[i].Contacts = null;
                    messageRelations[i].UploadServer = true;
                }
                SaveLocalOperationMessageRelation(messageRelations);
            }
            return results;
        }

        private CustomerCarrierObjects CreateEntity(Guid operationID, MailContactInfo mailContactInfo,ContactType contactType)
        {
            CustomerCarrierObjects customerCarrier = new CustomerCarrierObjects();
            #region  根据传递过来的默认选择用户类型的参数选择客户的默认类型
            FCMInterfaceUtility.SetStage(customerCarrier, ContactStage.Unknown);
            #endregion
            customerCarrier.IsCC = (mailContactInfo.ContactType == MailContactType.olCC);
            customerCarrier.CreateDate = DateTime.Now;
            customerCarrier.CreateByID = LocalData.UserInfo.LoginID;
            customerCarrier.UpdateDate = DateTime.Now;
            customerCarrier.UpdateByID = LocalData.UserInfo.LoginID;
            customerCarrier.Type = contactType;
            customerCarrier.OceanBookingID = operationID;
            if (!string.IsNullOrEmpty(mailContactInfo.EmailAddress))
            {
                customerCarrier.Mail = mailContactInfo.EmailAddress;
                customerCarrier.Name = mailContactInfo.Name;
            }
            return customerCarrier;
        }

        /// <summary>
        /// 将用户编辑的联系人替换为更新之前的联系人
        /// </summary>
        /// <param name="currentShipmentCustomerCarriers"></param>
        /// <param name="firstShipmentCustomerCarriers"></param>
        /// <returns></returns>
        private List<CustomerCarrierObjects> ReplaceCustomerCarriers(List<CustomerCarrierObjects> currentShipmentCustomerCarriers, List<CustomerCarrierObjects> firstShipmentCustomerCarriers, Guid operationID, OperationType operationType)
        {
            int count = firstShipmentCustomerCarriers.Count;
            for (int j = 0; j < count; j++)
            {
                //如果当前业务联系人包含第一票业务的联系人  
                CustomerCarrierObjects info = currentShipmentCustomerCarriers.Find(item => item.Mail.Trim().Equals(firstShipmentCustomerCarriers[j].Mail.Trim(), StringComparison.CurrentCultureIgnoreCase));
                if (info != null)
                {
                    firstShipmentCustomerCarriers[j].Id = info.Id;
                    firstShipmentCustomerCarriers[j].OceanBookingID = info.OceanBookingID;
                    firstShipmentCustomerCarriers[j].UpdateDate = info.UpdateDate;
                    firstShipmentCustomerCarriers[j].UpdateByID = info.UpdateByID;
                }
                else
                {
                    firstShipmentCustomerCarriers[j].Id = Guid.NewGuid();
                    firstShipmentCustomerCarriers[j].OperationType = operationType;
                    firstShipmentCustomerCarriers[j].OceanBookingID = operationID;
                }
            }
            return firstShipmentCustomerCarriers;
        }

    }
}
