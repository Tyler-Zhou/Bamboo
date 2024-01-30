using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.Common.UI.CustomerFinder
{
    [ToolboxItem(false)]
    public partial class CustomerSingleMainListPart : CustomerMainListPart
    {
        public CustomerSingleMainListPart()
            : base()
        {
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.DoubleClick += new EventHandler(gvMain_DoubleClick);
            this.gvMain.KeyDown += new KeyEventHandler(gvMain_KeyDown);
            this.Disposed += delegate {
                this.Selected = null;
                this.gvMain.DoubleClick -=gvMain_DoubleClick;
                this.gvMain.KeyDown -= gvMain_KeyDown;
            };
        }

        /// <summary>
        ///根据CodeOrName查询出来的结果可能会有很多。
        ///出于便利性考虑，默定位到与最完全匹配的结果中。
        /// </summary>
        /// <param name="codeOrName">CodeOrName</param>
        public void LocateToMatchedItem(string codeOrName)
        {
            var list = (List<CustomerInfo>)bsList.List;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Code.Trim().ToLower() == codeOrName.Trim().ToLower())
                {
                    bsList.Position = i;
                    gvMain.UnselectRow(0);
                    gvMain.SelectRow(i);
                    break;
                }
            }
        }

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConfirmSelected();
            }
        }

        void gvMain_DoubleClick(object sender, EventArgs e)
        {
            ConfirmSelected();
        }

        private void ConfirmSelected()
        {
            if (CurrentRow == null) return;

            if (Selected != null)
            {
                Selected(this, CurrentRow);
            }
            var findForm = this.FindForm();
            if (findForm != null) findForm.Close();
        }

        [CommandHandler(CustomerCommonConstants.Common_FinderConfirm)]
        public void Common_FinderConfirm(object sender, EventArgs e)
        {
            ConfirmSelected();
        }
       
        #region IListPart成员

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        #endregion
    }
}
