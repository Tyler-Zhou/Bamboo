namespace ICP.FAM.UI.ReleaseBL
{
    partial class ReleaseBLToolBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReleaseBLToolBar));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barViewBusinessInfo = new DevExpress.XtraBars.BarButtonItem();
            this.barBill = new DevExpress.XtraBars.BarButtonItem();
            this.barChangeToOriginal = new DevExpress.XtraBars.BarButtonItem();
            this.barChangeToTelex = new DevExpress.XtraBars.BarButtonItem();
            this.barReceived = new DevExpress.XtraBars.BarButtonItem();
            this.barApply = new DevExpress.XtraBars.BarButtonItem();
            this.barCancelApply = new DevExpress.XtraBars.BarButtonItem();
            this.barReleaseBL = new DevExpress.XtraBars.BarButtonItem();
            this.barCancelReleaseBL = new DevExpress.XtraBars.BarButtonItem();
            this.barReceviedOBL = new DevExpress.XtraBars.BarSubItem();
            this.barBtRecevied = new DevExpress.XtraBars.BarButtonItem();
            this.barBtNotRecevied = new DevExpress.XtraBars.BarButtonItem();
            this.barVisible = new DevExpress.XtraBars.BarSubItem();
            this.barVisibleAll = new DevExpress.XtraBars.BarCheckItem();
            this.barVisibleMBL = new DevExpress.XtraBars.BarCheckItem();
            this.barVisibleHBL = new DevExpress.XtraBars.BarCheckItem();
            this.barPressMoney = new DevExpress.XtraBars.BarButtonItem();
            this.barSetArEmail = new DevExpress.XtraBars.BarButtonItem();
            this.barExceptionCus = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barShowSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barPrintBL = new DevExpress.XtraBars.BarButtonItem();
            this.barExportToExcel = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barExRelease = new DevExpress.XtraBars.BarButtonItem();
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
            this.barReleaseBL,
            this.barCancelReleaseBL,
            this.barVisible,
            this.barVisibleAll,
            this.barVisibleHBL,
            this.barVisibleMBL,
            this.barClose,
            this.barChangeToOriginal,
            this.barChangeToTelex,
            this.barEdit,
            this.barReceived,
            this.barRefresh,
            this.barShowSearch,
            this.barApply,
            this.barCancelApply,
            this.barViewBusinessInfo,
            this.barBill,
            this.barSubItem1,
            this.barPrintBL,
            this.barExportToExcel,
            this.barReceviedOBL,
            this.barBtRecevied,
            this.barBtNotRecevied,
            this.barExceptionCus,
            this.barPressMoney,
            this.barSetArEmail,
            this.barExRelease});
            this.barManager1.MaxItemId = 111;
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBill, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barChangeToOriginal),
            new DevExpress.XtraBars.LinkPersistInfo(this.barChangeToTelex),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barReceived, "", true, true, false, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barApply, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCancelApply, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barReleaseBL),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCancelReleaseBL),
            new DevExpress.XtraBars.LinkPersistInfo(this.barReceviedOBL),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barVisible, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPressMoney),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSetArEmail),
            new DevExpress.XtraBars.LinkPersistInfo(this.barExRelease),
            new DevExpress.XtraBars.LinkPersistInfo(this.barExceptionCus),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barShowSearch),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barExportToExcel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
            // barChangeToOriginal
            // 
            this.barChangeToOriginal.Caption = "Change2Original";
            this.barChangeToOriginal.Glyph = global::ICP.FAM.UI.Properties.Resources.ChangeReleaseType;
            this.barChangeToOriginal.Id = 14;
            this.barChangeToOriginal.Name = "barChangeToOriginal";
            this.barChangeToOriginal.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barChangeToTelex
            // 
            this.barChangeToTelex.Caption = "Change2Telex";
            this.barChangeToTelex.Glyph = global::ICP.FAM.UI.Properties.Resources.ChangeReleaseType;
            this.barChangeToTelex.Id = 14;
            this.barChangeToTelex.Name = "barChangeToTelex";
            this.barChangeToTelex.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barReceived
            // 
            this.barReceived.Caption = "Receive&d";
            this.barReceived.Glyph = global::ICP.FAM.UI.Properties.Resources.Down_16;
            this.barReceived.Id = 19;
            this.barReceived.Name = "barReceived";
            // 
            // barApply
            // 
            this.barApply.Caption = "A&pply";
            this.barApply.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barApply.Id = 24;
            this.barApply.Name = "barApply";
            // 
            // barCancelApply
            // 
            this.barCancelApply.Caption = "Cancel A&pply";
            this.barCancelApply.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barCancelApply.Id = 24;
            this.barCancelApply.Name = "barCancelApply";
            // 
            // barReleaseBL
            // 
            this.barReleaseBL.Caption = "Release&BL";
            this.barReleaseBL.Glyph = global::ICP.FAM.UI.Properties.Resources.Telex;
            this.barReleaseBL.Id = 1;
            this.barReleaseBL.Name = "barReleaseBL";
            this.barReleaseBL.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barCancelReleaseBL
            // 
            this.barCancelReleaseBL.Caption = "Cancel Release&BL";
            this.barCancelReleaseBL.Glyph = global::ICP.FAM.UI.Properties.Resources.Telex;
            this.barCancelReleaseBL.Id = 1;
            this.barCancelReleaseBL.Name = "barCancelReleaseBL";
            this.barCancelReleaseBL.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barReceviedOBL
            // 
            this.barReceviedOBL.Caption = "ReceivedOBL";
            this.barReceviedOBL.Glyph = global::ICP.FAM.UI.Properties.Resources.Telex;
            this.barReceviedOBL.Id = 104;
            this.barReceviedOBL.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtRecevied),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtNotRecevied)});
            this.barReceviedOBL.Name = "barReceviedOBL";
            this.barReceviedOBL.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barBtRecevied
            // 
            this.barBtRecevied.Caption = "Recevied";
            this.barBtRecevied.Id = 105;
            this.barBtRecevied.Name = "barBtRecevied";
            // 
            // barBtNotRecevied
            // 
            this.barBtNotRecevied.Caption = "Cancel Recevied";
            this.barBtNotRecevied.Id = 106;
            this.barBtNotRecevied.Name = "barBtNotRecevied";
            // 
            // barVisible
            // 
            this.barVisible.Caption = "ALL";
            this.barVisible.Id = 40;
            this.barVisible.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barVisibleAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.barVisibleMBL),
            new DevExpress.XtraBars.LinkPersistInfo(this.barVisibleHBL)});
            this.barVisible.Name = "barVisible";
            // 
            // barVisibleAll
            // 
            this.barVisibleAll.Caption = "ALL";
            this.barVisibleAll.Checked = true;
            this.barVisibleAll.Id = 41;
            this.barVisibleAll.Name = "barVisibleAll";
            // 
            // barVisibleMBL
            // 
            this.barVisibleMBL.Caption = "MBL";
            this.barVisibleMBL.Id = 42;
            this.barVisibleMBL.Name = "barVisibleMBL";
            // 
            // barVisibleHBL
            // 
            this.barVisibleHBL.Caption = "HBL";
            this.barVisibleHBL.Id = 43;
            this.barVisibleHBL.Name = "barVisibleHBL";
            // 
            // barPressMoney
            // 
            this.barPressMoney.Caption = "PressMoney";
            this.barPressMoney.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barPressMoney.Id = 108;
            this.barPressMoney.Name = "barPressMoney";
            this.barPressMoney.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barSetArEmail
            // 
            this.barSetArEmail.Caption = "SetArEmail";
            this.barSetArEmail.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barSetArEmail.Id = 109;
            this.barSetArEmail.Name = "barSetArEmail";
            this.barSetArEmail.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barExceptionCus
            // 
            this.barExceptionCus.Caption = "ExceptionCus";
            this.barExceptionCus.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.barExceptionCus.Id = 107;
            this.barExceptionCus.Name = "barExceptionCus";
            this.barExceptionCus.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = global::ICP.FAM.UI.Properties.Resources.Refresh_16;
            this.barRefresh.Hint = "&Refresh";
            this.barRefresh.Id = 21;
            this.barRefresh.Name = "barRefresh";
            // 
            // barShowSearch
            // 
            this.barShowSearch.Caption = "ShowSearc&h";
            this.barShowSearch.Glyph = global::ICP.FAM.UI.Properties.Resources.Sarch_16;
            this.barShowSearch.Hint = "ShowSearc&h";
            this.barShowSearch.Id = 23;
            this.barShowSearch.Name = "barShowSearch";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "Print";
            this.barSubItem1.Glyph = global::ICP.FAM.UI.Properties.Resources.Print_16;
            this.barSubItem1.Id = 100;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrintBL)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barPrintBL
            // 
            this.barPrintBL.Caption = "Print BL";
            this.barPrintBL.Id = 101;
            this.barPrintBL.Name = "barPrintBL";
            // 
            // barExportToExcel
            // 
            this.barExportToExcel.Caption = "ExportToExcel";
            this.barExportToExcel.Glyph = global::ICP.FAM.UI.Properties.Resources.Save_Blue_16;
            this.barExportToExcel.Id = 103;
            this.barExportToExcel.Name = "barExportToExcel";
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
            this.barDockControlTop.Size = new System.Drawing.Size(1746, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 28);
            this.barDockControlBottom.Size = new System.Drawing.Size(1746, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 2);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1746, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 2);
            // 
            // barExRelease
            // 
            this.barExRelease.Caption = "ExRelease";
            this.barExRelease.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.barExRelease.Id = 110;
            this.barExRelease.Name = "barExRelease";
            this.barExRelease.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // ReleaseBLToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ReleaseBLToolBar";
            this.Size = new System.Drawing.Size(1746, 28);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barReleaseBL;
        private DevExpress.XtraBars.BarButtonItem barCancelReleaseBL;
        private DevExpress.XtraBars.BarSubItem barVisible;
        private DevExpress.XtraBars.BarCheckItem barVisibleAll;
        private DevExpress.XtraBars.BarCheckItem barVisibleMBL;
        private DevExpress.XtraBars.BarCheckItem barVisibleHBL;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barChangeToOriginal;
        private DevExpress.XtraBars.BarButtonItem barChangeToTelex;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        private DevExpress.XtraBars.BarButtonItem barReceived;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraBars.BarButtonItem barShowSearch;
        private DevExpress.XtraBars.BarButtonItem barApply;
        private DevExpress.XtraBars.BarButtonItem barCancelApply;
        private DevExpress.XtraBars.BarButtonItem barViewBusinessInfo;
        private DevExpress.XtraBars.BarButtonItem barBill;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barPrintBL;
        private DevExpress.XtraBars.BarButtonItem barExportToExcel;
        private DevExpress.XtraBars.BarSubItem barReceviedOBL;
        private DevExpress.XtraBars.BarButtonItem barBtRecevied;
        private DevExpress.XtraBars.BarButtonItem barBtNotRecevied;
        private DevExpress.XtraBars.BarButtonItem barExceptionCus;
        private DevExpress.XtraBars.BarButtonItem barPressMoney;
        private DevExpress.XtraBars.BarButtonItem barSetArEmail;
        private DevExpress.XtraBars.BarButtonItem barExRelease;
    }
}
