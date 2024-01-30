namespace ICP.FAM.UI.Bill
{
    partial class BillOperationToolBar
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillOperationToolBar));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barCheck = new DevExpress.XtraBars.BarSubItem();
            this.barWriteOff = new DevExpress.XtraBars.BarButtonItem();
            this.barMultiCurrencyWriteOff = new DevExpress.XtraBars.BarButtonItem();
            this.barAuditor = new DevExpress.XtraBars.BarButtonItem();
            this.barUnAuditor = new DevExpress.XtraBars.BarButtonItem();
            this.barInvoiceContract = new DevExpress.XtraBars.BarButtonItem();
            this.barInvoice = new DevExpress.XtraBars.BarButtonItem();
            this.barLab = new DevExpress.XtraBars.BarStaticItem();
            this.numoperateRate = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.barWorkFlow = new DevExpress.XtraBars.BarSubItem();
            this.barPaymentRequest = new DevExpress.XtraBars.BarButtonItem();
            this.barBusinessCost = new DevExpress.XtraBars.BarButtonItem();
            this.barRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barClear = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemFontEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).BeginInit();
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
            this.barAuditor,
            this.barRemove,
            this.barClear,
            this.barInvoice,
            this.numoperateRate,
            this.barLab,
            this.barUnAuditor,
            this.barCheck,
            this.barWriteOff,
            this.barMultiCurrencyWriteOff,
            this.barWorkFlow,
            this.barPaymentRequest,
            this.barBusinessCost,
            this.barInvoiceContract});
            this.barManager1.MaxItemId = 28;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit1,
            this.repositoryItemFontEdit1});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCheck, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAuditor, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barUnAuditor, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barInvoiceContract, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barInvoice, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLab, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.numoperateRate),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barWorkFlow, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRemove, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClear, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barCheck
            // 
            this.barCheck.Caption = "销账";
            this.barCheck.Glyph = global::ICP.FAM.UI.Properties.Resources.BlueFile_Edit_16;
            this.barCheck.Id = 20;
            this.barCheck.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barWriteOff),
            new DevExpress.XtraBars.LinkPersistInfo(this.barMultiCurrencyWriteOff)});
            this.barCheck.Name = "barCheck";
            // 
            // barWriteOff
            // 
            this.barWriteOff.Caption = "单币种销账(&W)";
            this.barWriteOff.Glyph = global::ICP.FAM.UI.Properties.Resources.BlueFile_Edit_16;
            this.barWriteOff.Id = 21;
            this.barWriteOff.Name = "barWriteOff";
            toolTipTitleItem1.Text = "按单币种进行销账";
            superToolTip1.Items.Add(toolTipTitleItem1);
            this.barWriteOff.SuperTip = superToolTip1;
            // 
            // barMultiCurrencyWriteOff
            // 
            this.barMultiCurrencyWriteOff.Caption = "多币种销账(&M)";
            this.barMultiCurrencyWriteOff.Glyph = global::ICP.FAM.UI.Properties.Resources.BlueFile_Edit_16;
            this.barMultiCurrencyWriteOff.Id = 22;
            this.barMultiCurrencyWriteOff.Name = "barMultiCurrencyWriteOff";
            // 
            // barAuditor
            // 
            this.barAuditor.Caption = "审核";
            this.barAuditor.Glyph = global::ICP.FAM.UI.Properties.Resources.Check_16;
            this.barAuditor.Id = 4;
            this.barAuditor.Name = "barAuditor";
            // 
            // barUnAuditor
            // 
            this.barUnAuditor.Caption = "取消审核";
            this.barUnAuditor.Glyph = global::ICP.FAM.UI.Properties.Resources.Return_16;
            this.barUnAuditor.Id = 18;
            this.barUnAuditor.Name = "barUnAuditor";
            // 
            // barInvoiceContract
            // 
            this.barInvoiceContract.Caption = "发票合同";
            this.barInvoiceContract.Glyph = global::ICP.FAM.UI.Properties.Resources.BillDetails;
            this.barInvoiceContract.Id = 27;
            this.barInvoiceContract.Name = "barInvoiceContract";
            // 
            // barInvoice
            // 
            this.barInvoice.Caption = "开发票";
            this.barInvoice.Glyph = global::ICP.FAM.UI.Properties.Resources.ViewCheck;
            this.barInvoice.Id = 14;
            this.barInvoice.Name = "barInvoice";
            // 
            // barLab
            // 
            this.barLab.Caption = "手续费比例(%)";
            this.barLab.Id = 17;
            this.barLab.Name = "barLab";
            this.barLab.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // numoperateRate
            // 
            this.numoperateRate.Edit = this.repositoryItemSpinEdit1;
            this.numoperateRate.EditValue = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numoperateRate.Id = 15;
            this.numoperateRate.Name = "numoperateRate";
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // barWorkFlow
            // 
            this.barWorkFlow.Caption = "发起流程";
            this.barWorkFlow.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barWorkFlow.Id = 24;
            this.barWorkFlow.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barPaymentRequest),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBusinessCost)});
            this.barWorkFlow.Name = "barWorkFlow";
            // 
            // barPaymentRequest
            // 
            this.barPaymentRequest.Caption = "付款申请";
            this.barPaymentRequest.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barPaymentRequest.Id = 25;
            this.barPaymentRequest.Name = "barPaymentRequest";
            // 
            // barBusinessCost
            // 
            this.barBusinessCost.Caption = "发起业务管理成本";
            this.barBusinessCost.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barBusinessCost.Id = 26;
            this.barBusinessCost.Name = "barBusinessCost";
            this.barBusinessCost.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBusinessCost_ItemClick);
            // 
            // barRemove
            // 
            this.barRemove.Caption = "移除";
            this.barRemove.Glyph = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.barRemove.Id = 9;
            this.barRemove.Name = "barRemove";
            // 
            // barClear
            // 
            this.barClear.Caption = "清空";
            this.barClear.Glyph = ((System.Drawing.Image)(resources.GetObject("barClear.Glyph")));
            this.barClear.Id = 10;
            this.barClear.Name = "barClear";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(995, 30);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 28);
            this.barDockControlBottom.Size = new System.Drawing.Size(995, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 30);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(995, 30);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 0);
            // 
            // repositoryItemFontEdit1
            // 
            this.repositoryItemFontEdit1.AutoHeight = false;
            this.repositoryItemFontEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFontEdit1.Name = "repositoryItemFontEdit1";
            // 
            // BillOperationToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BillOperationToolBar";
            this.Size = new System.Drawing.Size(995, 28);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barAuditor;
        private DevExpress.XtraBars.BarButtonItem barRemove;
        private DevExpress.XtraBars.BarButtonItem barClear;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraBars.BarButtonItem barInvoice;
        private DevExpress.XtraBars.BarStaticItem barLab;
        private DevExpress.XtraBars.BarEditItem numoperateRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit1;
        private DevExpress.XtraBars.BarButtonItem barUnAuditor;
        private DevExpress.XtraBars.BarSubItem barCheck;
        private DevExpress.XtraBars.BarButtonItem barWriteOff;
        private DevExpress.XtraBars.BarButtonItem barMultiCurrencyWriteOff;
        private DevExpress.XtraBars.BarSubItem barWorkFlow;
        private DevExpress.XtraBars.BarButtonItem barPaymentRequest;
        private DevExpress.XtraBars.BarButtonItem barBusinessCost;
        private DevExpress.XtraBars.BarButtonItem barInvoiceContract;
    }
}
