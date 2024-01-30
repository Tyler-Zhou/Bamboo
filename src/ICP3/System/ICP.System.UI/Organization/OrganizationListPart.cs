using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Common;
namespace ICP.Sys.UI.Organization
{
    [ToolboxItem(false)]
    public partial class OrganizationListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }

        #endregion

        #region Property

        OrganizationList CurrentRow
        {
            get { return bsList.Current as OrganizationList; }
            set
            {
                OrganizationList current = CurrentRow;
                if (current != null) current = value;
            }
        }

        #endregion

        #region init

        public OrganizationListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanging = null;
                this.CurrentChanged =null;
                this.treeMain.DataSource = null;
                this.treeMain.AfterDragNode -= this.treeMain_AfterDragNode;
                this.treeMain.BeforeDragNode -= this.treeMain_BeforeDragNode;
                this.treeMain.BeforeFocusNode -= this.treeMain_BeforeFocusNode;
                this.treeMain.CustomDrawNodeIndicator -= this.treeMain_CustomDrawNodeIndicator;
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
            colCode.Caption = "代码";
            colCName.Caption = "中文名";
            colEName.Caption = "英文名";
            colCreateBy.Caption = "创建人";
            colCreateDate.Caption = "创建日期";
        }

        #endregion

        #region Common

        [CommandHandler(OrganizationCommonConstants.Common_DisuseData)]
        public void Common_DisuseData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

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

                ManyResultData result = OrganizationService.ChangeOrganizationState(CurrentRow.ID, !CurrentRow.IsValid, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                List<OrganizationList> list = bsList.DataSource as List<OrganizationList>;

                List<OrganizationList> needUpdate = null;
                if (isValid == false)
                    needUpdate = list.FindAll(delegate(OrganizationList s) { return s.HierarchyCode.StartsWith(CurrentRow.HierarchyCode); });
                else
                    needUpdate = list.FindAll(delegate(OrganizationList s) { return CurrentRow.HierarchyCode.Contains(s.HierarchyCode); });

                if (needUpdate != null && needUpdate.Count > 0)
                {
                    foreach (OrganizationList j in needUpdate)
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

        [CommandHandler(OrganizationCommonConstants.Common_AddData)]
        public void Common_AddData(object sender, EventArgs e)
        {
            if (CurrentRow != null && CurrentRow.IsNew) return;

            OrganizationList newData = new OrganizationList();
            newData.ID = GlobalConstants.NewRowID;
            if (CurrentRow != null && CurrentRow.IsValid != false) newData.ParentID = CurrentRow.ID;
            newData.CreateBy = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.IsValid = true;
            newData.Type = OrganizationType.Company;
            newData.BeginEdit();

            if (bsList.List == null || bsList.List.Count == 0)
                treeMain.BeforeFocusNode -= new BeforeFocusNodeEventHandler(treeMain_BeforeFocusNode);

            bsList.Add(newData);
            treeMain.ExpandAll();
            for (int i = 0; i < treeMain.AllNodesCount; i++)
            {
                TreeListNode tn = treeMain.GetNodeByVisibleIndex(i);
                OrganizationList tager = treeMain.GetDataRecordByNode(tn) as OrganizationList;
                if (tager.ID == newData.ID)
                {
                    treeMain.FocusedNode = tn;
                }
            }

            treeMain.BeforeFocusNode -= new BeforeFocusNodeEventHandler(treeMain_BeforeFocusNode);
            treeMain.BeforeFocusNode += new BeforeFocusNodeEventHandler(treeMain_BeforeFocusNode);
        }

        #endregion

        #region tree Event

        Guid? beforeDragParentID = Guid.Empty;

        private void treeMain_BeforeDragNode(object sender, BeforeDragNodeEventArgs e)
        {
            beforeDragParentID = Guid.Empty;

            OrganizationList data = treeMain.GetDataRecordByNode(e.Node) as OrganizationList;
            if (data == null || data.IsNew||data.IsValid ==false)
            {
                e.CanDrag = false;
                return;
            }
            beforeDragParentID = data.ParentID;
            if (treeMain.Selection != null && treeMain.Selection.Count > 0)
            {
                List<OrganizationList> organizationLists = new List<OrganizationList>();
                foreach (TreeListNode tn in treeMain.Selection)
                {
                    OrganizationList organization = treeMain.GetDataRecordByNode(tn) as OrganizationList;
                    if (organization != null) organizationLists.Add(organization);
                }
                e.Node.Tag = organizationLists;
            }

        }

        private void treeMain_AfterDragNode(object sender, NodeEventArgs e)
        {
            DragNode(e.Node);
        }

        private void DragNode(TreeListNode tn)
        {
            if (tn == null) return;
            OrganizationList nodeData = treeMain.GetDataRecordByNode(tn) as OrganizationList;
            if (nodeData == null)
            {
                beforeDragParentID = Guid.Empty;
                return;
            }

            //同级拖放无效
            if (beforeDragParentID != Guid.Empty && nodeData.ParentID == beforeDragParentID) return;

            if (Utility.EnquireIsSaveCurrentData() == false)
            {
                nodeData.ParentID = beforeDragParentID;
                beforeDragParentID = Guid.Empty;
                return;
            }

            try
            {
                ManyHierarchyResultData result = OrganizationService.SetParentOrganization(nodeData.ID, nodeData.ParentID, LocalData.UserInfo.LoginID, nodeData.UpdateDate);
                UpdateChangedOrganizationList(bsList.DataSource, result);
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

        void UpdateChangedOrganizationList(object datasource, ManyHierarchyResultData result)
        {
            if (datasource == null) return;

            List<OrganizationList> returnlist = new List<OrganizationList>();
            List<OrganizationList> sourcelist = datasource as List<OrganizationList>;
            foreach (SingleHierarchyResultData sr in result.ChildResults)
            {
                OrganizationList value = sourcelist.Find(delegate(OrganizationList v) { return v.ID == sr.ID; });
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
            if (CurrentChanged != null) CurrentChanged(this, Current);
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

            OrganizationList nodeData = treeMain.GetDataRecordByNode(e.Node) as OrganizationList;
            if (nodeData == null) return;

            if (nodeData.IsValid == false)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
        }

        /// <summary>
        /// 绘制行号
        /// </summary>
        private void treeMain_CustomDrawNodeIndicator(object sender, DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventArgs e)
        {
            if (e.IsNodeIndicator == false || e.ObjectArgs == null) return;

            DevExpress.Utils.Drawing.IndicatorObjectInfoArgs args = e.ObjectArgs as DevExpress.Utils.Drawing.IndicatorObjectInfoArgs;
            if (args != null)
            {
                int rowNum = treeMain.GetVisibleIndexByNode(e.Node) + 1;
                args.DisplayText = rowNum.ToString();
            }
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
                bsList.DataSource = value;
                bsList.ResetBindings(false);
                treeMain.ExpandAll();
                if (CurrentChanged != null) CurrentChanged(this, Current);
            }
        }

        public override event CancelEventHandler CurrentChanging;
        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

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
