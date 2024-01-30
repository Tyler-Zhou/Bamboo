using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.Common.UI.Memo
{
    [ToolboxItem(false)]
    public partial class MemoListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region Init

        public MemoListPart()
        {
            InitializeComponent();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            //colContent.Caption = "内容";
            //colSubject.Caption = "主题";
            //colIsShowAgent.Caption = "显示代理";
            //colIsShowCustomer .Caption = "显示客户";

            //colCreateByName.Caption = "创建人";
            //colCreateDate.Caption = "创建时间";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            List<CommonMemoList> list = new List<CommonMemoList>();
            CommonMemoList data = UIModelHelper.GetNormalObject<CommonMemoList>();
            //data.ID = Guid.NewGuid();
            //data.CreateByName = ICP.Framework.CommonLibrary.Client.LocalData.UserInfo.LoginName;
            //data.CreateDate = DateTime.Now;
            //data.Subject = "Test";
            //data.Content = "New";
            //data.IsShowAgent = false;
            //data.IsShowCustomer = false;
            //data.OwnerSource = BussinessType.OceanExport;
            //data.IsDirty = false;

            list.Add(data);
            bsList.DataSource = list;
            InitControls();
        }

        private void InitControls()
        {
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected CommonMemoList CurrentRow
        {
            get { return Current as CommonMemoList; }
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
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        private void bsMainList_PositionChanged(object sender, EventArgs e)
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

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            CommonMemoList list = gvMain.GetRow(e.RowHandle) as CommonMemoList;
            if (list == null) return;

            if (list.IsDirty || list.IsNew)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
        }

        private void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2) GvMainDoubleClick();
        }

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) GvMainDoubleClick();
        }

        protected virtual void GvMainDoubleClick()
        {
            EditMemoPart ep = new EditMemoPart();
            ep.DataSource = UIModelHelper.GetNormalObject<CommonMemoInfo>();
            Utility.ShowDialog(ep, "Memo");
        }


        #endregion

        #region barItem Click

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditMemoPart ep = new EditMemoPart();
            ep.DataSource = UIModelHelper.GetNormalObject<CommonMemoInfo>();
            Utility.ShowDialog(ep, "Memo");
        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bsList.RemoveCurrent();
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditMemoPart ep = new EditMemoPart();
            ep.DataSource = UIModelHelper.GetNormalObject<CommonMemoInfo>();
            Utility.ShowDialog(ep, "Memo");
        }

        #endregion
    }
}

