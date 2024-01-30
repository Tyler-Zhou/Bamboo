using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.Configure.ChargingCode
{
    [ToolboxItem(false)]
    public partial class ChargingCodeMultiMainListPart : ChargingCodeMainListPart
    {
        public ChargingCodeMultiMainListPart()
            : base()
        {
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.DoubleClick += new EventHandler(gvMain_DoubleClick);
            this.Disposed += delegate
            {
                this.Selected = null;
                this.gvMain.DoubleClick -= this.gvMain_DoubleClick;
            };
        }

        List<SolutionChargingCodeList> SelectedItem
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<SolutionChargingCodeList> tagers = new List<SolutionChargingCodeList>();
                foreach (var item in rowIndexs)
                {
                    SolutionChargingCodeList ma = gvMain.GetRow(item) as SolutionChargingCodeList;
                    if (ma != null) tagers.Add(ma);
                }

                return tagers;
            }
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
                using (new CursorHelper())
                {
                    Selected(this, SelectedItem);
                }
            }
        }

        [CommandHandler(ChargingCodeCommonConstants.Common_FindeSelect)]
        public void Common_FindeSelect(object sender, EventArgs e)
        {
            ConfirmSelected();

        }

        #region IListPart成员

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        #endregion
    }
}
