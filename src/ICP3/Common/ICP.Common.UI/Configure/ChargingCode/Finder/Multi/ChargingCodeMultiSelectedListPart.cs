using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Common.UI.Configure.ChargingCode
{
    [ToolboxItem(false)]
    public partial class ChargingCodeMultiSelectedListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

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

        #region Init

        public ChargingCodeMultiSelectedListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.gcMain.DataSource = null;
                this.bsList.PositionChanged -= this.bsMainList_PositionChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.Selected = null;
                this.CurrentChanged = null;
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
        private SolutionChargingCodeList CurrentRow
        {
            get { return Current as SolutionChargingCodeList; }
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

        [CommandHandler(ChargingCodeCommonConstants.Common_FinderRemove)]
        public void Common_FinderRemove(object sender, EventArgs e)
        {
            List<SolutionChargingCodeList> selectedItem = SelectedItem;
            if (selectedItem == null || selectedItem.Count == 0) return;

            foreach (var item in selectedItem)
            {
                bsList.Remove(item);
            }
            bsList.ResetBindings(false);
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        [CommandHandler(ChargingCodeCommonConstants.Common_FinderRemoveAll)]
        public void Common_FinderRemoveAll(object sender, EventArgs e)
        {
            this.DataSource = new List<SolutionChargingCodeList>();
        }

        [CommandHandler(ChargingCodeCommonConstants.Common_FinderConfirm)]
        public void Common_FinderConfirm(object sender, EventArgs e)
        {
            if (Selected != null)
                Selected(this, this.DataSource);
        }


        #endregion
    }
}
