
//-----------------------------------------------------------------------
// <copyright file="DeployForm.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.UI
{
    using System;
    using System.Windows.Forms;
    using ICP.WF.ServiceInterface;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using ICP.WF.ServiceInterface.DataObject;
    using System.Data;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.WF.Controls;
    using System.Collections.Generic;
    using System.Linq;
    using ICP.Framework.CommonLibrary.Common;
    using DevExpress.XtraNavBar;
    using System.Collections;
    using ICP.Sys.ServiceInterface;
    using System.ServiceModel;
    using ICP.Framework.ClientComponents.Controls;
    using ICP.Sys.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary;
    using ICP.Common.ServiceInterface;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Server;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 部署表单和数据源界面
    /// </summary>
    [SmartPart]
    public partial class WorkListEditPart : DevExpress.XtraEditors.XtraUserControl
    {
        #region 初始化与注销
        public WorkListEditPart()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(WorkListEditPart_Disposed);
        }

        void WorkListEditPart_Disposed(object sender, EventArgs e)
        {
            this.workflowData = null;
            this.workItemInfo = null;
            this.DepFormIDList = null;
            this.FinishData = null;
            if (this.UCWorkNewList != null)
            {
                this.WorkItem.Items.Remove(this.UCWorkNewList);
                this.UCWorkNewList.Selected -= this.UCWorkNewList_Selected;
                this.UCWorkNewList.Dispose();
                this.UCWorkNewList = null;
            }

            IDataFinder finder = DataFinderFactory.GetDataFinder(SystemFinderConstants.UserFinder);

            if (finder != null)
            {
                finder.DataChoosed -= finder_DataChoosed;
            }
            if (this.CurrentForm != null)
            {
                this.NCMain.Groups.Clear();
                this.NCMain.Controls.Clear();
                this.CurrentForm.Dispose();
                this.CurrentForm = null;
            }
            if (WorkItem != null)
            {
                WorkItem.Items.Remove(this);
                WorkItem = null;
            }
        }

        #endregion

        #region 服务
        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

        public IWorkflowService workFlowService
        {
            get
            {
                return ServiceClient.GetService<IWorkflowService>();
            }
        }
        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public IFormDesignClientService formDesignClientService
        {

            get
            {
                return ServiceClient.GetClientService<IFormDesignClientService>();
            }
        }
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        IDataFinderFactory DataFinderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IDataFinderFactory>();
            }
        }


        #endregion

        #region 属性

        /// <summary>
        /// 窗体类型
        /// </summary>
        public WorkFlowFormType WFFormType
        {
            get;
            set;
        }
        /// <summary>
        /// 编辑时是否显示主表
        /// </summary>
        public bool ShowMainTable
        {
            get;
            set;
        }
        /// <summary>
        /// 流程键值  [WorkFlowConfigID 的Key]
        /// </summary>
        public string WorkFlowConfigKey
        {
            get;
            set;
        }
        /// <summary>
        /// 流程配置ID
        /// </summary>
        public Guid WorkFlowConfigID
        {
            get;
            set;
        }
        /// <summary>
        /// 表单ID
        /// </summary>
        public Guid FormConfigID
        {
            get;
            set;
        }

        /// <summary>
        /// 任务ID
        /// </summary>
        public Guid WorkInfoID
        {
            get;
            set;
        }

        /// <summary>
        /// 当前活动环节的ID
        /// </summary>
        public Guid? CurrentID
        {
            get;
            set;
        }

        /// <summary>
        /// 流程信息(当前流程信息)
        /// </summary>
        private WorkItemInfo workItemInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 任务信息
        /// </summary>
        public WorkFlowData workflowData
        {
            get;
            set;
        }

        /// <summary>
        /// 当前活动的窗体
        /// </summary>
        public Control CurrentForm
        {
            get;
            set;
        }
        /// <summary>
        /// 凭证界面
        /// </summary>
        public UCLedgerPart CurrentLedgerPart
        {
            get;
            set;
        }

        /// <summary>
        /// 需要更新部门的流程列表
        /// </summary>
        public List<Guid> DepFormIDList
        {
            get;
            set;
        }
        #endregion

        #region 变量
        private WorkListTopPart UCWorkNewList;

        public string[] ResultValue = new string[] { "ID", "Code", "EName", "CName" };
        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            this.SuspendLayout();

            if (WFFormType == WorkFlowFormType.Add)
            {
                #region 新增

                bgMain.Visible = true;

                UCWorkNewList = WorkItem.Items.AddNew<WorkListTopPart>(Guid.NewGuid().ToString());
                bgcMain.Controls.Clear();
                UCWorkNewList.Dock = DockStyle.Fill;
                UCWorkNewList.WorkFlowConfigID = WorkFlowConfigID;
                bgcMain.Controls.Add(UCWorkNewList);

                UCWorkNewList.InitControls();
                UCWorkNewList.Selected += new ICP.Framework.ClientComponents.UIFramework.SelectedHandler(UCWorkNewList_Selected);


                ///新增时，初始化数据
                try
                {
                    workItemInfo = workFlowService.StartWorkflowInternal(WorkFlowConfigID, null, false, null, LocalData.UserInfo.LoginID, UCWorkNewList.OrganizationID, true);
                }
                catch (ApplicationException ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
                }
                if (workItemInfo == null)
                {
                    return;
                }
                if (workItemInfo.WorkFlowDataList != null && workItemInfo.WorkFlowDataList.Count > 0)
                {
                    workflowData = workItemInfo.WorkFlowDataList[0];
                }
                UCWorkNewList.WorkItemNo = workItemInfo.WorkNo;

                ///初始化表单
                InitForm();
                #endregion
            }
            else if (WFFormType == WorkFlowFormType.Edit)
            {
                #region 编辑
                ///是否只读
                bool isReadOnly = false;
                bgMain.Visible = this.ShowMainTable ? true : false;
                List<WorkItemInfo> infoList = workFlowService.GetWorkItemDetailList(WorkInfoID);
                bgMain.Visible = this.ShowMainTable ? true : false;
                bgForm.Visible = false;
                //显示主表信息[以便修改工作名称]
                if (this.ShowMainTable)
                {
                    UCWorkNewList = WorkItem.Items.AddNew<WorkListTopPart>(Guid.NewGuid().ToString());
                    bgcMain.Controls.Clear();
                    UCWorkNewList.Dock = DockStyle.Fill;
                    UCWorkNewList.WorkName = workflowData.Name;
                    UCWorkNewList.WorkItemNo = workflowData.No;
                    UCWorkNewList.WorkFlowConfigID = WorkFlowConfigID;
                    bgcMain.Controls.Add(UCWorkNewList);
                    UCWorkNewList.InitControls(false);
                    UCWorkNewList.Selected += new ICP.Framework.ClientComponents.UIFramework.SelectedHandler(UCWorkNewList_Selected);
                }

                WorkItemInfo currentInfo = null;

                ///找出当前环节的
                if (CurrentID != null)
                {
                    var currentWork = from i in infoList where i.ID == new Guid(CurrentID.ToString()) select i;

                    foreach (var c in currentWork)
                    {
                        currentInfo = c as WorkItemInfo;
                        if (infoList.Contains(currentInfo))
                        {
                            infoList.Remove(currentInfo);
                            break;
                        }
                    }
                }
                #region 判断是否为只读
                ///查看时，设置为只读
                if (WFFormType == WorkFlowFormType.View)
                {
                    isReadOnly = true;
                }
                //当前环节不是待办、在办状态的，不能编辑 
                if (currentInfo != null && currentInfo.State == WorkItemState.Finished)
                {
                    isReadOnly = true;
                }
                //整个流程是完成、取消、退回状态的，不能编辑 
                if (workflowData != null && (workflowData.State == WorkflowState.Cancel || workflowData.State == WorkflowState.Finished || workflowData.State == WorkflowState.Return))
                {
                    isReadOnly = true;
                }
                //当前状态是在办状态，但当前用户在当前环节的执行人列表中为空，不能编辑
                if (currentInfo != null && currentInfo.workItemParticipantsList == null)
                {
                    isReadOnly = true;
                }
                if (currentInfo != null && currentInfo.workItemParticipantsList != null)
                {
                    int i = (from d in currentInfo.workItemParticipantsList where d.ParticipantID == LocalData.UserInfo.LoginID select d).Count();
                    if (i == 0)
                    {
                        isReadOnly = true;
                    }
                }

                //已经是在办状态，但执行人不是当前用户，不能编辑
                if (currentInfo == null)
                {
                    isReadOnly = true;
                }
                else if (currentInfo.State == WorkItemState.Waiting && currentInfo != null && currentInfo.ExecutorID != null && new Guid(currentInfo.ExecutorID.ToString()) == LocalData.UserInfo.LoginID)
                {
                    isReadOnly = true;
                }
                #endregion

                #region 先加载当前环节的
                if (currentInfo != null)
                {
                    DevExpress.XtraNavBar.NavBarGroup bgCurrent = new DevExpress.XtraNavBar.NavBarGroup();
                    bgCurrent.Name = "bgbgCurrent";
                    bgCurrent.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
                    bgCurrent.Caption = currentInfo.WorkName;
                    bgCurrent.Tag = currentInfo.ID;
                    FormConfigID = currentInfo.FormID;

                    bgCurrent.Expanded = true;

                    DevExpress.XtraNavBar.NavBarGroupControlContainer bgcCurrent = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
                    bgcCurrent.Name = "bgcCurrent";
                    bgcCurrent.Tag = currentInfo.ID;


                    if (!string.IsNullOrEmpty(currentInfo.FormSchema))
                    {

                        if (currentInfo.IsMain)
                        {
                            if (currentInfo.FormData != null &&
                                 currentInfo.FormData.Tables.Count > 0 &&
                                 currentInfo.FormData.Tables[0].Rows.Count > 0 &&
                                 currentInfo.FormData.Tables[0].Columns.Count > 0 &&
                                 currentInfo.FormData.Tables[0].Columns.Contains("No") &&
                                 (currentInfo.FormData.Tables[0].Rows[0]["No"] == null ||
                                 currentInfo.FormData.Tables[0].Rows[0]["No"].ToString() == string.Empty))
                            {
                                currentInfo.FormData.Tables[0].Rows[0]["No"] = workflowData.No;
                            }
                        }
                        //获取主表单数据
                        WorkItemInfo mainForm = infoList.Where(o => o.IsMain == true).FirstOrDefault();
                        LWBaseForm cl = GetForm(currentInfo.WorkflowInstanceID, currentInfo.FormSchema, currentInfo.FormData);

                        UCLedgerPart ledgerPart = GetLedgerPart(currentInfo.FormID, infoList, cl.DataSource);

                        if (WFFormType == WorkFlowFormType.Edit)
                        {
                            #region 编辑
                            bool isEnabled = false;

                            if (currentInfo.State == WorkItemState.Waiting)
                            {
                                //待办状态时，判断当前用户是否在当前环节可执行人列表中
                                foreach (WorkItemParticipantsList wp in currentInfo.workItemParticipantsList)
                                {
                                    if ((wp.WorkItemID == currentInfo.ID && wp.ParticipantID == LocalData.UserInfo.LoginID))
                                    {
                                        isEnabled = true;
                                    }
                                }
                            }
                            else if (currentInfo.State == WorkItemState.Processing)
                            {
                                //在办状态时,判断执行是否是当前用户
                                if (currentInfo.ExecutorID == null)
                                {
                                    //执行人为空，可编辑(现在不存在这种情况,以为了预防老数据出现这种现象，先做判断)
                                    isEnabled = true;
                                }
                                if (new Guid(currentInfo.ExecutorID.ToString()) == LocalData.UserInfo.LoginID)
                                {
                                    isEnabled = true;
                                }

                            }
                            else
                            {
                                //完成状态时，设置为不可编辑
                                isEnabled = false;
                            }
                            if (!isEnabled)
                            {
                                cl.SetReadOnly(true);
                            }
                            #endregion
                        }

                        if (isReadOnly)
                        {
                            cl.SetReadOnly(true);
                        }
                        //凭证列表界面
                        if (ledgerPart != null)
                        {
                            bgCurrent.GroupClientHeight = ledgerPart.Height;
                            bgcCurrent.Height = ledgerPart.Height + 100;
                            ledgerPart.Dock = DockStyle.Fill;
                            bgcCurrent.Controls.Add(ledgerPart);

                            cl.IsShowLedgerPart = true;
                            bgcCurrent.Controls.Add(cl);
                            CurrentLedgerPart = ledgerPart;
                        }
                        else
                        {
                            cl.IsShowLedgerPart = false;
                            bgCurrent.GroupClientHeight = cl.Height;
                            bgcCurrent.Height = cl.Height;
                            bgcCurrent.Controls.Add(cl);
                        }

                        CurrentForm = cl;
                    }

                    bgCurrent.ControlContainer = bgcCurrent;

                    this.NCMain.Groups.Add(bgCurrent);
                    this.NCMain.Controls.Add(bgcCurrent);
                }
                #endregion

                if (isReadOnly)
                {
                    barSave.Enabled = false;
                    barFinish.Enabled = false;
                }

                #region 加载其它环节的
                foreach (WorkItemInfo info in infoList)
                {
                    DevExpress.XtraNavBar.NavBarGroup bg = new DevExpress.XtraNavBar.NavBarGroup();
                    bg.Name = "bg" + info.ID.ToString();
                    bg.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
                    bg.Caption = info.WorkName;
                    bg.Tag = "bg" + info.ID.ToString();

                    DevExpress.XtraNavBar.NavBarGroupControlContainer bgc = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
                    bgc.Name = "bgc" + info.ID.ToString();
                    bgc.Tag = info.ID;

                    this.NCMain.Groups.Add(bg);
                    this.NCMain.Controls.Add(bgc);

                    bg.ControlContainer = bgc;

                    if (!string.IsNullOrEmpty(info.FormSchema))
                    {
                        if (info.IsMain && info.WorkFlowID == WorkInfoID)
                        {
                            bg.Expanded = true;
                        }
                        else
                        {
                            bg.Expanded = false;
                        }

                        LWBaseForm cl = GetForm(info.WorkflowInstanceID, info.FormSchema, info.FormData);

                        UCLedgerPart ledgerPart = GetLedgerPart(info.FormID, infoList, cl.DataSource);

                        if (ledgerPart != null)
                        {
                            bg.GroupClientHeight = ledgerPart.Height;
                            bgc.Height = ledgerPart.Height;
                            NCMain.Height = NCMain.Height + bg.GroupClientHeight;
                            ledgerPart.Dock = DockStyle.Fill;
                            bgc.Controls.Add(ledgerPart);

                            cl.IsShowLedgerPart = true;
                            bgc.Controls.Add(cl);
                        }
                        else
                        {
                            cl.Dock = DockStyle.Fill;
                            cl.IsShowLedgerPart = false;
                            cl.SetReadOnly(true);
                            bg.GroupClientHeight = cl.Height;
                            bgc.Height = cl.Height;
                            NCMain.Height = NCMain.Height + bg.GroupClientHeight;
                            bgc.Controls.Add(cl);
                        }

                        if (info.IsMain)
                        {
                            cl.SetButtonReadonly();
                        }

                    }
                }
                #endregion

                #endregion
            }
            else if (WFFormType == WorkFlowFormType.View)
            {
                #region 查看

                bgMain.Visible = false;

                List<WorkItemInfo> infoList = workFlowService.GetWorkItemDetailList(WorkInfoID);

                workflowData = workFlowService.GetWorkFlowData(WorkInfoID, LocalData.UserInfo.LoginID);

                bgMain.Visible = false;
                bgForm.Visible = false;

                WorkItemInfo currentInfo = null;

                ///找出当前环节的
                if (CurrentID != null)
                {
                    var currentWork = from i in infoList where i.ID == new Guid(CurrentID.ToString()) select i;

                    foreach (var c in currentWork)
                    {
                        currentInfo = c as WorkItemInfo;
                        if (infoList.Contains(currentInfo))
                        {
                            infoList.Remove(currentInfo);
                            break;
                        }
                    }
                }
                barSave.Enabled = false;
                barFinish.Enabled = false;

                if (DataTypeHelper.GuidIsNullOrEmpty(WorkFlowConfigID))
                {
                    WorkFlowConfigID = currentInfo.WorkflowConfigID;
                }

                #region 先加载当前环节的
                if (currentInfo != null)
                {
                    DevExpress.XtraNavBar.NavBarGroup bgCurrent = new DevExpress.XtraNavBar.NavBarGroup();
                    bgCurrent.Name = "bgbgCurrent";
                    bgCurrent.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
                    bgCurrent.Caption = currentInfo.WorkName;
                    bgCurrent.Tag = currentInfo.ID;

                    bgCurrent.Expanded = true;

                    DevExpress.XtraNavBar.NavBarGroupControlContainer bgcCurrent = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
                    bgcCurrent.Name = "bgcCurrent";
                    bgcCurrent.Tag = currentInfo.ID;

                    FormConfigID = currentInfo.FormID;

                    if (!string.IsNullOrEmpty(currentInfo.FormSchema))
                    {
                        LWBaseForm cl = GetForm(currentInfo.WorkflowInstanceID, currentInfo.FormSchema, currentInfo.FormData);

                        UCLedgerPart ledgerPart = GetLedgerPart(currentInfo.FormID, infoList, cl.DataSource);
                        //凭证列表界面
                        if (ledgerPart != null)
                        {
                            bgCurrent.GroupClientHeight = ledgerPart.Height;
                            bgcCurrent.Height = ledgerPart.Height + 100;
                            ledgerPart.Dock = DockStyle.Fill;
                            bgcCurrent.Controls.Add(ledgerPart);

                            cl.IsShowLedgerPart = true;
                            bgcCurrent.Controls.Add(cl);

                            CurrentLedgerPart = ledgerPart;
                        }
                        else
                        {
                            cl.IsShowLedgerPart = false;
                            bgCurrent.GroupClientHeight = cl.Height;
                            bgcCurrent.Height = cl.Height;
                            bgcCurrent.Controls.Clear();
                            bgcCurrent.Controls.Add(cl);
                        }
                        CurrentForm = cl;
                    }

                    bgCurrent.ControlContainer = bgcCurrent;

                    this.NCMain.Groups.Add(bgCurrent);
                    this.NCMain.Controls.Add(bgcCurrent);
                    if (!infoList.Contains(currentInfo))
                    {
                        infoList.Add(currentInfo);
                    }
                }
                #endregion

                #region 加载其它环节的
                foreach (WorkItemInfo info in infoList)
                {
                    if (info.ID == currentInfo.ID)
                    {
                        //当前环节的，已经加载过了，所以不再重复加载
                        continue;
                    }
                    DevExpress.XtraNavBar.NavBarGroup bg = new DevExpress.XtraNavBar.NavBarGroup();
                    bg.Name = "bg" + info.ID.ToString();
                    bg.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
                    bg.Caption = info.WorkName;
                    bg.Tag = "bg" + info.ID.ToString();

                    DevExpress.XtraNavBar.NavBarGroupControlContainer bgc = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
                    bgc.Name = "bgc" + info.ID.ToString();
                    bgc.Tag = info.ID;

                    this.NCMain.Groups.Add(bg);
                    this.NCMain.Controls.Add(bgc);

                    bg.ControlContainer = bgc;

                    if (!string.IsNullOrEmpty(info.FormSchema))
                    {
                        LWBaseForm cl = GetForm(info.WorkflowInstanceID, info.FormSchema, info.FormData);

                        UCLedgerPart ledgerPart = GetLedgerPart(info.FormID, infoList, cl.DataSource);

                        if (ledgerPart != null)
                        {
                            bg.GroupClientHeight = ledgerPart.Height;
                            bgc.Height = ledgerPart.Height + 100;
                            NCMain.Height = NCMain.Height + bg.GroupClientHeight;
                            ledgerPart.Dock = DockStyle.Fill;
                            bgc.Controls.Add(ledgerPart);

                            cl.IsShowLedgerPart = true;
                            bgc.Controls.Add(cl);
                        }
                        else
                        {
                            cl.Dock = DockStyle.Fill;
                            cl.IsShowLedgerPart = false;
                            cl.SetReadOnly(true);
                            bg.GroupClientHeight = cl.Height;
                            bgc.Height = cl.Height;
                            NCMain.Height = NCMain.Height + bg.GroupClientHeight;
                            bgc.Controls.Add(cl);
                        }
                    }
                }
                #endregion

                #endregion
            }

            this.ResumeLayout(false);

        }

        /// <summary>
        /// 选择的组织结构发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        void UCWorkNewList_Selected(object sender, object data)
        {
            UserOrganizationTreeList item = data as UserOrganizationTreeList;
            if (item == null)
            {
                return;
            }
            if (appForm == null)
            {
                return;
            }
            DataSet ds = appForm.DataSource;
            if (ds == null || ds.Tables.Count == 0)
            {
                return;
            }
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return;
            }
            //根据申请部门，改变表单业务部门
            if (WorkFlowConfigID == new Guid("62DBA1E8-F503-4A48-9E6E-17C175987312") || //办公用品采购费用报销申请流程
                WorkFlowConfigID == new Guid("0087318E-84FD-40D8-9229-2AAAC00A4011") || //付款通知申请流程
                WorkFlowConfigID == new Guid("DEA9B18D-AEC2-4A00-8482-FB827082FA38") || //非业务费用报销申请流程
                WorkFlowConfigID == new Guid("9F81BA32-9A98-4E62-86E8-C46E2EFF2D75") || //非业务费用报销申请流程(黄晖)
                WorkFlowConfigID == new Guid("AC08A35C-BEEC-4E5A-9E12-76118403FAAF") || //固定资产申请流程
                WorkFlowConfigID == new Guid("3CD2D8A3-EDB6-44AA-909D-17F898BF5B81") || //借款申请流程
                WorkFlowConfigID == new Guid("565B6F73-9C68-4DB1-A437-E814C89B687E") || //其它业务支出报销申请流程
                WorkFlowConfigID == new Guid("573F5475-76B1-4948-B3C3-A2F31003210C") || //请假申请流程
                WorkFlowConfigID == new Guid("1ACB0832-9118-46FF-93B0-9E5038283D13") || //业务费用报销申请流程_Sales
                WorkFlowConfigID == new Guid("4EE24687-D3B9-47BC-A828-52B649BF3D08") || //业务费用报销申请流程
                WorkFlowConfigID == new Guid("FEC164CE-35AD-4688-9525-E312705DA794"))   //团队建设经费审批
            {
                List<string> depColumnList = new List<string>();

                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.ToUpper().Contains("DEPARTMENTID") || dc.ColumnName.ToUpper().Contains("DEPARTMENTNAME"))
                    {
                        depColumnList.Add(dc.ColumnName);
                    }
                }
                foreach (string columnName in depColumnList)
                {
                    if (dt.Columns[columnName].DataType == typeof(Guid))
                    {
                        dt.Rows[0][columnName] = item.ID;
                    }
                }
            }
            //部门改变,费用列表更新
            //foreach (LWDataGridView grid in appForm.gridViews)
            //{
            //    foreach (var col in grid.Columns)
            //    {
            //        if (col is LWCostItemColumn)
            //        {
            //            LWCostItemColumn s = col as LWCostItemColumn;
            //            if (s != null)
            //            {
            //                IWorkFlowExtendService extendService = (IWorkFlowExtendService)appForm.ServiceContainer.Get(typeof(IWorkFlowExtendService));
            //                List<CostItemData> DataList = extendService.GetCostItemList(item.ID);
            //                if (DataList.Count > 0)
            //                    s.DataSource = DataList;
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 申请表单的界面
        /// </summary>
        LWBaseForm appForm = null;

        /// <summary>
        /// 初始化表单界面
        /// </summary>
        private void InitForm()
        {
            DataSet ds = new DataSet();

            if (workflowData != null)
            {
                string formSchema = workflowData.FormSchema;
                ds = workflowData.FormData;

                if (!string.IsNullOrEmpty(formSchema))
                {
                    appForm = GetForm(workItemInfo.WorkflowInstanceID, formSchema, ds);

                    if (appForm != null)
                    {
                        CurrentForm = appForm;

                        bgcForm.Controls.Clear();

                        appForm.Dock = DockStyle.Fill;

                        bgcForm.Height = appForm.Height + 7;

                        bgcForm.Controls.Add(appForm);

                        if (WFFormType == WorkFlowFormType.Add)
                        {
                            bgForm.Expanded = true;
                        }

                        NCMain.Height = bgcForm.Height + 200;

                    }
                }
            }


        }

        /// <summary>
        /// 根据xml文件获得表单界面
        /// </summary>
        private LWBaseForm GetForm(Guid workflowInstanceID, string formSchema, DataSet formData)
        {
            DataSet ds = new DataSet();

            if (!string.IsNullOrEmpty(formSchema))
            {
                LWBaseForm cl = (LWBaseForm)FormBuildService.CreateObjectFromXmlData(WorkListModuleInit.serviceContainers, formSchema, new ArrayList());

                string workflowstr = workFlowService.GetDataCollectString(workflowInstanceID);
                DataCollector workflowCollector = Newtonsoft.Json.JsonConvert.DeserializeObject<DataCollector>(workflowstr);
                Dictionary<string, object> workflowConstants = workflowCollector.DataCollect;

                cl.CommonConstants = workflowConstants;

                cl.DataSource = formData;

                cl.Dock = DockStyle.Fill;

                return cl;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 工作名(申请人+报销+工作名)
        /// </summary>
        public string WorkName
        {
            get
            {
                string str = string.Empty;
                if (workflowData == null)
                {
                    return str;
                }
                if (!workflowData.Name.Contains("报销") && !workflowData.Name.Contains("Pay"))
                {
                    str = (LocalData.IsEnglish ? "Pay" : "报销") + workflowData.Name;
                }
                else
                {
                    str = workflowData.Name;
                }
                if (!workflowData.Name.Contains(workflowData.OwnerUserName))
                {
                    str = workflowData.OwnerUserName + str;
                }
                //黄晖的流程，直接返回工作名
                if (WorkFlowConfigID == Utility.NotBussinessExpenseHH)
                {
                    return workflowData.Name;
                }

                return str;
            }
        }


        /// <summary>
        /// 获得凭证列表界面
        /// </summary>
        /// <returns></returns>
        public List<WFLedgerList> GetLedger(Guid formID, List<WorkItemInfo> workItemList, DataSet currentDataSet, Guid workFlowConfigID)
        {
            WorkFlowConfigID = workFlowConfigID;
            //UCLedgerPart part = workItem.Items.AddNew<UCLedgerPart>();
            WFLedgerMaster master = new WFLedgerMaster();
            List<WFLedgerList> dataList = new List<WFLedgerList>();
            decimal totalDRAmount = 0, totalCRAmount = 0;
            Guid deptID = Guid.Empty, companyID = Guid.Empty, newCompanyID = Guid.Empty;
            string companyName = string.Empty;
            Guid inCompanyID = Guid.Empty, outCompanyID = Guid.Empty;
            //try
            //{
            //    part.startDate = Convert.ToDateTime(workItemList[0].FormData.Tables[0].Rows[0]["Date"]);
            //}
            //catch
            //{

            //}

            #region 获得申请表单中的部门所属公司
            if (formID == Utility.NewAccountAuditorFormID
                || formID == Utility.NewCashierAuditorFormID)
            {
                WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo data) { return data.WorkFlowID == WorkInfoID && data.IsMain; });
                if (applyForm != null
                 && applyForm.FormData != null
                 && applyForm.FormData.Tables[0] != null)
                {
                    foreach (DataColumn dc in applyForm.FormData.Tables[0].Columns)
                    {
                        if (WorkFlowConfigID == Utility.Allocation)
                        {
                            #region 资金调拨流程
                            //资金调拨流程中有两个部门                          
                            //调入方
                            if ((dc.ColumnName.Contains(WWFConstants.DepartmentID) || dc.ColumnName.Contains(WWFConstants.DepartmentName))
                                && !dc.ColumnName.ToUpper().Contains("OUT")
                                )
                            {
                                inCompanyID = DataTypeHelper.GetGuid(applyForm.FormData.Tables[0].Rows[0][dc.ColumnName], Guid.Empty);
                                OrganizationInfo inOrgInfo = OrganizationService.GetOrganizationParentCompanyID(inCompanyID);
                                if (inOrgInfo != null)
                                {
                                    inCompanyID = inOrgInfo.ID;
                                }
                            }
                            //调出方
                            if ((dc.ColumnName.Contains(WWFConstants.DepartmentID) || dc.ColumnName.Contains(WWFConstants.DepartmentName))
                               && dc.ColumnName.ToUpper().Contains("OUT")
                               )
                            {
                                outCompanyID = DataTypeHelper.GetGuid(applyForm.FormData.Tables[0].Rows[0][dc.ColumnName], Guid.Empty);
                            }
                            if (formID == Utility.NewAccountAuditorFormID)
                            {
                                //调入会计审核
                                deptID = inCompanyID;
                                newCompanyID = outCompanyID;
                            }
                            else if (formID == Utility.NewCashierAuditorFormID)
                            {
                                //调出会计审核
                                deptID = outCompanyID;
                                newCompanyID = inCompanyID;
                            }
                            //找到对方公司的名称
                            if (newCompanyID != null && newCompanyID != Guid.Empty)
                            {
                                OrganizationInfo orgInfo = OrganizationService.GetOrganizationInfo(newCompanyID);
                                if (orgInfo != null)
                                {
                                    companyName = orgInfo.CShortName;
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region 其它流程
                            if (dc.ColumnName.Contains(WWFConstants.DepartmentID) ||
                                           dc.ColumnName.Contains(WWFConstants.DepartmentName))
                            {
                                deptID = DataTypeHelper.GetGuid(applyForm.FormData.Tables[0].Rows[0][dc.ColumnName], Guid.Empty);
                                break;
                            }
                            #endregion
                        }
                    }

                    if (deptID != Guid.Empty)
                    {
                        OrganizationInfo orgInfo = OrganizationService.GetOrganizationParentCompanyID(deptID);
                        if (orgInfo != null)
                        {
                            companyID = orgInfo.ID;
                        }
                        else
                        {
                            companyID = LocalData.UserInfo.DefaultCompanyID;
                        }
                    }
                }
            }
            #endregion

            //if (part.CompanyID == Guid.Empty)
            //{
            //    part.CompanyID = companyID;
            //}

            //if (WFFormType == WorkFlowFormType.Edit)
            //{
            //    //申请部门为表单中的部门，不能为流程中的申请部门
            //    //例如：上海公司申请成都的计提工资流程
            //    part.ApplyDeptID = deptID;
            //    part.ApplyUserID = workflowData.OwnerUserId;
            //}

            if (WorkFlowConfigID == Utility.BussinessExpense)
            {
                #region 业务费用
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 业务费用--会计审核
                    if (currentDataSet.Tables[1].Rows[0][0] == null
                   || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的数据,从申请表单中去提取数据
                        //找出申请表单中的内容
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        //part.IsGetServerData = true;
                        //part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;
                        foreach (DataRow dr in applyForm.FormData.Tables[1].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = DataTypeHelper.GetGuid(dr["ChargeCode"], Guid.Empty);
                            item.Remark = DataTypeHelper.GetString(dr["Remark"], "");
                            item.DRAmt = DataTypeHelper.GetDecimal(dr["Amount"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid(dr["CurrencyCode"], Guid.Empty);
                            item.CRAmt = 0.0m;

                            //总金额
                            totalDRAmount += DataTypeHelper.GetDecimal(item.DRAmt, 0);
                            totalCRAmount += DataTypeHelper.GetDecimal(item.CRAmt, 0);

                            dataList.Add(item);
                            //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }
                        //插入一行默认的凭证信息 摘要=工作名，贷方=总金额，科目=现金
                        WFLedgerList newItem = new WFLedgerList();
                        if (companyID == new Guid("9A4725F3-E7CF-4AD3-91A4-988892AF2781"))
                        {
                            //长沙公司
                            newItem.GLID = Utility.RMBCSCashGLID;
                        }
                        else
                        {
                            newItem.GLID = Utility.RMBCashGLID;
                        }
                        newItem.CRAmt = (totalDRAmount > totalCRAmount) ? (totalDRAmount - totalCRAmount) : 0.0m;
                        newItem.DRAmt = (totalCRAmount > totalDRAmount) ? (totalCRAmount - totalDRAmount) : 0.0m;
                        newItem.Remark = WorkName;
                        dataList.Add(newItem);
                        //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, null));
                        #endregion
                    }
                    else
                    {
                        // 已保存过的数据，直接从会计审核表中取出
                        //SetAccountAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    //part.IsShowReceiptQty = true;
                    //part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    //part.BindDataSource(master);

                    return dataList;
                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 业务费用--出纳支付
                    //SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);
                    //part.CompanyID = companyID;

                    return dataList;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.NotBussinessExpense)
            {
                #region 非业务费用
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 非业务费用--会计审核
                    if (currentDataSet.Tables[1].Rows[0][0] == null
                        || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的数据,从申请表单中去提取数据
                        //找出申请表单中的内容
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        //part.IsGetServerData = true;
                        //part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;

                        foreach (DataRow dr in applyForm.FormData.Tables[1].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = DataTypeHelper.GetGuid(dr["ChargingCode"], Guid.Empty);
                            item.Remark = DataTypeHelper.GetString(dr["FeeRemark"], "");
                            item.DRAmt = DataTypeHelper.GetDecimal(dr["Amount"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid(dr["CurrencyCode"], Guid.Empty);
                            item.CRAmt = 0.0m;

                            //总金额
                            totalDRAmount += DataTypeHelper.GetDecimal(item.DRAmt, 0);
                            totalCRAmount += DataTypeHelper.GetDecimal(item.CRAmt, 0);
                            dataList.Add(item);

                            //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }
                        //插入一行默认的凭证信息
                        //摘要=工作名，贷方=总金额，科目=现金
                        WFLedgerList newItem = new WFLedgerList();
                        if (companyID == new Guid("9A4725F3-E7CF-4AD3-91A4-988892AF2781"))
                        {
                            //长沙公司
                            newItem.GLID = Utility.RMBCSCashGLID;
                        }
                        else
                        {
                            newItem.GLID = Utility.RMBCashGLID;
                        }
                        newItem.CRAmt = (totalDRAmount > totalCRAmount) ? (totalDRAmount - totalCRAmount) : 0.0m;
                        newItem.DRAmt = (totalCRAmount > totalDRAmount) ? (totalCRAmount - totalDRAmount) : 0.0m;
                        newItem.Remark = WorkName;
                        dataList.Add(newItem);
                        //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, null));
                        #endregion
                    }
                    else
                    {
                        //SetAccountAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    //part.IsShowReceiptQty = true;
                    //part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    //part.BindDataSource(master);

                    return dataList;

                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 非业务费-出纳支付
                    //SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);
                    //part.CompanyID = companyID;

                    return dataList;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.NotBussinessExpenseHH)
            {
                #region 非业务费用-黄晖
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 非业务费用(黄晖)--会计审核
                    if (currentDataSet.Tables[1].Rows[0][0] == null
                    || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的数据,从申请表单中去提取数据
                        //找出申请表单中的内容
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        //part.IsGetServerData = true;
                        //part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;

                        foreach (DataRow dr in applyForm.FormData.Tables[1].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = DataTypeHelper.GetGuid(dr["ChargeCode"], Guid.Empty);
                            item.Remark = DataTypeHelper.GetString(dr["Remark"], "");
                            item.DRAmt = DataTypeHelper.GetDecimal(dr["Amount"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid(dr["Currency"], Guid.Empty);
                            item.CRAmt = 0.0m;

                            //总金额
                            totalDRAmount += DataTypeHelper.GetDecimal(item.DRAmt, 0);
                            totalCRAmount += DataTypeHelper.GetDecimal(item.CRAmt, 0);
                            dataList.Add(item);
                            //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }
                        //插入一行默认的凭证信息
                        //摘要=工作名，贷方=总金额，科目=现金
                        WFLedgerList newItem = new WFLedgerList();
                        if (companyID == new Guid("9A4725F3-E7CF-4AD3-91A4-988892AF2781"))
                        {
                            //长沙公司
                            newItem.GLID = Utility.RMBCSCashGLID;
                        }
                        else
                        {
                            newItem.GLID = Utility.RMBCashGLID;
                        }
                        newItem.CRAmt = (totalDRAmount > totalCRAmount) ? (totalDRAmount - totalCRAmount) : 0.0m;
                        newItem.DRAmt = (totalCRAmount > totalDRAmount) ? (totalCRAmount - totalDRAmount) : 0.0m;
                        newItem.Remark = WorkName;
                        dataList.Add(newItem);
                        //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, null));
                        #endregion
                    }
                    else
                    {
                        //已保存过的数据，直接从出纳支付中找出数据
                        //SetCashierAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    //part.IsShowReceiptQty = true;
                    //part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    //part.BindDataSource(master);

                    return dataList;
                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 非业务费用(黄晖)--出纳支付
                    //SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);
                    //part.CompanyID = companyID;

                    return dataList;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.MovieExpense)
            {
                #region 影视公司
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 影视公司--会计审核
                    if (currentDataSet.Tables[1].Rows[0][0] == null
                    || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的数据,从申请表单中去提取数据
                        //找出申请表单中的内容
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        //part.IsGetServerData = true;
                        //part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;

                        foreach (DataRow dr in applyForm.FormData.Tables[1].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = DataTypeHelper.GetGuid(dr["ChargingCode"], Guid.Empty);
                            item.Remark = DataTypeHelper.GetString(dr["FeeRemark"], "");
                            item.DRAmt = DataTypeHelper.GetDecimal(dr["Amount"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid(dr["CurrencyCode"], Guid.Empty);
                            item.CRAmt = 0.0m;

                            //总金额
                            totalDRAmount += DataTypeHelper.GetDecimal(item.DRAmt, 0);
                            totalCRAmount += DataTypeHelper.GetDecimal(item.CRAmt, 0);
                            dataList.Add(item);
                            //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }
                        //插入一行默认的凭证信息
                        //摘要=工作名，贷方=总金额，科目=现金
                        WFLedgerList newItem = new WFLedgerList();
                        if (companyID == new Guid("9A4725F3-E7CF-4AD3-91A4-988892AF2781"))
                        {
                            //长沙公司
                            newItem.GLID = Utility.RMBCSCashGLID;
                        }
                        else
                        {
                            newItem.GLID = Utility.RMBCashGLID;
                        }
                        newItem.CRAmt = (totalDRAmount > totalCRAmount) ? (totalDRAmount - totalCRAmount) : 0.0m;
                        newItem.DRAmt = (totalCRAmount > totalDRAmount) ? (totalCRAmount - totalDRAmount) : 0.0m;
                        newItem.Remark = WorkName;
                        dataList.Add(newItem);
                        //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, null));
                        #endregion
                    }
                    else
                    {
                        //已保存过的数据，直接从出纳支付中找出数据
                        //SetCashierAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    //part.IsShowReceiptQty = true;
                    //part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    //part.BindDataSource(master);

                    return dataList;
                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 影视公司--出纳支付
                    //SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);
                    //part.CompanyID = companyID;

                    return dataList;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.OtherBussinessExpense)
            {
                #region 其它业务支出
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 其它业务支出--财务审核
                    if (currentDataSet.Tables[1].Rows.Count == 0
                      || currentDataSet.Tables[1].Rows[0][0] == null
                      || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的，从申请表单中取出数据
                        //借方金额=申请表单中的金额，摘要=流程名
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        //part.IsGetServerData = true;
                        //part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;

                        foreach (DataRow dr in applyForm.FormData.Tables[0].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = Guid.Empty;
                            item.Remark = workflowData.Name;
                            item.OrgAmt = DataTypeHelper.GetDecimal(dr["Amount"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid(dr["Currency"], Guid.Empty);
                            if (applyForm.FormData.Tables[0].Columns.Contains("Customer"))
                            {
                                item.CustomerID = DataTypeHelper.GetGuid(dr["Customer"], Guid.Empty);
                            }
                            item.CRAmt = 0.0m;
                            item.DRAmt = 0.0m;
                            dataList.Add(item);
                            //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }

                        #endregion
                    }
                    else
                    {
                        //已经保存过的，直接取表中的数据
                        //SetAccountAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    //part.IsShowReceiptQty = false;
                    //part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    //part.BindDataSource(master);

                    return dataList;

                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 其它业务支出--出纳支付
                    //SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);
                    //part.CompanyID = companyID;

                    return dataList;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.RoyaltyExpense)
            {
                #region 提成发放
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 提成发放的财务审核界面
                    if (currentDataSet.Tables[1].Rows.Count == 0
                        || currentDataSet.Tables[1].Rows[0][0] == null
                        || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的，从申请表单中取出数据
                        //借方金额=申请表单中的金额，摘要=流程名
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        //part.IsGetServerData = true;
                        //part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;

                        foreach (DataRow dr in applyForm.FormData.Tables[0].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = new Guid("991FE590-A42C-47E1-8C2F-A7A294D0A07C");
                            string CustomRemark = DataTypeHelper.GetString(dr["CustomRemark"], "");
                            item.Remark = CustomRemark.Replace("【", "").Replace("】", "").Replace(":", "");
                            item.DRAmt = DataTypeHelper.GetDecimal(dr["Amount"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid("CurrencyID", Guid.Empty);
                            item.CRAmt = 0.0m;

                            dataList.Add(item);
                            //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }

                        #endregion
                    }
                    else
                    {
                        //已经保存过的，直接取表中的数据
                        //SetAccountAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    //part.IsShowReceiptQty = true;
                    master.LedgerList = dataList;
                    //part.BindDataSource(master);

                    return dataList;

                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region  提成发放--出纳支付
                    //SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);
                    //part.CompanyID = companyID;

                    return dataList;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.LoanExpense)
            {
                #region 借款申请
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 借款申请--财务审核
                    if (currentDataSet.Tables[1].Rows.Count == 0
                        || currentDataSet.Tables[1].Rows[0][0] == null
                        || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的，从申请表单中取出数据
                        //借方金额=申请表单中的金额，摘要=流程名
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        //part.IsGetServerData = true;
                        //part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;

                        foreach (DataRow dr in applyForm.FormData.Tables[0].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = new Guid("569C29BC-C815-464C-B928-AE3B0F41AFAA");//11330103 员工借款
                            item.Remark = DataTypeHelper.GetString(dr["CustomRemark"], string.Empty);
                            item.DRAmt = DataTypeHelper.GetDecimal(dr["Amount"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid("Currency", Guid.Empty);
                            item.CRAmt = 0.0m;

                            dataList.Add(item);
                            //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }

                        #endregion
                    }
                    else
                    {
                        //已经保存过的，直接取表中的数据
                        //SetAccountAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    //part.IsShowReceiptQty = true;
                    //part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    //part.BindDataSource(master);

                    return dataList;
                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 借款申请--出纳支付
                    //SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);

                    return dataList;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.FixedAssetsExpense)
            {
                #region 固定资产
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 固定资产--财务审核
                    if (currentDataSet.Tables[1].Rows.Count == 0
                        || currentDataSet.Tables[1].Rows[0][0] == null
                        || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的，从申请表单中取出数据
                        //借方金额=申请表单中的金额，摘要=流程名
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        //part.IsGetServerData = true;
                        //part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;

                        foreach (DataRow dr in applyForm.FormData.Tables[0].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = Guid.Empty;//11330103 员工借款
                            item.Remark = workflowData.Name;
                            item.DRAmt = DataTypeHelper.GetDecimal(dr["Price"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid("Currency", Guid.Empty);
                            item.CRAmt = 0.0m;

                            dataList.Add(item);
                            //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }
                        #endregion
                    }
                    else
                    {
                        //已经保存过的，直接取表中的数据
                        //SetAccountAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    //part.IsShowReceiptQty = true;
                    //part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    //part.BindDataSource(master);

                    return dataList;
                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 固定资产--出纳支付
                    //SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);
                    //part.CompanyID = companyID;
                    return dataList;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.Allocation)
            {
                #region 物流部资金调拨
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 物流部资金调拨--进方会计
                    if (currentDataSet.Tables[1].Rows.Count == 0
                        || currentDataSet.Tables[1].Rows[0][0] == null
                        || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的，从申请表单中取出数据
                        //借方金额=申请表单中的金额，摘要=流程名
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        //part.IsGetServerData = true;

                        string remakr = string.Empty;
                        decimal amount = DataTypeHelper.GetDecimal(applyForm.FormData.Tables[0].Rows[0]["Amount"], 0);
                        DateTime outGeneralManagerDate = DataTypeHelper.GetDateTime(applyForm.FormData.Tables[0].Rows[0]["OutGeneralManagerDate"], DateTime.Now);
                        decimal rate = 0;
                        if (inCompanyID == Guid.Empty || inCompanyID == null)
                        {
                            inCompanyID = LocalData.UserInfo.DefaultCompanyID;
                        }
                        //借方 转入方: 借方是现金
                        decimal Amount = DataTypeHelper.GetDecimal(applyForm.FormData.Tables[0].Rows[0]["Amount"], 0);
                        WFLedgerList drItem = new WFLedgerList();
                        drItem.Remark = "从" + companyName + "调入现金";
                        drItem.DRAmt = 0.0m;
                        drItem.CurrencyID = DataTypeHelper.GetGuid(applyForm.FormData.Tables[0].Rows[0]["Currency"], Guid.Empty);
                        drItem.CRAmt = Amount;

                        //贷方 转入方: 贷方是内部往来
                        WFLedgerList crItem = new WFLedgerList();
                        crItem.Remark = "从" + companyName + "调入现金";
                        crItem.DRAmt = Amount;
                        crItem.CurrencyID = DataTypeHelper.GetGuid(applyForm.FormData.Tables[0].Rows[0]["Currency"], Guid.Empty);
                        crItem.CRAmt = 0.0m;
                        crItem.CustomerID = GetCompanyCustomerID(outCompanyID);

                        if (drItem.CurrencyID == Utility.RMBID)
                        {
                            //人民币
                            drItem.GLID = Utility.RMBCashGLID;
                            crItem.GLID = Utility.IntercompanyRMBID;
                        }
                        else if (drItem.CurrencyID == Utility.HKDID)
                        {
                            //港币
                            drItem.GLID = Utility.HKDCashGLID;
                            crItem.GLID = Utility.IntercompanyHKDID;

                            rate = ConfigureService.GetCompanyStandardCurrencyRate(inCompanyID, Utility.HKDID, outGeneralManagerDate);
                            if (rate == 0)
                            {
                                rate = 1;
                            }
                            drItem.OrgAmt = amount;
                            drItem.DRAmt = amount * rate;

                            crItem.OrgAmt = amount;
                            crItem.CRAmt = amount * rate;
                        }
                        else if (drItem.CurrencyID == Utility.USDID)
                        {
                            //美金
                            drItem.GLID = Utility.USDCashGLID;
                            crItem.GLID = Utility.IntercompanyUSDID;

                            rate = ConfigureService.GetCompanyStandardCurrencyRate(inCompanyID, Utility.USDID, outGeneralManagerDate);
                            if (rate == 0)
                            {
                                rate = 1;
                            }
                            drItem.OrgAmt = amount;
                            drItem.DRAmt = amount * rate;

                            crItem.OrgAmt = amount;
                            crItem.CRAmt = amount * rate;
                        }

                        dataList.Add(drItem);
                        dataList.Add(crItem);
                        //part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(0, drItem.CurrencyID));
                        //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(1, crItem.CurrencyID));
                        #endregion
                    }
                    else
                    {
                        //已经保存过的，直接取表中的数据
                        //SetAccountAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    //part.IsShowReceiptQty = false;
                    //part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    //part.BindDataSource(master);

                    return dataList;
                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 物流部资金调拨--出方会计
                    if (currentDataSet.Tables[1].Rows.Count == 0
                        || currentDataSet.Tables[1].Rows[0][0] == null
                        || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        //未保存过的，从申请表单中取
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        //part.IsGetServerData = true;

                        string remakr = string.Empty;
                        decimal amount = DataTypeHelper.GetDecimal(applyForm.FormData.Tables[0].Rows[0]["Amount"], 0);
                        DateTime outGeneralManagerDate = DataTypeHelper.GetDateTime(applyForm.FormData.Tables[0].Rows[0]["OutGeneralManagerDate"], DateTime.Now);
                        decimal rate = 0;
                        if (outCompanyID == Guid.Empty || outCompanyID == null)
                        {
                            outCompanyID = LocalData.UserInfo.DefaultCompanyID;
                        }

                        //借方 转出方：借方是内部往来
                        WFLedgerList drItem = new WFLedgerList();
                        drItem.Remark = "转" + companyName + "现金";
                        drItem.DRAmt = amount;
                        drItem.CurrencyID = DataTypeHelper.GetGuid(applyForm.FormData.Tables[0].Rows[0]["Currency"], Guid.Empty);
                        drItem.CRAmt = 0.0m;
                        drItem.CustomerID = GetCompanyCustomerID(inCompanyID);
                        //贷方 转出方：贷方是现金
                        WFLedgerList crItem = new WFLedgerList();
                        crItem.Remark = "转" + companyName + "现金";
                        crItem.DRAmt = 0.0m;
                        crItem.CurrencyID = DataTypeHelper.GetGuid(applyForm.FormData.Tables[0].Rows[0]["Currency"], Guid.Empty);
                        crItem.CRAmt = amount;
                        if (drItem.CurrencyID == Utility.RMBID)
                        {
                            drItem.GLID = Utility.IntercompanyRMBID;
                            crItem.GLID = Utility.RMBCashGLID;
                        }
                        else if (drItem.CurrencyID == Utility.HKDID)
                        {
                            drItem.GLID = Utility.IntercompanyHKDID;
                            crItem.GLID = Utility.HKDCashGLID;

                            rate = ConfigureService.GetCompanyStandardCurrencyRate(outCompanyID, Utility.HKDID, outGeneralManagerDate);
                            if (rate == 0)
                            {
                                rate = 1;
                            }
                            drItem.OrgAmt = amount;
                            drItem.DRAmt = amount * rate;

                            crItem.OrgAmt = amount;
                            crItem.CRAmt = amount * rate;
                        }
                        else if (drItem.CurrencyID == Utility.USDID)
                        {
                            drItem.GLID = Utility.IntercompanyUSDID;
                            crItem.GLID = Utility.USDCashGLID;

                            rate = ConfigureService.GetCompanyStandardCurrencyRate(outCompanyID, Utility.USDID, outGeneralManagerDate);
                            if (rate == 0)
                            {
                                rate = 1;
                            }
                            drItem.OrgAmt = amount;
                            drItem.DRAmt = amount * rate;

                            crItem.OrgAmt = amount;
                            crItem.CRAmt = amount * rate;
                        }

                        dataList.Add(drItem);
                        dataList.Add(crItem);
                        //part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(0, drItem.CurrencyID));
                        //part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(1, crItem.CurrencyID));
                    }
                    else
                    {
                        //已经保存过的，从出纳支付界面中取出数据
                        //SetCashierAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    //part.IsShowReceiptQty = false;
                    master.LedgerList = dataList;
                    //part.BindDataSource(master);

                    return dataList;
                    #endregion
                }
                #endregion
            }
            return null;
        }

        /// <summary>
        /// 获得凭证列表界面
        /// </summary>
        /// <returns></returns>
        public UCLedgerPart GetLedgerPart(Guid formID, List<WorkItemInfo> workItemList, DataSet currentDataSet)
        {
            UCLedgerPart part = WorkItem.Items.AddNew<UCLedgerPart>();
            WFLedgerMaster master = new WFLedgerMaster();
            List<WFLedgerList> dataList = new List<WFLedgerList>();
            decimal totalDRAmount = 0, totalCRAmount = 0;

            Guid deptID = Guid.Empty,
                companyID = Guid.Empty,
                newCompanyID = Guid.Empty;

            string companyName = string.Empty;

            Guid inCompanyID = Guid.Empty,
                outCompanyID = Guid.Empty;
            try
            {
                part.startDate = Convert.ToDateTime(workItemList[0].FormData.Tables[0].Rows[0]["Date"]);
            }
            catch
            {

            }

            #region 获得申请表单中的部门所属公司
            if (formID == Utility.NewAccountAuditorFormID
                || formID == Utility.NewCashierAuditorFormID)
            {
                WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo data) { return data.WorkFlowID == WorkInfoID && data.IsMain; });
                if (applyForm != null
                 && applyForm.FormData != null
                 && applyForm.FormData.Tables[0] != null)
                {
                    foreach (DataColumn dc in applyForm.FormData.Tables[0].Columns)
                    {
                        if (WorkFlowConfigID == Utility.Allocation)
                        {
                            #region 资金调拨流程
                            //资金调拨流程中有两个部门                          
                            //调入方
                            if ((dc.ColumnName.Contains(WWFConstants.DepartmentID) || dc.ColumnName.Contains(WWFConstants.DepartmentName))
                                && !dc.ColumnName.ToUpper().Contains("OUT")
                                )
                            {
                                inCompanyID = DataTypeHelper.GetGuid(applyForm.FormData.Tables[0].Rows[0][dc.ColumnName], Guid.Empty);
                                OrganizationInfo inOrgInfo = OrganizationService.GetOrganizationParentCompanyID(inCompanyID);
                                if (inOrgInfo != null)
                                {
                                    inCompanyID = inOrgInfo.ID;
                                }
                            }
                            //调出方
                            if ((dc.ColumnName.Contains(WWFConstants.DepartmentID) || dc.ColumnName.Contains(WWFConstants.DepartmentName))
                               && dc.ColumnName.ToUpper().Contains("OUT")
                               )
                            {
                                outCompanyID = DataTypeHelper.GetGuid(applyForm.FormData.Tables[0].Rows[0][dc.ColumnName], Guid.Empty);
                            }
                            if (formID == Utility.NewAccountAuditorFormID)//调出会计审核
                            {
                                deptID = outCompanyID;
                                newCompanyID = inCompanyID;
                            }
                            else if (formID == Utility.NewCashierAuditorFormID)//调入会计审核
                            {
                                deptID = inCompanyID;
                                newCompanyID = outCompanyID;
                            }
                            //找到对方公司的名称
                            if (newCompanyID != null && newCompanyID != Guid.Empty)
                            {
                                OrganizationInfo orgInfo = OrganizationService.GetOrganizationInfo(newCompanyID);
                                if (orgInfo != null)
                                {
                                    companyName = orgInfo.CShortName;
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region 其它流程
                            if (dc.ColumnName.Contains(WWFConstants.DepartmentID) ||
                                           dc.ColumnName.Contains(WWFConstants.DepartmentName))
                            {
                                deptID = DataTypeHelper.GetGuid(applyForm.FormData.Tables[0].Rows[0][dc.ColumnName], Guid.Empty);
                                break;
                            }
                            #endregion
                        }
                    }

                    if (deptID != Guid.Empty)
                    {
                        OrganizationInfo orgInfo = OrganizationService.GetOrganizationParentCompanyID(deptID);
                        if (orgInfo != null)
                        {
                            companyID = orgInfo.ID;
                        }
                        else
                        {
                            companyID = LocalData.UserInfo.DefaultCompanyID;
                        }
                    }
                }
            }
            #endregion

            if (part.CompanyID == Guid.Empty)
            {
                part.CompanyID = companyID;
            }

            if (WFFormType == WorkFlowFormType.Edit)
            {
                //申请部门为表单中的部门，不能为流程中的申请部门
                //例如：上海公司申请成都的计提工资流程
                part.ApplyDeptID = deptID;
                part.ApplyUserID = workflowData.OwnerUserId;
            }

            if (WorkFlowConfigID == Utility.BussinessExpense)
            {
                #region 业务费用
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 业务费用--会计审核
                    if (currentDataSet.Tables[1].Rows[0][0] == null
                   || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的数据,从申请表单中去提取数据
                        //找出申请表单中的内容
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        DateTime gmApplyDate = DataTypeHelper.GetDateTime(applyForm.FormData.Tables[0].Rows[0]["GMApplyDate"], DateTime.Now);
                        master.FeeDate = gmApplyDate.ToString();
                        part.IsGetServerData = true;
                        part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;
                        foreach (DataRow dr in applyForm.FormData.Tables[1].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = DataTypeHelper.GetGuid(dr["ChargeCode"], Guid.Empty);
                            item.Remark = DataTypeHelper.GetString(dr["Remark"], "");
                            item.DRAmt = DataTypeHelper.GetDecimal(dr["Amount"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid(dr["CurrencyCode"], Guid.Empty);
                            item.CRAmt = 0.0m;

                            //总金额
                            totalDRAmount += DataTypeHelper.GetDecimal(item.DRAmt, 0);
                            totalCRAmount += DataTypeHelper.GetDecimal(item.CRAmt, 0);

                            dataList.Add(item);
                            part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }
                        //插入一行默认的凭证信息 摘要=工作名，贷方=总金额，科目=现金
                        WFLedgerList newItem = new WFLedgerList();
                        if (companyID == new Guid("9A4725F3-E7CF-4AD3-91A4-988892AF2781"))
                        {
                            //长沙公司
                            newItem.GLID = Utility.RMBCSCashGLID;
                        }
                        else
                        {
                            newItem.GLID = Utility.RMBCashGLID;
                        }
                        newItem.CRAmt = (totalDRAmount > totalCRAmount) ? (totalDRAmount - totalCRAmount) : 0.0m;
                        newItem.DRAmt = (totalCRAmount > totalDRAmount) ? (totalCRAmount - totalDRAmount) : 0.0m;
                        newItem.Remark = WorkName;
                        dataList.Add(newItem);
                        part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, null));
                        #endregion
                    }
                    else
                    {
                        // 已保存过的数据，直接从会计审核表中取出
                        SetAccountAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    part.IsShowReceiptQty = true;
                    part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    part.BindDataSource(master);

                    return part;
                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 业务费用--出纳支付
                    WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                    if (applyForm == null)
                    {
                        return null;
                    }
                    DateTime gmApplyDate = DataTypeHelper.GetDateTime(applyForm.FormData.Tables[0].Rows[0]["GMApplyDate"], DateTime.Now);
                    master.FeeDate = gmApplyDate.ToString();
                    SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);
                    part.CompanyID = companyID;

                    return part;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.NotBussinessExpense)
            {
                #region 非业务费用
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 非业务费用--会计审核
                    if (currentDataSet.Tables[1].Rows[0][0] == null
                        || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的数据,从申请表单中去提取数据
                        //找出申请表单中的内容
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        DateTime gmApplyDate = DataTypeHelper.GetDateTime(applyForm.FormData.Tables[0].Rows[0]["GMApplyDate"], DateTime.Now);
                        master.FeeDate = gmApplyDate.ToString();
                        part.IsGetServerData = true;
                        part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;

                        foreach (DataRow dr in applyForm.FormData.Tables[1].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = DataTypeHelper.GetGuid(dr["ChargingCode"], Guid.Empty);
                            item.Remark = DataTypeHelper.GetString(dr["FeeRemark"], "");
                            item.DRAmt = DataTypeHelper.GetDecimal(dr["Amount"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid(dr["CurrencyCode"], Guid.Empty);
                            item.CRAmt = 0.0m;

                            //总金额
                            totalDRAmount += DataTypeHelper.GetDecimal(item.DRAmt, 0);
                            totalCRAmount += DataTypeHelper.GetDecimal(item.CRAmt, 0);
                            dataList.Add(item);

                            part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }
                        //插入一行默认的凭证信息
                        //摘要=工作名，贷方=总金额，科目=现金
                        WFLedgerList newItem = new WFLedgerList();
                        if (companyID == new Guid("9A4725F3-E7CF-4AD3-91A4-988892AF2781"))
                        {
                            //长沙公司
                            newItem.GLID = Utility.RMBCSCashGLID;
                        }
                        else if (companyID == new Guid("1DBF7671-0D2D-4F08-A8A9-3663A0DB0037"))
                        {
                            //马来西亚公司
                            newItem.GLID = Utility.RMBankGLID;
                        }
                        else
                        {
                            newItem.GLID = Utility.RMBCashGLID;
                        }
                        newItem.CRAmt = (totalDRAmount > totalCRAmount) ? (totalDRAmount - totalCRAmount) : 0.0m;
                        newItem.DRAmt = (totalCRAmount > totalDRAmount) ? (totalCRAmount - totalDRAmount) : 0.0m;
                        newItem.Remark = WorkName;
                        dataList.Add(newItem);
                        part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, null));
                        #endregion
                    }
                    else
                    {
                        SetAccountAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    part.IsShowReceiptQty = true;
                    part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    part.BindDataSource(master);

                    return part;

                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    //找出申请表单中的内容
                    WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                    if (applyForm == null)
                    {
                        return null;
                    }
                    DateTime gmApplyDate = DataTypeHelper.GetDateTime(applyForm.FormData.Tables[0].Rows[0]["GMApplyDate"], DateTime.Now);
                    master.FeeDate = gmApplyDate.ToString();
                    #region 非业务费-出纳支付
                    SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);
                    part.CompanyID = companyID;

                    return part;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.NotBussinessExpenseHH)
            {
                #region 非业务费用-黄晖
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 非业务费用(黄晖)--会计审核
                    if (currentDataSet.Tables[1].Rows[0][0] == null
                    || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的数据,从申请表单中去提取数据
                        //找出申请表单中的内容
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        part.IsGetServerData = true;
                        part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;

                        foreach (DataRow dr in applyForm.FormData.Tables[1].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = DataTypeHelper.GetGuid(dr["ChargeCode"], Guid.Empty);
                            item.Remark = DataTypeHelper.GetString(dr["Remark"], "");
                            item.DRAmt = DataTypeHelper.GetDecimal(dr["Amount"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid(dr["Currency"], Guid.Empty);
                            item.CRAmt = 0.0m;

                            //总金额
                            totalDRAmount += DataTypeHelper.GetDecimal(item.DRAmt, 0);
                            totalCRAmount += DataTypeHelper.GetDecimal(item.CRAmt, 0);
                            dataList.Add(item);
                            part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }
                        //插入一行默认的凭证信息
                        //摘要=工作名，贷方=总金额，科目=现金
                        WFLedgerList newItem = new WFLedgerList();
                        if (companyID == new Guid("9A4725F3-E7CF-4AD3-91A4-988892AF2781"))
                        {
                            //长沙公司
                            newItem.GLID = Utility.RMBCSCashGLID;
                        }
                        else
                        {
                            newItem.GLID = Utility.RMBCashGLID;
                        }
                        newItem.CRAmt = (totalDRAmount > totalCRAmount) ? (totalDRAmount - totalCRAmount) : 0.0m;
                        newItem.DRAmt = (totalCRAmount > totalDRAmount) ? (totalCRAmount - totalDRAmount) : 0.0m;
                        newItem.Remark = WorkName;
                        dataList.Add(newItem);
                        part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, null));
                        #endregion
                    }
                    else
                    {
                        //已保存过的数据，直接从出纳支付中找出数据
                        SetCashierAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    part.IsShowReceiptQty = true;
                    part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    part.BindDataSource(master);

                    return part;
                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 非业务费用(黄晖)--出纳支付
                    SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);
                    part.CompanyID = companyID;

                    return part;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.MovieExpense)
            {
                #region 影视公司
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 影视公司--会计审核
                    if (currentDataSet.Tables[1].Rows[0][0] == null
                    || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的数据,从申请表单中去提取数据
                        //找出申请表单中的内容
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        part.IsGetServerData = true;
                        part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;

                        foreach (DataRow dr in applyForm.FormData.Tables[1].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = DataTypeHelper.GetGuid(dr["ChargingCode"], Guid.Empty);
                            item.Remark = DataTypeHelper.GetString(dr["FeeRemark"], "");
                            item.DRAmt = DataTypeHelper.GetDecimal(dr["Amount"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid(dr["CurrencyCode"], Guid.Empty);
                            item.CRAmt = 0.0m;

                            //总金额
                            totalDRAmount += DataTypeHelper.GetDecimal(item.DRAmt, 0);
                            totalCRAmount += DataTypeHelper.GetDecimal(item.CRAmt, 0);
                            dataList.Add(item);
                            part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }
                        //插入一行默认的凭证信息
                        //摘要=工作名，贷方=总金额，科目=现金
                        WFLedgerList newItem = new WFLedgerList();
                        if (companyID == new Guid("9A4725F3-E7CF-4AD3-91A4-988892AF2781"))
                        {
                            //长沙公司
                            newItem.GLID = Utility.RMBCSCashGLID;
                        }
                        else
                        {
                            newItem.GLID = Utility.RMBCashGLID;
                        }
                        newItem.CRAmt = (totalDRAmount > totalCRAmount) ? (totalDRAmount - totalCRAmount) : 0.0m;
                        newItem.DRAmt = (totalCRAmount > totalDRAmount) ? (totalCRAmount - totalDRAmount) : 0.0m;
                        newItem.Remark = WorkName;
                        dataList.Add(newItem);
                        part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, null));
                        #endregion
                    }
                    else
                    {
                        //已保存过的数据，直接从出纳支付中找出数据
                        SetCashierAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    part.IsShowReceiptQty = true;
                    part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    part.BindDataSource(master);

                    return part;
                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 影视公司--出纳支付
                    SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);
                    part.CompanyID = companyID;

                    return part;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.OtherBussinessExpense)
            {
                #region 其它业务支出
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 其它业务支出--财务审核
                    if (currentDataSet.Tables[1].Rows.Count == 0
                      || currentDataSet.Tables[1].Rows[0][0] == null
                      || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的，从申请表单中取出数据
                        //借方金额=申请表单中的金额，摘要=流程名
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        DateTime gmApplyDate = DataTypeHelper.GetDateTime(applyForm.FormData.Tables[0].Rows[0]["GeneralManagerDate"], DateTime.Now);
                        master.FeeDate = gmApplyDate.ToString();
                        part.IsGetServerData = true;
                        part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;

                        foreach (DataRow dr in applyForm.FormData.Tables[0].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = Guid.Empty;
                            item.Remark = workflowData.Name;
                            item.OrgAmt = DataTypeHelper.GetDecimal(dr["Amount"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid(dr["Currency"], Guid.Empty);
                            if (applyForm.FormData.Tables[0].Columns.Contains("Customer"))
                            {
                                item.CustomerID = DataTypeHelper.GetGuid(dr["Customer"], Guid.Empty);
                            }
                            item.CRAmt = 0.0m;
                            item.DRAmt = 0.0m;
                            dataList.Add(item);
                            part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }

                        #endregion
                    }
                    else
                    {
                        //已经保存过的，直接取表中的数据
                        SetAccountAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    part.IsShowReceiptQty = false;
                    part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    part.BindDataSource(master);

                    return part;

                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 其它业务支出--出纳支付
                    WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                    if (applyForm == null)
                    {
                        return null;
                    }
                    DateTime gmApplyDate = DataTypeHelper.GetDateTime(applyForm.FormData.Tables[0].Rows[0]["GeneralManagerDate"], DateTime.Now);
                    master.FeeDate = gmApplyDate.ToString();
                    SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);
                    part.CompanyID = companyID;

                    return part;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.RoyaltyExpense)
            {
                #region 提成发放
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 提成发放的财务审核界面
                    if (currentDataSet.Tables[1].Rows.Count == 0
                        || currentDataSet.Tables[1].Rows[0][0] == null
                        || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的，从申请表单中取出数据
                        //借方金额=申请表单中的金额，摘要=流程名
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        DateTime gmApplyDate = DataTypeHelper.GetDateTime(applyForm.FormData.Tables[0].Rows[0]["GeneralManagerDate"], DateTime.Now);
                        master.FeeDate = gmApplyDate.ToString();
                        part.IsGetServerData = true;
                        part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;

                        foreach (DataRow dr in applyForm.FormData.Tables[0].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = new Guid("991FE590-A42C-47E1-8C2F-A7A294D0A07C");
                            string CustomRemark = DataTypeHelper.GetString(dr["CustomRemark"], "");
                            item.Remark = CustomRemark.Replace("【", "").Replace("】", "").Replace(":", "");
                            item.DRAmt = DataTypeHelper.GetDecimal(dr["Amount"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid("CurrencyID", Guid.Empty);
                            item.CRAmt = 0.0m;

                            dataList.Add(item);
                            part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }

                        #endregion
                    }
                    else
                    {
                        //已经保存过的，直接取表中的数据
                        SetAccountAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    part.IsShowReceiptQty = true;
                    master.LedgerList = dataList;
                    part.BindDataSource(master);

                    return part;

                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region  提成发放--出纳支付
                    SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);
                    part.CompanyID = companyID;

                    return part;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.LoanExpense)
            {
                #region 借款申请
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 借款申请--财务审核
                    if (currentDataSet.Tables[1].Rows.Count == 0
                        || currentDataSet.Tables[1].Rows[0][0] == null
                        || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的，从申请表单中取出数据
                        //借方金额=申请表单中的金额，摘要=流程名
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        DateTime gmApplyDate = DataTypeHelper.GetDateTime(applyForm.FormData.Tables[0].Rows[0]["GeneralManagerDate"], DateTime.Now);
                        master.FeeDate = gmApplyDate.ToString();
                        part.IsGetServerData = true;
                        part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;

                        foreach (DataRow dr in applyForm.FormData.Tables[0].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = new Guid("569C29BC-C815-464C-B928-AE3B0F41AFAA");//11330103 员工借款
                            item.Remark = DataTypeHelper.GetString(dr["CustomRemark"], string.Empty);
                            item.DRAmt = DataTypeHelper.GetDecimal(dr["Amount"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid("Currency", Guid.Empty);
                            item.CRAmt = 0.0m;

                            dataList.Add(item);
                            part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }

                        #endregion
                    }
                    else
                    {
                        //已经保存过的，直接取表中的数据
                        SetAccountAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    part.IsShowReceiptQty = true;
                    part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    part.BindDataSource(master);

                    return part;
                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 借款申请--出纳支付
                    SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);

                    return part;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.FixedAssetsExpense)
            {
                #region 固定资产
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 固定资产--财务审核
                    if (currentDataSet.Tables[1].Rows.Count == 0
                        || currentDataSet.Tables[1].Rows[0][0] == null
                        || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的，从申请表单中取出数据
                        //借方金额=申请表单中的金额，摘要=流程名
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        DateTime gmApplyDate = DataTypeHelper.GetDateTime(applyForm.FormData.Tables[0].Rows[0]["GeneralManagerDate"], DateTime.Now);
                        master.FeeDate = gmApplyDate.ToString();
                        part.IsGetServerData = true;
                        part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        int index = 0;

                        foreach (DataRow dr in applyForm.FormData.Tables[0].Rows)
                        {
                            WFLedgerList item = new WFLedgerList();
                            item.GLID = Guid.Empty;//11330103 员工借款
                            item.Remark = workflowData.Name;
                            item.DRAmt = DataTypeHelper.GetDecimal(dr["Price"], 0);
                            item.CurrencyID = DataTypeHelper.GetGuid("Currency", Guid.Empty);
                            item.CRAmt = 0.0m;

                            dataList.Add(item);
                            part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                            index++;
                        }
                        #endregion
                    }
                    else
                    {
                        //已经保存过的，直接取表中的数据
                        SetAccountAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    part.IsShowReceiptQty = true;
                    part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    part.BindDataSource(master);

                    return part;
                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 固定资产--出纳支付
                    SetCashierAuditorFormData(master, part, workItemList, currentDataSet, dataList, companyID);
                    part.CompanyID = companyID;
                    return part;
                    #endregion
                }
                #endregion
            }
            else if (WorkFlowConfigID == Utility.Allocation)
            {
                #region 物流部资金调拨
                if (formID == Utility.NewAccountAuditorFormID)
                {
                    #region 物流部资金调拨--出方会计
                    if (currentDataSet.Tables[1].Rows.Count == 0
                        || currentDataSet.Tables[1].Rows[0][0] == null
                        || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        #region 未保存过的，从申请表单中取出数据
                        //借方金额=申请表单中的金额，摘要=流程名
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        part.IsGetServerData = true;

                        string remakr = string.Empty;
                        decimal amount = DataTypeHelper.GetDecimal(applyForm.FormData.Tables[0].Rows[0]["Amount"], 0);
                        DateTime outGeneralManagerDate = DataTypeHelper.GetDateTime(applyForm.FormData.Tables[0].Rows[0]["OutGeneralManagerDate"], DateTime.Now);
                        master.FeeDate = outGeneralManagerDate.ToString();
                        decimal rate = 0;
                        if (inCompanyID == Guid.Empty || inCompanyID == null)
                        {
                            inCompanyID = LocalData.UserInfo.DefaultCompanyID;
                        }

                        //借方 转入方: 借方是现金
                        WFLedgerList drItem = new WFLedgerList();
                        OrganizationInfo inCompany = OrganizationService.GetOrganizationInfo(inCompanyID);
                        drItem.Remark = "转" + (inCompany != null ? inCompany.CShortName : "") + "现金";
                        drItem.DRAmt = 0.0m;
                        drItem.CurrencyID = DataTypeHelper.GetGuid(applyForm.FormData.Tables[0].Rows[0]["Currency"], Guid.Empty);
                        drItem.CRAmt = amount;

                        //贷方 转入方: 贷方是内部往来 
                        WFLedgerList crItem = new WFLedgerList();
                        crItem.Remark = "转" + (inCompany != null ? inCompany.CShortName : "") + "现金";
                        crItem.DRAmt = amount;
                        crItem.CurrencyID = DataTypeHelper.GetGuid(applyForm.FormData.Tables[0].Rows[0]["Currency"], Guid.Empty);
                        crItem.CRAmt = 0.0m;
                        crItem.CustomerID = GetCompanyCustomerID(inCompanyID);

                        if (drItem.CurrencyID == Utility.RMBID)
                        {
                            //人民币
                            drItem.GLID = Utility.RMBCashGLID;
                            crItem.GLID = Utility.IntercompanyRMBID;
                        }
                        else if (drItem.CurrencyID == Utility.HKDID)
                        {
                            //港币
                            drItem.GLID = Utility.HKDCashGLID;
                            crItem.GLID = Utility.IntercompanyHKDID;

                            rate = ConfigureService.GetCompanyStandardCurrencyRate(inCompanyID, Utility.HKDID, outGeneralManagerDate);
                            if (rate == 0)
                            {
                                rate = 1;
                            }
                            drItem.OrgAmt = amount;
                            drItem.DRAmt = 0;
                            drItem.CRAmt = amount * rate;

                            crItem.OrgAmt = amount;
                            crItem.CRAmt = 0;
                            crItem.DRAmt = amount * rate;
                        }
                        else if (drItem.CurrencyID == Utility.USDID)
                        {
                            //美金
                            drItem.GLID = Utility.USDCashGLID;
                            crItem.GLID = Utility.IntercompanyUSDID;

                            rate = ConfigureService.GetCompanyStandardCurrencyRate(inCompanyID, Utility.USDID, outGeneralManagerDate);
                            if (rate == 0)
                            {
                                rate = 1;
                            }
                            drItem.OrgAmt = amount;
                            drItem.CRAmt = amount * rate;
                            drItem.DRAmt = 0;

                            crItem.OrgAmt = amount;
                            crItem.CRAmt = 0;
                            crItem.DRAmt = amount * rate;
                        }

                        dataList.Add(drItem);
                        dataList.Add(crItem);
                        part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(0, drItem.CurrencyID));
                        part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(1, crItem.CurrencyID));
                        #endregion
                    }
                    else
                    {
                        //已经保存过的，直接取表中的数据
                        SetAccountAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    part.IsShowReceiptQty = false;
                    part.CompanyID = companyID;
                    master.LedgerList = dataList;
                    part.BindDataSource(master);

                    return part;
                    #endregion
                }
                else if (formID == Utility.NewCashierAuditorFormID)
                {
                    #region 物流部资金调拨--进方会计
                    if (currentDataSet.Tables[1].Rows.Count == 0
                        || currentDataSet.Tables[1].Rows[0][0] == null
                        || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
                    {
                        //未保存过的，从申请表单中取
                        WorkItemInfo applyForm = workItemList.Find(delegate (WorkItemInfo d) { return d.IsMain && d.WorkFlowID == WorkInfoID; });
                        if (applyForm == null)
                        {
                            return null;
                        }
                        master.Opinion = "0";
                        master.Remark = "";
                        part.IsGetServerData = true;

                        string remakr = string.Empty;
                        decimal amount = DataTypeHelper.GetDecimal(applyForm.FormData.Tables[0].Rows[0]["Amount"], 0);
                        DateTime outGeneralManagerDate = DataTypeHelper.GetDateTime(applyForm.FormData.Tables[0].Rows[0]["OutGeneralManagerDate"], DateTime.Now);
                        master.FeeDate = outGeneralManagerDate.ToString();
                        decimal rate = 0;
                        if (outCompanyID == Guid.Empty || outCompanyID == null)
                        {
                            outCompanyID = LocalData.UserInfo.DefaultCompanyID;
                        }

                        //借方 转出方：借方是内部往来
                        WFLedgerList drItem = new WFLedgerList();
                        OrganizationInfo inCompany = OrganizationService.GetOrganizationInfo(outCompanyID);
                        drItem.Remark = "从" + (inCompany != null ? inCompany.CShortName : "") + "调入现金";
                        drItem.DRAmt = 0.0m;
                        drItem.CurrencyID = DataTypeHelper.GetGuid(applyForm.FormData.Tables[0].Rows[0]["Currency"], Guid.Empty);
                        drItem.CRAmt = amount;
                        drItem.CustomerID = GetCompanyCustomerID(outCompanyID);
                        //贷方 转出方：贷方是现金
                        WFLedgerList crItem = new WFLedgerList();
                        crItem.Remark = "从" + (inCompany != null ? inCompany.CShortName : "") + "调入现金";
                        crItem.DRAmt = amount;
                        crItem.CurrencyID = DataTypeHelper.GetGuid(applyForm.FormData.Tables[0].Rows[0]["Currency"], Guid.Empty);
                        crItem.CRAmt = 0.0m;
                        if (drItem.CurrencyID == Utility.RMBID)
                        {
                            drItem.GLID = Utility.IntercompanyRMBID;
                            crItem.GLID = Utility.RMBCashGLID;
                        }
                        else if (drItem.CurrencyID == Utility.HKDID)
                        {
                            drItem.GLID = Utility.IntercompanyHKDID;
                            crItem.GLID = Utility.HKDCashGLID;

                            rate = ConfigureService.GetCompanyStandardCurrencyRate(outCompanyID, Utility.HKDID, outGeneralManagerDate);
                            if (rate == 0)
                            {
                                rate = 1;
                            }
                            drItem.OrgAmt = amount;
                            drItem.CRAmt = amount * rate;
                            drItem.DRAmt = 0;

                            crItem.OrgAmt = amount;
                            crItem.DRAmt = amount * rate;
                            crItem.CRAmt = 0;
                        }
                        else if (drItem.CurrencyID == Utility.USDID)
                        {
                            drItem.GLID = Utility.IntercompanyUSDID;
                            crItem.GLID = Utility.USDCashGLID;

                            rate = ConfigureService.GetCompanyStandardCurrencyRate(outCompanyID, Utility.USDID, outGeneralManagerDate);
                            if (rate == 0)
                            {
                                rate = 1;
                            }
                            drItem.OrgAmt = amount;
                            drItem.DRAmt = 0;
                            drItem.CRAmt = amount * rate;

                            crItem.OrgAmt = amount;
                            crItem.CRAmt = 0;
                            crItem.DRAmt = amount * rate;
                        }

                        dataList.Add(drItem);
                        dataList.Add(crItem);
                        part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
                        part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(0, drItem.CurrencyID));
                        part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(1, crItem.CurrencyID));
                    }
                    else
                    {
                        //已经保存过的，从出纳支付界面中取出数据
                        SetCashierAuditorFormData(master, part, currentDataSet, dataList);
                    }
                    part.IsShowReceiptQty = false;
                    master.LedgerList = dataList;
                    part.BindDataSource(master);

                    return part;
                    #endregion
                }
                #endregion
            }
            return null;
        }
        private Guid GetCompanyCustomerID(Guid companyID)
        {
            ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(companyID);
            if (configure != null)
            {
                return configure.CustomerID;
            }

            return Guid.Empty;
        }

        /// <summary>
        /// 设置出纳支付表单数据
        /// </summary>
        /// <param name="master"></param>
        /// <param name="part"></param>
        /// <param name="workItemList"></param>
        /// <param name="currentDataSet"></param>
        /// <param name="dataList"></param>
        private void SetCashierAuditorFormData(WFLedgerMaster master, UCLedgerPart part, List<WorkItemInfo> workItemList, DataSet currentDataSet, List<WFLedgerList> dataList, Guid companyID)
        {
            if (currentDataSet.Tables[1].Rows.Count == 0
                || currentDataSet.Tables[1].Rows[0][0] == null
                || currentDataSet.Tables[1].Rows[0][0].ToString() == string.Empty)
            {
                //未保存过的，从财务审核的表单中取数据
                SetCashierAuditorFormDataByAccount(master, part, workItemList, dataList, companyID);
            }
            else
            {
                //已经保存过的，从出纳支付界面中取出数据
                SetCashierAuditorFormData(master, part, currentDataSet, dataList);
            }
            part.IsShowReceiptQty = false;
            master.LedgerList = dataList;
            part.BindDataSource(master);
        }
        /// <summary>
        /// 设置出纳支付界面数据(取出纳支付表单中的数据)
        /// </summary>
        /// <param name="master"></param>
        /// <param name="part"></param>
        /// <param name="dt"></param>
        /// <param name="dataList"></param>
        private void SetCashierAuditorFormData(WFLedgerMaster master, UCLedgerPart part, DataSet ds, List<WFLedgerList> dataList)
        {
            master.Opinion = DataTypeHelper.GetString(ds.Tables[0].Rows[0]["Opinion"], "0");
            master.Remark = DataTypeHelper.GetString(ds.Tables[0].Rows[0]["Remark"], "");
            master.FeeDate = DataTypeHelper.GetString(ds.Tables[0].Rows[0]["FeeDate"], "");

            part.IsGetServerData = false;
            part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
            int index = 0;

            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                WFLedgerList item = new WFLedgerList();
                item.GLID = DataTypeHelper.GetGuid(dr[CashierAuditorFormDataColumns.GLID], Guid.Empty);
                item.GLFullName = DataTypeHelper.GetString(dr[CashierAuditorFormDataColumns.GLFullName], "");
                item.Remark = DataTypeHelper.GetString(dr[CashierAuditorFormDataColumns.DetailRemark], "");
                item.DRAmt = DataTypeHelper.GetDecimal(dr[CashierAuditorFormDataColumns.DRAmt], 0);
                item.CRAmt = DataTypeHelper.GetDecimal(dr[CashierAuditorFormDataColumns.CRAmt], 0);
                item.CustomerID = DataTypeHelper.GetGuid(dr[CashierAuditorFormDataColumns.CustomerID], Guid.Empty);
                item.CustomerName = DataTypeHelper.GetString(dr[CashierAuditorFormDataColumns.CustomerName], "");
                item.DeptID = DataTypeHelper.GetGuid(dr[CashierAuditorFormDataColumns.DeptID], Guid.Empty);
                item.DeptName = DataTypeHelper.GetString(dr[CashierAuditorFormDataColumns.DeptName], "");
                item.UserID = DataTypeHelper.GetGuid(dr[CashierAuditorFormDataColumns.UserID], Guid.Empty);
                item.UserName = DataTypeHelper.GetString(dr[CashierAuditorFormDataColumns.UserName], "");
                item.CurrencyID = DataTypeHelper.GetGuid(dr[CashierAuditorFormDataColumns.CurrencyID], Guid.Empty);
                if (ds.Tables[1].Columns.Contains("OrgAmt"))
                {
                    item.OrgAmt = DataTypeHelper.GetDecimal(dr[CashierAuditorFormDataColumns.OrgAmt], 0);
                }
                dataList.Add(item);
                part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                index++;
            }
            part.IsShowReceiptQty = false;
            master.LedgerList = dataList;
            part.BindDataSource(master);
        }
        /// <summary>
        /// 设置财务审核界面数据(取财务审核表单中的数据)
        /// </summary>
        /// <param name="master"></param>
        /// <param name="part"></param>
        /// <param name="ds"></param>
        /// <param name="dataList"></param>
        private void SetAccountAuditorFormData(WFLedgerMaster master, UCLedgerPart part, DataSet ds, List<WFLedgerList> dataList)
        {
            master.Opinion = DataTypeHelper.GetString(ds.Tables[0].Rows[0]["Opinion"], "0");
            master.Remark = DataTypeHelper.GetString(ds.Tables[0].Rows[0]["Remark"], "");
            master.ReceiptQty = DataTypeHelper.GetString(ds.Tables[0].Rows[0]["ReceiptQty"], "");

            part.IsGetServerData = false;
            part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
            int index = 0;
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                WFLedgerList item = new WFLedgerList();
                item.GLID = DataTypeHelper.GetGuid(dr[AccountAuditorFormDataColumns.GLID], Guid.Empty);
                item.GLFullName = DataTypeHelper.GetString(dr[AccountAuditorFormDataColumns.GLFullName], "");
                item.Remark = DataTypeHelper.GetString(dr[AccountAuditorFormDataColumns.DetailRemark], "");
                item.DRAmt = DataTypeHelper.GetDecimal(dr[AccountAuditorFormDataColumns.DRAmt], 0);
                item.CRAmt = DataTypeHelper.GetDecimal(dr[AccountAuditorFormDataColumns.CRAmt], 0);
                item.CustomerID = DataTypeHelper.GetGuid(dr[AccountAuditorFormDataColumns.CustomerID], Guid.Empty);
                item.CustomerName = DataTypeHelper.GetString(dr[AccountAuditorFormDataColumns.CustomerName], "");
                item.DeptID = DataTypeHelper.GetGuid(dr[AccountAuditorFormDataColumns.DeptID], Guid.Empty);
                item.DeptName = DataTypeHelper.GetString(dr[AccountAuditorFormDataColumns.DeptName], "");
                item.UserID = DataTypeHelper.GetGuid(dr[AccountAuditorFormDataColumns.UserID], Guid.Empty);
                item.UserName = DataTypeHelper.GetString(dr[AccountAuditorFormDataColumns.UserName], "");
                item.CurrencyID = DataTypeHelper.GetGuid(dr[AccountAuditorFormDataColumns.CurrencyID], Guid.Empty);

                if (ds.Tables[1].Columns.Contains("OrgAmt"))
                {
                    item.OrgAmt = DataTypeHelper.GetDecimal(dr[AccountAuditorFormDataColumns.OrgAmt], 0);
                }

                dataList.Add(item);
                part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                index++;
            }
            part.IsShowReceiptQty = true;
            master.LedgerList = dataList;
            part.BindDataSource(master);
        }
        /// <summary>
        /// 设置出纳支付界面数据(取财务审核表单中的数据)
        /// </summary>
        private void SetCashierAuditorFormDataByAccount(WFLedgerMaster master, UCLedgerPart part, List<WorkItemInfo> workItemList, List<WFLedgerList> dataList, Guid companyID)
        {
            //找出财务审核表单中的内容
            WorkItemInfo applyForm = (from d in workItemList where d.FormID == Utility.NewAccountAuditorFormID && d.WorkFlowID == WorkInfoID select d).SingleOrDefault();

            master.Opinion = "0";
            master.Remark = "";
            part.IsGetServerData = true;
            part.OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();
            int index = 0;
            decimal totalDRAmount = 0, totalCRAmount = 0;

            foreach (DataRow dr in applyForm.FormData.Tables[1].Rows)
            {
                WFLedgerList item = new WFLedgerList();
                item.GLID = DataTypeHelper.GetGuid(dr[AccountAuditorFormDataColumns.GLID], Guid.Empty);
                item.Remark = DataTypeHelper.GetString(dr[AccountAuditorFormDataColumns.DetailRemark], "");
                item.DRAmt = DataTypeHelper.GetDecimal(dr[AccountAuditorFormDataColumns.DRAmt], 0);
                item.CRAmt = DataTypeHelper.GetDecimal(dr[AccountAuditorFormDataColumns.CRAmt], 0);
                item.CurrencyID = DataTypeHelper.GetGuid(dr[AccountAuditorFormDataColumns.CurrencyID], Guid.Empty);
                item.CustomerID = DataTypeHelper.GetGuid(dr[AccountAuditorFormDataColumns.CustomerID], Guid.Empty);
                item.DeptID = DataTypeHelper.GetGuid(dr[AccountAuditorFormDataColumns.DeptID], Guid.Empty);
                item.UserID = DataTypeHelper.GetGuid(dr[AccountAuditorFormDataColumns.UserID], Guid.Empty);
                if (applyForm.FormData.Tables[1].Columns.Contains("OrgAmt"))
                {
                    item.OrgAmt = DataTypeHelper.GetDecimal(dr[AccountAuditorFormDataColumns.OrgAmt], 0);
                }
                //总金额
                totalDRAmount += DataTypeHelper.GetDecimal(item.DRAmt, 0);
                totalCRAmount += DataTypeHelper.GetDecimal(item.CRAmt, 0);

                dataList.Add(item);
                part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                index++;
            }
            //插入一行默认的凭证信息
            //摘要=工作名，贷方=总金额，科目=现金
            if (totalDRAmount != totalCRAmount && totalDRAmount > 0)
            {
                WFLedgerList newItem = new WFLedgerList();
                if (companyID == new Guid("9A4725F3-E7CF-4AD3-91A4-988892AF2781"))
                {
                    //长沙公司
                    newItem.GLID = Utility.RMBCSCashGLID;
                }
                else
                {
                    newItem.GLID = Utility.RMBCashGLID;
                }
                newItem.CRAmt = (totalDRAmount > totalCRAmount) ? (totalDRAmount - totalCRAmount) : 0.0m;
                newItem.DRAmt = (totalCRAmount > totalDRAmount) ? (totalCRAmount - totalDRAmount) : 0.0m;
                newItem.Remark = WorkName;
                dataList.Add(newItem);
                part.OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, null));
            }
        }

        #endregion

        #region 按钮事件
        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barFinish_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Finish();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LWBaseForm baseForm = CurrentForm as LWBaseForm;
            if (baseForm == null)
            {
                return;
            }
            Save(false);
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Print();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        #endregion

        #region 按钮方法
        #region 完成
        /// <summary>
        /// 完成
        /// </summary>
        private void Finish()
        {
            try
            {
                if (WFFormType == WorkFlowFormType.Add)
                {
                    #region 新增时点击完成
                    LWBaseForm baseForm = CurrentForm as LWBaseForm;
                    if (baseForm == null)
                    {
                        return;
                    }

                    if (!Save(true))
                    {
                        return;
                    }

                    DataSet ds = baseForm.DataSource;

                    EnqueueItem enqueueItem = new EnqueueItem(workflowData.Id, true, null);

                    workflowData.CurrentWorkItemId = workflowData.Id;

                    if (workFlowService.FinishTask(workflowData.Id, LocalData.UserInfo.LoginID, ds, enqueueItem))
                    {
                        string results = workFlowService.GetWorkInfo(LocalData.UserInfo.LoginID, WorkInfoID);
                        WorkFlowData data = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkFlowData>(results);
                        if (data.Id != new Guid() && FinishData != null)
                        {
                            FinishData(data);
                        }
                        Close();
                    }

                    #endregion
                }
                else
                {
                    #region 编辑时点击完成
                    LWBaseForm baseForm = null;

                    DataSet ds = null;
                    //找到当前环节的


                    if (CurrentForm == null)
                    {
                        return;
                    }
                    baseForm = CurrentForm as LWBaseForm;
                    if (baseForm == null)
                    {
                        return;
                    }

                    ds = baseForm.DataSource;

                    if (!Save(true))
                    {
                        return;
                    }

                    //获取一个值判断是同意
                    bool isAgree = false;
                    try
                    {
                        //审批表单是固定的，所以该表，该列是确定的
                        isAgree = ds.Tables["MainTable"].Rows[0]["Opinion"].ToString().Equals("0");
                    }
                    catch
                    {
                        isAgree = true;
                    }

                    EnqueueItem enqueueItem = new EnqueueItem(workflowData.Id, isAgree, null);
                    if (workFlowService.FinishTask(new Guid(workflowData.CurrentWorkItemId.ToString()), LocalData.UserInfo.LoginID, ds, enqueueItem))
                    {
                        string results = workFlowService.GetWorkInfo(LocalData.UserInfo.LoginID, WorkInfoID);
                        WorkFlowData data = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkFlowData>(results);
                        if (data.Id != new Guid() && FinishData != null)
                        {
                            FinishData(data);
                        }
                        Close();
                    }
                    #endregion
                }
            }
            catch (FaultException<WorkflowExecutorNullExceptionInfo> ex)
            {
                if (ex != null &&
                    ex.Detail != null)
                {
                    this.Invoke(new ChooseParticipants(this.chooseParticipants), new object[] { ex.Detail.WName });
                }
            }
            //catch (FaultException<ExceptionDetail> eex)
            //{

            //    if (wene != null && wene.CallerId == LocalData.UserInfo.LoginID && wene.WorkItemId == workflowData.Id)
            //    {
            //        this.Invoke(new ChooseParticipants(this.chooseParticipants), new object[] { wene.Name });
            //    }
            //}
            catch (Exception ex)
            {
                ErrorInfoData errorInfo = new ErrorInfoData(null, ex);
                DevExpress.XtraEditors.XtraMessageBox.Show(errorInfo.Message);
            }


        }


        #region 指派

        private SearchConditionCollection GetCollection(string name)
        {
            string titel = "选择[" + name + "]执行人";
            if (LocalData.IsEnglish)
            {
                titel = "Select[" + name + "]executor";
            }
            SearchConditionCollection list = new SearchConditionCollection();
            list.AddWithValue("FormTitle", titel, true);

            return list;
        }

        delegate void ChooseParticipants(string name);
        void chooseParticipants(string name)
        {

            IDataFinder finder = DataFinderFactory.GetDataFinder(SystemFinderConstants.UserFinder);
            finder.DataChoosed += new EventHandler<DataFindEventArgs>(finder_DataChoosed);
            finder.PickMany(
            SystemFinderConstants.UserFinder,
            @"Code/Name",
            GetCollection(name),
            ResultValue,
            FinderTriggerType.ClickButton,
            null,
            ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
        }

        void finder_DataChoosed(object sender, DataFindEventArgs e)
        {
            Guid[] userids = new Guid[e.Data.Length];
            for (int i = 0; i < e.Data.Length; i++)
            {
                object[] itemValues = (object[])e.Data[i];
                userids[i] = (Guid)itemValues[0];
            }
            EnqueueItem enqueueItem = new EnqueueItem(workflowData.Id, true, userids);
            if (workFlowService.FinishTask(new Guid(workflowData.CurrentWorkItemId.ToString()), LocalData.UserInfo.LoginID, null, enqueueItem))
            {
                string results = workFlowService.GetWorkInfo(LocalData.UserInfo.LoginID, WorkInfoID);
                WorkFlowData data = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkFlowData>(results);

                if (data.Id != new Guid() && FinishData != null)
                {
                    FinishData(data);
                }
                Close();
            }

        }
        #endregion

        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="isFinish">是否由完成动作保存</param>
        /// <returns></returns>
        private bool Save(bool isFinish)
        {

            LWBaseForm baseForm = CurrentForm as LWBaseForm;
            if (baseForm == null)
            {
                return false;
            }

            bool isValid = true;  //是否需要验证
            if (baseForm.IsShowLedgerPart && CurrentLedgerPart != null)
            {
                //将凭证明细表中的数据源转换为DataSet
                baseForm.DataSource = CurrentLedgerPart.GetData(baseForm.DataSource, WorkFlowConfigID, FormConfigID);
                if (baseForm.DataSource.Tables["GLList"] != null)
                {
                    decimal dramt = 0m;
                    decimal cramt = 0m;
                    for (int i = 0; i < baseForm.DataSource.Tables["GLList"].Rows.Count; i++)
                    {
                        dramt += Convert.ToDecimal(baseForm.DataSource.Tables["GLList"].Rows[i]["DRAmt"]);
                        cramt += Convert.ToDecimal(baseForm.DataSource.Tables["GLList"].Rows[i]["CRAmt"]);
                    }
                    if (dramt != cramt)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Unbalanced borrowing!" : "借贷不平衡!");
                        return false;
                    }
                }
            }

            if (baseForm.DataSource != null)
            {
                foreach (DataColumn col in baseForm.DataSource.Tables[0].Columns)
                {
                    if (col.ColumnName == "Opinion")
                    {
                        isValid = false;
                        break;
                    }

                    if (WorkFlowConfigID == new Guid("8118C71D-E0B1-4D97-B0E4-42A1B4129514") && col.ColumnName == "EName")//入职申请流程
                    {
                        bool result = UserService.IsExistUser(baseForm.DataSource.Tables[0].Rows[0]["EName"].ToString().Trim(),
                            baseForm.DataSource.Tables[0].Rows[0]["IdentityCard"].ToString().Trim());
                        if (result)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ?
                                    "The user name or IdentityCard already exists, please modify it and try again." : "用户名或身份证已经存在，请修改后重试？");
                            return false;
                        }
                    }
                }
            }
            if (isValid == false && baseForm.DataSource.Tables[0].Rows[0]["Opinion"].ToString().Equals("0"))  //点“同意”时才需要验证
                isValid = true;
            if (isValid)
            {
                if (!baseForm.ValidateForRuntime())
                {
                    return false;
                }
                if (baseForm.IsShowLedgerPart && CurrentLedgerPart != null)
                {
                    if (!CurrentLedgerPart.ValidateData(FormConfigID))
                    {
                        return false;
                    }
                }
            }
            //判断归属地部门和申请部门是否一致
            //跳过判断流程
            if (WorkFlowConfigID != new Guid("8118C71D-E0B1-4D97-B0E4-42A1B4129514") &&//入职申请流程
                WorkFlowConfigID != new Guid("9F81BA32-9A98-4E62-86E8-C46E2EFF2D75")) //非业务费用报销申请流程(黄晖)
            {
                if (baseForm.DataSource.Tables[0].Columns.Contains("DepartmentDepartmentID") && UCWorkNewList != null && baseForm.DataSource.Tables[0].Rows[0]["DepartmentDepartmentID"] != null)
                {
                    if (UCWorkNewList.OrganizationID != new Guid(baseForm.DataSource.Tables[0].Rows[0]["DepartmentDepartmentID"].ToString()))
                    {

                        DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ?
                            "The location  department and the application department are not in agreement. Do you want to continue the application process?" : "归属地部门和申请部门不一致，确定要继续申请流程？",
                            "tip", MessageBoxButtons.OKCancel);
                        if (result == DialogResult.Cancel)
                            return false;
                    }
                }
            }
            if (WFFormType == WorkFlowFormType.Add)
            {
                #region 新增时保存数据
                if (!UCWorkNewList.ValidateData())
                {
                    return false;
                }
                UCWorkNewList.GetData();

                OrganizationInfo regionInfo = OrganizationService.GetOrganizationInfo(UCWorkNewList.OrganizationID);
                if (regionInfo != null && (regionInfo.Type <= OrganizationType.Section || regionInfo.Type == OrganizationType.Group))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Section and above departments cannot apply for forms." : "区域及以上的部门不能申请表单。");
                    return false;
                }
                if (workItemInfo != null)
                {
                    try
                    {
                        ///保存主表数据
                        SingleResult result = workFlowService.SaveWorkflowInfo(workItemInfo.ID, workItemInfo.WorkflowInstanceID, UCWorkNewList.WorkItemName, true, workItemInfo.OwnerUserID, UCWorkNewList.OrganizationID, workItemInfo.StartTime, workItemInfo.FinishTime, workItemInfo.WorkState, workItemInfo.WorkflowConfigID, LocalData.UserInfo.LoginID);
                        WorkInfoID = result.GetValue<Guid>("ID");
                        UCWorkNewList.WorkItemNo = result.GetValue<string>("No");

                        ///保存表单数据
                        if (workItemInfo.WorkFlowDataList != null && workItemInfo.WorkFlowDataList.Count > 0)
                        {
                            DataSet ds = baseForm.DataSource;
                            workFlowService.SetWorkItemData(workItemInfo.WorkFlowDataList[0].Id, LocalData.UserInfo.LoginID, ds);

                        }
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully! " : "保存成功!");
                        //刷新任务列表记录
                        if (isFinish == false)
                        {
                            string results = workFlowService.GetWorkInfo(LocalData.UserInfo.LoginID, WorkInfoID);
                            WorkFlowData data = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkFlowData>(results);
                            if (data.Id != new Guid() && FinishData != null)
                            {
                                FinishData(data);
                            }
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
                        return false;
                    }
                }
                #endregion
            }
            else
            {
                #region 编辑时保存
                if (CurrentID == null)
                {
                    return false;
                }
                try
                {
                    DataSet ds = baseForm.DataSource;

                    workFlowService.SetWorkItemData(new Guid(CurrentID.ToString()), LocalData.UserInfo.LoginID, ds);

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully! " : "保存成功!");
                    //刷新任务列表记录
                    if (isFinish == false)
                    {
                        string results = workFlowService.GetWorkInfo(LocalData.UserInfo.LoginID, WorkInfoID);
                        WorkFlowData data = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkFlowData>(results);
                        if (data.Id != new Guid() && FinishData != null)
                        {
                            FinishData(data);
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
                    return false;
                }
                #endregion
            }

            return true;
        }

        #endregion

        #region 打印&关闭
        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            if (CurrentID == null)
            {
                return;
            }
            string results = workFlowService.GetWorkInfo(LocalData.UserInfo.LoginID, WorkInfoID);
            WorkFlowData data = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkFlowData>(results);

            string orgNo = string.Empty;
            workItemInfo = workFlowService.GetMainItem(WorkInfoID);
            try
            {
                orgNo = workItemInfo.FormData.Tables[0].Rows[0]["NO"].ToString();
            }
            catch
            {

            }

            if (orgNo != data.No)
            {
                workItemInfo.FormData.Tables[0].Rows[0]["NO"] = orgNo + ";" + data.No;
            }

            if (workItemInfo != null)
            {
                LWBaseForm cl = GetForm(workItemInfo.WorkflowInstanceID, workItemInfo.FormSchema, workItemInfo.FormData);
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
                    webPrint.WorkName = workItemInfo.WorkFlowPrintTitle;

                    Utility.SetReadOnly(cl, true);
                    cl.Enabled = false;
                    webPrint.ShowPrint(cl, workItemInfo.WorkFlowPrintTitle, data.Name);
                    PartLoader.ShowDialog(webPrint, "Print WorkFlow");
                }
            }


        }
        /// <summary>
        /// 关闭
        /// </summary>
        private void Close()
        {
            this.FindForm().Close();
        }
        #endregion

        #endregion

        #region 外部事件

        /// <summary>
        /// 完成事件
        /// </summary>
        public event ICP.Framework.ClientComponents.UIFramework.SavedHandler FinishData;

        #endregion
    }

}
