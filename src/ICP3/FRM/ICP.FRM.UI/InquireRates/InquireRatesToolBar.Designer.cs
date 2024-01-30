namespace ICP.FRM.UI.InquireRates
{
    partial class InquireRatesToolBar
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
            this.barNew = new DevExpress.XtraBars.BarSubItem();
            this.barNewOceanRate = new DevExpress.XtraBars.BarButtonItem();
            this.barNewAirRate = new DevExpress.XtraBars.BarButtonItem();
            this.barNewTruckingRate = new DevExpress.XtraBars.BarButtonItem();
            this.barReInquire = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barMail = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
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
            this.barNew,
            this.barDelete,
            this.barSearch,
            this.barClose,
            this.barCopy,
            this.barReInquire,
            this.barSave,
            this.barRefresh,
            this.barButtonItem1,
            this.barNewOceanRate,
            this.barNewAirRate,
            this.barNewTruckingRate,
            this.barMail,
            this.barButtonItem2});
            this.barManager1.MaxItemId = 17;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barReInquire, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCopy, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSearch, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barMail, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barNew
            // 
            this.barNew.Caption = "New";
            this.barNew.Glyph = global::ICP.FRM.UI.Properties.Resources.Add_16;
            this.barNew.Id = 0;
            this.barNew.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barNewOceanRate),
            new DevExpress.XtraBars.LinkPersistInfo(this.barNewAirRate),
            new DevExpress.XtraBars.LinkPersistInfo(this.barNewTruckingRate)});
            this.barNew.Name = "barNew";
            // 
            // barNewOceanRate
            // 
            this.barNewOceanRate.Caption = "New Inquire Ocean Rate";
            this.barNewOceanRate.Id = 12;
            this.barNewOceanRate.Name = "barNewOceanRate";
            // 
            // barNewAirRate
            // 
            this.barNewAirRate.Caption = "New Inquire Air Rate";
            this.barNewAirRate.Id = 13;
            this.barNewAirRate.Name = "barNewAirRate";
            // 
            // barNewTruckingRate
            // 
            this.barNewTruckingRate.Caption = "New Inquire Trucking Rate";
            this.barNewTruckingRate.Id = 14;
            this.barNewTruckingRate.Name = "barNewTruckingRate";
            // 
            // barReInquire
            // 
            this.barReInquire.Caption = "&Re-Inquire";
            this.barReInquire.Glyph = global::ICP.FRM.UI.Properties.Resources.Input_16;
            this.barReInquire.Id = 8;
            this.barReInquire.Name = "barReInquire";
            // 
            // barDelete
            // 
            this.barDelete.Caption = "&Delete";
            this.barDelete.Glyph = global::ICP.FRM.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            // 
            // barSave
            // 
            this.barSave.Caption = "Sa&ve";
            this.barSave.Glyph = global::ICP.FRM.UI.Properties.Resources.Save_16;
            this.barSave.Id = 9;
            this.barSave.Name = "barSave";
            // 
            // barCopy
            // 
            this.barCopy.Caption = "C&opy";
            this.barCopy.Glyph = global::ICP.FRM.UI.Properties.Resources.Copy_16;
            this.barCopy.Id = 4;
            this.barCopy.Name = "barCopy";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Up-FUEL";
            this.barButtonItem2.Id = 16;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = global::ICP.FRM.UI.Properties.Resources.Refresh_16;
            this.barRefresh.Id = 10;
            this.barRefresh.Name = "barRefresh";
            // 
            // barSearch
            // 
            this.barSearch.Caption = "Searc&h";
            this.barSearch.Glyph = global::ICP.FRM.UI.Properties.Resources.Search_16;
            this.barSearch.Id = 2;
            this.barSearch.Name = "barSearch";
            // 
            // barMail
            // 
            this.barMail.Caption = "Mail";
            this.barMail.Glyph = global::ICP.FRM.UI.Properties.Resources.Edit_16;
            this.barMail.Id = 15;
            this.barMail.Name = "barMail";
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FRM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 3;
            this.barClose.Name = "barClose";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(804, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 26);
            this.barDockControlBottom.Size = new System.Drawing.Size(804, 0);
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
            this.barDockControlRight.Location = new System.Drawing.Point(804, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 0);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 11;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // InquireRatesToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IsMultiLanguage = false;
            this.Name = "InquireRatesToolBar";
            this.Size = new System.Drawing.Size(804, 26);
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
        private DevExpress.XtraBars.BarSubItem barNew;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarButtonItem barSearch;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarButtonItem barCopy;
        private DevExpress.XtraBars.BarButtonItem barReInquire;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barNewOceanRate;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barNewAirRate;
        private DevExpress.XtraBars.BarButtonItem barNewTruckingRate;
        private DevExpress.XtraBars.BarButtonItem barMail;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
    }
}
