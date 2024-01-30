using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Extender;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ICP.FAM.UI.BankReceiptList.Finder
{
    public partial class FinderListPart : BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region Init

        public FinderListPart()
        {
            InitializeComponent();

            Disposed += delegate {
                Selected = null;
                CurrentChanged = null;
                CurrentChanging = null;
                gvMain.BeforeLeaveRow -= gvMain_BeforeLeaveRow;
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsMainList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();

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
            gvMain.ShowGridViewRowNo();
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected BankReceiptInfo CurrentRow
        {
            get { return Current as BankReceiptInfo; }
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
                if (bsList.List.Count > 0)
                {
                    gvMain.Focus();
                    gvMain.SelectRow(0);
                }
            }
        }

        public override void Refresh(object items)
        {
            List<BankReceiptInfo> list = DataSource as List<BankReceiptInfo>;
            if (list == null) return;
            List<BankReceiptInfo> newLists = items as List<BankReceiptInfo>;

            foreach (var item in newLists)
            {
                BankReceiptInfo tager = list.Find(delegate(BankReceiptInfo jItem) { return item.ID == item.ID; });
                if (tager == null) continue;

                Utility.CopyToValue(tager, item, typeof(BankReceiptInfo));
            }
            bsList.ResetBindings(false);
        }

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }

        public override event SelectedHandler Selected;

        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            gvMain.BeforeLeaveRow -= new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvMain_BeforeLeaveRow);
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
            gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvMain_BeforeLeaveRow);
        }

        #endregion
    }
}
