using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface.DataObjects;
using DevExpress.XtraTreeList;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraTreeList.Nodes;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.Job
{
    [ToolboxItem(false)]
    public partial class JobMainListPart:ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        public IJobService JobService
        {
            get
            {
                return ServiceClient.GetService<IJobService>();
            }
        }

        #endregion

        #region Property

        JobList CurrentRow
        {
            get { return bsList.Current as JobList; }
            set
            {
                JobList current = CurrentRow;
                if (current != null) current = value;
            }
        }

        #endregion

        #region init

        public JobMainListPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                Utility.RemoveSetControlKeyEnterToClickButton(new List<Control> { this.txtCode, this.txtName }, OnKeyDownHandle);
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this.treeMain.DataSource = null;
                this.treeMain.AfterDragNode -= this.treeMain_AfterDragNode;
                this.treeMain.BeforeDragNode -= this.treeMain_BeforeDragNode;
                this.treeMain.BeforeFocusNode -= this.treeMain_BeforeFocusNode;
                this.treeMain.NodeCellStyle -= this.treeMain_NodeCellStyle;
                this.bsList.PositionChanged -= this.bsMainList_PositionChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            barAdd.Caption = "新增(&A)";
            barDisuse.Caption = "作废(&D)";
            barClose.Caption = "关闭(&C)";
            barRefresh.Caption = "刷新(&R)";
            barSearch.Caption = "查询(&H)";

            labCode.Text = "代码";
            labIsValid.Text = "状态";
            labMax.Text = "最大行数";
            labName.Text = "名称";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";

            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&R)";

            colCode.Caption = "代码";
            colCName.Caption = "中文名";
            colEName.Caption = "英文名";
            colDescription.Caption = "描述";
            colCreateBy.Caption = "创建人";
            colCreateDate.Caption = "创建日期";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
            RefreshData();
            RefreshEnabled();
        }

        private void InitControls()
        {
            Utility.SetControlKeyEnterToClickButton(new List<Control> { this.txtCode, this.txtName }, this.btnSearch, OnKeyDownHandle);
        }
        private void OnKeyDownHandle(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                this.btnSearch.PerformClick();
            }

        }
        #endregion

        #region barItem

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        private void barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            List<JobList> data = JobService.GetJobList(string.Empty, string.Empty, true, 0);
            JobList tager = data.Find(delegate(JobList item) { return Utility.GuidIsNullOrEmpty(item.ParentID); });
            if (tager != null) data.Remove(tager);

            bsList.DataSource = data;
            bsMainList_PositionChanged(null, null);
        }

        private void barSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (splitContainerControl1.CollapsePanel == SplitCollapsePanel.Panel1)
                splitContainerControl1.CollapsePanel = SplitCollapsePanel.None;
            else
                splitContainerControl1.CollapsePanel = SplitCollapsePanel.Panel1;
        }

        private void barDisuse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CurrentRow == null)return;

            try
            {

                bool isValid = !CurrentRow.IsValid;
                if (isValid == false)
                {
                    if (Utility.EnquireIsDisuseCurrentData() == false) return;
                }
                else
                {
                    if (Utility.EnquireIsAvailableCurrentData() == false) return;
                }


                ManyResultData result = JobService.ChangeJobState(CurrentRow.ID, isValid, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                List<JobList> list = bsList.DataSource as List<JobList>;
                

                List<JobList> needUpdate = null;
                if (isValid == false)
                    needUpdate=list.FindAll(delegate(JobList s) { return s.HierarchyCode.StartsWith(CurrentRow.HierarchyCode); });
                else
                    needUpdate=list.FindAll(delegate(JobList s) { return CurrentRow.HierarchyCode.Contains(s.HierarchyCode); });

                if (needUpdate != null && needUpdate.Count > 0)
                {
                    foreach (JobList j in needUpdate)
                    {
                        foreach (SingleResultData sr in result.ChildResults)
                        {
                            if (j.ID == sr.ID)
                            {
                                j.UpdateDate = sr.UpdateDate;
                            }
                        }
                        j.IsValid = isValid;
                        j.BeginEdit();
                    }
                    bsList.ResetBindings(false);
                }
               
                bsList.ResetCurrentItem();
                bsMainList_PositionChanged(this, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CurrentRow != null && CurrentRow.IsNew) return;

            JobList newData = new JobList();
            newData.ID = GlobalConstants.NewRowID;
            if (CurrentRow != null && CurrentRow.IsValid != false) newData.ParentID = CurrentRow.ID;
            newData.CreateBy = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified); 
            newData.IsValid = true;
            newData.BeginEdit ();

            if(bsList.List ==null || bsList.List.Count ==0)
                treeMain.BeforeFocusNode-=new BeforeFocusNodeEventHandler(treeMain_BeforeFocusNode);

            bsList.Add(newData);
            treeMain.ExpandAll();
            for (int i = 0; i < treeMain.AllNodesCount; i++)
            {
                TreeListNode tn = treeMain.GetNodeByVisibleIndex(i);
                JobList tager = treeMain.GetDataRecordByNode(tn) as JobList;
                if (tager.ID == newData.ID)
                {
                    treeMain.FocusedNode = tn;
                }
            }

            treeMain.BeforeFocusNode -= new BeforeFocusNodeEventHandler(treeMain_BeforeFocusNode);
            treeMain.BeforeFocusNode += new BeforeFocusNodeEventHandler(treeMain_BeforeFocusNode);
        }

        #endregion

        #region btn

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCode.Text = txtName.Text = string.Empty;
            lwchkIsValid.Checked = true;
            numMax.Value = 100;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<JobList> data = JobService.GetJobList(txtCode.Text.Trim(),
                                                 txtName.Text.Trim(),
                                                 lwchkIsValid.Checked,
                                                int.Parse(numMax.Value.ToString()));
            JobList tager = data.Find(delegate(JobList item) { return Utility.GuidIsNullOrEmpty(item.ParentID); });
            if (tager != null) data.Remove(tager);
            this.DataSource = data;
        }

        #endregion

        #region tree Event

        Guid? beforeDragParentID = Guid.Empty;

        private void treeMain_BeforeDragNode(object sender, BeforeDragNodeEventArgs e)
        {
            beforeDragParentID = Guid.Empty;

            JobList data = treeMain.GetDataRecordByNode(e.Node) as JobList;
            if (data == null || data.IsNew || data.IsValid == false)
            {
                e.CanDrag = false;
                return;
            }
            beforeDragParentID = data.ParentID;
            if (treeMain.Selection != null && treeMain.Selection.Count > 0)
            {
                List<JobList> jobLists = new List<JobList>();
                foreach (TreeListNode tn in treeMain.Selection)
                {
                    JobList job = treeMain.GetDataRecordByNode(tn) as JobList;
                    if (job != null) jobLists.Add(job);
                }
                e.Node.Tag = jobLists;
            }
            
        }

        private void treeMain_AfterDragNode(object sender, NodeEventArgs e)
        {
            DragNode(e.Node);
        }

        private void DragNode(TreeListNode tn)
        {
            if (tn == null) return;
            JobList nodeData = treeMain.GetDataRecordByNode(tn) as JobList;
            if (nodeData == null)
            {
                beforeDragParentID = Guid.Empty;
                return;
            }

            //同级拖放无效
            if (beforeDragParentID!=Guid.Empty && nodeData.ParentID == beforeDragParentID) return;

            if (Utility.EnquireIsSaveCurrentData() == false)
            {
                nodeData.ParentID = beforeDragParentID;
                beforeDragParentID = Guid.Empty;
                return;
            }

            try
            {
                ManyHierarchyResultData result = JobService.SetParentJob(nodeData.ID, nodeData.ParentID, LocalData.UserInfo.LoginID, nodeData.UpdateDate);
                UpdateChangedJobList(bsList.DataSource, result);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置成功");
            }
            catch (Exception ex) 
            {
                nodeData.ParentID = beforeDragParentID;
                treeMain.RefreshDataSource();
                beforeDragParentID = Guid.Empty;
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); 
            }
        }

        void UpdateChangedJobList(object datasource, ManyHierarchyResultData result)
        {
            if (datasource == null) return;

            List<JobList> returnlist = new List<JobList>();
            List<JobList> sourcelist = datasource as List<JobList>;
            foreach (SingleHierarchyResultData sr in result.ChildResults)
            {
                JobList value = sourcelist.Find(delegate(JobList v) { return v.ID == sr.ID; });
                if (value != null)
                {
                    value.UpdateDate = sr.UpdateDate;
                    value.HierarchyCode = sr.HierarchyCode;

                    returnlist.Add(value);
                }
            }

            bsList.ResetBindings(false);
        }


        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if(CurrentChanged!=null)CurrentChanged(this, Current);
            RefreshEnabled();
        }

        private void RefreshEnabled()
        {
            if (CurrentRow == null || CurrentRow.IsNew)
            {
                barDisuse.Enabled = false;
            }
            else
            {
                barDisuse.Enabled = true;
                if (CurrentRow.IsValid)
                    barDisuse.Caption = LocalData.IsEnglish ? "Disuse(&D)" : "作废(&D)";
                else
                    barDisuse.Caption = LocalData.IsEnglish ? "Available(&D)" : "激活(&D)";
            }
        }

        private void treeMain_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.CanFocus = !ce.Cancel;
            }
        }

        private void treeMain_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == null) return;

            JobList nodeData = treeMain.GetDataRecordByNode(e.Node) as JobList;
            if (nodeData == null) return;

            if (nodeData.IsValid == false)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
        }

        #endregion

        #region IListPart 成员


        public override object Current
        {
            get { return bsList.Current; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                List<JobList> data = value as List<JobList>;
                if (data != null && data.Count > 0)
                {
                    foreach (var item in data) { item.BeginEdit(); }
                }
                bsList.DataSource = data;
                bsList.ResetBindings(false);
                treeMain.ExpandAll();
                if (CurrentChanged != null) CurrentChanged(this, Current);
                RefreshEnabled();
            }
        }

        public override event CancelEventHandler CurrentChanging;
        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        public override void Refresh(object items)
        {
            RefreshEnabled();
        }

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }

        #endregion
    }
}
