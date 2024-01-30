using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.Sys.UI.UserManage.Finder
{
    public class UserMultiMainListPart : UserMainListPart
    {
        public UserMultiMainListPart()
            : base()
        {
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(gvMain_SelectionChanged);
            this.gvMain.DoubleClick += new EventHandler(gvMain_DoubleClick);
            this.Disposed += delegate {
                this.Selected = null;
                this.gvMain.DoubleClick -= this.gvMain_DoubleClick;
                this.gvMain.SelectionChanged -= this.gvMain_SelectionChanged;
            
            };
        }

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

        void gvMain_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            //if (Selected != null)
            //    Selected(this, SelectedItem);
        }

        void gvMain_DoubleClick(object sender, EventArgs e)
        {
            ConfirmSelected();
        }

        private void ConfirmSelected()
        {
            if (SelectedItem == null) return;

            if (Selected != null)
            {
                Selected(this, SelectedItem);
            }
        }

        [CommandHandler(UserCommonConstants.Common_FindeSelect)]
        public void Common_FindeSelect(object sender, EventArgs e)
        {
            ConfirmSelected();

        }

        #region IListPart成员

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        #endregion
    }
}
