using System.Windows.Forms;
using System.Drawing;
namespace ICP.FCM.OceanExport.UI.Booking
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookingBaseEditPart));
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.bsBookingInfo = new System.Windows.Forms.BindingSource(this.components);
            this.cmbType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
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
            this.txtHBLRequirements = new DevExpress.XtraEditors.MemoEdit();
            this.cmbHBLPaymentTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbHBLReleaseType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtMBLRequirements = new DevExpress.XtraEditors.MemoEdit();
            this.cmbMBLPaymentTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbMBLReleaseType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
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
            this.stxtPlaceOfDelivery = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtAgentOfCarrier = new DevExpress.XtraEditors.ButtonEdit();
            this.dteSODate = new DevExpress.XtraEditors.DateEdit();
            this.stxtConsignee = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtBookingCustomer = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtShipper = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.numQuantity = new DevExpress.XtraEditors.SpinEdit();
            this.numWeight = new DevExpress.XtraEditors.SpinEdit();
            this.numMeasurement = new DevExpress.XtraEditors.SpinEdit();
            this.cmbShippingLine = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtPlaceOfReceipt = new ICP.FCM.Common.UI.UCButtonEdit();
            this.stxtPOL = new ICP.FCM.Common.UI.UCButtonEdit();
            this.stxtPOD = new ICP.FCM.Common.UI.UCButtonEdit();
            this.stxtCustomer = new DevExpress.XtraEditors.PopupContainerEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.barAuditAndSave = new DevExpress.XtraBars.BarButtonItem();
            this.barSubPrint = new DevExpress.XtraBars.BarSubItem();
            this.barPrintOrder = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintBookingConfirm = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintProfit = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintInWarehouse = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barReject = new DevExpress.XtraBars.BarButtonItem();
            this.barTruck = new DevExpress.XtraBars.BarButtonItem();
            this.barApplyAgent = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barE_Booking = new DevExpress.XtraBars.BarButtonItem();
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
            this.mcmbOverseasFiler = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.mcmbBookinger = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtVoyage = new ICP.Common.UI.UCVoyageLookupEdit();
            this.stxtPreVoyage = new ICP.Common.UI.UCVoyageLookupEdit();
            this.containerDemandControl1 = new ICP.Framework.ClientComponents.Controls.ContainerDemandControl();
            this.labState = new DevExpress.XtraEditors.LabelControl();
            this.labBookingDate = new DevExpress.XtraEditors.LabelControl();
            this.labSalesType = new DevExpress.XtraEditors.LabelControl();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.labFiler = new DevExpress.XtraEditors.LabelControl();
            this.labOverseasFiler = new DevExpress.XtraEditors.LabelControl();
            this.labBookingMode = new DevExpress.XtraEditors.LabelControl();
            this.labSalesDepartment = new DevExpress.XtraEditors.LabelControl();
            this.labSales = new DevExpress.XtraEditors.LabelControl();
            this.labAgent = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labTradeTerm = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.chkIsOnlyMBL = new DevExpress.XtraEditors.CheckEdit();
            this.labPlaceOfDelivery = new DevExpress.XtraEditors.LabelControl();
            this.labPOL2 = new DevExpress.XtraEditors.LabelControl();
            this.dtstxtPlaceOfReceipt = new DevExpress.XtraEditors.DateEdit();
            this.labETD = new DevExpress.XtraEditors.LabelControl();
            this.labVoyage = new DevExpress.XtraEditors.LabelControl();
            this.groupLocalService = new System.Windows.Forms.GroupBox();
            this.labContractNo = new DevExpress.XtraEditors.LabelControl();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.labExpectedArriveDate = new DevExpress.XtraEditors.LabelControl();
            this.dtstxtPOD = new DevExpress.XtraEditors.DateEdit();
            this.labETA = new DevExpress.XtraEditors.LabelControl();
            this.labDeliveryDate = new DevExpress.XtraEditors.LabelControl();
            this.chkHasContract = new DevExpress.XtraEditors.CheckEdit();
            this.labEstimatedDeliveryDate = new DevExpress.XtraEditors.LabelControl();
            this.labShippingLine = new DevExpress.XtraEditors.LabelControl();
            this.labExpectedShipDate = new DevExpress.XtraEditors.LabelControl();
            this.labBookingCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labAgentOfCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labCargoType = new DevExpress.XtraEditors.LabelControl();
            this.labQuantity = new DevExpress.XtraEditors.LabelControl();
            this.labPaymentTerm = new DevExpress.XtraEditors.LabelControl();
            this.dteClosingDate = new DevExpress.XtraEditors.DateEdit();
            this.dteDOCClosingDate = new DevExpress.XtraEditors.DateEdit();
            this.labClosingDate = new DevExpress.XtraEditors.LabelControl();
            this.labWeight = new DevExpress.XtraEditors.LabelControl();
            this.labDOCClosingDate = new DevExpress.XtraEditors.LabelControl();
            this.labTransportClause = new DevExpress.XtraEditors.LabelControl();
            this.dteCYClosingDate = new DevExpress.XtraEditors.DateEdit();
            this.labCYClosingDate = new DevExpress.XtraEditors.LabelControl();
            this.labSODate = new DevExpress.XtraEditors.LabelControl();
            this.labMeasurement = new DevExpress.XtraEditors.LabelControl();
            this.labOrderNo = new DevExpress.XtraEditors.LabelControl();
            this.labCommodity = new DevExpress.XtraEditors.LabelControl();
            this.labConsignee = new DevExpress.XtraEditors.LabelControl();
            this.labShipper = new DevExpress.XtraEditors.LabelControl();
            this.txtContractNo = new DevExpress.XtraEditors.ButtonEdit();
            this.groupHBL = new System.Windows.Forms.GroupBox();
            this.labHBLPaymentTerm = new DevExpress.XtraEditors.LabelControl();
            this.labHBLReleaseType = new DevExpress.XtraEditors.LabelControl();
            this.groupMBL = new System.Windows.Forms.GroupBox();
            this.labMBLPaymentTerm = new DevExpress.XtraEditors.LabelControl();
            this.labMBLReleaseType = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageBase = new DevExpress.XtraTab.XtraTabPage();
            this.panelScroll = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labBookinger = new DevExpress.XtraEditors.LabelControl();
            this.mcmbSales = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.trsSalesDep = new ICP.Framework.ClientComponents.Controls.TreeSelectBox();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.mcmbCarrier = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.cargoDescriptionPart1 = new ICP.FCM.OceanExport.UI.Common.CargoDescriptionPart();
            this.stxtAgent = new ICP.Framework.ClientComponents.Controls.ComboCustomerDescriptionControl();
            this.labPreVoyage = new DevExpress.XtraEditors.LabelControl();
            this.labPlaceOfReceipt = new DevExpress.XtraEditors.LabelControl();
            this.labFinalDestination = new DevExpress.XtraEditors.LabelControl();
            this.labWarehouse = new DevExpress.XtraEditors.LabelControl();
            this.labReturnLocation = new DevExpress.XtraEditors.LabelControl();
            this.dtstxtPOL = new DevExpress.XtraEditors.DateEdit();
            this.stxtWarehouse = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtReturnLocation = new DevExpress.XtraEditors.ButtonEdit();
            this.labCloseWarehouse = new DevExpress.XtraEditors.LabelControl();
            this.dteWarehouseClosing = new DevExpress.XtraEditors.DateEdit();
            this.cmbOrderNo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.stxtFinalDestination = new DevExpress.XtraEditors.ButtonEdit();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.groupRemark = new System.Windows.Forms.GroupBox();
            this.navBarGroupControlContainer4 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.orderFeeEditPart1 = new ICP.FCM.OceanExport.UI.Order.OrderFeeEditPart();
            this.navBarDelegate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarOther = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarFee = new DevExpress.XtraNavBar.NavBarGroup();
            this.tabPagePO = new DevExpress.XtraTab.XtraTabPage();
            this.bookingPOEditPart1 = new ICP.FCM.OceanExport.UI.Booking.BookingPOEditPart();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBookingInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtHBLRequirements.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLPaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLReleaseType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMBLRequirements.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMBLPaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMBLReleaseType.Properties)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgentOfCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteSODate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteSODate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShippingLine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfReceipt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.mcmbOverseasFiler.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbBookinger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOnlyMBL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPlaceOfReceipt.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPlaceOfReceipt.Properties)).BeginInit();
            this.groupLocalService.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasContract.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOCClosingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOCClosingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCYClosingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCYClosingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).BeginInit();
            this.groupHBL.SuspendLayout();
            this.groupMBL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPageBase.SuspendLayout();
            this.panelScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOL.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtWarehouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtReturnLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteWarehouseClosing.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteWarehouseClosing.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtFinalDestination.Properties)).BeginInit();
            this.navBarGroupControlContainer3.SuspendLayout();
            this.groupRemark.SuspendLayout();
            this.navBarGroupControlContainer4.SuspendLayout();
            this.tabPagePO.SuspendLayout();
            this.SuspendLayout();
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bsBookingInfo;
            // 
            // bsBookingInfo
            // 
            this.bsBookingInfo.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanBookingInfo);
            // 
            // cmbType
            // 
            this.cmbType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "OEOperationType", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbType.Location = new System.Drawing.Point(282, 26);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbType.Size = new System.Drawing.Size(125, 21);
            this.cmbType.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbType.TabIndex = 210;
            // 
            // cmbBookingMode
            // 
            this.cmbBookingMode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "BookingMode", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbBookingMode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbBookingMode.Location = new System.Drawing.Point(715, 49);
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
            this.cmbCompany.Size = new System.Drawing.Size(119, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbCompany.TabIndex = 200;
            // 
            // cmbTradeTerm
            // 
            this.cmbTradeTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "TradeTermID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbTradeTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbTradeTerm.Location = new System.Drawing.Point(101, 73);
            this.cmbTradeTerm.Name = "cmbTradeTerm";
            this.cmbTradeTerm.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbTradeTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTradeTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTradeTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbTradeTerm.Size = new System.Drawing.Size(117, 21);
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
            this.dteBookingDate.Location = new System.Drawing.Point(715, 73);
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
            this.txtCargoDescription.Location = new System.Drawing.Point(213, 243);
            this.txtCargoDescription.Name = "txtCargoDescription";
            this.txtCargoDescription.Properties.ReadOnly = true;
            this.txtCargoDescription.Size = new System.Drawing.Size(194, 44);
            this.txtCargoDescription.TabIndex = 13;
            this.txtCargoDescription.TabStop = false;
            // 
            // cmbCargoType
            // 
            this.dxErrorProvider1.SetIconAlignment(this.cmbCargoType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbCargoType.Location = new System.Drawing.Point(287, 218);
            this.cmbCargoType.Name = "cmbCargoType";
            this.cmbCargoType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCargoType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCargoType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCargoType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCargoType.Size = new System.Drawing.Size(120, 21);
            this.cmbCargoType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCargoType.TabIndex = 490;
            // 
            // cmbQuantityUnit
            // 
            this.cmbQuantityUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "QuantityUnitID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbQuantityUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbQuantityUnit.Location = new System.Drawing.Point(155, 218);
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
            this.cmbMeasurementUnit.Location = new System.Drawing.Point(155, 266);
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
            this.cmbWeightUnit.Location = new System.Drawing.Point(155, 242);
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
            this.cmbPaymentTerm.Location = new System.Drawing.Point(513, 73);
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
            this.cmbTransportClause.Location = new System.Drawing.Point(513, 49);
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
            this.txtRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dxErrorProvider1.SetIconAlignment(this.txtRemark, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtRemark.Location = new System.Drawing.Point(3, 18);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(821, 52);
            this.txtRemark.TabIndex = 840;
            // 
            // txtHBLRequirements
            // 
            this.txtHBLRequirements.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "HBLRequirements", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtHBLRequirements, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtHBLRequirements.Location = new System.Drawing.Point(7, 62);
            this.txtHBLRequirements.Name = "txtHBLRequirements";
            this.txtHBLRequirements.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHBLRequirements.Size = new System.Drawing.Size(184, 41);
            this.txtHBLRequirements.TabIndex = 830;
            // 
            // cmbHBLPaymentTerm
            // 
            this.cmbHBLPaymentTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "HBLPaymentTermID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbHBLPaymentTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbHBLPaymentTerm.Location = new System.Drawing.Point(95, 14);
            this.cmbHBLPaymentTerm.Name = "cmbHBLPaymentTerm";
            this.cmbHBLPaymentTerm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbHBLPaymentTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbHBLPaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHBLPaymentTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbHBLPaymentTerm.Size = new System.Drawing.Size(96, 21);
            this.cmbHBLPaymentTerm.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbHBLPaymentTerm.TabIndex = 810;
            // 
            // cmbHBLReleaseType
            // 
            this.cmbHBLReleaseType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "HBLReleaseType", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbHBLReleaseType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbHBLReleaseType.Location = new System.Drawing.Point(95, 38);
            this.cmbHBLReleaseType.Name = "cmbHBLReleaseType";
            this.cmbHBLReleaseType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbHBLReleaseType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbHBLReleaseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHBLReleaseType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbHBLReleaseType.Size = new System.Drawing.Size(96, 21);
            this.cmbHBLReleaseType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbHBLReleaseType.TabIndex = 820;
            // 
            // txtMBLRequirements
            // 
            this.txtMBLRequirements.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "MBLRequirements", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtMBLRequirements, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtMBLRequirements.Location = new System.Drawing.Point(7, 62);
            this.txtMBLRequirements.Name = "txtMBLRequirements";
            this.txtMBLRequirements.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMBLRequirements.Size = new System.Drawing.Size(193, 41);
            this.txtMBLRequirements.TabIndex = 800;
            // 
            // cmbMBLPaymentTerm
            // 
            this.cmbMBLPaymentTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "MBLPaymentTermID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbMBLPaymentTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbMBLPaymentTerm.Location = new System.Drawing.Point(99, 14);
            this.cmbMBLPaymentTerm.Name = "cmbMBLPaymentTerm";
            this.cmbMBLPaymentTerm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbMBLPaymentTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMBLPaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMBLPaymentTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbMBLPaymentTerm.Size = new System.Drawing.Size(101, 21);
            this.cmbMBLPaymentTerm.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbMBLPaymentTerm.TabIndex = 780;
            // 
            // cmbMBLReleaseType
            // 
            this.cmbMBLReleaseType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "MBLReleaseType", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbMBLReleaseType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbMBLReleaseType.Location = new System.Drawing.Point(99, 38);
            this.cmbMBLReleaseType.Name = "cmbMBLReleaseType";
            this.cmbMBLReleaseType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbMBLReleaseType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMBLReleaseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMBLReleaseType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbMBLReleaseType.Size = new System.Drawing.Size(101, 21);
            this.cmbMBLReleaseType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbMBLReleaseType.TabIndex = 790;
            // 
            // dteExpectedArriveDate
            // 
            this.dteExpectedArriveDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ExpectedArriveDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteExpectedArriveDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteExpectedArriveDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteExpectedArriveDate.Location = new System.Drawing.Point(298, 86);
            this.dteExpectedArriveDate.Name = "dteExpectedArriveDate";
            this.dteExpectedArriveDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteExpectedArriveDate.Properties.Mask.EditMask = "";
            this.dteExpectedArriveDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteExpectedArriveDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteExpectedArriveDate.Size = new System.Drawing.Size(109, 21);
            this.dteExpectedArriveDate.TabIndex = 770;
            // 
            // dteExpectedShipDate
            // 
            this.dteExpectedShipDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ExpectedShipDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteExpectedShipDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteExpectedShipDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteExpectedShipDate.Location = new System.Drawing.Point(100, 87);
            this.dteExpectedShipDate.Name = "dteExpectedShipDate";
            this.dteExpectedShipDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteExpectedShipDate.Properties.Mask.EditMask = "";
            this.dteExpectedShipDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteExpectedShipDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteExpectedShipDate.Size = new System.Drawing.Size(107, 21);
            this.dteExpectedShipDate.TabIndex = 760;
            // 
            // dteDeliveryDate
            // 
            this.dteDeliveryDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "DeliveryDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteDeliveryDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteDeliveryDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteDeliveryDate.Location = new System.Drawing.Point(298, 63);
            this.dteDeliveryDate.Name = "dteDeliveryDate";
            this.dteDeliveryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDeliveryDate.Properties.Mask.EditMask = "";
            this.dteDeliveryDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteDeliveryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteDeliveryDate.Size = new System.Drawing.Size(109, 21);
            this.dteDeliveryDate.TabIndex = 750;
            // 
            // dteEstimatedDeliveryDate
            // 
            this.dteEstimatedDeliveryDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "EstimatedDeliveryDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteEstimatedDeliveryDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteEstimatedDeliveryDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteEstimatedDeliveryDate.Location = new System.Drawing.Point(100, 64);
            this.dteEstimatedDeliveryDate.Name = "dteEstimatedDeliveryDate";
            this.dteEstimatedDeliveryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEstimatedDeliveryDate.Properties.Mask.EditMask = "";
            this.dteEstimatedDeliveryDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteEstimatedDeliveryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteEstimatedDeliveryDate.Size = new System.Drawing.Size(107, 21);
            this.dteEstimatedDeliveryDate.TabIndex = 740;
            // 
            // txtState
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtState, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtState.Location = new System.Drawing.Point(282, 3);
            this.txtState.Name = "txtState";
            this.txtState.Properties.ReadOnly = true;
            this.txtState.Size = new System.Drawing.Size(125, 21);
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
            this.txtCommodity.Location = new System.Drawing.Point(100, 194);
            this.txtCommodity.Name = "txtCommodity";
            this.txtCommodity.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtCommodity.Properties.Appearance.Options.UseBackColor = true;
            this.txtCommodity.Size = new System.Drawing.Size(307, 21);
            this.txtCommodity.TabIndex = 420;
            // 
            // stxtPlaceOfDelivery
            // 
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "PlaceOfDeliveryID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "PlaceOfDeliveryName", true));
            this.stxtPlaceOfDelivery.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtPlaceOfDelivery, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPlaceOfDelivery.Location = new System.Drawing.Point(100, 147);
            this.stxtPlaceOfDelivery.Name = "stxtPlaceOfDelivery";
            this.stxtPlaceOfDelivery.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtPlaceOfDelivery.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlaceOfDelivery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlaceOfDelivery.Size = new System.Drawing.Size(307, 21);
            this.stxtPlaceOfDelivery.TabIndex = 400;
            //this.stxtPlaceOfDelivery.Leave += new System.EventHandler(this.stxtPlaceOfDelivery_Leave);
            // 
            // stxtAgentOfCarrier
            // 
            this.stxtAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "AgentOfCarrierName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "AgentOfCarrierID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtAgentOfCarrier, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtAgentOfCarrier.Location = new System.Drawing.Point(514, 51);
            this.stxtAgentOfCarrier.Name = "stxtAgentOfCarrier";
            this.stxtAgentOfCarrier.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtAgentOfCarrier.Properties.Appearance.Options.UseBackColor = true;
            this.stxtAgentOfCarrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtAgentOfCarrier.Size = new System.Drawing.Size(303, 21);
            this.stxtAgentOfCarrier.TabIndex = 530;
            // 
            // dteSODate
            // 
            this.dteSODate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "SODate", true));
            this.dteSODate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteSODate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteSODate.Location = new System.Drawing.Point(715, 3);
            this.dteSODate.Name = "dteSODate";
            this.dteSODate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteSODate.Properties.Mask.EditMask = "";
            this.dteSODate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteSODate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteSODate.Size = new System.Drawing.Size(102, 21);
            this.dteSODate.TabIndex = 510;
            // 
            // stxtConsignee
            // 
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "ConsigneeID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "ConsigneeName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtConsignee, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtConsignee.Location = new System.Drawing.Point(100, 51);
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
            this.stxtConsignee.Size = new System.Drawing.Size(307, 21);
            this.stxtConsignee.TabIndex = 360;
            // 
            // stxtBookingCustomer
            // 
            this.stxtBookingCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "BookingCustomerID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtBookingCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "BookingCustomerName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtBookingCustomer, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtBookingCustomer.Location = new System.Drawing.Point(100, 3);
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
            this.stxtBookingCustomer.Size = new System.Drawing.Size(307, 21);
            this.stxtBookingCustomer.TabIndex = 340;
            // 
            // stxtShipper
            // 
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "ShipperID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "ShipperName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtShipper, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtShipper.Location = new System.Drawing.Point(100, 27);
            this.stxtShipper.Name = "stxtShipper";
            this.stxtShipper.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtShipper.Properties.ActionButtonIndex = 1;
            this.stxtShipper.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.stxtShipper.Properties.Appearance.Options.UseBackColor = true;
            this.stxtShipper.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtShipper.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtShipper.Properties.PopupSizeable = false;
            this.stxtShipper.Properties.ShowPopupCloseButton = false;
            this.stxtShipper.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtShipper.Size = new System.Drawing.Size(307, 21);
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
            this.numQuantity.Location = new System.Drawing.Point(100, 218);
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
            this.numWeight.Location = new System.Drawing.Point(100, 242);
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
            this.numMeasurement.Location = new System.Drawing.Point(100, 266);
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
            this.cmbShippingLine.Location = new System.Drawing.Point(514, 170);
            this.cmbShippingLine.Name = "cmbShippingLine";
            this.cmbShippingLine.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbShippingLine.Properties.Appearance.Options.UseBackColor = true;
            this.cmbShippingLine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbShippingLine.Size = new System.Drawing.Size(303, 21);
            this.cmbShippingLine.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbShippingLine.TabIndex = 600;
            // 
            // stxtPlaceOfReceipt
            // 
            this.stxtPlaceOfReceipt.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "PlaceOfReceiptID", true));
            this.stxtPlaceOfReceipt.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "PlaceOfReceiptName", true));
            this.stxtPlaceOfReceipt.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtPlaceOfReceipt, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPlaceOfReceipt.Location = new System.Drawing.Point(100, 75);
            this.stxtPlaceOfReceipt.Name = "stxtPlaceOfReceipt";
            this.stxtPlaceOfReceipt.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtPlaceOfReceipt.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlaceOfReceipt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlaceOfReceipt.Size = new System.Drawing.Size(155, 21);
            this.stxtPlaceOfReceipt.TabIndex = 370;
            this.stxtPlaceOfReceipt.Tag = null;
            // 
            // stxtPOL
            // 
            this.stxtPOL.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "POLID", true));
            this.stxtPOL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "POLName", true));
            this.stxtPOL.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtPOL, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPOL.Location = new System.Drawing.Point(100, 99);
            this.stxtPOL.Name = "stxtPOL";
            this.stxtPOL.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtPOL.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPOL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPOL.Size = new System.Drawing.Size(155, 21);
            this.stxtPOL.TabIndex = 380;
            this.stxtPOL.Tag = null;
            // 
            // stxtPOD
            // 
            this.stxtPOD.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "PODName", true));
            this.stxtPOD.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "PODID", true));
            this.stxtPOD.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtPOD, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPOD.Location = new System.Drawing.Point(100, 123);
            this.stxtPOD.Name = "stxtPOD";
            this.stxtPOD.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtPOD.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPOD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPOD.Size = new System.Drawing.Size(155, 21);
            this.stxtPOD.TabIndex = 390;
            this.stxtPOD.Tag = null;
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
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Close, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtCustomer.Properties.CloseOnLostFocus = false;
            this.stxtCustomer.Properties.PopupControl = this.popupContainerControl1;
            this.stxtCustomer.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            this.stxtCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtCustomer.ShowToolTips = false;
            this.stxtCustomer.Size = new System.Drawing.Size(307, 21);
            toolTipTitleItem2.Text = "提示";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "没有业务";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.stxtCustomer.SuperTip = superToolTip2;
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
            this.barPrintProfit,
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSaveAs, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAuditAndSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barReject, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barTruck, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barApplyAgent, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Tools";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = ((System.Drawing.Image)(resources.GetObject("barSave.Glyph")));
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barSaveAs
            // 
            this.barSaveAs.Caption = "Save&As";
            this.barSaveAs.Glyph = ((System.Drawing.Image)(resources.GetObject("barSaveAs.Glyph")));
            this.barSaveAs.Id = 1;
            this.barSaveAs.Name = "barSaveAs";
            this.barSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSaveAs_ItemClick);
            // 
            // barAuditAndSave
            // 
            this.barAuditAndSave.Caption = "&Audit&&Save";
            this.barAuditAndSave.Glyph = ((System.Drawing.Image)(resources.GetObject("barAuditAndSave.Glyph")));
            this.barAuditAndSave.Id = 10;
            this.barAuditAndSave.Name = "barAuditAndSave";
            this.barAuditAndSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAuditAndSave_ItemClick);
            // 
            // barSubPrint
            // 
            this.barSubPrint.Caption = "Print";
            this.barSubPrint.Glyph = ((System.Drawing.Image)(resources.GetObject("barSubPrint.Glyph")));
            this.barSubPrint.Id = 14;
            this.barSubPrint.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrintOrder),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrintBookingConfirm, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrintProfit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrintInWarehouse)});
            this.barSubPrint.Name = "barSubPrint";
            // 
            // barPrintOrder
            // 
            this.barPrintOrder.Caption = "Order";
            this.barPrintOrder.Id = 15;
            this.barPrintOrder.Name = "barPrintOrder";
            this.barPrintOrder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrintOrder_ItemClick);
            // 
            // barPrintBookingConfirm
            // 
            this.barPrintBookingConfirm.Caption = "Booking confirm";
            this.barPrintBookingConfirm.Id = 2;
            this.barPrintBookingConfirm.Name = "barPrintBookingConfirm";
            this.barPrintBookingConfirm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barPrintProfit
            // 
            this.barPrintProfit.Caption = "Profit";
            this.barPrintProfit.Id = 11;
            this.barPrintProfit.Name = "barPrintProfit";
            this.barPrintProfit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barProfit_ItemClick);
            // 
            // barPrintInWarehouse
            // 
            this.barPrintInWarehouse.Caption = "In Warehouse";
            this.barPrintInWarehouse.Id = 16;
            this.barPrintInWarehouse.Name = "barPrintInWarehouse";
            this.barPrintInWarehouse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrintInWarehouse_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("barRefresh.Glyph")));
            this.barRefresh.Id = 9;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barReject
            // 
            this.barReject.Caption = "Re&ject";
            this.barReject.Glyph = ((System.Drawing.Image)(resources.GetObject("barReject.Glyph")));
            this.barReject.Id = 3;
            this.barReject.Name = "barReject";
            this.barReject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barReject_ItemClick);
            // 
            // barTruck
            // 
            this.barTruck.Caption = "&Truck";
            this.barTruck.Glyph = ((System.Drawing.Image)(resources.GetObject("barTruck.Glyph")));
            this.barTruck.Id = 11;
            this.barTruck.Name = "barTruck";
            this.barTruck.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barTruck_ItemClick);
            // 
            // barApplyAgent
            // 
            this.barApplyAgent.Caption = "Apply &Agent";
            this.barApplyAgent.Glyph = ((System.Drawing.Image)(resources.GetObject("barApplyAgent.Glyph")));
            this.barApplyAgent.Id = 12;
            this.barApplyAgent.Name = "barApplyAgent";
            this.barApplyAgent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barApplyAgent_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = ((System.Drawing.Image)(resources.GetObject("barClose.Glyph")));
            this.barClose.Id = 8;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(864, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 988);
            this.barDockControlBottom.Size = new System.Drawing.Size(864, 0);
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
            this.barDockControlRight.Location = new System.Drawing.Point(864, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 962);
            // 
            // barE_Booking
            // 
            this.barE_Booking.Caption = "&E_Booking";
            this.barE_Booking.Glyph = ((System.Drawing.Image)(resources.GetObject("barE_Booking.Glyph")));
            this.barE_Booking.Id = 6;
            this.barE_Booking.Name = "barE_Booking";
            this.barE_Booking.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barE_Booking_ItemClick);
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
            this.bsRecentTenOrders.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanOrderList);
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
            this.colNo.Width = 130;
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
            this.cmbSalesType.Size = new System.Drawing.Size(125, 21);
            this.cmbSalesType.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbSalesType.TabIndex = 240;
            // 
            // mcmbFiler
            // 
            this.mcmbFiler.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "FilerId", true));
            this.dxErrorProvider1.SetIconAlignment(this.mcmbFiler, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.mcmbFiler.Location = new System.Drawing.Point(715, 26);
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
            // mcmbOverseasFiler
            // 
            this.mcmbOverseasFiler.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "OverSeasFilerID", true));
            this.dxErrorProvider1.SetIconAlignment(this.mcmbOverseasFiler, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.mcmbOverseasFiler.Location = new System.Drawing.Point(623, 3);
            this.mcmbOverseasFiler.Name = "mcmbOverseasFiler";
            this.mcmbOverseasFiler.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.mcmbOverseasFiler.Properties.Appearance.Options.UseBackColor = true;
            this.mcmbOverseasFiler.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mcmbOverseasFiler.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.mcmbOverseasFiler.Size = new System.Drawing.Size(61, 21);
            this.mcmbOverseasFiler.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbOverseasFiler.TabIndex = 260;
            // 
            // mcmbBookinger
            // 
            this.mcmbBookinger.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "BookingerID", true));
            this.dxErrorProvider1.SetIconAlignment(this.mcmbBookinger, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.mcmbBookinger.Location = new System.Drawing.Point(754, 3);
            this.mcmbBookinger.Name = "mcmbBookinger";
            this.mcmbBookinger.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.mcmbBookinger.Properties.Appearance.Options.UseBackColor = true;
            this.mcmbBookinger.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mcmbBookinger.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.mcmbBookinger.Size = new System.Drawing.Size(61, 21);
            this.mcmbBookinger.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.mcmbBookinger.TabIndex = 270;
            // 
            // stxtVoyage
            // 
            this.stxtVoyage.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsBookingInfo, "VoyageName", true));
            this.stxtVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "VoyageID", true));
            this.stxtVoyage.EditText = "";
            this.stxtVoyage.EditValue = null;
            this.stxtVoyage.Location = new System.Drawing.Point(514, 147);
            this.stxtVoyage.Name = "stxtVoyage";
            this.stxtVoyage.ReadOnly = false;
            this.stxtVoyage.RefreshButtonToolTip = "刷新以获取与输入相匹配的船名/航次";
            this.stxtVoyage.ShowRefreshButton = true;
            this.stxtVoyage.Size = new System.Drawing.Size(303, 21);
            this.stxtVoyage.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtVoyage.TabIndex = 590;
            this.stxtVoyage.ToolTip = "";
            // 
            // stxtPreVoyage
            // 
            this.stxtPreVoyage.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsBookingInfo, "PreVoyageName", true));
            this.stxtPreVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "PreVoyageID", true));
            this.stxtPreVoyage.EditText = "";
            this.stxtPreVoyage.EditValue = null;
            this.stxtPreVoyage.Location = new System.Drawing.Point(514, 123);
            this.stxtPreVoyage.Name = "stxtPreVoyage";
            this.stxtPreVoyage.ReadOnly = false;
            this.stxtPreVoyage.RefreshButtonToolTip = "刷新以获取与输入相匹配的船名/航次";
            this.stxtPreVoyage.ShowRefreshButton = true;
            this.stxtPreVoyage.Size = new System.Drawing.Size(303, 21);
            this.stxtPreVoyage.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtPreVoyage.TabIndex = 580;
            this.stxtPreVoyage.ToolTip = "";
            // 
            // containerDemandControl1
            // 
            this.containerDemandControl1.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.containerDemandControl1.Appearance.Options.UseBackColor = true;
            this.containerDemandControl1.Location = new System.Drawing.Point(3, 290);
            this.containerDemandControl1.Name = "containerDemandControl1";
            this.containerDemandControl1.Size = new System.Drawing.Size(405, 21);
            this.containerDemandControl1.SpecifiedBackColor = System.Drawing.Color.White;
            this.containerDemandControl1.TabIndex = 14;
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
            this.labBookingDate.Location = new System.Drawing.Point(639, 76);
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
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(225, 29);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(28, 14);
            this.labType.TabIndex = 0;
            this.labType.Text = "Type";
            // 
            // labFiler
            // 
            this.labFiler.Location = new System.Drawing.Point(639, 33);
            this.labFiler.Name = "labFiler";
            this.labFiler.Size = new System.Drawing.Size(21, 14);
            this.labFiler.TabIndex = 0;
            this.labFiler.Text = "Filer";
            // 
            // labOverseasFiler
            // 
            this.labOverseasFiler.Location = new System.Drawing.Point(581, 6);
            this.labOverseasFiler.Name = "labOverseasFiler";
            this.labOverseasFiler.Size = new System.Drawing.Size(36, 14);
            this.labOverseasFiler.TabIndex = 0;
            this.labOverseasFiler.Text = "O.FIler";
            // 
            // labBookingMode
            // 
            this.labBookingMode.Location = new System.Drawing.Point(639, 52);
            this.labBookingMode.Name = "labBookingMode";
            this.labBookingMode.Size = new System.Drawing.Size(73, 14);
            this.labBookingMode.TabIndex = 0;
            this.labBookingMode.Text = "BookingMode";
            // 
            // labSalesDepartment
            // 
            this.labSalesDepartment.Location = new System.Drawing.Point(422, 29);
            this.labSalesDepartment.Name = "labSalesDepartment";
            this.labSalesDepartment.Size = new System.Drawing.Size(53, 14);
            this.labSalesDepartment.TabIndex = 0;
            this.labSalesDepartment.Text = "Sales Dep";
            // 
            // labSales
            // 
            this.labSales.Location = new System.Drawing.Point(420, 6);
            this.labSales.Name = "labSales";
            this.labSales.Size = new System.Drawing.Size(27, 14);
            this.labSales.TabIndex = 0;
            this.labSales.Text = "Sales";
            // 
            // labAgent
            // 
            this.labAgent.Location = new System.Drawing.Point(422, 31);
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
            this.chkIsOnlyMBL.Location = new System.Drawing.Point(728, 98);
            this.chkIsOnlyMBL.Name = "chkIsOnlyMBL";
            this.chkIsOnlyMBL.Properties.Caption = "IsOnlyMBL";
            this.chkIsOnlyMBL.Size = new System.Drawing.Size(82, 19);
            this.chkIsOnlyMBL.TabIndex = 570;
            // 
            // labPlaceOfDelivery
            // 
            this.labPlaceOfDelivery.Location = new System.Drawing.Point(3, 150);
            this.labPlaceOfDelivery.Name = "labPlaceOfDelivery";
            this.labPlaceOfDelivery.Size = new System.Drawing.Size(83, 14);
            this.labPlaceOfDelivery.TabIndex = 5;
            this.labPlaceOfDelivery.Text = "PlaceOfDelivery";
            // 
            // labPOL2
            // 
            this.labPOL2.Location = new System.Drawing.Point(3, 102);
            this.labPOL2.Name = "labPOL2";
            this.labPOL2.Size = new System.Drawing.Size(22, 14);
            this.labPOL2.TabIndex = 0;
            this.labPOL2.Text = "POL";
            // 
            // dtstxtPlaceOfReceipt
            // 
            this.dtstxtPlaceOfReceipt.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "PORETD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtstxtPlaceOfReceipt.EditValue = null;
            this.dtstxtPlaceOfReceipt.Location = new System.Drawing.Point(304, 75);
            this.dtstxtPlaceOfReceipt.Name = "dtstxtPlaceOfReceipt";
            this.dtstxtPlaceOfReceipt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtstxtPlaceOfReceipt.Properties.Mask.EditMask = "";
            this.dtstxtPlaceOfReceipt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtstxtPlaceOfReceipt.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtstxtPlaceOfReceipt.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtstxtPlaceOfReceipt.Size = new System.Drawing.Size(103, 21);
            this.dtstxtPlaceOfReceipt.TabIndex = 610;
            this.dtstxtPlaceOfReceipt.TabStop = false;
            // 
            // labETD
            // 
            this.labETD.Location = new System.Drawing.Point(261, 78);
            this.labETD.Name = "labETD";
            this.labETD.Size = new System.Drawing.Size(23, 14);
            this.labETD.TabIndex = 0;
            this.labETD.Text = "ETD";
            // 
            // labVoyage
            // 
            this.labVoyage.Location = new System.Drawing.Point(422, 150);
            this.labVoyage.Name = "labVoyage";
            this.labVoyage.Size = new System.Drawing.Size(41, 14);
            this.labVoyage.TabIndex = 0;
            this.labVoyage.Text = "Voyage";
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
            this.groupLocalService.Size = new System.Drawing.Size(397, 57);
            this.groupLocalService.TabIndex = 15;
            this.groupLocalService.TabStop = false;
            this.groupLocalService.Text = "LocalService";
            // 
            // labContractNo
            // 
            this.labContractNo.Location = new System.Drawing.Point(422, 102);
            this.labContractNo.Name = "labContractNo";
            this.labContractNo.Size = new System.Drawing.Size(62, 14);
            this.labContractNo.TabIndex = 0;
            this.labContractNo.Text = "ContractNo";
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(3, 126);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(24, 14);
            this.labPOD.TabIndex = 0;
            this.labPOD.Text = "POD";
            // 
            // labExpectedArriveDate
            // 
            this.labExpectedArriveDate.Location = new System.Drawing.Point(220, 89);
            this.labExpectedArriveDate.Name = "labExpectedArriveDate";
            this.labExpectedArriveDate.Size = new System.Drawing.Size(55, 14);
            this.labExpectedArriveDate.TabIndex = 31;
            this.labExpectedArriveDate.Text = "Exp.Arrive";
            // 
            // dtstxtPOD
            // 
            this.dtstxtPOD.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ETA", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtstxtPOD.EditValue = null;
            this.dtstxtPOD.Location = new System.Drawing.Point(304, 121);
            this.dtstxtPOD.Name = "dtstxtPOD";
            this.dtstxtPOD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtstxtPOD.Properties.Mask.EditMask = "";
            this.dtstxtPOD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtstxtPOD.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtstxtPOD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtstxtPOD.Size = new System.Drawing.Size(103, 21);
            this.dtstxtPOD.TabIndex = 620;
            this.dtstxtPOD.TabStop = false;
            // 
            // labETA
            // 
            this.labETA.Location = new System.Drawing.Point(261, 125);
            this.labETA.Name = "labETA";
            this.labETA.Size = new System.Drawing.Size(23, 14);
            this.labETA.TabIndex = 0;
            this.labETA.Text = "ETA";
            // 
            // labDeliveryDate
            // 
            this.labDeliveryDate.Location = new System.Drawing.Point(220, 66);
            this.labDeliveryDate.Name = "labDeliveryDate";
            this.labDeliveryDate.Size = new System.Drawing.Size(68, 14);
            this.labDeliveryDate.TabIndex = 32;
            this.labDeliveryDate.Text = "DeliveryDate";
            // 
            // chkHasContract
            // 
            this.chkHasContract.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsContract", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkHasContract.Location = new System.Drawing.Point(644, 99);
            this.chkHasContract.Name = "chkHasContract";
            this.chkHasContract.Properties.Caption = "Contract";
            this.chkHasContract.Size = new System.Drawing.Size(82, 19);
            this.chkHasContract.TabIndex = 560;
            // 
            // labEstimatedDeliveryDate
            // 
            this.labEstimatedDeliveryDate.Location = new System.Drawing.Point(3, 67);
            this.labEstimatedDeliveryDate.Name = "labEstimatedDeliveryDate";
            this.labEstimatedDeliveryDate.Size = new System.Drawing.Size(63, 14);
            this.labEstimatedDeliveryDate.TabIndex = 32;
            this.labEstimatedDeliveryDate.Text = "Est.Delivery";
            // 
            // labShippingLine
            // 
            this.labShippingLine.Location = new System.Drawing.Point(422, 173);
            this.labShippingLine.Name = "labShippingLine";
            this.labShippingLine.Size = new System.Drawing.Size(68, 14);
            this.labShippingLine.TabIndex = 0;
            this.labShippingLine.Text = "ShippingLine";
            // 
            // labExpectedShipDate
            // 
            this.labExpectedShipDate.Location = new System.Drawing.Point(3, 90);
            this.labExpectedShipDate.Name = "labExpectedShipDate";
            this.labExpectedShipDate.Size = new System.Drawing.Size(47, 14);
            this.labExpectedShipDate.TabIndex = 33;
            this.labExpectedShipDate.Text = "Exp.Ship";
            // 
            // labBookingCustomer
            // 
            this.labBookingCustomer.Location = new System.Drawing.Point(3, 6);
            this.labBookingCustomer.Name = "labBookingCustomer";
            this.labBookingCustomer.Size = new System.Drawing.Size(95, 14);
            this.labBookingCustomer.TabIndex = 0;
            this.labBookingCustomer.Text = "BookingCustomer";
            // 
            // labAgentOfCarrier
            // 
            this.labAgentOfCarrier.Location = new System.Drawing.Point(422, 54);
            this.labAgentOfCarrier.Name = "labAgentOfCarrier";
            this.labAgentOfCarrier.Size = new System.Drawing.Size(81, 14);
            this.labAgentOfCarrier.TabIndex = 5;
            this.labAgentOfCarrier.Text = "AgentOfCarrier";
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(422, 78);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(34, 14);
            this.labCarrier.TabIndex = 5;
            this.labCarrier.Text = "Carrier";
            // 
            // labCargoType
            // 
            this.labCargoType.Location = new System.Drawing.Point(213, 221);
            this.labCargoType.Name = "labCargoType";
            this.labCargoType.Size = new System.Drawing.Size(59, 14);
            this.labCargoType.TabIndex = 38;
            this.labCargoType.Text = "CargoType";
            // 
            // labQuantity
            // 
            this.labQuantity.Location = new System.Drawing.Point(3, 221);
            this.labQuantity.Name = "labQuantity";
            this.labQuantity.Size = new System.Drawing.Size(47, 14);
            this.labQuantity.TabIndex = 31;
            this.labQuantity.Text = "Quantity";
            // 
            // labPaymentTerm
            // 
            this.labPaymentTerm.Location = new System.Drawing.Point(422, 76);
            this.labPaymentTerm.Name = "labPaymentTerm";
            this.labPaymentTerm.Size = new System.Drawing.Size(77, 14);
            this.labPaymentTerm.TabIndex = 0;
            this.labPaymentTerm.Text = "PaymentTerm";
            // 
            // dteClosingDate
            // 
            this.dteClosingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ClosingDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteClosingDate.EditValue = null;
            this.dteClosingDate.Location = new System.Drawing.Point(717, 197);
            this.dteClosingDate.Name = "dteClosingDate";
            this.dteClosingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteClosingDate.Properties.Mask.EditMask = "";
            this.dteClosingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteClosingDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteClosingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteClosingDate.Size = new System.Drawing.Size(101, 21);
            this.dteClosingDate.TabIndex = 640;
            // 
            // dteDOCClosingDate
            // 
            this.dteDOCClosingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "DOCClosingDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteDOCClosingDate.EditValue = null;
            this.dteDOCClosingDate.Location = new System.Drawing.Point(514, 221);
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
            this.labClosingDate.Location = new System.Drawing.Point(642, 200);
            this.labClosingDate.Name = "labClosingDate";
            this.labClosingDate.Size = new System.Drawing.Size(37, 14);
            this.labClosingDate.TabIndex = 0;
            this.labClosingDate.Text = "Closing";
            // 
            // labWeight
            // 
            this.labWeight.Location = new System.Drawing.Point(3, 245);
            this.labWeight.Name = "labWeight";
            this.labWeight.Size = new System.Drawing.Size(40, 14);
            this.labWeight.TabIndex = 29;
            this.labWeight.Text = "Weight";
            // 
            // labDOCClosingDate
            // 
            this.labDOCClosingDate.Location = new System.Drawing.Point(422, 224);
            this.labDOCClosingDate.Name = "labDOCClosingDate";
            this.labDOCClosingDate.Size = new System.Drawing.Size(61, 14);
            this.labDOCClosingDate.TabIndex = 0;
            this.labDOCClosingDate.Text = "DOCClosing";
            // 
            // labTransportClause
            // 
            this.labTransportClause.Location = new System.Drawing.Point(421, 52);
            this.labTransportClause.Name = "labTransportClause";
            this.labTransportClause.Size = new System.Drawing.Size(87, 14);
            this.labTransportClause.TabIndex = 0;
            this.labTransportClause.Text = "TransportClause";
            // 
            // dteCYClosingDate
            // 
            this.dteCYClosingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "CYClosingDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteCYClosingDate.EditValue = null;
            this.dteCYClosingDate.Location = new System.Drawing.Point(514, 197);
            this.dteCYClosingDate.Name = "dteCYClosingDate";
            this.dteCYClosingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteCYClosingDate.Properties.Mask.EditMask = "";
            this.dteCYClosingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteCYClosingDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteCYClosingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteCYClosingDate.Size = new System.Drawing.Size(113, 21);
            this.dteCYClosingDate.TabIndex = 630;
            // 
            // labCYClosingDate
            // 
            this.labCYClosingDate.Location = new System.Drawing.Point(422, 200);
            this.labCYClosingDate.Name = "labCYClosingDate";
            this.labCYClosingDate.Size = new System.Drawing.Size(52, 14);
            this.labCYClosingDate.TabIndex = 0;
            this.labCYClosingDate.Text = "CYClosing";
            // 
            // labSODate
            // 
            this.labSODate.Location = new System.Drawing.Point(641, 6);
            this.labSODate.Name = "labSODate";
            this.labSODate.Size = new System.Drawing.Size(42, 14);
            this.labSODate.TabIndex = 0;
            this.labSODate.Text = "SODate";
            // 
            // labMeasurement
            // 
            this.labMeasurement.Location = new System.Drawing.Point(3, 270);
            this.labMeasurement.Name = "labMeasurement";
            this.labMeasurement.Size = new System.Drawing.Size(74, 14);
            this.labMeasurement.TabIndex = 30;
            this.labMeasurement.Text = "Measurement";
            // 
            // labOrderNo
            // 
            this.labOrderNo.Location = new System.Drawing.Point(422, 6);
            this.labOrderNo.Name = "labOrderNo";
            this.labOrderNo.Size = new System.Drawing.Size(46, 14);
            this.labOrderNo.TabIndex = 0;
            this.labOrderNo.Text = "OrderNo";
            // 
            // labCommodity
            // 
            this.labCommodity.Location = new System.Drawing.Point(3, 198);
            this.labCommodity.Name = "labCommodity";
            this.labCommodity.Size = new System.Drawing.Size(61, 14);
            this.labCommodity.TabIndex = 3;
            this.labCommodity.Text = "Commodity";
            // 
            // labConsignee
            // 
            this.labConsignee.Location = new System.Drawing.Point(3, 54);
            this.labConsignee.Name = "labConsignee";
            this.labConsignee.Size = new System.Drawing.Size(56, 14);
            this.labConsignee.TabIndex = 3;
            this.labConsignee.Text = "Consignee";
            // 
            // labShipper
            // 
            this.labShipper.Location = new System.Drawing.Point(3, 30);
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
            this.txtContractNo.Location = new System.Drawing.Point(514, 99);
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtContractNo.Size = new System.Drawing.Size(113, 21);
            this.txtContractNo.TabIndex = 550;
            this.txtContractNo.Click += new System.EventHandler(this.txtContractNo_Click);
            // 
            // groupHBL
            // 
            this.groupHBL.Controls.Add(this.txtHBLRequirements);
            this.groupHBL.Controls.Add(this.cmbHBLPaymentTerm);
            this.groupHBL.Controls.Add(this.cmbHBLReleaseType);
            this.groupHBL.Controls.Add(this.labHBLPaymentTerm);
            this.groupHBL.Controls.Add(this.labHBLReleaseType);
            this.groupHBL.Location = new System.Drawing.Point(621, 1);
            this.groupHBL.Name = "groupHBL";
            this.groupHBL.Size = new System.Drawing.Size(194, 107);
            this.groupHBL.TabIndex = 1;
            this.groupHBL.TabStop = false;
            this.groupHBL.Text = "HBL";
            // 
            // labHBLPaymentTerm
            // 
            this.labHBLPaymentTerm.Location = new System.Drawing.Point(7, 17);
            this.labHBLPaymentTerm.Name = "labHBLPaymentTerm";
            this.labHBLPaymentTerm.Size = new System.Drawing.Size(77, 14);
            this.labHBLPaymentTerm.TabIndex = 0;
            this.labHBLPaymentTerm.Text = "PaymentTerm";
            // 
            // labHBLReleaseType
            // 
            this.labHBLReleaseType.Location = new System.Drawing.Point(7, 41);
            this.labHBLReleaseType.Name = "labHBLReleaseType";
            this.labHBLReleaseType.Size = new System.Drawing.Size(69, 14);
            this.labHBLReleaseType.TabIndex = 0;
            this.labHBLReleaseType.Text = "ReleaseType";
            // 
            // groupMBL
            // 
            this.groupMBL.Controls.Add(this.txtMBLRequirements);
            this.groupMBL.Controls.Add(this.cmbMBLPaymentTerm);
            this.groupMBL.Controls.Add(this.cmbMBLReleaseType);
            this.groupMBL.Controls.Add(this.labMBLPaymentTerm);
            this.groupMBL.Controls.Add(this.labMBLReleaseType);
            this.groupMBL.Location = new System.Drawing.Point(415, 1);
            this.groupMBL.Name = "groupMBL";
            this.groupMBL.Size = new System.Drawing.Size(204, 107);
            this.groupMBL.TabIndex = 0;
            this.groupMBL.TabStop = false;
            this.groupMBL.Text = "MBL";
            // 
            // labMBLPaymentTerm
            // 
            this.labMBLPaymentTerm.Location = new System.Drawing.Point(7, 17);
            this.labMBLPaymentTerm.Name = "labMBLPaymentTerm";
            this.labMBLPaymentTerm.Size = new System.Drawing.Size(77, 14);
            this.labMBLPaymentTerm.TabIndex = 0;
            this.labMBLPaymentTerm.Text = "PaymentTerm";
            // 
            // labMBLReleaseType
            // 
            this.labMBLReleaseType.Location = new System.Drawing.Point(7, 41);
            this.labMBLReleaseType.Name = "labMBLReleaseType";
            this.labMBLReleaseType.Size = new System.Drawing.Size(69, 14);
            this.labMBLReleaseType.TabIndex = 0;
            this.labMBLReleaseType.Text = "ReleaseType";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 26);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabPageBase;
            this.xtraTabControl1.Size = new System.Drawing.Size(864, 962);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageBase,
            this.tabPagePO});
            // 
            // tabPageBase
            // 
            this.tabPageBase.Controls.Add(this.panelScroll);
            this.tabPageBase.Name = "tabPageBase";
            this.tabPageBase.Size = new System.Drawing.Size(834, 955);
            this.tabPageBase.Text = "Base";
            // 
            // panelScroll
            // 
            this.panelScroll.Controls.Add(this.navBarControl1);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.Location = new System.Drawing.Point(0, 0);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(834, 955);
            this.panelScroll.TabIndex = 6;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer4);
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarBase,
            this.navBarDelegate,
            this.navBarOther,
            this.navBarFee});
            this.navBarControl1.Location = new System.Drawing.Point(3, 3);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 806;
            this.navBarControl1.Size = new System.Drawing.Size(831, 945);
            this.navBarControl1.TabIndex = 4;
            this.navBarControl1.Text = "navBarControl1";
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
            this.navBarGroupControlContainer1.Controls.Add(this.labBookinger);
            this.navBarGroupControlContainer1.Controls.Add(this.mcmbOverseasFiler);
            this.navBarGroupControlContainer1.Controls.Add(this.mcmbFiler);
            this.navBarGroupControlContainer1.Controls.Add(this.mcmbBookinger);
            this.navBarGroupControlContainer1.Controls.Add(this.mcmbSales);
            this.navBarGroupControlContainer1.Controls.Add(this.trsSalesDep);
            this.navBarGroupControlContainer1.Controls.Add(this.popupContainerControl1);
            this.navBarGroupControlContainer1.Controls.Add(this.labNo);
            this.navBarGroupControlContainer1.Controls.Add(this.dteBookingDate);
            this.navBarGroupControlContainer1.Controls.Add(this.txtNo);
            this.navBarGroupControlContainer1.Controls.Add(this.labTradeTerm);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbBookingMode);
            this.navBarGroupControlContainer1.Controls.Add(this.labCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.labCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.labState);
            this.navBarGroupControlContainer1.Controls.Add(this.labSales);
            this.navBarGroupControlContainer1.Controls.Add(this.labBookingDate);
            this.navBarGroupControlContainer1.Controls.Add(this.labSalesDepartment);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.labSalesType);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbTradeTerm);
            this.navBarGroupControlContainer1.Controls.Add(this.labType);
            this.navBarGroupControlContainer1.Controls.Add(this.labBookingMode);
            this.navBarGroupControlContainer1.Controls.Add(this.labFiler);
            this.navBarGroupControlContainer1.Controls.Add(this.labOverseasFiler);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbTransportClause);
            this.navBarGroupControlContainer1.Controls.Add(this.labTransportClause);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbPaymentTerm);
            this.navBarGroupControlContainer1.Controls.Add(this.labPaymentTerm);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbSalesType);
            this.navBarGroupControlContainer1.Controls.Add(this.stxtCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbType);
            this.navBarGroupControlContainer1.Controls.Add(this.txtState);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(827, 99);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // labBookinger
            // 
            this.labBookinger.Location = new System.Drawing.Point(690, 6);
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
            this.mcmbSales.Location = new System.Drawing.Point(513, 0);
            this.mcmbSales.Name = "mcmbSales";
            this.mcmbSales.ReadOnly = false;
            this.mcmbSales.RefreshButtonToolTip = "";
            this.mcmbSales.ShowRefreshButton = false;
            this.mcmbSales.Size = new System.Drawing.Size(61, 21);
            this.mcmbSales.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbSales.TabIndex = 250;
            this.mcmbSales.ToolTip = "";
            // 
            // trsSalesDep
            // 
            this.trsSalesDep.AllText = "Selecte ALL";
            this.trsSalesDep.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "SalesDepartmentID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trsSalesDep.EditValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.trsSalesDep.Location = new System.Drawing.Point(513, 26);
            this.trsSalesDep.Name = "trsSalesDep";
            this.trsSalesDep.ReadOnly = false;
            this.trsSalesDep.Size = new System.Drawing.Size(113, 21);
            this.trsSalesDep.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.trsSalesDep.TabIndex = 280;
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.labelControl1);
            this.navBarGroupControlContainer2.Controls.Add(this.labETD);
            this.navBarGroupControlContainer2.Controls.Add(this.labCYClosingDate);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtPlaceOfReceipt);
            this.navBarGroupControlContainer2.Controls.Add(this.mcmbCarrier);
            this.navBarGroupControlContainer2.Controls.Add(this.labETA);
            this.navBarGroupControlContainer2.Controls.Add(this.cargoDescriptionPart1);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtAgent);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtPOD);
            this.navBarGroupControlContainer2.Controls.Add(this.dtstxtPOD);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtVoyage);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtPOL);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtPreVoyage);
            this.navBarGroupControlContainer2.Controls.Add(this.labPOD);
            this.navBarGroupControlContainer2.Controls.Add(this.labPreVoyage);
            this.navBarGroupControlContainer2.Controls.Add(this.labPlaceOfReceipt);
            this.navBarGroupControlContainer2.Controls.Add(this.chkIsOnlyMBL);
            this.navBarGroupControlContainer2.Controls.Add(this.labAgent);
            this.navBarGroupControlContainer2.Controls.Add(this.labPlaceOfDelivery);
            this.navBarGroupControlContainer2.Controls.Add(this.labBookingCustomer);
            this.navBarGroupControlContainer2.Controls.Add(this.labFinalDestination);
            this.navBarGroupControlContainer2.Controls.Add(this.labVoyage);
            this.navBarGroupControlContainer2.Controls.Add(this.labWarehouse);
            this.navBarGroupControlContainer2.Controls.Add(this.labReturnLocation);
            this.navBarGroupControlContainer2.Controls.Add(this.labPOL2);
            this.navBarGroupControlContainer2.Controls.Add(this.txtContractNo);
            this.navBarGroupControlContainer2.Controls.Add(this.dtstxtPOL);
            this.navBarGroupControlContainer2.Controls.Add(this.dtstxtPlaceOfReceipt);
            this.navBarGroupControlContainer2.Controls.Add(this.cmbShippingLine);
            this.navBarGroupControlContainer2.Controls.Add(this.labContractNo);
            this.navBarGroupControlContainer2.Controls.Add(this.numMeasurement);
            this.navBarGroupControlContainer2.Controls.Add(this.numWeight);
            this.navBarGroupControlContainer2.Controls.Add(this.numQuantity);
            this.navBarGroupControlContainer2.Controls.Add(this.labShipper);
            this.navBarGroupControlContainer2.Controls.Add(this.labConsignee);
            this.navBarGroupControlContainer2.Controls.Add(this.chkHasContract);
            this.navBarGroupControlContainer2.Controls.Add(this.labCommodity);
            this.navBarGroupControlContainer2.Controls.Add(this.cmbWeightUnit);
            this.navBarGroupControlContainer2.Controls.Add(this.cmbMeasurementUnit);
            this.navBarGroupControlContainer2.Controls.Add(this.labShippingLine);
            this.navBarGroupControlContainer2.Controls.Add(this.cmbQuantityUnit);
            this.navBarGroupControlContainer2.Controls.Add(this.dteSODate);
            this.navBarGroupControlContainer2.Controls.Add(this.labOrderNo);
            this.navBarGroupControlContainer2.Controls.Add(this.labMeasurement);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtWarehouse);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtReturnLocation);
            this.navBarGroupControlContainer2.Controls.Add(this.labSODate);
            this.navBarGroupControlContainer2.Controls.Add(this.labAgentOfCarrier);
            this.navBarGroupControlContainer2.Controls.Add(this.dteCYClosingDate);
            this.navBarGroupControlContainer2.Controls.Add(this.labCloseWarehouse);
            this.navBarGroupControlContainer2.Controls.Add(this.labDOCClosingDate);
            this.navBarGroupControlContainer2.Controls.Add(this.labWeight);
            this.navBarGroupControlContainer2.Controls.Add(this.labCarrier);
            this.navBarGroupControlContainer2.Controls.Add(this.labClosingDate);
            this.navBarGroupControlContainer2.Controls.Add(this.dteWarehouseClosing);
            this.navBarGroupControlContainer2.Controls.Add(this.dteDOCClosingDate);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtAgentOfCarrier);
            this.navBarGroupControlContainer2.Controls.Add(this.dteClosingDate);
            this.navBarGroupControlContainer2.Controls.Add(this.labCargoType);
            this.navBarGroupControlContainer2.Controls.Add(this.labQuantity);
            this.navBarGroupControlContainer2.Controls.Add(this.cmbOrderNo);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtPlaceOfDelivery);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtFinalDestination);
            this.navBarGroupControlContainer2.Controls.Add(this.containerDemandControl1);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtShipper);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtBookingCustomer);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtConsignee);
            this.navBarGroupControlContainer2.Controls.Add(this.txtCommodity);
            this.navBarGroupControlContainer2.Controls.Add(this.txtCargoDescription);
            this.navBarGroupControlContainer2.Controls.Add(this.cmbCargoType);
            this.navBarGroupControlContainer2.ForeColor = System.Drawing.Color.Black;
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(827, 314);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(261, 101);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(23, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "ETD";
            // 
            // mcmbCarrier
            // 
            this.mcmbCarrier.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "CarrierID", true));
            this.mcmbCarrier.EditText = "";
            this.mcmbCarrier.EditValue = null;
            this.mcmbCarrier.Location = new System.Drawing.Point(514, 75);
            this.mcmbCarrier.Name = "mcmbCarrier";
            this.mcmbCarrier.ReadOnly = false;
            this.mcmbCarrier.RefreshButtonToolTip = "";
            this.mcmbCarrier.ShowRefreshButton = false;
            this.mcmbCarrier.Size = new System.Drawing.Size(303, 21);
            this.mcmbCarrier.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbCarrier.TabIndex = 540;
            this.mcmbCarrier.ToolTip = "";
            // 
            // cargoDescriptionPart1
            // 
            this.cargoDescriptionPart1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.cargoDescriptionPart1.Appearance.Options.UseBackColor = true;
            this.cargoDescriptionPart1.Location = new System.Drawing.Point(406, 197);
            this.cargoDescriptionPart1.Name = "cargoDescriptionPart1";
            this.cargoDescriptionPart1.Size = new System.Drawing.Size(19, 21);
            this.cargoDescriptionPart1.TabIndex = 50;
            // 
            // stxtAgent
            // 
            this.stxtAgent.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "AgentID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtAgent.DataSource = null;
            this.stxtAgent.DisplayMember = "EName";
            this.stxtAgent.EditValue = null;
            this.stxtAgent.Location = new System.Drawing.Point(514, 27);
            this.stxtAgent.Margin = new System.Windows.Forms.Padding(0);
            this.stxtAgent.Name = "stxtAgent";
            this.stxtAgent.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Bottom;
            this.stxtAgent.Size = new System.Drawing.Size(303, 21);
            this.stxtAgent.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtAgent.TabIndex = 520;
            this.stxtAgent.Tag = null;
            this.stxtAgent.ValueMember = "ID";
            // 
            // labPreVoyage
            // 
            this.labPreVoyage.Location = new System.Drawing.Point(422, 126);
            this.labPreVoyage.Name = "labPreVoyage";
            this.labPreVoyage.Size = new System.Drawing.Size(59, 14);
            this.labPreVoyage.TabIndex = 44;
            this.labPreVoyage.Text = "PreVoyage";
            // 
            // labPlaceOfReceipt
            // 
            this.labPlaceOfReceipt.Location = new System.Drawing.Point(3, 78);
            this.labPlaceOfReceipt.Name = "labPlaceOfReceipt";
            this.labPlaceOfReceipt.Size = new System.Drawing.Size(31, 14);
            this.labPlaceOfReceipt.TabIndex = 46;
            this.labPlaceOfReceipt.Text = "P.O.R";
            // 
            // labFinalDestination
            // 
            this.labFinalDestination.Location = new System.Drawing.Point(3, 173);
            this.labFinalDestination.Name = "labFinalDestination";
            this.labFinalDestination.Size = new System.Drawing.Size(84, 14);
            this.labFinalDestination.TabIndex = 5;
            this.labFinalDestination.Text = "FinalDestination";
            // 
            // labWarehouse
            // 
            this.labWarehouse.Location = new System.Drawing.Point(421, 249);
            this.labWarehouse.Name = "labWarehouse";
            this.labWarehouse.Size = new System.Drawing.Size(62, 14);
            this.labWarehouse.TabIndex = 5;
            this.labWarehouse.Text = "Warehouse";
            // 
            // labReturnLocation
            // 
            this.labReturnLocation.Location = new System.Drawing.Point(421, 273);
            this.labReturnLocation.Name = "labReturnLocation";
            this.labReturnLocation.Size = new System.Drawing.Size(83, 14);
            this.labReturnLocation.TabIndex = 5;
            this.labReturnLocation.Text = "ReturnLocation";
            // 
            // dtstxtPOL
            // 
            this.dtstxtPOL.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ETD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtstxtPOL.EditValue = null;
            this.dtstxtPOL.Location = new System.Drawing.Point(304, 98);
            this.dtstxtPOL.Name = "dtstxtPOL";
            this.dtstxtPOL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtstxtPOL.Properties.Mask.EditMask = "";
            this.dtstxtPOL.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtstxtPOL.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtstxtPOL.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtstxtPOL.Size = new System.Drawing.Size(103, 21);
            this.dtstxtPOL.TabIndex = 610;
            this.dtstxtPOL.TabStop = false;
            // 
            // stxtWarehouse
            // 
            this.stxtWarehouse.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "WarehouseID", true));
            this.stxtWarehouse.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "WarehouseName", true));
            this.stxtWarehouse.EditValue = "";
            this.stxtWarehouse.Location = new System.Drawing.Point(514, 245);
            this.stxtWarehouse.Name = "stxtWarehouse";
            this.stxtWarehouse.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtWarehouse.Properties.Appearance.Options.UseBackColor = true;
            this.stxtWarehouse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtWarehouse.Size = new System.Drawing.Size(303, 21);
            this.stxtWarehouse.TabIndex = 670;
            // 
            // stxtReturnLocation
            // 
            this.stxtReturnLocation.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "ReturnLocationID", true));
            this.stxtReturnLocation.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "ReturnLocationName", true));
            this.stxtReturnLocation.EditValue = "";
            this.stxtReturnLocation.Location = new System.Drawing.Point(514, 269);
            this.stxtReturnLocation.Name = "stxtReturnLocation";
            this.stxtReturnLocation.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtReturnLocation.Properties.Appearance.Options.UseBackColor = true;
            this.stxtReturnLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtReturnLocation.Size = new System.Drawing.Size(303, 21);
            this.stxtReturnLocation.TabIndex = 680;
            // 
            // labCloseWarehouse
            // 
            this.labCloseWarehouse.Location = new System.Drawing.Point(642, 225);
            this.labCloseWarehouse.Name = "labCloseWarehouse";
            this.labCloseWarehouse.Size = new System.Drawing.Size(49, 14);
            this.labCloseWarehouse.TabIndex = 0;
            this.labCloseWarehouse.Text = "WClosing";
            // 
            // dteWarehouseClosing
            // 
            this.dteWarehouseClosing.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ClosingWarehousedate", true));
            this.dteWarehouseClosing.EditValue = null;
            this.dteWarehouseClosing.Location = new System.Drawing.Point(717, 222);
            this.dteWarehouseClosing.Name = "dteWarehouseClosing";
            this.dteWarehouseClosing.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteWarehouseClosing.Properties.Mask.EditMask = "";
            this.dteWarehouseClosing.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteWarehouseClosing.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteWarehouseClosing.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteWarehouseClosing.Size = new System.Drawing.Size(101, 21);
            this.dteWarehouseClosing.TabIndex = 660;
            // 
            // cmbOrderNo
            // 
            this.cmbOrderNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "OceanShippingOrderNo", true));
            this.cmbOrderNo.EditValue = "";
            this.cmbOrderNo.Location = new System.Drawing.Point(514, 3);
            this.cmbOrderNo.Name = "cmbOrderNo";
            this.cmbOrderNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cmbOrderNo.Size = new System.Drawing.Size(113, 21);
            this.cmbOrderNo.TabIndex = 500;
            // 
            // stxtFinalDestination
            // 
            this.stxtFinalDestination.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "FinalDestinationID", true));
            this.stxtFinalDestination.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "FinalDestinationName", true));
            this.stxtFinalDestination.EditValue = "";
            this.stxtFinalDestination.Location = new System.Drawing.Point(100, 170);
            this.stxtFinalDestination.Name = "stxtFinalDestination";
            this.stxtFinalDestination.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtFinalDestination.Properties.Appearance.Options.UseBackColor = true;
            this.stxtFinalDestination.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtFinalDestination.Size = new System.Drawing.Size(307, 21);
            this.stxtFinalDestination.TabIndex = 410;
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.groupHBL);
            this.navBarGroupControlContainer3.Controls.Add(this.dteEstimatedDeliveryDate);
            this.navBarGroupControlContainer3.Controls.Add(this.groupMBL);
            this.navBarGroupControlContainer3.Controls.Add(this.groupLocalService);
            this.navBarGroupControlContainer3.Controls.Add(this.dteDeliveryDate);
            this.navBarGroupControlContainer3.Controls.Add(this.dteExpectedShipDate);
            this.navBarGroupControlContainer3.Controls.Add(this.groupRemark);
            this.navBarGroupControlContainer3.Controls.Add(this.dteExpectedArriveDate);
            this.navBarGroupControlContainer3.Controls.Add(this.labExpectedShipDate);
            this.navBarGroupControlContainer3.Controls.Add(this.labEstimatedDeliveryDate);
            this.navBarGroupControlContainer3.Controls.Add(this.labExpectedArriveDate);
            this.navBarGroupControlContainer3.Controls.Add(this.labDeliveryDate);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(827, 187);
            this.navBarGroupControlContainer3.TabIndex = 2;
            // 
            // groupRemark
            // 
            this.groupRemark.Controls.Add(this.txtRemark);
            this.groupRemark.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupRemark.Location = new System.Drawing.Point(0, 114);
            this.groupRemark.Name = "groupRemark";
            this.groupRemark.Size = new System.Drawing.Size(827, 73);
            this.groupRemark.TabIndex = 2;
            this.groupRemark.TabStop = false;
            this.groupRemark.Text = "Remark";
            // 
            // navBarGroupControlContainer4
            // 
            this.navBarGroupControlContainer4.Controls.Add(this.orderFeeEditPart1);
            this.navBarGroupControlContainer4.Name = "navBarGroupControlContainer4";
            this.navBarGroupControlContainer4.Size = new System.Drawing.Size(827, 220);
            this.navBarGroupControlContainer4.TabIndex = 3;
            // 
            // orderFeeEditPart1
            // 
            this.orderFeeEditPart1.BaseMultiLanguageList = null;
            this.orderFeeEditPart1.BasePartList = null;
            this.orderFeeEditPart1.CodeValuePairs = null;
            this.orderFeeEditPart1.ControlsList = null;
            this.orderFeeEditPart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderFeeEditPart1.FormName = "OrderFeeEditPart";
            this.orderFeeEditPart1.IsMultiLanguage = true;
            this.orderFeeEditPart1.Location = new System.Drawing.Point(0, 0);
            this.orderFeeEditPart1.Name = "orderFeeEditPart1";
            this.orderFeeEditPart1.Resources = null;
            this.orderFeeEditPart1.Size = new System.Drawing.Size(827, 220);
            this.orderFeeEditPart1.TabIndex = 1;
            this.orderFeeEditPart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("orderFeeEditPart1.UsedMessages")));
            // 
            // navBarDelegate
            // 
            this.navBarDelegate.Caption = "Delegate Info";
            this.navBarDelegate.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarDelegate.Expanded = true;
            this.navBarDelegate.GroupClientHeight = 316;
            this.navBarDelegate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarDelegate.Name = "navBarDelegate";
            // 
            // navBarOther
            // 
            this.navBarOther.Caption = "Other Info";
            this.navBarOther.ControlContainer = this.navBarGroupControlContainer3;
            this.navBarOther.Expanded = true;
            this.navBarOther.GroupClientHeight = 189;
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
            // tabPagePO
            // 
            this.tabPagePO.Controls.Add(this.bookingPOEditPart1);
            this.tabPagePO.Name = "tabPagePO";
            this.tabPagePO.Size = new System.Drawing.Size(834, 955);
            this.tabPagePO.Text = "PO";
            // 
            // bookingPOEditPart1
            // 
            this.bookingPOEditPart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.bookingPOEditPart1.IsDesignMode = true;
            this.bookingPOEditPart1.IsOrderPO = false;
            this.bookingPOEditPart1.Location = new System.Drawing.Point(0, 0);
            this.bookingPOEditPart1.Name = "bookingPOEditPart1";
            this.bookingPOEditPart1.OrderId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.bookingPOEditPart1.Size = new System.Drawing.Size(831, 952);
            this.bookingPOEditPart1.TabIndex = 0;
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
            this.Size = new System.Drawing.Size(864, 988);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBookingInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtHBLRequirements.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLPaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLReleaseType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMBLRequirements.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMBLPaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMBLReleaseType.Properties)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgentOfCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteSODate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteSODate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShippingLine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfReceipt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.mcmbOverseasFiler.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbBookinger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOnlyMBL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPlaceOfReceipt.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPlaceOfReceipt.Properties)).EndInit();
            this.groupLocalService.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasContract.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOCClosingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOCClosingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCYClosingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCYClosingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).EndInit();
            this.groupHBL.ResumeLayout(false);
            this.groupHBL.PerformLayout();
            this.groupMBL.ResumeLayout(false);
            this.groupMBL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPageBase.ResumeLayout(false);
            this.panelScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOL.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtWarehouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtReturnLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteWarehouseClosing.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteWarehouseClosing.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtFinalDestination.Properties)).EndInit();
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.navBarGroupControlContainer3.PerformLayout();
            this.groupRemark.ResumeLayout(false);
            this.navBarGroupControlContainer4.ResumeLayout(false);
            this.tabPagePO.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsBookingInfo;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbBookingMode;
        private DevExpress.XtraEditors.LabelControl labBookingDate;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.LabelControl labSalesType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTradeTerm;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.LabelControl labOverseasFiler;
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
        private ICP.Framework.ClientComponents.Controls.ContainerDemandControl containerDemandControl1;
        private DevExpress.XtraEditors.LabelControl labAgentOfCarrier;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private DevExpress.XtraEditors.ButtonEdit stxtAgentOfCarrier;
        private DevExpress.XtraEditors.LabelControl labSODate;
        private DevExpress.XtraEditors.LabelControl labOrderNo;
        private DevExpress.XtraEditors.CheckEdit chkHasContract;
        private DevExpress.XtraEditors.CheckEdit chkIsOnlyMBL;
        private DevExpress.XtraEditors.DateEdit dteBookingDate;
        private ICP.Common.UI.UCVoyageLookupEdit stxtVoyage;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private DevExpress.XtraEditors.LabelControl labPOL2;
        private DevExpress.XtraEditors.DateEdit dtstxtPlaceOfReceipt;
        private DevExpress.XtraEditors.LabelControl labETD;
        private DevExpress.XtraEditors.LabelControl labVoyage;
        private DevExpress.XtraEditors.LabelControl labContractNo;
        private DevExpress.XtraEditors.DateEdit dtstxtPOD;
        private DevExpress.XtraEditors.LabelControl labETA;
        private DevExpress.XtraEditors.LabelControl labPlaceOfDelivery;
        private DevExpress.XtraEditors.LabelControl labShippingLine;
        private DevExpress.XtraEditors.DateEdit dteClosingDate;
        private DevExpress.XtraEditors.DateEdit dteDOCClosingDate;
        private DevExpress.XtraEditors.LabelControl labClosingDate;
        private DevExpress.XtraEditors.LabelControl labDOCClosingDate;
        private DevExpress.XtraEditors.DateEdit dteCYClosingDate;
        private DevExpress.XtraEditors.LabelControl labCYClosingDate;
        private DevExpress.XtraEditors.LabelControl labExpectedArriveDate;
        private DevExpress.XtraEditors.LabelControl labEstimatedDeliveryDate;
        private DevExpress.XtraEditors.LabelControl labExpectedShipDate;
        private DevExpress.XtraEditors.DateEdit dteExpectedArriveDate;
        private DevExpress.XtraEditors.DateEdit dteExpectedShipDate;
        private DevExpress.XtraEditors.DateEdit dteEstimatedDeliveryDate;
        private System.Windows.Forms.GroupBox groupHBL;
        private DevExpress.XtraEditors.MemoEdit txtHBLRequirements;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbHBLPaymentTerm;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbHBLReleaseType;
        private DevExpress.XtraEditors.LabelControl labHBLPaymentTerm;
        private DevExpress.XtraEditors.LabelControl labHBLReleaseType;
        private System.Windows.Forms.GroupBox groupMBL;
        private DevExpress.XtraEditors.MemoEdit txtMBLRequirements;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMBLPaymentTerm;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMBLReleaseType;
        private DevExpress.XtraEditors.LabelControl labMBLPaymentTerm;
        private DevExpress.XtraEditors.LabelControl labMBLReleaseType;
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
        private DevExpress.XtraTab.XtraTabPage tabPagePO;
        private BookingPOEditPart bookingPOEditPart1;
        private DevExpress.XtraEditors.DateEdit dteSODate;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barSaveAs;
        private DevExpress.XtraBars.BarButtonItem barPrintBookingConfirm;
        private DevExpress.XtraBars.BarButtonItem barPrintProfit;
        private DevExpress.XtraBars.BarButtonItem barReject;
        private DevExpress.XtraBars.BarButtonItem barE_Booking;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraEditors.SpinEdit numQuantity;
        private DevExpress.XtraEditors.SpinEdit numWeight;
        private DevExpress.XtraEditors.SpinEdit numMeasurement;
        private DevExpress.XtraEditors.ButtonEdit stxtPlaceOfDelivery;
        private DevExpress.XtraEditors.MemoEdit txtCommodity;
        private DevExpress.XtraEditors.LabelControl labCommodity;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbShippingLine;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraEditors.ButtonEdit txtContractNo;
        private DevExpress.XtraEditors.XtraScrollableControl panelScroll;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarBase;
        private DevExpress.XtraNavBar.NavBarGroup navBarDelegate;
        private DevExpress.XtraNavBar.NavBarGroup navBarOther;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private GroupBox groupRemark;
        private DevExpress.XtraEditors.LabelControl labPlaceOfReceipt;
        private ICP.FCM.Common.UI.UCButtonEdit stxtPlaceOfReceipt;
        private ICP.Common.UI.UCVoyageLookupEdit stxtPreVoyage;
        private DevExpress.XtraEditors.LabelControl labPreVoyage;
        private ICP.FCM.Common.UI.UCButtonEdit stxtPOD;
        private ICP.FCM.Common.UI.UCButtonEdit stxtPOL;
        private ICP.FCM.OceanExport.UI.Common.CargoDescriptionPart cargoDescriptionPart1;
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
        private DevExpress.XtraEditors.LabelControl labFinalDestination;
        private DevExpress.XtraEditors.ButtonEdit stxtFinalDestination;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbCarrier;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer4;
        private ICP.FCM.OceanExport.UI.Order.OrderFeeEditPart orderFeeEditPart1;
        private DevExpress.XtraNavBar.NavBarGroup navBarFee;
        private DevExpress.XtraBars.BarButtonItem barAuditAndSave;
        private DevExpress.XtraEditors.LabelControl labReturnLocation;
        private DevExpress.XtraEditors.ButtonEdit stxtReturnLocation;
        private DevExpress.XtraEditors.ComboBoxEdit cmbOrderNo;
        private ICP.Framework.ClientComponents.Controls.TreeSelectBox trsSalesDep;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbSales;
        private DevExpress.XtraEditors.LabelControl labWarehouse;
        private DevExpress.XtraEditors.ButtonEdit stxtWarehouse;
        private DevExpress.XtraBars.BarButtonItem barTruck;
        private DevExpress.XtraBars.BarButtonItem barApplyAgent;
        private DevExpress.XtraBars.BarSubItem barSubPrint;
        private DevExpress.XtraBars.BarButtonItem barPrintOrder;
        private DevExpress.XtraBars.BarButtonItem barPrintInWarehouse;
        private DevExpress.XtraEditors.LabelControl labCloseWarehouse;
        private DevExpress.XtraEditors.DateEdit dteWarehouseClosing;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbSalesType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit mcmbFiler;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit mcmbOverseasFiler;
        private DevExpress.XtraEditors.LabelControl labBookinger;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit mcmbBookinger;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dtstxtPOL;
    }
}
