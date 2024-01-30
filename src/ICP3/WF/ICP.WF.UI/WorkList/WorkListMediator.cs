//-----------------------------------------------------------------------
// <copyright file="ShellMediator.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.UI
{
    using System;
    using System.Windows.Forms;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI.Commands;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using ICP.WF.ServiceInterface;
    using ICP.WF.ServiceInterface.DataObject;
    using System.Collections.Generic;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.WF.Controls;
    using DevExpress.XtraEditors;
    using System.Data;
    using System.Collections;
    using ICP.Framework.ClientComponents.Controls;
    using ICP.WF.UI.Common;
    using ICP.Framework.CommonLibrary.Common;
    using System.Linq;
    using ICP.Sys.ServiceInterface;
    using ICP.Sys.ServiceInterface.DataObjects;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// 页面之间交互处理逻辑
    /// </summary>
    public class WorkListMediator : IPartBridge, IDisposable
    {
        #region 本地变量
        ILayoutBuilderContext currentContext;

        WorkListToolBarPart _toolBar;
        WorkListSearch _searchPart;
        WorkListPart _mainListPart;
        WorkListFlowChatPart _chatPart;
        IWorkListFlowChatService flowChatService;

        #endregion

        #region 服务
        public IWorkflowService WorkflowService
        {
            get
            {
                return ServiceClient.GetService<IWorkflowService>();
            }
        }

        public WFPrintHelper WFPrintHelper
        {
            get
            {
                return ClientHelper.Get<WFPrintHelper, WFPrintHelper>();
            }
        }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        #endregion

        #region IPartBridge接口

        /// <summary>
        /// 初始化
        /// <remarks>
        /// 把由布局生成的面板信息初始化到该处,以便在此处理各个部件之间的交互.
        /// 由布局生成类调用该接口
        /// </remarks>
        /// </summary>
        /// <param name="context">生成上下文</param>
        /// <param name="partNames">处理那几个部件之间的交互</param>
        public void Init(
            ILayoutBuilderContext context,
            string[] partNames)
        {
            //当前上下文
            currentContext = context;

            ////设置要交互的面版
            _toolBar = context.GetService<WorkListToolBarPart>();
            _searchPart = context.GetService<WorkListSearch>();
            _mainListPart = context.GetService<WorkListPart>();
            flowChatService = context.GetService<IWorkListFlowChatService>();
            _chatPart = context.GetService<WorkListFlowChatPart>();

            WorkFlowUIAdapter adapter = new WorkFlowUIAdapter();
            adapter.Init(_toolBar,
                        _searchPart,
                        _mainListPart,
                        _chatPart,
                        currentContext.WorkItem);

        }


        /// <summary>
        /// 动态要注入交互面板
        /// </summary>
        /// <typeparam name="T">面板类型</typeparam>
        /// <param name="part">面板</param>
        /// <param name="name">面板名称</param>
        public void Register<T>(
            T part,
            string name)
        {
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (this._mainListPart != null)
            {
                this._mainListPart = null;
            }
            if (this._searchPart != null)
            {
                this._searchPart = null;
            }
            this._toolBar = null;
            this.currentContext = null;
            if (this.flowChatService != null)
            {
                this.flowChatService = null;
            }
        }

        #endregion
    }


    public class WorkFlowUIAdapter
    {

        #region Parts&Services
        WorkListToolBarPart toolBar = null;
        ISearchPart searchPart = null;
        WorkListPart mainListPart = null;
        WorkItem WorkItem = null;
        bool flag = false;
        WorkListFlowChatPart chatPart = null;
        public IWorkflowService WorkflowService
        {
            get
            {
                return ServiceClient.GetService<IWorkflowService>();
            }
        }

        public WFPrintHelper WFPrintHelper
        {
            get
            {
                return ClientHelper.Get<WFPrintHelper, WFPrintHelper>();
            }
        }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        #endregion

        #region Init
        public void Init(
             WorkListToolBarPart _toolBar ,
             WorkListSearch _searchPart,
             WorkListPart _mainListPart ,
             WorkListFlowChatPart _chatPart,
             WorkItem _WorkItem )
        {
            toolBar = _toolBar;
            searchPart = _searchPart;
            mainListPart = _mainListPart;
            WorkItem = _WorkItem;
            chatPart = _chatPart;

            #region 交互事件

            #region 当前行发生改变

            mainListPart.CurrentChanged += this.OnMainListPartCurrentChanged;
            #endregion

            #region 查询事件
            searchPart.OnSearched += this.OnSearchPartSearched;

            #endregion

            #region 双击流程图
            chatPart.WorkItemDoubleClick += new EventHandler<WorkItemEventArgs>(flowChatService_WorkItemDoubleClick);
            #endregion

            #region 双击编辑
            mainListPart.DockClickEditWork += Command_WorkList_Edit;
            #endregion

            #region 工具栏按钮事件
            toolBar.NewWorkEventArgs += Command_WorkList_New;
            toolBar.EditWorkEventArgs += Command_WorkList_Edit;
            toolBar.MultiFinishWork += Command_WorkList_MultiFinishWork;
            toolBar.CancelWork += Command_Work_Cancel;
            toolBar.AuditorWork += Command_WorkList_Auditor;
            toolBar.AuditorMergerWork += Command_WorkList_AuditorMerger;
            toolBar.UnAuditorWork += Command_WorkList_UnAuditor;
            toolBar.PrintWork += Command_Work_Print;
            toolBar.PrintReportWork += Comand_WorkList_PrintReport;
            toolBar.RefreshWork += Command_WorkList_Refresh;
            toolBar.DeleteLedger += Command_Work_DeleteLedger;

            #endregion

            #endregion

            _mainListPart.RefreshData();
        }



        #endregion

        #region 按钮方法

        #region 新建
        /// <summary>
        /// 新建流程
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void Command_WorkList_New(object s, WorkItemClickEventArgs e)
        {
            try
            {
                WorkListEditPart newPart = WorkItem.Items.AddNew<WorkListEditPart>(Guid.NewGuid().ToString());
                Microsoft.Practices.CompositeUI.SmartParts.IWorkspace workSpace = WorkItem.RootWorkItem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

                newPart.WFFormType = WorkFlowFormType.Add;
                newPart.WorkFlowConfigID = new Guid(e.Value);

                SmartPartInfo sm = new SmartPartInfo();
                sm.Description = LocalData.IsEnglish ? "Application Tasks" : "申请任务";
                sm.Title = LocalData.IsEnglish ? "Application Tasks" : "申请任务";
                workSpace.Show(newPart, sm);

                newPart.FinishData += delegate(object[] prams)
                {
                    if (prams != null && prams.Length > 0)
                    {
                        WorkFlowData info = prams[0] as WorkFlowData;
                        if (info != null)
                        {
                            mainListPart.RefreshList(info, true);
                        }
                    }
                };



            }
            catch
            {

            }

        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑任务 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void Command_WorkList_Edit(object s, EventArgs e)
        {

            EditWork();
        }
        /// <summary>
        /// 编辑任务
        /// </summary>
        private void EditWork()
        {
            try
            {
                if (mainListPart.Current == null)
                {
                    return;
                }
                WorkFlowData workFlow = mainListPart.Current as WorkFlowData;

                if (workFlow == null)
                {
                    return;
                }

                WorkListEditPart newPart = WorkItem.Items.AddNew<WorkListEditPart>();
                Microsoft.Practices.CompositeUI.SmartParts.IWorkspace workSpace = WorkItem.RootWorkItem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                newPart.WFFormType = WorkFlowFormType.Edit;
                newPart.WorkInfoID = workFlow.Id;
                newPart.CurrentID = workFlow.CurrentWorkItemId;
                newPart.workflowData = workFlow;
                newPart.WorkFlowConfigID = workFlow.WorkFlowConfigID;

                SmartPartInfo sm = new SmartPartInfo();
                sm.Description = sm.Title = LocalData.IsEnglish ? "Edit Work" : "编辑任务";
                workSpace.Show(newPart, sm);

                newPart.FinishData += delegate(object[] prams)
                {
                    if (prams != null && prams.Length > 0)
                    {
                        WorkFlowData info = prams[0] as WorkFlowData;
                        if (info != null && info.Id != new Guid())
                        {
                            mainListPart.RefreshList(info, true);
                        }
                    }
                };

            }
            catch
            {

            }
        }
        #endregion

        #region 批量编辑
        private void Command_WorkList_MultiFinishWork(object s, EventArgs e)
        {
            if (mainListPart.SelectDataList.Count == 0)
            {
                return;
            }
            int theradID = 0;

            try
            {
                theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Execing...");

                List<CurrentWorkItem> DataList = WorkflowService.MultiFinishWork(mainListPart.SelectIDList.ToArray(), LocalData.UserInfo.LoginID);

                mainListPart.RefreshList(DataList);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(mainListPart.FindForm(), ex);
            }
            finally
            {

                ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
            }
        }
        #endregion

        #region 审核
        public void Command_WorkList_Auditor(object s, EventArgs e)
        {
            if (mainListPart.SelectIDList.Count == 0)
            {
                return;
            }
            if (!CheckAllocation(true))
            {
                return;
            }
            int theradID = 0;
            try
            {
                if (mainListPart.SelectIDList.Count > 10)
                {
                    theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("正在审核流程,请稍后...");
                }
                ManyResult result = WorkflowService.AuditorWork(mainListPart.SelectIDList.ToArray(), false, true, LocalData.UserInfo.LoginID);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(mainListPart.FindForm(), LocalData.IsEnglish ? "Auditor Success." : "审核成功.");

                foreach (var item in result.Items)
                {
                    Guid id = item.GetValue<Guid>("ID");
                    string voucherNo = item.GetValue<string>("VoucherNo");

                    WorkFlowData tager = mainListPart.SelectDataList.Find(delegate(WorkFlowData data) { return data.Id == id; });
                    if (tager != null)
                    {
                        tager.VoucherNo = voucherNo;
                        mainListPart.RefreshList(tager, false);
                    }
                }

                //刷新工具栏
                RefreshToolbars(mainListPart.CurrentRow);
            }
            catch (Exception ex)
            {
                if (toolBar != null)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(((Control)toolBar).FindForm(), ex.Message);
                }
            }
            finally
            {
                if (theradID != 0)
                {
                    ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
                }
            }
        }

        /// <summary>
        /// 验证资金调拨流程是否可以审核
        /// </summary>
        /// <returns></returns>
        private bool CheckAllocation(bool isAuditor)
        {
            foreach (WorkFlowData item in mainListPart.SelectDataList)
            {
                if (item.WorkFlowConfigID == Utility.Allocation)
                {
                    List<UserOrganizationTreeList> list = UserService.GetUserOrganizationTreeList(LocalData.UserInfo.LoginID);
                    int count = list.Count(p => p.ID == item.DepartmentID);
                    if (count == 0)
                    {
                        string methodName = "审核";
                        if (!isAuditor)
                        {
                            methodName = "取消审核";
                        }
                        string message = methodName + "失败!资金调拨流程[" + item.No + "]不是属于您公司的流程。";
                        DevExpress.XtraEditors.XtraMessageBox.Show(message);
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region 审核+合并
        public void Command_WorkList_AuditorMerger(object s, EventArgs e)
        {
            if (mainListPart.SelectIDList.Count == 0)
            {
                return;
            }
            if (!CheckAllocation(true))
            {
                return;
            }
            int theradID = 0;
            try
            {
                if (mainListPart.SelectIDList.Count > 10)
                {
                    theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("正在审核流程,请稍后...");
                }
                ManyResult result = WorkflowService.AuditorWork(mainListPart.SelectIDList.ToArray(), true, true, LocalData.UserInfo.LoginID);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(mainListPart.FindForm(), LocalData.IsEnglish ? "Auditor Success." : "审核成功.");

                foreach (var item in result.Items)
                {
                    Guid id = item.GetValue<Guid>("ID");
                    string voucherNo = item.GetValue<string>("VoucherNo");

                    WorkFlowData tager = mainListPart.SelectDataList.Find(delegate(WorkFlowData data) { return data.Id == id; });
                    if (tager != null)
                    {
                        tager.VoucherNo = voucherNo;
                        mainListPart.RefreshList(tager, false);
                    }
                }
                //刷新工具栏
                RefreshToolbars(mainListPart.CurrentRow);
            }
            catch (Exception ex)
            {
                if (toolBar != null)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(((Control)toolBar).FindForm(), ex.Message);
                }
            }
            finally
            {
                if (theradID != 0)
                {
                    ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
                }
            }
        }
        #endregion

        #region 取消审核
        public void Command_WorkList_UnAuditor(object s, EventArgs e)
        {
            if (mainListPart.SelectIDList.Count == 0)
            {
                return;
            }
            int theradID = 0;
            try
            {
                if (!CheckAllocation(false))
                {
                    return;
                }
                if (mainListPart.SelectIDList.Count > 10)
                {
                    theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("正在取消审核流程,请稍后...");
                }
                WorkflowService.AuditorWork(mainListPart.SelectIDList.ToArray(), false, false, LocalData.UserInfo.LoginID);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(mainListPart.FindForm(), LocalData.IsEnglish ? "UnAuditor Success." : "取消审核成功.");

                foreach (WorkFlowData item in mainListPart.SelectDataList)
                {
                    item.VoucherNo = string.Empty;
                    mainListPart.RefreshList(item, false);
                }
                //刷新工具栏
                RefreshToolbars(mainListPart.CurrentRow);
            }
            catch (Exception ex)
            {
                if (toolBar != null)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(((Control)toolBar).FindForm(), ex.Message);
                }
            }
            finally
            {
                if (theradID != 0)
                {
                    ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
                }
            }
        }
        #endregion

        #region 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void Command_Work_Cancel(object s, EventArgs e)
        {

            WorkFlowData data = mainListPart.Current as WorkFlowData;
            if (data == null)
            {
                return;
            }
            try
            {

                //不是自己申请的，无法进行取消操作
                if (data.OwnerUserId != LocalData.UserInfo.LoginID)
                {
                    XtraMessageBox.Show(string.Format(Utility.GetString("ApplicationNoCanceledByNoMy", "单号 {0} 的流程不是自己申请的，无法取消"), data.No));
                    return;
                }
                //已取消的
                if (data.State == WorkflowState.Cancel &&
                    data.State != WorkflowState.Return &&
                     data.State != WorkflowState.Finished)
                {
                    XtraMessageBox.Show(string.Format(Utility.GetString("ApplicationNoCanceledByNoActivated", "单号 {0} 的流程不是在办或待办状态，无法取消"), data.No));
                    return;
                }

                //提示是否取消
                if (Utility.EnquireIsDeleteCurrentData("CancelWorkFlow"))
                {
                    WorkflowService.CancelWorkflow(data.Id, LocalData.UserInfo.LoginID);
                    data.State = WorkflowState.Cancel;

                    mainListPart.SetCurrecntChanged();
                }

            }
            catch (Exception ex)
            {
                if (toolBar != null)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(((Control)toolBar).FindForm(), ex.Message);
                }
            }

        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void Command_Work_Print(object s, EventArgs e)
        {
            WorkFlowData data = mainListPart.Current as WorkFlowData;
            if (data == null)
            {
                return;
            }

            string orgNo = string.Empty;
            WorkItemInfo mainItem = WorkflowService.GetMainItem(data.Id);

            try
            {
                orgNo = mainItem.FormData.Tables[0].Rows[0]["NO"].ToString();
            }
            catch
            {
 
            }

            if (orgNo != data.No)
            {
                mainItem.FormData.Tables[0].Rows[0]["NO"] = orgNo + ";" + data.No;
            }

            LWBaseForm cl = GetForm(mainItem.WorkflowInstanceID, mainItem.FormSchema, mainItem.FormData);
            //cl = GetForm(mainItem.WorkflowInstanceID, mainItem.FormSchema, mainItem.FormData);

            if (cl != null)
            {

                //先SHOW 一个界面，不然里面的数据源不会加载到控件中
                ShowForm showForm = new ShowForm();
                showForm.Controls.Clear();
                showForm.Controls.Add(cl);
                showForm.Show();
                showForm.Hide();


                PrintWorkFlow webPrint = new PrintWorkFlow();
                webPrint.BaseForm = cl;
                webPrint.WorkName = mainItem.WorkFlowPrintTitle;

                Utility.SetReadOnly(cl, true);
                cl.Enabled = false;
                webPrint.ShowPrint(cl, mainItem.WorkFlowPrintTitle, data.Name);

                if (flag)
                {
                    PartLoader.ShowDialog(webPrint, "Print WorkFlow");
                }
                else
                {
                    flag = true;
                    Command_Work_Print(s, e);
                }
            }

        }

        public void Comand_WorkList_PrintReport(object s, EventArgs e)
        {
            WorkFlowData data = mainListPart.Current as WorkFlowData;
            if (data == null)
            {
                return;
            }
            WFPrintHelper.PrintCheckAP(data.Id);
        }
        #endregion

        #region 流程图
        /// <summary>
        /// 根据xml文件获得表单界面
        /// </summary>
        private LWBaseForm GetForm(Guid workflowInstanceID, string formSchema, DataSet formData)
        {
            DataSet ds = new DataSet();

            if (!string.IsNullOrEmpty(formSchema))
            {
                LWBaseForm cl = (LWBaseForm)FormBuildService.CreateObjectFromXmlData(WorkListModuleInit.serviceContainers, formSchema, new ArrayList());

                Dictionary<string, object> workflowConstants = WorkflowService.GetDataCollect(workflowInstanceID).DataCollect;

                cl.CommonConstants = workflowConstants;

                cl.DataSource = formData;

                return cl;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 流程图
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void Command_WorkList_FlowChat(object s, EventArgs e)
        {

            ShowFlowChat();
        }

        /// <summary>
        /// 显示流程图
        /// </summary>
        private void ShowFlowChat()
        {
            WorkFlowData data = mainListPart.Current as WorkFlowData;
            if (data == null)
            {
                //清空流程图
                chatPart.Clear();
            }
            else
            {
                chatPart.ViewWorkFolwChart(data.Id);
            }
        }
        #endregion

        #region 刷新
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void Command_WorkList_Refresh(object s, EventArgs e)
        {
            mainListPart.RefreshData();
        }

        #endregion

        #region 关闭
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void Command_WorkList_Close(object s, EventArgs e)
        {
            if (toolBar != null)
            {
                ((Control)toolBar).FindForm().Close();

            }
        }
        #endregion

        #region 删除凭证
        public void Command_Work_DeleteLedger(object s, EventArgs e)
        {

            WorkFlowData data = mainListPart.Current as WorkFlowData;
            if (data == null)
            {
                return;
            }
            if (!string.IsNullOrEmpty(data.VoucherNo))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("流程已审核,请先取消审核后再删除");
                return;
            }
            try
            {
                //提示是否删除
                if (Utility.EnquireIsDeleteCurrentData(LocalData.IsEnglish?"sure Delete ledger?":"确认删除该流程的凭证数据?"))
                {
                    WorkflowService.RemoveCostFee(data.Id);

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(mainListPart.FindForm(), LocalData.IsEnglish ? "Delete Successfully." : "删除成功!.");
                }

            }
            catch (Exception ex)
            {
                if (toolBar != null)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(((Control)toolBar).FindForm(), ex.Message);
                }
            }
        }
        #endregion

        #region 当前行发生改变时
        private void OnMainListPartCurrentChanged(object sender, object data)
        {
            WorkFlowData workFlow = data as WorkFlowData;
            if (workFlow != null)
            {
                ShowFlowChat();

                RefreshToolbars(workFlow);
                //不是自己申请的，无法进行取消操作
                if (workFlow.OwnerUserId != LocalData.UserInfo.LoginID)
                {
                    toolBar.SetEnable("barCancel", false);
                    return;
                }
            }
            else
            {
                ShowFlowChat();
                RefreshToolbars(null);
            }


        }
        #endregion

        #region 查询
        private void OnSearchPartSearched(object sender, object results)
        {
            mainListPart.DataSource = results;
        }
        #endregion

        #region 双击流程图
        /// <summary>
        /// 双击流程图、查看任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void flowChatService_WorkItemDoubleClick(object sender, WorkItemEventArgs e)
        {
            FlowChartNode node = sender as FlowChartNode;
            if (node != null && node.Id != Guid.Empty)
            {
                WorkListEditPart newPart = WorkItem.Items.AddNew<WorkListEditPart>();
                Microsoft.Practices.CompositeUI.SmartParts.IWorkspace workSpace = WorkItem.RootWorkItem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                newPart.WFFormType = WorkFlowFormType.View;
                newPart.WorkInfoID = e.WorkInfo.WorkflowID;
                newPart.CurrentID = node.Id;

                SmartPartInfo sm = new SmartPartInfo();
                sm.Description = sm.Title = LocalData.IsEnglish ? "ViewWork" : "查看任务";
                workSpace.Show(newPart, sm);
            }
        }
        #endregion

        #region 刷新工具栏状态
        /// <summary>
        /// 刷新工具栏状态
        /// </summary>
        private void RefreshToolbars(WorkFlowData workFlow)
        {
            toolBar.SetEnable("barDoTask", true);
            toolBar.SetEnable("barCancel", true);
            toolBar.SetEnable("barPrint", true);

            if (workFlow == null)
            {
                toolBar.SetEnable("barDoTask", false);
                toolBar.SetEnable("barCancel", false);
                toolBar.SetEnable("barPrint", false);

                return;
            }
            //已经取消、被打回、完成的，不能再取消
            if (workFlow.State == WorkflowState.Cancel
             || workFlow.State == WorkflowState.Return
             || workFlow.State == WorkflowState.Finished)
            {
                toolBar.SetEnable("barCancel", false);
            }
            if (workFlow.WorkFlowConfigID == Utility.BussinessExpense
            || workFlow.WorkFlowConfigID == Utility.NotBussinessExpense
            || workFlow.WorkFlowConfigID == Utility.NotBussinessExpenseHH
            || workFlow.WorkFlowConfigID == Utility.OtherBussinessExpense
            || workFlow.WorkFlowConfigID == Utility.RoyaltyExpense
            || workFlow.WorkFlowConfigID == Utility.FixedAssetsExpense
            || workFlow.WorkFlowConfigID == Utility.LoanExpense
            || workFlow.WorkFlowConfigID == Utility.Allocation)
            {
                if (workFlow.State == WorkflowState.Finished)
                {
                    toolBar.SetEnable("barAuditor", true);
                    toolBar.SetEnable("barAuditorMerger", true);
                    toolBar.SetEnable("barUnAudiror", true);
                    if (string.IsNullOrEmpty(workFlow.VoucherNo))
                    {
                        toolBar.SetEnable("barUnAudiror", false);
                    }
                }
                else
                {
                    toolBar.SetEnable("barAuditor", false);
                    toolBar.SetEnable("barAuditorMerger", false);
                    toolBar.SetEnable("barUnAudiror", false);
                }
            }
            else
            {
                toolBar.SetEnable("barAuditor", false);
                toolBar.SetEnable("barAuditorMerger", false);
                toolBar.SetEnable("barUnAudiror", false);
            }


        }

        #endregion
        #endregion

    }


}

