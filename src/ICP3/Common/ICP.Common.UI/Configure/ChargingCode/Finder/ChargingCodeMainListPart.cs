using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.Configure.ChargingCode
{
    public partial class ChargingCodeMainListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

      

        #endregion

        #region Init

        public ChargingCodeMainListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this.Selected = null;
                this.gvMain.BeforeLeaveRow -= this.gvMain_BeforeLeaveRow;
                this.gcMain.DataSource = null;
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
            if (LocalData.IsEnglish)
            {
                this.colEName.Visible = false;
            }
        }

        #endregion

        #region IListPart 成员

        public override  object Current
        {
            get { return bsList.Current; }
        }
        protected SolutionChargingCodeList CurrentRow
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
                if (bsList.List.Count > 0)
                {
                    gvMain.Focus();
                    gvMain.SelectRow(0);
                }
            }
        }

        public override void Refresh(object items)
        {
            List<SolutionChargingCodeList> list = this.DataSource as List<SolutionChargingCodeList>;
            if (list == null) return;
            List<SolutionChargingCodeList> newLists = items as List<SolutionChargingCodeList>;

            foreach (var item in newLists)
            {
                SolutionChargingCodeList tager = list.Find(delegate(SolutionChargingCodeList jItem) { return item.ID == item.ID; });
                if (tager == null) continue;

                CommonUtility.CopyToValue(tager, item, typeof(SolutionChargingCodeList));
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

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            this.gvMain.BeforeLeaveRow -=new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvMain_BeforeLeaveRow);
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvMain_BeforeLeaveRow);
        }


        #endregion

    }
}
