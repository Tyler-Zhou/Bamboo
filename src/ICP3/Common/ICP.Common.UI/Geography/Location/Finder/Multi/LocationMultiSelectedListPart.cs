using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Common.UI.Geography.Location
{
    [ToolboxItem(false)]
    public partial class LocationMultiSelectedListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

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

        #region Init

        public LocationMultiSelectedListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this.Selected = null;
                this.bsList.PositionChanged -= this.bsMainList_PositionChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CommonUtility.ShowGridRowNo(gvMain);
        }


        #endregion

        #region IListPart<userList> 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        private LocationList CurrentRow
        {
            get { return Current as LocationList; }
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

        [CommandHandler(LocationCommonConstants.Common_FinderRemove)]
        public void Common_FinderRemove(object sender, EventArgs e)
        {
            List<LocationList> selectedItem = SelectedItem;
            if (selectedItem == null || selectedItem.Count == 0) return;

            foreach (var item in selectedItem)
            {
                bsList.Remove(item);
            }
            bsList.ResetBindings(false);
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        [CommandHandler(LocationCommonConstants.Common_FinderRemoveAll)]
        public void Common_FinderRemoveAll(object sender, EventArgs e)
        {
            this.DataSource = new List<LocationList>();
        }

        [CommandHandler(LocationCommonConstants.Common_FinderConfirm)]
        public void Common_FinderConfirm(object sender, EventArgs e)
        {
            if (Selected != null)
                Selected(this, this.DataSource);
        }


        #endregion
    }
}
