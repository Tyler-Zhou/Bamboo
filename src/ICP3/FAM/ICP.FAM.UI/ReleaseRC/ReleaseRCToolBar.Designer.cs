namespace ICP.FAM.UI.ReleaseRC
{
    partial class ReleaseRCToolBar
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReleaseRCToolBar));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barViewBusinessInfo = new DevExpress.XtraBars.BarButtonItem();
            this.barBill = new DevExpress.XtraBars.BarButtonItem();
            this.barReceived = new DevExpress.XtraBars.BarButtonItem();
            this.barApply = new DevExpress.XtraBars.BarButtonItem();
            this.barExceptionReleaseRC = new DevExpress.XtraBars.BarButtonItem();
            this.barTransit = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barShowSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
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
            this.barTransit,
            this.barClose,
            this.barEdit,
            this.barReceived,
            this.barRefresh,
            this.barShowSearch,
            this.barApply,
            this.barViewBusinessInfo,
            this.barBill,
            this.barExceptionReleaseRC});
            this.barManager1.MaxItemId = 30;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barViewBusinessInfo, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBill, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barReceived, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barApply, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barTransit, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barExceptionReleaseRC, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barRefresh, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barShowSearch),
            new DevExpress.XtraBars.LinkPersistInfo(this.barClose, true)});
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barEdit
            // 
            this.barEdit.Caption = "&Edit";
            this.barEdit.Glyph = global::ICP.FAM.UI.Properties.Resources.Edit_16;
            this.barEdit.Id = 17;
            this.barEdit.Name = "barEdit";
            this.barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barViewBusinessInfo
            // 
            this.barViewBusinessInfo.Caption = "&ViewBusiness";
            this.barViewBusinessInfo.Glyph = global::ICP.FAM.UI.Properties.Resources.BillDetails;
            this.barViewBusinessInfo.Id = 26;
            this.barViewBusinessInfo.Name = "barViewBusinessInfo";
            // 
            // barBill
            // 
            this.barBill.Caption = "&Bill";
            this.barBill.Glyph = global::ICP.FAM.UI.Properties.Resources.BillDetails;
            this.barBill.Id = 27;
            this.barBill.Name = "barBill";
            // 
            // barReceived
            // 
            this.barReceived.Caption = "ReceiveBLN&otice";
            this.barReceived.Glyph = global::ICP.FAM.UI.Properties.Resources.CancelBullion;
            this.barReceived.Id = 19;
            this.barReceived.Name = "barReceived";
            // 
            // barApply
            // 
            this.barApply.Caption = "R&emark";
            this.barApply.Glyph = global::ICP.FAM.UI.Properties.Resources.Inform;
            this.barApply.Id = 24;
            this.barApply.Name = "barApply";
            // 
            // barExceptionReleaseRC
            // 
            this.barExceptionReleaseRC.Caption = "异常放货申请";
            this.barExceptionReleaseRC.Glyph = global::ICP.FAM.UI.Properties.Resources.Telex;
            this.barExceptionReleaseRC.Id = 29;
            this.barExceptionReleaseRC.Name = "barExceptionReleaseRC";
            // 
            // barTransit
            // 
            this.barTransit.Caption = "Tran&sit";
            this.barTransit.Glyph = global::ICP.FAM.UI.Properties.Resources.Telex;
            this.barTransit.Id = 1;
            this.barTransit.Name = "barTransit";
            this.barTransit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barTransit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = global::ICP.FAM.UI.Properties.Resources.Refresh_16;
            this.barRefresh.Hint = "&Refresh";
            this.barRefresh.Id = 21;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barShowSearch
            // 
            this.barShowSearch.Caption = "ShowSearc&h";
            this.barShowSearch.Glyph = global::ICP.FAM.UI.Properties.Resources.Sarch_16;
            this.barShowSearch.Hint = "ShowSearc&h";
            this.barShowSearch.Id = 23;
            this.barShowSearch.Name = "barShowSearch";
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 6;
            this.barClose.Name = "barClose";
            this.barClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(955, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 27);
            this.barDockControlBottom.Size = new System.Drawing.Size(955, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 1);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(955, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 1);
            // 
            // ReleaseRCToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ReleaseRCToolBar";
            this.Size = new System.Drawing.Size(955, 27);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barTransit;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        private DevExpress.XtraBars.BarButtonItem barReceived;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraBars.BarButtonItem barShowSearch;
        private DevExpress.XtraBars.BarButtonItem barApply;
        private DevExpress.XtraBars.BarButtonItem barViewBusinessInfo;
        private DevExpress.XtraBars.BarButtonItem barBill;
        private DevExpress.XtraBars.BarButtonItem barExceptionReleaseRC;
    }
}
