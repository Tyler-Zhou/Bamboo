using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.FAM.UI.BankReceiptList.Finder
{
    [ToolboxItem(false)]
    public partial class SingleFinderListPart : FinderListPart
    {
        public SingleFinderListPart()
            : base()
        {
            gvMain.OptionsSelection.MultiSelect = true;
            gvMain.DoubleClick += new EventHandler(gvMain_DoubleClick);
            gvMain.KeyDown += new KeyEventHandler(gvMain_KeyDown);
            Disposed += delegate {
                Selected = null;
                gvMain.DoubleClick -=gvMain_DoubleClick;
                gvMain.KeyDown -= gvMain_KeyDown;
            };
        }

        /// <summary>
        ///根据CodeOrName查询出来的结果可能会有很多。
        ///出于便利性考虑，默定位到与最完全匹配的结果中。
        /// </summary>
        /// <param name="refNo">CodeOrName</param>
        public void LocateToMatchedItem(string refNo)
        {
            var list = (List<BankReceiptListInfo>)bsList.List;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].No.Trim().ToLower() == refNo.Trim().ToLower())
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
            var findForm = FindForm();
            if (findForm != null) findForm.Close();
        }

        [CommandHandler(BankReceiptFinderConstants.COMMANDSINGLEFINDERCONFIRM)]
        public void SingleFinder_Confirm_BankReceipt(object sender, EventArgs e)
        {
            ConfirmSelected();
        }
       
        #region IListPart成员

        public override event SelectedHandler Selected;

        #endregion
    }
}
