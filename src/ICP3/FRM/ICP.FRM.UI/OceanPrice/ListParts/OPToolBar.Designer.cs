namespace ICP.FRM.UI.OceanPrice
{
    partial class OPToolBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OPToolBar));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barPause = new DevExpress.XtraBars.BarButtonItem();
            this.barInvalidate = new DevExpress.XtraBars.BarButtonItem();
            this.barCompare = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barExportToExcel = new DevExpress.XtraBars.BarButtonItem();
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
            this.barAdd,
            this.barDelete,
            this.barSearch,
            this.barClose,
            this.barCopy,
            this.barPause,
            this.barInvalidate,
            this.barCompare,
            this.barRefresh,
            this.barExportToExcel});
            this.barManager1.MaxItemId = 12;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCopy, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPause, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barInvalidate, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barExportToExcel, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCompare, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSearch, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "&Add";
            this.barAdd.Glyph = ((System.Drawing.Image)(resources.GetObject("barAdd.Glyph")));
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            // 
            // barDelete
            // 
            this.barDelete.Caption = "&Delete";
            this.barDelete.Glyph = ((System.Drawing.Image)(resources.GetObject("barDelete.Glyph")));
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            // 
            // barCopy
            // 
            this.barCopy.Caption = "C&opy";
            this.barCopy.Glyph = ((System.Drawing.Image)(resources.GetObject("barCopy.Glyph")));
            this.barCopy.Id = 4;
            this.barCopy.Name = "barCopy";
            // 
            // barPause
            // 
            this.barPause.Caption = "&Pause";
            this.barPause.Glyph = ((System.Drawing.Image)(resources.GetObject("barPause.Glyph")));
            this.barPause.Id = 5;
            this.barPause.Name = "barPause";
            // 
            // barInvalidate
            // 
            this.barInvalidate.Caption = "In&validate";
            this.barInvalidate.CausesValidation = true;
            this.barInvalidate.Glyph = global::ICP.FRM.UI.Properties.Resources.Input_16;
            this.barInvalidate.Id = 8;
            this.barInvalidate.Name = "barInvalidate";
            // 
            // barCompare
            // 
            this.barCompare.Caption = "Co&mpare";
            this.barCompare.Glyph = global::ICP.FRM.UI.Properties.Resources.Input_16;
            this.barCompare.Id = 9;
            this.barCompare.Name = "barCompare";
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
            this.barSearch.Glyph = ((System.Drawing.Image)(resources.GetObject("barSearch.Glyph")));
            this.barSearch.Id = 2;
            this.barSearch.Name = "barSearch";
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = ((System.Drawing.Image)(resources.GetObject("barClose.Glyph")));
            this.barClose.Id = 3;
            this.barClose.Name = "barClose";
            // 
            // barExportToExcel
            // 
            this.barExportToExcel.Caption = "ExportToExcel";
            this.barExportToExcel.Glyph = global::ICP.FRM.UI.Properties.Resources.Down_16;
            this.barExportToExcel.Id = 11;
            this.barExportToExcel.Name = "barExportToExcel";
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
            // OPToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IsMultiLanguage = false;
            this.Name = "OPToolBar";
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
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarButtonItem barSearch;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarButtonItem barCopy;
        private DevExpress.XtraBars.BarButtonItem barPause;
        private DevExpress.XtraBars.BarButtonItem barInvalidate;
        private DevExpress.XtraBars.BarButtonItem barCompare;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraBars.BarButtonItem barExportToExcel;
    }
}
