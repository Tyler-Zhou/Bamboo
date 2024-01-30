using System.Windows.Forms;
using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.ServiceInterface
{
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
        /// <param name="editPartSaved"></param>
        /// <param name="values"></param>
        /// <returns>是否申请成功</returns>
        bool OpenAgentRequestPart(Guid operationID, OperationType operationType,IDictionary<string,object> values, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 打开文档页面
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="workspaceName">workspaceName</param>
        /// <param name="workItem"></param>
        void ShowDocumentList(Guid operationID, OperationType operationType,WorkItem workItem ,string workspaceName);

        /// <summary>
        /// 打开传真日志页面
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="formId">表单ID</param>
        /// <param name="workspaceName">workspaceName</param>
        /// <param name="workItem"></param>
        void ShowMailFaxLogList(Guid operationID, OperationType operationType, Guid? formId,WorkItem workItem, string workspaceName);

        /// <summary>
        /// 打开备注输入窗体
        /// </summary>
        /// <param name="title">窗体标题</param>
        /// <param name="editPartSaved"></param>
        /// <param name="workItem"></param>
        void ShowRemarkDialog(string title, PartDelegate.EditPartSaved editPartSaved, WorkItem workItem);
        /// <summary>
        /// 分发文档
        /// </summary>
        /// <param name="context"></param>
        /// <param name="editPartSaved"></param>
        void DispatchDocument(BusinessOperationContext context, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 获取分发文件(文档)日志面板
        /// </summary>
        /// <param name="strQuery">查询条件：格式为SQL组合语句</param>
        /// <returns></returns>
        Control GetDispatchFileLogPart(string strQuery);

        /// <summary>
        /// 获取EDI日志面板
        /// </summary>
        /// <param name="strQuery">查询条件：格式为SQL组合语句</param>
        /// <returns></returns>
        Control GetEdiLogListPart(string strQuery);

        /// <summary>
        /// 导入PO
        /// </summary>
        /// <param name="context"></param>
        /// <param name="editPartSaved"></param>
        void ImportPurchaseOrder(BusinessOperationContext context, PartDelegate.EditPartSaved editPartSaved);
    }

}
