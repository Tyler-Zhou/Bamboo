using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.Common.UI.Geography.Location
{
    public partial class LocationMainListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region Init

        public LocationMainListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.Selected = null;
                this.gvMain.BeforeLeaveRow -= this.gvMain_BeforeLeaveRow;
                this.gvMain.RowStyle -= this.gvMain_RowStyle;
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this.bsList.PositionChanged -= this.bsMainList_PositionChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CommonUtility.ShowGridRowNo(gvMain);
            //if (LocalData.IsEnglish) colName.FieldName = "EName";
            //else colName.FieldName = "CName";
        }

        #endregion

        #region IListPart 成员

        public override  object Current
        {
            get { return bsList.Current; }
        }
        protected LocationList CurrentRow
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
                if (bsList.List.Count > 0)
                {
                    gvMain.Focus();
                    gvMain.SelectRow(0);
                }
            }
        }

        public override void Refresh(object items)
        {
            List<LocationList> list = this.DataSource as List<LocationList>;
            if (list == null) return;
            List<LocationList> newLists = items as List<LocationList>;

            foreach (var item in newLists)
            {
                LocationList tager = list.Find(delegate(LocationList jItem) { return item.ID == item.ID; });
                if (tager == null) continue;

                CommonUtility.CopyToValue(tager, item, typeof(LocationList));
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

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            LocationList list = gvMain.GetRow(e.RowHandle) as LocationList;
            if (list == null) return;

            if (list.IsValid == false)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
        }

        #endregion

        #region Workitem Common


        #endregion
    }
}
