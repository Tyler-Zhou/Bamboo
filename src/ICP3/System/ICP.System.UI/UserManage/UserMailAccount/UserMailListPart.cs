using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.UserManage.UserMailAccount
{
    [ToolboxItem(false)]
    public partial class UserMailListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {

        #region 服务注入
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public UserMailListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this._parentList = null;
                this.gvMain.BeforeLeaveRow -= this.gvMain_BeforeLeaveRow;
                this.gvMain.RowStyle -= this.gvMain_RowStyle;
                this.bsList.PositionChanged -= this.bsList_PositionChanged;
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
            colEmail.Caption = "邮箱";
        }

        #endregion

        #region Command

        [CommandHandler(UserCommonConstants.Common_MailAddData)]
        public void Common_MailAddData(object sender, EventArgs e)
        {
            AddData();
        }

        private void AddData()
        {
            if (CurrentRow != null && CurrentRow.IsNew) return;

            UserMailAccountList newData = new UserMailAccountList();
            newData.CreateBy = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.IsValid = true;
            newData.IsDirty = false;
            bsList.Insert(0, newData);
            gvMain.FocusedRowHandle = 0;
        }

        [CommandHandler(UserCommonConstants.Common_MailDeleteData)]
        public void Common_DeleteData(object sender, EventArgs e)
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
                bsList.ResetBindings(false);
                bsList_PositionChanged(null, null);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        [CommandHandler(UserCommonConstants.Command_MailSetDefault)]
        public void Command_MailSetDefault(object sender, EventArgs e)
        {
            SetDefault();
        }

        private void SetDefault()
        {
            if (CurrentRow == null) return;
            if (CurrentRow.IsNew)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "请先保存数据");
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
                            um.BeginEdit();
                        }
                    }
                }
                CurrentRow.IsDefault = true;
                CurrentRow.BeginEdit();
                bsList.ResetBindings(false);
                if (CurrentChanged != null) CurrentChanged(this, Current);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置默认帐号成功");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }

        }

        #endregion

        #region listEvent

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

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
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

            if (list.IsDefault)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
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
            if (data == null) { this.bsList.DataSource = new List<UserMailAccountList>(); }
            else
            {
                List<UserMailAccountList> datas = data as List<UserMailAccountList>;
                if(datas!=null)
                {
                    foreach (var item in datas)
	                {
                		 item.BeginEdit();
	                }
                }

                this.bsList.DataSource = data;

                if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
            }
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

        #region IPart 成员
        UserList _parentList = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ParentList")
                {
                    _parentList = item.Value as UserList;
                    if (_parentList == null || _parentList.IsValid == false)
                        this.Enabled = false;
                    else
                        this.Enabled = true;

                    if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                }
            }
        }
        #endregion
    }
}
