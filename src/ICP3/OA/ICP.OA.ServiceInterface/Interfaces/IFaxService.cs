////using System;
////using System.Collections.Generic;
////using ICP.OA.ServiceInterface.DataObjects;
////using System.ServiceModel;
////using ICP.Framework.CommonLibrary.Common;
////using ICP.Message.ServiceInterface;
////namespace ICP.OA.ServiceInterface
////{
////    [ServiceContract]
////    public interface IFaxService
////    {
////        /// <summary>
////        /// 获取用户所有的文件夹列表
////        /// </summary>
////        /// <param name="ownerID">所有者ID</param>
////        /// <returns>返回用户所有的文件夹列表</returns>
////        [OperationContract]
////        List<MessageFolderList> GetMessageFolderList(Guid ownerID);
////        /// <summary>
////        /// 修改文件夹信息
////        /// </summary>
////        /// <param name="folderID">文件夹ID</param>
////        /// <param name="parentID">父文件夹</param>
////        /// <param name="cName">中文名</param>
////        /// <param name="eName">英文名</param>
////        /// <param name="folderType">类型</param>
////        /// <param name="updateDate">数据版本</param>
////        /// <returns>返回ID</returns>
////        [OperationContract]
////        ManyResultData SaveMessageFolder(
////            Guid? folderID,
////            Guid parentID,
////            string Name,
////            MessageFolderType folderType,
////            DateTime? updateDate);

////        /// <summary>
////        /// 删除文件夹 
////        /// </summary>
////        /// <param name="folderID">文件夹ID</param>
////        /// <param name="updateDate">数据版本</param>
////        [OperationContract]
////        ManyResultData RemoveFolder(
////            Guid folderID,
////             DateTime? updateDate);

////        /// <summary>
////        /// 获取传真日志列表
////        /// </summary>
////        /// <param name="ownerID">用户ID</param>
////        /// <param name="ownerAccount">所有者帐号</param>
////        /// <param name="folderId">文件夹Id</param>
////        /// <param name="folderType">文件夹类型</param>
////        /// <param name="folderName">文件夹名</param>
////        /// <param name="from">发送人</param>
////        /// <param name="to">接收人</param>
////        /// <param name="subject">主题</param>
////        /// <param name="includeAttachment">是否包含附件</param>
////        /// <param name="priority">优先级</param>
////        /// <param name="flag">标志</param>
////        /// <param name="fromTime">开始时间</param>
////        /// <param name="toTime">结束时间</param>
////        /// <returns>返回邮件日志列表</returns>
////        [OperationContract]
////        List<ICP.Message.ServiceInterface.Message> GetMessageList(
////            Guid ownerID,
////            string ownerAccount,
////            Guid? folderId,
////            MessageFolderType? folderType,
////            string folderName,
////            string from,
////            string to,
////            string subject,
////            bool? includeAttachment,
////            MessagePriority? priority,
////            MessageFlag? flag,
////            DateTime? fromTime,
////            DateTime? toTime,
////            Guid? companyID
////            );

////        [OperationContract]
////        List<Message.ServiceInterface.Message> GetFaxMessageListByFastSearch(
////            Guid ownerID,
////            Guid? folderId,
////            Guid? companyId,
////            string keyWord);
////        /// <summary>
////        ///用户点击文件夹，默认导出最近一个月的传真日志
////        /// </summary>
////        /// <param name="folderId"></param>
////        /// <returns></returns>
////        [OperationContract]
////        List<ICP.Message.ServiceInterface.Message> GetMessageListByFolderId(Guid folderId);

////        /// <summary>
////        /// 改变日志所在文件夹
////        /// </summary>
////        /// <param name="ids">日志ID列表</param>
////        /// <param name="folderId">文件夹ID</param>
////        /// <param name="updateDates">数据版本</param>
////        [OperationContract]
////        ManyResult ChangeMessageFolder(
////            Guid[] ids,
////            Guid folderId,
////            DateTime?[] updateDates);

////        [OperationContract]
////        SingleResult Send(ICP.Message.ServiceInterface.Message message);
////        [OperationContract]
////        void Resend(ICP.Message.ServiceInterface.Message message);
////        /// <summary>
////        /// 根据MessageID或者CompanyID来获取配置信息
////        /// </summary>
////        /// <param name="ComanyID"></param>
////        /// <returns></returns>
////        [OperationContract]
////        ConfigureObjects GetConfigureInfoByCompanyID(Guid companyID);
////        /// <summary>
////        /// 根据邮件地址找到传真号
////        /// </summary>
////        /// <param name="email"></param>
////        /// <returns></returns>
////        [OperationContract]
////        List<ConfigureObjects> GetConfigureInfoByEmail(string email, bool isTaxNo);
////        /// <summary>
////        /// 保存
////        /// </summary>
////        /// <param name="info"></param>
////        /// <returns></returns>
////        [OperationContract]
////        ManyResult UpdateConfigureInfoByCompanyID(ConfigureObjects info);
////    }
////}
