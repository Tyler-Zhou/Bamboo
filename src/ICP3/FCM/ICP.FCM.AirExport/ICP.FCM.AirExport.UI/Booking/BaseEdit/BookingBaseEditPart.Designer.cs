using System.Windows.Forms;
namespace ICP.FCM.AirExport.UI.Booking
{
    partial class BookingBaseEditPart
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookingBaseEditPart));
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.bsBookingInfo = new System.Windows.Forms.BindingSource(this.components);
            this.cmbBookingMode = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbTradeTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.dteBookingDate = new DevExpress.XtraEditors.DateEdit();
            this.txtCargoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.cmbCargoType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbQuantityUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbMeasurementUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbWeightUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbPaymentTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbTransportClause = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.dteExpectedArriveDate = new DevExpress.XtraEditors.DateEdit();
            this.dteExpectedShipDate = new DevExpress.XtraEditors.DateEdit();
            this.dteDeliveryDate = new DevExpress.XtraEditors.DateEdit();
            this.dteEstimatedDeliveryDate = new DevExpress.XtraEditors.DateEdit();
            this.txtState = new DevExpress.XtraEditors.TextEdit();
            this.chkIsWarehouse = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsQuarantineInspection = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsCommodityInspection = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsCustoms = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsTruck = new DevExpress.XtraEditors.CheckEdit();
            this.txtCommodity = new DevExpress.XtraEditors.MemoEdit();
            this.stxtConsignee = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtBookingCustomer = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtShipper = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.numQuantity = new DevExpress.XtraEditors.SpinEdit();
            this.numWeight = new DevExpress.XtraEditors.SpinEdit();
            this.numMeasurement = new DevExpress.XtraEditors.SpinEdit();
            this.cmbShippingLine = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtDeparture = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtDetination = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtCustomer = new DevExpress.XtraEditors.PopupContainerEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.barAuditAndSave = new DevExpress.XtraBars.BarButtonItem();
            this.barReject = new DevExpress.XtraBars.BarButtonItem();
            this.barTruck = new DevExpress.XtraBars.BarButtonItem();
            this.barApplyAgent = new DevExpress.XtraBars.BarButtonItem();
            this.barE_Booking = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barPrintBookingConfirm = new DevExpress.XtraBars.BarButtonItem();
            this.barSubPrint = new DevExpress.XtraBars.BarSubItem();
            this.barPrintOrder = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintInWarehouse = new DevExpress.XtraBars.BarButtonItem();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.gridControl1 = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsRecentTenOrders = new System.Windows.Forms.BindingSource(this.components);
            this.gvOrders = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClosingDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbQtyUnit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rspinEditInt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.rspinEditFloat = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.cmbSalesType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.mcmbFiler = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.mcmbBookinger = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtPlaceOfDelivery = new DevExpress.XtraEditors.ButtonEdit();
            this.dteSODate = new DevExpress.XtraEditors.DateEdit();
            this.stxtAgentOfCarrier = new DevExpress.XtraEditors.ButtonEdit();
            this.cmbFlightNo = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labState = new DevExpress.XtraEditors.LabelControl();
            this.labBookingDate = new DevExpress.XtraEditors.LabelControl();
            this.labSalesType = new DevExpress.XtraEditors.LabelControl();
            this.labFiler = new DevExpress.XtraEditors.LabelControl();
            this.labBookingMode = new DevExpress.XtraEditors.LabelControl();
            this.labSalesDepartment = new DevExpress.XtraEditors.LabelControl();
            this.labSales = new DevExpress.XtraEditors.LabelControl();
            this.labAgent = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labTradeTerm = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.chkIsOnlyMBL = new DevExpress.XtraEditors.CheckEdit();
            this.labDeparture = new DevExpress.XtraEditors.LabelControl();
            this.dteETD = new DevExpress.XtraEditors.DateEdit();
            this.labETD = new DevExpress.XtraEditors.LabelControl();
            this.groupLocalService = new System.Windows.Forms.GroupBox();
            this.labContractNo = new DevExpress.XtraEditors.LabelControl();
            this.labDetination = new DevExpress.XtraEditors.LabelControl();
            this.labExpectedArriveDate = new DevExpress.XtraEditors.LabelControl();
            this.dteETA = new DevExpress.XtraEditors.DateEdit();
            this.labETA = new DevExpress.XtraEditors.LabelControl();
            this.labDeliveryDate = new DevExpress.XtraEditors.LabelControl();
            this.chkHasContract = new DevExpress.XtraEditors.CheckEdit();
            this.labEstimatedDeliveryDate = new DevExpress.XtraEditors.LabelControl();
            this.labShippingLine = new DevExpress.XtraEditors.LabelControl();
            this.labExpectedShipDate = new DevExpress.XtraEditors.LabelControl();
            this.labBookingCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labAgentOfCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labAirCompany = new DevExpress.XtraEditors.LabelControl();
            this.labCargoType = new DevExpress.XtraEditors.LabelControl();
            this.labQuantity = new DevExpress.XtraEditors.LabelControl();
            this.labPaymentTerm = new DevExpress.XtraEditors.LabelControl();
            this.dteClosingDate = new DevExpress.XtraEditors.DateEdit();
            this.dteDOCClosingDate = new DevExpress.XtraEditors.DateEdit();
            this.labClosingDate = new DevExpress.XtraEditors.LabelControl();
            this.labWeight = new DevExpress.XtraEditors.LabelControl();
            this.labDOCClosingDate = new DevExpress.XtraEditors.LabelControl();
            this.labTransportClause = new DevExpress.XtraEditors.LabelControl();
            this.labMeasurement = new DevExpress.XtraEditors.LabelControl();
            this.labCommodity = new DevExpress.XtraEditors.LabelControl();
            this.labConsignee = new DevExpress.XtraEditors.LabelControl();
            this.labShipper = new DevExpress.XtraEditors.LabelControl();
            this.txtContractNo = new DevExpress.XtraEditors.ButtonEdit();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageBase = new DevExpress.XtraTab.XtraTabPage();
            this.panelScroll = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControlMain = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labBookinger = new DevExpress.XtraEditors.LabelControl();
            this.mcmbSales = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.trsSalesDep = new ICP.Framework.ClientComponents.Controls.TreeSelectBox();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labFlightNo = new DevExpress.XtraEditors.LabelControl();
            this.labSODate = new DevExpress.XtraEditors.LabelControl();
            this.cargoDescriptionPart1 = new ICP.FCM.AirExport.UI.Common.CargoDescriptionPart();
            this.labPlaceOfDelivery = new DevExpress.XtraEditors.LabelControl();
            this.mcmbAirCompany = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.stxtAgent = new ICP.Framework.ClientComponents.Controls.ComboCustomerDescriptionControl();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupRemark = new System.Windows.Forms.GroupBox();
            this.navBarGroupControlContainer4 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.orderFeeEditPart1 = new ICP.FCM.AirExport.UI.Order.OrderFeeEditPart();
            this.navBarGroupControlContainer5 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.partDelegate = new ICP.FCM.Common.UI.CommonPart.PartBookingForCSP();
            this.navBarDelegate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupCSPBooking = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarOther = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarFee = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBookingInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBookingMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTradeTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCargoDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCargoType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuantityUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedArriveDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedArriveDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedShipDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedShipDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDeliveryDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDeliveryDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEstimatedDeliveryDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEstimatedDeliveryDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsWarehouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsQuarantineInspection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCommodityInspection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCustoms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTruck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShippingLine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDeparture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDetination.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRecentTenOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQtyUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditFloat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbFiler.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbBookinger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteSODate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteSODate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgentOfCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOnlyMBL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties)).BeginInit();
            this.groupLocalService.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasContract.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOCClosingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOCClosingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPageBase.SuspendLayout();
            this.panelScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControlMain)).BeginInit();
            this.navBarControlMain.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.navBarGroupControlContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.navBarGroupControlContainer3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupRemark.SuspendLayout();
            this.navBarGroupControlContainer4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.navBarGroupControlContainer5.SuspendLayout();
            this.SuspendLayout();
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bsBookingInfo;
            // 
            // bsBookingInfo
            // 
            this.bsBookingInfo.DataSource = typeof(ICP.FCM.AirExport.ServiceInterface.DataObjects.AirBookingInfo);
            // 
            // cmbBookingMode
            // 
            this.cmbBookingMode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "BookingMode", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbBookingMode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbBookingMode.Location = new System.Drawing.Point(693, 49);
            this.cmbBookingMode.Name = "cmbBookingMode";
            this.cmbBookingMode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbBookingMode.Properties.Appearance.Options.UseBackColor = true;
            this.cmbBookingMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBookingMode.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbBookingMode.Size = new System.Drawing.Size(101, 21);
            this.cmbBookingMode.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbBookingMode.TabIndex = 310;
            // 
            // cmbCompany
            // 
            this.cmbCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "CompanyID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbCompany, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbCompany.Location = new System.Drawing.Point(100, 26);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCompany.Size = new System.Drawing.Size(283, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbCompany.TabIndex = 200;
            // 
            // cmbTradeTerm
            // 
            this.cmbTradeTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "TradeTermID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbTradeTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbTradeTerm.Location = new System.Drawing.Point(100, 73);
            this.cmbTradeTerm.Name = "cmbTradeTerm";
            this.cmbTradeTerm.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbTradeTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTradeTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTradeTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbTradeTerm.Size = new System.Drawing.Size(118, 21);
            this.cmbTradeTerm.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbTradeTerm.TabIndex = 230;
            // 
            // txtNo
            // 
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "No", true));
            this.txtNo.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtNo.Location = new System.Drawing.Point(100, 3);
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.ReadOnly = true;
            this.txtNo.Size = new System.Drawing.Size(118, 21);
            this.txtNo.TabIndex = 13;
            this.txtNo.TabStop = false;
            // 
            // dteBookingDate
            // 
            this.dteBookingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "BookingDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteBookingDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteBookingDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteBookingDate.Location = new System.Drawing.Point(693, 73);
            this.dteBookingDate.Name = "dteBookingDate";
            this.dteBookingDate.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.dteBookingDate.Properties.Appearance.Options.UseBackColor = true;
            this.dteBookingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteBookingDate.Properties.Mask.EditMask = "";
            this.dteBookingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteBookingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteBookingDate.Size = new System.Drawing.Size(101, 21);
            this.dteBookingDate.TabIndex = 330;
            // 
            // txtCargoDescription
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtCargoDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCargoDescription.Location = new System.Drawing.Point(220, 170);
            this.txtCargoDescription.Name = "txtCargoDescription";
            this.txtCargoDescription.Properties.ReadOnly = true;
            this.txtCargoDescription.Size = new System.Drawing.Size(163, 44);
            this.txtCargoDescription.TabIndex = 13;
            this.txtCargoDescription.TabStop = false;
            // 
            // cmbCargoType
            // 
            this.dxErrorProvider1.SetIconAlignment(this.cmbCargoType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbCargoType.Location = new System.Drawing.Point(282, 146);
            this.cmbCargoType.Name = "cmbCargoType";
            this.cmbCargoType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCargoType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCargoType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCargoType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCargoType.Size = new System.Drawing.Size(101, 21);
            this.cmbCargoType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCargoType.TabIndex = 490;
            // 
            // cmbQuantityUnit
            // 
            this.cmbQuantityUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "QuantityUnitID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbQuantityUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbQuantityUnit.Location = new System.Drawing.Point(155, 146);
            this.cmbQuantityUnit.Name = "cmbQuantityUnit";
            this.cmbQuantityUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbQuantityUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbQuantityUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQuantityUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbQuantityUnit.Size = new System.Drawing.Size(52, 21);
            this.cmbQuantityUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbQuantityUnit.TabIndex = 440;
            // 
            // cmbMeasurementUnit
            // 
            this.cmbMeasurementUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "MeasurementUnitID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbMeasurementUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbMeasurementUnit.Location = new System.Drawing.Point(155, 193);
            this.cmbMeasurementUnit.Name = "cmbMeasurementUnit";
            this.cmbMeasurementUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbMeasurementUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMeasurementUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMeasurementUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbMeasurementUnit.Size = new System.Drawing.Size(52, 21);
            this.cmbMeasurementUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbMeasurementUnit.TabIndex = 480;
            // 
            // cmbWeightUnit
            // 
            this.cmbWeightUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "WeightUnitID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbWeightUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbWeightUnit.Location = new System.Drawing.Point(155, 170);
            this.cmbWeightUnit.Name = "cmbWeightUnit";
            this.cmbWeightUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbWeightUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbWeightUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWeightUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbWeightUnit.Size = new System.Drawing.Size(52, 21);
            this.cmbWeightUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbWeightUnit.TabIndex = 460;
            // 
            // cmbPaymentTerm
            // 
            this.cmbPaymentTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "PaymentTermID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbPaymentTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbPaymentTerm.Location = new System.Drawing.Point(491, 73);
            this.cmbPaymentTerm.Name = "cmbPaymentTerm";
            this.cmbPaymentTerm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbPaymentTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbPaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaymentTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbPaymentTerm.Size = new System.Drawing.Size(113, 21);
            this.cmbPaymentTerm.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbPaymentTerm.TabIndex = 320;
            // 
            // cmbTransportClause
            // 
            this.cmbTransportClause.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "TransportClauseID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbTransportClause, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbTransportClause.Location = new System.Drawing.Point(491, 49);
            this.cmbTransportClause.Name = "cmbTransportClause";
            this.cmbTransportClause.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbTransportClause.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTransportClause.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTransportClause.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbTransportClause.Size = new System.Drawing.Size(113, 21);
            this.cmbTransportClause.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbTransportClause.TabIndex = 300;
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "Remark", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtRemark, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtRemark.Location = new System.Drawing.Point(2, 21);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(795, 60);
            this.txtRemark.TabIndex = 840;
            // 
            // dteExpectedArriveDate
            // 
            this.dteExpectedArriveDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ExpectedArriveDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteExpectedArriveDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteExpectedArriveDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteExpectedArriveDate.Location = new System.Drawing.Point(697, 36);
            this.dteExpectedArriveDate.Name = "dteExpectedArriveDate";
            this.dteExpectedArriveDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteExpectedArriveDate.Properties.Mask.EditMask = "";
            this.dteExpectedArriveDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteExpectedArriveDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteExpectedArriveDate.Size = new System.Drawing.Size(97, 21);
            this.dteExpectedArriveDate.TabIndex = 770;
            // 
            // dteExpectedShipDate
            // 
            this.dteExpectedShipDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ExpectedShipDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteExpectedShipDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteExpectedShipDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteExpectedShipDate.Location = new System.Drawing.Point(491, 36);
            this.dteExpectedShipDate.Name = "dteExpectedShipDate";
            this.dteExpectedShipDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteExpectedShipDate.Properties.Mask.EditMask = "";
            this.dteExpectedShipDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteExpectedShipDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteExpectedShipDate.Size = new System.Drawing.Size(97, 21);
            this.dteExpectedShipDate.TabIndex = 760;
            // 
            // dteDeliveryDate
            // 
            this.dteDeliveryDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "DeliveryDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteDeliveryDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteDeliveryDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteDeliveryDate.Location = new System.Drawing.Point(697, 12);
            this.dteDeliveryDate.Name = "dteDeliveryDate";
            this.dteDeliveryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDeliveryDate.Properties.Mask.EditMask = "";
            this.dteDeliveryDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteDeliveryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteDeliveryDate.Size = new System.Drawing.Size(97, 21);
            this.dteDeliveryDate.TabIndex = 750;
            // 
            // dteEstimatedDeliveryDate
            // 
            this.dteEstimatedDeliveryDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "EstimatedDeliveryDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteEstimatedDeliveryDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteEstimatedDeliveryDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteEstimatedDeliveryDate.Location = new System.Drawing.Point(491, 12);
            this.dteEstimatedDeliveryDate.Name = "dteEstimatedDeliveryDate";
            this.dteEstimatedDeliveryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEstimatedDeliveryDate.Properties.Mask.EditMask = "";
            this.dteEstimatedDeliveryDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteEstimatedDeliveryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteEstimatedDeliveryDate.Size = new System.Drawing.Size(97, 21);
            this.dteEstimatedDeliveryDate.TabIndex = 740;
            // 
            // txtState
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtState, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtState.Location = new System.Drawing.Point(282, 3);
            this.txtState.Name = "txtState";
            this.txtState.Properties.ReadOnly = true;
            this.txtState.Size = new System.Drawing.Size(101, 21);
            this.txtState.TabIndex = 12;
            this.txtState.TabStop = false;
            // 
            // chkIsWarehouse
            // 
            this.chkIsWarehouse.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsWareHouse", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.chkIsWarehouse, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsWarehouse.Location = new System.Drawing.Point(6, 19);
            this.chkIsWarehouse.Name = "chkIsWarehouse";
            this.chkIsWarehouse.Properties.Caption = "Warehouse";
            this.chkIsWarehouse.Size = new System.Drawing.Size(85, 19);
            this.chkIsWarehouse.TabIndex = 690;
            // 
            // chkIsQuarantineInspection
            // 
            this.chkIsQuarantineInspection.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsQuarantineInspection", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.chkIsQuarantineInspection, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsQuarantineInspection.Location = new System.Drawing.Point(301, 19);
            this.chkIsQuarantineInspection.Name = "chkIsQuarantineInspection";
            this.chkIsQuarantineInspection.Properties.Caption = "QuarantineInspection";
            this.chkIsQuarantineInspection.Size = new System.Drawing.Size(75, 19);
            this.chkIsQuarantineInspection.TabIndex = 730;
            // 
            // chkIsCommodityInspection
            // 
            this.chkIsCommodityInspection.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsCommodityInspection", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.chkIsCommodityInspection, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsCommodityInspection.Location = new System.Drawing.Point(224, 19);
            this.chkIsCommodityInspection.Name = "chkIsCommodityInspection";
            this.chkIsCommodityInspection.Properties.Caption = "CommodityInspection";
            this.chkIsCommodityInspection.Size = new System.Drawing.Size(75, 19);
            this.chkIsCommodityInspection.TabIndex = 720;
            // 
            // chkIsCustoms
            // 
            this.chkIsCustoms.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsCustoms", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.chkIsCustoms, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsCustoms.Location = new System.Drawing.Point(157, 19);
            this.chkIsCustoms.Name = "chkIsCustoms";
            this.chkIsCustoms.Properties.Caption = "Customs";
            this.chkIsCustoms.Size = new System.Drawing.Size(68, 19);
            this.chkIsCustoms.TabIndex = 710;
            // 
            // chkIsTruck
            // 
            this.chkIsTruck.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsTruck", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.chkIsTruck, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsTruck.Location = new System.Drawing.Point(90, 19);
            this.chkIsTruck.Name = "chkIsTruck";
            this.chkIsTruck.Properties.Caption = "Truck";
            this.chkIsTruck.Size = new System.Drawing.Size(68, 19);
            this.chkIsTruck.TabIndex = 700;
            // 
            // txtCommodity
            // 
            this.txtCommodity.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "Commodity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCommodity, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCommodity.Location = new System.Drawing.Point(100, 122);
            this.txtCommodity.Name = "txtCommodity";
            this.txtCommodity.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtCommodity.Properties.Appearance.Options.UseBackColor = true;
            this.txtCommodity.Size = new System.Drawing.Size(283, 21);
            this.txtCommodity.TabIndex = 420;
            // 
            // stxtConsignee
            // 
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "ConsigneeID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "ConsigneeName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtConsignee, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtConsignee.Location = new System.Drawing.Point(100, 27);
            this.stxtConsignee.Name = "stxtConsignee";
            this.stxtConsignee.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtConsignee.Properties.ActionButtonIndex = 1;
            this.stxtConsignee.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtConsignee.Properties.Appearance.Options.UseBackColor = true;
            this.stxtConsignee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtConsignee.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtConsignee.Properties.PopupSizeable = false;
            this.stxtConsignee.Properties.ShowPopupCloseButton = false;
            this.stxtConsignee.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtConsignee.Size = new System.Drawing.Size(283, 21);
            this.stxtConsignee.TabIndex = 360;
            // 
            // stxtBookingCustomer
            // 
            this.stxtBookingCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "BookingCustomerID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtBookingCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "BookingCustomerName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtBookingCustomer, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtBookingCustomer.Location = new System.Drawing.Point(497, 3);
            this.stxtBookingCustomer.Name = "stxtBookingCustomer";
            this.stxtBookingCustomer.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtBookingCustomer.Properties.ActionButtonIndex = 1;
            this.stxtBookingCustomer.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtBookingCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.stxtBookingCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtBookingCustomer.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtBookingCustomer.Properties.PopupSizeable = false;
            this.stxtBookingCustomer.Properties.ShowPopupCloseButton = false;
            this.stxtBookingCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtBookingCustomer.Size = new System.Drawing.Size(297, 21);
            this.stxtBookingCustomer.TabIndex = 340;
            // 
            // stxtShipper
            // 
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "ShipperID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "ShipperName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtShipper, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtShipper.Location = new System.Drawing.Point(100, 3);
            this.stxtShipper.Name = "stxtShipper";
            this.stxtShipper.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtShipper.Properties.ActionButtonIndex = 1;
            this.stxtShipper.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtShipper.Properties.Appearance.Options.UseBackColor = true;
            this.stxtShipper.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtShipper.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtShipper.Properties.PopupSizeable = false;
            this.stxtShipper.Properties.ShowPopupCloseButton = false;
            this.stxtShipper.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtShipper.Size = new System.Drawing.Size(283, 21);
            this.stxtShipper.TabIndex = 350;
            // 
            // numQuantity
            // 
            this.numQuantity.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "Quantity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numQuantity.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dxErrorProvider1.SetIconAlignment(this.numQuantity, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.numQuantity.Location = new System.Drawing.Point(100, 146);
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numQuantity.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numQuantity.Properties.IsFloatValue = false;
            this.numQuantity.Properties.Mask.EditMask = "N00";
            this.numQuantity.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numQuantity.Size = new System.Drawing.Size(54, 21);
            this.numQuantity.TabIndex = 430;
            // 
            // numWeight
            // 
            this.numWeight.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "Weight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numWeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dxErrorProvider1.SetIconAlignment(this.numWeight, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.numWeight.Location = new System.Drawing.Point(100, 170);
            this.numWeight.Name = "numWeight";
            this.numWeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numWeight.Properties.DisplayFormat.FormatString = "F3";
            this.numWeight.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numWeight.Properties.EditFormat.FormatString = "F3";
            this.numWeight.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numWeight.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numWeight.Properties.Mask.EditMask = "F3";
            this.numWeight.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numWeight.Size = new System.Drawing.Size(54, 21);
            this.numWeight.TabIndex = 450;
            // 
            // numMeasurement
            // 
            this.numMeasurement.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "Measurement", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numMeasurement.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dxErrorProvider1.SetIconAlignment(this.numMeasurement, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.numMeasurement.Location = new System.Drawing.Point(100, 193);
            this.numMeasurement.Name = "numMeasurement";
            this.numMeasurement.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMeasurement.Properties.DisplayFormat.FormatString = "F3";
            this.numMeasurement.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numMeasurement.Properties.EditFormat.FormatString = "F3";
            this.numMeasurement.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numMeasurement.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numMeasurement.Properties.Mask.EditMask = "F3";
            this.numMeasurement.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numMeasurement.Size = new System.Drawing.Size(54, 21);
            this.numMeasurement.TabIndex = 470;
            // 
            // cmbShippingLine
            // 
            this.cmbShippingLine.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ShippingLineID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbShippingLine, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbShippingLine.Location = new System.Drawing.Point(497, 150);
            this.cmbShippingLine.Name = "cmbShippingLine";
            this.cmbShippingLine.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbShippingLine.Properties.Appearance.Options.UseBackColor = true;
            this.cmbShippingLine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbShippingLine.Size = new System.Drawing.Size(297, 21);
            this.cmbShippingLine.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbShippingLine.TabIndex = 600;
            // 
            // stxtDeparture
            // 
            this.stxtDeparture.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "POLID", true));
            this.stxtDeparture.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "DepartureName", true));
            this.stxtDeparture.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtDeparture, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtDeparture.Location = new System.Drawing.Point(100, 50);
            this.stxtDeparture.Name = "stxtDeparture";
            this.stxtDeparture.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtDeparture.Properties.Appearance.Options.UseBackColor = true;
            this.stxtDeparture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtDeparture.Size = new System.Drawing.Size(283, 21);
            this.stxtDeparture.TabIndex = 380;
            // 
            // stxtDetination
            // 
            this.stxtDetination.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "PODID", true));
            this.stxtDetination.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "DetinationName", true));
            this.stxtDetination.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtDetination, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtDetination.Location = new System.Drawing.Point(100, 74);
            this.stxtDetination.Name = "stxtDetination";
            this.stxtDetination.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtDetination.Properties.Appearance.Options.UseBackColor = true;
            this.stxtDetination.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtDetination.Size = new System.Drawing.Size(283, 21);
            this.stxtDetination.TabIndex = 390;
            // 
            // stxtCustomer
            // 
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "CustomerID", true));
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "CustomerName", true));
            this.dxErrorProvider1.SetIconAlignment(this.stxtCustomer, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtCustomer.Location = new System.Drawing.Point(100, 49);
            this.stxtCustomer.MenuManager = this.barManager1;
            this.stxtCustomer.Name = "stxtCustomer";
            this.stxtCustomer.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.stxtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Close, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtCustomer.Properties.CloseOnLostFocus = false;
            this.stxtCustomer.Properties.PopupControl = this.popupContainerControl1;
            this.stxtCustomer.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            this.stxtCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtCustomer.ShowToolTips = false;
            this.stxtCustomer.Size = new System.Drawing.Size(283, 21);
            toolTipTitleItem1.Text = "提示";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "没有业务";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.stxtCustomer.SuperTip = superToolTip1;
            this.stxtCustomer.TabIndex = 220;
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
            this.barSave,
            this.barSaveAs,
            this.barPrintBookingConfirm,
            this.barReject,
            this.barE_Booking,
            this.barClose,
            this.barRefresh,
            this.barAuditAndSave,
            this.barTruck,
            this.barApplyAgent,
            this.barSubPrint,
            this.barPrintOrder,
            this.barPrintInWarehouse});
            this.barManager1.MaxItemId = 18;
            // 
            // bar2
            // 
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSaveAs, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAuditAndSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barReject, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barTruck, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barApplyAgent, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barE_Booking, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Tools";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Save_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barSaveAs
            // 
            this.barSaveAs.Caption = "Save&As";
            this.barSaveAs.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Save_16;
            this.barSaveAs.Id = 1;
            this.barSaveAs.Name = "barSaveAs";
            this.barSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSaveAs_ItemClick);
            // 
            // barAuditAndSave
            // 
            this.barAuditAndSave.Caption = "&Audit&&Save";
            this.barAuditAndSave.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Save_Blue_16;
            this.barAuditAndSave.Id = 10;
            this.barAuditAndSave.Name = "barAuditAndSave";
            this.barAuditAndSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAuditAndSave_ItemClick);
            // 
            // barReject
            // 
            this.barReject.Caption = "Re&ject";
            this.barReject.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Memo_16;
            this.barReject.Id = 3;
            this.barReject.Name = "barReject";
            this.barReject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barReject_ItemClick);
            // 
            // barTruck
            // 
            this.barTruck.Caption = "&Truck";
            this.barTruck.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Transfer_16;
            this.barTruck.Id = 11;
            this.barTruck.Name = "barTruck";
            this.barTruck.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barTruck.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barTruck_ItemClick);
            // 
            // barApplyAgent
            // 
            this.barApplyAgent.Caption = "Apply &Agent";
            this.barApplyAgent.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Center_16;
            this.barApplyAgent.Id = 12;
            this.barApplyAgent.Name = "barApplyAgent";
            this.barApplyAgent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barApplyAgent_ItemClick);
            // 
            // barE_Booking
            // 
            this.barE_Booking.Caption = "&E-Booking";
            this.barE_Booking.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Transfer_16;
            this.barE_Booking.Id = 6;
            this.barE_Booking.Name = "barE_Booking";
            this.barE_Booking.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barE_Booking.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barE_Booking_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Refresh_161;
            this.barRefresh.Id = 9;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Left_D_16;
            this.barClose.Id = 8;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1047, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 988);
            this.barDockControlBottom.Size = new System.Drawing.Size(1047, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 962);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1047, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 962);
            // 
            // barPrintBookingConfirm
            // 
            this.barPrintBookingConfirm.Caption = "Booking confirm";
            this.barPrintBookingConfirm.Id = 2;
            this.barPrintBookingConfirm.Name = "barPrintBookingConfirm";
            this.barPrintBookingConfirm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barSubPrint
            // 
            this.barSubPrint.Caption = "Print";
            this.barSubPrint.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Print_16;
            this.barSubPrint.Id = 14;
            this.barSubPrint.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrintOrder),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrintBookingConfirm, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrintInWarehouse)});
            this.barSubPrint.Name = "barSubPrint";
            this.barSubPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barPrintOrder
            // 
            this.barPrintOrder.Caption = "Order";
            this.barPrintOrder.Id = 15;
            this.barPrintOrder.Name = "barPrintOrder";
            this.barPrintOrder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrintOrder_ItemClick);
            // 
            // barPrintInWarehouse
            // 
            this.barPrintInWarehouse.Caption = "In Warehouse";
            this.barPrintInWarehouse.Id = 16;
            this.barPrintInWarehouse.Name = "barPrintInWarehouse";
            this.barPrintInWarehouse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrintInWarehouse_ItemClick);
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.gridControl1);
            this.popupContainerControl1.Location = new System.Drawing.Point(31, 111);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(653, 178);
            this.popupContainerControl1.TabIndex = 16;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bsRecentTenOrders;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gvOrders;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbQtyUnit,
            this.repositoryItemImageComboBox2,
            this.repositoryItemImageComboBox3,
            this.repositoryItemImageComboBox1,
            this.rspinEditInt,
            this.rspinEditFloat,
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(653, 178);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrders});
            // 
            // bsRecentTenOrders
            // 
            this.bsRecentTenOrders.DataSource = typeof(ICP.FCM.AirExport.ServiceInterface.DataObjects.AirOrderList);
            // 
            // gvOrders
            // 
            this.gvOrders.ChildGridLevelName = "Level1";
            this.gvOrders.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNo,
            this.colPOLName,
            this.colPODName,
            this.colClosingDate,
            this.colShipperName,
            this.colConsigneeName});
            this.gvOrders.DetailTabHeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Right;
            this.gvOrders.GridControl = this.gridControl1;
            this.gvOrders.LevelIndent = 0;
            this.gvOrders.Name = "gvOrders";
            this.gvOrders.OptionsSelection.MultiSelect = true;
            this.gvOrders.OptionsView.ColumnAutoWidth = false;
            this.gvOrders.OptionsView.EnableAppearanceEvenRow = true;
            this.gvOrders.OptionsView.ShowDetailButtons = false;
            this.gvOrders.OptionsView.ShowGroupPanel = false;
            this.gvOrders.PreviewFieldName = "Date";
            // 
            // colNo
            // 
            this.colNo.FieldName = "RefNo";
            this.colNo.Name = "colNo";
            this.colNo.OptionsColumn.AllowEdit = false;
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 0;
            this.colNo.Width = 140;
            // 
            // colPOLName
            // 
            this.colPOLName.Caption = "POL";
            this.colPOLName.FieldName = "POLName";
            this.colPOLName.Name = "colPOLName";
            this.colPOLName.OptionsColumn.AllowEdit = false;
            this.colPOLName.Visible = true;
            this.colPOLName.VisibleIndex = 1;
            this.colPOLName.Width = 100;
            // 
            // colPODName
            // 
            this.colPODName.Caption = "POD";
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.OptionsColumn.AllowEdit = false;
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 2;
            this.colPODName.Width = 100;
            // 
            // colClosingDate
            // 
            this.colClosingDate.Caption = "CLSDate";
            this.colClosingDate.FieldName = "ClosingDate";
            this.colClosingDate.Name = "colClosingDate";
            this.colClosingDate.OptionsColumn.AllowEdit = false;
            this.colClosingDate.Visible = true;
            this.colClosingDate.VisibleIndex = 3;
            this.colClosingDate.Width = 80;
            // 
            // colShipperName
            // 
            this.colShipperName.Caption = "Shipper";
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.OptionsColumn.AllowEdit = false;
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 4;
            this.colShipperName.Width = 100;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.Caption = "Consignee";
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.OptionsColumn.AllowEdit = false;
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 5;
            this.colConsigneeName.Width = 100;
            // 
            // cmbQtyUnit
            // 
            this.cmbQtyUnit.AutoHeight = false;
            this.cmbQtyUnit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQtyUnit.Name = "cmbQtyUnit";
            // 
            // repositoryItemImageComboBox2
            // 
            this.repositoryItemImageComboBox2.AutoHeight = false;
            this.repositoryItemImageComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox2.Name = "repositoryItemImageComboBox2";
            // 
            // repositoryItemImageComboBox3
            // 
            this.repositoryItemImageComboBox3.AutoHeight = false;
            this.repositoryItemImageComboBox3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox3.Name = "repositoryItemImageComboBox3";
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // rspinEditInt
            // 
            this.rspinEditInt.AutoHeight = false;
            this.rspinEditInt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rspinEditInt.IsFloatValue = false;
            this.rspinEditInt.Mask.EditMask = "N00";
            this.rspinEditInt.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.rspinEditInt.Name = "rspinEditInt";
            // 
            // rspinEditFloat
            // 
            this.rspinEditFloat.AutoHeight = false;
            this.rspinEditFloat.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rspinEditFloat.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.rspinEditFloat.Name = "rspinEditFloat";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // cmbSalesType
            // 
            this.cmbSalesType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "SalesTypeID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbSalesType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbSalesType.Location = new System.Drawing.Point(282, 73);
            this.cmbSalesType.Name = "cmbSalesType";
            this.cmbSalesType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbSalesType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbSalesType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSalesType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbSalesType.Size = new System.Drawing.Size(101, 21);
            this.cmbSalesType.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbSalesType.TabIndex = 240;
            // 
            // mcmbFiler
            // 
            this.mcmbFiler.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "FilerId", true));
            this.dxErrorProvider1.SetIconAlignment(this.mcmbFiler, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.mcmbFiler.Location = new System.Drawing.Point(693, 26);
            this.mcmbFiler.Name = "mcmbFiler";
            this.mcmbFiler.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.mcmbFiler.Properties.Appearance.Options.UseBackColor = true;
            this.mcmbFiler.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mcmbFiler.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.mcmbFiler.Size = new System.Drawing.Size(102, 21);
            this.mcmbFiler.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbFiler.TabIndex = 290;
            // 
            // mcmbBookinger
            // 
            this.mcmbBookinger.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "BookingerID", true));
            this.dxErrorProvider1.SetIconAlignment(this.mcmbBookinger, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.mcmbBookinger.Location = new System.Drawing.Point(692, 3);
            this.mcmbBookinger.Name = "mcmbBookinger";
            this.mcmbBookinger.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.mcmbBookinger.Properties.Appearance.Options.UseBackColor = true;
            this.mcmbBookinger.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mcmbBookinger.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.mcmbBookinger.Size = new System.Drawing.Size(102, 21);
            this.mcmbBookinger.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.mcmbBookinger.TabIndex = 270;
            // 
            // stxtPlaceOfDelivery
            // 
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "PlaceOfDeliveryID", true));
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "PlaceOfDeliveryName", true));
            this.stxtPlaceOfDelivery.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtPlaceOfDelivery, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPlaceOfDelivery.Location = new System.Drawing.Point(100, 98);
            this.stxtPlaceOfDelivery.Name = "stxtPlaceOfDelivery";
            this.stxtPlaceOfDelivery.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtPlaceOfDelivery.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlaceOfDelivery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlaceOfDelivery.Size = new System.Drawing.Size(283, 21);
            this.stxtPlaceOfDelivery.TabIndex = 654;
            // 
            // dteSODate
            // 
            this.dteSODate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "SODate", true));
            this.dteSODate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteSODate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteSODate.Location = new System.Drawing.Point(698, 76);
            this.dteSODate.Name = "dteSODate";
            this.dteSODate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteSODate.Properties.Mask.EditMask = "";
            this.dteSODate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteSODate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteSODate.Size = new System.Drawing.Size(96, 21);
            this.dteSODate.TabIndex = 659;
            // 
            // stxtAgentOfCarrier
            // 
            this.stxtAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "AgentOfCarrierID", true));
            this.stxtAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "AgentOfCarrierName", true));
            this.stxtAgentOfCarrier.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtAgentOfCarrier, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtAgentOfCarrier.Location = new System.Drawing.Point(497, 49);
            this.stxtAgentOfCarrier.Name = "stxtAgentOfCarrier";
            this.stxtAgentOfCarrier.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtAgentOfCarrier.Properties.Appearance.Options.UseBackColor = true;
            this.stxtAgentOfCarrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtAgentOfCarrier.Size = new System.Drawing.Size(297, 21);
            this.stxtAgentOfCarrier.TabIndex = 661;
            // 
            // cmbFlightNo
            // 
            this.cmbFlightNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "FilightId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbFlightNo.EditText = "";
            this.cmbFlightNo.EditValue = null;
            this.cmbFlightNo.Location = new System.Drawing.Point(497, 76);
            this.cmbFlightNo.Name = "cmbFlightNo";
            this.cmbFlightNo.ReadOnly = false;
            this.cmbFlightNo.RefreshButtonToolTip = "";
            this.cmbFlightNo.ShowRefreshButton = false;
            this.cmbFlightNo.Size = new System.Drawing.Size(113, 21);
            this.cmbFlightNo.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbFlightNo.TabIndex = 660;
            this.cmbFlightNo.ToolTip = "";
            // 
            // labState
            // 
            this.labState.Location = new System.Drawing.Point(225, 6);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(30, 14);
            this.labState.TabIndex = 0;
            this.labState.Text = "State";
            // 
            // labBookingDate
            // 
            this.labBookingDate.Location = new System.Drawing.Point(617, 76);
            this.labBookingDate.Name = "labBookingDate";
            this.labBookingDate.Size = new System.Drawing.Size(69, 14);
            this.labBookingDate.TabIndex = 0;
            this.labBookingDate.Text = "BookingDate";
            // 
            // labSalesType
            // 
            this.labSalesType.Location = new System.Drawing.Point(225, 76);
            this.labSalesType.Name = "labSalesType";
            this.labSalesType.Size = new System.Drawing.Size(55, 14);
            this.labSalesType.TabIndex = 0;
            this.labSalesType.Text = "SalesType";
            // 
            // labFiler
            // 
            this.labFiler.Location = new System.Drawing.Point(617, 33);
            this.labFiler.Name = "labFiler";
            this.labFiler.Size = new System.Drawing.Size(21, 14);
            this.labFiler.TabIndex = 0;
            this.labFiler.Text = "Filer";
            // 
            // labBookingMode
            // 
            this.labBookingMode.Location = new System.Drawing.Point(617, 52);
            this.labBookingMode.Name = "labBookingMode";
            this.labBookingMode.Size = new System.Drawing.Size(73, 14);
            this.labBookingMode.TabIndex = 0;
            this.labBookingMode.Text = "BookingMode";
            // 
            // labSalesDepartment
            // 
            this.labSalesDepartment.Location = new System.Drawing.Point(400, 29);
            this.labSalesDepartment.Name = "labSalesDepartment";
            this.labSalesDepartment.Size = new System.Drawing.Size(53, 14);
            this.labSalesDepartment.TabIndex = 0;
            this.labSalesDepartment.Text = "Sales Dep";
            // 
            // labSales
            // 
            this.labSales.Location = new System.Drawing.Point(400, 6);
            this.labSales.Name = "labSales";
            this.labSales.Size = new System.Drawing.Size(27, 14);
            this.labSales.TabIndex = 0;
            this.labSales.Text = "Sales";
            // 
            // labAgent
            // 
            this.labAgent.Location = new System.Drawing.Point(400, 31);
            this.labAgent.Name = "labAgent";
            this.labAgent.Size = new System.Drawing.Size(34, 14);
            this.labAgent.TabIndex = 0;
            this.labAgent.Text = "Agent";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(3, 29);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 0;
            this.labCompany.Text = "Company";
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(3, 52);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 0;
            this.labCustomer.Text = "Customer";
            // 
            // labTradeTerm
            // 
            this.labTradeTerm.Location = new System.Drawing.Point(3, 76);
            this.labTradeTerm.Name = "labTradeTerm";
            this.labTradeTerm.Size = new System.Drawing.Size(61, 14);
            this.labTradeTerm.TabIndex = 0;
            this.labTradeTerm.Text = "TradeTerm";
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(3, 6);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(17, 14);
            this.labNo.TabIndex = 0;
            this.labNo.Text = "NO";
            // 
            // chkIsOnlyMBL
            // 
            this.chkIsOnlyMBL.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsOnlyMBL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsOnlyMBL.Location = new System.Drawing.Point(719, 128);
            this.chkIsOnlyMBL.Name = "chkIsOnlyMBL";
            this.chkIsOnlyMBL.Properties.Caption = "IsOnlyMBL";
            this.chkIsOnlyMBL.Size = new System.Drawing.Size(82, 19);
            this.chkIsOnlyMBL.TabIndex = 570;
            // 
            // labDeparture
            // 
            this.labDeparture.Location = new System.Drawing.Point(3, 53);
            this.labDeparture.Name = "labDeparture";
            this.labDeparture.Size = new System.Drawing.Size(55, 14);
            this.labDeparture.TabIndex = 0;
            this.labDeparture.Text = "Departure";
            // 
            // dteETD
            // 
            this.dteETD.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ETD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteETD.EditValue = null;
            this.dteETD.Location = new System.Drawing.Point(497, 177);
            this.dteETD.Name = "dteETD";
            this.dteETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteETD.Properties.Mask.EditMask = "";
            this.dteETD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteETD.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteETD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteETD.Size = new System.Drawing.Size(113, 21);
            this.dteETD.TabIndex = 610;
            this.dteETD.TabStop = false;
            // 
            // labETD
            // 
            this.labETD.Location = new System.Drawing.Point(400, 174);
            this.labETD.Name = "labETD";
            this.labETD.Size = new System.Drawing.Size(23, 14);
            this.labETD.TabIndex = 0;
            this.labETD.Text = "ETD";
            // 
            // groupLocalService
            // 
            this.groupLocalService.Controls.Add(this.chkIsWarehouse);
            this.groupLocalService.Controls.Add(this.chkIsQuarantineInspection);
            this.groupLocalService.Controls.Add(this.chkIsCommodityInspection);
            this.groupLocalService.Controls.Add(this.chkIsCustoms);
            this.groupLocalService.Controls.Add(this.chkIsTruck);
            this.groupLocalService.Font = new System.Drawing.Font("Tahoma", 8F);
            this.groupLocalService.Location = new System.Drawing.Point(10, 3);
            this.groupLocalService.Name = "groupLocalService";
            this.groupLocalService.Size = new System.Drawing.Size(381, 57);
            this.groupLocalService.TabIndex = 15;
            this.groupLocalService.TabStop = false;
            this.groupLocalService.Text = "LocalService";
            // 
            // labContractNo
            // 
            this.labContractNo.Location = new System.Drawing.Point(400, 126);
            this.labContractNo.Name = "labContractNo";
            this.labContractNo.Size = new System.Drawing.Size(62, 14);
            this.labContractNo.TabIndex = 0;
            this.labContractNo.Text = "ContractNo";
            // 
            // labDetination
            // 
            this.labDetination.Location = new System.Drawing.Point(3, 77);
            this.labDetination.Name = "labDetination";
            this.labDetination.Size = new System.Drawing.Size(56, 14);
            this.labDetination.TabIndex = 0;
            this.labDetination.Text = "Detination";
            // 
            // labExpectedArriveDate
            // 
            this.labExpectedArriveDate.Location = new System.Drawing.Point(617, 39);
            this.labExpectedArriveDate.Name = "labExpectedArriveDate";
            this.labExpectedArriveDate.Size = new System.Drawing.Size(55, 14);
            this.labExpectedArriveDate.TabIndex = 31;
            this.labExpectedArriveDate.Text = "Exp.Arrive";
            // 
            // dteETA
            // 
            this.dteETA.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ETA", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteETA.EditValue = null;
            this.dteETA.Location = new System.Drawing.Point(701, 176);
            this.dteETA.Name = "dteETA";
            this.dteETA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteETA.Properties.Mask.EditMask = "";
            this.dteETA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteETA.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteETA.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteETA.Size = new System.Drawing.Size(93, 21);
            this.dteETA.TabIndex = 620;
            this.dteETA.TabStop = false;
            // 
            // labETA
            // 
            this.labETA.Location = new System.Drawing.Point(624, 178);
            this.labETA.Name = "labETA";
            this.labETA.Size = new System.Drawing.Size(23, 14);
            this.labETA.TabIndex = 0;
            this.labETA.Text = "ETA";
            // 
            // labDeliveryDate
            // 
            this.labDeliveryDate.Location = new System.Drawing.Point(617, 15);
            this.labDeliveryDate.Name = "labDeliveryDate";
            this.labDeliveryDate.Size = new System.Drawing.Size(68, 14);
            this.labDeliveryDate.TabIndex = 32;
            this.labDeliveryDate.Text = "DeliveryDate";
            // 
            // chkHasContract
            // 
            this.chkHasContract.Location = new System.Drawing.Point(621, 128);
            this.chkHasContract.Name = "chkHasContract";
            this.chkHasContract.Properties.Caption = "Contract";
            this.chkHasContract.Size = new System.Drawing.Size(82, 19);
            this.chkHasContract.TabIndex = 560;
            // 
            // labEstimatedDeliveryDate
            // 
            this.labEstimatedDeliveryDate.Location = new System.Drawing.Point(400, 15);
            this.labEstimatedDeliveryDate.Name = "labEstimatedDeliveryDate";
            this.labEstimatedDeliveryDate.Size = new System.Drawing.Size(63, 14);
            this.labEstimatedDeliveryDate.TabIndex = 32;
            this.labEstimatedDeliveryDate.Text = "Est.Delivery";
            // 
            // labShippingLine
            // 
            this.labShippingLine.Location = new System.Drawing.Point(400, 150);
            this.labShippingLine.Name = "labShippingLine";
            this.labShippingLine.Size = new System.Drawing.Size(68, 14);
            this.labShippingLine.TabIndex = 0;
            this.labShippingLine.Text = "ShippingLine";
            // 
            // labExpectedShipDate
            // 
            this.labExpectedShipDate.Location = new System.Drawing.Point(400, 39);
            this.labExpectedShipDate.Name = "labExpectedShipDate";
            this.labExpectedShipDate.Size = new System.Drawing.Size(47, 14);
            this.labExpectedShipDate.TabIndex = 33;
            this.labExpectedShipDate.Text = "Exp.Ship";
            // 
            // labBookingCustomer
            // 
            this.labBookingCustomer.Location = new System.Drawing.Point(400, 6);
            this.labBookingCustomer.Name = "labBookingCustomer";
            this.labBookingCustomer.Size = new System.Drawing.Size(95, 14);
            this.labBookingCustomer.TabIndex = 0;
            this.labBookingCustomer.Text = "BookingCustomer";
            // 
            // labAgentOfCarrier
            // 
            this.labAgentOfCarrier.Location = new System.Drawing.Point(400, 56);
            this.labAgentOfCarrier.Name = "labAgentOfCarrier";
            this.labAgentOfCarrier.Size = new System.Drawing.Size(81, 14);
            this.labAgentOfCarrier.TabIndex = 5;
            this.labAgentOfCarrier.Text = "AgentOfCarrier";
            // 
            // labAirCompany
            // 
            this.labAirCompany.Location = new System.Drawing.Point(401, 103);
            this.labAirCompany.Name = "labAirCompany";
            this.labAirCompany.Size = new System.Drawing.Size(64, 14);
            this.labAirCompany.TabIndex = 5;
            this.labAirCompany.Text = "AirCompany";
            // 
            // labCargoType
            // 
            this.labCargoType.Location = new System.Drawing.Point(220, 149);
            this.labCargoType.Name = "labCargoType";
            this.labCargoType.Size = new System.Drawing.Size(59, 14);
            this.labCargoType.TabIndex = 38;
            this.labCargoType.Text = "CargoType";
            // 
            // labQuantity
            // 
            this.labQuantity.Location = new System.Drawing.Point(3, 149);
            this.labQuantity.Name = "labQuantity";
            this.labQuantity.Size = new System.Drawing.Size(47, 14);
            this.labQuantity.TabIndex = 31;
            this.labQuantity.Text = "Quantity";
            // 
            // labPaymentTerm
            // 
            this.labPaymentTerm.Location = new System.Drawing.Point(400, 76);
            this.labPaymentTerm.Name = "labPaymentTerm";
            this.labPaymentTerm.Size = new System.Drawing.Size(77, 14);
            this.labPaymentTerm.TabIndex = 0;
            this.labPaymentTerm.Text = "PaymentTerm";
            // 
            // dteClosingDate
            // 
            this.dteClosingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ClosingDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteClosingDate.EditValue = null;
            this.dteClosingDate.Location = new System.Drawing.Point(701, 200);
            this.dteClosingDate.Name = "dteClosingDate";
            this.dteClosingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteClosingDate.Properties.Mask.EditMask = "";
            this.dteClosingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteClosingDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteClosingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteClosingDate.Size = new System.Drawing.Size(93, 21);
            this.dteClosingDate.TabIndex = 640;
            // 
            // dteDOCClosingDate
            // 
            this.dteDOCClosingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "DOCClosingDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteDOCClosingDate.EditValue = null;
            this.dteDOCClosingDate.Location = new System.Drawing.Point(497, 201);
            this.dteDOCClosingDate.Name = "dteDOCClosingDate";
            this.dteDOCClosingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDOCClosingDate.Properties.Mask.EditMask = "";
            this.dteDOCClosingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteDOCClosingDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteDOCClosingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteDOCClosingDate.Size = new System.Drawing.Size(113, 21);
            this.dteDOCClosingDate.TabIndex = 650;
            // 
            // labClosingDate
            // 
            this.labClosingDate.Location = new System.Drawing.Point(624, 202);
            this.labClosingDate.Name = "labClosingDate";
            this.labClosingDate.Size = new System.Drawing.Size(37, 14);
            this.labClosingDate.TabIndex = 0;
            this.labClosingDate.Text = "Closing";
            // 
            // labWeight
            // 
            this.labWeight.Location = new System.Drawing.Point(3, 173);
            this.labWeight.Name = "labWeight";
            this.labWeight.Size = new System.Drawing.Size(40, 14);
            this.labWeight.TabIndex = 29;
            this.labWeight.Text = "Weight";
            // 
            // labDOCClosingDate
            // 
            this.labDOCClosingDate.Location = new System.Drawing.Point(400, 198);
            this.labDOCClosingDate.Name = "labDOCClosingDate";
            this.labDOCClosingDate.Size = new System.Drawing.Size(61, 14);
            this.labDOCClosingDate.TabIndex = 0;
            this.labDOCClosingDate.Text = "DOCClosing";
            // 
            // labTransportClause
            // 
            this.labTransportClause.Location = new System.Drawing.Point(399, 52);
            this.labTransportClause.Name = "labTransportClause";
            this.labTransportClause.Size = new System.Drawing.Size(87, 14);
            this.labTransportClause.TabIndex = 0;
            this.labTransportClause.Text = "TransportClause";
            // 
            // labMeasurement
            // 
            this.labMeasurement.Location = new System.Drawing.Point(3, 197);
            this.labMeasurement.Name = "labMeasurement";
            this.labMeasurement.Size = new System.Drawing.Size(74, 14);
            this.labMeasurement.TabIndex = 30;
            this.labMeasurement.Text = "Measurement";
            // 
            // labCommodity
            // 
            this.labCommodity.Location = new System.Drawing.Point(3, 125);
            this.labCommodity.Name = "labCommodity";
            this.labCommodity.Size = new System.Drawing.Size(61, 14);
            this.labCommodity.TabIndex = 3;
            this.labCommodity.Text = "Commodity";
            // 
            // labConsignee
            // 
            this.labConsignee.Location = new System.Drawing.Point(3, 30);
            this.labConsignee.Name = "labConsignee";
            this.labConsignee.Size = new System.Drawing.Size(56, 14);
            this.labConsignee.TabIndex = 3;
            this.labConsignee.Text = "Consignee";
            // 
            // labShipper
            // 
            this.labShipper.Location = new System.Drawing.Point(3, 6);
            this.labShipper.Name = "labShipper";
            this.labShipper.Size = new System.Drawing.Size(41, 14);
            this.labShipper.TabIndex = 3;
            this.labShipper.Text = "Shipper";
            // 
            // txtContractNo
            // 
            this.txtContractNo.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "ContractID", true));
            this.txtContractNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "ContractNo", true));
            this.txtContractNo.EditValue = "";
            this.txtContractNo.Location = new System.Drawing.Point(497, 126);
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtContractNo.Size = new System.Drawing.Size(113, 21);
            this.txtContractNo.TabIndex = 550;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 26);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabPageBase;
            this.xtraTabControl1.Size = new System.Drawing.Size(1047, 962);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageBase});
            // 
            // tabPageBase
            // 
            this.tabPageBase.Controls.Add(this.panelScroll);
            this.tabPageBase.Name = "tabPageBase";
            this.tabPageBase.Size = new System.Drawing.Size(1017, 955);
            this.tabPageBase.Text = "Base";
            // 
            // panelScroll
            // 
            this.panelScroll.Controls.Add(this.navBarControlMain);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.Location = new System.Drawing.Point(0, 0);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(1017, 955);
            this.panelScroll.TabIndex = 6;
            // 
            // navBarControlMain
            // 
            this.navBarControlMain.ActiveGroup = this.navBarBase;
            this.navBarControlMain.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControlMain.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControlMain.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControlMain.Controls.Add(this.navBarGroupControlContainer4);
            this.navBarControlMain.Controls.Add(this.navBarGroupControlContainer5);
            this.navBarControlMain.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarBase,
            this.navBarDelegate,
            this.navBarGroupCSPBooking,
            this.navBarOther,
            this.navBarFee});
            this.navBarControlMain.Location = new System.Drawing.Point(3, 3);
            this.navBarControlMain.Name = "navBarControlMain";
            this.navBarControlMain.OptionsNavPane.ExpandedWidth = 806;
            this.navBarControlMain.Size = new System.Drawing.Size(805, 945);
            this.navBarControlMain.TabIndex = 4;
            this.navBarControlMain.Text = "navBarControl1";
            // 
            // navBarBase
            // 
            this.navBarBase.Caption = "Base Info";
            this.navBarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarBase.Expanded = true;
            this.navBarBase.GroupClientHeight = 101;
            this.navBarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBase.Name = "navBarBase";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.panel1);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(801, 99);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel1.Controls.Add(this.labBookinger);
            this.panel1.Controls.Add(this.mcmbFiler);
            this.panel1.Controls.Add(this.mcmbBookinger);
            this.panel1.Controls.Add(this.cmbSalesType);
            this.panel1.Controls.Add(this.mcmbSales);
            this.panel1.Controls.Add(this.trsSalesDep);
            this.panel1.Controls.Add(this.popupContainerControl1);
            this.panel1.Controls.Add(this.stxtCustomer);
            this.panel1.Controls.Add(this.labNo);
            this.panel1.Controls.Add(this.dteBookingDate);
            this.panel1.Controls.Add(this.txtNo);
            this.panel1.Controls.Add(this.labTradeTerm);
            this.panel1.Controls.Add(this.cmbBookingMode);
            this.panel1.Controls.Add(this.labCustomer);
            this.panel1.Controls.Add(this.txtState);
            this.panel1.Controls.Add(this.labCompany);
            this.panel1.Controls.Add(this.labState);
            this.panel1.Controls.Add(this.labSales);
            this.panel1.Controls.Add(this.labBookingDate);
            this.panel1.Controls.Add(this.labSalesDepartment);
            this.panel1.Controls.Add(this.cmbCompany);
            this.panel1.Controls.Add(this.labSalesType);
            this.panel1.Controls.Add(this.cmbTradeTerm);
            this.panel1.Controls.Add(this.labBookingMode);
            this.panel1.Controls.Add(this.labFiler);
            this.panel1.Controls.Add(this.cmbTransportClause);
            this.panel1.Controls.Add(this.labTransportClause);
            this.panel1.Controls.Add(this.cmbPaymentTerm);
            this.panel1.Controls.Add(this.labPaymentTerm);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 99);
            this.panel1.TabIndex = 0;
            // 
            // labBookinger
            // 
            this.labBookinger.Location = new System.Drawing.Point(617, 6);
            this.labBookinger.Name = "labBookinger";
            this.labBookinger.Size = new System.Drawing.Size(54, 14);
            this.labBookinger.TabIndex = 46;
            this.labBookinger.Text = "Bookinger";
            // 
            // mcmbSales
            // 
            this.mcmbSales.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "SalesID", true));
            this.mcmbSales.EditText = "";
            this.mcmbSales.EditValue = null;
            this.mcmbSales.Location = new System.Drawing.Point(490, 3);
            this.mcmbSales.Name = "mcmbSales";
            this.mcmbSales.ReadOnly = false;
            this.mcmbSales.RefreshButtonToolTip = "";
            this.mcmbSales.ShowRefreshButton = false;
            this.mcmbSales.Size = new System.Drawing.Size(114, 21);
            this.mcmbSales.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbSales.TabIndex = 250;
            this.mcmbSales.ToolTip = "";
            this.mcmbSales.EditValueChanged += new System.EventHandler(this.mcmbSales_EditValueChanged);
            // 
            // trsSalesDep
            // 
            this.trsSalesDep.AllText = "Selecte ALL";
            this.trsSalesDep.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "SalesDepartmentID", true));
            this.trsSalesDep.EditValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.trsSalesDep.Location = new System.Drawing.Point(491, 26);
            this.trsSalesDep.Name = "trsSalesDep";
            this.trsSalesDep.OnlyLeafNodeCanSelect = false;
            this.trsSalesDep.ReadOnly = false;
            this.trsSalesDep.Size = new System.Drawing.Size(113, 21);
            this.trsSalesDep.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.trsSalesDep.TabIndex = 280;
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.panel2);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(801, 226);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel2.Controls.Add(this.stxtAgentOfCarrier);
            this.panel2.Controls.Add(this.cmbFlightNo);
            this.panel2.Controls.Add(this.dteSODate);
            this.panel2.Controls.Add(this.labFlightNo);
            this.panel2.Controls.Add(this.labSODate);
            this.panel2.Controls.Add(this.cargoDescriptionPart1);
            this.panel2.Controls.Add(this.stxtPlaceOfDelivery);
            this.panel2.Controls.Add(this.labPlaceOfDelivery);
            this.panel2.Controls.Add(this.labETD);
            this.panel2.Controls.Add(this.mcmbAirCompany);
            this.panel2.Controls.Add(this.labETA);
            this.panel2.Controls.Add(this.stxtAgent);
            this.panel2.Controls.Add(this.stxtDetination);
            this.panel2.Controls.Add(this.dteETA);
            this.panel2.Controls.Add(this.stxtDeparture);
            this.panel2.Controls.Add(this.labDetination);
            this.panel2.Controls.Add(this.cmbWeightUnit);
            this.panel2.Controls.Add(this.chkIsOnlyMBL);
            this.panel2.Controls.Add(this.labQuantity);
            this.panel2.Controls.Add(this.labCargoType);
            this.panel2.Controls.Add(this.labAgent);
            this.panel2.Controls.Add(this.labWeight);
            this.panel2.Controls.Add(this.labBookingCustomer);
            this.panel2.Controls.Add(this.cmbCargoType);
            this.panel2.Controls.Add(this.labDeparture);
            this.panel2.Controls.Add(this.labMeasurement);
            this.panel2.Controls.Add(this.txtContractNo);
            this.panel2.Controls.Add(this.cmbQuantityUnit);
            this.panel2.Controls.Add(this.dteETD);
            this.panel2.Controls.Add(this.cmbMeasurementUnit);
            this.panel2.Controls.Add(this.cmbShippingLine);
            this.panel2.Controls.Add(this.txtCargoDescription);
            this.panel2.Controls.Add(this.labContractNo);
            this.panel2.Controls.Add(this.numMeasurement);
            this.panel2.Controls.Add(this.labShipper);
            this.panel2.Controls.Add(this.txtCommodity);
            this.panel2.Controls.Add(this.stxtShipper);
            this.panel2.Controls.Add(this.numWeight);
            this.panel2.Controls.Add(this.stxtBookingCustomer);
            this.panel2.Controls.Add(this.labCommodity);
            this.panel2.Controls.Add(this.labConsignee);
            this.panel2.Controls.Add(this.numQuantity);
            this.panel2.Controls.Add(this.chkHasContract);
            this.panel2.Controls.Add(this.stxtConsignee);
            this.panel2.Controls.Add(this.labShippingLine);
            this.panel2.Controls.Add(this.labAgentOfCarrier);
            this.panel2.Controls.Add(this.labDOCClosingDate);
            this.panel2.Controls.Add(this.labAirCompany);
            this.panel2.Controls.Add(this.labClosingDate);
            this.panel2.Controls.Add(this.dteDOCClosingDate);
            this.panel2.Controls.Add(this.dteClosingDate);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(801, 226);
            this.panel2.TabIndex = 1;
            // 
            // labFlightNo
            // 
            this.labFlightNo.Location = new System.Drawing.Point(401, 81);
            this.labFlightNo.Name = "labFlightNo";
            this.labFlightNo.Size = new System.Drawing.Size(50, 14);
            this.labFlightNo.TabIndex = 657;
            this.labFlightNo.Text = "Flight NO";
            // 
            // labSODate
            // 
            this.labSODate.Location = new System.Drawing.Point(622, 81);
            this.labSODate.Name = "labSODate";
            this.labSODate.Size = new System.Drawing.Size(42, 14);
            this.labSODate.TabIndex = 656;
            this.labSODate.Text = "SODate";
            // 
            // cargoDescriptionPart1
            // 
            this.cargoDescriptionPart1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.cargoDescriptionPart1.Appearance.Options.UseBackColor = true;
            this.cargoDescriptionPart1.Location = new System.Drawing.Point(390, 147);
            this.cargoDescriptionPart1.Name = "cargoDescriptionPart1";
            this.cargoDescriptionPart1.Size = new System.Drawing.Size(16, 23);
            this.cargoDescriptionPart1.TabIndex = 655;
            this.cargoDescriptionPart1.WorkItem = null;
            // 
            // labPlaceOfDelivery
            // 
            this.labPlaceOfDelivery.Location = new System.Drawing.Point(3, 101);
            this.labPlaceOfDelivery.Name = "labPlaceOfDelivery";
            this.labPlaceOfDelivery.Size = new System.Drawing.Size(83, 14);
            this.labPlaceOfDelivery.TabIndex = 653;
            this.labPlaceOfDelivery.Text = "PlaceOfDelivery";
            // 
            // mcmbAirCompany
            // 
            this.mcmbAirCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "AirCompanyId", true));
            this.mcmbAirCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsBookingInfo, "AirCompanyName", true));
            this.mcmbAirCompany.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "AirCompanyId", true));
            this.mcmbAirCompany.EditText = "";
            this.mcmbAirCompany.EditValue = null;
            this.mcmbAirCompany.Location = new System.Drawing.Point(498, 99);
            this.mcmbAirCompany.Name = "mcmbAirCompany";
            this.mcmbAirCompany.ReadOnly = false;
            this.mcmbAirCompany.RefreshButtonToolTip = "";
            this.mcmbAirCompany.ShowRefreshButton = false;
            this.mcmbAirCompany.Size = new System.Drawing.Size(297, 21);
            this.mcmbAirCompany.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbAirCompany.TabIndex = 540;
            this.mcmbAirCompany.ToolTip = "";
            this.mcmbAirCompany.SelectedRow += new System.EventHandler(this.mcmbAirCompany_SelectedRow);
            // 
            // stxtAgent
            // 
            this.stxtAgent.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "AgentID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtAgent.DataSource = null;
            this.stxtAgent.DisplayMember = "EName";
            this.stxtAgent.EditValue = null;
            this.stxtAgent.Location = new System.Drawing.Point(497, 27);
            this.stxtAgent.Margin = new System.Windows.Forms.Padding(0);
            this.stxtAgent.Name = "stxtAgent";
            this.stxtAgent.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Bottom;
            this.stxtAgent.Size = new System.Drawing.Size(297, 21);
            this.stxtAgent.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtAgent.TabIndex = 520;
            this.stxtAgent.Tag = null;
            this.stxtAgent.ValueMember = "ID";
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.panel3);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(801, 153);
            this.navBarGroupControlContainer3.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel3.Controls.Add(this.dteEstimatedDeliveryDate);
            this.panel3.Controls.Add(this.groupLocalService);
            this.panel3.Controls.Add(this.dteDeliveryDate);
            this.panel3.Controls.Add(this.dteExpectedShipDate);
            this.panel3.Controls.Add(this.groupRemark);
            this.panel3.Controls.Add(this.dteExpectedArriveDate);
            this.panel3.Controls.Add(this.labExpectedShipDate);
            this.panel3.Controls.Add(this.labEstimatedDeliveryDate);
            this.panel3.Controls.Add(this.labExpectedArriveDate);
            this.panel3.Controls.Add(this.labDeliveryDate);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(801, 153);
            this.panel3.TabIndex = 2;
            // 
            // groupRemark
            // 
            this.groupRemark.Controls.Add(this.txtRemark);
            this.groupRemark.Location = new System.Drawing.Point(0, 63);
            this.groupRemark.Name = "groupRemark";
            this.groupRemark.Size = new System.Drawing.Size(801, 87);
            this.groupRemark.TabIndex = 2;
            this.groupRemark.TabStop = false;
            this.groupRemark.Text = "Remark";
            // 
            // navBarGroupControlContainer4
            // 
            this.navBarGroupControlContainer4.Controls.Add(this.panel4);
            this.navBarGroupControlContainer4.Name = "navBarGroupControlContainer4";
            this.navBarGroupControlContainer4.Size = new System.Drawing.Size(801, 220);
            this.navBarGroupControlContainer4.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel4.Controls.Add(this.orderFeeEditPart1);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(801, 220);
            this.panel4.TabIndex = 3;
            // 
            // orderFeeEditPart1
            // 
            this.orderFeeEditPart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderFeeEditPart1.Location = new System.Drawing.Point(0, 0);
            this.orderFeeEditPart1.Name = "orderFeeEditPart1";
            this.orderFeeEditPart1.Size = new System.Drawing.Size(801, 220);
            this.orderFeeEditPart1.TabIndex = 1;
            // 
            // navBarGroupControlContainer5
            // 
            this.navBarGroupControlContainer5.Controls.Add(this.partDelegate);
            this.navBarGroupControlContainer5.Name = "navBarGroupControlContainer5";
            this.navBarGroupControlContainer5.Size = new System.Drawing.Size(801, 158);
            this.navBarGroupControlContainer5.TabIndex = 4;
            // 
            // partDelegate
            // 
            this.partDelegate.BaseMultiLanguageList = null;
            this.partDelegate.BasePartList = null;
            this.partDelegate.CodeValuePairs = null;
            this.partDelegate.ControlsList = null;
            this.partDelegate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.partDelegate.FormName = "Booking Delegates";
            this.partDelegate.IsMultiLanguage = true;
            this.partDelegate.Location = new System.Drawing.Point(0, 0);
            this.partDelegate.Name = "partDelegate";
            this.partDelegate.Resources = null;
            this.partDelegate.Size = new System.Drawing.Size(801, 158);
            this.partDelegate.TabIndex = 3;
            this.partDelegate.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("partDelegate.UsedMessages")));
            // 
            // navBarDelegate
            // 
            this.navBarDelegate.Caption = "Delegate Info";
            this.navBarDelegate.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarDelegate.Expanded = true;
            this.navBarDelegate.GroupClientHeight = 228;
            this.navBarDelegate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarDelegate.Name = "navBarDelegate";
            // 
            // navBarGroupCSPBooking
            // 
            this.navBarGroupCSPBooking.Caption = "CSP Booking";
            this.navBarGroupCSPBooking.ControlContainer = this.navBarGroupControlContainer5;
            this.navBarGroupCSPBooking.GroupClientHeight = 160;
            this.navBarGroupCSPBooking.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroupCSPBooking.Name = "navBarGroupCSPBooking";
            // 
            // navBarOther
            // 
            this.navBarOther.Caption = "Other Info";
            this.navBarOther.ControlContainer = this.navBarGroupControlContainer3;
            this.navBarOther.Expanded = true;
            this.navBarOther.GroupClientHeight = 155;
            this.navBarOther.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarOther.Name = "navBarOther";
            // 
            // navBarFee
            // 
            this.navBarFee.Caption = "Fee Info";
            this.navBarFee.ControlContainer = this.navBarGroupControlContainer4;
            this.navBarFee.Expanded = true;
            this.navBarFee.GroupClientHeight = 222;
            this.navBarFee.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarFee.Name = "navBarFee";
            // 
            // BookingBaseEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BookingBaseEditPart";
            this.Size = new System.Drawing.Size(1047, 988);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBookingInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBookingMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTradeTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCargoDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCargoType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuantityUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedArriveDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedArriveDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedShipDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedShipDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDeliveryDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDeliveryDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEstimatedDeliveryDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEstimatedDeliveryDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsWarehouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsQuarantineInspection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCommodityInspection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCustoms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTruck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShippingLine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDeparture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDetination.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRecentTenOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQtyUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditFloat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbFiler.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbBookinger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteSODate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteSODate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgentOfCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOnlyMBL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties)).EndInit();
            this.groupLocalService.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasContract.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOCClosingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOCClosingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPageBase.ResumeLayout(false);
            this.panelScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControlMain)).EndInit();
            this.navBarControlMain.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupRemark.ResumeLayout(false);
            this.navBarGroupControlContainer4.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.navBarGroupControlContainer5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsBookingInfo;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbBookingMode;
        private DevExpress.XtraEditors.LabelControl labBookingDate;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.LabelControl labSalesType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTradeTerm;
        private DevExpress.XtraEditors.LabelControl labBookingMode;
        private DevExpress.XtraEditors.LabelControl labSalesDepartment;
        private DevExpress.XtraEditors.LabelControl labSales;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.LabelControl labTradeTerm;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.LabelControl labNo;
        private DevExpress.XtraEditors.LabelControl labBookingCustomer;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtBookingCustomer;
        private DevExpress.XtraEditors.LabelControl labShipper;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtShipper;
        private DevExpress.XtraEditors.LabelControl labConsignee;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtConsignee;
        private DevExpress.XtraEditors.LabelControl labTransportClause;
        private DevExpress.XtraEditors.LabelControl labPaymentTerm;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbPaymentTerm;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTransportClause;
        private DevExpress.XtraEditors.TextEdit txtState;
        private DevExpress.XtraEditors.LabelControl labState;
        private DevExpress.XtraEditors.LabelControl labFiler;
        private DevExpress.XtraEditors.LabelControl labAgent;
        private DevExpress.XtraEditors.LabelControl labQuantity;
        private DevExpress.XtraEditors.LabelControl labWeight;
        private DevExpress.XtraEditors.LabelControl labMeasurement;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbQuantityUnit;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMeasurementUnit;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbWeightUnit;
        private DevExpress.XtraEditors.MemoEdit txtCargoDescription;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCargoType;
        private DevExpress.XtraEditors.LabelControl labCargoType;
        private System.Windows.Forms.GroupBox groupLocalService;
        private DevExpress.XtraEditors.LabelControl labAgentOfCarrier;
        private DevExpress.XtraEditors.LabelControl labAirCompany;
        private DevExpress.XtraEditors.CheckEdit chkHasContract;
        private DevExpress.XtraEditors.CheckEdit chkIsOnlyMBL;
        private DevExpress.XtraEditors.DateEdit dteBookingDate;
        private DevExpress.XtraEditors.LabelControl labDetination;
        private DevExpress.XtraEditors.LabelControl labDeparture;
        private DevExpress.XtraEditors.DateEdit dteETD;
        private DevExpress.XtraEditors.LabelControl labETD;
        private DevExpress.XtraEditors.LabelControl labContractNo;
        private DevExpress.XtraEditors.DateEdit dteETA;
        private DevExpress.XtraEditors.LabelControl labETA;
        private DevExpress.XtraEditors.LabelControl labShippingLine;
        private DevExpress.XtraEditors.DateEdit dteClosingDate;
        private DevExpress.XtraEditors.DateEdit dteDOCClosingDate;
        private DevExpress.XtraEditors.LabelControl labClosingDate;
        private DevExpress.XtraEditors.LabelControl labDOCClosingDate;
        private DevExpress.XtraEditors.LabelControl labExpectedArriveDate;
        private DevExpress.XtraEditors.LabelControl labEstimatedDeliveryDate;
        private DevExpress.XtraEditors.LabelControl labExpectedShipDate;
        private DevExpress.XtraEditors.DateEdit dteExpectedArriveDate;
        private DevExpress.XtraEditors.DateEdit dteExpectedShipDate;
        private DevExpress.XtraEditors.DateEdit dteEstimatedDeliveryDate;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labDeliveryDate;
        private DevExpress.XtraEditors.DateEdit dteDeliveryDate;
        private DevExpress.XtraEditors.CheckEdit chkIsWarehouse;
        private DevExpress.XtraEditors.CheckEdit chkIsQuarantineInspection;
        private DevExpress.XtraEditors.CheckEdit chkIsCommodityInspection;
        private DevExpress.XtraEditors.CheckEdit chkIsCustoms;
        private DevExpress.XtraEditors.CheckEdit chkIsTruck;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabPageBase;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barSaveAs;
        private DevExpress.XtraBars.BarButtonItem barPrintBookingConfirm;
        private DevExpress.XtraBars.BarButtonItem barReject;
        private DevExpress.XtraBars.BarButtonItem barE_Booking;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraEditors.SpinEdit numQuantity;
        private DevExpress.XtraEditors.SpinEdit numWeight;
        private DevExpress.XtraEditors.SpinEdit numMeasurement;
        private DevExpress.XtraEditors.MemoEdit txtCommodity;
        private DevExpress.XtraEditors.LabelControl labCommodity;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbShippingLine;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraEditors.ButtonEdit txtContractNo;
        private DevExpress.XtraEditors.XtraScrollableControl panelScroll;
        private DevExpress.XtraNavBar.NavBarControl navBarControlMain;
        private DevExpress.XtraNavBar.NavBarGroup navBarBase;
        private DevExpress.XtraNavBar.NavBarGroup navBarDelegate;
        private DevExpress.XtraNavBar.NavBarGroup navBarOther;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private GroupBox groupRemark;
        private DevExpress.XtraEditors.ButtonEdit stxtDetination;
        private DevExpress.XtraEditors.ButtonEdit stxtDeparture;
        private ICP.Framework.ClientComponents.Controls.ComboCustomerDescriptionControl stxtAgent;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOrders;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colClosingDate;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbQtyUnit;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox2;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox3;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rspinEditInt;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rspinEditFloat;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.PopupContainerEdit stxtCustomer;
        private BindingSource bsRecentTenOrders;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbAirCompany;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer4;
        private ICP.FCM.AirExport.UI.Order.OrderFeeEditPart orderFeeEditPart1;
        private DevExpress.XtraNavBar.NavBarGroup navBarFee;
        private DevExpress.XtraBars.BarButtonItem barAuditAndSave;
        private ICP.Framework.ClientComponents.Controls.TreeSelectBox trsSalesDep;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbSales;
        private DevExpress.XtraBars.BarButtonItem barTruck;
        private DevExpress.XtraBars.BarButtonItem barApplyAgent;
        private DevExpress.XtraBars.BarSubItem barSubPrint;
        private DevExpress.XtraBars.BarButtonItem barPrintOrder;
        private DevExpress.XtraBars.BarButtonItem barPrintInWarehouse;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbSalesType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit mcmbFiler;
        private DevExpress.XtraEditors.LabelControl labBookinger;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit mcmbBookinger;
        private DevExpress.XtraEditors.ButtonEdit stxtPlaceOfDelivery;
        private DevExpress.XtraEditors.LabelControl labPlaceOfDelivery;
        private ICP.FCM.AirExport.UI.Common.CargoDescriptionPart cargoDescriptionPart1;
        private DevExpress.XtraEditors.DateEdit dteSODate;
        private DevExpress.XtraEditors.LabelControl labFlightNo;
        private DevExpress.XtraEditors.LabelControl labSODate;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox cmbFlightNo; 
        private DevExpress.XtraEditors.ButtonEdit stxtAgentOfCarrier;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer5;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupCSPBooking;
        private FCM.Common.UI.CommonPart.PartBookingForCSP partDelegate;
    }
}
