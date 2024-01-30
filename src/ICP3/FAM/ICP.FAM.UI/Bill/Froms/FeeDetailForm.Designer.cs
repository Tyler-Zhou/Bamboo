namespace ICP.FAM.UI.Bill
{
    partial class FeeDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeeDetailForm));
            this.bsChargeList = new System.Windows.Forms.BindingSource(this.components);
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.groupFeeInfo = new System.Windows.Forms.GroupBox();
            this.gcChargeList = new DevExpress.XtraGrid.GridControl();
            this.gvChargeList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colWay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbWay = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imgType = new System.Windows.Forms.ImageList(this.components);
            this.colChargingCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reQuantity = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.seUnitPrice = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.rtxtRemark = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.cmbUnit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rcmbChargingCode = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtTotalByCurrency = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalAmount = new DevExpress.XtraEditors.TextEdit();
            this.labCurrency = new DevExpress.XtraEditors.LabelControl();
            this.cmbCurrency = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labTotalCount = new DevExpress.XtraEditors.LabelControl();
            this.labTotal = new DevExpress.XtraEditors.LabelControl();
            this.labBillNO = new DevExpress.XtraEditors.LabelControl();
            this.txtBillNO = new DevExpress.XtraEditors.TextEdit();
            this.bsBill = new System.Windows.Forms.BindingSource(this.components);
            this.labCurrencyName = new DevExpress.XtraEditors.LabelControl();
            this.txtCurrencyName = new DevExpress.XtraEditors.TextEdit();
            this.labCustomerName = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.labAmount = new DevExpress.XtraEditors.LabelControl();
            this.seAmount = new DevExpress.XtraEditors.SpinEdit();
            this.labWriteOffAmount = new DevExpress.XtraEditors.LabelControl();
            this.seWriteOffAmount = new DevExpress.XtraEditors.SpinEdit();
            this.labBillRefNO = new DevExpress.XtraEditors.LabelControl();
            this.txtBillRefNO = new DevExpress.XtraEditors.TextEdit();
            this.labCheckBy = new DevExpress.XtraEditors.LabelControl();
            this.txtCheckBy = new DevExpress.XtraEditors.TextEdit();
            this.labCheckDate = new DevExpress.XtraEditors.LabelControl();
            this.dteCheckDate = new DevExpress.XtraEditors.DateEdit();
            this.labBankDate = new DevExpress.XtraEditors.LabelControl();
            this.dteBankDate = new DevExpress.XtraEditors.DateEdit();
            this.labInvoiceNo = new DevExpress.XtraEditors.LabelControl();
            this.txtInvoiceNo = new DevExpress.XtraEditors.TextEdit();
            this.labState = new DevExpress.XtraEditors.LabelControl();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.cmbState = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bsChargeList)).BeginInit();
            this.groupFeeInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcChargeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChargeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seUnitPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbChargingCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalByCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seWriteOffAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillRefNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCheckBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCheckDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCheckDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBankDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBankDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bsChargeList
            // 
            this.bsChargeList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.ChargeList);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(777, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupFeeInfo
            // 
            this.groupFeeInfo.Controls.Add(this.gcChargeList);
            this.groupFeeInfo.Controls.Add(this.panelControl1);
            this.groupFeeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupFeeInfo.Location = new System.Drawing.Point(0, 78);
            this.groupFeeInfo.Name = "groupFeeInfo";
            this.groupFeeInfo.Size = new System.Drawing.Size(864, 381);
            this.groupFeeInfo.TabIndex = 1;
            this.groupFeeInfo.TabStop = false;
            this.groupFeeInfo.Text = "费用信息";
            // 
            // gcChargeList
            // 
            this.gcChargeList.DataSource = this.bsChargeList;
            this.gcChargeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcChargeList.Location = new System.Drawing.Point(3, 18);
            this.gcChargeList.MainView = this.gvChargeList;
            this.gcChargeList.Name = "gcChargeList";
            this.gcChargeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbWay,
            this.reQuantity,
            this.seUnitPrice,
            this.rtxtRemark,
            this.cmbUnit,
            this.rcmbChargingCode});
            this.gcChargeList.Size = new System.Drawing.Size(858, 329);
            this.gcChargeList.TabIndex = 7;
            this.gcChargeList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvChargeList});
            // 
            // gvChargeList
            // 
            this.gvChargeList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colWay,
            this.colChargingCode,
            this.colCurrencyName,
            this.colAmount});
            this.gvChargeList.GridControl = this.gcChargeList;
            this.gvChargeList.Name = "gvChargeList";
            this.gvChargeList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvChargeList.OptionsSelection.MultiSelect = true;
            this.gvChargeList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvChargeList.OptionsView.ShowGroupPanel = false;
            // 
            // colWay
            // 
            this.colWay.Caption = "方向";
            this.colWay.ColumnEdit = this.cmbWay;
            this.colWay.FieldName = "Way";
            this.colWay.Name = "colWay";
            this.colWay.OptionsColumn.AllowEdit = false;
            this.colWay.Visible = true;
            this.colWay.VisibleIndex = 0;
            this.colWay.Width = 98;
            // 
            // cmbWay
            // 
            this.cmbWay.AutoHeight = false;
            this.cmbWay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWay.Name = "cmbWay";
            this.cmbWay.SmallImages = this.imgType;
            // 
            // imgType
            // 
            this.imgType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgType.ImageStream")));
            this.imgType.TransparentColor = System.Drawing.Color.Transparent;
            this.imgType.Images.SetKeyName(0, "+.png");
            this.imgType.Images.SetKeyName(1, "-.png");
            // 
            // colChargingCode
            // 
            this.colChargingCode.Caption = "费用名称";
            this.colChargingCode.FieldName = "ChargingCode";
            this.colChargingCode.Name = "colChargingCode";
            this.colChargingCode.OptionsColumn.AllowEdit = false;
            this.colChargingCode.Visible = true;
            this.colChargingCode.VisibleIndex = 1;
            this.colChargingCode.Width = 260;
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.Caption = "币种";
            this.colCurrencyName.FieldName = "CurrencyName";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.OptionsColumn.AllowEdit = false;
            this.colCurrencyName.Visible = true;
            this.colCurrencyName.VisibleIndex = 2;
            this.colCurrencyName.Width = 100;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "金额";
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 3;
            this.colAmount.Width = 379;
            // 
            // reQuantity
            // 
            this.reQuantity.AutoHeight = false;
            this.reQuantity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.reQuantity.IsFloatValue = false;
            this.reQuantity.Mask.EditMask = "N00";
            this.reQuantity.Name = "reQuantity";
            // 
            // seUnitPrice
            // 
            this.seUnitPrice.AutoHeight = false;
            this.seUnitPrice.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seUnitPrice.Mask.EditMask = "F3";
            this.seUnitPrice.Name = "seUnitPrice";
            // 
            // rtxtRemark
            // 
            this.rtxtRemark.AutoHeight = false;
            this.rtxtRemark.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rtxtRemark.Name = "rtxtRemark";
            this.rtxtRemark.ShowIcon = false;
            // 
            // cmbUnit
            // 
            this.cmbUnit.AutoHeight = false;
            this.cmbUnit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUnit.Name = "cmbUnit";
            // 
            // rcmbChargingCode
            // 
            this.rcmbChargingCode.AutoHeight = false;
            this.rcmbChargingCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbChargingCode.Name = "rcmbChargingCode";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.txtTotalByCurrency);
            this.panelControl1.Controls.Add(this.txtTotalAmount);
            this.panelControl1.Controls.Add(this.labCurrency);
            this.panelControl1.Controls.Add(this.cmbCurrency);
            this.panelControl1.Controls.Add(this.labTotalCount);
            this.panelControl1.Controls.Add(this.labTotal);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(3, 347);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(858, 31);
            this.panelControl1.TabIndex = 0;
            // 
            // txtTotalByCurrency
            // 
            this.txtTotalByCurrency.EditValue = "";
            this.txtTotalByCurrency.Location = new System.Drawing.Point(623, 5);
            this.txtTotalByCurrency.Name = "txtTotalByCurrency";
            this.txtTotalByCurrency.Properties.ReadOnly = true;
            this.txtTotalByCurrency.Size = new System.Drawing.Size(141, 21);
            this.txtTotalByCurrency.TabIndex = 3;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.EditValue = "";
            this.txtTotalAmount.Location = new System.Drawing.Point(161, 5);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Properties.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(330, 21);
            this.txtTotalAmount.TabIndex = 1;
            // 
            // labCurrency
            // 
            this.labCurrency.Location = new System.Drawing.Point(518, 8);
            this.labCurrency.Name = "labCurrency";
            this.labCurrency.Size = new System.Drawing.Size(24, 14);
            this.labCurrency.TabIndex = 72;
            this.labCurrency.Text = "币种";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.Location = new System.Drawing.Point(549, 6);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Size = new System.Drawing.Size(65, 21);
            this.cmbCurrency.TabIndex = 2;
           
            this.cmbCurrency.SelectedIndexChanged += this.cmbCurrency_SelectedIndexChanged;
            // 
            // labTotalCount
            // 
            this.labTotalCount.Location = new System.Drawing.Point(2, 8);
            this.labTotalCount.Name = "labTotalCount";
            this.labTotalCount.Size = new System.Drawing.Size(63, 14);
            this.labTotalCount.TabIndex = 5;
            this.labTotalCount.Text = "共 0 条数据";
            // 
            // labTotal
            // 
            this.labTotal.Location = new System.Drawing.Point(119, 8);
            this.labTotal.Name = "labTotal";
            this.labTotal.Size = new System.Drawing.Size(36, 14);
            this.labTotal.TabIndex = 70;
            this.labTotal.Text = "合计：";
            // 
            // labBillNO
            // 
            this.labBillNO.Location = new System.Drawing.Point(11, 8);
            this.labBillNO.Name = "labBillNO";
            this.labBillNO.Size = new System.Drawing.Size(36, 14);
            this.labBillNO.TabIndex = 38;
            this.labBillNO.Text = "账单号";
            // 
            // txtBillNO
            // 
            this.txtBillNO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBill, "BillNO", true));
            this.txtBillNO.Location = new System.Drawing.Point(69, 5);
            this.txtBillNO.Name = "txtBillNO";
            this.txtBillNO.Properties.ReadOnly = true;
            this.txtBillNO.Size = new System.Drawing.Size(135, 21);
            this.txtBillNO.TabIndex = 0;
            // 
            // bsBill
            // 
            this.bsBill.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.CurrencyBillList);
            // 
            // labCurrencyName
            // 
            this.labCurrencyName.Location = new System.Drawing.Point(420, 8);
            this.labCurrencyName.Name = "labCurrencyName";
            this.labCurrencyName.Size = new System.Drawing.Size(24, 14);
            this.labCurrencyName.TabIndex = 44;
            this.labCurrencyName.Text = "币种";
            // 
            // txtCurrencyName
            // 
            this.txtCurrencyName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBill, "CurrencyName", true));
            this.txtCurrencyName.Location = new System.Drawing.Point(482, 5);
            this.txtCurrencyName.Name = "txtCurrencyName";
            this.txtCurrencyName.Properties.ReadOnly = true;
            this.txtCurrencyName.Size = new System.Drawing.Size(135, 21);
            this.txtCurrencyName.TabIndex = 2;
            // 
            // labCustomerName
            // 
            this.labCustomerName.Location = new System.Drawing.Point(11, 32);
            this.labCustomerName.Name = "labCustomerName";
            this.labCustomerName.Size = new System.Drawing.Size(24, 14);
            this.labCustomerName.TabIndex = 46;
            this.labCustomerName.Text = "客户";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBill, "CustomerName", true));
            this.txtCustomerName.Location = new System.Drawing.Point(69, 29);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Properties.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(339, 21);
            this.txtCustomerName.TabIndex = 4;
            // 
            // labAmount
            // 
            this.labAmount.Location = new System.Drawing.Point(420, 35);
            this.labAmount.Name = "labAmount";
            this.labAmount.Size = new System.Drawing.Size(24, 14);
            this.labAmount.TabIndex = 48;
            this.labAmount.Text = "金额";
            // 
            // seAmount
            // 
            this.seAmount.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBill, "Amount", true));
            this.seAmount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seAmount.Location = new System.Drawing.Point(482, 29);
            this.seAmount.Name = "seAmount";
            this.seAmount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seAmount.Properties.ReadOnly = true;
            this.seAmount.Size = new System.Drawing.Size(135, 21);
            this.seAmount.TabIndex = 5;
            // 
            // labWriteOffAmount
            // 
            this.labWriteOffAmount.Location = new System.Drawing.Point(420, 56);
            this.labWriteOffAmount.Name = "labWriteOffAmount";
            this.labWriteOffAmount.Size = new System.Drawing.Size(48, 14);
            this.labWriteOffAmount.TabIndex = 50;
            this.labWriteOffAmount.Text = "销帐金额";
            // 
            // seWriteOffAmount
            // 
            this.seWriteOffAmount.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBill, "WriteOffAmount", true));
            this.seWriteOffAmount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seWriteOffAmount.Location = new System.Drawing.Point(482, 53);
            this.seWriteOffAmount.Name = "seWriteOffAmount";
            this.seWriteOffAmount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seWriteOffAmount.Properties.ReadOnly = true;
            this.seWriteOffAmount.Size = new System.Drawing.Size(135, 21);
            this.seWriteOffAmount.TabIndex = 9;
            // 
            // labBillRefNO
            // 
            this.labBillRefNO.Location = new System.Drawing.Point(210, 8);
            this.labBillRefNO.Name = "labBillRefNO";
            this.labBillRefNO.Size = new System.Drawing.Size(36, 14);
            this.labBillRefNO.TabIndex = 52;
            this.labBillRefNO.Text = "参考号";
            // 
            // txtBillRefNO
            // 
            this.txtBillRefNO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBill, "BillRefNO", true));
            this.txtBillRefNO.Location = new System.Drawing.Point(273, 5);
            this.txtBillRefNO.Name = "txtBillRefNO";
            this.txtBillRefNO.Properties.ReadOnly = true;
            this.txtBillRefNO.Size = new System.Drawing.Size(135, 21);
            this.txtBillRefNO.TabIndex = 1;
            // 
            // labCheckBy
            // 
            this.labCheckBy.Location = new System.Drawing.Point(628, 8);
            this.labCheckBy.Name = "labCheckBy";
            this.labCheckBy.Size = new System.Drawing.Size(36, 14);
            this.labCheckBy.TabIndex = 54;
            this.labCheckBy.Text = "审核人";
            // 
            // txtCheckBy
            // 
            this.txtCheckBy.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBill, "CheckBy", true));
            this.txtCheckBy.Location = new System.Drawing.Point(702, 5);
            this.txtCheckBy.Name = "txtCheckBy";
            this.txtCheckBy.Properties.ReadOnly = true;
            this.txtCheckBy.Size = new System.Drawing.Size(135, 21);
            this.txtCheckBy.TabIndex = 3;
            // 
            // labCheckDate
            // 
            this.labCheckDate.Location = new System.Drawing.Point(628, 32);
            this.labCheckDate.Name = "labCheckDate";
            this.labCheckDate.Size = new System.Drawing.Size(48, 14);
            this.labCheckDate.TabIndex = 56;
            this.labCheckDate.Text = "审核日期";
            // 
            // dteCheckDate
            // 
            this.dteCheckDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBill, "CheckDate", true));
            this.dteCheckDate.EditValue = null;
            this.dteCheckDate.Enabled = false;
            this.dteCheckDate.EnterMoveNextControl = true;
            this.dteCheckDate.Location = new System.Drawing.Point(702, 29);
            this.dteCheckDate.Name = "dteCheckDate";
            this.dteCheckDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteCheckDate.Properties.Mask.EditMask = "";
            this.dteCheckDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteCheckDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteCheckDate.Size = new System.Drawing.Size(135, 21);
            this.dteCheckDate.TabIndex = 6;
            // 
            // labBankDate
            // 
            this.labBankDate.Location = new System.Drawing.Point(628, 56);
            this.labBankDate.Name = "labBankDate";
            this.labBankDate.Size = new System.Drawing.Size(48, 14);
            this.labBankDate.TabIndex = 62;
            this.labBankDate.Text = "账单日期";
            // 
            // dteBankDate
            // 
            this.dteBankDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBill, "AccountDate", true));
            this.dteBankDate.EditValue = null;
            this.dteBankDate.Enabled = false;
            this.dteBankDate.EnterMoveNextControl = true;
            this.dteBankDate.Location = new System.Drawing.Point(702, 53);
            this.dteBankDate.Name = "dteBankDate";
            this.dteBankDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteBankDate.Properties.Mask.EditMask = "";
            this.dteBankDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteBankDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteBankDate.Size = new System.Drawing.Size(135, 21);
            this.dteBankDate.TabIndex = 10;
            // 
            // labInvoiceNo
            // 
            this.labInvoiceNo.Location = new System.Drawing.Point(211, 56);
            this.labInvoiceNo.Name = "labInvoiceNo";
            this.labInvoiceNo.Size = new System.Drawing.Size(36, 14);
            this.labInvoiceNo.TabIndex = 66;
            this.labInvoiceNo.Text = "发票号";
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBill, "InvoiceNo", true));
            this.txtInvoiceNo.Location = new System.Drawing.Point(273, 53);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Properties.ReadOnly = true;
            this.txtInvoiceNo.Size = new System.Drawing.Size(134, 21);
            this.txtInvoiceNo.TabIndex = 8;
            // 
            // labState
            // 
            this.labState.Location = new System.Drawing.Point(11, 56);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(24, 14);
            this.labState.TabIndex = 68;
            this.labState.Text = "状态";
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.cmbState);
            this.pnlTop.Controls.Add(this.labBillNO);
            this.pnlTop.Controls.Add(this.txtBillNO);
            this.pnlTop.Controls.Add(this.labState);
            this.pnlTop.Controls.Add(this.labCurrencyName);
            this.pnlTop.Controls.Add(this.txtInvoiceNo);
            this.pnlTop.Controls.Add(this.txtCurrencyName);
            this.pnlTop.Controls.Add(this.labInvoiceNo);
            this.pnlTop.Controls.Add(this.labCustomerName);
            this.pnlTop.Controls.Add(this.dteBankDate);
            this.pnlTop.Controls.Add(this.txtCustomerName);
            this.pnlTop.Controls.Add(this.labBankDate);
            this.pnlTop.Controls.Add(this.labAmount);
            this.pnlTop.Controls.Add(this.dteCheckDate);
            this.pnlTop.Controls.Add(this.seAmount);
            this.pnlTop.Controls.Add(this.labCheckDate);
            this.pnlTop.Controls.Add(this.labWriteOffAmount);
            this.pnlTop.Controls.Add(this.txtCheckBy);
            this.pnlTop.Controls.Add(this.seWriteOffAmount);
            this.pnlTop.Controls.Add(this.labCheckBy);
            this.pnlTop.Controls.Add(this.labBillRefNO);
            this.pnlTop.Controls.Add(this.txtBillRefNO);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(864, 78);
            this.pnlTop.TabIndex = 0;
            // 
            // cmbState
            // 
            this.cmbState.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBill, "State", true));
            this.cmbState.Enabled = false;
            this.cmbState.Location = new System.Drawing.Point(69, 54);
            this.cmbState.Name = "cmbState";
            this.cmbState.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbState.Properties.Appearance.Options.UseBackColor = true;
            this.cmbState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbState.Size = new System.Drawing.Size(135, 21);
            this.cmbState.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbState.TabIndex = 7;
            // 
            // FeeDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.groupFeeInfo);
            this.Controls.Add(this.pnlTop);
            this.Name = "FeeDetailForm";
            this.Size = new System.Drawing.Size(864, 459);
            ((System.ComponentModel.ISupportInitialize)(this.bsChargeList)).EndInit();
            this.groupFeeInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcChargeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChargeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seUnitPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbChargingCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalByCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seWriteOffAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillRefNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCheckBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCheckDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCheckDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBankDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBankDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.GroupBox groupFeeInfo;
        private System.Windows.Forms.BindingSource bsBill;
        private System.Windows.Forms.BindingSource bsChargeList;
        private DevExpress.XtraGrid.GridControl gcChargeList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvChargeList;
        private DevExpress.XtraGrid.Columns.GridColumn colWay;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbWay;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbChargingCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit seUnitPrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit reQuantity;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit rtxtRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colChargingCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraEditors.LabelControl labBillNO;
        private DevExpress.XtraEditors.TextEdit txtBillNO;
        private DevExpress.XtraEditors.LabelControl labCurrencyName;
        private DevExpress.XtraEditors.TextEdit txtCurrencyName;
        private DevExpress.XtraEditors.LabelControl labCustomerName;
        private DevExpress.XtraEditors.TextEdit txtCustomerName;
        private DevExpress.XtraEditors.LabelControl labAmount;
        private DevExpress.XtraEditors.SpinEdit seAmount;
        private DevExpress.XtraEditors.LabelControl labWriteOffAmount;
        private DevExpress.XtraEditors.SpinEdit seWriteOffAmount;
        private DevExpress.XtraEditors.LabelControl labBillRefNO;
        private DevExpress.XtraEditors.TextEdit txtBillRefNO;
        private DevExpress.XtraEditors.LabelControl labCheckBy;
        private DevExpress.XtraEditors.TextEdit txtCheckBy;
        private DevExpress.XtraEditors.LabelControl labBankDate;
        private DevExpress.XtraEditors.DateEdit dteBankDate;
        private DevExpress.XtraEditors.LabelControl labInvoiceNo;
        private DevExpress.XtraEditors.TextEdit txtInvoiceNo;
        private DevExpress.XtraEditors.LabelControl labCheckDate;
        private DevExpress.XtraEditors.DateEdit dteCheckDate;
        private DevExpress.XtraEditors.LabelControl labState;
        protected DevExpress.XtraEditors.TextEdit txtTotalByCurrency;
        protected DevExpress.XtraEditors.LabelControl labCurrency;
        protected DevExpress.XtraEditors.LabelControl labTotal;
        protected DevExpress.XtraEditors.TextEdit txtTotalAmount;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCurrency;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        protected DevExpress.XtraEditors.LabelControl labTotalCount;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbState;
        private System.Windows.Forms.ImageList imgType;
    }
}
