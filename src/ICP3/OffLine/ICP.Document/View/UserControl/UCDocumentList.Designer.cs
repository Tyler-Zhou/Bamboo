namespace ICP.Document
{
    partial class UCDocumentList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDocumentList));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barItemOpen = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItemOpenWith = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItemPreview = new DevExpress.XtraBars.BarLargeButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.xggcDocument = new DevExpress.XtraGrid.GridControl();
            this.bdscDocument = new System.Windows.Forms.BindingSource(this.components);
            this.gdvwDocument = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xggcDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdscDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvwDocument)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.Images.SetKeyName(0, "delete_16.png");
            this.imageList.Images.SetKeyName(1, "open.png");
            this.imageList.Images.SetKeyName(2, "openwith.png");
            this.imageList.Images.SetKeyName(3, "preview.png");
            this.imageList.Images.SetKeyName(4, "sync.png");
            this.imageList.Images.SetKeyName(5, "upload.png");
            this.imageList.Images.SetKeyName(6, "Down_16.png");
            this.imageList.Images.SetKeyName(7, "docOpen.png");
            this.imageList.Images.SetKeyName(8, "docPreview.png");
            this.imageList.Images.SetKeyName(9, "docWith.png");
            this.imageList.Images.SetKeyName(10, "open2.png");
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barItemOpen,
            this.barItemOpenWith,
            this.barItemPreview});
            this.barManager1.LargeImages = this.imageList;
            this.barManager1.MaxItemId = 4;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barItemOpen, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barItemOpenWith, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barItemPreview, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barItemOpen
            // 
            this.barItemOpen.Caption = "&Open";
            this.barItemOpen.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemOpen.Id = 1;
            this.barItemOpen.LargeImageIndex = 1;
            this.barItemOpen.Name = "barItemOpen";
            // 
            // barItemOpenWith
            // 
            this.barItemOpenWith.Caption = "Open &With";
            this.barItemOpenWith.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemOpenWith.Id = 2;
            this.barItemOpenWith.LargeImageIndex = 2;
            this.barItemOpenWith.Name = "barItemOpenWith";
            // 
            // barItemPreview
            // 
            this.barItemPreview.Caption = "&Preview";
            this.barItemPreview.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemPreview.Id = 3;
            this.barItemPreview.LargeImageIndex = 3;
            this.barItemPreview.Name = "barItemPreview";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(482, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 303);
            this.barDockControlBottom.Size = new System.Drawing.Size(482, 22);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 277);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(482, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 277);
            // 
            // xggcDocument
            // 
            this.xggcDocument.DataSource = this.bdscDocument;
            this.xggcDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xggcDocument.Location = new System.Drawing.Point(0, 26);
            this.xggcDocument.MainView = this.gdvwDocument;
            this.xggcDocument.MenuManager = this.barManager1;
            this.xggcDocument.Name = "xggcDocument";
            this.xggcDocument.Size = new System.Drawing.Size(482, 277);
            this.xggcDocument.TabIndex = 4;
            this.xggcDocument.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gdvwDocument});
            // 
            // gdvwDocument
            // 
            this.gdvwDocument.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumnDType,
            this.gridColumn2});
            this.gdvwDocument.GridControl = this.xggcDocument;
            this.gdvwDocument.Name = "gdvwDocument";
            this.gdvwDocument.OptionsBehavior.Editable = false;
            this.gdvwDocument.OptionsView.ColumnAutoWidth = false;
            this.gdvwDocument.OptionsView.ShowDetailButtons = false;
            this.gdvwDocument.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "ID";
            this.gridColumn3.FieldName = "ID";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumnDType
            // 
            this.gridColumnDType.Caption = "Type";
            this.gridColumnDType.FieldName = "DType";
            this.gridColumnDType.Name = "gridColumnDType";
            this.gridColumnDType.Visible = true;
            this.gridColumnDType.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Name";
            this.gridColumn2.FieldName = "DName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 200;
            // 
            // UCDocumentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xggcDocument);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCDocumentList";
            this.Size = new System.Drawing.Size(482, 325);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xggcDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdscDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvwDocument)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl xggcDocument;
        private DevExpress.XtraGrid.Views.Grid.GridView gdvwDocument;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraBars.BarLargeButtonItem barItemOpen;
        private DevExpress.XtraBars.BarLargeButtonItem barItemOpenWith;
        private DevExpress.XtraBars.BarLargeButtonItem barItemPreview;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        public System.Windows.Forms.BindingSource bdscDocument;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
