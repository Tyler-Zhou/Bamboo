namespace ICP.FAM.UI.CustomerBill
{
    partial class CustomerBillListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerBillListPart));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarSubItem();
            this.barAddAR = new DevExpress.XtraBars.BarButtonItem();
            this.barAddAP = new DevExpress.XtraBars.BarButtonItem();
            this.barAddDC = new DevExpress.XtraBars.BarButtonItem();
            this.barAddBillFromHistory = new DevExpress.XtraBars.BarButtonItem();
            this.barAddLocalFee = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barSubPrint = new DevExpress.XtraBars.BarSubItem();
            this.barPrintBill = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintCombinCharge = new DevExpress.XtraBars.BarButtonItem();
            this.barPritnFeeList = new DevExpress.XtraBars.BarButtonItem();
            this.barInvoiceContract = new DevExpress.XtraBars.BarButtonItem();
            this.barPayoffWF = new DevExpress.XtraBars.BarButtonItem();
            this.barDeficit = new DevExpress.XtraBars.BarButtonItem();
            this.barAdditionalWF = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barShowTotal = new DevExpress.XtraBars.BarButtonItem();
            this.ToRegenerateBill = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barToolbarsListItem1 = new DevExpress.XtraBars.BarToolbarsListItem();
            this.panelListTotal = new System.Windows.Forms.Panel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labBulkCargoCast = new DevExpress.XtraEditors.LabelControl();
            this.txtnoVatProfit = new DevExpress.XtraEditors.TextEdit();
            this.txtAdjustmentFee = new DevExpress.XtraEditors.TextEdit();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.txtOperationNo = new DevExpress.XtraEditors.TextEdit();
            this.labTotal = new DevExpress.XtraEditors.LabelControl();
            this.cmbProfitCurrency = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labOperationNo = new DevExpress.XtraEditors.LabelControl();
            this.labProfit = new DevExpress.XtraEditors.LabelControl();
            this.txtProfitByCurrency = new DevExpress.XtraEditors.TextEdit();
            this.bsBillList = new System.Windows.Forms.BindingSource(this.components);
            this.gcBillList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvBillList = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmountDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colPayAmountDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBalanceDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDueDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankDates = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckDates = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRefNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBizType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imageListType = new System.Windows.Forms.ImageList(this.components);
            this.barRegenerateDHF = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.panelListTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtnoVatProfit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdjustmentFee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProfitCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProfitByCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBillList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBillList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBillList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
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
            this.barDelete,
            this.barPayoffWF,
            this.barPrintBill,
            this.barDeficit,
            this.barRefresh,
            this.barClose,
            this.barSubPrint,
            this.barPrintCombinCharge,
            this.barShowTotal,
            this.barPritnFeeList,
            this.barAddBillFromHistory,
            this.barAdd,
            this.barToolbarsListItem1,
            this.barAddAR,
            this.barAddAP,
            this.barAddDC,
            this.barAdditionalWF,
            this.ToRegenerateBill,
            this.barInvoiceContract,
            this.barAddLocalFee,
            this.barRegenerateDHF});
            this.barManager1.MaxItemId = 24;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAddBillFromHistory, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAddLocalFee),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPayoffWF, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDeficit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAdditionalWF),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barShowTotal, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.ToRegenerateBill, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRegenerateDHF, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "&Add";
            this.barAdd.Enabled = false;
            this.barAdd.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_16;
            this.barAdd.Id = 14;
            this.barAdd.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAddAR),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAddAP),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAddDC)});
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barAddAR
            // 
            this.barAddAR.Caption = "AR [F2]";
            this.barAddAR.Id = 16;
            this.barAddAR.Name = "barAddAR";
            this.barAddAR.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddAR_ItemClick);
            // 
            // barAddAP
            // 
            this.barAddAP.Caption = "AP [F3]";
            this.barAddAP.Id = 17;
            this.barAddAP.Name = "barAddAP";
            this.barAddAP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddAP_ItemClick);
            // 
            // barAddDC
            // 
            this.barAddDC.Caption = "D/C [F4]";
            this.barAddDC.Id = 18;
            this.barAddDC.Name = "barAddDC";
            this.barAddDC.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddDC_ItemClick);
            // 
            // barAddBillFromHistory
            // 
            this.barAddBillFromHistory.Caption = "AddBillFromHistory";
            this.barAddBillFromHistory.Enabled = false;
            this.barAddBillFromHistory.Glyph = global::ICP.FAM.UI.Properties.Resources.MultiCheck;
            this.barAddBillFromHistory.Id = 12;
            this.barAddBillFromHistory.Name = "barAddBillFromHistory";
            this.barAddBillFromHistory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddBillFromHistory_ItemClick);
            // 
            // barAddLocalFee
            // 
            this.barAddLocalFee.Caption = "AddLocalFee";
            this.barAddLocalFee.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.barAddLocalFee.Id = 22;
            this.barAddLocalFee.Name = "barAddLocalFee";
            this.barAddLocalFee.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barAddLocalFee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddLocalFee_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "&Delete";
            this.barDelete.Glyph = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barSubPrint
            // 
            this.barSubPrint.Caption = "Print";
            this.barSubPrint.Enabled = false;
            this.barSubPrint.Glyph = global::ICP.FAM.UI.Properties.Resources.Print_16;
            this.barSubPrint.Id = 8;
            this.barSubPrint.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrintBill),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrintCombinCharge),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPritnFeeList),
            new DevExpress.XtraBars.LinkPersistInfo(this.barInvoiceContract)});
            this.barSubPrint.Name = "barSubPrint";
            // 
            // barPrintBill
            // 
            this.barPrintBill.Caption = "&Print Bill";
            this.barPrintBill.Id = 3;
            this.barPrintBill.Name = "barPrintBill";
            this.barPrintBill.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrintBill_ItemClick);
            // 
            // barPrintCombinCharge
            // 
            this.barPrintCombinCharge.Caption = "Print C&ombin ChargeBill";
            this.barPrintCombinCharge.Id = 9;
            this.barPrintCombinCharge.Name = "barPrintCombinCharge";
            this.barPrintCombinCharge.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrintCombinCharge_ItemClick);
            // 
            // barPritnFeeList
            // 
            this.barPritnFeeList.Caption = "Print Fee List";
            this.barPritnFeeList.Id = 11;
            this.barPritnFeeList.Name = "barPritnFeeList";
            this.barPritnFeeList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPritnFeeList_ItemClick);
            // 
            // barInvoiceContract
            // 
            this.barInvoiceContract.Caption = "Print Invoice Contract";
            this.barInvoiceContract.Id = 21;
            this.barInvoiceContract.Name = "barInvoiceContract";
            this.barInvoiceContract.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barInvoiceContract_ItemClick);
            // 
            // barPayoffWF
            // 
            this.barPayoffWF.Caption = "PayoffWF";
            this.barPayoffWF.Enabled = false;
            this.barPayoffWF.Glyph = global::ICP.FAM.UI.Properties.Resources.BlueFile_Edit_16;
            this.barPayoffWF.Id = 2;
            this.barPayoffWF.Name = "barPayoffWF";
            this.barPayoffWF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPayoffWF_ItemClick);
            // 
            // barDeficit
            // 
            this.barDeficit.Caption = "Deficit";
            this.barDeficit.Enabled = false;
            this.barDeficit.Glyph = global::ICP.FAM.UI.Properties.Resources.Data_16;
            this.barDeficit.Id = 4;
            this.barDeficit.Name = "barDeficit";
            this.barDeficit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDeficit_ItemClick);
            // 
            // barAdditionalWF
            // 
            this.barAdditionalWF.Caption = "AdditionalWF";
            this.barAdditionalWF.Id = 19;
            this.barAdditionalWF.Name = "barAdditionalWF";
            this.barAdditionalWF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdditionalWF_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = global::ICP.FAM.UI.Properties.Resources.Refresh_16;
            this.barRefresh.Hint = "&Refresh";
            this.barRefresh.Id = 5;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barShowTotal
            // 
            this.barShowTotal.Caption = "S&howTotal";
            this.barShowTotal.Glyph = global::ICP.FAM.UI.Properties.Resources.ViewCheck;
            this.barShowTotal.Id = 10;
            this.barShowTotal.Name = "barShowTotal";
            this.barShowTotal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barShowTotal_ItemClick);
            // 
            // ToRegenerateBill
            // 
            this.ToRegenerateBill.Caption = "To Regenerate";
            this.ToRegenerateBill.Glyph = global::ICP.FAM.UI.Properties.Resources.Save_Blue_16;
            this.ToRegenerateBill.Id = 20;
            this.ToRegenerateBill.Name = "ToRegenerateBill";
            this.ToRegenerateBill.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ToRegenerateBill_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 6;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1254, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 480);
            this.barDockControlBottom.Size = new System.Drawing.Size(1254, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 454);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1254, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 454);
            // 
            // barToolbarsListItem1
            // 
            this.barToolbarsListItem1.Caption = "Add";
            this.barToolbarsListItem1.Id = 15;
            this.barToolbarsListItem1.Name = "barToolbarsListItem1";
            // 
            // panelListTotal
            // 
            this.panelListTotal.Controls.Add(this.labelControl2);
            this.panelListTotal.Controls.Add(this.labBulkCargoCast);
            this.panelListTotal.Controls.Add(this.txtnoVatProfit);
            this.panelListTotal.Controls.Add(this.txtAdjustmentFee);
            this.panelListTotal.Controls.Add(this.txtTotal);
            this.panelListTotal.Controls.Add(this.txtOperationNo);
            this.panelListTotal.Controls.Add(this.labTotal);
            this.panelListTotal.Controls.Add(this.cmbProfitCurrency);
            this.panelListTotal.Controls.Add(this.labOperationNo);
            this.panelListTotal.Controls.Add(this.labProfit);
            this.panelListTotal.Controls.Add(this.txtProfitByCurrency);
            this.panelListTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelListTotal.Location = new System.Drawing.Point(0, 451);
            this.panelListTotal.Name = "panelListTotal";
            this.panelListTotal.Size = new System.Drawing.Size(1254, 29);
            this.panelListTotal.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Location = new System.Drawing.Point(821, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "除增值税利润";
            // 
            // labBulkCargoCast
            // 
            this.labBulkCargoCast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labBulkCargoCast.Location = new System.Drawing.Point(448, 7);
            this.labBulkCargoCast.Name = "labBulkCargoCast";
            this.labBulkCargoCast.Size = new System.Drawing.Size(48, 14);
            this.labBulkCargoCast.TabIndex = 8;
            this.labBulkCargoCast.Text = "调整费用";
            // 
            // txtnoVatProfit
            // 
            this.txtnoVatProfit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtnoVatProfit.Location = new System.Drawing.Point(897, 4);
            this.txtnoVatProfit.MenuManager = this.barManager1;
            this.txtnoVatProfit.Name = "txtnoVatProfit";
            this.txtnoVatProfit.Size = new System.Drawing.Size(118, 21);
            this.txtnoVatProfit.TabIndex = 7;
            // 
            // txtAdjustmentFee
            // 
            this.txtAdjustmentFee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdjustmentFee.Location = new System.Drawing.Point(502, 4);
            this.txtAdjustmentFee.Name = "txtAdjustmentFee";
            this.txtAdjustmentFee.Size = new System.Drawing.Size(310, 21);
            this.txtAdjustmentFee.TabIndex = 6;
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.Location = new System.Drawing.Point(285, 4);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Properties.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(157, 21);
            this.txtTotal.TabIndex = 3;
            // 
            // txtOperationNo
            // 
            this.txtOperationNo.Location = new System.Drawing.Point(82, 4);
            this.txtOperationNo.MenuManager = this.barManager1;
            this.txtOperationNo.Name = "txtOperationNo";
            this.txtOperationNo.Properties.ReadOnly = true;
            this.txtOperationNo.Size = new System.Drawing.Size(149, 21);
            this.txtOperationNo.TabIndex = 2;
            // 
            // labTotal
            // 
            this.labTotal.Location = new System.Drawing.Point(237, 7);
            this.labTotal.Name = "labTotal";
            this.labTotal.Size = new System.Drawing.Size(28, 14);
            this.labTotal.TabIndex = 3;
            this.labTotal.Text = "Total";
            // 
            // cmbProfitCurrency
            // 
            this.cmbProfitCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProfitCurrency.Location = new System.Drawing.Point(1061, 4);
            this.cmbProfitCurrency.Name = "cmbProfitCurrency";
            this.cmbProfitCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbProfitCurrency.Size = new System.Drawing.Size(73, 21);
            this.cmbProfitCurrency.TabIndex = 4;
            // 
            // labOperationNo
            // 
            this.labOperationNo.Location = new System.Drawing.Point(5, 7);
            this.labOperationNo.Name = "labOperationNo";
            this.labOperationNo.Size = new System.Drawing.Size(73, 14);
            this.labOperationNo.TabIndex = 3;
            this.labOperationNo.Text = "Operation No";
            // 
            // labProfit
            // 
            this.labProfit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labProfit.Location = new System.Drawing.Point(1021, 7);
            this.labProfit.Name = "labProfit";
            this.labProfit.Size = new System.Drawing.Size(29, 14);
            this.labProfit.TabIndex = 0;
            this.labProfit.Text = "Profit";
            // 
            // txtProfitByCurrency
            // 
            this.txtProfitByCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProfitByCurrency.Location = new System.Drawing.Point(1140, 4);
            this.txtProfitByCurrency.Name = "txtProfitByCurrency";
            this.txtProfitByCurrency.Properties.ReadOnly = true;
            this.txtProfitByCurrency.Size = new System.Drawing.Size(100, 21);
            this.txtProfitByCurrency.TabIndex = 5;
            // 
            // bsBillList
            // 
            this.bsBillList.PositionChanged += new System.EventHandler(this.bsBillList_PositionChanged);
            // 
            // gcBillList
            // 
            this.gcBillList.DataSource = this.bsBillList;
            this.gcBillList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBillList.Location = new System.Drawing.Point(0, 26);
            this.gcBillList.MainView = this.gvBillList;
            this.gcBillList.MenuManager = this.barManager1;
            this.gcBillList.Name = "gcBillList";
            this.gcBillList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbType,
            this.rcmbState,
            this.repositoryItemTextEdit1});
            this.gcBillList.Size = new System.Drawing.Size(1254, 425);
            this.gcBillList.TabIndex = 0;
            this.gcBillList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBillList});
            this.gcBillList.Click += new System.EventHandler(this.bsBillList_PositionChanged);
            this.gcBillList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcBillList_KeyDown);
            // 
            // gvBillList
            // 
            this.gvBillList.Appearance.Row.Options.UseTextOptions = true;
            this.gvBillList.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvBillList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colType,
            this.colState,
            this.colNo,
            this.colCustomerName,
            this.colAmountDescription,
            this.colPayAmountDescription,
            this.colBalanceDescription,
            this.colAccountDate,
            this.colDueDate,
            this.colBankDates,
            this.colCheckDates,
            this.colRefNo,
            this.colInvoiceNo,
            this.colCheckNo,
            this.colBizType,
            this.colCreateByName,
            this.colCreateDate});
            this.gvBillList.GridControl = this.gcBillList;
            this.gvBillList.Name = "gvBillList";
            this.gvBillList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvBillList.OptionsBehavior.Editable = false;
            this.gvBillList.OptionsBehavior.ReadOnly = true;
            this.gvBillList.OptionsCustomization.AllowRowSizing = true;
            this.gvBillList.OptionsDetail.EnableMasterViewMode = false;
            this.gvBillList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvBillList.OptionsSelection.MultiSelect = true;
            this.gvBillList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvBillList.OptionsView.ColumnAutoWidth = false;
            this.gvBillList.OptionsView.RowAutoHeight = true;
            this.gvBillList.OptionsView.ShowGroupPanel = false;
            this.gvBillList.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvBillList_RowCellStyle);
            this.gvBillList.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            // 
            // colType
            // 
            this.colType.Caption = "Type";
            this.colType.ColumnEdit = this.cmbType;
            this.colType.FieldName = "Type";
            this.colType.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colType.Name = "colType";
            this.colType.OptionsColumn.AllowMove = false;
            this.colType.OptionsColumn.AllowSize = false;
            this.colType.Visible = true;
            this.colType.VisibleIndex = 0;
            this.colType.Width = 45;
            // 
            // cmbType
            // 
            this.cmbType.AutoHeight = false;
            this.cmbType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cmbType.Name = "cmbType";
            // 
            // colState
            // 
            this.colState.Caption = "State";
            this.colState.ColumnEdit = this.rcmbState;
            this.colState.FieldName = "State";
            this.colState.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowMove = false;
            this.colState.Visible = true;
            this.colState.VisibleIndex = 1;
            this.colState.Width = 55;
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
            // 
            // colNo
            // 
            this.colNo.Caption = "No";
            this.colNo.FieldName = "No";
            this.colNo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colNo.Name = "colNo";
            this.colNo.OptionsColumn.AllowMove = false;
            this.colNo.OptionsColumn.AllowSize = false;
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 2;
            this.colNo.Width = 130;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Customer";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 3;
            // 
            // colAmountDescription
            // 
            this.colAmountDescription.Caption = "Amount";
            this.colAmountDescription.ColumnEdit = this.repositoryItemTextEdit1;
            this.colAmountDescription.FieldName = "AmountDescription";
            this.colAmountDescription.Name = "colAmountDescription";
            this.colAmountDescription.Visible = true;
            this.colAmountDescription.VisibleIndex = 4;
            this.colAmountDescription.Width = 83;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // colPayAmountDescription
            // 
            this.colPayAmountDescription.Caption = "PayAmount";
            this.colPayAmountDescription.ColumnEdit = this.repositoryItemTextEdit1;
            this.colPayAmountDescription.FieldName = "PayAmountDescription";
            this.colPayAmountDescription.Name = "colPayAmountDescription";
            this.colPayAmountDescription.Visible = true;
            this.colPayAmountDescription.VisibleIndex = 5;
            this.colPayAmountDescription.Width = 90;
            // 
            // colBalanceDescription
            // 
            this.colBalanceDescription.Caption = "Balance";
            this.colBalanceDescription.ColumnEdit = this.repositoryItemTextEdit1;
            this.colBalanceDescription.FieldName = "BalanceDescription";
            this.colBalanceDescription.Name = "colBalanceDescription";
            this.colBalanceDescription.Visible = true;
            this.colBalanceDescription.VisibleIndex = 6;
            // 
            // colAccountDate
            // 
            this.colAccountDate.Caption = "AccountDate";
            this.colAccountDate.FieldName = "AccountDate";
            this.colAccountDate.Name = "colAccountDate";
            this.colAccountDate.Visible = true;
            this.colAccountDate.VisibleIndex = 7;
            this.colAccountDate.Width = 97;
            // 
            // colDueDate
            // 
            this.colDueDate.Caption = "DueDate";
            this.colDueDate.FieldName = "DueDate";
            this.colDueDate.Name = "colDueDate";
            this.colDueDate.Visible = true;
            this.colDueDate.VisibleIndex = 8;
            // 
            // colBankDates
            // 
            this.colBankDates.Caption = "BankDates";
            this.colBankDates.FieldName = "BankDates";
            this.colBankDates.Name = "colBankDates";
            this.colBankDates.Visible = true;
            this.colBankDates.VisibleIndex = 9;
            // 
            // colCheckDates
            // 
            this.colCheckDates.Caption = "CheckDates";
            this.colCheckDates.FieldName = "CheckDates";
            this.colCheckDates.Name = "colCheckDates";
            this.colCheckDates.Visible = true;
            this.colCheckDates.VisibleIndex = 10;
            // 
            // colRefNo
            // 
            this.colRefNo.Caption = "RefNo";
            this.colRefNo.FieldName = "FormNo";
            this.colRefNo.Name = "colRefNo";
            this.colRefNo.Visible = true;
            this.colRefNo.VisibleIndex = 11;
            this.colRefNo.Width = 137;
            // 
            // colInvoiceNo
            // 
            this.colInvoiceNo.Caption = "InvoiceNo";
            this.colInvoiceNo.FieldName = "InvoiceNo";
            this.colInvoiceNo.Name = "colInvoiceNo";
            this.colInvoiceNo.Visible = true;
            this.colInvoiceNo.VisibleIndex = 12;
            // 
            // colCheckNo
            // 
            this.colCheckNo.Caption = "CheckNo";
            this.colCheckNo.FieldName = "CheckNo";
            this.colCheckNo.Name = "colCheckNo";
            this.colCheckNo.Visible = true;
            this.colCheckNo.VisibleIndex = 13;
            // 
            // colBizType
            // 
            this.colBizType.Caption = "gridColumn1";
            this.colBizType.FieldName = "BizType";
            this.colBizType.Name = "colBizType";
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "CreateBy";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 14;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "CreateDate";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 15;
            this.colCreateDate.Width = 100;
            // 
            // imageListType
            // 
            this.imageListType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListType.ImageStream")));
            this.imageListType.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListType.Images.SetKeyName(0, "-.png");
            this.imageListType.Images.SetKeyName(1, "+.png");
            this.imageListType.Images.SetKeyName(2, "-.png");
            this.imageListType.Images.SetKeyName(3, "+-.png");
            // 
            // barRegenerateDHF
            // 
            this.barRegenerateDHF.Caption = "Regenerate";
            this.barRegenerateDHF.Glyph = global::ICP.FAM.UI.Properties.Resources.Save_Blue_16;
            this.barRegenerateDHF.Id = 23;
            this.barRegenerateDHF.Name = "barRegenerateDHF";
            this.barRegenerateDHF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRegenerateDHF_ItemClick);
            // 
            // CustomerBillListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcBillList);
            this.Controls.Add(this.panelListTotal);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "CustomerBillListPart";
            this.Size = new System.Drawing.Size(1254, 480);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.panelListTotal.ResumeLayout(false);
            this.panelListTotal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtnoVatProfit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdjustmentFee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProfitCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProfitByCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBillList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBillList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBillList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarButtonItem barPayoffWF;
        private DevExpress.XtraBars.BarButtonItem barPrintBill;
        private DevExpress.XtraBars.BarButtonItem barDeficit;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarSubItem barSubPrint;
        private DevExpress.XtraBars.BarButtonItem barPrintCombinCharge;
        private System.Windows.Forms.Panel panelListTotal;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private DevExpress.XtraEditors.TextEdit txtOperationNo;
        private DevExpress.XtraEditors.LabelControl labTotal;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbProfitCurrency;
        private DevExpress.XtraEditors.LabelControl labOperationNo;
        private DevExpress.XtraEditors.LabelControl labProfit;
        private DevExpress.XtraEditors.TextEdit txtProfitByCurrency;
        private DevExpress.XtraBars.BarButtonItem barShowTotal;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcBillList;
        private System.Windows.Forms.BindingSource bsBillList;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvBillList;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbType;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colRefNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDueDate;
        private DevExpress.XtraGrid.Columns.GridColumn colAmountDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colPayAmountDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colBalanceDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private System.Windows.Forms.ImageList imageListType;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckNo;
        private DevExpress.XtraBars.BarButtonItem barPritnFeeList;
        private DevExpress.XtraBars.BarButtonItem barAddBillFromHistory;
        private DevExpress.XtraBars.BarSubItem barAdd;
        private DevExpress.XtraGrid.Columns.GridColumn colBizType;
        private DevExpress.XtraBars.BarToolbarsListItem barToolbarsListItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem barAddAR;
        private DevExpress.XtraBars.BarButtonItem barAddAP;
        private DevExpress.XtraBars.BarButtonItem barAddDC;
        private DevExpress.XtraGrid.Columns.GridColumn colBankDates;
        private DevExpress.XtraBars.BarButtonItem barAdditionalWF;
        private DevExpress.XtraEditors.TextEdit txtnoVatProfit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraBars.BarButtonItem ToRegenerateBill;
        private DevExpress.XtraBars.BarButtonItem barInvoiceContract;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckDates;
        private DevExpress.XtraBars.BarButtonItem barAddLocalFee;
        private DevExpress.XtraEditors.LabelControl labBulkCargoCast;
        private DevExpress.XtraEditors.TextEdit txtAdjustmentFee;
        private DevExpress.XtraBars.BarButtonItem barRegenerateDHF;
    }
}
