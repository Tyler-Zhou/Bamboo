namespace ICP.FCM.OtherBusiness.UI.Common
{
    partial class OBMainToolBar
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        public System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();


            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OBMainToolBar));
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDownload = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarSubItem();
            this.barOpContact = new DevExpress.XtraBars.BarButtonItem();
            this.barProfit = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barRemark = new DevExpress.XtraBars.BarButtonItem();
            this.barBill = new DevExpress.XtraBars.BarButtonItem();
            this.barVerifiSheet = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barPickUp = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barFaxLog = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Refresh";
            this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
            this.barButtonItem1.Id = 24;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAdd,
            this.barDownload,
            this.barEdit,
            this.barButtonItem5,
            this.barSearch,
            this.barPrint,
            this.barOpContact,
            this.barProfit,
            this.barRemark,
            this.barFaxLog,
            this.barClose,
            this.barRefresh,
            this.barCancel,
            this.barBill,
            this.barVerifiSheet,
            this.barCopy,
            this.barPickUp});
            this.barManager1.MaxItemId = 20;
            // 
            // bar2
            // 
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDownload, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEdit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCopy, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCancel, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSearch, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRemark, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBill, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barVerifiSheet, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPickUp, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Tools";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "Add";
            this.barAdd.Glyph = ((System.Drawing.Image)(resources.GetObject("barAdd.Glyph")));
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            // 
            // barDownload
            // 
            this.barDownload.Caption = "Download";
            this.barDownload.Id = 1;
            this.barDownload.Name = "barDownload";
            this.barDownload.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barEdit
            // 
            this.barEdit.Caption = "Edit";
            this.barEdit.Glyph = ((System.Drawing.Image)(resources.GetObject("barEdit.Glyph")));
            this.barEdit.Id = 2;
            this.barEdit.Name = "barEdit";
            // 
            // barCopy
            // 
            this.barCopy.Caption = "Copy";
            this.barCopy.Glyph = global::ICP.FCM.OtherBusiness.UI.Properties.Resources.Copy_16;
            this.barCopy.Id = 18;
            this.barCopy.Name = "barCopy";
            // 
            // barCancel
            // 
            this.barCancel.Caption = "Cancel";
            this.barCancel.Glyph = ((System.Drawing.Image)(resources.GetObject("barCancel.Glyph")));
            this.barCancel.Id = 15;
            this.barCancel.Name = "barCancel";
            // 
            // barSearch
            // 
            this.barSearch.Caption = "Search";
            this.barSearch.Glyph = ((System.Drawing.Image)(resources.GetObject("barSearch.Glyph")));
            this.barSearch.Id = 4;
            this.barSearch.Name = "barSearch";
            // 
            // barPrint
            // 
            this.barPrint.Caption = "Print";
            this.barPrint.Glyph = ((System.Drawing.Image)(resources.GetObject("barPrint.Glyph")));
            this.barPrint.Id = 5;
            this.barPrint.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barOpContact, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barProfit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barPrint.Name = "barPrint";
            // 
            // barOpContact
            // 
            this.barOpContact.Caption = "Operation Contact";
            this.barOpContact.Id = 24;
            this.barOpContact.Name = "barOpContact";
            // 
            // barProfit
            // 
            this.barProfit.Caption = "Profit";
            this.barProfit.Id = 23;
            this.barProfit.Name = "barProfit";
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "Refresh";
            this.barRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("barRefresh.Glyph")));
            this.barRefresh.Id = 14;
            this.barRefresh.Name = "barRefresh";
            // 
            // barRemark
            // 
            this.barRemark.Caption = "Remark";
            this.barRemark.Glyph = ((System.Drawing.Image)(resources.GetObject("barRemark.Glyph")));
            this.barRemark.Id = 6;
            this.barRemark.Name = "barRemark";
            this.barRemark.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barBill
            // 
            this.barBill.Caption = "ViewBill";
            this.barBill.Glyph = ((System.Drawing.Image)(resources.GetObject("barBill.Glyph")));
            this.barBill.Id = 16;
            this.barBill.Name = "barBill";
            // 
            // barVerifiSheet
            // 
            this.barVerifiSheet.Caption = "&VerifiSheet";
            this.barVerifiSheet.Glyph = ((System.Drawing.Image)(resources.GetObject("barVerifiSheet.Glyph")));
            this.barVerifiSheet.Id = 17;
            this.barVerifiSheet.Name = "barVerifiSheet";
            // 
            // barClose
            // 
            this.barClose.Caption = "Close";
            this.barClose.Glyph = ((System.Drawing.Image)(resources.GetObject("barClose.Glyph")));
            this.barClose.Id = 13;
            this.barClose.Name = "barClose";
            // 
            // barPickUp
            // 
            this.barPickUp.Caption = "&PickUp";
            this.barPickUp.Glyph = global::ICP.FCM.OtherBusiness.UI.Properties.Resources.Transfer_16;
            this.barPickUp.Id = 19;
            this.barPickUp.Name = "barPickUp";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(958, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 38);
            this.barDockControlBottom.Size = new System.Drawing.Size(958, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 12);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(958, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 12);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "Cancel";
            this.barButtonItem5.Id = 3;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barFaxLog
            // 
            this.barFaxLog.Caption = "FaxLog";
            this.barFaxLog.Id = 7;
            this.barFaxLog.Name = "barFaxLog";
            this.barFaxLog.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // OBMainToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "OBMainToolBar";
            this.Size = new System.Drawing.Size(958, 38);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraBars.BarButtonItem barButtonItem1;
        public DevExpress.XtraBars.Bar bar1;
        public DevExpress.XtraBars.BarManager barManager1;
        public DevExpress.XtraBars.Bar bar2;
        public DevExpress.XtraBars.BarDockControl barDockControlTop;
        public DevExpress.XtraBars.BarDockControl barDockControlBottom;
        public DevExpress.XtraBars.BarDockControl barDockControlLeft;
        public DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.BarButtonItem barAdd;
        public DevExpress.XtraBars.BarButtonItem barDownload;
        public DevExpress.XtraBars.BarButtonItem barEdit;
        public DevExpress.XtraBars.BarButtonItem barButtonItem5;
        public DevExpress.XtraBars.BarButtonItem barSearch;
        public DevExpress.XtraBars.BarSubItem barPrint;
        public DevExpress.XtraBars.BarButtonItem barOpContact;
        public DevExpress.XtraBars.BarButtonItem barProfit;
        public DevExpress.XtraBars.BarButtonItem barRemark;
        public DevExpress.XtraBars.BarButtonItem barFaxLog;
        public DevExpress.XtraBars.BarButtonItem barClose;
        public DevExpress.XtraBars.BarButtonItem barRefresh;
        public DevExpress.XtraBars.BarButtonItem barCancel;
        public DevExpress.XtraBars.BarButtonItem barBill;
        public DevExpress.XtraBars.BarButtonItem barVerifiSheet;
        public DevExpress.XtraBars.BarButtonItem barCopy;
        public DevExpress.XtraBars.BarButtonItem barPickUp;
    }
}
