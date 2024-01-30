using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI.Commands;
using System.ComponentModel;

namespace ICP.Sys.UI.UserManage.Finder
{
    [ToolboxItem(false)]
    public class UserSingleMainListPart : UserMainListPart
    {
        public UserSingleMainListPart()
            : base()
        {
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.DoubleClick += new EventHandler(gvMain_DoubleClick);
        }

        void gvMain_DoubleClick(object sender, EventArgs e)
        {
            ConfirmSelected();
            this.FindForm().Close();
        }

        private void ConfirmSelected()
        {
            if (CurrentRow == null) return;

            if (Selected != null)
            {
                Selected(this, CurrentRow);
            }
        }

        [CommandHandler(UserCommonConstants.Common_FinderConfirm)]
        public void Common_FinderConfirm(object sender, EventArgs e)
        {
            ConfirmSelected();
            this.FindForm().Close();
        }

        #region IListPart成员

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        #endregion
    }
}
