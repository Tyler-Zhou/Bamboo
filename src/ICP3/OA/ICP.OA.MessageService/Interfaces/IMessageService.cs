using System;
using System.Collections.Generic;
using System.ServiceModel;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Message.ServiceInterface
{
    [ServiceContract]
    public interface IMessageService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Message Get(Guid id);
        /// <summary>
        /// 根据选择公司来获取邮件信息
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        [OperationContract]
        List<FaxMessageObjects> GetMessageInfoByCompanyID(Guid companyID);
        /// <summary>
        /// 更改传真状态
        /// </summary>
        /// <param name="receiveFaxIDs"></param>
        /// <param name="updateDates"></param>
        /// <returns></returns>
        [OperationContract]
        ManyResult ChangeFaxState(Guid[] IDs, Guid?[] folderIds, ReceiveFaxState[] states, DateTime?[] updateDates, DateTime?[] faxUpdateDates);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [OperationContract]
        Message GetMessageByMessageId(string messageId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Message GetMessageInfoById(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ManyResult[] Save(Message message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="updateDates"></param>
        [OperationContract]
        void Remove(Guid[] ids, DateTime?[] updateDates);
        /// <summary>
        /// 转发，回复后改变标志
        /// </summary>
        /// <param name="mailMessageID">日志ID</param>
        /// <param name="flag">标志</param>
        /// <param name="updateDate">数据版本</param>
        [OperationContract]
        SingleResultData ChangeFlag(Guid id, MessageFlag flag, DateTime? updateDate);
        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="states"></param>
        /// <param name="updateDates"></param>
        /// <returns></returns>
        [OperationContract]
        ManyResult ChangeState(Guid[] ids, MessageState[] states, DateTime?[] updateDates);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalId"></param>
        /// <param name="updateDate"></param>
        /// <param name="message"></param>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void Reply(Guid originalId, DateTime? updateDate, Message message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalId"></param>
        /// <param name="updateDate"></param>
        /// <param name="message"></param>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void Forward(Guid originalId, DateTime? updateDate, Message message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageIds"></param>
        /// <param name="states"></param>
        /// <param name="type"></param>
        [OperationContract(Name = "ChangeStateByMessageIds")]
        void ChangeState(string[] messageIds, MessageState[] states, MessageType type);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [OperationContract]
        SingleResult Send(Message message);
        [OperationContract]
        void Transfer(List<ConfigureObjects> userCompanyList, Guid defaultCompanyID);
        /// <summary>
        /// 返回当前业务的客户的邮件地址
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="values">客户类型</param>
        [OperationContract]
        List<Message> CustomerMailList(Guid operationId, string values);
        /// <summary>
        /// 根据查询条件返回当前邮件集合
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="words">查找文字</param>
        /// <param name="wordType">查找位置</param>
        /// <param name="refNo">业务号</param>
        /// <param name="refNoType">业务搜索范围</param>
        /// <param name="customerName">客户名</param>
        /// <param name="customerType">客户类型</param>
        /// <param name="phase">邮件阶段</param>
        /// <param name="mails">邮件地址</param>
        /// <param name="mailType">邮件搜索范围</param>
        /// <param name="dateType">时间查找范围</param>
        /// <param name="fromDate">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        [OperationContract]
        List<Message> RuturnMailList(Guid userId, string words, int wordType,
                                                                  string refNo, int refNoType,
                                                                  string customerName, int customerType,
                                                                  int phase, string mails, int mailType,
                                                                  int dateType, DateTime fromDate,
                                                                  DateTime endDate);

        /// <summary>
        /// 获取服务端邮件信息
        /// </summary>
        /// <param name="messageId">邮件的MessageID</param>
        [OperationContract]
        List<ContentInfo> GetMailInfo(string messageId);
    }
}
