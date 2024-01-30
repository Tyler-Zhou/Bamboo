using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.UserManage
{
    [ToolboxItem(false)]
    public partial class UserMailAccountListPart : ICP.Framework.ClientComponents.UIFramework.BaseListEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        #endregion

        #region init

        public UserMailAccountListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this.Saved = null;
                this.gcMain.DataSource = null;
                this.gvMain.BeforeLeaveRow -= this.gvMain_BeforeLeaveRow;
                this.gvMain.RowStyle -= this.gvMain_RowStyle;
                this.dxErrorProvider1.DataSource = null;
                this.bsInfo.DataSource = null;
                this.bsInfo.Dispose();
                this.bsList.DataSource = null;
                this.bsList.PositionChanged -= this.bsList_PositionChanged;
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
            barSave.Caption = "保存(&S)";
            barRemove.Caption = "删除(&R)";

            gvMain.NewItemRowText = "点击这里以新增一行.";

            //colMailIncomingHost.Caption = "";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }
        private void InitControls()
        {
            List<EnumHelper.ListItem<MailProtocol>> mailProtocols = EnumHelper.GetEnumValues<MailProtocol>(LocalData.IsEnglish);
            cmbIncomingProtocol.Properties.BeginUpdate();
            cmbMailOutgoingProtocol.Properties.BeginUpdate();
            foreach (var item in mailProtocols)
            {
                cmbIncomingProtocol.Properties.Items.Add(new ImageComboBoxItem( item.Name,item.Value));
                cmbMailOutgoingProtocol.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbIncomingProtocol.Properties.EndUpdate();
            cmbMailOutgoingProtocol.Properties.EndUpdate();
            panel1.Enabled = false;
        }

        #endregion

        List<UserMailAccountList> SelectedItem
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<UserMailAccountList> tagers = new List<UserMailAccountList>();
                foreach (var item in rowIndexs)
                {
                    UserMailAccountList ma = gvMain.GetRow(item) as UserMailAccountList;
                    if (ma != null) tagers.Add(ma);
                }

                return tagers;
            }
        }

        #region BarItem

        private void barNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddData();
        }

        private void AddData()
        {
            UserMailAccountList newData = new UserMailAccountList();
            newData.CreateBy = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.IsValid = true;
            newData.IsDirty = false;
            bsList.List.Insert(0, newData);
            bsList.ResetCurrentItem();
            gvMain.FocusedRowHandle = 0;
            gvMain.RefreshData();
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private bool Save()
        {
            //if (_userList == null) return false;

            //List<UserMailAccountList> currentData = bsList.DataSource as List<UserMailAccountList>;
            //if (currentData == null || currentData.Count == 0) return false;

            //foreach (var item in currentData)
            //{
            //    if (item.Validate() == false) return false;
            //}

            //try
            //{
            //    List<User2OrganizationJobList> currentList = this.DataSource as List<User2OrganizationJobList>;
            //    List<Guid> userJobRelationIds = new List<Guid>();
            //    List<Guid> organizationJobs = new List<Guid>();
            //    List<Guid> jobIds = new List<Guid>();
            //    List<int> rowNums = new List<int>();
            //    List<DateTime?> updateDates = new List<DateTime?>();

            //    for (int i = 0; i < currentList.Count; i++)
            //    {
            //        if (currentList[i].Validate() == false)
            //        {
            //            return false;
            //        }

            //        userJobRelationIds.Add(currentList[i].ID);
            //        organizationJobs.Add(currentList[i].OrganizationJobID);
            //        updateDates.Add(currentList[i].UpdateDate);
            //        rowNums.Add(i);
            //    }

            //    ManyResultData result = userService.SetUserOrganizationJob(currentList[0].UserID,
            //                                                       userJobRelationIds.ToArray(),
            //                                                       organizationJobs.ToArray(),
            //                                                       updateDates.ToArray(),
            //        //jobIds.ToArray(),
            //                                                       LocalData.UserInfo.LoginID);

            //    for (int i = 0; i < currentList.Count; i++)
            //    {
            //        currentList[i].ID = result.ChildResults[i].ID;
            //        currentList[i].UpdateDate = result.ChildResults[i].UpdateDate;
            //        currentList[i].BeginEdit();
            //    }

            //    if (Saved != null) Saved(currentList);

            //    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            return true;
            //}
            //catch (Exception ex)
            //{
            //    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            //    return false;
            //}
        }

        private void barRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            List<UserMailAccountList> selectedItem = SelectedItem;
            if (selectedItem == null || selectedItem.Count == 0) return;

            if (Utility.EnquireIsDeleteCurrentData() == false) return;

            try
            {
                List<Guid> ids = new List<Guid>();
                List<DateTime?> updateDatas = new List<DateTime?>();
                foreach (var item in selectedItem)
                {
                    ids.Add(item.ID);
                    updateDatas.Add(item.UpdateDate);
                }

                UserService.RemoveUserMailAccount(ids.ToArray()
                                                           , updateDatas.ToArray()
                                                           , LocalData.UserInfo.LoginID);

                List<UserMailAccountList> currentData = bsList.DataSource as List<UserMailAccountList>;
                foreach (var item in selectedItem)
                {
                    currentData.Remove(item);
                }
                bsList.DataSource = currentData;
                gvMain.RefreshData();
                bsList_PositionChanged(this, null);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        private void barDefault_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetDefault();
        }

        private void SetDefault()
        {
            if (CurrentRow == null) return;
            if (CurrentRow.IsNew)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请先保存数据");
                return;
            }

            try
            {
                ManyResultData result = UserService.SetUserDefaultMailAccount(
                    CurrentRow.ID,
                    LocalData.UserInfo.LoginID);

                List<UserMailAccountList> currentList = this.DataSource as List<UserMailAccountList>;
                foreach (UserMailAccountList um in currentList)
                {
                    foreach (SingleResultData s in result.ChildResults)
                    {
                        if (um.ID == s.ID)
                        {
                            um.IsDefault = false;
                            um.UpdateDate = s.UpdateDate;
                        }
                    }
                }
                CurrentRow.IsDefault = true;
                if (Saved != null) Saved(currentList);

                bsList_PositionChanged(this, null);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置默认帐号成功");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }

        }

        private void chkSameAsIncoming_CheckedChanged(object sender, EventArgs e)
        {
            txtOutgoingLogin.Enabled = txtOutgoingPassword.Enabled = !chkSameAsIncoming.Checked;
            if (chkSameAsIncoming.Checked)
                txtOutgoingLogin.Text = txtOutgoingPassword.Text = string.Empty;
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }

        UserMailAccountList CurrentRow
        {
            get { return bsList.Current as UserMailAccountList; }
            set
            {
                UserMailAccountList current = CurrentRow;
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
            if (data == null) { this.bsList.DataSource = typeof(UserMailAccountList); }
            else
            {
                this.bsList.DataSource = data;
                bsList.ResetBindings(false);
                if (CurrentChanged != null) CurrentChanged(this, Current);
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

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region IPart 成员

        UserList _userList = null;

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ParentList")
                {
                    _userList = item.Value as UserList;

                    this.Enabled = _userList.IsValid;
                }
            }
        }

        #endregion

        #region List Event

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
            RefreshEnabled();

            if (CurrentRow == null)
            {
                bsInfo.DataSource = typeof(UserMailAccountList);
                panel1.Enabled = false;
            }
            else
            {
                panel1.Enabled = true;
                bsInfo.DataSource = CurrentRow;
            }
        }

        private void RefreshEnabled()
        {
            if (CurrentRow == null)
                barRemove.Enabled = false;
            else
            {
                barRemove.Enabled = true;
                if (string.IsNullOrEmpty(CurrentRow.MailOutgoingLogin) && string.IsNullOrEmpty(CurrentRow.MailOutgoingPassword))
                    chkSameAsIncoming.Checked = true;
            }
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

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            UserMailAccountList list = gvMain.GetRow(e.RowHandle) as UserMailAccountList;
            if (list == null) return;

            if (list.IsNew|| list.IsDirty)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
        }

        #endregion
    }
}

