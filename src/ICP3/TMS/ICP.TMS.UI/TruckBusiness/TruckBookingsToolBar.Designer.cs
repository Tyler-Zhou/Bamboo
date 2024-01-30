namespace ICP.TMS.UI
{
    partial class TruckBookingsToolBar
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
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barVoid = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barDownLoad = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barDispatch = new DevExpress.XtraBars.BarButtonItem();
            this.barBill = new DevExpress.XtraBars.BarButtonItem();
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
            this.barAdd,
            this.barCopy,
            this.barEdit,
            this.barVoid,
            this.barDelete,
            this.barPrint,
            this.barDispatch,
            this.barBill,
            this.barRefresh,
            this.barClose,
            this.barDownLoad,
            this.barShowSearch});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 12;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCopy, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barVoid, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDownLoad, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDispatch, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBill, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barShowSearch),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "新增(&A)";
            this.barAdd.Glyph = global::ICP.TMS.UI.Properties.Resources.Add_16;
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            this.barAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barCopy
            // 
            this.barCopy.Caption = "Copy";
            this.barCopy.Glyph = global::ICP.TMS.UI.Properties.Resources.copy_16;
            this.barCopy.Id = 11;
            this.barCopy.Name = "barCopy";
            this.barCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCopy_ItemClick);
            // 
            // barEdit
            // 
            this.barEdit.Caption = "编辑(&E)";
            this.barEdit.Glyph = global::ICP.TMS.UI.Properties.Resources.Edit_16;
            this.barEdit.Id = 1;
            this.barEdit.Name = "barEdit";
            this.barEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdit_ItemClick);
            // 
            // barVoid
            // 
            this.barVoid.Caption = "作废(&V)";
            this.barVoid.Glyph = global::ICP.TMS.UI.Properties.Resources.Empty;
            this.barVoid.Id = 2;
            this.barVoid.Name = "barVoid";
            this.barVoid.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barVoid.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barVoid_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "删除(&D)";
            this.barDelete.Glyph = global::ICP.TMS.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 3;
            this.barDelete.Name = "barDelete";
            this.barDelete.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barDownLoad
            // 
            this.barDownLoad.Caption = "下载(&L)";
            this.barDownLoad.Glyph = global::ICP.TMS.UI.Properties.Resources.Down_16;
            this.barDownLoad.Id = 9;
            this.barDownLoad.Name = "barDownLoad";
            this.barDownLoad.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barDownLoad.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDownLoad_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "打印(&P)";
            this.barPrint.Glyph = global::ICP.TMS.UI.Properties.Resources.Print_16;
            this.barPrint.Id = 4;
            this.barPrint.Name = "barPrint";
            this.barPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barDispatch
            // 
            this.barDispatch.Caption = "派车(&T)";
            this.barDispatch.Glyph = global::ICP.TMS.UI.Properties.Resources.Go;
            this.barDispatch.Id = 5;
            this.barDispatch.Name = "barDispatch";
            this.barDispatch.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barDispatch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDispatch_ItemClick);
            // 
            // barBill
            // 
            this.barBill.Caption = "账单(&B)";
            this.barBill.Glyph = global::ICP.TMS.UI.Properties.Resources.Transfer_16;
            this.barBill.Id = 6;
            this.barBill.Name = "barBill";
            this.barBill.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBill.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBill_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "刷新(&R)";
            this.barRefresh.Glyph = global::ICP.TMS.UI.Properties.Resources.Refresh_16;
            this.barRefresh.Id = 7;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barShowSearch
            // 
            this.barShowSearch.Caption = "相称";
            this.barShowSearch.Glyph = global::ICP.TMS.UI.Properties.Resources.Sarch_16;
            this.barShowSearch.Id = 10;
            this.barShowSearch.Name = "barShowSearch";
            this.barShowSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barShowSearch_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭(&C)";
            this.barClose.Glyph = global::ICP.TMS.UI.Properties.Resources.Close_16;
            this.barClose.Id = 8;
            this.barClose.Name = "barClose";
            this.barClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(977, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 29);
            this.barDockControlBottom.Size = new System.Drawing.Size(977, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 3);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(977, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 3);
            // 
            // TruckBookingsToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "TruckBookingsToolBar";
            this.Size = new System.Drawing.Size(977, 29);
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
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        private DevExpress.XtraBars.BarButtonItem barVoid;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barDispatch;
        private DevExpress.XtraBars.BarButtonItem barBill;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarButtonItem barDownLoad;
        private DevExpress.XtraBars.BarButtonItem barShowSearch;
        private DevExpress.XtraBars.BarButtonItem barCopy;
    }
}
