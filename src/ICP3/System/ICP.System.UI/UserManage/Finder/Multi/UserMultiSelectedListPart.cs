using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Sys.UI.UserManage.Finder
{
    [ToolboxItem(false)]
    public partial class UserMultiSelectedListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        List<UserList> SelectedItem
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<UserList> tagers = new List<UserList>();
                foreach (var item in rowIndexs)
                {
                    UserList ma = gvMain.GetRow(item) as UserList;
                    if (ma != null) tagers.Add(ma);
                }

                return tagers;
            }
        }

        #region Init

        public UserMultiSelectedListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this.Selected = null;
                this.gcMain.DataSource = null;
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
            colCreateByName.Caption = "创建人";
            colCreateDate.Caption = "创建日期";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Utility.ShowGridRowNo(gvMain);
        }

        #endregion

        #region IListPart<userList> 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        private UserList CurrentRow
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
                if (CurrentChanged != null) CurrentChanged(this, Current);
            }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        #endregion

        #region GridView Event

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        #endregion

        #region Workitem Common

        [CommandHandler(UserCommonConstants.Common_FinderRemove)]
        public void Common_FinderRemove(object sender, EventArgs e)
        {
            List<UserList> selectedItem = SelectedItem;
            if (selectedItem == null || selectedItem.Count == 0) return;

            foreach (var item in selectedItem)
            {
                bsList.Remove(item);
            }
            bsList.ResetBindings(false);
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        [CommandHandler(UserCommonConstants.Common_FinderRemoveAll)]
        public void Common_FinderRemoveAll(object sender, EventArgs e)
        {
            this.DataSource = new List<UserList>();
        }

        [CommandHandler(UserCommonConstants.Common_FinderConfirm)]
        public void Common_FinderConfirm(object sender, EventArgs e)
        {
            if (Selected != null)
                Selected(this, this.DataSource);
        }


        #endregion
    }
}

