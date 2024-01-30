using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.CustomerFinder
{
    [ToolboxItem(false)]
    public partial class CustomerMultiSelectedListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

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

        #region Init

        public CustomerMultiSelectedListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.gcMain.DataSource = null;
                this.bsList.DataSource = null;
                this.bsList.PositionChanged -= this.bsMainList_PositionChanged;
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
            if (LocalData.IsEnglish) colName.FieldName = "EName";
            else colName.FieldName = "CName";
        }


        #endregion

        #region IListPart<userList> 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        private CustomerInfo CurrentRow
        {
            get { return Current as CustomerInfo; }
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

        [CommandHandler(CustomerCommonConstants.Common_FinderRemove)]
        public void Common_FinderRemove(object sender, EventArgs e)
        {
            List<CustomerInfo> selectedItem = SelectedItem;
            if (selectedItem == null || selectedItem.Count == 0) return;

            foreach (var item in selectedItem)
            {
                bsList.Remove(item);
            }
            bsList.ResetBindings(false);
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        [CommandHandler(CustomerCommonConstants.Common_FinderRemoveAll)]
        public void Common_FinderRemoveAll(object sender, EventArgs e)
        {
            this.DataSource = new List<CustomerInfo>();
        }

        [CommandHandler(CustomerCommonConstants.Common_FinderConfirm)]
        public void Common_FinderConfirm(object sender, EventArgs e)
        {
            if (Selected != null)
                Selected(this, this.DataSource);
        }


        #endregion
    }
}
