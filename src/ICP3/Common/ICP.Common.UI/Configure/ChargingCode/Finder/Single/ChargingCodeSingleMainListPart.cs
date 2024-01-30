using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Common.UI.Configure.ChargingCode
{
    [ToolboxItem(false)]
    public partial class ChargingCodeSingleMainListPart : ChargingCodeMainListPart
    {
        public ChargingCodeSingleMainListPart()
            : base()
        {
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.DoubleClick += new EventHandler(gvMain_DoubleClick);
            this.gvMain.KeyDown += new KeyEventHandler(gvMain_KeyDown);
            this.Disposed += delegate
            {
                this.Selected = null;
                this.gvMain.DoubleClick -= this.gvMain_DoubleClick;
                this.gvMain.KeyDown -= this.gvMain_KeyDown;
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

        [CommandHandler(ChargingCodeCommonConstants.Common_FinderConfirm)]
        public void Common_FinderConfirm(object sender, EventArgs e)
        {
            ConfirmSelected();
        }

        #region IListPart成员

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        #endregion

        private void CloseForm()
        {
            var findForm = this.FindForm();
            if (findForm != null) findForm.Close();
        }
    }
}
