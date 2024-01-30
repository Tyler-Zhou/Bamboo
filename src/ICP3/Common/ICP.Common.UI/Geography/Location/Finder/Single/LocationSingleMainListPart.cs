using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Common.UI.Geography.Location
{
    [ToolboxItem(false)]
    public partial class LocationSingleMainListPart : LocationMainListPart
    {
        public LocationSingleMainListPart()
            : base()
        {
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.DoubleClick += new EventHandler(gvMain_DoubleClick);
            this.gvMain.KeyDown += new KeyEventHandler(gvMain_KeyDown);
            this.Disposed += delegate {
                this.gvMain.DoubleClick -= new EventHandler(gvMain_DoubleClick);
                this.gvMain.KeyDown -= new KeyEventHandler(gvMain_KeyDown);
                this.Selected = null;
            };
        }

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConfirmSelected();
                CloseForm();
            }
        }

        void gvMain_DoubleClick(object sender, EventArgs e)
        {
            ConfirmSelected();
            CloseForm();
        }

        private void ConfirmSelected()
        {
            if (CurrentRow == null) return;

            if (Selected != null)
            {
                Selected(this, CurrentRow);
            }
        }

        [CommandHandler(LocationCommonConstants.Common_FinderConfirm)]
        public void Common_FinderConfirm(object sender, EventArgs e)
        {
            ConfirmSelected();
        }

        private void CloseForm()
        {
            var findForm = this.FindForm();
            if (findForm != null) findForm.Close();
        }

        #region IListPart成员

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        #endregion
    }
}
