using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface.DataObjects;
using DevExpress.XtraTreeList;
using ICP.Sys.ServiceInterface;
using DevExpress.XtraTreeList.Nodes;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.PermissionManage.Permission
{
    [ToolboxItem(false)]
    public partial class PermissionMainListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IPermissionService PermissionService
        {
            get
            {
                return ServiceClient.GetService<IPermissionService>();
            }
        }

        #endregion

        #region Property

        protected virtual SiteType siteType { get; set; }

        UIItemList CurrentRow
        {
            get { return bsList.Current as UIItemList; }
        }

        #endregion

        #region init

        public PermissionMainListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.treeMain.DataSource = null;
                this.treeMain.AfterDragNode -= this.treeMain_AfterDragNode;
                this.treeMain.BeforeDragNode -= this.treeMain_BeforeDragNode;
                this.treeMain.DoubleClick -= this.treeMain_DoubleClick;
                this.treeMain.GetStateImage -= this.treeMain_GetStateImage;
                this.barAddFunction.ItemClick -= this.barAddFunction_ItemClick;
                this.barAddMenu.ItemClick -= this.barAddMenu_ItemClick;
                this.barClose.ItemClick -= this.barClose_ItemClick;
                this.barDelete.ItemClick -= this.barDelete_ItemClick;
                this.barRefresh.ItemClick -= this.barRefresh_ItemClick;
                this.barEdit.ItemClick -= this.barEdit_ItemClick;
                this.bsList.PositionChanged -= this.bsMainList_PositionChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.CurrentChanged = null;
                this.CurrentChanging = null;
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
            barAddMenu.Caption = "新增菜单组";
            barAddFunction.Caption = "新增功能项";
            barDelete.Caption = "删除(&D)";
            barClose.Caption = "关闭(&C)";
            barRefresh.Caption = "刷新(&R)";
            barEdit.Caption = "编辑(&E)";

            colCName.Caption = "名称";
            colEName.Visible = false;
            colCName.Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        
            RefreshData();
            RefreshEnabled();
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
            List<UIItemList> list = PermissionService.GetUIConfigurationList(siteType);
            bsList.DataSource = list;
            treeMain.ExpandAll();
        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteData();
        }
        private bool DeleteData()
        {
            if (CurrentRow == null) return false;

            if (CurrentRow.FunctionType == UIConfigItemType.Container) return false;

            List<UIItemList> list = bsList.DataSource as List<UIItemList>;
            
             List<UIItemList> needRemoves= list.FindAll(delegate(UIItemList item){return item.HierarchyCode.Contains(CurrentRow.HierarchyCode);});
            if(needRemoves!=null && needRemoves.Count >1)
            {
                if(Utility.EnquireIsDeleteCurrentDataByHasChild()==false) return false;
            }
            else
            {
                if(Utility.EnquireIsDeleteCurrentData()==false) return false;
            }

            try
            {
                PermissionService.RemoveUIConfigurationInfo(CurrentRow.ID,
                                                 LocalData.UserInfo.LoginID,
                                                CurrentRow.UpdateDate);
                if(needRemoves==null || needRemoves.Count <1)
                {
                    bsList.RemoveCurrent();
                    treeMain.Refresh();
                }
                else
                {
                    List<UIItemList> tagers = list.FindAll(delegate(UIItemList item) { return item.HierarchyCode.Contains(CurrentRow.HierarchyCode) == false; });
                    bsList.DataSource = tagers;
                    treeMain.Refresh();
                }

                bsMainList_PositionChanged(this, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                return false;
            }
        }

        private void barAddMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UIItemInfo newData = new UIItemInfo();
            newData.FunctionType = UIConfigItemType.MenuGroup;

            if (CurrentRow != null)
            {
                if (CurrentRow.FunctionType != UIConfigItemType.Action && CurrentRow.FunctionType != UIConfigItemType.MenuItem)
                {
                    newData.ParentID = CurrentRow.ID;
                    newData.ParentName = LocalData.IsEnglish ? CurrentRow.EName : CurrentRow.CName;
                }
                else
                {
                    newData.ParentID = CurrentRow.ParentID;
                    newData.ParentName = CurrentRow.ParentName;

                }
            }

            newData.ID = Guid.Empty;
            newData.ParentName = string.Empty;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified); newData.IsDirty = false;

            ShowEditPart(newData, LocalData.IsEnglish ? "Add Group" : "新增菜单组");
        }

        private void ShowEditPart(UIItemInfo newData,string titel)
        {
            PermissionEditPart editPart = this.Workitem.Items.AddNew<PermissionEditPart>();
            editPart.DataSource = newData;
            editPart.Saved += delegate(object[] prams)
            {
                UIItemList uiItem = prams[0] as UIItemList;
                UIItemList editData = (bsList.DataSource as List<UIItemList>).Find(delegate(UIItemList item) { return item.ID == uiItem.ID; });
                if (editData != null)
                {
                    Utility.CopyToValue(uiItem, editData, typeof(UIItemList));
                    treeMain.Refresh();
                }
                else
                {
                    bsList.Add(uiItem);
                    treeMain.Refresh();
                }
            };
            Utility.ShowPartInDialog(editPart, titel);
        }

        private void barAddFunction_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UIItemInfo newData = new UIItemInfo();
            if (CurrentRow != null)
            {
                if (CurrentRow.FunctionType != UIConfigItemType.Action && CurrentRow.FunctionType != UIConfigItemType.MenuItem)
                {
                    newData.ParentID = CurrentRow.ID;
                    newData.ParentName = LocalData.IsEnglish ? CurrentRow.EName : CurrentRow.CName;
                }
                else
                {
                    newData.ParentID = CurrentRow.ParentID;
                    newData.ParentName = CurrentRow.ParentName;
                   
                }
            }


            newData.FunctionType = UIConfigItemType.MenuItem;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified); newData.IsDirty = false;

            ShowEditPart(newData, LocalData.IsEnglish ? "Add Function" : "新增功能项");
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditData();
        }

        private void EditData()
        {
            if (CurrentRow == null) return;

            UIItemInfo editData = PermissionService.GetUIConfigurationInfo(CurrentRow.ID);

            ShowEditPart(editData, CurrentRow.FunctionType == UIConfigItemType.MenuGroup
                                              ? (LocalData.IsEnglish ? "Edit Group" : "编辑菜单组") : (LocalData.IsEnglish ? "Edit Function" : "编辑功能项"));
        }

        #endregion

        #region tree Event

        private void treeMain_DoubleClick(object sender, EventArgs e)
        {
            EditData();
        }

        private void treeMain_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            UIItemList data = treeMain.GetDataRecordByNode(e.Node) as UIItemList;
            if (data == null) return;
            e.Node.StateImageIndex = (short)data.FunctionType;
        }

        Guid? beforeDragParentID = Guid.Empty;

        private void treeMain_BeforeDragNode(object sender, BeforeDragNodeEventArgs e)
        {
            beforeDragParentID = Guid.Empty;

            UIItemList data = treeMain.GetDataRecordByNode(e.Node) as UIItemList;
            if (data == null)
            {
                e.CanDrag = false;
                return;
            }
            beforeDragParentID = data.ParentID;
        }

        private void treeMain_AfterDragNode(object sender, NodeEventArgs e)
        {
            DragNode(e.Node);
        }

        private void DragNode(TreeListNode tn)
        {
            if (tn == null) return;
            UIItemList nodeData = treeMain.GetDataRecordByNode(tn) as UIItemList;
            List<UIItemList> currentSource = this.DataSource as List<UIItemList>;
            if (nodeData == null || currentSource==null || currentSource.Count ==0)
            {
                beforeDragParentID = Guid.Empty;
                return;
            }

            UIItemList tager= currentSource.Find(delegate(UIItemList item) { return item.ID == nodeData.ParentID; });
            if (tager == null|| tager.FunctionType == UIConfigItemType.Action||tager.FunctionType == UIConfigItemType.MenuItem)
            {
                nodeData.ParentID = beforeDragParentID;
                beforeDragParentID = Guid.Empty;
                return;
            }


            if (Utility.EnquireIsSaveCurrentData() == false)
            {
                nodeData.ParentID = beforeDragParentID;
                beforeDragParentID = Guid.Empty;
                return;
            }

            try
            {
                ManyHierarchyResultData result = PermissionService.SetUIConfigurationPosition(nodeData.ID,
                                                                            nodeData.ParentID,
                                                                            Guid.Empty,
                                                                            LocalData.UserInfo.LoginID,
                                                                            nodeData.UpdateDate);

                List<UIItemList> listfinds = currentSource.FindAll(delegate(UIItemList s) { return s.HierarchyCode.StartsWith(nodeData.HierarchyCode); });
                foreach (UIItemList item in listfinds)
                {
                    foreach (SingleHierarchyResultData sr in result.ChildResults)
                    {
                        if (item.ID == sr.ID)
                        {
                            item.UpdateDate = sr.UpdateDate;
                            item.HierarchyCode = sr.HierarchyCode;
                        }
                    }
                }
                bsList.ResetBindings(false);
                if (CurrentChanged != null) CurrentChanged(this,nodeData);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            }
            catch (Exception ex)
            {
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
            if (CurrentChanged != null) CurrentChanged(this, Current);
            RefreshEnabled();
        }

        private void RefreshEnabled()
        {
            if (CurrentRow == null)
            {
                barDelete.Enabled = barAddFunction.Enabled = barEdit.Enabled = false;
            }
            else
            {
                if (CurrentRow.FunctionType == UIConfigItemType.MenuItem || CurrentRow.FunctionType == UIConfigItemType.Action)
                    barAddFunction.Enabled = false;
                else
                    barAddFunction.Enabled = true;

                if (CurrentRow.FunctionType != UIConfigItemType.Container)barEdit.Enabled= barDelete.Enabled = true;
                else barEdit.Enabled = barDelete.Enabled = false;
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

        public override void Refresh(object items)
        {
            List<UIItemList> list = this.DataSource as List<UIItemList>;
            if (list == null) return;

            List<UIItemList> newList = items as List<UIItemList>;

            foreach (var item in newList)
            {
                UIItemList tager = list.Find(delegate(UIItemList jItem) { return item.ID == item.ID; });
                if (tager == null) continue;

                Utility.CopyToValue(tager, item, typeof(UIItemList));
            }
            bsList.ResetBindings(false);
        }

        #endregion

    }

    public class PermissionMenuListPart : PermissionMainListPart
    {
        public PermissionMenuListPart():base() {this.Name ="PermissionMenuListPart"; }
        protected override SiteType siteType { get { return SiteType.Menu; } }
    }
    public class PermissionToolbarListPart : PermissionMainListPart
    {
        public PermissionToolbarListPart() : base() { this.Name = "PermissionToolbarListPart"; }

        protected override SiteType siteType { get { return SiteType.Toolbar; } }
    }
    public class PermissionStatusbarListPart : PermissionMainListPart
    {
        public PermissionStatusbarListPart() : base() { this.Name = "PermissionStatusbarListPart"; }
        protected override SiteType siteType { get { return SiteType.Statusbar; } }
    }
}
