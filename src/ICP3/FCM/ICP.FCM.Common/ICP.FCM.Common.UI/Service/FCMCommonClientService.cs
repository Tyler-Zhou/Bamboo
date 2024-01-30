

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
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.ClientComponents.Controls;
    using System.Windows.Forms;
    using ICP.Business.Common.UI.Communication;
    using ICP.Business.Common.UI.Communication_History;
    using ICP.FCM.Common.UI.CommonPart;

    /// <summary>
    /// UI服务
    /// </summary>
    public class FCMCommonClientService : IFCMCommonClientService
    {   
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        /// <summary>
        /// 打开申请代理页面
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="stateValues"></param>
        /// <param name="editPartSaved"></param>
        /// <returns>是否申请成功</returns>
        public bool OpenAgentRequestPart(Guid operationID, OperationType operationType, IDictionary<string, object> stateValues, PartDelegate.EditPartSaved editPartSaved)
        {
            AgentRequestInfo agentRequstInfo = new AgentRequestInfo();
            agentRequstInfo.OperationID = operationID;
            AgentRequest.AgentRequstPart agentRequstPart = this.WorkItem.Items.AddNew<AgentRequest.AgentRequstPart>();
            stateValues = stateValues ?? new Dictionary<string, object>();
            stateValues.Add("OperationType", operationType);
            agentRequstPart.Init(stateValues);
            agentRequstPart.DataSource = agentRequstInfo;
            if (editPartSaved != null)
            {
                agentRequstPart.Saved += new ICP.Framework.ClientComponents.UIFramework.SavedHandler(editPartSaved);
            }
            string title = LocalData.IsEnglish ? "Agent Request" : "申请代理.";
            
            //if (ICP.FCM.Common.UI.Utility.ShowDialog(agentRequstPart, title) == System.Windows.Forms.DialogResult.OK)
            if (PartLoader.ShowDialog(agentRequstPart, title) == DialogResult.OK)
                return true;
                else
                return false;
        }

        /// <summary>
        /// 打开文档页面
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="workItem">Workitem</param>
        /// <param name="workspaceName">workspaceName</param>
        public void ShowDocumentList(Guid operationID, OperationType operationType, WorkItem workItem ,string workspaceName)
        {
            Document.DocumentListPart docListPart = workItem.Items.Get<Document.DocumentListPart>("DocumentListPart");
            if (docListPart != null)
            {
                workItem.Workspaces[workspaceName].Activate(docListPart);
            }
            else
            {
                docListPart = workItem.Items.AddNew<Document.DocumentListPart>("DocumentListPart");
                workItem.Workspaces[workspaceName].Show(docListPart);
            }
        }

        /// <summary>
        /// 打开传真日志页面
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="Workitem">Workitem</param>
        /// <param name="workspaceName">workspaceName</param>
        public void ShowMailFaxLogList(Guid operationID, OperationType operationType, Guid? formId, WorkItem workItem, string workspaceName)
        {
            // List<CommonMailFaxLogList> logList=fcmCommonService.GetMailFaxLogList(operationID,operationType,formId);
            MemoParam param = new MemoParam() { FormID = formId.HasValue ? formId.Value : Guid.Empty, OperationId = operationID, OperationType = operationType };
            UCCommunicationHistory communicationListPart = workItem.Items.Get<UCCommunicationHistory>("FaxMailEDIListPart");

            if (communicationListPart != null)
            {
                workItem.Workspaces[workspaceName].Activate(communicationListPart);
            }
            else
            {
                //communicationListPart = Workitem.Items.AddNew<UCCommunicationHistory>(workspaceName);
                communicationListPart = workItem.Items.AddNew<UCCommunicationHistory>("FaxMailEDIListPart");
                workItem.Workspaces[workspaceName].Show(communicationListPart);
            }

            ICP.FCM.Common.UI.FCMUIUtility.SetCommunicationDataSource(communicationListPart, param);
        }
        /// <summary>
        /// 打开备注输入界面
        /// </summary>
        /// <param name="title">窗体标题</param>
        /// <param name="editPartSaved"></param>
        public void ShowRemarkDialog(string title, PartDelegate.EditPartSaved editPartSaved,WorkItem workItem)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ICP.FCM.Common.UI.Common.Parts.EditRemarkPart editRemarkPart = workItem.Items.AddNew<ICP.FCM.Common.UI.Common.Parts.EditRemarkPart>();
                editRemarkPart.Saved += delegate(object[] prams)
                {
                    editPartSaved(prams);
                };
                PartLoader.ShowDialog(editRemarkPart, title);
            }
        }

        /// <summary>
        /// 分发文档
        /// </summary>
        /// <param name="context"></param>
        /// <param name="editPartSaved"></param>
        /// <param name="workItem"></param>
        public void DispatchDocument(BusinessOperationContext context,PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    FCM.Common.UI.FCMUIUtility.ShowDispatchDocumentNew(WorkItem, context, editPartSaved,1);
                }
                catch (Exception ex)
                {
                    ICP.Framework.CommonLibrary.Client.LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
                }
            }
        }

        /// <summary>
        /// 获取分发文件(文档)日志面板
        /// </summary>
        /// <param name="strQuery">查询条件：格式为SQL组合语句</param>
        /// <returns></returns>
        public Control GetDispatchFileLogPart(string strQuery)
        {

            try
            {
                //DispatchFileLogShow disFileLog = new DispatchFileLogShow();
                //return disFileLog;
                return null;
            }
            catch (Exception ex)
            {
                ICP.Framework.CommonLibrary.Client.LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
            }
            return null;
        }

        /// <summary>
        /// 获取分发文件(文档)日志面板
        /// </summary>
        /// <param name="strQuery">查询条件：格式为SQL组合语句</param>
        /// <returns></returns>
        public Control GetEdiLogListPart(string strQuery)
        {
            try
            {
                EdiLogList disFileLog = new EdiLogList();
                disFileLog.SetQuery(strQuery);
                return disFileLog;
            }
            catch (Exception ex)
            {
                ICP.Framework.CommonLibrary.Client.LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
            }
            return null;
        }

        /// <summary>
        /// 导入PO
        /// </summary>
        /// <param name="context"></param>
        /// <param name="editPartSaved"></param>
        public void ImportPurchaseOrder(BusinessOperationContext context, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    PartLoader.ShowEditPart<PartPOItems>(WorkItem, context, EditMode.New, null, LocalData.IsEnglish ? "Import PO" : "导入PO", editPartSaved, "IMPORTPO");
                }
                catch (Exception ex)
                {
                    ICP.Framework.CommonLibrary.Client.LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
                }
            }
        }
    }
}
