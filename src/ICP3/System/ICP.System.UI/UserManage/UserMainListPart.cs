using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.UserManage
{
    [ToolboxItem(false)]
    public partial class UserMainListPart :  ICP.Framework.ClientComponents.UIFramework.BaseListPart
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

        #region Init

        public UserMainListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this.gvMain.BeforeLeaveRow -= this.gvMain_BeforeLeaveRow;
                this.gvMain.RowStyle -= this.gvMain_RowStyle;
                this.gcMain.DataSource = null;
                this.bsList.PositionChanged -= this.bsMainList_PositionChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.Selected = null;
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
            colCreateByName.Caption = "创建人";
            colCreateDate.Caption = "创建日期";
            colJobName.Caption = "职位";
            colOrganizationName.Caption = "部门";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Utility.ShowGridRowNo(gvMain);
        }

        #endregion

        #region IListPart 成员

        public override  object Current
        {
            get { return bsList.Current; }
        }
        protected UserList CurrentRow
        {
            get { return Current as UserList; }
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
                this.gvMain.BestFitColumns();
                if (CurrentChanged != null) CurrentChanged(this, Current);
            }
        }

        public override void Refresh(object items)
        {
            List<UserList> list = this.DataSource as List<UserList>;
            if (list == null) return;
            List<UserList> newLists = items as List<UserList>;

            foreach (var item in newLists)
            {
                UserList tager = list.Find(delegate(UserList jItem) { return item.ID == jItem.ID; });
                if (tager == null) continue;

                Utility.CopyToValue(tager, item, typeof(UserList));
            }
            bsList.ResetBindings(false);
        }

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            this.gvMain.BeforeLeaveRow -=new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvMain_BeforeLeaveRow);
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvMain_BeforeLeaveRow);
        }

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            UserList list = gvMain.GetRow(e.RowHandle) as UserList;
            if (list == null) return;

            if (list.IsValid == false)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
        }

        #endregion

        #region Workitem Common

        [CommandHandler(UserCommonConstants.Common_DisuseData)]
        public void Common_DisuseData(object sender, EventArgs e)
        {
            UserList currentRow = Current as UserList;
            if (currentRow == null) return;

            try
            {
                SingleResultData result = UserService.ChangeUserState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                currentRow.IsValid = !currentRow.IsValid;
                currentRow.UpdateDate = result.UpdateDate;
                bsList.ResetCurrentItem();
                bsMainList_PositionChanged(this, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Disuse Successfully" : "作废成功");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        [CommandHandler(UserCommonConstants.Common_AddData)]
        public void Common_AddData(object sender, EventArgs e)
        {
            if (CurrentRow != null && CurrentRow.IsNew) return;

            UserList newData = new UserList();
            newData.CreateBy = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.IsValid = true;
            newData.IsDirty = false;
            bsList.Insert(0, newData);
            gvMain.FocusedRowHandle = 0;
        }

        #endregion
    }
}

