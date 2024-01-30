namespace ICP.FAM.UI.Business
{
    partial class BusinessToolBar
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barViewBusinessInfo = new DevExpress.XtraBars.BarButtonItem();
            this.barBill = new DevExpress.XtraBars.BarButtonItem();
            this.barBatchAddBill = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barShowSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barSelectAll = new DevExpress.XtraBars.BarButtonItem();
            this.barShowMemo = new DevExpress.XtraBars.BarButtonItem();
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
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barViewBusinessInfo,
            this.barBill,
            this.barClose,
            this.barBatchAddBill,
            this.barRefresh,
            this.barShowSearch,
            this.barSelectAll,
            this.barShowMemo});
            this.barManager1.MaxItemId = 9;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barViewBusinessInfo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBill, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBatchAddBill, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(this.barShowSearch),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSelectAll, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barShowMemo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barViewBusinessInfo
            // 
            this.barViewBusinessInfo.Caption = "&ViewBusiness";
            this.barViewBusinessInfo.Glyph = global::ICP.FAM.UI.Properties.Resources.Edit_16;
            this.barViewBusinessInfo.Id = 0;
            this.barViewBusinessInfo.Name = "barViewBusinessInfo";
            // 
            // barBill
            // 
            this.barBill.Caption = "&Bill";
            this.barBill.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barBill.Id = 1;
            this.barBill.Name = "barBill";
            // 
            // barBatchAddBill
            // 
            this.barBatchAddBill.Caption = "BatchAddB&ill";
            this.barBatchAddBill.Enabled = false;
            this.barBatchAddBill.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barBatchAddBill.Id = 3;
            this.barBatchAddBill.Name = "barBatchAddBill";
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = global::ICP.FAM.UI.Properties.Resources.Refresh_16;
            this.barRefresh.Hint = "&Refresh";
            this.barRefresh.Id = 4;
            this.barRefresh.Name = "barRefresh";
            // 
            // barShowSearch
            // 
            this.barShowSearch.Caption = "ShowSearc&h";
            this.barShowSearch.Glyph = global::ICP.FAM.UI.Properties.Resources.Sarch_16;
            this.barShowSearch.Hint = "ShowSearc&h";
            this.barShowSearch.Id = 5;
            this.barShowSearch.Name = "barShowSearch";
            // 
            // barSelectAll
            // 
            this.barSelectAll.Caption = "Select&All";
            this.barSelectAll.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.barSelectAll.Id = 6;
            this.barSelectAll.Name = "barSelectAll";
            // 
            // barShowMemo
            // 
            this.barShowMemo.Caption = "Show Memo";
            this.barShowMemo.Glyph = global::ICP.FAM.UI.Properties.Resources.BrightFinder;
            this.barShowMemo.Id = 7;
            this.barShowMemo.Name = "barShowMemo";
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 2;
            this.barClose.Name = "barClose";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(829, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 26);
            this.barDockControlBottom.Size = new System.Drawing.Size(829, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(829, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 0);
            // 
            // BusinessToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BusinessToolBar";
            this.Size = new System.Drawing.Size(829, 26);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barViewBusinessInfo;
        private DevExpress.XtraBars.BarButtonItem barBill;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarButtonItem barBatchAddBill;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraBars.BarButtonItem barShowSearch;
        private DevExpress.XtraBars.BarButtonItem barSelectAll;
        private DevExpress.XtraBars.BarButtonItem barShowMemo;
    }
}
