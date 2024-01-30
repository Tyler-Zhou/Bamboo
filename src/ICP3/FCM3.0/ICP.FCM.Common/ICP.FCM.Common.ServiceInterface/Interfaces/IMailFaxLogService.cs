namespace ICP.FCM.Common.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;
    using System.ServiceModel;
    /// <summary>
    /// 邮件日志 接口
    /// </summary>
    [ServiceInfomation("邮件日志接口")]
    [ServiceContract]
    public interface IMailFaxLogService
    {
        #region MailFaxLog

        /// <summary>
        /// 获取邮件传真日志列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型(0:海运出口,1:海运进口,2:空运出口,3:空运进口,4:其他出口,5:其它进口)</param>
        /// <param name="ownerID">所有者ID</param>
        /// <returns>返回邮件传真日志列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CommonMailFaxLogList> GetMailFaxLogList(
            Guid operationID,
            ICP.Framework.CommonLibrary.Client.OperationType operationType,
            Guid? ownerID);

        /// <summary>
        /// 保存邮件传真日志
        /// </summary>
        /// <param name="ownerID">所有者ID</param>
        /// <param name="ownerSource">业务类型(0:海运出口,1:海运进口,2:空运出口,3:空运进口,4:其他出口,5:其它进口)</param>
        /// <param name="id">ID</param>
        /// <param name="keyID">关键字ID</param>
        /// <param name="userMailAccountID">用户邮件账号ID</param>
        /// <param name="mailFrom">发件人</param>
        /// <param name="mailTo">收件人</param>
        /// <param name="mailCC">抄送人</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="mailContent">邮件内容</param>
        /// <param name="priority">重要性（0普通、1高、2低）</param>
        /// <param name="isFax">是否发送传真</param>
        /// <param name="attachmentNames">附件名列表</param>
        /// <param name="attachmentContents">附件内容列表</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveMailFaxLogInfo(
             Guid ownerID,
            ICP.Framework.CommonLibrary.Client.OperationType ownerSource,
            Guid?[] id,
            Guid?[] keyID,
            Guid[] userMailAccountID,
            string[] mailFrom,
            string[] mailTo,
            string[] mailCC,
            string[] subject,
            string[] mailContent,
            EMailPriority[] priority,
            bool[] isFax,
            string[] attachmentNames,
            byte[][] attachmentContents,
            Guid saveByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取邮件的附件列表
        /// </summary>
        /// <param name="mailLogID">邮件日志ID</param>
        /// <returns>返回邮件的附件列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AttachmentList> GetMailLogAttachmentList(Guid mailLogID);

        /// <summary>
        /// 获取邮件的附件列表
        /// </summary>
        /// <param name="mailLogID">邮件日志ID</param>
        /// <returns>返回邮件的附件列表</returns>
        [FunctionInfomation]
        [OperationContract]
        void RemoveMailMessageInfo(Guid mailLogID,Guid removeByID,DateTime? updateDate);
         
        #endregion
    }
}
