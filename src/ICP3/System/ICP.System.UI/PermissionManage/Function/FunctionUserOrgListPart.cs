using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.PermissionManage.Function
{
    [ToolboxItem(false)]
    public partial class FunctionUserOrgListPart : ICP.Framework.ClientComponents.UIFramework.BaseListEditPart
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


        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        #endregion

        #region init

        public FunctionUserOrgListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.Saved = null;
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this.dxErrorProvider1.DataSource = null;
                this.gcMain.DataSource = null;
                this.bsList.PositionChanged -= this.bsList_PositionChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                if (userNameFinder != null)
                {
                    userNameFinder.Dispose();
                    userNameFinder = null;
                }
                if (organizationFinder != null)
                {
                    organizationFinder.Dispose();
                    organizationFinder = null;
                }
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            barSave.Caption = "保存(&S)";
            barRemove.Caption = "删除(&R)";
            colOrganizationName.Caption = "组织结构";
            colUserName.Caption = "用户";

            gvMain.NewItemRowText = "点击这里以新增一行.";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }
        private IDisposable userNameFinder, organizationFinder;
        private void InitControls()
        {
          userNameFinder=  DataFindClientService.RegisterGridColumnFinder(colUserName
                                                , ICP.Sys.ServiceInterface.SystemFinderConstants.UserFinder
                                                , "UserID"
                                                , "UserName"
                                                , "ID"
                                                ,LocalData.IsEnglish ?"EName":"CName");

          organizationFinder=  DataFindClientService.RegisterGridColumnFinder(colOrganizationName
                                               , ICP.Sys.ServiceInterface.SystemFinderConstants.OrganizationFinder
                                               , "OrganizationID"
                                               , "OrganizationName"
                                               , "ID"
                                               , LocalData.IsEnglish ? "EShortName" : "CShortName");
        }
 

        #endregion

        List<UserPermissionList> SelectedItem
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<UserPermissionList> tagers = new List<UserPermissionList>();
                foreach (var item in rowIndexs)
                {
                    UserPermissionList ma = gvMain.GetRow(item) as UserPermissionList;
                    if (ma != null) tagers.Add(ma);
                }

                return tagers;
            }
        }

        #region BarItem

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private bool Save()
        {
            if (_parentList == null) return false;

            List<UserPermissionList> currentList = bsList.DataSource as List<UserPermissionList>;
            if (currentList == null || currentList.Count == 0) return false;

            foreach (var item in currentList)
            {
                if (item.Validate() == false) return false;
            }

            try
            {
                List<Guid?> permissionRoleIds = new List<Guid?>();
                List<Guid> userIds = new List<Guid>();
                List<Guid> organizationIDs = new List<Guid>();
                List<Guid> functionIds = new List<Guid>();

                List<DateTime?> updateDates = new List<DateTime?>(); ;

                for (int i = 0; i < currentList.Count; i++)
                {
                    if (currentList[i].Validate() == false)
                    {
                        return false;
                    }

                    permissionRoleIds.Add(currentList[i].ID);

                    userIds.Add(currentList[i].UserID);
                    organizationIDs.Add(currentList[i].OrganizationID);
                    functionIds.Add(_parentList.ID);
                    updateDates.Add(currentList[i].UpdateDate);
                }

                ManyResultData result = null;

                if (_parentList.FunctionType == FunctionType.Action)
                    result = PermissionService.SetActionUserPermission(permissionRoleIds.ToArray()
                                                                , functionIds.ToArray()
                                                                , userIds.ToArray()
                                                                , organizationIDs.ToArray()
                                                                , LocalData.UserInfo.LoginID
                                                                , updateDates.ToArray());
                else
                    result = PermissionService.SetFunctionUserPermission(permissionRoleIds.ToArray()
                                                                , functionIds.ToArray()
                                                                , userIds.ToArray()
                                                                , organizationIDs.ToArray()
                                                                , LocalData.UserInfo.LoginID
                                                                , updateDates.ToArray());


                for (int i = 0; i < currentList.Count; i++)
                {
                    currentList[i].ID = result.ChildResults[i].ID;
                    currentList[i].UpdateDate = result.ChildResults[i].UpdateDate;
                    currentList[i].BeginEdit();
                }
                bsList.DataSource = currentList;
                if (Saved != null) Saved(currentList);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                return false;
            }
        }

        private void barRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<UserPermissionList> selectedItem = SelectedItem;
            if (selectedItem == null || selectedItem.Count == 0) return;

            if (Utility.EnquireIsDeleteCurrentData() == false) return;

            try
            {
                List<Guid> ids = new List<Guid>();
                List<DateTime?> updateDatas = new List<DateTime?>();
                foreach (var item in selectedItem)
                {
                    if (item.IsNew) continue;
                    ids.Add(item.ID);
                    updateDatas.Add(item.UpdateDate);
                }

                if (_parentList.FunctionType == FunctionType.Action)
                    PermissionService.RemoveActionUserPermission(ids.ToArray(), LocalData.UserInfo.LoginID, updateDatas.ToArray());
                else
                    PermissionService.RemoveFunctionUserPermission(ids.ToArray(), LocalData.UserInfo.LoginID, updateDatas.ToArray());

                List<UserPermissionList> currentData = bsList.DataSource as List<UserPermissionList>;
                foreach (var item in selectedItem)
                {
                    currentData.Remove(item);
                }
                bsList.DataSource = currentData;
                bsList.ResetBindings(false);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }

        UserPermissionList CurrentRow
        {
            get { return bsList.Current as UserPermissionList; }
            set
            {
                UserPermissionList current = CurrentRow;
                if (current != null) current = value;
            }
        }


        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        public override event CancelEventHandler CurrentChanging;

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }
        void BindingData(object data)
        {
            if (data == null) { this.bsList.DataSource = new List<UserPermissionList>(); }
            else
            {
                List<UserPermissionList> datas = data as List<UserPermissionList>;
                if (datas != null && datas.Count > 0)
                {
                    foreach (var item in datas) { item.BeginEdit(); }
                }

                this.bsList.DataSource = datas;
                this.bsList.ResetBindings(false);
            }
        }

        #endregion

        #region IEditPart 成员

        public override void EndEdit()
        {
            bsList.EndEdit();
        }

        public override bool SaveData()
        {
            return Save();
        }

        public event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region IPart 成员
        FunctionList _parentList = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ParentList")
                {
                    _parentList = item.Value as FunctionList;
                    if (_parentList == null||_parentList.FunctionType == FunctionType.Module)
                        this.Enabled = false;
                    else
                        this.Enabled = true;
                }
            }
        }
        #endregion

        #region List Event

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
            RefreshEnabled();
        }

        private void RefreshEnabled()
        {
            if (Current == null)
                barRemove.Enabled = false;
            else
                barRemove.Enabled = true;
        }

        private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
        }

        private void gvMain_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            UserPermissionList newData = gvMain.GetRow(e.RowHandle) as UserPermissionList;
            if (newData == null) return;

            newData.PermissionID = _parentList.ID;
            newData.IsDirty = false;
        }

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            UserPermissionList list = gvMain.GetRow(e.RowHandle) as UserPermissionList;
            if (list == null) return;

            if (list.IsNew || list.IsDirty)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
        }

        #endregion
    }
}
