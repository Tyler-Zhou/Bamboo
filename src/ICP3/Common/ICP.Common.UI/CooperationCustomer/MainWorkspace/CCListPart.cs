using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.Common.UI.CC
{
    [ToolboxItem(false)]
    public partial class CCListPart : BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public CCListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this.gvMain.BeforeLeaveRow -= this.gvMain_BeforeLeaveRow;
                this.gvMain.CustomDrawRowIndicator -= this.gvMain_CustomDrawRowIndicator;
                this.gcMain.DataSource = null;
                this.bsList.PositionChanged -= this.bsList_PositionChanged;
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
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected CooperationCustomerList CurrentRow
        {
            get { return Current as CooperationCustomerList; }
            set
            {
                CooperationCustomerList currentRow = CurrentRow;
                currentRow = value;
            }
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

        public override void Refresh(object items)
        {
            List<CooperationCustomerList> list = this.DataSource as List<CooperationCustomerList>;
            if (list == null) return;
            List<CooperationCustomerList> newLists = items as List<CooperationCustomerList>;

            foreach (var item in newLists)
            {
                CooperationCustomerList tager = list.Find(delegate(CooperationCustomerList jItem) { return item.ID == jItem.ID; });
                if (tager == null) continue;

                CommonUtility.CopyToValue(item, tager, typeof(CooperationCustomerList));
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
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
        }

        private void gvMain_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #endregion
        
    }
}
