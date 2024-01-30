namespace ICP.FAM.UI.Bill
{
    partial class BillToolBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillToolBar));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barBusinessInfo = new DevExpress.XtraBars.BarButtonItem();
            this.barBillList = new DevExpress.XtraBars.BarButtonItem();
            this.barFeeDetail = new DevExpress.XtraBars.BarButtonItem();
            this.barWriteOffHistory = new DevExpress.XtraBars.BarButtonItem();
            this.barSetInvoiceNo = new DevExpress.XtraBars.BarButtonItem();
            this.barViewInvoice = new DevExpress.XtraBars.BarButtonItem();
            this.barShowSelected = new DevExpress.XtraBars.BarButtonItem();
            this.barShowTotal = new DevExpress.XtraBars.BarButtonItem();
            this.barAllCheck = new DevExpress.XtraBars.BarButtonItem();
            this.barShowSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barOpenTaskCenter = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barConvertBillFromARToDN = new DevExpress.XtraBars.BarButtonItem();
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
            this.barClose,
            this.barShowSearch,
            this.barFeeDetail,
            this.barBusinessInfo,
            this.barWriteOffHistory,
            this.barShowSelected,
            this.barShowTotal,
            this.barViewInvoice,
            this.barAllCheck,
            this.barBillList,
            this.barSetInvoiceNo,
            this.barOpenTaskCenter,
            this.barConvertBillFromARToDN});
            this.barManager1.MaxItemId = 22;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBusinessInfo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBillList, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barFeeDetail, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barWriteOffHistory, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSetInvoiceNo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barViewInvoice, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barShowSelected, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barShowTotal, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAllCheck, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barShowSearch, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barOpenTaskCenter, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barConvertBillFromARToDN)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barBusinessInfo
            // 
            this.barBusinessInfo.Caption = "&BusinessInfo";
            this.barBusinessInfo.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barBusinessInfo.Id = 5;
            this.barBusinessInfo.Name = "barBusinessInfo";
            // 
            // barBillList
            // 
            this.barBillList.Caption = "Bill List";
            this.barBillList.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barBillList.Id = 17;
            this.barBillList.Name = "barBillList";
            // 
            // barFeeDetail
            // 
            this.barFeeDetail.Caption = "&FeeDetail";
            this.barFeeDetail.Glyph = global::ICP.FAM.UI.Properties.Resources.MultiCheck;
            this.barFeeDetail.Id = 4;
            this.barFeeDetail.Name = "barFeeDetail";
            // 
            // barWriteOffHistory
            // 
            this.barWriteOffHistory.Caption = "&WriteOffInfo";
            this.barWriteOffHistory.Glyph = global::ICP.FAM.UI.Properties.Resources.ViewCheck;
            this.barWriteOffHistory.Id = 6;
            this.barWriteOffHistory.Name = "barWriteOffHistory";
            // 
            // barSetInvoiceNo
            // 
            this.barSetInvoiceNo.Caption = "SetInvoiceNo";
            this.barSetInvoiceNo.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barSetInvoiceNo.Id = 18;
            this.barSetInvoiceNo.Name = "barSetInvoiceNo";
            // 
            // barViewInvoice
            // 
            this.barViewInvoice.Caption = "&InvoiceInfo";
            this.barViewInvoice.Glyph = global::ICP.FAM.UI.Properties.Resources.DollarSign;
            this.barViewInvoice.Id = 11;
            this.barViewInvoice.Name = "barViewInvoice";
            // 
            // barShowSelected
            // 
            this.barShowSelected.Caption = "ShowSelectedPart";
            this.barShowSelected.Glyph = global::ICP.FAM.UI.Properties.Resources.BlueFile_Edit_16;
            this.barShowSelected.Id = 9;
            this.barShowSelected.Name = "barShowSelected";
            // 
            // barShowTotal
            // 
            this.barShowTotal.Caption = "ShowTotal";
            this.barShowTotal.Glyph = global::ICP.FAM.UI.Properties.Resources.BlueFile_Edit_16;
            this.barShowTotal.Id = 10;
            this.barShowTotal.Name = "barShowTotal";
            // 
            // barAllCheck
            // 
            this.barAllCheck.Caption = "全选(&A)";
            this.barAllCheck.Glyph = global::ICP.FAM.UI.Properties.Resources.Check_16;
            this.barAllCheck.Id = 16;
            this.barAllCheck.Name = "barAllCheck";
            // 
            // barShowSearch
            // 
            this.barShowSearch.Caption = "Search";
            this.barShowSearch.Glyph = global::ICP.FAM.UI.Properties.Resources.Sarch_16;
            this.barShowSearch.Id = 3;
            this.barShowSearch.Name = "barShowSearch";
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 2;
            this.barClose.Name = "barClose";
            // 
            // barOpenTaskCenter
            // 
            this.barOpenTaskCenter.Caption = "Open Task Center";
            this.barOpenTaskCenter.Glyph = global::ICP.FAM.UI.Properties.Resources.BillDetails;
            this.barOpenTaskCenter.Id = 19;
            this.barOpenTaskCenter.Name = "barOpenTaskCenter";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1218, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 28);
            this.barDockControlBottom.Size = new System.Drawing.Size(1218, 0);
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
            this.barDockControlRight.Location = new System.Drawing.Point(1218, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 2);
            // 
            // barConvertBillFromARToDN
            // 
            this.barConvertBillFromARToDN.Caption = "AR To DN";
            this.barConvertBillFromARToDN.Id = 21;
            this.barConvertBillFromARToDN.Name = "barConvertBillFromARToDN";
            // 
            // BillToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BillToolBar";
            this.Size = new System.Drawing.Size(1218, 28);
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
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarButtonItem barShowSearch;
        private DevExpress.XtraBars.BarButtonItem barBusinessInfo;
        private DevExpress.XtraBars.BarButtonItem barFeeDetail;
        private DevExpress.XtraBars.BarButtonItem barWriteOffHistory;
        private DevExpress.XtraBars.BarButtonItem barShowSelected;
        private DevExpress.XtraBars.BarButtonItem barShowTotal;
        private DevExpress.XtraBars.BarButtonItem barViewInvoice;
        private DevExpress.XtraBars.BarButtonItem barAllCheck;
        private DevExpress.XtraBars.BarButtonItem barBillList;
        private DevExpress.XtraBars.BarButtonItem barSetInvoiceNo;
        private DevExpress.XtraBars.BarButtonItem barOpenTaskCenter;
        private DevExpress.XtraBars.BarButtonItem barConvertBillFromARToDN;
    }
}
