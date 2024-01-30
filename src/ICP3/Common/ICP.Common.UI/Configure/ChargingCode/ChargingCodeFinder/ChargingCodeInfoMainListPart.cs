using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Common.UI.Configure.ChargingCode
{
    public partial class ChargingCodeInfoMainListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region Init

        public ChargingCodeInfoMainListPart()
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

            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.DoubleClick += new EventHandler(gvMain_DoubleClick);
            this.gvMain.KeyDown += new KeyEventHandler(gvMain_KeyDown);
            this.Disposed += delegate
            {
                this.Selected = null;
                this.gvMain.DoubleClick -= this.gvMain_DoubleClick;
                this.gvMain.KeyDown -= this.gvMain_KeyDown;
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
        protected ChargingCodeList CurrentRow
        {
            get { return Current as ChargingCodeList; }
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
            List<ChargingCodeList> list = this.DataSource as List<ChargingCodeList>;
            if (list == null) return;
            List<ChargingCodeList> newLists = items as List<ChargingCodeList>;

            foreach (var item in newLists)
            {
                ChargingCodeList tager = list.Find(delegate(ChargingCodeList jItem) { return item.ID == item.ID; });
                if (tager == null) continue;

                CommonUtility.CopyToValue(tager, item, typeof(ChargingCodeList));
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

        void gvMain_DoubleClick(object sender, EventArgs e)
        {
            ConfirmSelected();
            CloseForm();
        }

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConfirmSelected();
                CloseForm();
            }
        }

        private void ConfirmSelected()
        {
            if (CurrentRow == null) return;

            if (Selected != null)
            {
                Selected(this, CurrentRow);
            }
        }

        [CommandHandler(ChargingCodeInfoCommonConstants.Common_FinderConfirm)]
        public void Common_FinderConfirm(object sender, EventArgs e)
        {
            ConfirmSelected();
        }

        private void CloseForm()
        {
            var findForm = this.FindForm();
            if (findForm != null) findForm.Close();
        }
        #endregion

    }
}
