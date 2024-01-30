

//-----------------------------------------------------------------------
// <copyright file="ShellToolboxPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.UI
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI;
    using ICP.WF.ServiceInterface;
    using ICP.WF.ServiceInterface.DataObject;
    using ICP.Framework.CommonLibrary.Client;
    using System.Linq;
    using ICP.Framework.CommonLibrary;

    /// <summary>
    /// 列表
    /// </summary>
    public class WorkListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region 资源初始化与释放

        public WorkListPart()
        {
            InitializeComponent();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.ComponentModel.IContainer components;
        private ICP.Framework.ClientComponents.Controls.LWGridControl lwGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkName;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colStartTime;
        private DevExpress.XtraGrid.Columns.GridColumn colEndTime;
        private DevExpress.XtraGrid.Columns.GridColumn colOwnerUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrentWorkItemName;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkItemStateDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherNo;

        #region Component Designer generated code

        private BindingSource WorkList;
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkListPart));
            this.WorkList = new System.Windows.Forms.BindingSource(this.components);
            this.lwGridControl1 = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOwnerUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrentWorkItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkItemStateDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.WorkList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // WorkList
            // 
            this.WorkList.DataSource = typeof(ICP.WF.ServiceInterface.DataObject.WorkFlowData);
            this.WorkList.PositionChanged += new System.EventHandler(this.WorkList_PositionChanged);
            // 
            // lwGridControl1
            // 
            this.lwGridControl1.DataSource = this.WorkList;
            resources.ApplyResources(this.lwGridControl1, "lwGridControl1");
            this.lwGridControl1.MainView = this.gridView1;
            this.lwGridControl1.Name = "lwGridControl1";
            this.lwGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNo,
            this.colVoucherNo,
            this.colName,
            this.colWorkName,
            this.colStartTime,
            this.colEndTime,
            this.colDepartmentName,
            this.colOwnerUserName,
            this.colState,
            this.colCurrentWorkItemName,
            this.colWorkItemStateDescription});
            this.gridView1.GridControl = this.lwGridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("gridView1.GroupSummary"))), resources.GetString("gridView1.GroupSummary1"), ((DevExpress.XtraGrid.Columns.GridColumn)(resources.GetObject("gridView1.GroupSummary2"))), resources.GetString("gridView1.GroupSummary3"))});
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView1.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colWorkItemStateDescription, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // colNo
            // 
            resources.ApplyResources(this.colNo, "colNo");
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            // 
            // colVoucherNo
            // 
            resources.ApplyResources(this.colVoucherNo, "colVoucherNo");
            this.colVoucherNo.FieldName = "VoucherNo";
            this.colVoucherNo.Name = "colVoucherNo";
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            // 
            // colWorkName
            // 
            resources.ApplyResources(this.colWorkName, "colWorkName");
            this.colWorkName.FieldName = "WorkName";
            this.colWorkName.Name = "colWorkName";
            // 
            // colStartTime
            // 
            resources.ApplyResources(this.colStartTime, "colStartTime");
            this.colStartTime.FieldName = "StartTime";
            this.colStartTime.Name = "colStartTime";
            // 
            // colEndTime
            // 
            resources.ApplyResources(this.colEndTime, "colEndTime");
            this.colEndTime.FieldName = "EndTime";
            this.colEndTime.Name = "colEndTime";
            // 
            // colDepartmentName
            // 
            resources.ApplyResources(this.colDepartmentName, "colDepartmentName");
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            // 
            // colOwnerUserName
            // 
            resources.ApplyResources(this.colOwnerUserName, "colOwnerUserName");
            this.colOwnerUserName.FieldName = "OwnerUserName";
            this.colOwnerUserName.Name = "colOwnerUserName";
            // 
            // colState
            // 
            this.colState.FieldName = "State";
            this.colState.Name = "colState";
            // 
            // colCurrentWorkItemName
            // 
            resources.ApplyResources(this.colCurrentWorkItemName, "colCurrentWorkItemName");
            this.colCurrentWorkItemName.FieldName = "CurrentWorkItemName";
            this.colCurrentWorkItemName.Name = "colCurrentWorkItemName";
            // 
            // colWorkItemStateDescription
            // 
            resources.ApplyResources(this.colWorkItemStateDescription, "colWorkItemStateDescription");
            this.colWorkItemStateDescription.FieldName = "WorkItemStateDescription";
            this.colWorkItemStateDescription.Name = "colWorkItemStateDescription";
            this.colWorkItemStateDescription.OptionsColumn.ReadOnly = true;
            // 
            // WorkListPart
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lwGridControl1);
            this.Name = "WorkListPart";
            ((System.ComponentModel.ISupportInitialize)(this.WorkList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #endregion

        #region 属性
        /// <summary>
        /// 当前行
        /// </summary>
        public override object Current
        {
            get 
            {
                 return WorkList.Current;
            }
        }

        public WorkFlowData CurrentRow
        {
            get
            {
                 return WorkList.Current as WorkFlowData;
            }
        }
        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return WorkList.DataSource;
            }
            set
            {
                List<WorkFlowData> list = value as List<WorkFlowData>;
                if (list == null)
                {
                    list = new List<WorkFlowData>();
                }

                WorkList.DataSource = list;
                WorkList.ResetBindings(false);

                if (list == null || list.Count == 0)
                {
                    gridView1.IndicatorWidth = 30;
                }
                else
                {
                    int count = list.Count.ToString().Length - 1;
                    int width = 10 + count * 10;
                    gridView1.IndicatorWidth = 30 + width;
                   // gridView1.BestFitColumns();
                }

                string message = string.Empty;
                if (LocalData.IsEnglish)
                {
                    if (list.Count > 1)
                    {
                        message = string.Format("{0} records found", list.Count);
                    }
                    else
                    {
                        message = string.Format("{0} record found", list.Count);
                    }
                }
                else
                {
                    message = string.Format("查询到 {0} 条记录", list.Count);
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }
            }
        }
        /// <summary>
        /// 当前选择的数据
        /// </summary>
        public List<WorkFlowData> SelectDataList
        {
            get
            {
                List<WorkFlowData> list = new List<WorkFlowData>();

                int[] rowIndexs = this.gridView1.GetSelectedRows();

                if (rowIndexs.Length == 0) return list;

                foreach (var item in rowIndexs)
                {
                    WorkFlowData dr = gridView1.GetRow(item) as WorkFlowData;
                    if (dr != null)
                    {
                        list.Add(dr);
                    }
                }
                return list;
            }
        }
        public List<Guid> SelectIDList
        {
            get
            {
                List<Guid> list = new List<Guid>();

                int[] rowIndexs = this.gridView1.GetSelectedRows();

                if (rowIndexs.Length == 0) return list;

                foreach (var item in rowIndexs)
                {
                    WorkFlowData dr = gridView1.GetRow(item) as WorkFlowData;
                    if (dr != null)
                    {
                        list.Add(dr.Id);
                    }
                }
                return list;
            }
        }
        public string ViewCode
        {
            get;
            set;
        }
        public string StrQuery
        {
            get;
            set;
        }
        #endregion

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IWorkflowService WorkflowService
        {
            get 
            {
                return ServiceClient.GetService<IWorkflowService>();
            }
        }
        #endregion

        #region 重写方法
        public override void AddItem(object item)
        {
            WorkFlowData data = item as WorkFlowData;
            if (data != null)
            {
                WorkList.Insert(0,data);
            }
        }

        public void RefreshList(object items, bool isRefreshChart)
        {
            WorkFlowData data = items as WorkFlowData;
            if (data != null)
            {
                List<WorkFlowData> list = WorkList.DataSource as List<WorkFlowData>;

                if (list == null)
                {
                    list = new List<WorkFlowData>();
                    WorkList.DataSource = list;
                    WorkList.ResetBindings(false);
                }

                WorkFlowData tager = list.Find(delegate(WorkFlowData item) { return item.Id == data.Id; });
                if (tager == null)
                {
                    WorkList.Insert(0, data);
                    WorkList.ResetBindings(false);
                }
                else
                {
                    //Utility.CopyToValue(data, tager, typeof(WorkFlowData));
                    tager.CName = data.CName;
                    tager.CurrentWorkItemExcutorID = data.CurrentWorkItemExcutorID;
                    tager.CurrentWorkItemExcutorName = data.CurrentWorkItemExcutorName;
                    tager.CurrentWorkItemId = data.CurrentWorkItemId;
                    tager.CurrentWorkItemName = data.CurrentWorkItemName;
                    tager.CurrentWorkItemState = data.CurrentWorkItemState;
                    tager.DataSchema = data.DataSchema;
                    tager.DepartmentID = data.DepartmentID;
                    tager.DepartmentName = data.DepartmentName;
                    tager.EName = data.EName;
                    tager.EndTime = data.EndTime;
                    tager.ExecutorID = data.ExecutorID;
                    tager.ExecutorName = data.ExecutorName;
                    tager.FormData = data.FormData;
                    tager.FormDataString = data.FormDataString;
                    tager.FormSchema = data.FormSchema;
                    tager.Id = data.Id;
                    tager.IsMain = data.IsMain;
                    tager.Name = data.Name;
                    tager.No = data.No;
                    tager.OwnerUserId = data.OwnerUserId;
                    tager.OwnerUserName = data.OwnerUserName;
                    tager.ParentID = data.ParentID;
                    tager.StartTime = data.StartTime;
                    tager.State = data.State;
                    tager.VoucherNo = data.VoucherNo;
                    tager.WorkFlowConfigID = data.WorkFlowConfigID;
                    tager.WorkItemState = data.WorkItemState;
                    tager.WorkName = data.WorkName;

                    WorkList.ResetItem(WorkList.IndexOf(tager));
                }

                if (isRefreshChart && CurrentChanged!=null)
                {
                    CurrentChanged(this, CurrentRow);
                }
            }
        }

        public void RefreshList(List<CurrentWorkItem> datas)
        {
            foreach (CurrentWorkItem item in datas)
            {
                #region 更新列表中的数据
                List<WorkFlowData> list = WorkList.DataSource as List<WorkFlowData>;

                if (list == null)
                {
                    return;
                }

                WorkFlowData tager = list.Find(delegate(WorkFlowData data) { return item.WorkInfoID == data.Id; });
                if (tager != null)
                {
                    tager.CurrentWorkItemName = item.CurrentWorkItemName;
                    tager.EndTime = item.FinishDate;
                    if (item.WorkflowState == WorkflowState.Finished)
                    {
                        tager.WorkItemState = WorkItemSearchStatus.Finished;
                    }
                    else if (item.WorkflowState == WorkflowState.Activated)
                    {
                        //流程的状态为活动的
                        if (item.UserList.Contains(LocalData.UserInfo.LoginID))
                        {
                            //下一步的执行的人中当前用户，状态为待办
                            tager.WorkItemState = WorkItemSearchStatus.Waiting;
                        }
                        else
                        {
                            //下一步的执行人列表中没有当前用户，状态为已办
                            tager.WorkItemState = WorkItemSearchStatus.OnceProcess;
                        }
                    }

                    WorkList.ResetItem(WorkList.IndexOf(tager));
                }
                #endregion
            }
            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }
        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="item"></param>
        public override void RemoveItem(object item)
        {
            WorkList.Remove(item);
        }
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }

            foreach (var item in values)
            {
                if (item.Key == "RefreshData")
                {
                    RefreshData();
                }
            }
        }
        #endregion

        #region 窗体事件
        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        public event EventHandler DockClickEditWork;
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                gridView1.IndicatorWidth = 30;
                InitControls();
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            //RefreshData();

            this.Workitem.Commands[CommandConstants.Command_WorkList_Edit].AddInvoker(lwGridControl1, "DoubleClick");
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshData()
        {
            List<WorkItemSearchStatus> statusList = new List<WorkItemSearchStatus>();
            WorkListSearchType searchType = WorkListSearchType.All;
            string no = string.Empty,workName = string.Empty,workFlow = string.Empty, applyBy=string.Empty;
            Guid? departmentID = null;
            DateTime? beginDate = null,endDate=null;
            int rowCount = 0;

            if (!string.IsNullOrEmpty(StrQuery))
            {
                #region 快速查询&高级查询
                if (!string.IsNullOrEmpty(StrQuery))
                {
                    var strReplace = StrQuery.Replace("and", "*").Replace("1=1", string.Empty);
                    var strSplit = strReplace.Split('*');
                    foreach (string str in strSplit)
                    {
                        if (string.IsNullOrEmpty(str))
                        {
                            continue;
                        }
                        if (str.Contains("$@Department@"))
                        {
                            departmentID = DataTypeHelper.GetGuid(StrReplace(str));
                        }
                        if (str.Contains("$@WorkFlow@"))
                        {
                            workFlow = StrReplace(str);
                        }
                        if (str.Contains("$@No@"))
                        {
                            no = StrReplace(str);
                        }
                        if (str.Contains("$@WorkName@"))
                        {
                            workName = StrReplace(str);
                        }
                        if (str.Contains("$@ApplyBy@"))
                        {
                            applyBy = StrReplace(str);
                        }
                        if (str.Contains("$@Type@"))
                        {
                            searchType = (WorkListSearchType)DataTypeHelper.GetInt(StrReplace(str));
                        }
                        if (str.Contains("$@StartDate@"))
                        {
                            beginDate = DataTypeHelper.GetDateTime(StrReplace(str));
                        }
                        if (str.Contains("$@EndDate@"))
                        {
                            endDate = DataTypeHelper.GetDateTime(StrReplace(str));
                        }

                        if (str.Contains("$@State@"))
                        {
                            string stateValue=StrReplace(str);
                            if (stateValue.Contains("0"))
                            {
                                statusList.Add(WorkItemSearchStatus.All);
                            }
                            if (stateValue.Contains("1"))
                            {
                                statusList.Add(WorkItemSearchStatus.Waiting);
                            }
                            if (stateValue.Contains("2"))
                            {
                                statusList.Add(WorkItemSearchStatus.Processing);
                            }
                            if (stateValue.Contains("3"))
                            {
                                statusList.Add(WorkItemSearchStatus.Finished);
                            }
                            if (stateValue.Contains("4"))
                            {
                                statusList.Add(WorkItemSearchStatus.Cancel);
                            }
                            if (stateValue.Contains("5"))
                            {
                                statusList.Add(WorkItemSearchStatus.Return);
                            }
                            if (stateValue.Contains("6"))
                            {
                                statusList.Add(WorkItemSearchStatus.OnceProcess);
                            }
                        }
                    }
                }
                if(statusList.Count==0)
                {
                    statusList.Add(WorkItemSearchStatus.All);
                    statusList.Add(WorkItemSearchStatus.Cancel);
                    statusList.Add(WorkItemSearchStatus.Finished);
                    statusList.Add(WorkItemSearchStatus.OnceProcess);
                    statusList.Add(WorkItemSearchStatus.Processing);
                    statusList.Add(WorkItemSearchStatus.Return);
                    statusList.Add(WorkItemSearchStatus.Waiting);

                    if (!DataTypeHelper.GetString(StrQuery,"").Replace(" ","").Contains("1=1"))
                    { 
                        beginDate = DateTime.Now.AddDays(-90);
                    }
                    rowCount = 500;
                }
                #endregion
            }
            else
            {
                statusList.Add(WorkItemSearchStatus.Waiting);
                statusList.Add(WorkItemSearchStatus.Processing);
            }

           string reslut = WorkflowService.GetWorkList(
                                LocalData.UserInfo.LoginID, 
                                departmentID,
                                workFlow,
                                workName,
                                no,
                                applyBy,
                                searchType,
                                statusList.ToArray(),
                                beginDate,
                                endDate,
                                null,
                                null,
                                rowCount);
           List<WorkFlowData> workFlowDataList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WorkFlowData>>(reslut);
           DataSource = workFlowDataList;

        }
        public string StrReplace(string replace)
        {
            if (replace.Contains("like"))
            {
                replace = replace.Replace("%", string.Empty).Replace("'", string.Empty)
                          .Replace("like", "?");

            }
            else if (replace.Contains("in"))
            {
                replace = replace.Replace("(", string.Empty).Replace("'", string.Empty)
                                 .Replace(")", string.Empty).Replace("in", "?");

            }
            else
            {
                replace = replace.Replace("'", string.Empty).Replace("=", "?");

            }
            string str = replace.Split('?')[1];
            if (str != null)
            {
                str = str.Trim();
            }
            return str;
        }
        /// <summary>
        /// 选择的纪录发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {
                this.lwGridControl1.Select();
                if (CurrentChanged!=null)
                { 
                    CurrentChanged(this, Current);
                }
            }
        
        }

        public void SetCurrecntChanged()
        {
            if (CurrentChanged != null)
            {
                this.lwGridControl1.Select();
                if (CurrentChanged!=null)
                { 
                    CurrentChanged(this, Current);
                }
            }
        }

        #endregion

      
        #region 复制
        //private void lwTreeGridControl1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    //if (e.Control && e.KeyCode == Keys.C && lwTreeGridControl1.FocusedNode.GetValue(lwTreeGridControl1.FocusedColumn.AbsoluteIndex) != null)
        //    //{
        //    //    Clipboard.SetDataObject(lwTreeGridControl1.FocusedNode.GetValue(lwTreeGridControl1.FocusedColumn.AbsoluteIndex).ToString(),true);
        //    //    XtraMessageBox.Show(lwTreeGridControl1.FocusedNode.GetValue(lwTreeGridControl1.FocusedColumn.AbsoluteIndex).ToString());
        //    //}
        //}
        //private void WorkListPart_KeyDown(object sender, KeyEventArgs e)
        //{
        //    //if (e.Control && e.KeyCode == Keys.C && lwTreeGridControl1.FocusedNode.GetValue(lwTreeGridControl1.FocusedColumn.AbsoluteIndex) != null)
        //    //{
        //    //    Clipboard.SetDataObject(lwTreeGridControl1.FocusedNode.GetValue(lwTreeGridControl1.FocusedColumn.AbsoluteIndex).ToString(), true);
        //    //    XtraMessageBox.Show(lwTreeGridControl1.FocusedNode.GetValue(lwTreeGridControl1.FocusedColumn.ColumnEditName).ToString());
        //    //}
        //}

        #endregion

        #region Grid 事件
        /// <summary>
        /// 根据不同的状态来设置行的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.CellValue == null)
                return;
            WorkFlowData data = gridView1.GetRow(e.RowHandle) as WorkFlowData;
            if (data != null && data.WorkItemState == WorkItemSearchStatus.Waiting)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Confirmed);
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator == false || e.Info == null)
                return;
            DevExpress.Utils.Drawing.IndicatorObjectInfoArgs args = e.Info as DevExpress.Utils.Drawing.IndicatorObjectInfoArgs;
            if (args != null)
            {
                int rowNum = gridView1.GetVisibleIndex(e.RowHandle) + 1;
                args.DisplayText = rowNum.ToString();
            }
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (DockClickEditWork != null
                && CurrentRow != null)
            {
                DockClickEditWork(sender,e);
            }
        }
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //using (new CursorHelper(Cursors.WaitCursor))
            //{
            //    if (e.Button == MouseButtons.Left && e.Clicks == 2)
            //    {

            //        if (e.RowHandle >= 0)
            //        {
            //            //注册双击事件
            //            this.Workitem.Commands[CommandConstants.Command_WorkList_Edit].AddInvoker(lwGridControl1, "DoubleClick");
            //        }
            //    }
            //}
        }
        #endregion

  

    }
   
}
