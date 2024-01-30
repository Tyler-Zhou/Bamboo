namespace ICP.FCM.OceanImport.UI
{
    partial class OIOrderToolBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OIOrderToolBar));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarSubItem();
            this.barPritnOrder = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barConfirmBooking = new DevExpress.XtraBars.BarButtonItem();
            this.barConfirmBookingShip = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
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
            this.bar1,
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAdd,
            this.barCancel,
            this.barSearch,
            this.barClose,
            this.barCopy,
            this.barEdit,
            this.barRefresh,
            this.barPrint,
            this.barPritnOrder,
            this.barConfirmBooking,
            this.barConfirmBookingShip});
            this.barManager1.MaxItemId = 33;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.KeyTip))), this.barAdd, "", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph, "&ADD", ""),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCopy, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEdit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCancel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSearch, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(this.barConfirmBooking),
            new DevExpress.XtraBars.LinkPersistInfo(this.barConfirmBookingShip),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // barAdd
            // 
            resources.ApplyResources(this.barAdd, "barAdd");
            this.barAdd.Glyph = ((System.Drawing.Image)(resources.GetObject("barAdd.Glyph")));
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            // 
            // barCopy
            // 
            resources.ApplyResources(this.barCopy, "barCopy");
            this.barCopy.Glyph = ((System.Drawing.Image)(resources.GetObject("barCopy.Glyph")));
            this.barCopy.Id = 4;
            this.barCopy.Name = "barCopy";
            // 
            // barEdit
            // 
            resources.ApplyResources(this.barEdit, "barEdit");
            this.barEdit.Id = 23;
            this.barEdit.Name = "barEdit";
            // 
            // barCancel
            // 
            resources.ApplyResources(this.barCancel, "barCancel");
            this.barCancel.Id = 1;
            this.barCancel.Name = "barCancel";
            // 
            // barPrint
            // 
            resources.ApplyResources(this.barPrint, "barPrint");
            this.barPrint.Id = 29;
            this.barPrint.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barPritnOrder)});
            this.barPrint.Name = "barPrint";
            // 
            // barPritnOrder
            // 
            resources.ApplyResources(this.barPritnOrder, "barPritnOrder");
            this.barPritnOrder.Id = 30;
            this.barPritnOrder.Name = "barPritnOrder";
            // 
            // barRefresh
            // 
            resources.ApplyResources(this.barRefresh, "barRefresh");
            this.barRefresh.Id = 25;
            this.barRefresh.Name = "barRefresh";
            // 
            // barSearch
            // 
            resources.ApplyResources(this.barSearch, "barSearch");
            this.barSearch.Glyph = ((System.Drawing.Image)(resources.GetObject("barSearch.Glyph")));
            this.barSearch.Id = 2;
            this.barSearch.Name = "barSearch";
            // 
            // barConfirmBooking
            // 
            resources.ApplyResources(this.barConfirmBooking, "barConfirmBooking");
            this.barConfirmBooking.Id = 31;
            this.barConfirmBooking.Name = "barConfirmBooking";
            this.barConfirmBooking.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barConfirmBookingShip
            // 
            resources.ApplyResources(this.barConfirmBookingShip, "barConfirmBookingShip");
            this.barConfirmBookingShip.Id = 32;
            this.barConfirmBookingShip.Name = "barConfirmBookingShip";
            this.barConfirmBookingShip.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barClose
            // 
            resources.ApplyResources(this.barClose, "barClose");
            this.barClose.Glyph = ((System.Drawing.Image)(resources.GetObject("barClose.Glyph")));
            this.barClose.Id = 3;
            this.barClose.Name = "barClose";
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 3";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 1;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            resources.ApplyResources(this.bar2, "bar2");
            // 
            // barDockControlTop
            // 
            resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
            // 
            // barDockControlBottom
            // 
            resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            // 
            // barDockControlLeft
            // 
            resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            // 
            // barDockControlRight
            // 
            resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
            // 
            // barButtonItem1
            // 
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 24;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // OIOrderToolBar
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "OIOrderToolBar";
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
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barCancel;
        private DevExpress.XtraBars.BarButtonItem barSearch;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarButtonItem barCopy;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarSubItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barPritnOrder;
        private DevExpress.XtraBars.BarButtonItem barConfirmBooking;
        private DevExpress.XtraBars.BarButtonItem barConfirmBookingShip;
    }
}
