using ICP.Common.CommandHandler.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.Client;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ICP.DataCache.BusinessOperation
{
    public class ClientBusinessOperationService : IClientBusinessOperationService
    {
        #region 服务
        public ILocalBusinessCacheDataOperation LocalCacheDataOperation
        {
            get
            {
                return ServiceClient.GetClientService<ILocalBusinessCacheDataOperation>();
            }
        }

        [ServiceDependency]
        public IClientBusinessContactService ClientBusinessContactService { get; set; }

        public IOperationMessageRelationService OperationMessageRelationService
        {
            get { return ServiceClient.GetService<IOperationMessageRelationService>(); }
        }

        public IBusinessQueryService BusinessQueryService
        {
            get { return ServiceClient.GetService<IBusinessQueryService>(); }
        }

        public IClientMessageService ClientMessageService
        {
            get {
                return ServiceClient.GetClientService<IClientMessageService>() ?? new ClientMessageService();
            }
        }
        #endregion

        #region IClientBusinessOperationService 成员
        /// <summary>
        /// 查找业务集合：配置文件组建SQL
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public System.Data.DataTable GetLocalOperationViewList(BusinessQueryCriteria criteria)
        {
            return LocalCacheDataOperation.GetOperationViewList(criteria);
        }
        /// <summary>
        /// 查找业务集合：固定SQL
        /// </summary>
        /// <param name="criteria"></param>
        public System.Data.DataTable GetOperationViewListFixed(BusinessQueryCriteria criteria)
        {
            return LocalCacheDataOperation.GetOperationViewListFixed(criteria);
        }

        public List<ICP.Message.ServiceInterface.OperationMessageRelation> GetLocalOperationMessageRelationByMessageIdAndReference(string messageId, string reference)
        {
            return LocalCacheDataOperation.GetOperationMessageRelationByMessageIdAndReference(messageId, reference);
        }
        public List<OperationMessageRelation> GetOperationMessageRelationByMessageIdAndReference(string messageId, string reference)
        {
            List<OperationMessageRelation> messageRelation = GetLocalOperationMessageRelationByMessageIdAndReference(messageId, reference);
            if (messageRelation != null && messageRelation.Count > 0) 
                return messageRelation;
            try
            {
                messageRelation = OperationMessageRelationService.GetByMessageIdAndReference(messageId, reference);
                if (messageRelation != null && messageRelation.Count > 0)
                    LocalCacheDataOperation.SaveOperationMessageRelation(messageRelation.ToArray());
            }
            catch
            {
                messageRelation = null;
            }
            return messageRelation;
        }

        public List<OperationMessageRelation> GetOperationMessages(string messageID, Guid[] operationIDs)
        {
            return LocalCacheDataOperation.GetOperationMessages(messageID, operationIDs);
        }

        public bool HasLocalOperationMessageRelation(string messageID)
        {
            return LocalCacheDataOperation.HasLocalOperationMessageRelation(messageID);
        }

        public OperationMessageRelation GetOperationMessageRelationByMessageID(string messageId)
        {
            return LocalCacheDataOperation.GetOperationMessageRelationByMessageID(messageId);
        }

        public BusinessQueryResult Get(BusinessQueryCriteria criteria, string messageID, string reference)
        {
            return LocalCacheDataOperation.Get(criteria, messageID, reference);
        }

        public DataTable GetLocalOperationViewListBySubjectKeyWord(BusinessQueryCriteria criteria)
        {
            return LocalCacheDataOperation.GetOperationViewListBySubjectKeyWord(criteria);
        }

        public List<OperationMessageRelation> EnsureAndSaveMailMessageReference(OperationMessageRelation relationMessage, Message.ServiceInterface.Message message)
        {
            if (!string.IsNullOrEmpty(relationMessage.MessageId))
            {
                relationMessage.EntryID = message.EntryID;
                //服务端查找关联信息
                var relationList = OperationMessageRelationService.Get(relationMessage.IMessageId);
                if (relationList != null && relationList.Count > 0)
                {
                    //如果当前业务没有关联信息，需要累加关联信息
                    if (relationMessage.OperationID != Guid.Empty)
                    {
                        if (relationList.All(item => item.OperationID != relationMessage.OperationID))
                        {
                            Guid imessageID = relationMessage.IMessageId;
                            OperationMessageRelation messageRelation =
                                FCMInterfaceUtility.CreateOperationMessageRelationInfo(Guid.NewGuid(), null, relationMessage.OperationID
                                , relationMessage.OperationType, relationMessage.MessageId, imessageID, ContactStage.Unknown, relationMessage.EntryID);
                            messageRelation.BackupMail = relationMessage.BackupMail;
                            messageRelation.RelationType = MessageRelationType.Hand;
                            SaveAndSyncOperationMessageRelation(new OperationMessageRelation[1] { messageRelation });
                            relationList.Add(messageRelation);
                        }
                    }

                    return relationList;
                }
                else
                {
                    //找到邮件后，保存关联信息
                    ICP.Message.ServiceInterface.Message messageInfo = ClientMessageService.GetMessageByMessageId(relationMessage.MessageId);
                    if (messageInfo != null)
                    {
                        relationMessage.IMessageId = messageInfo.Id;
                        SaveAndSyncOperationMessageRelation(new OperationMessageRelation[1] { relationMessage });

                        return new List<OperationMessageRelation>(1) { relationMessage };
                    }
                    else
                    {
                        //如果OA.IMessages表中没有找到邮件，就保存邮件，以及邮件关联信息
                        message = MailHelper.GetMessageInfo(message);
                        ManyResult[] results = ClientMessageService.Save(message);
                        bool hasMessageRelation = message.UserProperties != null && message.UserProperties.OperationId != Guid.Empty;
                        if (hasMessageRelation)
                        {
                            //获取服务端关联信息后，同步本地缓存
                            ManyResult relationInfo = results[results.Length - 1];
                            OperationMessageRelation messageRelation =
                                FCMInterfaceUtility.CreateOperationMessageRelationInfo(
                                    (relationInfo.Items[0] as ManyResult).Items[0].GetValue<Guid>("ID"),
                                    (relationInfo.Items[0] as ManyResult).Items[0].GetValue<DateTime?>("UpdateDate"),
                                    relationMessage.OperationID,
                                    relationMessage.OperationType, relationMessage.MessageId,
                                    results[0].Items[0].GetValue<Guid>("ID"), relationMessage.ContactStage
                                    , relationMessage.EntryID);
                            messageRelation.BackupMail = relationMessage.BackupMail;
                            LocalCacheDataOperation.SaveOperationMessageRelation(new OperationMessageRelation[1] { messageRelation });
                            return new List<OperationMessageRelation>(1) { messageRelation };
                        }
                    }
                }
            }
            return new List<OperationMessageRelation>();
        }


        public ManyResult SaveOperationMessageRelation(OperationMessageRelation[] relationMessages)
        {
            foreach (var item in relationMessages)
            {
                //OperationMessageRelation existsRelation = CheckRelationIsChanged(item.MessageId,
                //                                                                 item.UpdateDate);
                //if (existsRelation != null)
                //{
                //    HandleMessageRelationChange(existsRelation);
                //}
                //判断业务信息是否存在
                if (!IsShipmentExists(item.OperationID,
                                                       item.OperationType) && item.OperationID != Guid.Empty)
                {
                    UpdateLocalBusinessData(item.OperationID, item.OperationType);
                }
            }
            return SaveAndSyncOperationMessageRelation(relationMessages);
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


        /// <summary>
        /// 判断是否存在一起关联的情况
        /// </summary>
        /// <param name="relation"></param>
        private void HandleMessageRelationChange(OperationMessageRelation relation)
        {
            Guid operationID = relation.OperationID;
            OperationType operationType = relation.OperationType;
            UpdateLocalBusinessData(operationID, operationType);
            string tip = LocalData.IsEnglish ? "Other user has archived current mail,PLS select other mail in mail list and then select the current mail again to refresh data." : "其他用户已归档此邮件，请在邮件列表选择其他邮件并再次选择当前邮件以刷新数据!";
            throw new ICPException(tip);
        }

        /// <summary>
        /// 根据MessageID删除邮件与业务的关联信息
        /// </summary>
        /// <param name="messageIDs"></param>
        public void RemoveOperationMessageRelation(List<string> messageIDs)
        {
            LocalCacheDataOperation.RemoveOperationMessageRelation(messageIDs);
        }

        /// <summary>
        /// 删除服务端数据库关联信息和本地缓存关联信息
        /// </summary>
        /// <param name="messageRelationIds"></param>
        public void RemoveAndSyncOperationMessageRelations(Guid[] messageRelationIds, DateTime?[] updateDates)
        {
            OperationMessageRelationService.RemoveOperationMessagesByIDs(messageRelationIds, updateDates);
            LocalCacheDataOperation.RemoveOperationMessageRelations(messageRelationIds);
        }

        #endregion

        /// <summary>
        /// 更新本地缓存业务数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <param name="dt"></param>
        public void UpdateLocalBusinessData(Guid operationId, OperationType operationType, DataTable dt)
        {
            UpdateLocalBusinessData(new List<Guid>() { operationId }, operationType, dt);
        }
        /// <summary>
        /// 批量更新缓存业务数据
        /// </summary>
        /// <param name="operationIds"></param>
        /// <param name="operationType"></param>
        /// <param name="dt"></param>
        public void UpdateLocalBusinessData(List<Guid> operationIds, OperationType operationType, DataTable dt)
        {
            LocalCacheDataOperation.UpdateLocalBusinessData(operationIds, operationType, dt);
        }

        public System.Data.DataTable GetLocalOperationViewInfo(Guid operationId, OperationType operationType)
        {
            return LocalCacheDataOperation.GetOperationViewInfo(operationId, operationType);
        }

        public DataTable GetOperationAssistantInfo(Guid operationID, OperationType operationType)
        {
            return LocalCacheDataOperation.GetOperationAssistantInfo(operationID, operationType);
        }

        public void ClearOperationContactEMail()
        {
            LocalCacheDataOperation.ClearOperationContactEMail();
        }



        /// <summary>
        /// 判断业务是否在本地缓存存在
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public bool IsShipmentExists(Guid operationId, OperationType operationType)
        {
            return LocalCacheDataOperation.IsShipmentExists(operationId, operationType);
        }
        /// <summary>
        /// 判断是否存在关联
        /// </summary>
        /// <param name="messageId">消息的MessageID特性</param>
        /// <returns></returns>
        public bool ExistsRelation(string messageId)
        {
            return OperationMessageRelationService.ExistsRelation(messageId);
        }
        /// <summary>
        /// 判断关联是否发送更改,如发送更改则返回关联，否则返回null.如果发送更改，则删除本地缓存的旧有关联，将新的关联保存到本地
        /// </summary>
        /// <param name="messageId">消息的MessageID特性</param>
        /// <param name="updateDate">消息的更改时间</param>
        /// <returns></returns>
        public OperationMessageRelation CheckRelationIsChanged(string messageId, DateTime? updateDate)
        {
            OperationMessageRelation relation = OperationMessageRelationService.CheckRelationIsChanged(messageId, updateDate);
            if (relation == null)
            {
                RemoveOperationMessageRelation(new List<string> { messageId });
                return null;
            }
            else
            {
                RemoveOperationMessageRelation(new List<string> { messageId });

                SaveAndSyncOperationMessageRelation(new OperationMessageRelation[1] { relation });
                return relation;
            }
        }
        /// <summary>
        /// 保存消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ManyResult[] SaveMessage(ICP.Message.ServiceInterface.Message message)
        {
            OperationMessageRelation relation = CheckRelationIsChanged(message.MessageId, null);
            if (relation == null)
            {
                return ClientMessageService.Save(message);
            }
            else
            {
                HandleMessageRelationChange(relation);
                return null;
            }
        }
        public void UpdateLocalBusinessData(List<Guid> operationIds, OperationType operationType)
        {
            DataTable temp = BusinessQueryService.GetOperationInfo(operationIds, operationType);
            UpdateLocalBusinessData(operationIds, operationType, temp);
        }
        /// <summary>
        /// 保存业务与消息的关系信息，并确保本地缓存存在此业务
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="isEnsureShipmentExists"></param>
        /// <returns></returns>
        public SingleResult SaveOperationMessageRelationAndEnsureShipmentExists(OperationMessageRelation relation, bool isEnsureShipmentExists)
        {
            return SaveOperationMessageRelation(new OperationMessageRelation[1] { relation });
        }
        /// <summary>
        /// 更新本地缓存业务数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <param name="dt"></param>
        public void UpdateLocalBusinessData(Guid operationId, OperationType operationType)
        {
            UpdateLocalBusinessData(new List<Guid> { operationId }, operationType);

        }
        /// <summary>
        /// 保存本地缓存邮件关联业务信息
        /// </summary>
        /// <param name="operationRelation"></param>
        /// <returns></returns>
        public void SaveLocalOperationMessageRelation(OperationMessageRelation[] operationRelations)
        {
            LocalCacheDataOperation.SaveOperationMessageRelation(operationRelations);
        }

        #region OperationContact

        /// <summary>
        /// 根据发件人地址获取发件人的类型
        /// </summary>
        /// <param name="senderAddress"></param>
        /// <returns></returns>

        public EmailSourceType GetEmailType(string senderAddress)
        {
            return ClientBusinessContactService.GetEmailType(senderAddress);
        }

        public EmailSourceType[] GetEmailTypes(List<string> emails)
        {
            return ClientBusinessContactService.GetEmailTypes(emails);
        }


        /// <summary>
        /// 根据email从服务端获取OperationContact业务联系人信息
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public List<OperationContactInfo> GetOperationContactByEmails(List<string> emails)
        {
            return ClientBusinessContactService.GetOperationContactByEmails(emails);
        }
        /// <summary>
        /// 从服务端获取业务联系人信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public OperationContactInfo GetOperationContactInfo(string email)
        {
            return ClientBusinessContactService.GetOperationContactInfo(email);
        }
        /// <summary>
        /// 保存业务联系人信息到本地缓存
        /// </summary>
        /// <param name="contactList"></param>
        public void LocalSaveOperationContacts(List<OperationContactInfo> contactList)
        {
            ClientBusinessContactService.LocalSaveOperationContacts(contactList);
        }

        /// <summary>
        /// 移除内存缓存的联系人类型信息
        /// </summary>
        /// <param name="emailAddresses"></param>
        public void RemoveMemCacheContacts(List<string> emailAddresses)
        {
            ClientBusinessContactService.RemoveMemCacheContacts(emailAddresses);
        }
        #endregion

        #region IBusinessContactService 成员

        /// <summary>
        /// 获取所有联系人信息(同步ICP通讯录)
        /// </summary>
        /// <returns>邮件联系人列表</returns>
        public List<EmailContactInfo> GetEmailContactList()
        {
            return ClientBusinessContactService.GetEmailContactList();
        }

        public void SaveLocalOperationContactMail(List<OperationContactParameters> ContactParameters)
        {
            LocalCacheDataOperation.SaveOperationContactMail(ContactParameters);
        }

        /// <summary>
        /// 联系人是否存在缓存
        /// </summary>
        /// <param name="ExternalEmails"></param>
        /// <returns></returns>
        public bool IsAllContactExsitCache(List<string> ExternalEmails)
        {
            return LocalCacheDataOperation.IsAllContactExsitCache(ExternalEmails);
        }

        /// <summary>
        /// 根据邮件ID，获取关联信息，返回Datatable
        /// </summary>
        public DataTable GetOperationMessageRelationByMessageId(string messageId)
        {
            return LocalCacheDataOperation.GetOperationMessageRelationByMessageId(messageId);
        }

        /// <summary>
        /// 根据邮件地址，获取联系人类型
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns>如果本地不存在，则返回null</returns>
        public int? GetContactPersonType(string emailAddress)
        {
            return LocalCacheDataOperation.GetContactPersonType(emailAddress);
        }
        #endregion

    }
}

