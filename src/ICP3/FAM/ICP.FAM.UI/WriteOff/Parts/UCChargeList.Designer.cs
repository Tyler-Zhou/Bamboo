namespace ICP.FAM.UI.WriteOff
{
    partial class UCChargeList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCChargeList));
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsCharges = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colWay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbWay = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imgType = new System.Windows.Forms.ImageList(this.components);
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChargeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbCurrencyID = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumAmount = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colStandardCurrencyRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumExchangeRate = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colStandardCurrencyAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cheComChargeName = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.cmbGL = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.btnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barSelectData = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.pnlTools = new DevExpress.XtraEditors.PanelControl();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.pnlLeft = new DevExpress.XtraEditors.PanelControl();
            this.labOther = new DevExpress.XtraEditors.LabelControl();
            this.txtTotalByCurrency = new DevExpress.XtraEditors.TextEdit();
            this.pnlBottom = new ICP.Framework.ClientComponents.UIManagement.PanelContainer();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labCurrency = new DevExpress.XtraEditors.LabelControl();
            this.cmbCurrency = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtPaymentAmount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtAmount = new DevExpress.XtraEditors.TextEdit();
            this.labARAmount = new DevExpress.XtraEditors.LabelControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colNumAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colNumExchangeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cheComChargeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTools)).BeginInit();
            this.pnlTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalByCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaymentAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsCharges;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(2, 35);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbCurrencyID,
            this.cheComChargeName,
            this.cmbGL,
            this.cmbWay,
            this.colNumAmount,
            this.colNumExchangeRate});
            this.gcMain.Size = new System.Drawing.Size(1000, 237);
            this.gcMain.TabIndex = 34;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsCharges
            // 
            this.bsCharges.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.WriteOffCharge);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colWay,
            this.colCustomerName,
            this.colBillNo,
            this.colChargeID,
            this.colCurrencyID,
            this.colAmount,
            this.colStandardCurrencyRate,
            this.colStandardCurrencyAmount,
            this.colRemark,
            this.colRate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvMain_CellValueChanged);
            this.gvMain.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvMain_CellValueChanging);
            // 
            // colWay
            // 
            this.colWay.Caption = "方向";
            this.colWay.ColumnEdit = this.cmbWay;
            this.colWay.FieldName = "Way";
            this.colWay.Name = "colWay";
            this.colWay.Visible = true;
            this.colWay.VisibleIndex = 0;
            this.colWay.Width = 48;
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
            // colCustomerName
            // 
            this.colCustomerName.Caption = "客户";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 1;
            this.colCustomerName.Width = 224;
            // 
            // colBillNo
            // 
            this.colBillNo.Caption = "账单号";
            this.colBillNo.FieldName = "BillNo";
            this.colBillNo.Name = "colBillNo";
            this.colBillNo.Visible = true;
            this.colBillNo.VisibleIndex = 2;
            this.colBillNo.Width = 77;
            // 
            // colChargeID
            // 
            this.colChargeID.Caption = "会计科目";
            this.colChargeID.FieldName = "GLFullName";
            this.colChargeID.Name = "colChargeID";
            this.colChargeID.Visible = true;
            this.colChargeID.VisibleIndex = 3;
            this.colChargeID.Width = 174;
            // 
            // colCurrencyID
            // 
            this.colCurrencyID.Caption = "币种";
            this.colCurrencyID.ColumnEdit = this.cmbCurrencyID;
            this.colCurrencyID.FieldName = "CurrencyID";
            this.colCurrencyID.Name = "colCurrencyID";
            this.colCurrencyID.Visible = true;
            this.colCurrencyID.VisibleIndex = 4;
            this.colCurrencyID.Width = 50;
            // 
            // cmbCurrencyID
            // 
            this.cmbCurrencyID.AutoHeight = false;
            this.cmbCurrencyID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrencyID.Name = "cmbCurrencyID";
            // 
            // colAmount
            // 
            this.colAmount.Caption = "金额";
            this.colAmount.ColumnEdit = this.colNumAmount;
            this.colAmount.DisplayFormat.FormatString = "N2";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 5;
            this.colAmount.Width = 71;
            // 
            // colNumAmount
            // 
            this.colNumAmount.AutoHeight = false;
            this.colNumAmount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.colNumAmount.DisplayFormat.FormatString = "N2";
            this.colNumAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNumAmount.Mask.EditMask = "N2";
            this.colNumAmount.MaxLength = 20;
            this.colNumAmount.Name = "colNumAmount";
            // 
            // colStandardCurrencyRate
            // 
            this.colStandardCurrencyRate.Caption = "汇率";
            this.colStandardCurrencyRate.ColumnEdit = this.colNumExchangeRate;
            this.colStandardCurrencyRate.DisplayFormat.FormatString = "N7";
            this.colStandardCurrencyRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colStandardCurrencyRate.FieldName = "StandardCurrencyRate";
            this.colStandardCurrencyRate.Name = "colStandardCurrencyRate";
            this.colStandardCurrencyRate.Visible = true;
            this.colStandardCurrencyRate.VisibleIndex = 6;
            // 
            // colNumExchangeRate
            // 
            this.colNumExchangeRate.AutoHeight = false;
            this.colNumExchangeRate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.colNumExchangeRate.DisplayFormat.FormatString = "F7";
            this.colNumExchangeRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNumExchangeRate.EditFormat.FormatString = "F7";
            this.colNumExchangeRate.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNumExchangeRate.MaxLength = 20;
            this.colNumExchangeRate.Name = "colNumExchangeRate";
            // 
            // colStandardCurrencyAmount
            // 
            this.colStandardCurrencyAmount.Caption = "金额(本位币)";
            this.colStandardCurrencyAmount.ColumnEdit = this.colNumAmount;
            this.colStandardCurrencyAmount.DisplayFormat.FormatString = "n";
            this.colStandardCurrencyAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colStandardCurrencyAmount.FieldName = "StandardCurrencyAmount";
            this.colStandardCurrencyAmount.Name = "colStandardCurrencyAmount";
            this.colStandardCurrencyAmount.OptionsColumn.AllowEdit = false;
            this.colStandardCurrencyAmount.Visible = true;
            this.colStandardCurrencyAmount.VisibleIndex = 7;
            this.colStandardCurrencyAmount.Width = 95;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "备注";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 8;
            this.colRemark.Width = 180;
            // 
            // colRate
            // 
            this.colRate.Caption = "汇率";
            this.colRate.ColumnEdit = this.colNumExchangeRate;
            this.colRate.DisplayFormat.FormatString = "N7";
            this.colRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRate.FieldName = "ExchangeRate";
            this.colRate.Name = "colRate";
            this.colRate.Width = 66;
            // 
            // cheComChargeName
            // 
            this.cheComChargeName.AutoHeight = false;
            this.cheComChargeName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cheComChargeName.Name = "cheComChargeName";
            // 
            // cmbGL
            // 
            this.cmbGL.AutoHeight = false;
            this.cmbGL.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGL.Name = "cmbGL";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this.pnlTools;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAdd,
            this.btnDelete,
            this.barSelectData});
            this.barManager1.MaxItemId = 3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDelete),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSelectData, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "新增";
            this.barAdd.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            this.barAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Caption = "删除";
            this.btnDelete.Glyph = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.btnDelete.Id = 1;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelete_ItemClick);
            // 
            // barSelectData
            // 
            this.barSelectData.Caption = "选择多收/多付";
            this.barSelectData.Glyph = global::ICP.FAM.UI.Properties.Resources.ChangeReleaseType;
            this.barSelectData.Id = 2;
            this.barSelectData.Name = "barSelectData";
            this.barSelectData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSelectData_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(915, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 27);
            this.barDockControlBottom.Size = new System.Drawing.Size(915, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 28);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(917, 28);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 0);
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.barDockControlLeft);
            this.pnlTools.Controls.Add(this.barDockControlRight);
            this.pnlTools.Controls.Add(this.barDockControlBottom);
            this.pnlTools.Controls.Add(this.barDockControlTop);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTools.Location = new System.Drawing.Point(79, 2);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(919, 29);
            this.pnlTools.TabIndex = 36;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.pnlTools);
            this.pnlTop.Controls.Add(this.pnlLeft);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(2, 2);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1000, 33);
            this.pnlTop.TabIndex = 35;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.labOther);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(2, 2);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(77, 29);
            this.pnlLeft.TabIndex = 92;
            // 
            // labOther
            // 
            this.labOther.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labOther.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labOther.Appearance.Options.UseFont = true;
            this.labOther.Appearance.Options.UseForeColor = true;
            this.labOther.Location = new System.Drawing.Point(7, 6);
            this.labOther.Name = "labOther";
            this.labOther.Size = new System.Drawing.Size(52, 14);
            this.labOther.TabIndex = 0;
            this.labOther.Text = "其他项目";
            // 
            // txtTotalByCurrency
            // 
            this.txtTotalByCurrency.EditValue = "0";
            this.txtTotalByCurrency.Location = new System.Drawing.Point(793, 5);
            this.txtTotalByCurrency.Name = "txtTotalByCurrency";
            this.txtTotalByCurrency.Properties.ReadOnly = true;
            this.txtTotalByCurrency.Size = new System.Drawing.Size(200, 21);
            this.txtTotalByCurrency.TabIndex = 77;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.labelControl3);
            this.pnlBottom.Controls.Add(this.labCurrency);
            this.pnlBottom.Controls.Add(this.cmbCurrency);
            this.pnlBottom.Controls.Add(this.txtPaymentAmount);
            this.pnlBottom.Controls.Add(this.labelControl1);
            this.pnlBottom.Controls.Add(this.txtAmount);
            this.pnlBottom.Controls.Add(this.labARAmount);
            this.pnlBottom.Controls.Add(this.txtTotalByCurrency);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 274);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1004, 31);
            this.pnlBottom.TabIndex = 82;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(718, 8);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 85;
            this.labelControl3.Text = "本位币汇总";
            // 
            // labCurrency
            // 
            this.labCurrency.Location = new System.Drawing.Point(722, 8);
            this.labCurrency.Name = "labCurrency";
            this.labCurrency.Size = new System.Drawing.Size(24, 14);
            this.labCurrency.TabIndex = 83;
            this.labCurrency.Text = "币种";
            this.labCurrency.Visible = false;
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.Location = new System.Drawing.Point(722, 5);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Size = new System.Drawing.Size(29, 21);
            this.cmbCurrency.TabIndex = 84;
            this.cmbCurrency.Visible = false;
            // 
            // txtPaymentAmount
            // 
            this.txtPaymentAmount.EditValue = "";
            this.txtPaymentAmount.Location = new System.Drawing.Point(469, 5);
            this.txtPaymentAmount.Name = "txtPaymentAmount";
            this.txtPaymentAmount.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPaymentAmount.Properties.Appearance.Options.UseForeColor = true;
            this.txtPaymentAmount.Properties.DisplayFormat.FormatString = "N";
            this.txtPaymentAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPaymentAmount.Properties.EditFormat.FormatString = "N";
            this.txtPaymentAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPaymentAmount.Properties.ReadOnly = true;
            this.txtPaymentAmount.Size = new System.Drawing.Size(234, 21);
            this.txtPaymentAmount.TabIndex = 82;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(392, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 81;
            this.labelControl1.Text = "预收预付合计";
            // 
            // txtAmount
            // 
            this.txtAmount.EditValue = "";
            this.txtAmount.Location = new System.Drawing.Point(47, 5);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtAmount.Properties.Appearance.Options.UseForeColor = true;
            this.txtAmount.Properties.DisplayFormat.FormatString = "N";
            this.txtAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAmount.Properties.EditFormat.FormatString = "N";
            this.txtAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAmount.Properties.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(323, 21);
            this.txtAmount.TabIndex = 82;
            // 
            // labARAmount
            // 
            this.labARAmount.Location = new System.Drawing.Point(11, 7);
            this.labARAmount.Name = "labARAmount";
            this.labARAmount.Size = new System.Drawing.Size(24, 14);
            this.labARAmount.TabIndex = 81;
            this.labARAmount.Text = "合计";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gcMain);
            this.pnlMain.Controls.Add(this.pnlTop);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1004, 274);
            this.pnlMain.TabIndex = 87;
            // 
            // UCChargeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Name = "UCChargeList";
            this.Size = new System.Drawing.Size(1004, 305);
            this.Load += new System.EventHandler(this.ChargeList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colNumAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colNumExchangeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cheComChargeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTools)).EndInit();
            this.pnlTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalByCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaymentAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colChargeID;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyID;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public System.Windows.Forms.BindingSource bsCharges;
        protected DevExpress.XtraEditors.TextEdit txtTotalByCurrency;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbCurrencyID;
        private ICP.Framework.ClientComponents.UIManagement.PanelContainer pnlBottom;
        private DevExpress.XtraEditors.TextEdit txtAmount;
        private DevExpress.XtraEditors.LabelControl labARAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit cheComChargeName;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbGL;
        private DevExpress.XtraGrid.Columns.GridColumn colBillNo;
        public DevExpress.XtraBars.BarButtonItem barAdd;
        public DevExpress.XtraBars.BarButtonItem btnDelete;
        private DevExpress.XtraGrid.Columns.GridColumn colWay;
        private System.Windows.Forms.ImageList imgType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbWay;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit colNumAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit colNumExchangeRate;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.PanelControl pnlLeft;
        private DevExpress.XtraEditors.PanelControl pnlTools;
        private DevExpress.XtraEditors.LabelControl labOther;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        protected DevExpress.XtraEditors.LabelControl labCurrency;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCurrency;
        private DevExpress.XtraBars.BarButtonItem barSelectData;
        private DevExpress.XtraEditors.TextEdit txtPaymentAmount;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colStandardCurrencyAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colStandardCurrencyRate;
    }
}
