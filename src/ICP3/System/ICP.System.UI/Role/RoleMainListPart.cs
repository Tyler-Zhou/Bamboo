using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.Role
{
    [ToolboxItem(false)]
    public partial class RoleMainListPart :  ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IRoleService RoleService
        {
            get
            {
                return ServiceClient.GetService<IRoleService>();
            }
        }

        #endregion

        #region Init

        public RoleMainListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this.gcMain.DataSource = null;
                this.gvMain.BeforeLeaveRow -= this.gvMain_BeforeLeaveRow;
                this.gvMain.RowStyle -= this.gvMain_RowStyle;
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
            colDescription.Caption = "描述";
            colCName.Caption = "中文名";
            colEName.Caption = "英文名";
            colCreateByName.Caption = "创建人";
            colCreateDate.Caption = "创建日期";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
        }

        #endregion

        #region IListPart成员

        public override  object Current
        {
            get { return bsList.Current; }
        }
        private RoleList CurrentRow
        {
            get { return Current as RoleList; }
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
                if (CurrentChanged != null) CurrentChanged(this, Current);
            }
        }

        public override void Refresh(object items)
        {
            List<RoleList> list = this.DataSource as List<RoleList>;
            if (list == null) return;
            List<RoleList> newLists = items as List<RoleList>;

            foreach (var item in newLists)
            {
                RoleList tager = list.Find(delegate(RoleList jItem) { return item.ID == item.ID; });
                if (tager == null) continue;

                Utility.CopyToValue(tager, item, typeof(RoleList));
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
            RoleList list = gvMain.GetRow(e.RowHandle) as RoleList;
            if (list == null) return;

            if(list.IsValid ==false)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
        }

        #endregion

        #region Workitem Common

        [CommandHandler(RoleCommonConstants.Common_DisuseData)]
        public void Common_DisuseData(object sender, EventArgs e)
        {
            RoleList currentRow = Current as RoleList;
            if (currentRow == null) return;

            try
            {
                SingleResultData result = RoleService.ChangeRoleState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                currentRow.IsValid = !currentRow.IsValid;
                currentRow.UpdateDate = result.UpdateDate;
                bsList.ResetCurrentItem();
                bsMainList_PositionChanged(this, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Disuse Successfully" : "作废成功");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        [CommandHandler(RoleCommonConstants.Common_AddData)]
        public void Common_AddData(object sender, EventArgs e)
        {
            if (CurrentRow!=null && CurrentRow.IsNew) return;
            RoleList newData = new RoleList();
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.IsValid = true;
            newData.IsDirty = false;
            bsList.Insert(0, newData);
            gvMain.FocusedRowHandle = 0;
        }


        #endregion
    }
}
