using System;
using System.Collections.Generic;
using System.Linq;
using ICP.DataCache.ServiceInterface;
using ICP.Message.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;
using ICP.FCM.Common.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.FileSystem.ServiceInterface;

namespace ICP.Message.Business
{
    public class MessageService : IMessageService
    {
        private ICommonMessageService _commonMessageService;
        private IOperationMessageRelationService operationMessageRelationService;
        private IMessageEDILogRelationService _messageEDILogRelationService;
        private IBusinessQueryService _businessQueryService;
        private IFCMCommonService _fcmCommonService;
        public MessageService(IFCMCommonService fcmCommonService, ICommonMessageService commonMessageService, IOperationMessageRelationService operationMessageRelationService, IMessageEDILogRelationService messageEDILogRelationService, IBusinessQueryService businessQueryService)
        {
            _fcmCommonService = fcmCommonService;
            _commonMessageService = commonMessageService;
            this.operationMessageRelationService = operationMessageRelationService;
            _messageEDILogRelationService = messageEDILogRelationService;
            _businessQueryService = businessQueryService;

        }
        #region IMessageService 成员

        public ServiceInterface.Message Get(Guid id)
        {
            ServiceInterface.Message message = _commonMessageService.Get(id);
            if (message == null) return null;

            if (message.Type == MessageType.EDI)
            {
                message.UserProperties = new MessageUserPropertiesObject();
                message.UserProperties.FormId = Guid.Empty;
                message.UserProperties.OperationId = Guid.Empty;
                message.UserProperties.OperationType = OperationType.Unknown;
                message.UserProperties.EDILogRelation = _messageEDILogRelationService.Get(id);
            }
            return message;
        }

        public ServiceInterface.Message GetMessageByMessageId(string messageId)
        {
            return _commonMessageService.GetMessageByMessageId(messageId);
        }

        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public ManyResult[] Save(ServiceInterface.Message message)
        {

            ManyResult[] results = _commonMessageService.Save(message);

            Guid messageId = results[0].Items[0].GetValue<Guid>("ID");
            message.Id = messageId;

            if (message.Type == MessageType.EDI)
            {
                SaveEDILog(message, messageId);
            }

            //保存邮件事件代码
            if (message.UserProperties != null && message.UserProperties.OperationId != Guid.Empty)
            {
                if (message.UserProperties.EventInfo != null && !message.UserProperties.EventInfo.Code.Equals("Nothing"))
                {
                    EventObjects info = message.UserProperties.EventInfo;
                    info.MessageID = messageId;
                    _fcmCommonService.SaveMemoInfo(info);
                }
                if (!string.IsNullOrEmpty(message.UserProperties.Action))
                {
                    List<BusinessSaveParameter> listBusinessParameter = new List<BusinessSaveParameter>();
                    BusinessSaveParameter parameter = new BusinessSaveParameter();
                    parameter["OceanBookingID"] = message.UserProperties.OperationId;
                    if (message.UserProperties.FormId != Guid.Empty)
                    {
                        parameter["BLID"] = message.UserProperties.FormId;
                    }
                    parameter["OperationType"] = (int)message.UserProperties.OperationType;
                    //if (message.UserProperties.Contains("UpdateDate"))
                    //parameter["UpdateDate"] = results[0].Items[0].GetValue<DateTime?>("UpdateDate");
                    parameter[message.UserProperties.Action] = 1;
                    listBusinessParameter.Add(parameter);
                    _businessQueryService.Save(listBusinessParameter);
                    listBusinessParameter = null;

                }
            }

            List<OperationMessageRelation> messageRelations = new List<OperationMessageRelation>();
            //是否有对应的操作日志，
            OperationMessageRelation relation = MessageUtility.GetOperationMessageRelation(message);
            if (relation != null && relation.HasData)
            {
                relation.IMessageId = messageId;
                relation.MessageId = message.MessageId;

                messageRelations.Add(relation);
            }
            //用户直接在邮件中心回复邮件，需要处理原邮件是否关联业务，如果关联，则需要新建关联并保存
            else if (message.UserProperties != null && message.UserProperties.OperationId != Guid.Empty && !string.IsNullOrEmpty(message.UserProperties.Reference))
            {
                messageRelations = operationMessageRelationService.GetByMessageIdAndReference(message.MessageId, message.UserProperties.Reference);
                if (messageRelations != null && messageRelations.Count > 0)
                {
                    foreach (var item in messageRelations)
                    {
                        item.HasData = true;
                        item.RelationType = MessageRelationType.Auto;
                        item.UpdateDataType = UpdateDataType.AddNew;
                        item.IMessageId = message.Id;
                        item.MessageId = message.MessageId;
                        item.CreateBy = ApplicationContext.Current.UserId;
                        item.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    };

                }
            }

            if (messageRelations != null && messageRelations.Count > 0)
            {
                ManyResult relationResult = operationMessageRelationService.SaveOperationMailMessage(messageRelations.ToArray());

                ManyResult temp = new ManyResult();
                temp.Items.Add(relationResult);
                List<ManyResult> result2 = results.ToList();

                result2.Add(temp);
                results = result2.ToArray();
            }

            return results;
        }

