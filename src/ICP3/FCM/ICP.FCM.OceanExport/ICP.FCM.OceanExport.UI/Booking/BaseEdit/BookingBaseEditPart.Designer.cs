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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookingBaseEditPart));
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            this.bsBookingInfo = new System.Windows.Forms.BindingSource();
            this.cmbQuantityUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbMeasurementUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbWeightUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtMBLRequirements = new DevExpress.XtraEditors.MemoEdit();
            this.cmbMBLPaymentTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbMBLReleaseType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.dteExpectedArriveDate = new DevExpress.XtraEditors.DateEdit();
            this.dteExpectedShipDate = new DevExpress.XtraEditors.DateEdit();
            this.dteDeliveryDate = new DevExpress.XtraEditors.DateEdit();
            this.dteEstimatedDeliveryDate = new DevExpress.XtraEditors.DateEdit();
            this.txtMarks = new DevExpress.XtraEditors.MemoEdit();
            this.stxtPlaceOfDelivery = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtConsignee = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtBookingCustomer = new ICP.Business.Common.UI.BusinessContactPopupContainerEdit();
            this.stxtShipper = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.numQuantity = new DevExpress.XtraEditors.SpinEdit();
            this.numWeight = new DevExpress.XtraEditors.SpinEdit();
            this.numMeasurement = new DevExpress.XtraEditors.SpinEdit();
            this.stxtPlaceOfReceipt = new ICP.FCM.Common.UI.UCButtonEdit();
            this.stxtPOL = new ICP.FCM.Common.UI.UCButtonEdit();
            this.stxtPOD = new ICP.FCM.Common.UI.UCButtonEdit();
            this.dteSODate = new DevExpress.XtraEditors.DateEdit();
            this.stxtAgentOfCarrier = new ICP.Business.Common.UI.BusinessContactPopupContainerEdit();
            this.cmbShippingLine = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtBookingShipper = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtBookingConsignee = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtBookingNotifyParty = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.txtCommodity = new DevExpress.XtraEditors.MemoEdit();
            this.chkIsWarehouse = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsFumigate = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsInsurance = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsCustoms = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsTruck = new DevExpress.XtraEditors.CheckEdit();
            this.stxtNotifyParty = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.cmbMblTransportClause = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.memoEdit3 = new DevExpress.XtraEditors.MemoEdit();
            this.cmbHBLReleaseType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbHBLPaymentTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtHBLRequirements = new DevExpress.XtraEditors.MemoEdit();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.stxtPlacePayOrder = new ICP.FCM.Common.UI.UCButtonEdit();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.cmbCargoType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbBookingMode = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbTransportClause = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtPlacePay = new ICP.FCM.Common.UI.UCButtonEdit();
            this.mcmbOverseasFiler = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.mcmbFiler = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.mcmbBookinger = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.dteBookingDate = new DevExpress.XtraEditors.DateEdit();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbTradeTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbPaymentTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbSalesType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtState = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barSavingClose = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.barAuditAndSave = new DevExpress.XtraBars.BarButtonItem();
            this.barSopv = new DevExpress.XtraBars.BarButtonItem();
            this.barSubPrint = new DevExpress.XtraBars.BarSubItem();
            this.barPrintOrder = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintBookingConfirm = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintProfit = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintInWarehouse = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barReject = new DevExpress.XtraBars.BarButtonItem();
            this.barTruck = new DevExpress.XtraBars.BarButtonItem();
            this.barApplyAgent = new DevExpress.XtraBars.BarButtonItem();
            this.barInquireRates = new DevExpress.XtraBars.BarButtonItem();
            this.barEmailbooking = new DevExpress.XtraBars.BarButtonItem();
            this.barOrderCustoms = new DevExpress.XtraBars.BarButtonItem();
            this.barMailToCustomer = new DevExpress.XtraBars.BarSubItem();
            this.barAskCustomerForSICHS = new DevExpress.XtraBars.BarButtonItem();
            this.barAskCustomerForSIENG = new DevExpress.XtraBars.BarButtonItem();
            this.barMailSOCopyToCustomerCHS = new DevExpress.XtraBars.BarButtonItem();
            this.barMailSOCopyToCustomerENG = new DevExpress.XtraBars.BarButtonItem();
            this.barAskProfitPromiseCHS = new DevExpress.XtraBars.BarButtonItem();
            this.barAskProfitPromiseENG = new DevExpress.XtraBars.BarButtonItem();
            this.barConfirmDebitFeesCHS = new DevExpress.XtraBars.BarButtonItem();
            this.barConfirmDebitFeesENG = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButViewSOHistory = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barSavingTools = new DevExpress.XtraBars.Bar();
            this.barCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barlabMessage = new DevExpress.XtraBars.BarStaticItem();
            this.barlabSeconds = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barE_Booking = new DevExpress.XtraBars.BarButtonItem();
            this.barMailCustomer = new DevExpress.XtraBars.BarButtonItem();
            this.barLocalService = new DevExpress.XtraBars.BarButtonItem();
            this.barBLInfo = new DevExpress.XtraBars.BarButtonItem();
            this.barEvent = new DevExpress.XtraBars.BarButtonItem();
            this.barOrderCustoms2 = new DevExpress.XtraBars.BarButtonItem();
            this.barOrderWarehouse = new DevExpress.XtraBars.BarButtonItem();
            this.barOrderCommodityInspection = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButViewSOHistory2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barInquireRates2 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageBase = new DevExpress.XtraTab.XtraTabPage();
            this.paneltabPageBase = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mcmbSales = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.txtCustomsClearance = new DevExpress.XtraEditors.TextEdit();
            this.lblCustomsClearance = new DevExpress.XtraEditors.LabelControl();
            this.labBookingMode = new DevExpress.XtraEditors.LabelControl();
            this.labTransportClause = new DevExpress.XtraEditors.LabelControl();
            this.trsSalesDep = new ICP.Framework.ClientComponents.Controls.TreeSelectBox();
            this.labSales = new DevExpress.XtraEditors.LabelControl();
            this.labSalesDepartment = new DevExpress.XtraEditors.LabelControl();
            this.chkThirdPlacePay = new DevExpress.XtraEditors.CheckEdit();
            this.labBookinger = new DevExpress.XtraEditors.LabelControl();
            this.mcmbBookingBy = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.labTradeTerm = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labState = new DevExpress.XtraEditors.LabelControl();
            this.labBookingDate = new DevExpress.XtraEditors.LabelControl();
            this.labSalesType = new DevExpress.XtraEditors.LabelControl();
            this.stxtCustomer = new ICP.Business.Common.UI.CustomerBusinessContactControl();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.labBookingBy = new DevExpress.XtraEditors.LabelControl();
            this.labFiler = new DevExpress.XtraEditors.LabelControl();
            this.labOverseasFiler = new DevExpress.XtraEditors.LabelControl();
            this.labPaymentTerm = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labPlaceOfDeliveryAddress = new DevExpress.XtraEditors.LabelControl();
            this.labPlaceOfReceiptAddress = new DevExpress.XtraEditors.LabelControl();
            this.txtPlaceOfDeliveryAddress = new DevExpress.XtraEditors.TextEdit();
            this.txtPlaceOfReceiptAddress = new DevExpress.XtraEditors.TextEdit();
            this.chkOkToSub = new DevExpress.XtraEditors.CheckEdit();
            this.labCargoType = new DevExpress.XtraEditors.LabelControl();
            this.lblCommodity = new DevExpress.XtraEditors.LabelControl();
            this.labNotifyParty = new DevExpress.XtraEditors.LabelControl();
            this.groupLocalService = new System.Windows.Forms.GroupBox();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.labPOLETD = new DevExpress.XtraEditors.LabelControl();
            this.labETD = new DevExpress.XtraEditors.LabelControl();
            this.labETA = new DevExpress.XtraEditors.LabelControl();
            this.dtstxtPOD = new DevExpress.XtraEditors.DateEdit();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.labPlaceOfReceipt = new DevExpress.XtraEditors.LabelControl();
            this.labPlaceOfDelivery = new DevExpress.XtraEditors.LabelControl();
            this.labBookingCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labFinalDestination = new DevExpress.XtraEditors.LabelControl();
            this.labPOL2 = new DevExpress.XtraEditors.LabelControl();
            this.dtstxtPOL = new DevExpress.XtraEditors.DateEdit();
            this.dtstxtPlaceOfReceipt = new DevExpress.XtraEditors.DateEdit();
            this.labShipper = new DevExpress.XtraEditors.LabelControl();
            this.labConsignee = new DevExpress.XtraEditors.LabelControl();
            this.labMarks = new DevExpress.XtraEditors.LabelControl();
            this.labMeasurement = new DevExpress.XtraEditors.LabelControl();
            this.labWeight = new DevExpress.XtraEditors.LabelControl();
            this.labQuantity = new DevExpress.XtraEditors.LabelControl();
            this.stxtFinalDestination = new DevExpress.XtraEditors.ButtonEdit();
            this.containerDemandControl1 = new ICP.Framework.ClientComponents.Controls.ContainerDemandControl();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtHSCode = new DevExpress.XtraEditors.TextEdit();
            this.labHSCode = new DevExpress.XtraEditors.LabelControl();
            this.lblRemark = new DevExpress.XtraEditors.LabelControl();
            this.labMBLRemark = new DevExpress.XtraEditors.LabelControl();
            this.groupHBL = new System.Windows.Forms.GroupBox();
            this.labHBLPaymentTerm = new DevExpress.XtraEditors.LabelControl();
            this.labHBLReleaseType = new DevExpress.XtraEditors.LabelControl();
            this.labExpectedShipDate = new DevExpress.XtraEditors.LabelControl();
            this.labEstimatedDeliveryDate = new DevExpress.XtraEditors.LabelControl();
            this.labExpectedArriveDate = new DevExpress.XtraEditors.LabelControl();
            this.labDeliveryDate = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer4 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel5 = new System.Windows.Forms.Panel();
            this.orderFeeEditPart1 = new ICP.FCM.OceanExport.UI.Order.OrderFeeEditPart();
            this.navGroupColOther = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navBarGroupControlContainer5 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labContranct = new DevExpress.XtraEditors.LabelControl();
            this.dteVGMCutOff = new DevExpress.XtraEditors.DateEdit();
            this.labVGMCutOff = new DevExpress.XtraEditors.LabelControl();
            this.labGateInDate = new DevExpress.XtraEditors.LabelControl();
            this.dteGateInDate = new DevExpress.XtraEditors.DateEdit();
            this.stxtRecentQuotedPrice = new ICP.Business.Common.UI.QuotedPrice.Recent.QuotedPriceOrderControl();
            this.ucInquirePrice = new ICP.FCM.OceanExport.UI.Common.Parts.UCInquirePrice();
            this.lblRefNo = new DevExpress.XtraEditors.LabelControl();
            this.daterailcutoff = new DevExpress.XtraEditors.DateEdit();
            this.lblrailcutoff = new DevExpress.XtraEditors.LabelControl();
            this.txtBookingRefNo = new DevExpress.XtraEditors.TextEdit();
            this.txtSCAC = new DevExpress.XtraEditors.TextEdit();
            this.lblScac = new DevExpress.XtraEditors.LabelControl();
            this.mcmbBookingParty = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.chkOrderThirdPay = new DevExpress.XtraEditors.CheckEdit();
            this.labBookingParty = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labVoyageETA = new DevExpress.XtraEditors.LabelControl();
            this.dateEdit3 = new DevExpress.XtraEditors.DateEdit();
            this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.dateEdit4 = new DevExpress.XtraEditors.DateEdit();
            this.checkEdit2 = new DevExpress.XtraEditors.CheckEdit();
            this.labQuotedPriceNo = new DevExpress.XtraEditors.LabelControl();
            this.labAMSClosing = new DevExpress.XtraEditors.LabelControl();
            this.dteAMSClosing = new DevExpress.XtraEditors.DateEdit();
            this.labShippingTransportClause = new DevExpress.XtraEditors.LabelControl();
            this.labMBLPaymentTerm = new DevExpress.XtraEditors.LabelControl();
            this.labBookingExplanation = new DevExpress.XtraEditors.LabelControl();
            this.labMBLReleaseType = new DevExpress.XtraEditors.LabelControl();
            this.labPickupRequirement = new DevExpress.XtraEditors.LabelControl();
            this.NotifyParty = new DevExpress.XtraEditors.LabelControl();
            this.labShippingShipper = new DevExpress.XtraEditors.LabelControl();
            this.labShippingConsignee = new DevExpress.XtraEditors.LabelControl();
            this.labCYClosingDate = new DevExpress.XtraEditors.LabelControl();
            this.cargoDescriptionPart1 = new ICP.FCM.OceanExport.UI.Common.CargoDescriptionPart();
            this.stxtVoyage = new ICP.Common.UI.UCVoyageLookupEdit();
            this.stxtPreVoyage = new ICP.Common.UI.UCVoyageLookupEdit();
            this.labPreVoyage = new DevExpress.XtraEditors.LabelControl();
            this.chkIsOnlyMBL = new DevExpress.XtraEditors.CheckEdit();
            this.labVoyage = new DevExpress.XtraEditors.LabelControl();
            this.labPickUpDate = new DevExpress.XtraEditors.LabelControl();
            this.labWarehouse = new DevExpress.XtraEditors.LabelControl();
            this.labReturnLocation = new DevExpress.XtraEditors.LabelControl();
            this.txtContractNo = new DevExpress.XtraEditors.ButtonEdit();
            this.chkHasContract = new DevExpress.XtraEditors.CheckEdit();
            this.labShippingLine = new DevExpress.XtraEditors.LabelControl();
            this.dtePickUpDate = new DevExpress.XtraEditors.DateEdit();
            this.stxtWarehouse = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtReturnLocation = new DevExpress.XtraEditors.ButtonEdit();
            this.dteCYClosingDate = new DevExpress.XtraEditors.DateEdit();
            this.labCloseWarehouse = new DevExpress.XtraEditors.LabelControl();
            this.labDOCClosingDate = new DevExpress.XtraEditors.LabelControl();
            this.labClosingDate = new DevExpress.XtraEditors.LabelControl();
            this.dteWarehouseClosing = new DevExpress.XtraEditors.DateEdit();
            this.dteDOCClosingDate = new DevExpress.XtraEditors.DateEdit();
            this.dteClosingDate = new DevExpress.XtraEditors.DateEdit();
            this.mcmbCarrier = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.stxtAgent = new ICP.Business.Common.UI.ComboBusinessContactDetailInfoControl();
            this.labAgent = new DevExpress.XtraEditors.LabelControl();
            this.labOrderNo = new DevExpress.XtraEditors.LabelControl();
            this.labSODate = new DevExpress.XtraEditors.LabelControl();
            this.labAgentOfCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.cmbOrderNo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.navBarGroupControlContainer6 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.partDelegate = new ICP.FCM.Common.UI.CommonPart.PartBookingForCSP();
            this.navBarDelegate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarDelegates = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarBooking = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarOther = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarFee = new DevExpress.XtraNavBar.NavBarGroup();
            this.navOther = new DevExpress.XtraNavBar.NavBarGroup();
            this.tabPagePO = new DevExpress.XtraTab.XtraTabPage();
            this.paneltabPagePO = new DevExpress.XtraEditors.PanelControl();
            this.bookingPOEditPart1 = new ICP.FCM.OceanExport.UI.Booking.BookingPOEditPart();
            this.cmbQtyUnit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rspinEditInt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.rspinEditFloat = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.devErrorCheck = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBookingInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuantityUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtMarks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfReceipt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteSODate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteSODate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgentOfCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShippingLine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingShipper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingConsignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingNotifyParty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsWarehouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsFumigate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsInsurance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCustoms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTruck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtNotifyParty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMblTransportClause.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLReleaseType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLPaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHBLRequirements.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlacePayOrder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCargoType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBookingMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlacePay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbOverseasFiler.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbFiler.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbBookinger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTradeTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPageBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneltabPageBase)).BeginInit();
            this.paneltabPageBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomsClearance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkThirdPlacePay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbBookingBy.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfDeliveryAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfReceiptAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOkToSub.Properties)).BeginInit();
            this.groupLocalService.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOL.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPlaceOfReceipt.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPlaceOfReceipt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtFinalDestination.Properties)).BeginInit();
            this.navBarGroupControlContainer3.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHSCode.Properties)).BeginInit();
            this.groupHBL.SuspendLayout();
            this.navBarGroupControlContainer4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.navBarGroupControlContainer5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteVGMCutOff.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteVGMCutOff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteGateInDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteGateInDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.daterailcutoff.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.daterailcutoff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookingRefNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSCAC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOrderThirdPay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit3.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit4.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteAMSClosing.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteAMSClosing.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOnlyMBL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasContract.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePickUpDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePickUpDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtWarehouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtReturnLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCYClosingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCYClosingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteWarehouseClosing.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteWarehouseClosing.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOCClosingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOCClosingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOrderNo.Properties)).BeginInit();
            this.navBarGroupControlContainer6.SuspendLayout();
            this.tabPagePO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneltabPagePO)).BeginInit();
            this.paneltabPagePO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQtyUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditFloat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.devErrorCheck)).BeginInit();
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
            // cmbQuantityUnit
            // 
            this.cmbQuantityUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "QuantityUnitID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbQuantityUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbQuantityUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbQuantityUnit.Location = new System.Drawing.Point(581, 62);
            this.cmbQuantityUnit.Name = "cmbQuantityUnit";
            this.cmbQuantityUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbQuantityUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbQuantityUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQuantityUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbQuantityUnit.Size = new System.Drawing.Size(52, 21);
            this.cmbQuantityUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbQuantityUnit.TabIndex = 37;
            // 
            // cmbMeasurementUnit
            // 
            this.cmbMeasurementUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "MeasurementUnitID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbMeasurementUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbMeasurementUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbMeasurementUnit.Location = new System.Drawing.Point(580, 112);
            this.cmbMeasurementUnit.Name = "cmbMeasurementUnit";
            this.cmbMeasurementUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbMeasurementUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMeasurementUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMeasurementUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbMeasurementUnit.Size = new System.Drawing.Size(52, 21);
            this.cmbMeasurementUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbMeasurementUnit.TabIndex = 41;
            // 
            // cmbWeightUnit
            // 
            this.cmbWeightUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "WeightUnitID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbWeightUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbWeightUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbWeightUnit.Location = new System.Drawing.Point(581, 86);
            this.cmbWeightUnit.Name = "cmbWeightUnit";
            this.cmbWeightUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbWeightUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbWeightUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWeightUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbWeightUnit.Size = new System.Drawing.Size(52, 21);
            this.cmbWeightUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbWeightUnit.TabIndex = 39;
            // 
            // txtMBLRequirements
            // 
            this.txtMBLRequirements.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "MBLRequirements", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtMBLRequirements, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.txtMBLRequirements, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtMBLRequirements.Location = new System.Drawing.Point(418, 27);
            this.txtMBLRequirements.Name = "txtMBLRequirements";
            this.txtMBLRequirements.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMBLRequirements.Size = new System.Drawing.Size(193, 111);
            this.txtMBLRequirements.TabIndex = 408;
            // 
            // cmbMBLPaymentTerm
            // 
            this.cmbMBLPaymentTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "MBLPaymentTermID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbMBLPaymentTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbMBLPaymentTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbMBLPaymentTerm.Location = new System.Drawing.Point(88, 246);
            this.cmbMBLPaymentTerm.Name = "cmbMBLPaymentTerm";
            this.cmbMBLPaymentTerm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbMBLPaymentTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMBLPaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMBLPaymentTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbMBLPaymentTerm.Size = new System.Drawing.Size(101, 21);
            this.cmbMBLPaymentTerm.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbMBLPaymentTerm.TabIndex = 311;
            // 
            // cmbMBLReleaseType
            // 
            this.cmbMBLReleaseType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "MBLReleaseType", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbMBLReleaseType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbMBLReleaseType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbMBLReleaseType.Location = new System.Drawing.Point(88, 222);
            this.cmbMBLReleaseType.Name = "cmbMBLReleaseType";
            this.cmbMBLReleaseType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbMBLReleaseType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMBLReleaseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMBLReleaseType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbMBLReleaseType.Size = new System.Drawing.Size(101, 21);
            this.cmbMBLReleaseType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbMBLReleaseType.TabIndex = 310;
            // 
            // dteExpectedArriveDate
            // 
            this.dteExpectedArriveDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ExpectedArriveDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteExpectedArriveDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteExpectedArriveDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.dteExpectedArriveDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteExpectedArriveDate.Location = new System.Drawing.Point(88, 90);
            this.dteExpectedArriveDate.Name = "dteExpectedArriveDate";
            this.dteExpectedArriveDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteExpectedArriveDate.Properties.Mask.EditMask = "";
            this.dteExpectedArriveDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteExpectedArriveDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteExpectedArriveDate.Size = new System.Drawing.Size(111, 21);
            this.dteExpectedArriveDate.TabIndex = 404;
            // 
            // dteExpectedShipDate
            // 
            this.dteExpectedShipDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ExpectedShipDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteExpectedShipDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteExpectedShipDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.dteExpectedShipDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteExpectedShipDate.Location = new System.Drawing.Point(88, 63);
            this.dteExpectedShipDate.Name = "dteExpectedShipDate";
            this.dteExpectedShipDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteExpectedShipDate.Properties.Mask.EditMask = "";
            this.dteExpectedShipDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteExpectedShipDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteExpectedShipDate.Size = new System.Drawing.Size(111, 21);
            this.dteExpectedShipDate.TabIndex = 403;
            // 
            // dteDeliveryDate
            // 
            this.dteDeliveryDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "DeliveryDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteDeliveryDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteDeliveryDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.dteDeliveryDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteDeliveryDate.Location = new System.Drawing.Point(88, 37);
            this.dteDeliveryDate.Name = "dteDeliveryDate";
            this.dteDeliveryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDeliveryDate.Properties.Mask.EditMask = "";
            this.dteDeliveryDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteDeliveryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteDeliveryDate.Size = new System.Drawing.Size(111, 21);
            this.dteDeliveryDate.TabIndex = 402;
            // 
            // dteEstimatedDeliveryDate
            // 
            this.dteEstimatedDeliveryDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "EstimatedDeliveryDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteEstimatedDeliveryDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteEstimatedDeliveryDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.dteEstimatedDeliveryDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteEstimatedDeliveryDate.Location = new System.Drawing.Point(88, 12);
            this.dteEstimatedDeliveryDate.Name = "dteEstimatedDeliveryDate";
            this.dteEstimatedDeliveryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEstimatedDeliveryDate.Properties.Mask.EditMask = "";
            this.dteEstimatedDeliveryDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteEstimatedDeliveryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteEstimatedDeliveryDate.Size = new System.Drawing.Size(111, 21);
            this.dteEstimatedDeliveryDate.TabIndex = 401;
            // 
            // txtMarks
            // 
            this.txtMarks.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "Marks", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtMarks, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.txtMarks, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtMarks.Location = new System.Drawing.Point(511, 139);
            this.txtMarks.Name = "txtMarks";
            this.txtMarks.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtMarks.Properties.Appearance.Options.UseBackColor = true;
            this.txtMarks.Size = new System.Drawing.Size(122, 56);
            this.txtMarks.TabIndex = 42;
            // 
            // stxtPlaceOfDelivery
            // 
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "PlaceOfDeliveryID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "PlaceOfDeliveryName", true));
            this.stxtPlaceOfDelivery.EditValue = "";
            this.devErrorCheck.SetIconAlignment(this.stxtPlaceOfDelivery, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.stxtPlaceOfDelivery, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPlaceOfDelivery.Location = new System.Drawing.Point(93, 205);
            this.stxtPlaceOfDelivery.Name = "stxtPlaceOfDelivery";
            this.stxtPlaceOfDelivery.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtPlaceOfDelivery.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlaceOfDelivery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlaceOfDelivery.Size = new System.Drawing.Size(318, 21);
            this.stxtPlaceOfDelivery.TabIndex = 29;
            // 
            // stxtConsignee
            // 
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "ConsigneeID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "ConsigneeName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtConsignee, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.stxtConsignee, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtConsignee.Location = new System.Drawing.Point(93, 60);
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
            this.stxtConsignee.Size = new System.Drawing.Size(318, 21);
            this.stxtConsignee.TabIndex = 21;
            // 
            // stxtBookingCustomer
            // 
            this.stxtBookingCustomer.ContactType = ICP.Framework.CommonLibrary.Common.ContactType.Customer;
            this.stxtBookingCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "BookingCustomerID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtBookingCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "BookingCustomerName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.devErrorCheck.SetIconAlignment(this.stxtBookingCustomer, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.stxtBookingCustomer, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtBookingCustomer.Location = new System.Drawing.Point(93, 12);
            this.stxtBookingCustomer.Name = "stxtBookingCustomer";
            this.stxtBookingCustomer.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtBookingCustomer.Properties.ActionButtonIndex = 1;
            this.stxtBookingCustomer.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtBookingCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.stxtBookingCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtBookingCustomer.Properties.CloseOnLostFocus = false;
            this.stxtBookingCustomer.Properties.CloseOnOuterMouseClick = false;
            this.stxtBookingCustomer.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtBookingCustomer.Properties.PopupSizeable = false;
            this.stxtBookingCustomer.Properties.ShowPopupCloseButton = false;
            this.stxtBookingCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtBookingCustomer.Size = new System.Drawing.Size(318, 21);
            this.stxtBookingCustomer.TabIndex = 19;
            // 
            // stxtShipper
            // 
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "ShipperID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "ShipperName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtShipper, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.stxtShipper, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtShipper.Location = new System.Drawing.Point(93, 36);
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
            this.stxtShipper.Size = new System.Drawing.Size(318, 21);
            this.stxtShipper.TabIndex = 20;
            // 
            // numQuantity
            // 
            this.numQuantity.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "Quantity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numQuantity.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.devErrorCheck.SetIconAlignment(this.numQuantity, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.numQuantity, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.numQuantity.Location = new System.Drawing.Point(511, 62);
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
            this.numQuantity.Size = new System.Drawing.Size(70, 21);
            this.numQuantity.TabIndex = 36;
            // 
            // numWeight
            // 
            this.numWeight.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "Weight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numWeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.devErrorCheck.SetIconAlignment(this.numWeight, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.numWeight, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.numWeight.Location = new System.Drawing.Point(511, 86);
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
            this.numWeight.Size = new System.Drawing.Size(70, 21);
            this.numWeight.TabIndex = 38;
            // 
            // numMeasurement
            // 
            this.numMeasurement.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "Measurement", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numMeasurement.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.devErrorCheck.SetIconAlignment(this.numMeasurement, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.numMeasurement, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.numMeasurement.Location = new System.Drawing.Point(510, 112);
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
            this.numMeasurement.Size = new System.Drawing.Size(70, 21);
            this.numMeasurement.TabIndex = 40;
            // 
            // stxtPlaceOfReceipt
            // 
            this.stxtPlaceOfReceipt.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "PlaceOfReceiptID", true));
            this.stxtPlaceOfReceipt.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "PlaceOfReceiptName", true));
            this.stxtPlaceOfReceipt.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtPlaceOfReceipt, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.stxtPlaceOfReceipt, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPlaceOfReceipt.Location = new System.Drawing.Point(93, 107);
            this.stxtPlaceOfReceipt.Name = "stxtPlaceOfReceipt";
            this.stxtPlaceOfReceipt.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtPlaceOfReceipt.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlaceOfReceipt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlaceOfReceipt.Size = new System.Drawing.Size(164, 21);
            this.stxtPlaceOfReceipt.TabIndex = 23;
            this.stxtPlaceOfReceipt.Tag = null;
            // 
            // stxtPOL
            // 
            this.stxtPOL.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "POLID", true));
            this.stxtPOL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "POLName", true));
            this.stxtPOL.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtPOL, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.stxtPOL, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPOL.Location = new System.Drawing.Point(93, 157);
            this.stxtPOL.Name = "stxtPOL";
            this.stxtPOL.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtPOL.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPOL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPOL.Size = new System.Drawing.Size(164, 21);
            this.stxtPOL.TabIndex = 25;
            this.stxtPOL.Tag = null;
            // 
            // stxtPOD
            // 
            this.stxtPOD.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "PODName", true));
            this.stxtPOD.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "PODID", true));
            this.stxtPOD.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtPOD, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.stxtPOD, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPOD.Location = new System.Drawing.Point(93, 181);
            this.stxtPOD.Name = "stxtPOD";
            this.stxtPOD.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtPOD.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPOD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPOD.Size = new System.Drawing.Size(164, 21);
            this.stxtPOD.TabIndex = 27;
            this.stxtPOD.Tag = null;
            // 
            // dteSODate
            // 
            this.dteSODate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "SODate", true));
            this.dteSODate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteSODate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.dteSODate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteSODate.Location = new System.Drawing.Point(88, 27);
            this.dteSODate.Name = "dteSODate";
            this.dteSODate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteSODate.Properties.Mask.EditMask = "";
            this.dteSODate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteSODate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteSODate.Size = new System.Drawing.Size(134, 21);
            this.dteSODate.TabIndex = 302;
            // 
            // stxtAgentOfCarrier
            // 
            this.stxtAgentOfCarrier.ContactType = ICP.Framework.CommonLibrary.Common.ContactType.Customer;
            this.stxtAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "AgentOfCarrierName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "AgentOfCarrierID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.devErrorCheck.SetIconAlignment(this.stxtAgentOfCarrier, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.stxtAgentOfCarrier, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtAgentOfCarrier.Location = new System.Drawing.Point(88, 99);
            this.stxtAgentOfCarrier.Name = "stxtAgentOfCarrier";
            this.stxtAgentOfCarrier.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtAgentOfCarrier.Properties.ActionButtonIndex = 1;
            this.stxtAgentOfCarrier.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtAgentOfCarrier.Properties.Appearance.Options.UseBackColor = true;
            this.stxtAgentOfCarrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtAgentOfCarrier.Properties.CloseOnLostFocus = false;
            this.stxtAgentOfCarrier.Properties.CloseOnOuterMouseClick = false;
            this.stxtAgentOfCarrier.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtAgentOfCarrier.Properties.PopupSizeable = false;
            this.stxtAgentOfCarrier.Properties.ShowPopupCloseButton = false;
            this.stxtAgentOfCarrier.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtAgentOfCarrier.Size = new System.Drawing.Size(318, 21);
            this.stxtAgentOfCarrier.TabIndex = 305;
            // 
            // cmbShippingLine
            // 
            this.cmbShippingLine.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ShippingLineID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbShippingLine, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbShippingLine, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbShippingLine.Location = new System.Drawing.Point(504, 345);
            this.cmbShippingLine.Name = "cmbShippingLine";
            this.cmbShippingLine.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbShippingLine.Properties.Appearance.Options.UseBackColor = true;
            this.cmbShippingLine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbShippingLine.Properties.ReadOnly = true;
            this.cmbShippingLine.Size = new System.Drawing.Size(122, 21);
            this.cmbShippingLine.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbShippingLine.TabIndex = 333;
            // 
            // stxtBookingShipper
            // 
            this.stxtBookingShipper.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "BookingShipperID", true));
            this.stxtBookingShipper.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "BookingShipperName", true));
            this.dxErrorProvider1.SetIconAlignment(this.stxtBookingShipper, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.stxtBookingShipper, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtBookingShipper.Location = new System.Drawing.Point(88, 123);
            this.stxtBookingShipper.Name = "stxtBookingShipper";
            this.stxtBookingShipper.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtBookingShipper.Properties.ActionButtonIndex = 1;
            this.stxtBookingShipper.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtBookingShipper.Properties.Appearance.Options.UseBackColor = true;
            this.stxtBookingShipper.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtBookingShipper.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtBookingShipper.Properties.PopupSizeable = false;
            this.stxtBookingShipper.Properties.ShowPopupCloseButton = false;
            this.stxtBookingShipper.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtBookingShipper.Size = new System.Drawing.Size(318, 21);
            this.stxtBookingShipper.TabIndex = 306;
            // 
            // stxtBookingConsignee
            // 
            this.stxtBookingConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "BookingConsigneeID", true));
            this.stxtBookingConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "BookingConsigneeName", true));
            this.dxErrorProvider1.SetIconAlignment(this.stxtBookingConsignee, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.stxtBookingConsignee, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtBookingConsignee.Location = new System.Drawing.Point(88, 147);
            this.stxtBookingConsignee.Name = "stxtBookingConsignee";
            this.stxtBookingConsignee.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtBookingConsignee.Properties.ActionButtonIndex = 1;
            this.stxtBookingConsignee.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtBookingConsignee.Properties.Appearance.Options.UseBackColor = true;
            this.stxtBookingConsignee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtBookingConsignee.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtBookingConsignee.Properties.PopupSizeable = false;
            this.stxtBookingConsignee.Properties.ShowPopupCloseButton = false;
            this.stxtBookingConsignee.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtBookingConsignee.Size = new System.Drawing.Size(318, 21);
            this.stxtBookingConsignee.TabIndex = 307;
            // 
            // stxtBookingNotifyParty
            // 
            this.stxtBookingNotifyParty.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "BookingNotifyPartyID", true));
            this.stxtBookingNotifyParty.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "BookingNotifyPartyname", true));
            this.dxErrorProvider1.SetIconAlignment(this.stxtBookingNotifyParty, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.stxtBookingNotifyParty, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtBookingNotifyParty.Location = new System.Drawing.Point(88, 171);
            this.stxtBookingNotifyParty.Name = "stxtBookingNotifyParty";
            this.stxtBookingNotifyParty.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtBookingNotifyParty.Properties.ActionButtonIndex = 1;
            this.stxtBookingNotifyParty.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtBookingNotifyParty.Properties.Appearance.Options.UseBackColor = true;
            this.stxtBookingNotifyParty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtBookingNotifyParty.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtBookingNotifyParty.Properties.PopupSizeable = false;
            this.stxtBookingNotifyParty.Properties.ShowPopupCloseButton = false;
            this.stxtBookingNotifyParty.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtBookingNotifyParty.Size = new System.Drawing.Size(318, 21);
            this.stxtBookingNotifyParty.TabIndex = 308;
            // 
            // txtCommodity
            // 
            this.txtCommodity.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "Commodity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCommodity, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.txtCommodity, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCommodity.Location = new System.Drawing.Point(636, 112);
            this.txtCommodity.Name = "txtCommodity";
            this.txtCommodity.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtCommodity.Properties.Appearance.Options.UseBackColor = true;
            this.txtCommodity.Size = new System.Drawing.Size(189, 83);
            this.txtCommodity.TabIndex = 45;
            // 
            // chkIsWarehouse
            // 
            this.chkIsWarehouse.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsWareHouse", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.devErrorCheck.SetIconAlignment(this.chkIsWarehouse, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.chkIsWarehouse, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsWarehouse.Location = new System.Drawing.Point(6, 17);
            this.chkIsWarehouse.Name = "chkIsWarehouse";
            this.chkIsWarehouse.Properties.Caption = "Warehouse";
            this.chkIsWarehouse.Size = new System.Drawing.Size(85, 19);
            this.chkIsWarehouse.TabIndex = 31;
            // 
            // chkIsFumigate
            // 
            this.chkIsFumigate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsFumigation", true));
            this.devErrorCheck.SetIconAlignment(this.chkIsFumigate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.chkIsFumigate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsFumigate.Location = new System.Drawing.Point(308, 17);
            this.chkIsFumigate.Name = "chkIsFumigate";
            this.chkIsFumigate.Properties.Caption = "Fumigate";
            this.chkIsFumigate.Size = new System.Drawing.Size(80, 19);
            this.chkIsFumigate.TabIndex = 35;
            // 
            // chkIsInsurance
            // 
            this.chkIsInsurance.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsInsurance", true));
            this.devErrorCheck.SetIconAlignment(this.chkIsInsurance, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.chkIsInsurance, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsInsurance.Location = new System.Drawing.Point(226, 17);
            this.chkIsInsurance.Name = "chkIsInsurance";
            this.chkIsInsurance.Properties.Caption = "Insurance";
            this.chkIsInsurance.Size = new System.Drawing.Size(78, 19);
            this.chkIsInsurance.TabIndex = 34;
            // 
            // chkIsCustoms
            // 
            this.chkIsCustoms.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsCustoms", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.devErrorCheck.SetIconAlignment(this.chkIsCustoms, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.chkIsCustoms, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsCustoms.Location = new System.Drawing.Point(152, 17);
            this.chkIsCustoms.Name = "chkIsCustoms";
            this.chkIsCustoms.Properties.Caption = "Customs";
            this.chkIsCustoms.Size = new System.Drawing.Size(75, 19);
            this.chkIsCustoms.TabIndex = 33;
            // 
            // chkIsTruck
            // 
            this.chkIsTruck.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsTruck", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.devErrorCheck.SetIconAlignment(this.chkIsTruck, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.chkIsTruck, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsTruck.Location = new System.Drawing.Point(92, 17);
            this.chkIsTruck.Name = "chkIsTruck";
            this.chkIsTruck.Properties.Caption = "Truck";
            this.chkIsTruck.Size = new System.Drawing.Size(59, 19);
            this.chkIsTruck.TabIndex = 32;
            // 
            // stxtNotifyParty
            // 
            this.stxtNotifyParty.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "NotifyPartyID", true));
            this.stxtNotifyParty.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "NotifyPartyname", true));
            this.dxErrorProvider1.SetIconAlignment(this.stxtNotifyParty, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.stxtNotifyParty, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtNotifyParty.Location = new System.Drawing.Point(93, 83);
            this.stxtNotifyParty.Name = "stxtNotifyParty";
            this.stxtNotifyParty.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtNotifyParty.Properties.ActionButtonIndex = 1;
            this.stxtNotifyParty.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtNotifyParty.Properties.Appearance.Options.UseBackColor = true;
            this.stxtNotifyParty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtNotifyParty.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtNotifyParty.Properties.PopupSizeable = false;
            this.stxtNotifyParty.Properties.ShowPopupCloseButton = false;
            this.stxtNotifyParty.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtNotifyParty.Size = new System.Drawing.Size(318, 21);
            this.stxtNotifyParty.TabIndex = 22;
            // 
            // cmbMblTransportClause
            // 
            this.cmbMblTransportClause.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "MBLTransportClauseID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbMblTransportClause, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbMblTransportClause, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbMblTransportClause.Location = new System.Drawing.Point(88, 198);
            this.cmbMblTransportClause.Name = "cmbMblTransportClause";
            this.cmbMblTransportClause.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbMblTransportClause.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMblTransportClause.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMblTransportClause.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbMblTransportClause.Size = new System.Drawing.Size(101, 21);
            this.cmbMblTransportClause.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbMblTransportClause.TabIndex = 309;
            // 
            // memoEdit3
            // 
            this.memoEdit3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "BookingExplanation", true));
            this.dxErrorProvider1.SetIconAlignment(this.memoEdit3, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.memoEdit3, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.memoEdit3.Location = new System.Drawing.Point(272, 198);
            this.memoEdit3.Name = "memoEdit3";
            this.memoEdit3.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.memoEdit3.Size = new System.Drawing.Size(135, 41);
            this.memoEdit3.TabIndex = 313;
            // 
            // cmbHBLReleaseType
            // 
            this.cmbHBLReleaseType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "HBLReleaseType", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbHBLReleaseType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbHBLReleaseType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbHBLReleaseType.Location = new System.Drawing.Point(95, 38);
            this.cmbHBLReleaseType.Name = "cmbHBLReleaseType";
            this.cmbHBLReleaseType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbHBLReleaseType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbHBLReleaseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHBLReleaseType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbHBLReleaseType.Size = new System.Drawing.Size(96, 21);
            this.cmbHBLReleaseType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbHBLReleaseType.TabIndex = 406;
            // 
            // cmbHBLPaymentTerm
            // 
            this.cmbHBLPaymentTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "HBLPaymentTermID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbHBLPaymentTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbHBLPaymentTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbHBLPaymentTerm.Location = new System.Drawing.Point(95, 14);
            this.cmbHBLPaymentTerm.Name = "cmbHBLPaymentTerm";
            this.cmbHBLPaymentTerm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbHBLPaymentTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbHBLPaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHBLPaymentTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbHBLPaymentTerm.Size = new System.Drawing.Size(96, 21);
            this.cmbHBLPaymentTerm.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbHBLPaymentTerm.TabIndex = 405;
            // 
            // txtHBLRequirements
            // 
            this.txtHBLRequirements.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "HBLRequirements", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtHBLRequirements, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.txtHBLRequirements, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtHBLRequirements.Location = new System.Drawing.Point(7, 62);
            this.txtHBLRequirements.Name = "txtHBLRequirements";
            this.txtHBLRequirements.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHBLRequirements.Size = new System.Drawing.Size(184, 70);
            this.txtHBLRequirements.TabIndex = 407;
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "Remark", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtRemark, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.txtRemark, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtRemark.Location = new System.Drawing.Point(622, 27);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemark.Size = new System.Drawing.Size(193, 111);
            this.txtRemark.TabIndex = 409;
            // 
            // stxtPlacePayOrder
            // 
            this.stxtPlacePayOrder.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "CollectbyAgentOrderID", true));
            this.stxtPlacePayOrder.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "CollectbyAgentNameOrder", true));
            this.stxtPlacePayOrder.EditValue = "";
            this.stxtPlacePayOrder.Enabled = false;
            this.dxErrorProvider1.SetIconAlignment(this.stxtPlacePayOrder, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.stxtPlacePayOrder, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPlacePayOrder.Location = new System.Drawing.Point(88, 270);
            this.stxtPlacePayOrder.Name = "stxtPlacePayOrder";
            this.stxtPlacePayOrder.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.stxtPlacePayOrder.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlacePayOrder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlacePayOrder.Size = new System.Drawing.Size(100, 21);
            this.stxtPlacePayOrder.TabIndex = 312;
            this.stxtPlacePayOrder.Tag = null;
            // 
            // memoEdit1
            // 
            this.memoEdit1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "PickupRequirement", true));
            this.dxErrorProvider1.SetIconAlignment(this.memoEdit1, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.memoEdit1, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.memoEdit1.Location = new System.Drawing.Point(272, 247);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.memoEdit1.Size = new System.Drawing.Size(135, 43);
            this.memoEdit1.TabIndex = 314;
            // 
            // cmbCargoType
            // 
            this.dxErrorProvider1.SetIconAlignment(this.cmbCargoType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbCargoType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbCargoType.Location = new System.Drawing.Point(705, 62);
            this.cmbCargoType.Name = "cmbCargoType";
            this.cmbCargoType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCargoType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCargoType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCargoType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCargoType.Size = new System.Drawing.Size(120, 21);
            this.cmbCargoType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCargoType.TabIndex = 797;
            // 
            // cmbBookingMode
            // 
            this.cmbBookingMode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "BookingMode", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbBookingMode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbBookingMode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbBookingMode.Location = new System.Drawing.Point(700, 56);
            this.cmbBookingMode.Name = "cmbBookingMode";
            this.cmbBookingMode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbBookingMode.Properties.Appearance.Options.UseBackColor = true;
            this.cmbBookingMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBookingMode.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbBookingMode.Size = new System.Drawing.Size(110, 21);
            this.cmbBookingMode.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbBookingMode.TabIndex = 417;
            // 
            // cmbTransportClause
            // 
            this.cmbTransportClause.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "TransportClauseID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbTransportClause, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbTransportClause, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbTransportClause.Location = new System.Drawing.Point(500, 56);
            this.cmbTransportClause.Name = "cmbTransportClause";
            this.cmbTransportClause.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbTransportClause.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTransportClause.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTransportClause.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbTransportClause.Size = new System.Drawing.Size(113, 21);
            this.cmbTransportClause.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbTransportClause.TabIndex = 416;
            // 
            // stxtPlacePay
            // 
            this.stxtPlacePay.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "CollectbyAgentID", true));
            this.stxtPlacePay.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "CollectbyAgentName", true));
            this.stxtPlacePay.EditValue = "";
            this.stxtPlacePay.Enabled = false;
            this.dxErrorProvider1.SetIconAlignment(this.stxtPlacePay, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.stxtPlacePay, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPlacePay.Location = new System.Drawing.Point(699, 105);
            this.stxtPlacePay.Name = "stxtPlacePay";
            this.stxtPlacePay.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.stxtPlacePay.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlacePay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlacePay.Size = new System.Drawing.Size(109, 21);
            this.stxtPlacePay.TabIndex = 420;
            this.stxtPlacePay.Tag = null;
            // 
            // mcmbOverseasFiler
            // 
            this.mcmbOverseasFiler.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "OverSeasFilerID", true));
            this.dxErrorProvider1.SetIconAlignment(this.mcmbOverseasFiler, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.mcmbOverseasFiler, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.mcmbOverseasFiler.Location = new System.Drawing.Point(700, 3);
            this.mcmbOverseasFiler.Name = "mcmbOverseasFiler";
            this.mcmbOverseasFiler.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.mcmbOverseasFiler.Properties.Appearance.Options.UseBackColor = true;
            this.mcmbOverseasFiler.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mcmbOverseasFiler.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.mcmbOverseasFiler.Size = new System.Drawing.Size(110, 21);
            this.mcmbOverseasFiler.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbOverseasFiler.TabIndex = 3;
            // 
            // mcmbFiler
            // 
            this.mcmbFiler.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "FilerId", true));
            this.dxErrorProvider1.SetIconAlignment(this.mcmbFiler, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.mcmbFiler, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.mcmbFiler.Location = new System.Drawing.Point(500, 27);
            this.mcmbFiler.Name = "mcmbFiler";
            this.mcmbFiler.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.mcmbFiler.Properties.Appearance.Options.UseBackColor = true;
            this.mcmbFiler.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mcmbFiler.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.mcmbFiler.Size = new System.Drawing.Size(113, 21);
            this.mcmbFiler.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbFiler.TabIndex = 414;
            // 
            // mcmbBookinger
            // 
            this.mcmbBookinger.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "BookingerID", true));
            this.dxErrorProvider1.SetIconAlignment(this.mcmbBookinger, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.mcmbBookinger, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.mcmbBookinger.Location = new System.Drawing.Point(500, 3);
            this.mcmbBookinger.Name = "mcmbBookinger";
            this.mcmbBookinger.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.mcmbBookinger.Properties.Appearance.Options.UseBackColor = true;
            this.mcmbBookinger.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mcmbBookinger.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.mcmbBookinger.Size = new System.Drawing.Size(113, 21);
            this.mcmbBookinger.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.mcmbBookinger.TabIndex = 2;
            // 
            // dteBookingDate
            // 
            this.dteBookingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "BookingDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteBookingDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteBookingDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.dteBookingDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteBookingDate.Location = new System.Drawing.Point(699, 83);
            this.dteBookingDate.Name = "dteBookingDate";
            this.dteBookingDate.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.dteBookingDate.Properties.Appearance.Options.UseBackColor = true;
            this.dteBookingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteBookingDate.Properties.Mask.EditMask = "";
            this.dteBookingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteBookingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteBookingDate.Size = new System.Drawing.Size(110, 21);
            this.dteBookingDate.TabIndex = 419;
            // 
            // txtNo
            // 
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "No", true));
            this.txtNo.EditValue = "";
            this.devErrorCheck.SetIconAlignment(this.txtNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.txtNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtNo.Location = new System.Drawing.Point(81, 3);
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.ReadOnly = true;
            this.txtNo.Size = new System.Drawing.Size(118, 21);
            this.txtNo.TabIndex = 0;
            this.txtNo.TabStop = false;
            // 
            // cmbCompany
            // 
            this.cmbCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "CompanyID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbCompany, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbCompany, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbCompany.Location = new System.Drawing.Point(80, 29);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCompany.Size = new System.Drawing.Size(119, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbCompany.TabIndex = 4;
            // 
            // cmbTradeTerm
            // 
            this.cmbTradeTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "TradeTermID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbTradeTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbTradeTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbTradeTerm.Location = new System.Drawing.Point(80, 83);
            this.cmbTradeTerm.Name = "cmbTradeTerm";
            this.cmbTradeTerm.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbTradeTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTradeTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTradeTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbTradeTerm.Size = new System.Drawing.Size(117, 21);
            this.cmbTradeTerm.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbTradeTerm.TabIndex = 408;
            // 
            // cmbPaymentTerm
            // 
            this.cmbPaymentTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "PaymentTermID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbPaymentTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbPaymentTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbPaymentTerm.Location = new System.Drawing.Point(499, 83);
            this.cmbPaymentTerm.Name = "cmbPaymentTerm";
            this.cmbPaymentTerm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbPaymentTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbPaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaymentTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbPaymentTerm.Size = new System.Drawing.Size(113, 21);
            this.cmbPaymentTerm.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbPaymentTerm.TabIndex = 418;
            // 
            // cmbSalesType
            // 
            this.cmbSalesType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "SalesTypeID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbSalesType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbSalesType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbSalesType.Location = new System.Drawing.Point(269, 109);
            this.cmbSalesType.Name = "cmbSalesType";
            this.cmbSalesType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbSalesType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbSalesType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSalesType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbSalesType.Size = new System.Drawing.Size(125, 21);
            this.cmbSalesType.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbSalesType.TabIndex = 411;
            // 
            // cmbType
            // 
            this.cmbType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "OEOperationType", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbType.Location = new System.Drawing.Point(269, 27);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbType.Size = new System.Drawing.Size(125, 21);
            this.cmbType.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbType.TabIndex = 5;
            // 
            // txtState
            // 
            this.devErrorCheck.SetIconAlignment(this.txtState, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.txtState, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtState.Location = new System.Drawing.Point(269, 3);
            this.txtState.Name = "txtState";
            this.txtState.Properties.ReadOnly = true;
            this.txtState.Size = new System.Drawing.Size(125, 21);
            this.txtState.TabIndex = 1;
            this.txtState.TabStop = false;
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.barSavingTools});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.barDockControl1);
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
            this.barPrintInWarehouse,
            this.barMailCustomer,
            this.barLocalService,
            this.barBLInfo,
            this.barEvent,
            this.barMailToCustomer,
            this.barAskCustomerForSICHS,
            this.barAskCustomerForSIENG,
            this.barMailSOCopyToCustomerCHS,
            this.barMailSOCopyToCustomerENG,
            this.barOrderCustoms2,
            this.barOrderWarehouse,
            this.barOrderCommodityInspection,
            this.barButtonItem1,
            this.barButViewSOHistory2,
            this.barAskProfitPromiseCHS,
            this.barAskProfitPromiseENG,
            this.barConfirmDebitFeesCHS,
            this.barConfirmDebitFeesENG,
            this.barButtonItem2,
            this.barInquireRates2,
            this.barSopv,
            this.barEmailbooking,
            this.barButViewSOHistory,
            this.barInquireRates,
            this.barOrderCustoms,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barSavingClose,
            this.barCancel,
            this.barlabMessage,
            this.barlabSeconds});
            this.barManager1.MaxItemId = 80;
            // 
            // bar2
            // 
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(109, 177);
            this.bar2.FloatSize = new System.Drawing.Size(1327, 52);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSavingClose, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSaveAs, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAuditAndSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSopv, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barReject, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barTruck, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barApplyAgent, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barInquireRates, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEmailbooking, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barOrderCustoms, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barMailToCustomer, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButViewSOHistory, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
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
            // barSavingClose
            // 
            this.barSavingClose.Caption = "Save && Close";
            this.barSavingClose.Glyph = ((System.Drawing.Image)(resources.GetObject("barSavingClose.Glyph")));
            this.barSavingClose.Id = 75;
            this.barSavingClose.Name = "barSavingClose";
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
            this.barAuditAndSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barAuditAndSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAuditAndSave_ItemClick);
            // 
            // barSopv
            // 
            this.barSopv.Caption = "&Audit&&Save";
            this.barSopv.Glyph = ((System.Drawing.Image)(resources.GetObject("barSopv.Glyph")));
            this.barSopv.Id = 66;
            this.barSopv.Name = "barSopv";
            this.barSopv.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSopv_ItemClick);
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
            this.barApplyAgent.Id = 13;
            this.barApplyAgent.Name = "barApplyAgent";
            this.barApplyAgent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barApplyAgent_ItemClick);
            // 
            // barInquireRates
            // 
            this.barInquireRates.Caption = "InquireRates";
            this.barInquireRates.Glyph = ((System.Drawing.Image)(resources.GetObject("barInquireRates.Glyph")));
            this.barInquireRates.Id = 70;
            this.barInquireRates.Name = "barInquireRates";
            this.barInquireRates.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barInquireRates_ItemClick);
            // 
            // barEmailbooking
            // 
            this.barEmailbooking.Caption = "EmailBooking";
            this.barEmailbooking.Glyph = ((System.Drawing.Image)(resources.GetObject("barEmailbooking.Glyph")));
            this.barEmailbooking.Id = 67;
            this.barEmailbooking.Name = "barEmailbooking";
            this.barEmailbooking.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEmailbooking_ItemClick);
            // 
            // barOrderCustoms
            // 
            this.barOrderCustoms.Caption = "Order Customs";
            this.barOrderCustoms.Glyph = ((System.Drawing.Image)(resources.GetObject("barOrderCustoms.Glyph")));
            this.barOrderCustoms.Id = 71;
            this.barOrderCustoms.Name = "barOrderCustoms";
            this.barOrderCustoms.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barOrderCustoms_ItemClick);
            // 
            // barMailToCustomer
            // 
            this.barMailToCustomer.Caption = "EMail";
            this.barMailToCustomer.Glyph = ((System.Drawing.Image)(resources.GetObject("barMailToCustomer.Glyph")));
            this.barMailToCustomer.Id = 28;
            this.barMailToCustomer.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAskCustomerForSICHS),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAskCustomerForSIENG),
            new DevExpress.XtraBars.LinkPersistInfo(this.barMailSOCopyToCustomerCHS),
            new DevExpress.XtraBars.LinkPersistInfo(this.barMailSOCopyToCustomerENG),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAskProfitPromiseCHS),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAskProfitPromiseENG),
            new DevExpress.XtraBars.LinkPersistInfo(this.barConfirmDebitFeesCHS),
            new DevExpress.XtraBars.LinkPersistInfo(this.barConfirmDebitFeesENG),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5)});
            this.barMailToCustomer.Name = "barMailToCustomer";
            // 
            // barAskCustomerForSICHS
            // 
            this.barAskCustomerForSICHS.Caption = "Ask Customer For SI (CHS)";
            this.barAskCustomerForSICHS.Id = 29;
            this.barAskCustomerForSICHS.Name = "barAskCustomerForSICHS";
            this.barAskCustomerForSICHS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAskCustomerForSICHS_ItemClick);
            // 
            // barAskCustomerForSIENG
            // 
            this.barAskCustomerForSIENG.Caption = "Ask Customer For SI (ENG)";
            this.barAskCustomerForSIENG.Id = 30;
            this.barAskCustomerForSIENG.Name = "barAskCustomerForSIENG";
            this.barAskCustomerForSIENG.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAskCustomerForSIENG_ItemClick);
            // 
            // barMailSOCopyToCustomerCHS
            // 
            this.barMailSOCopyToCustomerCHS.Caption = "Mail ADJ SO Copy To Customer (CHS)";
            this.barMailSOCopyToCustomerCHS.Id = 31;
            this.barMailSOCopyToCustomerCHS.Name = "barMailSOCopyToCustomerCHS";
            this.barMailSOCopyToCustomerCHS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barMailSOCopyToCustomerCHS_ItemClick);
            // 
            // barMailSOCopyToCustomerENG
            // 
            this.barMailSOCopyToCustomerENG.Caption = "Mail ADJ SO Copy To Customer (ENG)";
            this.barMailSOCopyToCustomerENG.Id = 32;
            this.barMailSOCopyToCustomerENG.Name = "barMailSOCopyToCustomerENG";
            this.barMailSOCopyToCustomerENG.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barMailSOCopyToCustomerENG_ItemClick);
            // 
            // barAskProfitPromiseCHS
            // 
            this.barAskProfitPromiseCHS.Caption = "Ask Profit Promise(CHS)";
            this.barAskProfitPromiseCHS.Id = 60;
            this.barAskProfitPromiseCHS.Name = "barAskProfitPromiseCHS";
            this.barAskProfitPromiseCHS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAskProfitPromiseCHS_ItemClick);
            // 
            // barAskProfitPromiseENG
            // 
            this.barAskProfitPromiseENG.Caption = "Ask Profit Promise(ENG)";
            this.barAskProfitPromiseENG.Id = 61;
            this.barAskProfitPromiseENG.Name = "barAskProfitPromiseENG";
            this.barAskProfitPromiseENG.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAskProfitPromiseENG_ItemClick);
            // 
            // barConfirmDebitFeesCHS
            // 
            this.barConfirmDebitFeesCHS.Caption = "Confirm Debit Fees(CHS)";
            this.barConfirmDebitFeesCHS.Id = 62;
            this.barConfirmDebitFeesCHS.Name = "barConfirmDebitFeesCHS";
            this.barConfirmDebitFeesCHS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barConfirmDebitFeesCHS_ItemClick);
            // 
            // barConfirmDebitFeesENG
            // 
            this.barConfirmDebitFeesENG.Caption = "Confirm Debit Fees(ENG)";
            this.barConfirmDebitFeesENG.Id = 63;
            this.barConfirmDebitFeesENG.Name = "barConfirmDebitFeesENG";
            this.barConfirmDebitFeesENG.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barConfirmDebitFeesENG_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Mail So Confirmation To Customer (CHS)";
            this.barButtonItem3.Id = 72;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Mail So Confirmation To Customer (ENG)";
            this.barButtonItem4.Id = 73;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "Mail So Confirmation To Agent";
            this.barButtonItem5.Id = 74;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // barButViewSOHistory
            // 
            this.barButViewSOHistory.Caption = "ViewSOHistory";
            this.barButViewSOHistory.Glyph = ((System.Drawing.Image)(resources.GetObject("barButViewSOHistory.Glyph")));
            this.barButViewSOHistory.Id = 69;
            this.barButViewSOHistory.Name = "barButViewSOHistory";
            this.barButViewSOHistory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButViewSOHistory_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = ((System.Drawing.Image)(resources.GetObject("barClose.Glyph")));
            this.barClose.Id = 8;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barSavingTools
            // 
            this.barSavingTools.BarName = "SavingTools";
            this.barSavingTools.DockCol = 0;
            this.barSavingTools.DockRow = 1;
            this.barSavingTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barSavingTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCancel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barlabMessage, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barlabSeconds)});
            this.barSavingTools.OptionsBar.AllowQuickCustomization = false;
            this.barSavingTools.OptionsBar.DrawDragBorder = false;
            this.barSavingTools.OptionsBar.UseWholeRow = true;
            this.barSavingTools.Text = "SavingTools";
            // 
            // barCancel
            // 
            this.barCancel.Caption = "Cancel";
            this.barCancel.Glyph = ((System.Drawing.Image)(resources.GetObject("barCancel.Glyph")));
            this.barCancel.Id = 77;
            this.barCancel.Name = "barCancel";
            // 
            // barlabMessage
            // 
            this.barlabMessage.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barlabMessage.Caption = "Message";
            this.barlabMessage.Glyph = ((System.Drawing.Image)(resources.GetObject("barlabMessage.Glyph")));
            this.barlabMessage.Id = 78;
            this.barlabMessage.ImageIndex = 1;
            this.barlabMessage.Name = "barlabMessage";
            this.barlabMessage.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barlabSeconds
            // 
            this.barlabSeconds.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barlabSeconds.Caption = "0s";
            this.barlabSeconds.Id = 79;
            this.barlabSeconds.Name = "barlabSeconds";
            this.barlabSeconds.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(900, 53);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 555);
            this.barDockControlBottom.Size = new System.Drawing.Size(900, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlRight.Location = new System.Drawing.Point(0, 53);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 502);
            // 
            // barDockControl1
            // 
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl1.Location = new System.Drawing.Point(900, 53);
            this.barDockControl1.Size = new System.Drawing.Size(0, 502);
            // 
            // barE_Booking
            // 
            this.barE_Booking.Caption = "&E_Booking";
            this.barE_Booking.Glyph = ((System.Drawing.Image)(resources.GetObject("barE_Booking.Glyph")));
            this.barE_Booking.Id = 6;
            this.barE_Booking.Name = "barE_Booking";
            this.barE_Booking.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barE_Booking_ItemClick);
            // 
            // barMailCustomer
            // 
            this.barMailCustomer.Caption = "Mail To Customer ";
            this.barMailCustomer.Id = 21;
            this.barMailCustomer.Name = "barMailCustomer";
            // 
            // barLocalService
            // 
            this.barLocalService.Caption = "Local Service";
            this.barLocalService.Id = 22;
            this.barLocalService.Name = "barLocalService";
            // 
            // barBLInfo
            // 
            this.barBLInfo.Caption = "BL Info";
            this.barBLInfo.Id = 23;
            this.barBLInfo.Name = "barBLInfo";
            // 
            // barEvent
            // 
            this.barEvent.Caption = "Event";
            this.barEvent.Id = 24;
            this.barEvent.Name = "barEvent";
            // 
            // barOrderCustoms2
            // 
            this.barOrderCustoms2.Caption = "Order Customs";
            this.barOrderCustoms2.Id = 37;
            this.barOrderCustoms2.Name = "barOrderCustoms2";
            this.barOrderCustoms2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barOrderCustoms_ItemClick);
            // 
            // barOrderWarehouse
            // 
            this.barOrderWarehouse.Caption = "Order Warehouse";
            this.barOrderWarehouse.Id = 38;
            this.barOrderWarehouse.Name = "barOrderWarehouse";
            // 
            // barOrderCommodityInspection
            // 
            this.barOrderCommodityInspection.Caption = "Order Commodity Inspection";
            this.barOrderCommodityInspection.Id = 39;
            this.barOrderCommodityInspection.Name = "barOrderCommodityInspection";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "View SO History";
            this.barButtonItem1.Id = 56;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButViewSOHistory2
            // 
            this.barButViewSOHistory2.Caption = "View SO History";
            this.barButViewSOHistory2.Id = 58;
            this.barButViewSOHistory2.Name = "barButViewSOHistory2";
            this.barButViewSOHistory2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButViewSOHistory_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Inquire Rates";
            this.barButtonItem2.Id = 64;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barInquireRates2
            // 
            this.barInquireRates2.Caption = "Inquire Rates";
            this.barInquireRates2.Id = 65;
            this.barInquireRates2.Name = "barInquireRates2";
            this.barInquireRates2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barInquireRates_ItemClick);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 0);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 53);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabPageBase;
            this.xtraTabControl1.Size = new System.Drawing.Size(900, 502);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageBase,
            this.tabPagePO});
            // 
            // tabPageBase
            // 
            this.tabPageBase.Appearance.PageClient.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tabPageBase.Appearance.PageClient.Options.UseBackColor = true;
            this.tabPageBase.Controls.Add(this.paneltabPageBase);
            this.tabPageBase.Name = "tabPageBase";
            this.tabPageBase.Size = new System.Drawing.Size(870, 495);
            this.tabPageBase.Text = "Base";
            // 
            // paneltabPageBase
            // 
            this.paneltabPageBase.Controls.Add(this.navBarControl1);
            this.paneltabPageBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paneltabPageBase.Location = new System.Drawing.Point(0, 0);
            this.paneltabPageBase.Name = "paneltabPageBase";
            this.paneltabPageBase.Size = new System.Drawing.Size(870, 495);
            this.paneltabPageBase.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer4);
            this.navBarControl1.Controls.Add(this.navGroupColOther);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer5);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer6);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarBase,
            this.navBarDelegate,
            this.navBarDelegates,
            this.navBarBooking,
            this.navBarOther,
            this.navBarFee,
            this.navOther});
            this.navBarControl1.Location = new System.Drawing.Point(2, 2);
            this.navBarControl1.MaximumSize = new System.Drawing.Size(845, 0);
            this.navBarControl1.MinimumSize = new System.Drawing.Size(845, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 806;
            this.navBarControl1.Size = new System.Drawing.Size(845, 491);
            this.navBarControl1.SkinExplorerBarViewScrollStyle = DevExpress.XtraNavBar.SkinExplorerBarViewScrollStyle.ScrollBar;
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarBase
            // 
            this.navBarBase.Caption = "Base Info";
            this.navBarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarBase.Expanded = true;
            this.navBarBase.GroupClientHeight = 143;
            this.navBarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBase.Name = "navBarBase";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.panel1);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(824, 141);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel1.Controls.Add(this.mcmbSales);
            this.panel1.Controls.Add(this.txtCustomsClearance);
            this.panel1.Controls.Add(this.lblCustomsClearance);
            this.panel1.Controls.Add(this.cmbBookingMode);
            this.panel1.Controls.Add(this.labBookingMode);
            this.panel1.Controls.Add(this.cmbTransportClause);
            this.panel1.Controls.Add(this.labTransportClause);
            this.panel1.Controls.Add(this.trsSalesDep);
            this.panel1.Controls.Add(this.labSales);
            this.panel1.Controls.Add(this.labSalesDepartment);
            this.panel1.Controls.Add(this.stxtPlacePay);
            this.panel1.Controls.Add(this.chkThirdPlacePay);
            this.panel1.Controls.Add(this.labBookinger);
            this.panel1.Controls.Add(this.mcmbOverseasFiler);
            this.panel1.Controls.Add(this.mcmbBookingBy);
            this.panel1.Controls.Add(this.mcmbFiler);
            this.panel1.Controls.Add(this.mcmbBookinger);
            this.panel1.Controls.Add(this.labNo);
            this.panel1.Controls.Add(this.dteBookingDate);
            this.panel1.Controls.Add(this.txtNo);
            this.panel1.Controls.Add(this.labTradeTerm);
            this.panel1.Controls.Add(this.labCustomer);
            this.panel1.Controls.Add(this.labCompany);
            this.panel1.Controls.Add(this.labState);
            this.panel1.Controls.Add(this.labBookingDate);
            this.panel1.Controls.Add(this.cmbCompany);
            this.panel1.Controls.Add(this.labSalesType);
            this.panel1.Controls.Add(this.stxtCustomer);
            this.panel1.Controls.Add(this.cmbTradeTerm);
            this.panel1.Controls.Add(this.labType);
            this.panel1.Controls.Add(this.labBookingBy);
            this.panel1.Controls.Add(this.labFiler);
            this.panel1.Controls.Add(this.labOverseasFiler);
            this.panel1.Controls.Add(this.cmbPaymentTerm);
            this.panel1.Controls.Add(this.labPaymentTerm);
            this.panel1.Controls.Add(this.cmbSalesType);
            this.panel1.Controls.Add(this.cmbType);
            this.panel1.Controls.Add(this.txtState);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 141);
            this.panel1.TabIndex = 390;
            // 
            // mcmbSales
            // 
            this.mcmbSales.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "SalesID", true));
            this.mcmbSales.EditText = "";
            this.mcmbSales.EditValue = null;
            this.mcmbSales.Location = new System.Drawing.Point(269, 83);
            this.mcmbSales.Name = "mcmbSales";
            this.mcmbSales.ReadOnly = false;
            this.mcmbSales.RefreshButtonToolTip = "";
            this.mcmbSales.ShowRefreshButton = false;
            this.mcmbSales.Size = new System.Drawing.Size(125, 21);
            this.mcmbSales.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbSales.TabIndex = 409;
            this.mcmbSales.ToolTip = "";
            // 
            // txtCustomsClearance
            // 
            this.txtCustomsClearance.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "CusClearanceNo", true));
            this.txtCustomsClearance.Location = new System.Drawing.Point(498, 111);
            this.txtCustomsClearance.MenuManager = this.barManager1;
            this.txtCustomsClearance.Name = "txtCustomsClearance";
            this.txtCustomsClearance.Size = new System.Drawing.Size(113, 21);
            this.txtCustomsClearance.TabIndex = 427;
            // 
            // lblCustomsClearance
            // 
            this.lblCustomsClearance.Location = new System.Drawing.Point(415, 118);
            this.lblCustomsClearance.Name = "lblCustomsClearance";
            this.lblCustomsClearance.Size = new System.Drawing.Size(75, 14);
            this.lblCustomsClearance.TabIndex = 426;
            this.lblCustomsClearance.Text = "Cus Clearance";
            // 
            // labBookingMode
            // 
            this.labBookingMode.Location = new System.Drawing.Point(625, 60);
            this.labBookingMode.Name = "labBookingMode";
            this.labBookingMode.Size = new System.Drawing.Size(73, 14);
            this.labBookingMode.TabIndex = 425;
            this.labBookingMode.Text = "BookingMode";
            // 
            // labTransportClause
            // 
            this.labTransportClause.Location = new System.Drawing.Point(412, 59);
            this.labTransportClause.Name = "labTransportClause";
            this.labTransportClause.Size = new System.Drawing.Size(87, 14);
            this.labTransportClause.TabIndex = 424;
            this.labTransportClause.Text = "TransportClause";
            // 
            // trsSalesDep
            // 
            this.trsSalesDep.AllText = "Selecte ALL";
            this.trsSalesDep.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "SalesDepartmentID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trsSalesDep.EditValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.trsSalesDep.Location = new System.Drawing.Point(83, 109);
            this.trsSalesDep.Name = "trsSalesDep";
            this.trsSalesDep.OnlyLeafNodeCanSelect = false;
            this.trsSalesDep.ReadOnly = false;
            this.trsSalesDep.Size = new System.Drawing.Size(117, 21);
            this.trsSalesDep.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.trsSalesDep.TabIndex = 410;
            // 
            // labSales
            // 
            this.labSales.Location = new System.Drawing.Point(217, 90);
            this.labSales.Name = "labSales";
            this.labSales.Size = new System.Drawing.Size(27, 14);
            this.labSales.TabIndex = 423;
            this.labSales.Text = "Sales";
            // 
            // labSalesDepartment
            // 
            this.labSalesDepartment.Location = new System.Drawing.Point(10, 112);
            this.labSalesDepartment.Name = "labSalesDepartment";
            this.labSalesDepartment.Size = new System.Drawing.Size(62, 14);
            this.labSalesDepartment.TabIndex = 395;
            this.labSalesDepartment.Text = "Sales Dept.";
            // 
            // chkThirdPlacePay
            // 
            this.chkThirdPlacePay.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsThirdPlacePay", true));
            this.chkThirdPlacePay.Location = new System.Drawing.Point(610, 112);
            this.chkThirdPlacePay.MenuManager = this.barManager1;
            this.chkThirdPlacePay.Name = "chkThirdPlacePay";
            this.chkThirdPlacePay.Properties.Caption = "Third Pay.";
            this.chkThirdPlacePay.Size = new System.Drawing.Size(92, 19);
            this.chkThirdPlacePay.TabIndex = 422;
            this.chkThirdPlacePay.ToolTip = "Third Payment";
            // 
            // labBookinger
            // 
            this.labBookinger.Location = new System.Drawing.Point(419, 6);
            this.labBookinger.Name = "labBookinger";
            this.labBookinger.Size = new System.Drawing.Size(26, 14);
            this.labBookinger.TabIndex = 421;
            this.labBookinger.Text = "C. S.";
            this.labBookinger.ToolTip = "Customer Service";
            // 
            // mcmbBookingBy
            // 
            this.mcmbBookingBy.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "BookingByID", true));
            this.mcmbBookingBy.Location = new System.Drawing.Point(700, 30);
            this.mcmbBookingBy.Name = "mcmbBookingBy";
            this.mcmbBookingBy.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.mcmbBookingBy.Properties.Appearance.Options.UseBackColor = true;
            this.mcmbBookingBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mcmbBookingBy.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.mcmbBookingBy.Size = new System.Drawing.Size(110, 21);
            this.mcmbBookingBy.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.mcmbBookingBy.TabIndex = 415;
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(12, 3);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(17, 14);
            this.labNo.TabIndex = 397;
            this.labNo.Text = "NO";
            // 
            // labTradeTerm
            // 
            this.labTradeTerm.Location = new System.Drawing.Point(12, 89);
            this.labTradeTerm.Name = "labTradeTerm";
            this.labTradeTerm.Size = new System.Drawing.Size(61, 14);
            this.labTradeTerm.TabIndex = 391;
            this.labTradeTerm.Text = "TradeTerm";
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(12, 59);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 396;
            this.labCustomer.Text = "Customer";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(12, 31);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 402;
            this.labCompany.Text = "Company";
            // 
            // labState
            // 
            this.labState.Location = new System.Drawing.Point(212, 6);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(30, 14);
            this.labState.TabIndex = 398;
            this.labState.Text = "State";
            // 
            // labBookingDate
            // 
            this.labBookingDate.Location = new System.Drawing.Point(622, 87);
            this.labBookingDate.Name = "labBookingDate";
            this.labBookingDate.Size = new System.Drawing.Size(69, 14);
            this.labBookingDate.TabIndex = 399;
            this.labBookingDate.Text = "BookingDate";
            // 
            // labSalesType
            // 
            this.labSalesType.Location = new System.Drawing.Point(212, 113);
            this.labSalesType.Name = "labSalesType";
            this.labSalesType.Size = new System.Drawing.Size(59, 14);
            this.labSalesType.TabIndex = 401;
            this.labSalesType.Text = "Sales Type";
            // 
            // stxtCustomer
            // 
            this.stxtCustomer.CompanyID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.stxtCustomer.ContactStage = ICP.Framework.CommonLibrary.Common.ContactStage.Unknown;
            this.stxtCustomer.ContactType = ICP.Framework.CommonLibrary.Common.ContactType.Customer;
            this.stxtCustomer.CustomerID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.stxtCustomer.CustomerName = null;
            this.stxtCustomer.CustomerType = ICP.Common.ServiceInterface.DataObjects.CustomerType.Unknown;
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "CustomerID", true));
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "CustomerName", true));
            this.stxtCustomer.EditValue = null;
            this.stxtCustomer.Location = new System.Drawing.Point(80, 54);
            this.stxtCustomer.Name = "stxtCustomer";
            this.stxtCustomer.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtCustomer.Size = new System.Drawing.Size(319, 21);
            this.stxtCustomer.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.stxtCustomer.TabIndex = 6;
            this.stxtCustomer.Tag = null;
            this.stxtCustomer.TradeTermID = null;
            this.stxtCustomer.TradeTermName = null;
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(214, 30);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(28, 14);
            this.labType.TabIndex = 392;
            this.labType.Text = "Type";
            // 
            // labBookingBy
            // 
            this.labBookingBy.Location = new System.Drawing.Point(625, 33);
            this.labBookingBy.Name = "labBookingBy";
            this.labBookingBy.Size = new System.Drawing.Size(56, 14);
            this.labBookingBy.TabIndex = 393;
            this.labBookingBy.Text = "Bookingby";
            // 
            // labFiler
            // 
            this.labFiler.Location = new System.Drawing.Point(421, 32);
            this.labFiler.Name = "labFiler";
            this.labFiler.Size = new System.Drawing.Size(21, 14);
            this.labFiler.TabIndex = 394;
            this.labFiler.Text = "Filer";
            // 
            // labOverseasFiler
            // 
            this.labOverseasFiler.Location = new System.Drawing.Point(625, 10);
            this.labOverseasFiler.Name = "labOverseasFiler";
            this.labOverseasFiler.Size = new System.Drawing.Size(42, 14);
            this.labOverseasFiler.TabIndex = 390;
            this.labOverseasFiler.Text = "G. C. S.";
            this.labOverseasFiler.ToolTip = "General Customer Service";
            // 
            // labPaymentTerm
            // 
            this.labPaymentTerm.Location = new System.Drawing.Point(416, 89);
            this.labPaymentTerm.Name = "labPaymentTerm";
            this.labPaymentTerm.Size = new System.Drawing.Size(77, 14);
            this.labPaymentTerm.TabIndex = 400;
            this.labPaymentTerm.Text = "PaymentTerm";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.panel2);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(824, 305);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel2.Controls.Add(this.labPlaceOfDeliveryAddress);
            this.panel2.Controls.Add(this.labPlaceOfReceiptAddress);
            this.panel2.Controls.Add(this.txtPlaceOfDeliveryAddress);
            this.panel2.Controls.Add(this.txtPlaceOfReceiptAddress);
            this.panel2.Controls.Add(this.chkOkToSub);
            this.panel2.Controls.Add(this.labCargoType);
            this.panel2.Controls.Add(this.cmbCargoType);
            this.panel2.Controls.Add(this.lblCommodity);
            this.panel2.Controls.Add(this.labNotifyParty);
            this.panel2.Controls.Add(this.stxtNotifyParty);
            this.panel2.Controls.Add(this.groupLocalService);
            this.panel2.Controls.Add(this.checkEdit1);
            this.panel2.Controls.Add(this.txtCommodity);
            this.panel2.Controls.Add(this.labPOLETD);
            this.panel2.Controls.Add(this.labETD);
            this.panel2.Controls.Add(this.stxtPlaceOfReceipt);
            this.panel2.Controls.Add(this.labETA);
            this.panel2.Controls.Add(this.stxtPOD);
            this.panel2.Controls.Add(this.dtstxtPOD);
            this.panel2.Controls.Add(this.stxtPOL);
            this.panel2.Controls.Add(this.labPOD);
            this.panel2.Controls.Add(this.labPlaceOfReceipt);
            this.panel2.Controls.Add(this.labPlaceOfDelivery);
            this.panel2.Controls.Add(this.labBookingCustomer);
            this.panel2.Controls.Add(this.labFinalDestination);
            this.panel2.Controls.Add(this.labPOL2);
            this.panel2.Controls.Add(this.dtstxtPOL);
            this.panel2.Controls.Add(this.dtstxtPlaceOfReceipt);
            this.panel2.Controls.Add(this.numMeasurement);
            this.panel2.Controls.Add(this.numWeight);
            this.panel2.Controls.Add(this.numQuantity);
            this.panel2.Controls.Add(this.labShipper);
            this.panel2.Controls.Add(this.labConsignee);
            this.panel2.Controls.Add(this.labMarks);
            this.panel2.Controls.Add(this.cmbWeightUnit);
            this.panel2.Controls.Add(this.cmbMeasurementUnit);
            this.panel2.Controls.Add(this.cmbQuantityUnit);
            this.panel2.Controls.Add(this.labMeasurement);
            this.panel2.Controls.Add(this.labWeight);
            this.panel2.Controls.Add(this.labQuantity);
            this.panel2.Controls.Add(this.stxtPlaceOfDelivery);
            this.panel2.Controls.Add(this.stxtFinalDestination);
            this.panel2.Controls.Add(this.containerDemandControl1);
            this.panel2.Controls.Add(this.stxtShipper);
            this.panel2.Controls.Add(this.stxtBookingCustomer);
            this.panel2.Controls.Add(this.stxtConsignee);
            this.panel2.Controls.Add(this.txtMarks);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(824, 305);
            this.panel2.TabIndex = 0;
            // 
            // labPlaceOfDeliveryAddress
            // 
            this.labPlaceOfDeliveryAddress.Location = new System.Drawing.Point(9, 232);
            this.labPlaceOfDeliveryAddress.Name = "labPlaceOfDeliveryAddress";
            this.labPlaceOfDeliveryAddress.Size = new System.Drawing.Size(43, 14);
            this.labPlaceOfDeliveryAddress.TabIndex = 820;
            this.labPlaceOfDeliveryAddress.Text = "Address";
            // 
            // labPlaceOfReceiptAddress
            // 
            this.labPlaceOfReceiptAddress.Location = new System.Drawing.Point(9, 135);
            this.labPlaceOfReceiptAddress.Name = "labPlaceOfReceiptAddress";
            this.labPlaceOfReceiptAddress.Size = new System.Drawing.Size(43, 14);
            this.labPlaceOfReceiptAddress.TabIndex = 820;
            this.labPlaceOfReceiptAddress.Text = "Address";
            // 
            // txtPlaceOfDeliveryAddress
            // 
            this.txtPlaceOfDeliveryAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "PlaceOfDeliveryAddress", true));
            this.txtPlaceOfDeliveryAddress.Location = new System.Drawing.Point(93, 229);
            this.txtPlaceOfDeliveryAddress.Name = "txtPlaceOfDeliveryAddress";
            this.txtPlaceOfDeliveryAddress.Size = new System.Drawing.Size(318, 21);
            this.txtPlaceOfDeliveryAddress.TabIndex = 819;
            // 
            // txtPlaceOfReceiptAddress
            // 
            this.txtPlaceOfReceiptAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "PlaceOfReceiptAddress", true));
            this.txtPlaceOfReceiptAddress.Location = new System.Drawing.Point(93, 132);
            this.txtPlaceOfReceiptAddress.MenuManager = this.barManager1;
            this.txtPlaceOfReceiptAddress.Name = "txtPlaceOfReceiptAddress";
            this.txtPlaceOfReceiptAddress.Size = new System.Drawing.Size(318, 21);
            this.txtPlaceOfReceiptAddress.TabIndex = 819;
            // 
            // chkOkToSub
            // 
            this.chkOkToSub.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "OkToSub", true));
            this.chkOkToSub.Location = new System.Drawing.Point(747, 203);
            this.chkOkToSub.MenuManager = this.barManager1;
            this.chkOkToSub.Name = "chkOkToSub";
            this.chkOkToSub.Properties.Caption = "OkToSub";
            this.chkOkToSub.Size = new System.Drawing.Size(77, 19);
            this.chkOkToSub.TabIndex = 798;
            // 
            // labCargoType
            // 
            this.labCargoType.Location = new System.Drawing.Point(636, 65);
            this.labCargoType.Name = "labCargoType";
            this.labCargoType.Size = new System.Drawing.Size(59, 14);
            this.labCargoType.TabIndex = 796;
            this.labCargoType.Text = "CargoType";
            // 
            // lblCommodity
            // 
            this.lblCommodity.Location = new System.Drawing.Point(636, 92);
            this.lblCommodity.Name = "lblCommodity";
            this.lblCommodity.Size = new System.Drawing.Size(61, 14);
            this.lblCommodity.TabIndex = 795;
            this.lblCommodity.Text = "Commodity";
            // 
            // labNotifyParty
            // 
            this.labNotifyParty.Location = new System.Drawing.Point(9, 86);
            this.labNotifyParty.Name = "labNotifyParty";
            this.labNotifyParty.Size = new System.Drawing.Size(64, 14);
            this.labNotifyParty.TabIndex = 793;
            this.labNotifyParty.Text = "Notify Party";
            // 
            // groupLocalService
            // 
            this.groupLocalService.Controls.Add(this.chkIsWarehouse);
            this.groupLocalService.Controls.Add(this.chkIsFumigate);
            this.groupLocalService.Controls.Add(this.chkIsInsurance);
            this.groupLocalService.Controls.Add(this.chkIsCustoms);
            this.groupLocalService.Controls.Add(this.chkIsTruck);
            this.groupLocalService.Font = new System.Drawing.Font("Tahoma", 8F);
            this.groupLocalService.Location = new System.Drawing.Point(430, 15);
            this.groupLocalService.Name = "groupLocalService";
            this.groupLocalService.Size = new System.Drawing.Size(394, 42);
            this.groupLocalService.TabIndex = 792;
            this.groupLocalService.TabStop = false;
            this.groupLocalService.Text = "LocalService";
            // 
            // checkEdit1
            // 
            this.checkEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsWoodPacking", true));
            this.checkEdit1.Location = new System.Drawing.Point(703, 89);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Wood Packing";
            this.checkEdit1.Size = new System.Drawing.Size(121, 19);
            this.checkEdit1.TabIndex = 44;
            // 
            // labPOLETD
            // 
            this.labPOLETD.Location = new System.Drawing.Point(264, 159);
            this.labPOLETD.Name = "labPOLETD";
            this.labPOLETD.Size = new System.Drawing.Size(23, 14);
            this.labPOLETD.TabIndex = 0;
            this.labPOLETD.Text = "ETD";
            // 
            // labETD
            // 
            this.labETD.Location = new System.Drawing.Point(264, 110);
            this.labETD.Name = "labETD";
            this.labETD.Size = new System.Drawing.Size(23, 14);
            this.labETD.TabIndex = 0;
            this.labETD.Text = "ETD";
            // 
            // labETA
            // 
            this.labETA.Location = new System.Drawing.Point(264, 183);
            this.labETA.Name = "labETA";
            this.labETA.Size = new System.Drawing.Size(23, 14);
            this.labETA.TabIndex = 0;
            this.labETA.Text = "ETA";
            // 
            // dtstxtPOD
            // 
            this.dtstxtPOD.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ETA", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtstxtPOD.EditValue = null;
            this.dtstxtPOD.Location = new System.Drawing.Point(310, 179);
            this.dtstxtPOD.Name = "dtstxtPOD";
            this.dtstxtPOD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtstxtPOD.Properties.Mask.EditMask = "";
            this.dtstxtPOD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtstxtPOD.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtstxtPOD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtstxtPOD.Size = new System.Drawing.Size(103, 21);
            this.dtstxtPOD.TabIndex = 28;
            this.dtstxtPOD.TabStop = false;
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(9, 184);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(24, 14);
            this.labPOD.TabIndex = 0;
            this.labPOD.Text = "POD";
            // 
            // labPlaceOfReceipt
            // 
            this.labPlaceOfReceipt.Location = new System.Drawing.Point(9, 110);
            this.labPlaceOfReceipt.Name = "labPlaceOfReceipt";
            this.labPlaceOfReceipt.Size = new System.Drawing.Size(31, 14);
            this.labPlaceOfReceipt.TabIndex = 46;
            this.labPlaceOfReceipt.Text = "P.O.R";
            // 
            // labPlaceOfDelivery
            // 
            this.labPlaceOfDelivery.Location = new System.Drawing.Point(9, 208);
            this.labPlaceOfDelivery.Name = "labPlaceOfDelivery";
            this.labPlaceOfDelivery.Size = new System.Drawing.Size(83, 14);
            this.labPlaceOfDelivery.TabIndex = 5;
            this.labPlaceOfDelivery.Text = "PlaceOfDelivery";
            // 
            // labBookingCustomer
            // 
            this.labBookingCustomer.Location = new System.Drawing.Point(9, 15);
            this.labBookingCustomer.Name = "labBookingCustomer";
            this.labBookingCustomer.Size = new System.Drawing.Size(95, 14);
            this.labBookingCustomer.TabIndex = 0;
            this.labBookingCustomer.Text = "BookingCustomer";
            // 
            // labFinalDestination
            // 
            this.labFinalDestination.Location = new System.Drawing.Point(9, 257);
            this.labFinalDestination.Name = "labFinalDestination";
            this.labFinalDestination.Size = new System.Drawing.Size(84, 14);
            this.labFinalDestination.TabIndex = 5;
            this.labFinalDestination.Text = "FinalDestination";
            // 
            // labPOL2
            // 
            this.labPOL2.Location = new System.Drawing.Point(9, 160);
            this.labPOL2.Name = "labPOL2";
            this.labPOL2.Size = new System.Drawing.Size(22, 14);
            this.labPOL2.TabIndex = 0;
            this.labPOL2.Text = "POL";
            // 
            // dtstxtPOL
            // 
            this.dtstxtPOL.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ETD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtstxtPOL.EditValue = null;
            this.dtstxtPOL.Location = new System.Drawing.Point(310, 156);
            this.dtstxtPOL.Name = "dtstxtPOL";
            this.dtstxtPOL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtstxtPOL.Properties.Mask.EditMask = "";
            this.dtstxtPOL.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtstxtPOL.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtstxtPOL.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtstxtPOL.Size = new System.Drawing.Size(103, 21);
            this.dtstxtPOL.TabIndex = 26;
            this.dtstxtPOL.TabStop = false;
            // 
            // dtstxtPlaceOfReceipt
            // 
            this.dtstxtPlaceOfReceipt.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "PORETD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtstxtPlaceOfReceipt.EditValue = null;
            this.dtstxtPlaceOfReceipt.Location = new System.Drawing.Point(310, 107);
            this.dtstxtPlaceOfReceipt.Name = "dtstxtPlaceOfReceipt";
            this.dtstxtPlaceOfReceipt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtstxtPlaceOfReceipt.Properties.Mask.EditMask = "";
            this.dtstxtPlaceOfReceipt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtstxtPlaceOfReceipt.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtstxtPlaceOfReceipt.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtstxtPlaceOfReceipt.Size = new System.Drawing.Size(103, 21);
            this.dtstxtPlaceOfReceipt.TabIndex = 24;
            this.dtstxtPlaceOfReceipt.TabStop = false;
            // 
            // labShipper
            // 
            this.labShipper.Location = new System.Drawing.Point(9, 39);
            this.labShipper.Name = "labShipper";
            this.labShipper.Size = new System.Drawing.Size(41, 14);
            this.labShipper.TabIndex = 3;
            this.labShipper.Text = "Shipper";
            // 
            // labConsignee
            // 
            this.labConsignee.Location = new System.Drawing.Point(9, 63);
            this.labConsignee.Name = "labConsignee";
            this.labConsignee.Size = new System.Drawing.Size(56, 14);
            this.labConsignee.TabIndex = 3;
            this.labConsignee.Text = "Consignee";
            // 
            // labMarks
            // 
            this.labMarks.Location = new System.Drawing.Point(430, 141);
            this.labMarks.Name = "labMarks";
            this.labMarks.Size = new System.Drawing.Size(30, 14);
            this.labMarks.TabIndex = 3;
            this.labMarks.Text = "Marks";
            // 
            // labMeasurement
            // 
            this.labMeasurement.Location = new System.Drawing.Point(429, 114);
            this.labMeasurement.Name = "labMeasurement";
            this.labMeasurement.Size = new System.Drawing.Size(74, 14);
            this.labMeasurement.TabIndex = 30;
            this.labMeasurement.Text = "Measurement";
            // 
            // labWeight
            // 
            this.labWeight.Location = new System.Drawing.Point(430, 87);
            this.labWeight.Name = "labWeight";
            this.labWeight.Size = new System.Drawing.Size(40, 14);
            this.labWeight.TabIndex = 29;
            this.labWeight.Text = "Weight";
            // 
            // labQuantity
            // 
            this.labQuantity.Location = new System.Drawing.Point(430, 63);
            this.labQuantity.Name = "labQuantity";
            this.labQuantity.Size = new System.Drawing.Size(47, 14);
            this.labQuantity.TabIndex = 31;
            this.labQuantity.Text = "Quantity";
            // 
            // stxtFinalDestination
            // 
            this.stxtFinalDestination.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "FinalDestinationID", true));
            this.stxtFinalDestination.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "FinalDestinationName", true));
            this.stxtFinalDestination.EditValue = "";
            this.stxtFinalDestination.Location = new System.Drawing.Point(93, 254);
            this.stxtFinalDestination.Name = "stxtFinalDestination";
            this.stxtFinalDestination.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtFinalDestination.Properties.Appearance.Options.UseBackColor = true;
            this.stxtFinalDestination.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtFinalDestination.Size = new System.Drawing.Size(318, 21);
            this.stxtFinalDestination.TabIndex = 30;
            // 
            // containerDemandControl1
            // 
            this.containerDemandControl1.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.containerDemandControl1.Appearance.Options.UseBackColor = true;
            this.containerDemandControl1.Location = new System.Drawing.Point(427, 201);
            this.containerDemandControl1.Name = "containerDemandControl1";
            this.containerDemandControl1.Size = new System.Drawing.Size(318, 21);
            this.containerDemandControl1.SpecifiedBackColor = System.Drawing.Color.White;
            this.containerDemandControl1.TabIndex = 46;
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.panel3);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(824, 156);
            this.navBarGroupControlContainer3.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel3.Controls.Add(this.txtHSCode);
            this.panel3.Controls.Add(this.labHSCode);
            this.panel3.Controls.Add(this.lblRemark);
            this.panel3.Controls.Add(this.txtRemark);
            this.panel3.Controls.Add(this.labMBLRemark);
            this.panel3.Controls.Add(this.txtMBLRequirements);
            this.panel3.Controls.Add(this.groupHBL);
            this.panel3.Controls.Add(this.dteEstimatedDeliveryDate);
            this.panel3.Controls.Add(this.dteDeliveryDate);
            this.panel3.Controls.Add(this.dteExpectedShipDate);
            this.panel3.Controls.Add(this.dteExpectedArriveDate);
            this.panel3.Controls.Add(this.labExpectedShipDate);
            this.panel3.Controls.Add(this.labEstimatedDeliveryDate);
            this.panel3.Controls.Add(this.labExpectedArriveDate);
            this.panel3.Controls.Add(this.labDeliveryDate);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(824, 156);
            this.panel3.TabIndex = 0;
            // 
            // txtHSCode
            // 
            this.txtHSCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "HSCode", true));
            this.txtHSCode.Location = new System.Drawing.Point(88, 117);
            this.txtHSCode.MenuManager = this.barManager1;
            this.txtHSCode.Name = "txtHSCode";
            this.txtHSCode.Size = new System.Drawing.Size(111, 21);
            this.txtHSCode.TabIndex = 807;
            // 
            // labHSCode
            // 
            this.labHSCode.Location = new System.Drawing.Point(5, 119);
            this.labHSCode.Name = "labHSCode";
            this.labHSCode.Size = new System.Drawing.Size(43, 14);
            this.labHSCode.TabIndex = 806;
            this.labHSCode.Text = "HSCode";
            // 
            // lblRemark
            // 
            this.lblRemark.Location = new System.Drawing.Point(623, 10);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(40, 14);
            this.lblRemark.TabIndex = 803;
            this.lblRemark.Text = "Remark";
            // 
            // labMBLRemark
            // 
            this.labMBLRemark.Location = new System.Drawing.Point(419, 6);
            this.labMBLRemark.Name = "labMBLRemark";
            this.labMBLRemark.Size = new System.Drawing.Size(66, 14);
            this.labMBLRemark.TabIndex = 801;
            this.labMBLRemark.Text = "MBL Remark";
            // 
            // groupHBL
            // 
            this.groupHBL.Controls.Add(this.txtHBLRequirements);
            this.groupHBL.Controls.Add(this.cmbHBLPaymentTerm);
            this.groupHBL.Controls.Add(this.cmbHBLReleaseType);
            this.groupHBL.Controls.Add(this.labHBLPaymentTerm);
            this.groupHBL.Controls.Add(this.labHBLReleaseType);
            this.groupHBL.Location = new System.Drawing.Point(215, 6);
            this.groupHBL.Name = "groupHBL";
            this.groupHBL.Size = new System.Drawing.Size(194, 141);
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
            // labExpectedShipDate
            // 
            this.labExpectedShipDate.Location = new System.Drawing.Point(5, 66);
            this.labExpectedShipDate.Name = "labExpectedShipDate";
            this.labExpectedShipDate.Size = new System.Drawing.Size(47, 14);
            this.labExpectedShipDate.TabIndex = 33;
            this.labExpectedShipDate.Text = "Exp.Ship";
            // 
            // labEstimatedDeliveryDate
            // 
            this.labEstimatedDeliveryDate.Location = new System.Drawing.Point(5, 15);
            this.labEstimatedDeliveryDate.Name = "labEstimatedDeliveryDate";
            this.labEstimatedDeliveryDate.Size = new System.Drawing.Size(63, 14);
            this.labEstimatedDeliveryDate.TabIndex = 32;
            this.labEstimatedDeliveryDate.Text = "Est.Delivery";
            // 
            // labExpectedArriveDate
            // 
            this.labExpectedArriveDate.Location = new System.Drawing.Point(5, 93);
            this.labExpectedArriveDate.Name = "labExpectedArriveDate";
            this.labExpectedArriveDate.Size = new System.Drawing.Size(55, 14);
            this.labExpectedArriveDate.TabIndex = 31;
            this.labExpectedArriveDate.Text = "Exp.Arrive";
            // 
            // labDeliveryDate
            // 
            this.labDeliveryDate.Location = new System.Drawing.Point(5, 40);
            this.labDeliveryDate.Name = "labDeliveryDate";
            this.labDeliveryDate.Size = new System.Drawing.Size(68, 14);
            this.labDeliveryDate.TabIndex = 32;
            this.labDeliveryDate.Text = "DeliveryDate";
            // 
            // navBarGroupControlContainer4
            // 
            this.navBarGroupControlContainer4.Controls.Add(this.panel5);
            this.navBarGroupControlContainer4.Name = "navBarGroupControlContainer4";
            this.navBarGroupControlContainer4.Size = new System.Drawing.Size(824, 328);
            this.navBarGroupControlContainer4.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel5.Controls.Add(this.orderFeeEditPart1);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(828, 328);
            this.panel5.TabIndex = 1;
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
            this.orderFeeEditPart1.Size = new System.Drawing.Size(828, 328);
            this.orderFeeEditPart1.TabIndex = 1;
            this.orderFeeEditPart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("orderFeeEditPart1.UsedMessages")));
            this.orderFeeEditPart1.Workitem = null;
            // 
            // navGroupColOther
            // 
            this.navGroupColOther.Name = "navGroupColOther";
            this.navGroupColOther.Size = new System.Drawing.Size(824, 398);
            this.navGroupColOther.TabIndex = 4;
            // 
            // navBarGroupControlContainer5
            // 
            this.navBarGroupControlContainer5.Controls.Add(this.panel4);
            this.navBarGroupControlContainer5.Name = "navBarGroupControlContainer5";
            this.navBarGroupControlContainer5.Size = new System.Drawing.Size(824, 384);
            this.navBarGroupControlContainer5.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel4.Controls.Add(this.btnAdd);
            this.panel4.Controls.Add(this.labContranct);
            this.panel4.Controls.Add(this.dteVGMCutOff);
            this.panel4.Controls.Add(this.labVGMCutOff);
            this.panel4.Controls.Add(this.labGateInDate);
            this.panel4.Controls.Add(this.dteGateInDate);
            this.panel4.Controls.Add(this.stxtRecentQuotedPrice);
            this.panel4.Controls.Add(this.ucInquirePrice);
            this.panel4.Controls.Add(this.lblRefNo);
            this.panel4.Controls.Add(this.daterailcutoff);
            this.panel4.Controls.Add(this.lblrailcutoff);
            this.panel4.Controls.Add(this.txtBookingRefNo);
            this.panel4.Controls.Add(this.txtSCAC);
            this.panel4.Controls.Add(this.lblScac);
            this.panel4.Controls.Add(this.mcmbBookingParty);
            this.panel4.Controls.Add(this.memoEdit1);
            this.panel4.Controls.Add(this.stxtPlacePayOrder);
            this.panel4.Controls.Add(this.chkOrderThirdPay);
            this.panel4.Controls.Add(this.labBookingParty);
            this.panel4.Controls.Add(this.labelControl12);
            this.panel4.Controls.Add(this.labelControl11);
            this.panel4.Controls.Add(this.labVoyageETA);
            this.panel4.Controls.Add(this.memoEdit3);
            this.panel4.Controls.Add(this.dateEdit3);
            this.panel4.Controls.Add(this.dateEdit2);
            this.panel4.Controls.Add(this.dateEdit4);
            this.panel4.Controls.Add(this.checkEdit2);
            this.panel4.Controls.Add(this.labQuotedPriceNo);
            this.panel4.Controls.Add(this.labAMSClosing);
            this.panel4.Controls.Add(this.dteAMSClosing);
            this.panel4.Controls.Add(this.cmbMblTransportClause);
            this.panel4.Controls.Add(this.labShippingTransportClause);
            this.panel4.Controls.Add(this.cmbMBLPaymentTerm);
            this.panel4.Controls.Add(this.cmbMBLReleaseType);
            this.panel4.Controls.Add(this.labMBLPaymentTerm);
            this.panel4.Controls.Add(this.labBookingExplanation);
            this.panel4.Controls.Add(this.labMBLReleaseType);
            this.panel4.Controls.Add(this.labPickupRequirement);
            this.panel4.Controls.Add(this.NotifyParty);
            this.panel4.Controls.Add(this.stxtBookingNotifyParty);
            this.panel4.Controls.Add(this.labShippingShipper);
            this.panel4.Controls.Add(this.labShippingConsignee);
            this.panel4.Controls.Add(this.stxtBookingShipper);
            this.panel4.Controls.Add(this.stxtBookingConsignee);
            this.panel4.Controls.Add(this.labCYClosingDate);
            this.panel4.Controls.Add(this.cargoDescriptionPart1);
            this.panel4.Controls.Add(this.stxtVoyage);
            this.panel4.Controls.Add(this.stxtPreVoyage);
            this.panel4.Controls.Add(this.labPreVoyage);
            this.panel4.Controls.Add(this.chkIsOnlyMBL);
            this.panel4.Controls.Add(this.labVoyage);
            this.panel4.Controls.Add(this.labPickUpDate);
            this.panel4.Controls.Add(this.labWarehouse);
            this.panel4.Controls.Add(this.labReturnLocation);
            this.panel4.Controls.Add(this.txtContractNo);
            this.panel4.Controls.Add(this.cmbShippingLine);
            this.panel4.Controls.Add(this.chkHasContract);
            this.panel4.Controls.Add(this.labShippingLine);
            this.panel4.Controls.Add(this.dtePickUpDate);
            this.panel4.Controls.Add(this.stxtWarehouse);
            this.panel4.Controls.Add(this.stxtReturnLocation);
            this.panel4.Controls.Add(this.dteCYClosingDate);
            this.panel4.Controls.Add(this.labCloseWarehouse);
            this.panel4.Controls.Add(this.labDOCClosingDate);
            this.panel4.Controls.Add(this.labClosingDate);
            this.panel4.Controls.Add(this.dteWarehouseClosing);
            this.panel4.Controls.Add(this.dteDOCClosingDate);
            this.panel4.Controls.Add(this.dteClosingDate);
            this.panel4.Controls.Add(this.mcmbCarrier);
            this.panel4.Controls.Add(this.stxtAgent);
            this.panel4.Controls.Add(this.labAgent);
            this.panel4.Controls.Add(this.dteSODate);
            this.panel4.Controls.Add(this.labOrderNo);
            this.panel4.Controls.Add(this.labSODate);
            this.panel4.Controls.Add(this.labAgentOfCarrier);
            this.panel4.Controls.Add(this.labCarrier);
            this.panel4.Controls.Add(this.stxtAgentOfCarrier);
            this.panel4.Controls.Add(this.cmbOrderNo);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(824, 384);
            this.panel4.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(228, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 21);
            this.btnAdd.TabIndex = 816;
            this.btnAdd.Text = "+";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labContranct
            // 
            this.labContranct.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labContranct.Location = new System.Drawing.Point(639, 32);
            this.labContranct.Name = "labContranct";
            this.labContranct.Size = new System.Drawing.Size(176, 0);
            this.labContranct.TabIndex = 815;
            // 
            // dteVGMCutOff
            // 
            this.dteVGMCutOff.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "VGMCutOffDate", true));
            this.dteVGMCutOff.EditValue = null;
            this.dteVGMCutOff.Location = new System.Drawing.Point(504, 253);
            this.dteVGMCutOff.Name = "dteVGMCutOff";
            this.dteVGMCutOff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteVGMCutOff.Properties.Mask.EditMask = "";
            this.dteVGMCutOff.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteVGMCutOff.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteVGMCutOff.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteVGMCutOff.Size = new System.Drawing.Size(122, 21);
            this.dteVGMCutOff.TabIndex = 814;
            this.dteVGMCutOff.TabStop = false;
            // 
            // labVGMCutOff
            // 
            this.labVGMCutOff.Location = new System.Drawing.Point(417, 256);
            this.labVGMCutOff.Name = "labVGMCutOff";
            this.labVGMCutOff.Size = new System.Drawing.Size(61, 14);
            this.labVGMCutOff.TabIndex = 813;
            this.labVGMCutOff.Text = "VGMCutOff";
            // 
            // labGateInDate
            // 
            this.labGateInDate.Location = new System.Drawing.Point(417, 279);
            this.labGateInDate.Name = "labGateInDate";
            this.labGateInDate.Size = new System.Drawing.Size(63, 14);
            this.labGateInDate.TabIndex = 812;
            this.labGateInDate.Text = "GateInDate";
            // 
            // dteGateInDate
            // 
            this.dteGateInDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "GateInDate", true));
            this.dteGateInDate.EditValue = null;
            this.dteGateInDate.Location = new System.Drawing.Point(504, 276);
            this.dteGateInDate.Name = "dteGateInDate";
            this.dteGateInDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteGateInDate.Properties.Mask.EditMask = "";
            this.dteGateInDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteGateInDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteGateInDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteGateInDate.Size = new System.Drawing.Size(122, 21);
            this.dteGateInDate.TabIndex = 811;
            this.dteGateInDate.TabStop = false;
            // 
            // stxtRecentQuotedPrice
            // 
            this.stxtRecentQuotedPrice.CustomerID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.stxtRecentQuotedPrice.CustomerName = null;
            this.stxtRecentQuotedPrice.Location = new System.Drawing.Point(504, 159);
            this.stxtRecentQuotedPrice.Name = "stxtRecentQuotedPrice";
            this.stxtRecentQuotedPrice.QuotedPriceID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.stxtRecentQuotedPrice.QuotedPriceNo = null;
            this.stxtRecentQuotedPrice.RootWorkItem = null;
            this.stxtRecentQuotedPrice.SalesID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.stxtRecentQuotedPrice.Size = new System.Drawing.Size(311, 21);
            this.stxtRecentQuotedPrice.TabIndex = 428;
            // 
            // ucInquirePrice
            // 
            this.ucInquirePrice.BaseMultiLanguageList = null;
            this.ucInquirePrice.BasePartList = null;
            this.ucInquirePrice.CodeValuePairs = null;
            this.ucInquirePrice.ControlsList = null;
            this.ucInquirePrice.DataSource = null;
            this.ucInquirePrice.EditMode = ICP.Framework.CommonLibrary.Common.EditMode.New;
            this.ucInquirePrice.FormName = "UCInquirePrice";
            this.ucInquirePrice.IsMultiLanguage = true;
            this.ucInquirePrice.Location = new System.Drawing.Point(427, 55);
            this.ucInquirePrice.Name = "ucInquirePrice";
            this.ucInquirePrice.OceanBookingInfo = null;
            this.ucInquirePrice.Resources = null;
            this.ucInquirePrice.Size = new System.Drawing.Size(388, 98);
            this.ucInquirePrice.SyncLocalData = false;
            this.ucInquirePrice.TabIndex = 810;
            this.ucInquirePrice.Title = "";
            this.ucInquirePrice.UsedMessages = null;
            this.ucInquirePrice.Workitem = null;
            // 
            // lblRefNo
            // 
            this.lblRefNo.Location = new System.Drawing.Point(258, 8);
            this.lblRefNo.Name = "lblRefNo";
            this.lblRefNo.Size = new System.Drawing.Size(33, 14);
            this.lblRefNo.TabIndex = 809;
            this.lblRefNo.Text = "RefNo";
            // 
            // daterailcutoff
            // 
            this.daterailcutoff.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "RailCutOff", true));
            this.daterailcutoff.EditValue = null;
            this.daterailcutoff.Location = new System.Drawing.Point(504, 299);
            this.daterailcutoff.Name = "daterailcutoff";
            this.daterailcutoff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.daterailcutoff.Properties.Mask.EditMask = "";
            this.daterailcutoff.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.daterailcutoff.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.daterailcutoff.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.daterailcutoff.Size = new System.Drawing.Size(122, 21);
            this.daterailcutoff.TabIndex = 808;
            this.daterailcutoff.TabStop = false;
            // 
            // lblrailcutoff
            // 
            this.lblrailcutoff.Location = new System.Drawing.Point(417, 302);
            this.lblrailcutoff.Name = "lblrailcutoff";
            this.lblrailcutoff.Size = new System.Drawing.Size(73, 14);
            this.lblrailcutoff.TabIndex = 807;
            this.lblrailcutoff.Text = "RAIL CUTOFF";
            // 
            // txtBookingRefNo
            // 
            this.txtBookingRefNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "BookingRefNo", true));
            this.txtBookingRefNo.Location = new System.Drawing.Point(294, 3);
            this.txtBookingRefNo.MenuManager = this.barManager1;
            this.txtBookingRefNo.Name = "txtBookingRefNo";
            this.txtBookingRefNo.Size = new System.Drawing.Size(112, 21);
            this.txtBookingRefNo.TabIndex = 9;
            // 
            // txtSCAC
            // 
            this.txtSCAC.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "ScacCode", true));
            this.txtSCAC.Location = new System.Drawing.Point(294, 25);
            this.txtSCAC.MenuManager = this.barManager1;
            this.txtSCAC.Name = "txtSCAC";
            this.txtSCAC.Size = new System.Drawing.Size(111, 21);
            this.txtSCAC.TabIndex = 805;
            // 
            // lblScac
            // 
            this.lblScac.Location = new System.Drawing.Point(260, 28);
            this.lblScac.Name = "lblScac";
            this.lblScac.Size = new System.Drawing.Size(29, 14);
            this.lblScac.TabIndex = 804;
            this.lblScac.Text = "SCAC";
            // 
            // mcmbBookingParty
            // 
            this.mcmbBookingParty.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "BookingPartyID", true));
            this.mcmbBookingParty.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsBookingInfo, "BookingPartyName", true));
            this.mcmbBookingParty.EditText = "";
            this.mcmbBookingParty.EditValue = null;
            this.mcmbBookingParty.Location = new System.Drawing.Point(88, 51);
            this.mcmbBookingParty.Name = "mcmbBookingParty";
            this.mcmbBookingParty.ReadOnly = false;
            this.mcmbBookingParty.RefreshButtonToolTip = "";
            this.mcmbBookingParty.ShowRefreshButton = false;
            this.mcmbBookingParty.Size = new System.Drawing.Size(318, 21);
            this.mcmbBookingParty.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbBookingParty.TabIndex = 303;
            this.mcmbBookingParty.ToolTip = "";
            // 
            // chkOrderThirdPay
            // 
            this.chkOrderThirdPay.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsThirdPlacePayOrder", true));
            this.chkOrderThirdPay.Location = new System.Drawing.Point(5, 269);
            this.chkOrderThirdPay.MenuManager = this.barManager1;
            this.chkOrderThirdPay.Name = "chkOrderThirdPay";
            this.chkOrderThirdPay.Properties.Caption = "Third Pay.";
            this.chkOrderThirdPay.Size = new System.Drawing.Size(92, 19);
            this.chkOrderThirdPay.TabIndex = 388;
            this.chkOrderThirdPay.ToolTip = "Third Payment";
            this.chkOrderThirdPay.CheckedChanged += new System.EventHandler(this.chkOrderThirdPay_CheckedChanged);
            // 
            // labBookingParty
            // 
            this.labBookingParty.Location = new System.Drawing.Point(5, 49);
            this.labBookingParty.Name = "labBookingParty";
            this.labBookingParty.Size = new System.Drawing.Size(71, 14);
            this.labBookingParty.TabIndex = 803;
            this.labBookingParty.Text = "BookingParty";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(417, 234);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(23, 14);
            this.labelControl12.TabIndex = 796;
            this.labelControl12.Text = "ETD";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(679, 188);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(23, 14);
            this.labelControl11.TabIndex = 795;
            this.labelControl11.Text = "ETD";
            // 
            // labVoyageETA
            // 
            this.labVoyageETA.Location = new System.Drawing.Point(631, 256);
            this.labVoyageETA.Name = "labVoyageETA";
            this.labVoyageETA.Size = new System.Drawing.Size(23, 14);
            this.labVoyageETA.TabIndex = 795;
            this.labVoyageETA.Text = "ETA";
            // 
            // dateEdit3
            // 
            this.dateEdit3.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ETA", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dateEdit3.EditValue = null;
            this.dateEdit3.Location = new System.Drawing.Point(707, 253);
            this.dateEdit3.Name = "dateEdit3";
            this.dateEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit3.Properties.Mask.EditMask = "";
            this.dateEdit3.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateEdit3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateEdit3.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit3.Size = new System.Drawing.Size(111, 21);
            this.dateEdit3.TabIndex = 324;
            this.dateEdit3.TabStop = false;
            // 
            // dateEdit2
            // 
            this.dateEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "PORETD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dateEdit2.EditValue = null;
            this.dateEdit2.Location = new System.Drawing.Point(713, 185);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.Mask.EditMask = "";
            this.dateEdit2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateEdit2.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit2.Size = new System.Drawing.Size(104, 21);
            this.dateEdit2.TabIndex = 320;
            this.dateEdit2.TabStop = false;
            // 
            // dateEdit4
            // 
            this.dateEdit4.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ETD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dateEdit4.EditValue = null;
            this.dateEdit4.Location = new System.Drawing.Point(504, 231);
            this.dateEdit4.Name = "dateEdit4";
            this.dateEdit4.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit4.Properties.Mask.EditMask = "";
            this.dateEdit4.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateEdit4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateEdit4.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit4.Size = new System.Drawing.Size(122, 21);
            this.dateEdit4.TabIndex = 323;
            this.dateEdit4.TabStop = false;
            // 
            // checkEdit2
            // 
            this.checkEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsCarrierSendAMS", true));
            this.checkEdit2.Location = new System.Drawing.Point(711, 6);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "CarrierSendAMS";
            this.checkEdit2.Size = new System.Drawing.Size(109, 19);
            this.checkEdit2.TabIndex = 318;
            // 
            // labQuotedPriceNo
            // 
            this.labQuotedPriceNo.Location = new System.Drawing.Point(417, 160);
            this.labQuotedPriceNo.Name = "labQuotedPriceNo";
            this.labQuotedPriceNo.Size = new System.Drawing.Size(72, 14);
            this.labQuotedPriceNo.TabIndex = 396;
            this.labQuotedPriceNo.Text = "Quoted Price";
            // 
            // labAMSClosing
            // 
            this.labAMSClosing.Location = new System.Drawing.Point(631, 325);
            this.labAMSClosing.Name = "labAMSClosing";
            this.labAMSClosing.Size = new System.Drawing.Size(49, 14);
            this.labAMSClosing.TabIndex = 793;
            this.labAMSClosing.Text = "AMSClos.";
            // 
            // dteAMSClosing
            // 
            this.dteAMSClosing.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "AMSClosingDate", true));
            this.dteAMSClosing.EditValue = null;
            this.dteAMSClosing.Location = new System.Drawing.Point(707, 322);
            this.dteAMSClosing.Name = "dteAMSClosing";
            this.dteAMSClosing.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteAMSClosing.Properties.Mask.EditMask = "";
            this.dteAMSClosing.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteAMSClosing.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteAMSClosing.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteAMSClosing.Size = new System.Drawing.Size(111, 21);
            this.dteAMSClosing.TabIndex = 328;
            // 
            // labShippingTransportClause
            // 
            this.labShippingTransportClause.Location = new System.Drawing.Point(5, 199);
            this.labShippingTransportClause.Name = "labShippingTransportClause";
            this.labShippingTransportClause.Size = new System.Drawing.Size(87, 14);
            this.labShippingTransportClause.TabIndex = 791;
            this.labShippingTransportClause.Text = "TransportClause";
            // 
            // labMBLPaymentTerm
            // 
            this.labMBLPaymentTerm.Location = new System.Drawing.Point(5, 247);
            this.labMBLPaymentTerm.Name = "labMBLPaymentTerm";
            this.labMBLPaymentTerm.Size = new System.Drawing.Size(77, 14);
            this.labMBLPaymentTerm.TabIndex = 0;
            this.labMBLPaymentTerm.Text = "PaymentTerm";
            // 
            // labBookingExplanation
            // 
            this.labBookingExplanation.Location = new System.Drawing.Point(195, 202);
            this.labBookingExplanation.Name = "labBookingExplanation";
            this.labBookingExplanation.Size = new System.Drawing.Size(71, 14);
            this.labBookingExplanation.TabIndex = 712;
            this.labBookingExplanation.Text = "Booking Exp.";
            this.labBookingExplanation.ToolTip = "Booking Explanation";
            // 
            // labMBLReleaseType
            // 
            this.labMBLReleaseType.Location = new System.Drawing.Point(5, 223);
            this.labMBLReleaseType.Name = "labMBLReleaseType";
            this.labMBLReleaseType.Size = new System.Drawing.Size(69, 14);
            this.labMBLReleaseType.TabIndex = 0;
            this.labMBLReleaseType.Text = "ReleaseType";
            // 
            // labPickupRequirement
            // 
            this.labPickupRequirement.Location = new System.Drawing.Point(195, 246);
            this.labPickupRequirement.Name = "labPickupRequirement";
            this.labPickupRequirement.Size = new System.Drawing.Size(68, 14);
            this.labPickupRequirement.TabIndex = 712;
            this.labPickupRequirement.Text = "Pick-up Req.";
            this.labPickupRequirement.ToolTip = "Pick-up Requirement";
            // 
            // NotifyParty
            // 
            this.NotifyParty.Location = new System.Drawing.Point(5, 170);
            this.NotifyParty.Name = "NotifyParty";
            this.NotifyParty.Size = new System.Drawing.Size(60, 14);
            this.NotifyParty.TabIndex = 710;
            this.NotifyParty.Text = "NotifyParty";
            // 
            // labShippingShipper
            // 
            this.labShippingShipper.Location = new System.Drawing.Point(5, 122);
            this.labShippingShipper.Name = "labShippingShipper";
            this.labShippingShipper.Size = new System.Drawing.Size(41, 14);
            this.labShippingShipper.TabIndex = 707;
            this.labShippingShipper.Text = "Shipper";
            // 
            // labShippingConsignee
            // 
            this.labShippingConsignee.Location = new System.Drawing.Point(5, 146);
            this.labShippingConsignee.Name = "labShippingConsignee";
            this.labShippingConsignee.Size = new System.Drawing.Size(56, 14);
            this.labShippingConsignee.TabIndex = 706;
            this.labShippingConsignee.Text = "Consignee";
            // 
            // labCYClosingDate
            // 
            this.labCYClosingDate.Location = new System.Drawing.Point(631, 279);
            this.labCYClosingDate.Name = "labCYClosingDate";
            this.labCYClosingDate.Size = new System.Drawing.Size(52, 14);
            this.labCYClosingDate.TabIndex = 687;
            this.labCYClosingDate.Text = "CYClosing";
            // 
            // cargoDescriptionPart1
            // 
            this.cargoDescriptionPart1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.cargoDescriptionPart1.Appearance.Options.UseBackColor = true;
            this.cargoDescriptionPart1.Location = new System.Drawing.Point(402, 103);
            this.cargoDescriptionPart1.Name = "cargoDescriptionPart1";
            this.cargoDescriptionPart1.Size = new System.Drawing.Size(19, 21);
            this.cargoDescriptionPart1.TabIndex = 692;
            // 
            // stxtVoyage
            // 
            this.stxtVoyage.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsBookingInfo, "VoyageName", true));
            this.stxtVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "VoyageID", true));
            this.stxtVoyage.EditText = "";
            this.stxtVoyage.EditValue = null;
            this.stxtVoyage.Location = new System.Drawing.Point(504, 208);
            this.stxtVoyage.Name = "stxtVoyage";
            this.stxtVoyage.ReadOnly = false;
            this.stxtVoyage.RefreshButtonToolTip = "刷新以获取与输入相匹配的船名/航次";
            this.stxtVoyage.ShowRefreshButton = true;
            this.stxtVoyage.Size = new System.Drawing.Size(313, 21);
            this.stxtVoyage.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtVoyage.TabIndex = 322;
            this.stxtVoyage.ToolTip = "";
            this.stxtVoyage.EditValueChanged += new System.EventHandler(this.stxtVoyage_EditValueChanged);
            // 
            // stxtPreVoyage
            // 
            this.stxtPreVoyage.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsBookingInfo, "PreVoyageName", true));
            this.stxtPreVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "PreVoyageID", true));
            this.stxtPreVoyage.EditText = "";
            this.stxtPreVoyage.EditValue = null;
            this.stxtPreVoyage.Location = new System.Drawing.Point(504, 185);
            this.stxtPreVoyage.Name = "stxtPreVoyage";
            this.stxtPreVoyage.ReadOnly = false;
            this.stxtPreVoyage.RefreshButtonToolTip = "刷新以获取与输入相匹配的船名/航次";
            this.stxtPreVoyage.ShowRefreshButton = true;
            this.stxtPreVoyage.Size = new System.Drawing.Size(169, 21);
            this.stxtPreVoyage.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtPreVoyage.TabIndex = 319;
            this.stxtPreVoyage.ToolTip = "";
            // 
            // labPreVoyage
            // 
            this.labPreVoyage.Location = new System.Drawing.Point(417, 188);
            this.labPreVoyage.Name = "labPreVoyage";
            this.labPreVoyage.Size = new System.Drawing.Size(59, 14);
            this.labPreVoyage.TabIndex = 691;
            this.labPreVoyage.Text = "PreVoyage";
            // 
            // chkIsOnlyMBL
            // 
            this.chkIsOnlyMBL.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsOnlyMBL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsOnlyMBL.Location = new System.Drawing.Point(639, 5);
            this.chkIsOnlyMBL.Name = "chkIsOnlyMBL";
            this.chkIsOnlyMBL.Properties.Caption = "OnlyMBL";
            this.chkIsOnlyMBL.Size = new System.Drawing.Size(67, 19);
            this.chkIsOnlyMBL.TabIndex = 317;
            // 
            // labVoyage
            // 
            this.labVoyage.Location = new System.Drawing.Point(417, 211);
            this.labVoyage.Name = "labVoyage";
            this.labVoyage.Size = new System.Drawing.Size(41, 14);
            this.labVoyage.TabIndex = 683;
            this.labVoyage.Text = "Voyage";
            // 
            // labPickUpDate
            // 
            this.labPickUpDate.Location = new System.Drawing.Point(7, 296);
            this.labPickUpDate.Name = "labPickUpDate";
            this.labPickUpDate.Size = new System.Drawing.Size(65, 14);
            this.labPickUpDate.TabIndex = 690;
            this.labPickUpDate.Text = "Pick-Up Ear.";
            // 
            // labWarehouse
            // 
            this.labWarehouse.Location = new System.Drawing.Point(8, 319);
            this.labWarehouse.Name = "labWarehouse";
            this.labWarehouse.Size = new System.Drawing.Size(90, 14);
            this.labWarehouse.TabIndex = 689;
            this.labWarehouse.Text = "Pick-Up Location";
            // 
            // labReturnLocation
            // 
            this.labReturnLocation.Location = new System.Drawing.Point(8, 343);
            this.labReturnLocation.Name = "labReturnLocation";
            this.labReturnLocation.Size = new System.Drawing.Size(87, 14);
            this.labReturnLocation.TabIndex = 688;
            this.labReturnLocation.Text = "Return Location";
            // 
            // txtContractNo
            // 
            this.txtContractNo.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "ContractID", true));
            this.txtContractNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "ContractNo", true));
            this.txtContractNo.EditValue = "";
            this.txtContractNo.Location = new System.Drawing.Point(504, 28);
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtContractNo.Size = new System.Drawing.Size(129, 21);
            this.txtContractNo.TabIndex = 316;
            // 
            // chkHasContract
            // 
            this.chkHasContract.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "IsContract", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkHasContract.Location = new System.Drawing.Point(417, 30);
            this.chkHasContract.Name = "chkHasContract";
            this.chkHasContract.Properties.Caption = "Contract No";
            this.chkHasContract.Size = new System.Drawing.Size(90, 19);
            this.chkHasContract.TabIndex = 695;
            // 
            // labShippingLine
            // 
            this.labShippingLine.Location = new System.Drawing.Point(417, 348);
            this.labShippingLine.Name = "labShippingLine";
            this.labShippingLine.Size = new System.Drawing.Size(72, 14);
            this.labShippingLine.TabIndex = 681;
            this.labShippingLine.Text = "Shipping Line";
            // 
            // dtePickUpDate
            // 
            this.dtePickUpDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "PickupEarliestDate", true));
            this.dtePickUpDate.EditValue = null;
            this.dtePickUpDate.Location = new System.Drawing.Point(92, 295);
            this.dtePickUpDate.Name = "dtePickUpDate";
            this.dtePickUpDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtePickUpDate.Properties.Mask.EditMask = "";
            this.dtePickUpDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtePickUpDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtePickUpDate.Size = new System.Drawing.Size(315, 21);
            this.dtePickUpDate.TabIndex = 329;
            // 
            // stxtWarehouse
            // 
            this.stxtWarehouse.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "WarehouseID", true));
            this.stxtWarehouse.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "WarehouseName", true));
            this.stxtWarehouse.EditValue = "";
            this.stxtWarehouse.Location = new System.Drawing.Point(93, 318);
            this.stxtWarehouse.Name = "stxtWarehouse";
            this.stxtWarehouse.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtWarehouse.Properties.Appearance.Options.UseBackColor = true;
            this.stxtWarehouse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtWarehouse.Size = new System.Drawing.Size(314, 21);
            this.stxtWarehouse.TabIndex = 331;
            // 
            // stxtReturnLocation
            // 
            this.stxtReturnLocation.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBookingInfo, "ReturnLocationID", true));
            this.stxtReturnLocation.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "ReturnLocationName", true));
            this.stxtReturnLocation.EditValue = "";
            this.stxtReturnLocation.Location = new System.Drawing.Point(93, 342);
            this.stxtReturnLocation.Name = "stxtReturnLocation";
            this.stxtReturnLocation.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtReturnLocation.Properties.Appearance.Options.UseBackColor = true;
            this.stxtReturnLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtReturnLocation.Size = new System.Drawing.Size(314, 21);
            this.stxtReturnLocation.TabIndex = 332;
            // 
            // dteCYClosingDate
            // 
            this.dteCYClosingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "CYClosingDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteCYClosingDate.EditValue = null;
            this.dteCYClosingDate.Location = new System.Drawing.Point(707, 276);
            this.dteCYClosingDate.Name = "dteCYClosingDate";
            this.dteCYClosingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteCYClosingDate.Properties.Mask.EditMask = "";
            this.dteCYClosingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteCYClosingDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteCYClosingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteCYClosingDate.Size = new System.Drawing.Size(111, 21);
            this.dteCYClosingDate.TabIndex = 325;
            // 
            // labCloseWarehouse
            // 
            this.labCloseWarehouse.Location = new System.Drawing.Point(631, 348);
            this.labCloseWarehouse.Name = "labCloseWarehouse";
            this.labCloseWarehouse.Size = new System.Drawing.Size(49, 14);
            this.labCloseWarehouse.TabIndex = 686;
            this.labCloseWarehouse.Text = "WClosing";
            // 
            // labDOCClosingDate
            // 
            this.labDOCClosingDate.Location = new System.Drawing.Point(631, 302);
            this.labDOCClosingDate.Name = "labDOCClosingDate";
            this.labDOCClosingDate.Size = new System.Drawing.Size(61, 14);
            this.labDOCClosingDate.TabIndex = 685;
            this.labDOCClosingDate.Text = "DOCClosing";
            // 
            // labClosingDate
            // 
            this.labClosingDate.Location = new System.Drawing.Point(417, 325);
            this.labClosingDate.Name = "labClosingDate";
            this.labClosingDate.Size = new System.Drawing.Size(37, 14);
            this.labClosingDate.TabIndex = 684;
            this.labClosingDate.Text = "Closing";
            // 
            // dteWarehouseClosing
            // 
            this.dteWarehouseClosing.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ClosingWarehousedate", true));
            this.dteWarehouseClosing.EditValue = null;
            this.dteWarehouseClosing.Location = new System.Drawing.Point(707, 345);
            this.dteWarehouseClosing.Name = "dteWarehouseClosing";
            this.dteWarehouseClosing.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteWarehouseClosing.Properties.Mask.EditMask = "";
            this.dteWarehouseClosing.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteWarehouseClosing.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteWarehouseClosing.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteWarehouseClosing.Size = new System.Drawing.Size(111, 21);
            this.dteWarehouseClosing.TabIndex = 330;
            // 
            // dteDOCClosingDate
            // 
            this.dteDOCClosingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "DOCClosingDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteDOCClosingDate.EditValue = null;
            this.dteDOCClosingDate.Location = new System.Drawing.Point(707, 299);
            this.dteDOCClosingDate.Name = "dteDOCClosingDate";
            this.dteDOCClosingDate.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.dteDOCClosingDate.Properties.Appearance.Options.UseBackColor = true;
            this.dteDOCClosingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDOCClosingDate.Properties.DisplayFormat.FormatString = "s";
            this.dteDOCClosingDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteDOCClosingDate.Properties.EditFormat.FormatString = "s";
            this.dteDOCClosingDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteDOCClosingDate.Properties.Mask.EditMask = "";
            this.dteDOCClosingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteDOCClosingDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteDOCClosingDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.dteDOCClosingDate.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            this.dteDOCClosingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteDOCClosingDate.Size = new System.Drawing.Size(111, 21);
            this.dteDOCClosingDate.TabIndex = 327;
            // 
            // dteClosingDate
            // 
            this.dteClosingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "ClosingDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteClosingDate.EditValue = null;
            this.dteClosingDate.Location = new System.Drawing.Point(504, 322);
            this.dteClosingDate.Name = "dteClosingDate";
            this.dteClosingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteClosingDate.Properties.Mask.EditMask = "";
            this.dteClosingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteClosingDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteClosingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteClosingDate.Size = new System.Drawing.Size(122, 21);
            this.dteClosingDate.TabIndex = 326;
            // 
            // mcmbCarrier
            // 
            this.mcmbCarrier.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "CarrierID", true));
            this.mcmbCarrier.EditText = "";
            this.mcmbCarrier.EditValue = null;
            this.mcmbCarrier.Location = new System.Drawing.Point(504, 3);
            this.mcmbCarrier.Name = "mcmbCarrier";
            this.mcmbCarrier.ReadOnly = false;
            this.mcmbCarrier.RefreshButtonToolTip = "";
            this.mcmbCarrier.ShowRefreshButton = false;
            this.mcmbCarrier.Size = new System.Drawing.Size(129, 21);
            this.mcmbCarrier.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbCarrier.TabIndex = 315;
            this.mcmbCarrier.ToolTip = "";
            // 
            // stxtAgent
            // 
            this.stxtAgent.ContactStage = ICP.Framework.CommonLibrary.Common.ContactStage.Unknown;
            this.stxtAgent.ContactType = ICP.Framework.CommonLibrary.Common.ContactType.Customer;
            this.stxtAgent.CustomerID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.stxtAgent.CustomerName = null;
            this.stxtAgent.CustomerType = ICP.Common.ServiceInterface.DataObjects.CustomerType.Unknown;
            this.stxtAgent.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBookingInfo, "AgentID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtAgent.DataSource = null;
            this.stxtAgent.DisplayMember = "EName";
            this.stxtAgent.EditValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.stxtAgent.Location = new System.Drawing.Point(88, 75);
            this.stxtAgent.Margin = new System.Windows.Forms.Padding(0);
            this.stxtAgent.Name = "stxtAgent";
            this.stxtAgent.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Bottom;
            this.stxtAgent.Size = new System.Drawing.Size(318, 21);
            this.stxtAgent.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtAgent.TabIndex = 304;
            this.stxtAgent.Tag = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.stxtAgent.TradeTermID = null;
            this.stxtAgent.TradeTermName = null;
            this.stxtAgent.ValueMember = "ID";
            // 
            // labAgent
            // 
            this.labAgent.Location = new System.Drawing.Point(5, 73);
            this.labAgent.Name = "labAgent";
            this.labAgent.Size = new System.Drawing.Size(34, 14);
            this.labAgent.TabIndex = 541;
            this.labAgent.Text = "Agent";
            // 
            // labOrderNo
            // 
            this.labOrderNo.Location = new System.Drawing.Point(5, 6);
            this.labOrderNo.Name = "labOrderNo";
            this.labOrderNo.Size = new System.Drawing.Size(69, 14);
            this.labOrderNo.TabIndex = 543;
            this.labOrderNo.Text = "SONo/RefNo";
            // 
            // labSODate
            // 
            this.labSODate.Location = new System.Drawing.Point(5, 28);
            this.labSODate.Name = "labSODate";
            this.labSODate.Size = new System.Drawing.Size(42, 14);
            this.labSODate.TabIndex = 542;
            this.labSODate.Text = "SODate";
            // 
            // labAgentOfCarrier
            // 
            this.labAgentOfCarrier.Location = new System.Drawing.Point(5, 97);
            this.labAgentOfCarrier.Name = "labAgentOfCarrier";
            this.labAgentOfCarrier.Size = new System.Drawing.Size(81, 14);
            this.labAgentOfCarrier.TabIndex = 545;
            this.labAgentOfCarrier.Text = "AgentOfCarrier";
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(417, 6);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(34, 14);
            this.labCarrier.TabIndex = 544;
            this.labCarrier.Text = "Carrier";
            // 
            // cmbOrderNo
            // 
            this.cmbOrderNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBookingInfo, "OceanShippingOrderNo", true));
            this.cmbOrderNo.EditValue = "";
            this.cmbOrderNo.Location = new System.Drawing.Point(88, 3);
            this.cmbOrderNo.Name = "cmbOrderNo";
            this.cmbOrderNo.Properties.AutoComplete = false;
            this.cmbOrderNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cmbOrderNo.Size = new System.Drawing.Size(134, 21);
            this.cmbOrderNo.TabIndex = 301;
            this.cmbOrderNo.Leave += new System.EventHandler(this.cmbOrderNo_Leave);
            // 
            // navBarGroupControlContainer6
            // 
            this.navBarGroupControlContainer6.Controls.Add(this.partDelegate);
            this.navBarGroupControlContainer6.Name = "navBarGroupControlContainer6";
            this.navBarGroupControlContainer6.Size = new System.Drawing.Size(824, 176);
            this.navBarGroupControlContainer6.TabIndex = 7;
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
            this.partDelegate.Size = new System.Drawing.Size(824, 176);
            this.partDelegate.TabIndex = 2;
            this.partDelegate.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("partDelegate.UsedMessages")));
            // 
            // navBarDelegate
            // 
            this.navBarDelegate.Caption = "Booking Note";
            this.navBarDelegate.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarDelegate.Expanded = true;
            this.navBarDelegate.GroupClientHeight = 307;
            this.navBarDelegate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarDelegate.Name = "navBarDelegate";
            // 
            // navBarDelegates
            // 
            this.navBarDelegates.Caption = "CSP Booking Delegates";
            this.navBarDelegates.ControlContainer = this.navBarGroupControlContainer6;
            this.navBarDelegates.GroupClientHeight = 178;
            this.navBarDelegates.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarDelegates.Name = "navBarDelegates";
            // 
            // navBarBooking
            // 
            this.navBarBooking.Caption = "Shipping Order";
            this.navBarBooking.ControlContainer = this.navBarGroupControlContainer5;
            this.navBarBooking.Expanded = true;
            this.navBarBooking.GroupClientHeight = 386;
            this.navBarBooking.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBooking.Name = "navBarBooking";
            // 
            // navBarOther
            // 
            this.navBarOther.Caption = "Other Info";
            this.navBarOther.ControlContainer = this.navBarGroupControlContainer3;
            this.navBarOther.Expanded = true;
            this.navBarOther.GroupClientHeight = 158;
            this.navBarOther.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarOther.Name = "navBarOther";
            // 
            // navBarFee
            // 
            this.navBarFee.Caption = "Fee Info";
            this.navBarFee.ControlContainer = this.navBarGroupControlContainer4;
            this.navBarFee.Expanded = true;
            this.navBarFee.GroupClientHeight = 330;
            this.navBarFee.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarFee.Name = "navBarFee";
            // 
            // navOther
            // 
            this.navOther.Caption = "Contact Info";
            this.navOther.ControlContainer = this.navGroupColOther;
            this.navOther.Expanded = true;
            this.navOther.GroupClientHeight = 400;
            this.navOther.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navOther.Name = "navOther";
            // 
            // tabPagePO
            // 
            this.tabPagePO.Controls.Add(this.paneltabPagePO);
            this.tabPagePO.Name = "tabPagePO";
            this.tabPagePO.Size = new System.Drawing.Size(870, 495);
            this.tabPagePO.Text = "PO";
            // 
            // paneltabPagePO
            // 
            this.paneltabPagePO.Controls.Add(this.bookingPOEditPart1);
            this.paneltabPagePO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paneltabPagePO.Location = new System.Drawing.Point(0, 0);
            this.paneltabPagePO.Name = "paneltabPagePO";
            this.paneltabPagePO.Size = new System.Drawing.Size(870, 495);
            this.paneltabPagePO.TabIndex = 1;
            // 
            // bookingPOEditPart1
            // 
            this.bookingPOEditPart1.Dock = System.Windows.Forms.DockStyle.Left;
            this.bookingPOEditPart1.IsDesignMode = true;
            this.bookingPOEditPart1.IsOrderPO = false;
            this.bookingPOEditPart1.Location = new System.Drawing.Point(2, 2);
            this.bookingPOEditPart1.Name = "bookingPOEditPart1";
            this.bookingPOEditPart1.OrderId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.bookingPOEditPart1.Size = new System.Drawing.Size(816, 491);
            this.bookingPOEditPart1.TabIndex = 0;
            this.bookingPOEditPart1.Workitem = null;
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
            // devErrorCheck
            // 
            this.devErrorCheck.ContainerControl = this;
            this.devErrorCheck.DataSource = this.bsBookingInfo;
            // 
            // BookingBaseEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControl1);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BookingBaseEditPart";
            this.Size = new System.Drawing.Size(900, 555);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBookingInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuantityUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtMarks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfReceipt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteSODate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteSODate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgentOfCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShippingLine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingShipper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingConsignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingNotifyParty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsWarehouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsFumigate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsInsurance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCustoms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTruck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtNotifyParty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMblTransportClause.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLReleaseType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLPaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHBLRequirements.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlacePayOrder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCargoType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBookingMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlacePay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbOverseasFiler.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbFiler.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbBookinger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTradeTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPageBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paneltabPageBase)).EndInit();
            this.paneltabPageBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomsClearance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkThirdPlacePay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbBookingBy.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfDeliveryAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfReceiptAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOkToSub.Properties)).EndInit();
            this.groupLocalService.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOL.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPlaceOfReceipt.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstxtPlaceOfReceipt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtFinalDestination.Properties)).EndInit();
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHSCode.Properties)).EndInit();
            this.groupHBL.ResumeLayout(false);
            this.groupHBL.PerformLayout();
            this.navBarGroupControlContainer4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.navBarGroupControlContainer5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteVGMCutOff.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteVGMCutOff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteGateInDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteGateInDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.daterailcutoff.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.daterailcutoff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookingRefNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSCAC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOrderThirdPay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit3.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit4.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteAMSClosing.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteAMSClosing.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOnlyMBL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasContract.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePickUpDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePickUpDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtWarehouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtReturnLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCYClosingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCYClosingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteWarehouseClosing.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteWarehouseClosing.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOCClosingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOCClosingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOrderNo.Properties)).EndInit();
            this.navBarGroupControlContainer6.ResumeLayout(false);
            this.tabPagePO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paneltabPagePO)).EndInit();
            this.paneltabPagePO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbQtyUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditFloat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.devErrorCheck)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsBookingInfo;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.LabelControl labBookingCustomer;
        private ICP.Business.Common.UI.BusinessContactPopupContainerEdit stxtBookingCustomer;
        private DevExpress.XtraEditors.LabelControl labShipper;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtShipper;
        private DevExpress.XtraEditors.LabelControl labConsignee;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtConsignee;
        private DevExpress.XtraEditors.LabelControl labQuantity;
        private DevExpress.XtraEditors.LabelControl labWeight;
        private DevExpress.XtraEditors.LabelControl labMeasurement;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbQuantityUnit;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMeasurementUnit;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbWeightUnit;
        private ICP.Framework.ClientComponents.Controls.ContainerDemandControl containerDemandControl1;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private DevExpress.XtraEditors.LabelControl labPOL2;
        private DevExpress.XtraEditors.DateEdit dtstxtPlaceOfReceipt;
        private DevExpress.XtraEditors.LabelControl labETD;
        private DevExpress.XtraEditors.DateEdit dtstxtPOD;
        private DevExpress.XtraEditors.LabelControl labETA;
        private DevExpress.XtraEditors.LabelControl labPlaceOfDelivery;
        private DevExpress.XtraEditors.LabelControl labExpectedArriveDate;
        private DevExpress.XtraEditors.LabelControl labEstimatedDeliveryDate;
        private DevExpress.XtraEditors.LabelControl labExpectedShipDate;
        private DevExpress.XtraEditors.DateEdit dteExpectedArriveDate;
        private DevExpress.XtraEditors.DateEdit dteExpectedShipDate;
        private DevExpress.XtraEditors.DateEdit dteEstimatedDeliveryDate;
        private DevExpress.XtraEditors.MemoEdit txtMBLRequirements;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMBLPaymentTerm;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMBLReleaseType;
        private DevExpress.XtraEditors.LabelControl labMBLPaymentTerm;
        private DevExpress.XtraEditors.LabelControl labMBLReleaseType;
        private DevExpress.XtraEditors.LabelControl labDeliveryDate;
        private DevExpress.XtraEditors.DateEdit dteDeliveryDate;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabPageBase;
        private DevExpress.XtraTab.XtraTabPage tabPagePO;
        private BookingPOEditPart bookingPOEditPart1;
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
        private DevExpress.XtraEditors.MemoEdit txtMarks;
        private DevExpress.XtraEditors.LabelControl labMarks;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarBase;
        private DevExpress.XtraNavBar.NavBarGroup navBarDelegate;
        private DevExpress.XtraNavBar.NavBarGroup navBarOther;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private DevExpress.XtraEditors.LabelControl labPlaceOfReceipt;
        private ICP.FCM.Common.UI.UCButtonEdit stxtPlaceOfReceipt;
        private ICP.FCM.Common.UI.UCButtonEdit stxtPOD;
        private ICP.FCM.Common.UI.UCButtonEdit stxtPOL;
         
        
        
      
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbQtyUnit;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox2;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox3;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rspinEditInt;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rspinEditFloat;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        
        private DevExpress.XtraEditors.LabelControl labFinalDestination;
        private DevExpress.XtraEditors.ButtonEdit stxtFinalDestination;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer4;
        private ICP.FCM.OceanExport.UI.Order.OrderFeeEditPart orderFeeEditPart1;
        private DevExpress.XtraNavBar.NavBarGroup navBarFee;
        private DevExpress.XtraBars.BarButtonItem barAuditAndSave;
        private DevExpress.XtraBars.BarButtonItem barTruck;
        private DevExpress.XtraBars.BarButtonItem barApplyAgent;
        private DevExpress.XtraBars.BarSubItem barSubPrint;
        private DevExpress.XtraBars.BarButtonItem barPrintOrder;
        private DevExpress.XtraBars.BarButtonItem barPrintInWarehouse;
        private DevExpress.XtraEditors.LabelControl labPOLETD;
        private DevExpress.XtraEditors.DateEdit dtstxtPOL;
        private DevExpress.XtraNavBar.NavBarGroup navOther;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navGroupColOther;
        private DevExpress.XtraBars.BarButtonItem barMailCustomer;
        private DevExpress.XtraBars.BarButtonItem barLocalService;
        private DevExpress.XtraBars.BarButtonItem barBLInfo;
        private DevExpress.XtraBars.BarButtonItem barEvent;
        private DevExpress.XtraBars.BarSubItem barMailToCustomer;
        private DevExpress.XtraBars.BarButtonItem barAskCustomerForSICHS;
        private DevExpress.XtraBars.BarButtonItem barAskCustomerForSIENG;
        private DevExpress.XtraBars.BarButtonItem barMailSOCopyToCustomerCHS;
        private DevExpress.XtraBars.BarButtonItem barMailSOCopyToCustomerENG;
        private DevExpress.XtraBars.BarButtonItem barOrderCustoms2;
        private DevExpress.XtraBars.BarButtonItem barOrderWarehouse;
        private DevExpress.XtraBars.BarButtonItem barOrderCommodityInspection;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButViewSOHistory2;
        private DevExpress.XtraBars.BarButtonItem barAskProfitPromiseCHS;
        private DevExpress.XtraBars.BarButtonItem barAskProfitPromiseENG;
        private DevExpress.XtraBars.BarButtonItem barConfirmDebitFeesCHS;
        private DevExpress.XtraBars.BarButtonItem barConfirmDebitFeesENG;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barInquireRates2;
        private DevExpress.XtraBars.BarButtonItem barSopv;
        private DevExpress.XtraBars.BarButtonItem barEmailbooking;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer5;
        private DevExpress.XtraNavBar.NavBarGroup navBarBooking;
        private DevExpress.XtraEditors.LabelControl NotifyParty;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtBookingNotifyParty;
        private DevExpress.XtraEditors.LabelControl labShippingShipper;
        private DevExpress.XtraEditors.LabelControl labShippingConsignee;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtBookingShipper;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtBookingConsignee;
        private DevExpress.XtraEditors.LabelControl labCYClosingDate;
        private ICP.FCM.OceanExport.UI.Common.CargoDescriptionPart cargoDescriptionPart1;
        private ICP.Common.UI.UCVoyageLookupEdit stxtVoyage;
        private ICP.Common.UI.UCVoyageLookupEdit stxtPreVoyage;
        private DevExpress.XtraEditors.LabelControl labPreVoyage;
        private DevExpress.XtraEditors.CheckEdit chkIsOnlyMBL;
        private DevExpress.XtraEditors.LabelControl labVoyage;
        private DevExpress.XtraEditors.LabelControl labPickUpDate;
        private DevExpress.XtraEditors.LabelControl labWarehouse;
        private DevExpress.XtraEditors.LabelControl labReturnLocation;
        private DevExpress.XtraEditors.ButtonEdit txtContractNo;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbShippingLine;
        private DevExpress.XtraEditors.CheckEdit chkHasContract;
        private DevExpress.XtraEditors.LabelControl labShippingLine;
        private DevExpress.XtraEditors.DateEdit dtePickUpDate;
        private DevExpress.XtraEditors.ButtonEdit stxtWarehouse;
        private DevExpress.XtraEditors.DateEdit dteCYClosingDate;
        private DevExpress.XtraEditors.LabelControl labCloseWarehouse;
        private DevExpress.XtraEditors.LabelControl labDOCClosingDate;
        private DevExpress.XtraEditors.LabelControl labClosingDate;
        private DevExpress.XtraEditors.DateEdit dteWarehouseClosing;
        private DevExpress.XtraEditors.DateEdit dteDOCClosingDate;
        private DevExpress.XtraEditors.DateEdit dteClosingDate;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbCarrier;
        private ICP.Business.Common.UI.ComboBusinessContactDetailInfoControl stxtAgent;
        private DevExpress.XtraEditors.LabelControl labAgent;
        private DevExpress.XtraEditors.DateEdit dteSODate;
        private DevExpress.XtraEditors.LabelControl labOrderNo;
        private DevExpress.XtraEditors.LabelControl labSODate;
        private DevExpress.XtraEditors.LabelControl labAgentOfCarrier;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private ICP.Business.Common.UI.BusinessContactPopupContainerEdit stxtAgentOfCarrier;
        private DevExpress.XtraEditors.ComboBoxEdit cmbOrderNo;
        private DevExpress.XtraEditors.LabelControl labPickupRequirement;
        private DevExpress.XtraEditors.LabelControl labBookingExplanation;
        private GroupBox groupLocalService;
        private DevExpress.XtraEditors.CheckEdit chkIsWarehouse;
        private DevExpress.XtraEditors.CheckEdit chkIsFumigate;
        private DevExpress.XtraEditors.CheckEdit chkIsInsurance;
        private DevExpress.XtraEditors.CheckEdit chkIsCustoms;
        private DevExpress.XtraEditors.CheckEdit chkIsTruck;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.MemoEdit txtCommodity;
        private DevExpress.XtraEditors.LabelControl labNotifyParty;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtNotifyParty;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMblTransportClause;
        private DevExpress.XtraEditors.LabelControl labShippingTransportClause;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labVoyageETA;
        private DevExpress.XtraEditors.MemoEdit memoEdit3;
        private DevExpress.XtraEditors.DateEdit dateEdit3;
        private DevExpress.XtraEditors.DateEdit dateEdit2;
        private DevExpress.XtraEditors.DateEdit dateEdit4;
        private DevExpress.XtraEditors.CheckEdit checkEdit2;
        private DevExpress.XtraEditors.LabelControl labAMSClosing;
        private DevExpress.XtraEditors.DateEdit dteAMSClosing;
        private DevExpress.XtraEditors.LabelControl labBookingParty;
        private DevExpress.XtraEditors.LabelControl labMBLRemark;
        private GroupBox groupHBL;
        private DevExpress.XtraEditors.MemoEdit txtHBLRequirements;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbHBLPaymentTerm;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbHBLReleaseType;
        private DevExpress.XtraEditors.LabelControl labHBLPaymentTerm;
        private DevExpress.XtraEditors.LabelControl labHBLReleaseType;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl lblRemark;
        private DevExpress.XtraEditors.LabelControl lblCommodity;
        private ICP.FCM.Common.UI.UCButtonEdit stxtPlacePayOrder;
        private DevExpress.XtraEditors.CheckEdit chkOrderThirdPay;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.ButtonEdit stxtReturnLocation;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbBookingParty;
        private DevExpress.XtraEditors.LabelControl labCargoType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCargoType;
        private DevExpress.XtraEditors.LabelControl lblScac;
        private DevExpress.XtraEditors.TextEdit txtSCAC;
        private DevExpress.XtraBars.BarButtonItem barInquireRates;
        private DevExpress.XtraBars.BarButtonItem barButViewSOHistory;
        private DevExpress.XtraBars.BarButtonItem barOrderCustoms;
        private DevExpress.XtraEditors.CheckEdit chkOkToSub;
        private DevExpress.XtraEditors.TextEdit txtBookingRefNo;
        private DevExpress.XtraEditors.LabelControl lblrailcutoff;
        private DevExpress.XtraEditors.DateEdit daterailcutoff;
        private DevExpress.XtraEditors.LabelControl lblRefNo;
        private ICP.FCM.OceanExport.UI.Common.Parts.UCInquirePrice ucInquirePrice;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider devErrorCheck;
        private DevExpress.XtraEditors.LabelControl labGateInDate;
        private DevExpress.XtraEditors.DateEdit dteGateInDate;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private DevExpress.XtraEditors.TextEdit txtCustomsClearance;
        private DevExpress.XtraEditors.LabelControl lblCustomsClearance;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbBookingMode;
        private DevExpress.XtraEditors.LabelControl labBookingMode;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTransportClause;
        private DevExpress.XtraEditors.LabelControl labTransportClause;
        private Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbSales;
        private Framework.ClientComponents.Controls.TreeSelectBox trsSalesDep;
        private DevExpress.XtraEditors.LabelControl labSales;
        private DevExpress.XtraEditors.LabelControl labSalesDepartment;
        private FCM.Common.UI.UCButtonEdit stxtPlacePay;
        private DevExpress.XtraEditors.CheckEdit chkThirdPlacePay;
        private DevExpress.XtraEditors.LabelControl labBookinger;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit mcmbOverseasFiler;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit mcmbBookingBy;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit mcmbFiler;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit mcmbBookinger;
        private DevExpress.XtraEditors.LabelControl labNo;
        private DevExpress.XtraEditors.DateEdit dteBookingDate;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.LabelControl labTradeTerm;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.LabelControl labState;
        private DevExpress.XtraEditors.LabelControl labBookingDate;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.LabelControl labSalesType;
        private Business.Common.UI.CustomerBusinessContactControl stxtCustomer;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTradeTerm;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.LabelControl labBookingBy;
        private DevExpress.XtraEditors.LabelControl labFiler;
        private DevExpress.XtraEditors.LabelControl labOverseasFiler;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbPaymentTerm;
        private DevExpress.XtraEditors.LabelControl labPaymentTerm;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbSalesType;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbType;
        private DevExpress.XtraEditors.TextEdit txtState;
        private DevExpress.XtraBars.BarButtonItem barSavingClose;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.Bar barSavingTools;
        private DevExpress.XtraBars.BarButtonItem barCancel;
        private DevExpress.XtraBars.BarStaticItem barlabMessage;
        private DevExpress.XtraBars.BarStaticItem barlabSeconds;
        private DevExpress.XtraEditors.LabelControl labQuotedPriceNo;
        private Business.Common.UI.QuotedPrice.Recent.QuotedPriceOrderControl stxtRecentQuotedPrice;
        private DevExpress.XtraEditors.DateEdit dteVGMCutOff;
        private DevExpress.XtraEditors.LabelControl labVGMCutOff;
        private DevExpress.XtraEditors.LabelControl labContranct;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.PanelControl paneltabPageBase;
        private DevExpress.XtraEditors.PanelControl paneltabPagePO;
        private DevExpress.XtraEditors.TextEdit txtHSCode;
        private DevExpress.XtraEditors.LabelControl labHSCode;
        private DevExpress.XtraEditors.LabelControl labPlaceOfReceiptAddress;
        private DevExpress.XtraEditors.TextEdit txtPlaceOfReceiptAddress;
        private DevExpress.XtraEditors.LabelControl labPlaceOfDeliveryAddress;
        private DevExpress.XtraEditors.TextEdit txtPlaceOfDeliveryAddress;
        private DevExpress.XtraNavBar.NavBarGroup navBarDelegates;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer6;
        private ICP.FCM.Common.UI.CommonPart.PartBookingForCSP partDelegate;
    }
}
