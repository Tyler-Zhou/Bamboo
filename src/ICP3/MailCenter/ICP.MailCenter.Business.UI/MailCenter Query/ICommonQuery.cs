using System;
using ICP.Message.ServiceInterface;
using System.Data;
using ICP.Operation.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.MailCenter.Business.UI
{
    /// <summary>
    /// 邮件中心业务查询接口
    /// </summary>
    public interface ICommonQuery
    {
        /// <summary>
        /// 是否是未知业务面板业务面板
        /// </summary>
        /// <returns></returns>
        bool IsUnknownBusinessPart();

        /// <summary>
        /// 获取本地缓存单票业务
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        DataTable GetLocalOperationViewInfo(Guid operationID, ICP.Framework.CommonLibrary.Common.OperationType operationType);

        /// <summary>
        /// 获取服务端单票业务
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        DataTable GetOperationViewInfo(Guid operationID, OperationType operationType);
        /// <summary>
        /// 根据关联信息获取业务列表
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="messageRelations"></param>
        /// <returns></returns>
        DataTable GetOperationListByMessageRelations(BusinessQueryCriteria criteria, OperationMessageRelation[] messageRelations);
        /// <summary>
        /// 获取联系人业务列表
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        DataTable GetOperationListByContact(BusinessQueryCriteria criteria);
        /// <summary>
        /// 根据邮件主题关键字获取业务(匹配到多个单号循环从本地缓存查找)
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        DataTable GetOperationListBySubjectInNO(BusinessQueryCriteria criteria, Message.ServiceInterface.Message messageInfo);
        /// <summary>
        /// 根据输入单号或客户名称查询业务列表
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        DataTable GetOperationListByKeyWord(BusinessQueryCriteria criteria);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        BusinessQueryResult GetData(ICP.Operation.Common.ServiceInterface.BusinessQueryCriteria criteria, ICP.Message.ServiceInterface.Message message);
        /// <summary>
        /// 获取业务列表
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        DataTable GetOperationViewList(ICP.Operation.Common.ServiceInterface.BusinessQueryCriteria criteria);
    }
}
