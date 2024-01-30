

//-----------------------------------------------------------------------
// <copyright file="CommonHelper.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.Common.UI.Service
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;
    using Microsoft.Practices.CompositeUI;
    using ICP.FCM.Common.ServiceInterface;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.MailCenter.CommonUI;

    /// <summary>
    /// UI服务
    /// </summary>
    public class FCMCommonClientService : IFCMCommonClientService
    {
        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }
        /// <summary>
        /// 打开申请代理页面
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="Workitem">workitem</param>
        /// <returns>是否申请成功</returns>
        public bool OpenAgentRequestPart(Guid operationID, OperationType operationType, WorkItem Workitem)
        {
            AgentRequestInfo agentRequstInfo = new AgentRequestInfo();
            agentRequstInfo.OperationID = operationID;
            AgentRequest.AgentRequstPart agentRequstPart = Workitem.Items.AddNew<AgentRequest.AgentRequstPart>();
            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            stateValues.Add("OperationType", operationType);
            agentRequstPart.Init(stateValues);
            agentRequstPart.DataSource = agentRequstInfo;
            string title = LocalData.IsEnglish ? "Agent Request" : "申请代理.";

            if (Utility.ShowDialog(agentRequstPart, title) == System.Windows.Forms.DialogResult.OK)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 打开备注页面
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="Workitem">Workitem</param>
        /// <param name="workspaceName">workspaceName</param>
        public void ShowMemoList(Guid operationID, OperationType operationType, WorkItem Workitem, string workspaceName)
        {
            Memo.MemoListPart memoListPart = Workitem.Items.Get<Memo.MemoListPart>("MemoListPart");
            if (memoListPart != null)
            {
                Workitem.Workspaces[workspaceName].Activate(memoListPart);
            }
            else
            {
                memoListPart = Workitem.Items.AddNew<Memo.MemoListPart>("MemoListPart");
                Workitem.Workspaces[workspaceName].Show(memoListPart, new SmartPartInfo(LocalData.IsEnglish ? "Memo" : "备忘录", ""));
            }
        }

        /// <summary>
        /// 打开文档页面
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="Workitem">Workitem</param>
        /// <param name="workspaceName">workspaceName</param>
        public void ShowDocumentList(Guid operationID, OperationType operationType, WorkItem Workitem, string workspaceName)
        {
            Document.DocumentListPart docListPart = Workitem.Items.Get<Document.DocumentListPart>("DocumentListPart");
            if (docListPart != null)
            {
                Workitem.Workspaces[workspaceName].Activate(docListPart);
            }
            else
            {
                docListPart = Workitem.Items.AddNew<Document.DocumentListPart>("DocumentListPart");
                Workitem.Workspaces[workspaceName].Show(docListPart);
            }
        }

        /// <summary>
        /// 打开传真日志页面
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="Workitem">Workitem</param>
        /// <param name="workspaceName">workspaceName</param>
        public void ShowMailFaxLogList(Guid operationID, OperationType operationType, Guid? formId, WorkItem Workitem, string workspaceName)
        {
            // List<CommonMailFaxLogList> logList=fcmCommonService.GetMailFaxLogList(operationID,operationType,formId);
            MemoParam param = new MemoParam() { FormID = formId.HasValue ? formId.Value : Guid.Empty, OperationId = operationID, OperationType = operationType };
            UCCommunicationHistory communicationListPart = Workitem.Items.Get<UCCommunicationHistory>(workspaceName);

            if (communicationListPart != null)
            {
                Workitem.Workspaces[workspaceName].Activate(communicationListPart);
            }
            else
            {
                communicationListPart = Workitem.Items.AddNew<UCCommunicationHistory>(workspaceName);
                Workitem.Workspaces[workspaceName].Show(communicationListPart);
            }

            Utility.SetCommunicationDataSource(communicationListPart, param);
        }
    }
}
