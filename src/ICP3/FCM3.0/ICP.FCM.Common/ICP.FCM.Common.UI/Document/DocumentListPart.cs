using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.Common.UI.Document
{
    [ToolboxItem(false)]
    public partial class DocumentListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region Init

        public DocumentListPart()
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

            List<CommonDocumentList> list = new List<CommonDocumentList>();
            CommonDocumentList data = UIModelHelper.GetNormalObject<CommonDocumentList>();
            list.Add(data);
            bsList.DataSource = list;
            InitControls();
        }

        private void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DocumentType>> memoTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DocumentType>(LocalData.IsEnglish);
            foreach (var item in memoTypes)
            {
                rcmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected CommonDocumentList CurrentRow
        {
            get { return Current as CommonDocumentList; }
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
            CommonDocumentList list = gvMain.GetRow(e.RowHandle) as CommonDocumentList;
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
            EditDocumentPart ep = new EditDocumentPart();
            ep.DataSource = UIModelHelper.GetNormalObject<CommonMemoInfo>();
            Utility.ShowDialog(ep, "Memo");
        }


        #endregion

        #region barItem Click

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditDocumentPart ep = new EditDocumentPart();
            CommonMemoInfo info = UIModelHelper.GetNormalObject<CommonMemoInfo>();
            ep.DataSource = info;
            Utility.ShowDialog(ep, "Document");
        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bsList.RemoveCurrent();
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditDocumentPart ep = new EditDocumentPart();
            ep.DataSource = UIModelHelper.GetNormalObject<CommonMemoInfo>();
            Utility.ShowDialog(ep, "Document");
        }

        #endregion
    }
}
