namespace ICP.FAM.UI
{
    partial class BankTool
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
            this.barNew = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barPrintVientam = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintInvoice = new DevExpress.XtraBars.BarButtonItem();
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
            this.barNew,
            this.barEdit,
            this.barCancel,
            this.barSearch,
            this.barSubItem1,
            this.barPrintVientam,
            this.barPrintInvoice,
            this.barClose});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 13;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCancel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSearch, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barNew
            // 
            this.barNew.Caption = "新建(&N)";
            this.barNew.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.barNew.Id = 0;
            this.barNew.Name = "barNew";
            // 
            // barEdit
            // 
            this.barEdit.Caption = "编辑(&E)";
            this.barEdit.Glyph = global::ICP.FAM.UI.Properties.Resources.Edit_16;
            this.barEdit.Id = 1;
            this.barEdit.Name = "barEdit";
            // 
            // barCancel
            // 
            this.barCancel.Caption = "作废(&D)";
            this.barCancel.Glyph = global::ICP.FAM.UI.Properties.Resources.Warning_16;
            this.barCancel.Id = 2;
            this.barCancel.Name = "barCancel";
            this.barCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCancel_ItemClick);
            // 
            // barSearch
            // 
            this.barSearch.Caption = "查询(&H)";
            this.barSearch.Glyph = global::ICP.FAM.UI.Properties.Resources.Sarch_16;
            this.barSearch.Id = 4;
            this.barSearch.Name = "barSearch";
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭(&C)";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 12;
            this.barClose.Name = "barClose";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(812, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 37);
            this.barDockControlBottom.Size = new System.Drawing.Size(812, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 11);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(812, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 11);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "打印";
            this.barSubItem1.Id = 8;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barPrintVientam
            // 
            this.barPrintVientam.Caption = "Print Vientam";
            this.barPrintVientam.Glyph = global::ICP.FAM.UI.Properties.Resources.Print_16;
            this.barPrintVientam.Id = 10;
            this.barPrintVientam.Name = "barPrintVientam";
            // 
            // barPrintInvoice
            // 
            this.barPrintInvoice.Caption = "Print Invoice";
            this.barPrintInvoice.Glyph = global::ICP.FAM.UI.Properties.Resources.Print_16;
            this.barPrintInvoice.Id = 11;
            this.barPrintInvoice.Name = "barPrintInvoice";
            // 
            // BankTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BankTool";
            this.Size = new System.Drawing.Size(812, 37);
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
        private DevExpress.XtraBars.BarButtonItem barNew;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        private DevExpress.XtraBars.BarButtonItem barCancel;
        private DevExpress.XtraBars.BarButtonItem barSearch;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barPrintVientam;
        private DevExpress.XtraBars.BarButtonItem barPrintInvoice;
        private DevExpress.XtraBars.BarButtonItem barClose;
    }
}
