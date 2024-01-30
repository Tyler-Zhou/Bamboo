using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Common.UI.Geography.Location
{
    [ToolboxItem(false)]
    public partial class LocationMultiMainListPart : LocationMainListPart
    {
        public LocationMultiMainListPart()
            : base()
        {
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.DoubleClick += new EventHandler(gvMain_DoubleClick);
            this.Disposed += delegate {

                this.Selected = null;
            };
        }

        List<LocationList> SelectedItem
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<LocationList> tagers = new List<LocationList>();
                foreach (var item in rowIndexs)
                {
                    LocationList ma = gvMain.GetRow(item) as LocationList;
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

        [CommandHandler(LocationCommonConstants.Common_FindeSelect)]
        public void Common_FindeSelect(object sender, EventArgs e)
        {
            ConfirmSelected();

        }

        #region IListPart成员

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        #endregion
    }
}
