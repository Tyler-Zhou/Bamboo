using System;
using System.Collections.Generic;
using ICP.Message.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;
using ICP.FileSystem.ServiceInterface;

namespace ICP.Message.LocalFunction
{
    public class MessageService : IMessageService
    {
        private ICommonMessageService _commonMessageService;
        public MessageService(ICommonMessageService commonMessageService)
        {
            _commonMessageService = commonMessageService;
        }


        #region IMessageService 成员

        public ICP.Message.ServiceInterface.Message Get(Guid id)
        {
            return _commonMessageService.Get(id);
        }
        public Message.ServiceInterface.Message GetMessageByMessageId(string messageId)
        {
            return _commonMessageService.GetMessageByMessageId(messageId);
        }

        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public ManyResult[] Save(ICP.Message.ServiceInterface.Message message)
        {
            return _commonMessageService.Save(message);
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


        public void Reply(Guid originalId, DateTime? updateDate, ICP.Message.ServiceInterface.Message message)
        {
            _commonMessageService.Reply(originalId, updateDate, message);
        }

        public void Forward(Guid originalId, DateTime? updateDate, ICP.Message.ServiceInterface.Message message)
        {
            _commonMessageService.Forward(originalId, updateDate, message);
        }
        public void ChangeState(string[] messageIds, MessageState[] states, MessageType type)
        {
            _commonMessageService.ChangeState(messageIds, states, type);
        }
        public SingleResult Send(ICP.Message.ServiceInterface.Message message)
        {
            return _commonMessageService.Send(message);
        }
        #endregion

        #region IMessageService 成员


        public void Transfer(List<ConfigureObjects> userCompanyList,Guid defaultCompanyID)
        {
            _commonMessageService.Transfer(userCompanyList, defaultCompanyID);
        }

        public List<FaxMessageObjects> GetMessageInfoByCompanyID(Guid companyID)
        {
            return _commonMessageService.GetMessageInfoByCompanyID(companyID);
        }

        public ManyResult ChangeFaxState(Guid[] ids,Guid?[] folderIds, ReceiveFaxState[] states, DateTime?[] updateDates,DateTime?[] faxUpdateDates)
        {
            return _commonMessageService.ChangeFaxState(ids, folderIds, states, updateDates, faxUpdateDates);
        }
        public Message.ServiceInterface.Message GetMessageInfoById(Guid id)
        {
            return _commonMessageService.GetMessageInfoById(id);
        }
        #endregion


        #region IMessageService 成员


        public List<ICP.Message.ServiceInterface.Message> CustomerMailList(Guid operationId, string values)
        {
            return _commonMessageService.CustomerMailList(operationId, values);
        }

        public List<ICP.Message.ServiceInterface.Message> RuturnMailList(Guid userId, string words, int wordType, string refNo, int refNoType, string customerName, int customerType, int phase, string mails, int mailType, int dateType, DateTime fromDate, DateTime endDate)
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
