
namespace ICP.FCM.Common.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;

    /// <summary>
    /// 业务客户端服务接口
    /// </summary>
    public interface IFCMCommonClientService
    {
        /// <summary>
        /// 打开申请代理页面
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="Workitem">workitem</param>
        /// <returns>是否申请成功</returns>
        bool OpenAgentRequestPart(Guid operationID, OperationType operationType, WorkItem Workitem);

        /// <summary>
        /// 打开备注页面
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="Workitem">Workitem</param>
        /// <param name="workspaceName">workspaceName</param>
        void ShowMemoList(Guid operationID, ICP.Framework.CommonLibrary.Client.OperationType operationType, WorkItem Workitem, string workspaceName);

        /// <summary>
        /// 打开文档页面
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="Workitem">Workitem</param>
        /// <param name="workspaceName">workspaceName</param>
        void ShowDocumentList(Guid operationID, ICP.Framework.CommonLibrary.Client.OperationType operationType, WorkItem Workitem, string workspaceName);

        /// <summary>
        /// 打开传真日志页面
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="formId">表单ID</param>
        /// <param name="Workitem">Workitem</param>
        /// <param name="workspaceName">workspaceName</param>
        void ShowMailFaxLogList(Guid operationID, ICP.Framework.CommonLibrary.Client.OperationType operationType,Guid? formId,WorkItem Workitem, string workspaceName);
    }

}
