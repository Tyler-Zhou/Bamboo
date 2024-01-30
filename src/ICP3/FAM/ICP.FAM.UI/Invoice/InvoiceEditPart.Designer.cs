using ICP.Framework.ClientComponents.Controls;
namespace ICP.FAM.UI
{
    partial class InvoiceEditPart
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceEditPart));
            this.gvFee = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBillFeeFeeSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillFeeChargingCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillFeeCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillFeeAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBill = new DevExpress.XtraGrid.GridControl();
            this.bsBill = new System.Windows.Forms.BindingSource(this.components);
            this.gvBill = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.labTitleEName = new DevExpress.XtraEditors.LabelControl();
            this.labTitleCName = new DevExpress.XtraEditors.LabelControl();
            this.labSONo = new DevExpress.XtraEditors.LabelControl();
            this.txtTitleEName = new DevExpress.XtraEditors.TextEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txtSONo = new DevExpress.XtraEditors.TextEdit();
            this.labCtn = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labBank1 = new DevExpress.XtraEditors.LabelControl();
            this.labBank2 = new DevExpress.XtraEditors.LabelControl();
            this.labTax = new DevExpress.XtraEditors.LabelControl();
            this.seTax = new DevExpress.XtraEditors.SpinEdit();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.labPlaceOfDelivery = new DevExpress.XtraEditors.LabelControl();
            this.labVesselVoyage = new DevExpress.XtraEditors.LabelControl();
            this.labContainerNo = new DevExpress.XtraEditors.LabelControl();
            this.txtContainerNo = new DevExpress.XtraEditors.TextEdit();
            this.labInvoiceNo = new DevExpress.XtraEditors.LabelControl();
            this.labInvoiceDate = new DevExpress.XtraEditors.LabelControl();
            this.dteInvoiceDate = new DevExpress.XtraEditors.DateEdit();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labBLNo = new DevExpress.XtraEditors.LabelControl();
            this.txtBLNo = new DevExpress.XtraEditors.TextEdit();
            this.labETD = new DevExpress.XtraEditors.LabelControl();
            this.dteETD = new DevExpress.XtraEditors.DateEdit();
            this.chkIsValid = new DevExpress.XtraEditors.CheckEdit();
            this.txtCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barGetInvoiceNo = new DevExpress.XtraBars.BarButtonItem();
            this.barEditInvoiceNo = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomerSync = new DevExpress.XtraBars.BarButtonItem();
            this.barImportTaxInfo = new DevExpress.XtraBars.BarButtonItem();
            this.barExpressNo = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barAddFee = new DevExpress.XtraBars.BarButtonItem();
            this.barRemoveFee = new DevExpress.XtraBars.BarButtonItem();
            this.barClearFee = new DevExpress.XtraBars.BarButtonItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.txtBillTotal = new DevExpress.XtraEditors.TextEdit();
            this.labBillTotal = new DevExpress.XtraEditors.LabelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupBoxBill = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDeleteBill = new DevExpress.XtraEditors.SimpleButton();
            this.labSelectBill = new DevExpress.XtraEditors.LabelControl();
            this.txtSelectBill = new DevExpress.XtraEditors.ButtonEdit();
            this.groupBoxFee = new System.Windows.Forms.GroupBox();
            this.gcChargeList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsInvoiceFeeDate = new System.Windows.Forms.BindingSource(this.components);
            this.gvChargeList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChargingCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.curCureency_ = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reQuantity = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtRemark = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.cmbWay = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.cmbCurrency = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.seUnitPrice = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.cmbUnit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rseAmount = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this._curRate = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtFeeTotal = new DevExpress.XtraEditors.TextEdit();
            this.labFeeTotal = new DevExpress.XtraEditors.LabelControl();
            this.labVoyage = new DevExpress.XtraEditors.LabelControl();
            this.txtInvoiceNo = new DevExpress.XtraEditors.TextEdit();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.cmbBank2 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbCompany = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtPOL = new DevExpress.XtraEditors.ButtonEdit();
            this.txtPOD = new DevExpress.XtraEditors.ButtonEdit();
            this.txtVessel = new DevExpress.XtraEditors.ButtonEdit();
            this.txtPlaceOfDelivery = new DevExpress.XtraEditors.ButtonEdit();
            this.cmbBank1 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbCtnType = new ICP.Framework.ClientComponents.Controls.ContainerDemandControl();
            this.txtVoyage = new DevExpress.XtraEditors.TextEdit();
            this.bsChargeList = new System.Windows.Forms.BindingSource(this.components);
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.bgInvoceHader = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cmbInvoiceType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbReceivables = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.cmbReview = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.cmbCustomerBankAccountNo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbInvoiceTitleName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtCustomerAddressTel = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbCustomerTaxIDNo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.txtExpressNo = new DevExpress.XtraEditors.TextEdit();
            this.labReview = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labReceivables = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labTaxNo = new DevExpress.XtraEditors.LabelControl();
            this.labCustomerAddressTel = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.bgBusinessInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgDetail = new DevExpress.XtraNavBar.NavBarGroup();
            this.vScrollBar1 = new DevExpress.XtraEditors.VScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.gvFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitleEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSONo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContainerNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInvoiceDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInvoiceDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBLNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsValid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.groupBoxBill.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectBill.Properties)).BeginInit();
            this.groupBoxFee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcChargeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoiceFeeDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChargeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.curCureency_)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seUnitPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rseAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._curRate)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFeeTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBank2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVessel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBank1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoyage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsChargeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInvoiceType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerBankAccountNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInvoiceTitleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerAddressTel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerTaxIDNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.navBarGroupControlContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvFee
            // 
            this.gvFee.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBillFeeFeeSelected,
            this.colBillFeeChargingCode,
            this.colBillFeeCurrencyName,
            this.colBillFeeAmount});
            this.gvFee.GridControl = this.gcBill;
            this.gvFee.HorzScrollStep = 1;
            this.gvFee.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvFee.Name = "gvFee";
            this.gvFee.OptionsBehavior.Editable = false;
            this.gvFee.OptionsSelection.MultiSelect = true;
            this.gvFee.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvFee.OptionsView.EnableAppearanceEvenRow = true;
            this.gvFee.OptionsView.ShowDetailButtons = false;
            this.gvFee.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gvFee.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvFee.OptionsView.ShowGroupPanel = false;
            this.gvFee.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvFee_RowCellClick);
            // 
            // colBillFeeFeeSelected
            // 
            this.colBillFeeFeeSelected.Caption = " Selected";
            this.colBillFeeFeeSelected.FieldName = "Selected";
            this.colBillFeeFeeSelected.Name = "colBillFeeFeeSelected";
            this.colBillFeeFeeSelected.OptionsColumn.AllowEdit = false;
            this.colBillFeeFeeSelected.Visible = true;
            this.colBillFeeFeeSelected.VisibleIndex = 0;
            this.colBillFeeFeeSelected.Width = 45;
            // 
            // colBillFeeChargingCode
            // 
            this.colBillFeeChargingCode.Caption = "ChargingCode";
            this.colBillFeeChargingCode.FieldName = "ChargingCode";
            this.colBillFeeChargingCode.Name = "colBillFeeChargingCode";
            this.colBillFeeChargingCode.OptionsColumn.AllowEdit = false;
            this.colBillFeeChargingCode.Visible = true;
            this.colBillFeeChargingCode.VisibleIndex = 1;
            this.colBillFeeChargingCode.Width = 124;
            // 
            // colBillFeeCurrencyName
            // 
            this.colBillFeeCurrencyName.Caption = "Currency";
            this.colBillFeeCurrencyName.FieldName = "CurrencyName";
            this.colBillFeeCurrencyName.Name = "colBillFeeCurrencyName";
            this.colBillFeeCurrencyName.OptionsColumn.AllowEdit = false;
            this.colBillFeeCurrencyName.Visible = true;
            this.colBillFeeCurrencyName.VisibleIndex = 2;
            this.colBillFeeCurrencyName.Width = 124;
            // 
            // colBillFeeAmount
            // 
            this.colBillFeeAmount.FieldName = "Amount";
            this.colBillFeeAmount.Name = "colBillFeeAmount";
            this.colBillFeeAmount.OptionsColumn.AllowEdit = false;
            this.colBillFeeAmount.Visible = true;
            this.colBillFeeAmount.VisibleIndex = 3;
            this.colBillFeeAmount.Width = 134;
            // 
            // gcBill
            // 
            this.gcBill.DataSource = this.bsBill;
            this.gcBill.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.LevelTemplate = this.gvFee;
            gridLevelNode1.RelationName = "Fees";
            this.gcBill.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcBill.Location = new System.Drawing.Point(3, 49);
            this.gcBill.MainView = this.gvBill;
            this.gcBill.Name = "gcBill";
            this.gcBill.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcBill.Size = new System.Drawing.Size(400, 173);
            this.gcBill.TabIndex = 0;
            this.gcBill.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBill,
            this.gvFee});
            this.gcBill.DataSourceChanged += new System.EventHandler(this.gcBill_DataSourceChanged);
            // 
            // bsBill
            // 
            this.bsBill.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.BillList);
            this.bsBill.PositionChanged += new System.EventHandler(this.bsBill_PositionChanged);
            // 
            // gvBill
            // 
            this.gvBill.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNo,
            this.colCustomerName});
            this.gvBill.GridControl = this.gcBill;
            this.gvBill.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvBill.Name = "gvBill";
            this.gvBill.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvBill.OptionsBehavior.Editable = false;
            this.gvBill.OptionsSelection.MultiSelect = true;
            this.gvBill.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvBill.OptionsView.EnableAppearanceEvenRow = true;
            this.gvBill.OptionsView.RowAutoHeight = true;
            this.gvBill.OptionsView.ShowGroupPanel = false;
            // 
            // colNo
            // 
            this.colNo.Caption = "No";
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.OptionsColumn.AllowEdit = false;
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 0;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Customer";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.OptionsColumn.AllowEdit = false;
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 1;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // labTitleEName
            // 
            this.labTitleEName.Location = new System.Drawing.Point(27, 153);
            this.labTitleEName.Name = "labTitleEName";
            this.labTitleEName.Size = new System.Drawing.Size(66, 14);
            this.labTitleEName.TabIndex = 68;
            this.labTitleEName.Text = "Title EName";
            this.labTitleEName.Visible = false;
            // 
            // labTitleCName
            // 
            this.labTitleCName.Location = new System.Drawing.Point(340, 31);
            this.labTitleCName.Name = "labTitleCName";
            this.labTitleCName.Size = new System.Drawing.Size(66, 14);
            this.labTitleCName.TabIndex = 68;
            this.labTitleCName.Text = "Title CName";
            // 
            // labSONo
            // 
            this.labSONo.Location = new System.Drawing.Point(247, 27);
            this.labSONo.Name = "labSONo";
            this.labSONo.Size = new System.Drawing.Size(31, 14);
            this.labSONo.TabIndex = 68;
            this.labSONo.Text = "SONo";
            // 
            // txtTitleEName
            // 
            this.txtTitleEName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "TitleEName", true));
            this.txtTitleEName.Location = new System.Drawing.Point(115, 150);
            this.txtTitleEName.Name = "txtTitleEName";
            this.txtTitleEName.Size = new System.Drawing.Size(224, 21);
            this.txtTitleEName.TabIndex = 5;
            this.txtTitleEName.Visible = false;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.InvoiceInfo);
            // 
            // txtSONo
            // 
            this.txtSONo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "SONo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSONo.Location = new System.Drawing.Point(334, 24);
            this.txtSONo.Name = "txtSONo";
            this.txtSONo.Size = new System.Drawing.Size(228, 21);
            this.txtSONo.TabIndex = 5;
            // 
            // labCtn
            // 
            this.labCtn.Location = new System.Drawing.Point(247, 74);
            this.labCtn.Name = "labCtn";
            this.labCtn.Size = new System.Drawing.Size(80, 14);
            this.labCtn.TabIndex = 72;
            this.labCtn.Text = "ContainerType";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(10, 4);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 74;
            this.labCompany.Text = "Company";
            // 
            // labBank1
            // 
            this.labBank1.Location = new System.Drawing.Point(10, 27);
            this.labBank1.Name = "labBank1";
            this.labBank1.Size = new System.Drawing.Size(33, 14);
            this.labBank1.TabIndex = 78;
            this.labBank1.Text = "Bank1";
            // 
            // labBank2
            // 
            this.labBank2.Location = new System.Drawing.Point(10, 51);
            this.labBank2.Name = "labBank2";
            this.labBank2.Size = new System.Drawing.Size(33, 14);
            this.labBank2.TabIndex = 82;
            this.labBank2.Text = "Bank2";
            // 
            // labTax
            // 
            this.labTax.Location = new System.Drawing.Point(10, 74);
            this.labTax.Name = "labTax";
            this.labTax.Size = new System.Drawing.Size(20, 14);
            this.labTax.TabIndex = 86;
            this.labTax.Text = "Tax";
            // 
            // seTax
            // 
            this.seTax.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Tax", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.seTax.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seTax.Location = new System.Drawing.Point(72, 71);
            this.seTax.Name = "seTax";
            this.seTax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seTax.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.seTax.Size = new System.Drawing.Size(160, 21);
            this.seTax.TabIndex = 3;
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(10, 98);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(40, 14);
            this.labRemark.TabIndex = 88;
            this.labRemark.Text = "Remark";
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(568, 4);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(53, 14);
            this.labPOL.TabIndex = 90;
            this.labPOL.Text = "POLName";
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(568, 27);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(55, 14);
            this.labPOD.TabIndex = 92;
            this.labPOD.Text = "PODName";
            // 
            // labPlaceOfDelivery
            // 
            this.labPlaceOfDelivery.Location = new System.Drawing.Point(568, 51);
            this.labPlaceOfDelivery.Name = "labPlaceOfDelivery";
            this.labPlaceOfDelivery.Size = new System.Drawing.Size(83, 14);
            this.labPlaceOfDelivery.TabIndex = 94;
            this.labPlaceOfDelivery.Text = "PlaceOfDelivery";
            // 
            // labVesselVoyage
            // 
            this.labVesselVoyage.Location = new System.Drawing.Point(888, 27);
            this.labVesselVoyage.Name = "labVesselVoyage";
            this.labVesselVoyage.Size = new System.Drawing.Size(34, 14);
            this.labVesselVoyage.TabIndex = 102;
            this.labVesselVoyage.Text = "Vessel";
            // 
            // labContainerNo
            // 
            this.labContainerNo.Location = new System.Drawing.Point(247, 51);
            this.labContainerNo.Name = "labContainerNo";
            this.labContainerNo.Size = new System.Drawing.Size(67, 14);
            this.labContainerNo.TabIndex = 106;
            this.labContainerNo.Text = "ContainerNo";
            // 
            // txtContainerNo
            // 
            this.txtContainerNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ContainerNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtContainerNo.Location = new System.Drawing.Point(334, 48);
            this.txtContainerNo.Name = "txtContainerNo";
            this.txtContainerNo.Size = new System.Drawing.Size(228, 21);
            this.txtContainerNo.TabIndex = 6;
            // 
            // labInvoiceNo
            // 
            this.labInvoiceNo.Location = new System.Drawing.Point(10, 31);
            this.labInvoiceNo.Name = "labInvoiceNo";
            this.labInvoiceNo.Size = new System.Drawing.Size(48, 14);
            this.labInvoiceNo.TabIndex = 108;
            this.labInvoiceNo.Text = "发票号码";
            // 
            // labInvoiceDate
            // 
            this.labInvoiceDate.Location = new System.Drawing.Point(178, 31);
            this.labInvoiceDate.Name = "labInvoiceDate";
            this.labInvoiceDate.Size = new System.Drawing.Size(48, 14);
            this.labInvoiceDate.TabIndex = 110;
            this.labInvoiceDate.Text = "发票日期";
            // 
            // dteInvoiceDate
            // 
            this.dteInvoiceDate.CausesValidation = false;
            this.dteInvoiceDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "InvoiceDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteInvoiceDate.EditValue = null;
            this.dteInvoiceDate.Location = new System.Drawing.Point(236, 28);
            this.dteInvoiceDate.Name = "dteInvoiceDate";
            this.dteInvoiceDate.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.dteInvoiceDate.Properties.Appearance.Options.UseBackColor = true;
            this.dteInvoiceDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteInvoiceDate.Properties.Mask.EditMask = "";
            this.dteInvoiceDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteInvoiceDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteInvoiceDate.Size = new System.Drawing.Size(87, 21);
            this.dteInvoiceDate.TabIndex = 3;
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(340, 6);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 112;
            this.labCustomer.Text = "Customer";
            // 
            // labBLNo
            // 
            this.labBLNo.Location = new System.Drawing.Point(247, 4);
            this.labBLNo.Name = "labBLNo";
            this.labBLNo.Size = new System.Drawing.Size(28, 14);
            this.labBLNo.TabIndex = 117;
            this.labBLNo.Text = "BLNo";
            // 
            // txtBLNo
            // 
            this.txtBLNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "BLNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBLNo.Location = new System.Drawing.Point(334, 1);
            this.txtBLNo.Name = "txtBLNo";
            this.txtBLNo.Size = new System.Drawing.Size(228, 21);
            this.txtBLNo.TabIndex = 4;
            // 
            // labETD
            // 
            this.labETD.Location = new System.Drawing.Point(888, 4);
            this.labETD.Name = "labETD";
            this.labETD.Size = new System.Drawing.Size(23, 14);
            this.labETD.TabIndex = 119;
            this.labETD.Text = "ETD";
            // 
            // dteETD
            // 
            this.dteETD.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ETD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteETD.EditValue = null;
            this.dteETD.Location = new System.Drawing.Point(952, 1);
            this.dteETD.Name = "dteETD";
            this.dteETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteETD.Properties.Mask.EditMask = "";
            this.dteETD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteETD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteETD.Size = new System.Drawing.Size(151, 21);
            this.dteETD.TabIndex = 11;
            // 
            // chkIsValid
            // 
            this.chkIsValid.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsValid", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsValid.EditValue = true;
            this.chkIsValid.Location = new System.Drawing.Point(954, 72);
            this.chkIsValid.Name = "chkIsValid";
            this.chkIsValid.Properties.Caption = "IsValid";
            this.chkIsValid.Size = new System.Drawing.Size(149, 19);
            this.chkIsValid.TabIndex = 14;
            // 
            // txtCustomer
            // 
            this.txtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CustomerName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "CustomerID", true));
            this.txtCustomer.Location = new System.Drawing.Point(415, 3);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.txtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomer.Size = new System.Drawing.Size(254, 21);
            this.txtCustomer.TabIndex = 6;
            this.txtCustomer.TabStop = false;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave,
            this.barPrint,
            this.barEditInvoiceNo,
            this.barClose,
            this.barClearFee,
            this.barRemoveFee,
            this.barAddFee,
            this.barGetInvoiceNo,
            this.barImportTaxInfo,
            this.barExpressNo,
            this.btnCustomerSync});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 11;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barGetInvoiceNo, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEditInvoiceNo, "", false, true, false, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnCustomerSync, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barImportTaxInfo, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barExpressNo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = global::ICP.FAM.UI.Properties.Resources.Save_Blue_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barGetInvoiceNo
            // 
            this.barGetInvoiceNo.Caption = "获得发票号";
            this.barGetInvoiceNo.Glyph = global::ICP.FAM.UI.Properties.Resources.Review;
            this.barGetInvoiceNo.Id = 7;
            this.barGetInvoiceNo.Name = "barGetInvoiceNo";
            this.barGetInvoiceNo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barGetInvoiceNo_ItemClick);
            // 
            // barEditInvoiceNo
            // 
            this.barEditInvoiceNo.Caption = "&Edit InvoiceNo";
            this.barEditInvoiceNo.Glyph = global::ICP.FAM.UI.Properties.Resources.Edit_16;
            this.barEditInvoiceNo.Id = 2;
            this.barEditInvoiceNo.Name = "barEditInvoiceNo";
            this.barEditInvoiceNo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEditInvoiceNo_ItemClick);
            // 
            // btnCustomerSync
            // 
            this.btnCustomerSync.Caption = "客户资料同步";
            this.btnCustomerSync.Glyph = global::ICP.FAM.UI.Properties.Resources.ChangeReleaseType;
            this.btnCustomerSync.Id = 10;
            this.btnCustomerSync.Name = "btnCustomerSync";
            this.btnCustomerSync.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnCustomerSync.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomerSync_ItemClick);
            // 
            // barImportTaxInfo
            // 
            this.barImportTaxInfo.Caption = "导入开票系统信息";
            this.barImportTaxInfo.Glyph = global::ICP.FAM.UI.Properties.Resources.ChangeReleaseType;
            this.barImportTaxInfo.Id = 8;
            this.barImportTaxInfo.Name = "barImportTaxInfo";
            this.barImportTaxInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barImportTaxInfo_ItemClick);
            // 
            // barExpressNo
            // 
            this.barExpressNo.Caption = "设置快递号";
            this.barExpressNo.Glyph = global::ICP.FAM.UI.Properties.Resources.Telex;
            this.barExpressNo.Id = 9;
            this.barExpressNo.Name = "barExpressNo";
            this.barExpressNo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExpressNo_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "&Print";
            this.barPrint.Glyph = global::ICP.FAM.UI.Properties.Resources.Preview;
            this.barPrint.Id = 1;
            this.barPrint.Name = "barPrint";
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 3;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 3";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.FloatLocation = new System.Drawing.Point(900, 406);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAddFee, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRemoveFee, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClearFee, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Custom 3";
            // 
            // barAddFee
            // 
            this.barAddFee.Caption = "&AddFee";
            this.barAddFee.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_16;
            this.barAddFee.Id = 6;
            this.barAddFee.Name = "barAddFee";
            this.barAddFee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddFee_ItemClick);
            // 
            // barRemoveFee
            // 
            this.barRemoveFee.Caption = "&RemoveFee";
            this.barRemoveFee.Enabled = false;
            this.barRemoveFee.Glyph = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.barRemoveFee.Id = 5;
            this.barRemoveFee.Name = "barRemoveFee";
            this.barRemoveFee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRemoveFee_ItemClick);
            // 
            // barClearFee
            // 
            this.barClearFee.Caption = "C&learFee";
            this.barClearFee.Enabled = false;
            this.barClearFee.Glyph = global::ICP.FAM.UI.Properties.Resources.Empty;
            this.barClearFee.Id = 4;
            this.barClearFee.Name = "barClearFee";
            this.barClearFee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClearFee_ItemClick);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(3, 18);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(697, 26);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1119, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 596);
            this.barDockControlBottom.Size = new System.Drawing.Size(1119, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 570);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1119, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 570);
            // 
            // txtBillTotal
            // 
            this.txtBillTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBillTotal.Location = new System.Drawing.Point(94, 2);
            this.txtBillTotal.MenuManager = this.barManager1;
            this.txtBillTotal.Name = "txtBillTotal";
            this.txtBillTotal.Properties.ReadOnly = true;
            this.txtBillTotal.Size = new System.Drawing.Size(298, 21);
            this.txtBillTotal.TabIndex = 1;
            // 
            // labBillTotal
            // 
            this.labBillTotal.Location = new System.Drawing.Point(7, 7);
            this.labBillTotal.Name = "labBillTotal";
            this.labBillTotal.Size = new System.Drawing.Size(32, 14);
            this.labBillTotal.TabIndex = 0;
            this.labBillTotal.Text = "Total:";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupBoxBill);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupBoxFee);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1115, 250);
            this.splitContainerControl1.SplitterPosition = 406;
            this.splitContainerControl1.TabIndex = 138;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupBoxBill
            // 
            this.groupBoxBill.Controls.Add(this.gcBill);
            this.groupBoxBill.Controls.Add(this.panel1);
            this.groupBoxBill.Controls.Add(this.panel2);
            this.groupBoxBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxBill.Location = new System.Drawing.Point(0, 0);
            this.groupBoxBill.Name = "groupBoxBill";
            this.groupBoxBill.Size = new System.Drawing.Size(406, 250);
            this.groupBoxBill.TabIndex = 0;
            this.groupBoxBill.TabStop = false;
            this.groupBoxBill.Text = "Bill";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtBillTotal);
            this.panel1.Controls.Add(this.labBillTotal);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 222);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 25);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDeleteBill);
            this.panel2.Controls.Add(this.labSelectBill);
            this.panel2.Controls.Add(this.txtSelectBill);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(400, 31);
            this.panel2.TabIndex = 0;
            // 
            // btnDeleteBill
            // 
            this.btnDeleteBill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteBill.Location = new System.Drawing.Point(317, 4);
            this.btnDeleteBill.Name = "btnDeleteBill";
            this.btnDeleteBill.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteBill.TabIndex = 1;
            this.btnDeleteBill.Text = "&Delete Bill";
            this.btnDeleteBill.Click += new System.EventHandler(this.btnDeleteBill_Click);
            // 
            // labSelectBill
            // 
            this.labSelectBill.Location = new System.Drawing.Point(7, 8);
            this.labSelectBill.Name = "labSelectBill";
            this.labSelectBill.Size = new System.Drawing.Size(51, 14);
            this.labSelectBill.TabIndex = 88;
            this.labSelectBill.Text = "Select Bill";
            // 
            // txtSelectBill
            // 
            this.txtSelectBill.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSelectBill.Location = new System.Drawing.Point(94, 5);
            this.txtSelectBill.Name = "txtSelectBill";
            this.txtSelectBill.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtSelectBill.Size = new System.Drawing.Size(207, 21);
            this.txtSelectBill.TabIndex = 0;
            this.txtSelectBill.TabStop = false;
            // 
            // groupBoxFee
            // 
            this.groupBoxFee.Controls.Add(this.gcChargeList);
            this.groupBoxFee.Controls.Add(this.standaloneBarDockControl1);
            this.groupBoxFee.Controls.Add(this.panel3);
            this.groupBoxFee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFee.Location = new System.Drawing.Point(0, 0);
            this.groupBoxFee.Name = "groupBoxFee";
            this.groupBoxFee.Size = new System.Drawing.Size(703, 250);
            this.groupBoxFee.TabIndex = 0;
            this.groupBoxFee.TabStop = false;
            this.groupBoxFee.Text = "Fee";
            // 
            // gcChargeList
            // 
            this.gcChargeList.DataSource = this.bsInvoiceFeeDate;
            this.gcChargeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcChargeList.Location = new System.Drawing.Point(3, 44);
            this.gcChargeList.MainView = this.gvChargeList;
            this.gcChargeList.MenuManager = this.barManager1;
            this.gcChargeList.Name = "gcChargeList";
            this.gcChargeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbWay,
            this.cmbCurrency,
            this.reQuantity,
            this.seUnitPrice,
            this.rtxtRemark,
            this.cmbUnit,
            this.rseAmount,
            this.repositoryItemCheckEdit2,
            this.btnEdit1,
            this.curCureency_,
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemTextEdit3,
            this._curRate,
            this.txtRate});
            this.gcChargeList.Size = new System.Drawing.Size(697, 178);
            this.gcChargeList.TabIndex = 0;
            this.gcChargeList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvChargeList});
            // 
            // bsInvoiceFeeDate
            // 
            this.bsInvoiceFeeDate.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.InvoiceFeeDate);
            this.bsInvoiceFeeDate.PositionChanged += new System.EventHandler(this.bsInvoiceFeeDate_PositionChanged);
            // 
            // gvChargeList
            // 
            this.gvChargeList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChargingCode,
            this.colCurrencyName,
            this.colRate,
            this.colQuantity,
            this.colAmount,
            this.colRemark});
            this.gvChargeList.GridControl = this.gcChargeList;
            this.gvChargeList.Name = "gvChargeList";
            this.gvChargeList.OptionsSelection.MultiSelect = true;
            this.gvChargeList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvChargeList.OptionsView.ShowGroupPanel = false;
            this.gvChargeList.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvChargeList_CellValueChanged);
            this.gvChargeList.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvChargeList_CellValueChanging);
            // 
            // colChargingCode
            // 
            this.colChargingCode.Caption = "Charging";
            this.colChargingCode.ColumnEdit = this.btnEdit1;
            this.colChargingCode.FieldName = "ChargingCode";
            this.colChargingCode.Name = "colChargingCode";
            this.colChargingCode.Visible = true;
            this.colChargingCode.VisibleIndex = 0;
            this.colChargingCode.Width = 81;
            // 
            // btnEdit1
            // 
            this.btnEdit1.AutoHeight = false;
            this.btnEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEdit1.Name = "btnEdit1";
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.Caption = "CurrencyName";
            this.colCurrencyName.ColumnEdit = this.curCureency_;
            this.colCurrencyName.FieldName = "CurrencyID";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.Visible = true;
            this.colCurrencyName.VisibleIndex = 1;
            // 
            // curCureency_
            // 
            this.curCureency_.AutoHeight = false;
            this.curCureency_.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.curCureency_.Name = "curCureency_";
            // 
            // colRate
            // 
            this.colRate.Caption = "Rate";
            this.colRate.ColumnEdit = this.txtRate;
            this.colRate.DisplayFormat.FormatString = "F4";
            this.colRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRate.FieldName = "Rate";
            this.colRate.Name = "colRate";
            this.colRate.OptionsFilter.AllowAutoFilter = false;
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 2;
            this.colRate.Width = 79;
            // 
            // txtRate
            // 
            this.txtRate.AutoHeight = false;
            this.txtRate.Name = "txtRate";
            // 
            // colQuantity
            // 
            this.colQuantity.Caption = "Quantity";
            this.colQuantity.ColumnEdit = this.reQuantity;
            this.colQuantity.DisplayFormat.FormatString = "F2";
            this.colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 3;
            this.colQuantity.Width = 81;
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
            // colAmount
            // 
            this.colAmount.AppearanceCell.BackColor = System.Drawing.Color.LightBlue;
            this.colAmount.AppearanceCell.Options.UseBackColor = true;
            this.colAmount.Caption = "Amount";
            this.colAmount.ColumnEdit = this.repositoryItemTextEdit3;
            this.colAmount.DisplayFormat.FormatString = "F2";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 4;
            this.colAmount.Width = 79;
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // colRemark
            // 
            this.colRemark.Caption = "Remark";
            this.colRemark.ColumnEdit = this.rtxtRemark;
            this.colRemark.CustomizationCaption = "Remark";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 5;
            this.colRemark.Width = 80;
            // 
            // rtxtRemark
            // 
            this.rtxtRemark.AutoHeight = false;
            this.rtxtRemark.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rtxtRemark.Name = "rtxtRemark";
            this.rtxtRemark.ShowIcon = false;
            // 
            // cmbWay
            // 
            this.cmbWay.AutoHeight = false;
            this.cmbWay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWay.Name = "cmbWay";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.AutoHeight = false;
            this.cmbCurrency.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Name = "cmbCurrency";
            // 
            // seUnitPrice
            // 
            this.seUnitPrice.AutoHeight = false;
            this.seUnitPrice.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seUnitPrice.Mask.EditMask = "F3";
            this.seUnitPrice.Name = "seUnitPrice";
            // 
            // cmbUnit
            // 
            this.cmbUnit.AutoHeight = false;
            this.cmbUnit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUnit.Name = "cmbUnit";
            // 
            // rseAmount
            // 
            this.rseAmount.AutoHeight = false;
            this.rseAmount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rseAmount.Mask.EditMask = "F2";
            this.rseAmount.Name = "rseAmount";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // _curRate
            // 
            this._curRate.AutoHeight = false;
            this._curRate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._curRate.Name = "_curRate";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtFeeTotal);
            this.panel3.Controls.Add(this.labFeeTotal);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 222);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(697, 25);
            this.panel3.TabIndex = 5;
            // 
            // txtFeeTotal
            // 
            this.txtFeeTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFeeTotal.Location = new System.Drawing.Point(94, 2);
            this.txtFeeTotal.MenuManager = this.barManager1;
            this.txtFeeTotal.Name = "txtFeeTotal";
            this.txtFeeTotal.Properties.ReadOnly = true;
            this.txtFeeTotal.Size = new System.Drawing.Size(595, 21);
            this.txtFeeTotal.TabIndex = 1;
            // 
            // labFeeTotal
            // 
            this.labFeeTotal.Location = new System.Drawing.Point(7, 7);
            this.labFeeTotal.Name = "labFeeTotal";
            this.labFeeTotal.Size = new System.Drawing.Size(32, 14);
            this.labFeeTotal.TabIndex = 0;
            this.labFeeTotal.Text = "Total:";
            // 
            // labVoyage
            // 
            this.labVoyage.Location = new System.Drawing.Point(888, 51);
            this.labVoyage.Name = "labVoyage";
            this.labVoyage.Size = new System.Drawing.Size(41, 14);
            this.labVoyage.TabIndex = 121;
            this.labVoyage.Text = "Voyage";
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "InvoiceNo", true));
            this.txtInvoiceNo.Location = new System.Drawing.Point(64, 28);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(107, 21);
            this.txtInvoiceNo.TabIndex = 2;
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Remark", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtRemark.Location = new System.Drawing.Point(72, 95);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(1031, 35);
            this.txtRemark.TabIndex = 15;
            this.txtRemark.TabStop = false;
            // 
            // cmbBank2
            // 
            this.cmbBank2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Bank2ID", true));
            this.cmbBank2.Location = new System.Drawing.Point(72, 48);
            this.cmbBank2.Name = "cmbBank2";
            this.cmbBank2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBank2.Size = new System.Drawing.Size(160, 21);
            this.cmbBank2.TabIndex = 2;
            this.cmbBank2.TabStop = false;
            this.cmbBank2.SelectedIndexChanged += new System.EventHandler(this.cmbBank2_SelectedIndexChanged);
            // 
            // cmbCompany
            // 
            this.cmbCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CompanyID", true));
            this.cmbCompany.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "CompanyName", true));
            this.cmbCompany.Location = new System.Drawing.Point(72, 1);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(160, 21);
            this.cmbCompany.TabIndex = 0;
            this.cmbCompany.TabStop = false;
            this.cmbCompany.SelectedValueChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // txtPOL
            // 
            this.txtPOL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "POLName", true));
            this.txtPOL.Location = new System.Drawing.Point(675, 1);
            this.txtPOL.Name = "txtPOL";
            this.txtPOL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPOL.Size = new System.Drawing.Size(191, 21);
            this.txtPOL.TabIndex = 8;
            this.txtPOL.TabStop = false;
            // 
            // txtPOD
            // 
            this.txtPOD.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PODName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPOD.Location = new System.Drawing.Point(675, 24);
            this.txtPOD.Name = "txtPOD";
            this.txtPOD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPOD.Size = new System.Drawing.Size(191, 21);
            this.txtPOD.TabIndex = 9;
            this.txtPOD.TabStop = false;
            // 
            // txtVessel
            // 
            this.txtVessel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Vessel", true));
            this.txtVessel.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "Vessel", true));
            this.txtVessel.Location = new System.Drawing.Point(952, 24);
            this.txtVessel.Name = "txtVessel";
            this.txtVessel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtVessel.Size = new System.Drawing.Size(151, 21);
            this.txtVessel.TabIndex = 12;
            this.txtVessel.TabStop = false;
            // 
            // txtPlaceOfDelivery
            // 
            this.txtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PlaceOfDeliveryName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPlaceOfDelivery.Location = new System.Drawing.Point(675, 48);
            this.txtPlaceOfDelivery.Name = "txtPlaceOfDelivery";
            this.txtPlaceOfDelivery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPlaceOfDelivery.Size = new System.Drawing.Size(191, 21);
            this.txtPlaceOfDelivery.TabIndex = 10;
            this.txtPlaceOfDelivery.TabStop = false;
            // 
            // cmbBank1
            // 
            this.cmbBank1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Bank1ID", true));
            this.cmbBank1.Location = new System.Drawing.Point(72, 24);
            this.cmbBank1.Name = "cmbBank1";
            this.cmbBank1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbBank1.Properties.Appearance.Options.UseBackColor = true;
            this.cmbBank1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBank1.Size = new System.Drawing.Size(160, 21);
            this.cmbBank1.TabIndex = 1;
            this.cmbBank1.TabStop = false;
            this.cmbBank1.SelectedIndexChanged += new System.EventHandler(this.cmbBank1_SelectedIndexChanged);
            // 
            // cmbCtnType
            // 
            this.cmbCtnType.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.cmbCtnType.CausesValidation = false;
            this.cmbCtnType.Location = new System.Drawing.Point(334, 71);
            this.cmbCtnType.Name = "cmbCtnType";
            this.cmbCtnType.Size = new System.Drawing.Size(532, 21);
            this.cmbCtnType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCtnType.TabIndex = 7;
            // 
            // txtVoyage
            // 
            this.txtVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "Voyage", true));
            this.txtVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Voyage", true));
            this.txtVoyage.Location = new System.Drawing.Point(952, 48);
            this.txtVoyage.Name = "txtVoyage";
            this.txtVoyage.Size = new System.Drawing.Size(151, 21);
            this.txtVoyage.TabIndex = 13;
            this.txtVoyage.TabStop = false;
            // 
            // bsChargeList
            // 
            this.bsChargeList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.ChargeList);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bsChargeList;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.bgInvoceHader;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.bgInvoceHader,
            this.bgBusinessInfo,
            this.bgDetail});
            this.navBarControl1.Location = new System.Drawing.Point(0, 26);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 1063;
            this.navBarControl1.Size = new System.Drawing.Size(1119, 570);
            this.navBarControl1.TabIndex = 15;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // bgInvoceHader
            // 
            this.bgInvoceHader.Caption = "发票抬头";
            this.bgInvoceHader.ControlContainer = this.navBarGroupControlContainer2;
            this.bgInvoceHader.Expanded = true;
            this.bgInvoceHader.GroupClientHeight = 77;
            this.bgInvoceHader.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgInvoceHader.Name = "bgInvoceHader";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.cmbInvoiceType);
            this.navBarGroupControlContainer2.Controls.Add(this.cmbReceivables);
            this.navBarGroupControlContainer2.Controls.Add(this.cmbReview);
            this.navBarGroupControlContainer2.Controls.Add(this.cmbCustomerBankAccountNo);
            this.navBarGroupControlContainer2.Controls.Add(this.cmbInvoiceTitleName);
            this.navBarGroupControlContainer2.Controls.Add(this.txtCustomerAddressTel);
            this.navBarGroupControlContainer2.Controls.Add(this.cmbCustomerTaxIDNo);
            this.navBarGroupControlContainer2.Controls.Add(this.txtCustomer);
            this.navBarGroupControlContainer2.Controls.Add(this.txtNo);
            this.navBarGroupControlContainer2.Controls.Add(this.txtExpressNo);
            this.navBarGroupControlContainer2.Controls.Add(this.txtInvoiceNo);
            this.navBarGroupControlContainer2.Controls.Add(this.labReview);
            this.navBarGroupControlContainer2.Controls.Add(this.labelControl3);
            this.navBarGroupControlContainer2.Controls.Add(this.labReceivables);
            this.navBarGroupControlContainer2.Controls.Add(this.labInvoiceDate);
            this.navBarGroupControlContainer2.Controls.Add(this.labNo);
            this.navBarGroupControlContainer2.Controls.Add(this.labCustomer);
            this.navBarGroupControlContainer2.Controls.Add(this.labelControl2);
            this.navBarGroupControlContainer2.Controls.Add(this.labelControl1);
            this.navBarGroupControlContainer2.Controls.Add(this.labInvoiceNo);
            this.navBarGroupControlContainer2.Controls.Add(this.txtTitleEName);
            this.navBarGroupControlContainer2.Controls.Add(this.dateEdit1);
            this.navBarGroupControlContainer2.Controls.Add(this.labTitleEName);
            this.navBarGroupControlContainer2.Controls.Add(this.dteInvoiceDate);
            this.navBarGroupControlContainer2.Controls.Add(this.labelControl4);
            this.navBarGroupControlContainer2.Controls.Add(this.labTaxNo);
            this.navBarGroupControlContainer2.Controls.Add(this.labCustomerAddressTel);
            this.navBarGroupControlContainer2.Controls.Add(this.labTitleCName);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(1115, 75);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // cmbInvoiceType
            // 
            this.cmbInvoiceType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "InvoiceType", true));
            this.cmbInvoiceType.Location = new System.Drawing.Point(238, 3);
            this.cmbInvoiceType.MenuManager = this.barManager1;
            this.cmbInvoiceType.Name = "cmbInvoiceType";
            this.cmbInvoiceType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbInvoiceType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbInvoiceType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbInvoiceType.Size = new System.Drawing.Size(86, 21);
            this.cmbInvoiceType.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbInvoiceType.TabIndex = 11;
            // 
            // cmbReceivables
            // 
            this.cmbReceivables.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bindingSource1, "ReceivablesName", true));
            this.cmbReceivables.EditText = "";
            this.cmbReceivables.EditValue = null;
            this.cmbReceivables.Location = new System.Drawing.Point(767, 53);
            this.cmbReceivables.Name = "cmbReceivables";
            this.cmbReceivables.ReadOnly = false;
            this.cmbReceivables.RefreshButtonToolTip = "";
            this.cmbReceivables.ShowRefreshButton = false;
            this.cmbReceivables.Size = new System.Drawing.Size(141, 21);
            this.cmbReceivables.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbReceivables.TabIndex = 11;
            this.cmbReceivables.ToolTip = "";
            // 
            // cmbReview
            // 
            this.cmbReview.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bindingSource1, "ReviewName", true));
            this.cmbReview.EditText = "";
            this.cmbReview.EditValue = null;
            this.cmbReview.Location = new System.Drawing.Point(982, 54);
            this.cmbReview.Name = "cmbReview";
            this.cmbReview.ReadOnly = false;
            this.cmbReview.RefreshButtonToolTip = "";
            this.cmbReview.ShowRefreshButton = false;
            this.cmbReview.Size = new System.Drawing.Size(123, 21);
            this.cmbReview.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbReview.TabIndex = 12;
            this.cmbReview.ToolTip = "";
            // 
            // cmbCustomerBankAccountNo
            // 
            this.cmbCustomerBankAccountNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CustomerBankAccountNo", true));
            this.cmbCustomerBankAccountNo.EditValue = "";
            this.cmbCustomerBankAccountNo.Location = new System.Drawing.Point(767, 28);
            this.cmbCustomerBankAccountNo.Name = "cmbCustomerBankAccountNo";
            this.cmbCustomerBankAccountNo.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbCustomerBankAccountNo.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCustomerBankAccountNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCustomerBankAccountNo.Size = new System.Drawing.Size(337, 21);
            this.cmbCustomerBankAccountNo.TabIndex = 10;
            this.cmbCustomerBankAccountNo.SelectedIndexChanged += new System.EventHandler(this.cmbCustomerBankAccountNo_SelectedIndexChanged);
            // 
            // cmbInvoiceTitleName
            // 
            this.cmbInvoiceTitleName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "TitleCName", true));
            this.cmbInvoiceTitleName.EditValue = "";
            this.cmbInvoiceTitleName.Location = new System.Drawing.Point(415, 28);
            this.cmbInvoiceTitleName.Name = "cmbInvoiceTitleName";
            this.cmbInvoiceTitleName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbInvoiceTitleName.Properties.Appearance.Options.UseBackColor = true;
            this.cmbInvoiceTitleName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbInvoiceTitleName.Size = new System.Drawing.Size(254, 21);
            this.cmbInvoiceTitleName.TabIndex = 7;
            this.cmbInvoiceTitleName.SelectedValueChanged += new System.EventHandler(this.cmbInvoiceTitleName_SelectedValueChanged);
            // 
            // txtCustomerAddressTel
            // 
            this.txtCustomerAddressTel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CustomerAddressTel", true));
            this.txtCustomerAddressTel.EditValue = "";
            this.txtCustomerAddressTel.Location = new System.Drawing.Point(767, 3);
            this.txtCustomerAddressTel.Name = "txtCustomerAddressTel";
            this.txtCustomerAddressTel.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtCustomerAddressTel.Properties.Appearance.Options.UseBackColor = true;
            this.txtCustomerAddressTel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCustomerAddressTel.Size = new System.Drawing.Size(336, 21);
            this.txtCustomerAddressTel.TabIndex = 9;
            // 
            // cmbCustomerTaxIDNo
            // 
            this.cmbCustomerTaxIDNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CustomerTaxIDNo", true));
            this.cmbCustomerTaxIDNo.EditValue = "";
            this.cmbCustomerTaxIDNo.Location = new System.Drawing.Point(415, 53);
            this.cmbCustomerTaxIDNo.MenuManager = this.barManager1;
            this.cmbCustomerTaxIDNo.Name = "cmbCustomerTaxIDNo";
            this.cmbCustomerTaxIDNo.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbCustomerTaxIDNo.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCustomerTaxIDNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCustomerTaxIDNo.Size = new System.Drawing.Size(254, 21);
            this.cmbCustomerTaxIDNo.TabIndex = 8;
            // 
            // txtNo
            // 
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "No", true));
            this.txtNo.Location = new System.Drawing.Point(64, 3);
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.ReadOnly = true;
            this.txtNo.Size = new System.Drawing.Size(108, 21);
            this.txtNo.TabIndex = 0;
            // 
            // txtExpressNo
            // 
            this.txtExpressNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ExpressNo", true));
            this.txtExpressNo.Location = new System.Drawing.Point(64, 53);
            this.txtExpressNo.Name = "txtExpressNo";
            this.txtExpressNo.Size = new System.Drawing.Size(107, 21);
            this.txtExpressNo.TabIndex = 4;
            // 
            // labReview
            // 
            this.labReview.Location = new System.Drawing.Point(932, 57);
            this.labReview.Name = "labReview";
            this.labReview.Size = new System.Drawing.Size(24, 14);
            this.labReview.TabIndex = 110;
            this.labReview.Text = "复核";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(178, 56);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 110;
            this.labelControl3.Text = "快递日期";
            // 
            // labReceivables
            // 
            this.labReceivables.Location = new System.Drawing.Point(675, 56);
            this.labReceivables.Name = "labReceivables";
            this.labReceivables.Size = new System.Drawing.Size(24, 14);
            this.labReceivables.TabIndex = 110;
            this.labReceivables.Text = "收款";
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(10, 6);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(48, 14);
            this.labNo.TabIndex = 108;
            this.labNo.Text = "系统单号";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 56);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 108;
            this.labelControl2.Text = "快递单号";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(178, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 108;
            this.labelControl1.Text = "发票类型";
            // 
            // dateEdit1
            // 
            this.dateEdit1.CausesValidation = false;
            this.dateEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ExpressDate", true));
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(234, 53);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.dateEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.Mask.EditMask = "";
            this.dateEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateEdit1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit1.Size = new System.Drawing.Size(87, 21);
            this.dateEdit1.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(675, 31);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(72, 14);
            this.labelControl4.TabIndex = 68;
            this.labelControl4.Text = "客户银行帐号";
            // 
            // labTaxNo
            // 
            this.labTaxNo.Location = new System.Drawing.Point(340, 56);
            this.labTaxNo.Name = "labTaxNo";
            this.labTaxNo.Size = new System.Drawing.Size(60, 14);
            this.labTaxNo.TabIndex = 68;
            this.labTaxNo.Text = "客户税务号";
            // 
            // labCustomerAddressTel
            // 
            this.labCustomerAddressTel.Location = new System.Drawing.Point(675, 6);
            this.labCustomerAddressTel.Name = "labCustomerAddressTel";
            this.labCustomerAddressTel.Size = new System.Drawing.Size(77, 14);
            this.labCustomerAddressTel.TabIndex = 68;
            this.labCustomerAddressTel.Text = "客户地址/电话";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.cmbBank1);
            this.navBarGroupControlContainer1.Controls.Add(this.labVoyage);
            this.navBarGroupControlContainer1.Controls.Add(this.txtVoyage);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbCtnType);
            this.navBarGroupControlContainer1.Controls.Add(this.txtPlaceOfDelivery);
            this.navBarGroupControlContainer1.Controls.Add(this.labTax);
            this.navBarGroupControlContainer1.Controls.Add(this.labBank2);
            this.navBarGroupControlContainer1.Controls.Add(this.txtContainerNo);
            this.navBarGroupControlContainer1.Controls.Add(this.txtVessel);
            this.navBarGroupControlContainer1.Controls.Add(this.txtPOD);
            this.navBarGroupControlContainer1.Controls.Add(this.labSONo);
            this.navBarGroupControlContainer1.Controls.Add(this.seTax);
            this.navBarGroupControlContainer1.Controls.Add(this.labContainerNo);
            this.navBarGroupControlContainer1.Controls.Add(this.labBank1);
            this.navBarGroupControlContainer1.Controls.Add(this.txtRemark);
            this.navBarGroupControlContainer1.Controls.Add(this.labBLNo);
            this.navBarGroupControlContainer1.Controls.Add(this.txtPOL);
            this.navBarGroupControlContainer1.Controls.Add(this.labRemark);
            this.navBarGroupControlContainer1.Controls.Add(this.labVesselVoyage);
            this.navBarGroupControlContainer1.Controls.Add(this.labCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbBank2);
            this.navBarGroupControlContainer1.Controls.Add(this.chkIsValid);
            this.navBarGroupControlContainer1.Controls.Add(this.txtBLNo);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.labPOL);
            this.navBarGroupControlContainer1.Controls.Add(this.labPlaceOfDelivery);
            this.navBarGroupControlContainer1.Controls.Add(this.labCtn);
            this.navBarGroupControlContainer1.Controls.Add(this.labETD);
            this.navBarGroupControlContainer1.Controls.Add(this.dteETD);
            this.navBarGroupControlContainer1.Controls.Add(this.txtSONo);
            this.navBarGroupControlContainer1.Controls.Add(this.labPOD);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(1115, 130);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.splitContainerControl1);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(1115, 250);
            this.navBarGroupControlContainer3.TabIndex = 2;
            // 
            // bgBusinessInfo
            // 
            this.bgBusinessInfo.Caption = "业务信息 ";
            this.bgBusinessInfo.ControlContainer = this.navBarGroupControlContainer1;
            this.bgBusinessInfo.Expanded = true;
            this.bgBusinessInfo.GroupClientHeight = 132;
            this.bgBusinessInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgBusinessInfo.Name = "bgBusinessInfo";
            // 
            // bgDetail
            // 
            this.bgDetail.Caption = "明细";
            this.bgDetail.ControlContainer = this.navBarGroupControlContainer3;
            this.bgDetail.Expanded = true;
            this.bgDetail.GroupClientHeight = 252;
            this.bgDetail.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgDetail.Name = "bgDetail";
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(3, 23);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(1060, 556);
            this.vScrollBar1.TabIndex = 20;
            // 
            // InvoiceEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.navBarControl1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "InvoiceEditPart";
            this.Size = new System.Drawing.Size(1119, 596);
            this.Load += new System.EventHandler(this.InvoiceEditPart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitleEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSONo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContainerNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInvoiceDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInvoiceDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBLNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsValid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.groupBoxBill.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectBill.Properties)).EndInit();
            this.groupBoxFee.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcChargeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoiceFeeDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChargeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.curCureency_)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seUnitPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rseAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._curRate)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFeeTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBank2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVessel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBank1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoyage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsChargeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInvoiceType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerBankAccountNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInvoiceTitleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerAddressTel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerTaxIDNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }       

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barEditInvoiceNo;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraEditors.TextEdit txtBillTotal;
        private DevExpress.XtraEditors.LabelControl labBillTotal;
        private DevExpress.XtraEditors.LabelControl labSONo;
        private DevExpress.XtraEditors.TextEdit txtSONo;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.LabelControl labCtn;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.LabelControl labBank1;
        private DevExpress.XtraEditors.LabelControl labBank2;
        private DevExpress.XtraEditors.LabelControl labTax;
        private DevExpress.XtraEditors.SpinEdit seTax;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private DevExpress.XtraEditors.LabelControl labPOL;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private DevExpress.XtraEditors.LabelControl labPlaceOfDelivery;
        private DevExpress.XtraEditors.LabelControl labVesselVoyage;
        private DevExpress.XtraEditors.LabelControl labContainerNo;
        private DevExpress.XtraEditors.TextEdit txtContainerNo;
        private DevExpress.XtraEditors.LabelControl labInvoiceNo;
        private DevExpress.XtraEditors.LabelControl labInvoiceDate;
        private DevExpress.XtraEditors.DateEdit dteInvoiceDate;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.LabelControl labBLNo;
        private DevExpress.XtraEditors.TextEdit txtBLNo;
        private DevExpress.XtraEditors.LabelControl labETD;
        private DevExpress.XtraEditors.DateEdit dteETD;
        private DevExpress.XtraEditors.CheckEdit chkIsValid;
        private DevExpress.XtraEditors.ButtonEdit txtCustomer;
        private DevExpress.XtraEditors.LabelControl labTitleEName;
        private DevExpress.XtraEditors.LabelControl labTitleCName;
        private DevExpress.XtraEditors.TextEdit txtTitleEName;
        private DevExpress.XtraEditors.ButtonEdit txtVessel;
        private DevExpress.XtraEditors.ButtonEdit txtPlaceOfDelivery;
        private DevExpress.XtraEditors.ButtonEdit txtPOD;
        private DevExpress.XtraEditors.ButtonEdit txtPOL;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbBank2;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private System.Windows.Forms.GroupBox groupBoxFee;
        private System.Windows.Forms.GroupBox groupBoxBill;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnDeleteBill;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labSelectBill;
        private DevExpress.XtraEditors.ButtonEdit txtSelectBill;
        private DevExpress.XtraGrid.GridControl gcBill;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBill;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.BindingSource bsBill;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFee;
        private DevExpress.XtraGrid.Columns.GridColumn colBillFeeFeeSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colBillFeeChargingCode;
        private DevExpress.XtraGrid.Columns.GridColumn colBillFeeCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colBillFeeAmount;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barRemoveFee;
        private DevExpress.XtraBars.BarButtonItem barClearFee;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.TextEdit txtFeeTotal;
        private DevExpress.XtraEditors.LabelControl labFeeTotal;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcChargeList;
        private System.Windows.Forms.BindingSource bsInvoiceFeeDate;
        private DevExpress.XtraGrid.Views.Grid.GridView gvChargeList;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbWay;
        private DevExpress.XtraGrid.Columns.GridColumn colChargingCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit seUnitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit reQuantity;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbUnit;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rseAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit rtxtRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.TextEdit txtInvoiceNo;
        private System.Windows.Forms.BindingSource bsChargeList;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbBank1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnEdit1;
        private ContainerDemandControl cmbCtnType;
        private DevExpress.XtraEditors.LabelControl labVoyage;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox curCureency_;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox _curRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtRate;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.TextEdit txtVoyage;
        private DevExpress.XtraBars.BarButtonItem barAddFee;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup bgInvoceHader;
        private DevExpress.XtraNavBar.NavBarGroup bgBusinessInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.LabelControl labNo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labTaxNo;
        private DevExpress.XtraEditors.LabelControl labCustomerAddressTel;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private DevExpress.XtraNavBar.NavBarGroup bgDetail;
        private DevExpress.XtraEditors.VScrollBar vScrollBar1;
        private DevExpress.XtraBars.BarButtonItem barGetInvoiceNo;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCustomerTaxIDNo;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCustomerBankAccountNo;
        private DevExpress.XtraEditors.LabelControl labReview;
        private DevExpress.XtraEditors.LabelControl labReceivables;
        private MultiSearchCommonBox cmbReview;
        private MultiSearchCommonBox cmbReceivables;
        private DevExpress.XtraEditors.ComboBoxEdit cmbInvoiceTitleName;
        private DevExpress.XtraEditors.ComboBoxEdit txtCustomerAddressTel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbInvoiceType;
        private DevExpress.XtraBars.BarButtonItem barImportTaxInfo;
        private DevExpress.XtraBars.BarButtonItem barExpressNo;
        private DevExpress.XtraEditors.TextEdit txtExpressNo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraBars.BarButtonItem btnCustomerSync;
    }
}
