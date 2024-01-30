namespace ICP.FCM.OceanExport.UI.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using System.Windows.Forms;
    using ICP.FCM.Common.ServiceInterface.DataObjects;

    [SmartPart]
    [ToolboxItem(false)]
    public partial class FaxEMailLogListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }
        #endregion

        #region Init

        public FaxEMailLogListPart()
        {
            InitializeComponent();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        { 
            colSubject.Caption = "主题";
            colSender.Caption = "发件人";
            colTo.Caption = "收件人";
            colCreateDate.Caption = "创建时间";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
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
        protected CommonMailFaxLogList CurrentRow
        {
            get { return Current as CommonMailFaxLogList; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        MemoParam _memoParam = null;
        private void BindingData(object value)
        {
            _memoParam = value as MemoParam;
            if (_memoParam == null)
            {
                this.bsList.DataSource = typeof(CommonMailFaxLogList);
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;

                List<CommonMailFaxLogList> MailFaxLogList = fcmCommonService.GetMailFaxLogList(_memoParam.OperationId, _memoParam.OperationType, _memoParam.FormID);
                bsList.DataSource = MailFaxLogList;
                bsList.ResetBindings(false);
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
            CommonMailFaxLogList list = gvMain.GetRow(e.RowHandle) as CommonMailFaxLogList;
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
            //EditFaxEMailLogPart ep = new EditFaxEMailLogPart();
            //ep.DataSource = UIModelHelper.GetNormalObject<CommonMailFaxLogList>();
            //Utility.ShowDialog(ep, "FaxEMailLog");
            ViewAttachment();
        }


        #endregion

        #region barItem Click

        private void barView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //EditFaxEMailLogPart ep = new EditFaxEMailLogPart();
            //ep.DataSource = UIModelHelper.GetNormalObject<CommonMailFaxLogList>();
            //Utility.ShowDialog(ep, "FaxEMailLog");
            ViewAttachment();
        }
        /// <summary>
        /// 查看附件
        /// </summary>
        private void ViewAttachment()
        {
            if (CurrentRow == null || CurrentRow.AttachmentList == null || CurrentRow.AttachmentList.Count <= 0)
                return;


        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bsList.RemoveCurrent();
        }

        #endregion

       
    }
}

