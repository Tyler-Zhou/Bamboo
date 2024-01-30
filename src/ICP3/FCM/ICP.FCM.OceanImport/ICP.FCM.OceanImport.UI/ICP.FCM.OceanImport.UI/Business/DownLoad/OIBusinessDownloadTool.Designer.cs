namespace ICP.FCM.OceanImport.UI
{
    partial class OIBusinessDownloadTool
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
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barDownLoad = new DevExpress.XtraBars.BarButtonItem();
            this.toolAccepted = new DevExpress.XtraBars.BarButtonItem();
            this.toolUnAccepted = new DevExpress.XtraBars.BarButtonItem();
            this.toolTransition = new DevExpress.XtraBars.BarButtonItem();
            this.toolAssignTo = new DevExpress.XtraBars.BarButtonItem();
            this.toolPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDownLoadNew = new DevExpress.XtraBars.BarButtonItem();
            this.barAcceptedNew = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.toolTestAccepted = new DevExpress.XtraBars.BarButtonItem();
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
            this.barRefresh,
            this.barSearch,
            this.barDownLoad,
            this.barClose,
            this.toolUnAccepted,
            this.toolTransition,
            this.toolAssignTo,
            this.toolPrint,
            this.toolAccepted,
            this.barButtonItem1,
            this.toolTestAccepted,
            this.barDownLoadNew,
            this.barAcceptedNew});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 14;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSearch, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDownLoad, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolAccepted, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolUnAccepted, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolTransition, "", false, true, false, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolAssignTo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDownLoadNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAcceptedNew)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "刷新(&R)";
            this.barRefresh.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Refresh_161;
            this.barRefresh.Id = 0;
            this.barRefresh.Name = "barRefresh";
            // 
            // barSearch
            // 
            this.barSearch.Caption = "查询(&H)";
            this.barSearch.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Search_16;
            this.barSearch.Id = 1;
            this.barSearch.Name = "barSearch";
            // 
            // barDownLoad
            // 
            this.barDownLoad.Caption = "下载(&D)";
            this.barDownLoad.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Down_16;
            this.barDownLoad.Id = 2;
            this.barDownLoad.Name = "barDownLoad";
            // 
            // toolAccepted
            // 
            this.toolAccepted.Caption = "签收";
            this.toolAccepted.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Transfer_16;
            this.toolAccepted.Id = 9;
            this.toolAccepted.Name = "toolAccepted";
            // 
            // toolUnAccepted
            // 
            this.toolUnAccepted.Caption = "取消签收(&U)";
            this.toolUnAccepted.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Transfer_16;
            this.toolUnAccepted.Id = 5;
            this.toolUnAccepted.Name = "toolUnAccepted";
            this.toolUnAccepted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // toolTransition
            // 
            this.toolTransition.Caption = "移交给";
            this.toolTransition.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Transfer_16;
            this.toolTransition.Id = 6;
            this.toolTransition.Name = "toolTransition";
            // 
            // toolAssignTo
            // 
            this.toolAssignTo.Caption = "指派给(&T)";
            this.toolAssignTo.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Transfer_16;
            this.toolAssignTo.Id = 7;
            this.toolAssignTo.Name = "toolAssignTo";
            // 
            // toolPrint
            // 
            this.toolPrint.Caption = "打印";
            this.toolPrint.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Print_16;
            this.toolPrint.Id = 8;
            this.toolPrint.Name = "toolPrint";
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭(&C)";
            this.barClose.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Left_D_16;
            this.barClose.Id = 3;
            this.barClose.Name = "barClose";
            // 
            // barDownLoadNew
            // 
            this.barDownLoadNew.Caption = "下载";
            this.barDownLoadNew.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Down_16;
            this.barDownLoadNew.Id = 12;
            this.barDownLoadNew.Name = "barDownLoadNew";
            this.barDownLoadNew.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barDownLoadNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barAcceptedNew
            // 
            this.barAcceptedNew.Caption = "签收";
            this.barAcceptedNew.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Transfer_16;
            this.barAcceptedNew.Id = 13;
            this.barAcceptedNew.Name = "barAcceptedNew";
            this.barAcceptedNew.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barAcceptedNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(981, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 26);
            this.barDockControlBottom.Size = new System.Drawing.Size(981, 0);
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
            this.barDockControlRight.Location = new System.Drawing.Point(981, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 0);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "TestAccept";
            this.barButtonItem1.Id = 10;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // toolTestAccepted
            // 
            this.toolTestAccepted.Caption = "TestAccepted";
            this.toolTestAccepted.Id = 11;
            this.toolTestAccepted.Name = "toolTestAccepted";
            this.toolTestAccepted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // OIBusinessDownloadTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "OIBusinessDownloadTool";
            this.Size = new System.Drawing.Size(981, 26);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraBars.BarButtonItem barSearch;
        private DevExpress.XtraBars.BarButtonItem barClose;
        public DevExpress.XtraBars.BarButtonItem toolUnAccepted;
        public DevExpress.XtraBars.BarButtonItem toolTransition;
        public DevExpress.XtraBars.BarButtonItem toolAssignTo;
        private DevExpress.XtraBars.BarButtonItem toolPrint;
        public DevExpress.XtraBars.BarButtonItem toolAccepted;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem toolTestAccepted;
        public DevExpress.XtraBars.BarButtonItem barDownLoad;
        private DevExpress.XtraBars.BarButtonItem barDownLoadNew;
        private DevExpress.XtraBars.BarButtonItem barAcceptedNew;
    }
}