        private void SaveEDILog(ServiceInterface.Message messageInfo, Guid messageID)
        {
            MessageEDILogRelation ediLogRelation = messageInfo.UserProperties.EDILogRelation;
            ediLogRelation.IMessageId = messageID;
            _messageEDILogRelationService.Save(ediLogRelation);
        }

        public void Remove(Guid[] ids, DateTime?[] updateDates)
        {
            _commonMessageService.Remove(ids, updateDates);
        }



        public SingleResultData ChangeFlag(Guid id, MessageFlag flag, DateTime? updateDate)
        {
            return _commonMessageService.ChangeFlag(id, flag, updateDate);
        }

        public ManyResult ChangeState(Guid[] id, MessageState[] state, DateTime?[] updateDate)
        {
            return _commonMessageService.ChangeState(id, state, updateDate);
        }

        #endregion

        #region IMessageService 成员


        public void Reply(Guid originalId, DateTime? updateDate, ServiceInterface.Message message)
        {
            _commonMessageService.Reply(originalId, updateDate, message);
        }

        public void Forward(Guid originalId, DateTime? updateDate, ServiceInterface.Message message)
        {
            _commonMessageService.Forward(originalId, updateDate, message);
        }
        public void ChangeState(string[] messageIds, MessageState[] states, MessageType type)
        {
            try
            {
                _commonMessageService.ChangeState(messageIds, states, type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SingleResult Send(ServiceInterface.Message message)
        {
            SingleResult result = null; Guid messageId = Guid.Empty; ManyResult[] results = null;
            try
            {
                result = _commonMessageService.Send(message);
                message.MessageId = result.GetValue<string>("MessageID");
                message.SendFrom = result.GetValue<string>("SendFrom");
                results = Save(message);

                ChangedState(results, MessageState.Success, message.Type);

            }
            catch (Exception ex)
            {
                if (message.Type == MessageType.Fax)
                {
                    ChangedState(results, MessageState.Failure, message.Type);
                }
            }

            return result;
        }

        private void ChangedState(ManyResult[] result, MessageState state, MessageType type)
        {
            if (result != null && result.Length > 0)
            {
                if (result[0].Items != null && result[0].Items.Count > 0)
                {
                    ChangeState(new string[1] { result[0].Items[0].GetValue<Guid>("ID").ToString() }, new MessageState[1] { state }, type);
                }
            }
        }

        public List<ServiceInterface.Message> CustomerMailList(Guid operationId, string values)
        {
            return _commonMessageService.CustomerMailList(operationId, values);
        }
        #endregion

        #region IMessageService 成员


        public void Transfer(List<ConfigureObjects> userCompanyList, Guid defaultCompanyID)
        {
            _commonMessageService.Transfer(userCompanyList, defaultCompanyID);
        }
        public List<FaxMessageObjects> GetMessageInfoByCompanyID(Guid companyID)
        {
            return _commonMessageService.GetMessageInfoByCompanyID(companyID);
        }

        public ManyResult ChangeFaxState(Guid[] ids, Guid?[] folderIds, ReceiveFaxState[] states, DateTime?[] updateDates, DateTime?[] faxUpdateDates)
        {
            return _commonMessageService.ChangeFaxState(ids, folderIds, states, updateDates, faxUpdateDates);
        }

        public ServiceInterface.Message GetMessageInfoById(Guid id)
        {
            return _commonMessageService.GetMessageInfoById(id);
        }

        public List<ServiceInterface.Message> RuturnMailList(Guid userId, string words, int wordType, string refNo, int refNoType, string customerName, int customerType, int phase, string mails, int mailType, int dateType, DateTime fromDate, DateTime endDate)
        {
            return _commonMessageService.RuturnMailList(userId, words, wordType, refNo, refNoType, customerName, customerType, phase, mails, mailType, dateType, fromDate, endDate);
        }

        public List<ContentInfo> GetMailInfo(string messageId)
        {
            return _commonMessageService.GetMailInfo(messageId);
        }

        #endregion

    }
}
