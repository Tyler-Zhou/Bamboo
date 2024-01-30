using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Common.UI.CustomerFinder
{
    [ToolboxItem(false)]
    public partial class CustomerMultiMainListPart : CustomerMainListPart
    {
        public CustomerMultiMainListPart()
            : base()
        {
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.DoubleClick += new EventHandler(gvMain_DoubleClick);
            this.Disposed += delegate {

                this.Selected = null;
                this.gvMain.DoubleClick -= new EventHandler(gvMain_DoubleClick);
                
            };
        }

        List<CustomerInfo> SelectedItem
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<CustomerInfo> tagers = new List<CustomerInfo>();
                foreach (var item in rowIndexs)
                {
                    CustomerInfo ma = gvMain.GetRow(item) as CustomerInfo;
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
                Selected(this, SelectedItem);
            }
        }

        [CommandHandler(CustomerCommonConstants.Common_FindeSelect)]
        public void Common_FindeSelect(object sender, EventArgs e)
        {
            ConfirmSelected();

        }

        #region IListPart成员

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        #endregion
    }
}
