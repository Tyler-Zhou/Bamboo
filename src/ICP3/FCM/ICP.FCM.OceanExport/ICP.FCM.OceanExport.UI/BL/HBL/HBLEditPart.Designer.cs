using System.Windows.Forms;
namespace ICP.FCM.OceanExport.UI.HBL
{
    partial class HBLEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HBLEditPart));
            this.components = new System.ComponentModel.Container();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dteReleaseDate = new DevExpress.XtraEditors.DateEdit();
            this.cmbReleaseType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbTransportClause = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbPaymentTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbQuantityUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbMeasurementUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbWeightUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtChecker = new ICP.Business.Common.UI.BusinessContactPopupContainerEdit();
            this.stxtIssuePlace = new DevExpress.XtraEditors.ButtonEdit();
            this.dteIssue = new DevExpress.XtraEditors.DateEdit();
            this.stxtPlaceOfDelivery = new DevExpress.XtraEditors.ButtonEdit();
            this.txtPODName = new DevExpress.XtraEditors.TextEdit();
            this.txtPOLName = new DevExpress.XtraEditors.TextEdit();
            this.txtPlaceOfDeliveryName = new DevExpress.XtraEditors.TextEdit();
            this.txtNotifyPartyDescription = new DevExpress.XtraEditors.MemoEdit();
            this.stxtPlaceOfReceipt = new ICP.FCM.Common.UI.UCButtonEdit();
            this.txtConsigneeDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtShipperDescription = new DevExpress.XtraEditors.MemoEdit();
            this.stxtFinalDestination = new DevExpress.XtraEditors.ButtonEdit();
            this.txtFinalDestinationName = new DevExpress.XtraEditors.TextEdit();
            this.txtPlaceOfReceiptName = new DevExpress.XtraEditors.TextEdit();
            this.stxtNotifyParty = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtConsignee = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtShipper = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.txtFreightDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtMarks = new DevExpress.XtraEditors.MemoEdit();
            this.txtAgentDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtGoodsDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtCtnQtyInfo = new DevExpress.XtraEditors.MemoEdit();
            this.stxtRefNo = new DevExpress.XtraEditors.ButtonEdit();
            this.txtPOLCode = new ICP.FCM.Common.UI.UCButtonEdit();
            this.txtPODCode = new ICP.FCM.Common.UI.UCButtonEdit();
            this.cmbACIEntryType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtLastPortOfCall = new ICP.FCM.Common.UI.UCButtonEdit();
            this.txtFirstPortOfCall = new ICP.FCM.Common.UI.UCButtonEdit();
            this.txtPOL = new ICP.FCM.Common.UI.UCButtonEdit();
            this.stxtPlacePay = new ICP.FCM.Common.UI.UCButtonEdit();
            this.txtBookNo = new DevExpress.XtraEditors.TextEdit();
            this.stxtVoyage = new ICP.Common.UI.UCVoyageLookupEdit();
            this.stxtPreVoyage = new ICP.Common.UI.UCVoyageLookupEdit();
            this.dtETA = new DevExpress.XtraEditors.DateEdit();
            this.dtPETD = new DevExpress.XtraEditors.DateEdit();
            this.chkIsWoodPacking = new DevExpress.XtraEditors.CheckEdit();
            this.labIssueBy = new DevExpress.XtraEditors.LabelControl();
            this.labReleaseType = new DevExpress.XtraEditors.LabelControl();
            this.labIssueDate = new DevExpress.XtraEditors.LabelControl();
            this.labReleaseDate = new DevExpress.XtraEditors.LabelControl();
            this.labChecker = new DevExpress.XtraEditors.LabelControl();
            this.labMBLNo = new DevExpress.XtraEditors.LabelControl();
            this.labRefNo = new DevExpress.XtraEditors.LabelControl();
            this.labIssuePlace = new DevExpress.XtraEditors.LabelControl();
            this.labHBlNo = new DevExpress.XtraEditors.LabelControl();
            this.labPaymentTerm = new DevExpress.XtraEditors.LabelControl();
            this.labPlaceOfReceipt = new DevExpress.XtraEditors.LabelControl();
            this.labPlaceOfDelivery = new DevExpress.XtraEditors.LabelControl();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.labPOL2 = new DevExpress.XtraEditors.LabelControl();
            this.labETA = new DevExpress.XtraEditors.LabelControl();
            this.labETD2 = new DevExpress.XtraEditors.LabelControl();
            this.labVoyage = new DevExpress.XtraEditors.LabelControl();
            this.labPreVoyage = new DevExpress.XtraEditors.LabelControl();
            this.labTransportClause = new DevExpress.XtraEditors.LabelControl();
            this.labFinalDestination = new DevExpress.XtraEditors.LabelControl();
            this.labNotifyParty = new DevExpress.XtraEditors.LabelControl();
            this.labConsignee = new DevExpress.XtraEditors.LabelControl();
            this.labAgent = new DevExpress.XtraEditors.LabelControl();
            this.labShipper = new DevExpress.XtraEditors.LabelControl();
            this.labDescriptionOfGoods = new DevExpress.XtraEditors.LabelControl();
            this.labQuantity = new DevExpress.XtraEditors.LabelControl();
            this.labWeight = new DevExpress.XtraEditors.LabelControl();
            this.labMeasurement = new DevExpress.XtraEditors.LabelControl();
            this.btnContainer = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtIsWoodPacking = new DevExpress.XtraEditors.MemoEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barSavingClose = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.barSubPrint = new DevExpress.XtraBars.BarSubItem();
            this.barPrintBL = new DevExpress.XtraBars.BarButtonItem();
            this.barReplyAgent = new DevExpress.XtraBars.BarButtonItem();
            this.barSubCheck = new DevExpress.XtraBars.BarSubItem();
            this.barCheck = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckDone = new DevExpress.XtraBars.BarButtonItem();
            this.barSubEDI = new DevExpress.XtraBars.BarSubItem();
            this.barAMS = new DevExpress.XtraBars.BarButtonItem();
            this.barACI = new DevExpress.XtraBars.BarButtonItem();
            this.barISF = new DevExpress.XtraBars.BarButtonItem();
            this.btnAMSandACI = new DevExpress.XtraBars.BarButtonItem();
            this.barAMSISF = new DevExpress.XtraBars.BarButtonItem();
            this.barAMSACIISF = new DevExpress.XtraBars.BarButtonItem();
            this.barWebEdi = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barBill = new DevExpress.XtraBars.BarButtonItem();
            this.barbl = new DevExpress.XtraBars.BarSubItem();
            this.barblCHS = new DevExpress.XtraBars.BarButtonItem();
            this.barBlENG = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barSavingTools = new DevExpress.XtraBars.Bar();
            this.barCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barlabMessage = new DevExpress.XtraBars.BarStaticItem();
            this.barlabSeconds = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barAMSACI = new DevExpress.XtraBars.BarButtonItem();
            this.txtCtnQty = new DevExpress.XtraEditors.MemoEdit();
            this.labFreightDescription = new DevExpress.XtraEditors.LabelControl();
            this.labContainerDescription = new DevExpress.XtraEditors.LabelControl();
            this.labCtnQtyInfo = new DevExpress.XtraEditors.LabelControl();
            this.labMarks = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageBase = new DevExpress.XtraTab.XtraTabPage();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarBaseInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mcmbBookingParty = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labelControl30 = new DevExpress.XtraEditors.LabelControl();
            this.txtScac = new DevExpress.XtraEditors.TextEdit();
            this.txtTelexNo = new DevExpress.XtraEditors.TextEdit();
            this.labScac = new DevExpress.XtraEditors.LabelControl();
            this.txtRleaseBy = new DevExpress.XtraEditors.TextEdit();
            this.labTelexNo = new DevExpress.XtraEditors.LabelControl();
            this.labReleaseBy = new DevExpress.XtraEditors.LabelControl();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.cmbIssueType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.chkToAgent = new DevExpress.XtraEditors.CheckEdit();
            this.cmbMBLNO = new DevExpress.XtraEditors.ComboBoxEdit();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkIsBook = new DevExpress.XtraEditors.CheckEdit();
            this.labGateInDate = new DevExpress.XtraEditors.LabelControl();
            this.dteGateInDate = new DevExpress.XtraEditors.DateEdit();
            this.chkThirdPlacePay = new DevExpress.XtraEditors.CheckEdit();
            this.stxtAgent = new ICP.Framework.ClientComponents.Controls.ComboCustomerDescriptionControl();
            this.chkShowVoyage = new DevExpress.XtraEditors.CheckEdit();
            this.chkShowPreVoyage = new DevExpress.XtraEditors.CheckEdit();
            this.dtETD = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.numMeasurement = new DevExpress.XtraEditors.SpinEdit();
            this.numWeight = new DevExpress.XtraEditors.SpinEdit();
            this.numQuantity = new DevExpress.XtraEditors.SpinEdit();
            this.txtCtnInfo = new DevExpress.XtraEditors.MemoEdit();
            this.navBarGroupControlContainer4 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.mcmbIssueBy = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.navBarControlContainerContact = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navBarBLInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarCargo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarIssueInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarContact = new DevExpress.XtraNavBar.NavBarGroup();
            this.tabPageAMS = new DevExpress.XtraTab.XtraTabPage();
            this.panelALL = new DevExpress.XtraEditors.PanelControl();
            this.panelContainerDetails = new DevExpress.XtraEditors.PanelControl();
            this.txtHS10 = new DevExpress.XtraEditors.TextEdit();
            this.mscmbHS10 = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.txtHS9 = new DevExpress.XtraEditors.TextEdit();
            this.mscmbHS9 = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.txtHS8 = new DevExpress.XtraEditors.TextEdit();
            this.mscmbHS8 = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.txtHS7 = new DevExpress.XtraEditors.TextEdit();
            this.mscmbHS7 = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.txtHS6 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl24 = new DevExpress.XtraEditors.LabelControl();
            this.mscmbHS6 = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labelControl25 = new DevExpress.XtraEditors.LabelControl();
            this.txtHS5 = new DevExpress.XtraEditors.TextEdit();
            this.mscmbHS5 = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.txtHS4 = new DevExpress.XtraEditors.TextEdit();
            this.mscmbHS4 = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.txtHS3 = new DevExpress.XtraEditors.TextEdit();
            this.mscmbHS3 = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.txtHS2 = new DevExpress.XtraEditors.TextEdit();
            this.mscmbHS2 = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.txtHS1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.mscmbHS1 = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gcAMSContainer = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bindingContainers = new System.Windows.Forms.BindingSource(this.components);
            this.gvAMSContainer = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colContainerNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbBoxType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colSeal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKilos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitOfMeasure = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colFreeFormDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.barDockControl7 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl8 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl6 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl5 = new DevExpress.XtraBars.BarDockControl();
            this.panelContent = new DevExpress.XtraEditors.PanelControl();
            this.vScrollBar2 = new DevExpress.XtraEditors.VScrollBar();
            this.panelLeft = new DevExpress.XtraEditors.PanelControl();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.panel9 = new System.Windows.Forms.Panel();
            this.stxtConsolidator = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblConsolidator = new DevExpress.XtraEditors.LabelControl();
            this.ckConsolidator = new DevExpress.XtraEditors.CheckEdit();
            this.ConsolidatorDescription = new ICP.FCM.OceanExport.UI.BL.HBL.AmsCustomerDes();
            this.panel8 = new System.Windows.Forms.Panel();
            this.stxtStuffingLocation = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblStuffingLocation = new DevExpress.XtraEditors.LabelControl();
            this.ckStuffingLocation = new DevExpress.XtraEditors.CheckEdit();
            this.StuffingDescription = new ICP.FCM.OceanExport.UI.BL.HBL.AmsCustomerDes();
            this.panel7 = new System.Windows.Forms.Panel();
            this.stxtManufacturer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ckManufacturer = new DevExpress.XtraEditors.CheckEdit();
            this.lblManufacturer = new DevExpress.XtraEditors.LabelControl();
            this.ManuDescription = new ICP.FCM.OceanExport.UI.BL.HBL.AmsCustomerDes();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ckSeller = new DevExpress.XtraEditors.CheckEdit();
            this.stxtSeller = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblSeller = new DevExpress.XtraEditors.LabelControl();
            this.SellerDescription = new ICP.FCM.OceanExport.UI.BL.HBL.AmsCustomerDes();
            this.panel5 = new System.Windows.Forms.Panel();
            this.stxtAMSShipper = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ShipDescription = new ICP.FCM.OceanExport.UI.BL.HBL.AmsCustomerDes();
            this.labAMSShipper = new DevExpress.XtraEditors.LabelControl();
            this.panelISFImporterRef = new DevExpress.XtraEditors.PanelControl();
            this.cmbISFImporterRefCountry = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.bindingAMSACIISF = new System.Windows.Forms.BindingSource(this.components);
            this.dateISFImporterRefDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.txtISFImporterRefLastName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtISFImporterRefFirstName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.txtISFImporterRef = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbISFImporterRef = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.panelRight = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.panel11 = new System.Windows.Forms.Panel();
            this.BookingDescription = new ICP.FCM.OceanExport.UI.BL.HBL.AmsCustomerDes();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.stxtBookingPartyInfo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panel10 = new System.Windows.Forms.Panel();
            this.stxtShipToPatry = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblShipToPatry = new DevExpress.XtraEditors.LabelControl();
            this.ckShipTo = new DevExpress.XtraEditors.CheckEdit();
            this.ShiptoDescription = new ICP.FCM.OceanExport.UI.BL.HBL.AmsCustomerDes();
            this.labAMSNotifyParty = new DevExpress.XtraEditors.LabelControl();
            this.txtAMSNotifyPartyDescription = new DevExpress.XtraEditors.MemoEdit();
            this.stxtAMSNotifyParty = new ICP.Common.UI.Controls.CustomerPopupContainerForAMSEdit();
            this.panelBuyer = new DevExpress.XtraEditors.PanelControl();
            this.cmbBuyerCountry = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.dateBuyerDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.txtBuyerLastName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.txtBuyerFirstName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.BuyerDescription = new ICP.FCM.OceanExport.UI.BL.HBL.AmsCustomerDes();
            this.stxtBuyer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ckBuyerRef = new DevExpress.XtraEditors.CheckEdit();
            this.ckBuyer = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtBuyerImportNumber = new DevExpress.XtraEditors.TextEdit();
            this.cmbBuyerImportNumber = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.lblBuyer = new DevExpress.XtraEditors.LabelControl();
            this.panelConsignee = new DevExpress.XtraEditors.PanelControl();
            this.cmbConsigneeCountry = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.dateConsigneeDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.txtConsigneeLastName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.txtConsigneeFirstName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.ConsigneeDescription = new ICP.FCM.OceanExport.UI.BL.HBL.AmsCustomerDes();
            this.stxtAMSConsignee = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labAMSConsignee = new DevExpress.XtraEditors.LabelControl();
            this.ckConsigneeRef = new DevExpress.XtraEditors.CheckEdit();
            this.cmbConsigneeNumber = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtConsigneeNumber = new DevExpress.XtraEditors.TextEdit();
            this.lblCneeNumber = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtBondRefNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cmbBondRef = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbBondActivityCode = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.panelControl8 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.cmbCargoType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.panelAddAMSACIISF = new DevExpress.XtraEditors.PanelControl();
            this.gcAMSACIISF = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvAMSACIISF = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colShipperDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSellerDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyerDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipToDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colManufacturerDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStuffingLocationDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsolidatorDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.panelTop = new DevExpress.XtraEditors.PanelControl();
            this.txtFirstPortOfCallDate = new DevExpress.XtraEditors.DateEdit();
            this.txtETD = new DevExpress.XtraEditors.DateEdit();
            this.labelControl31 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl29 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl27 = new DevExpress.XtraEditors.LabelControl();
            this.txtVoyageNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl28 = new DevExpress.XtraEditors.LabelControl();
            this.txtVesselName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.txtAMSNO = new DevExpress.XtraEditors.TextEdit();
            this.txtIMO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labAMSEntryType = new DevExpress.XtraEditors.LabelControl();
            this.labAMSNO = new DevExpress.XtraEditors.LabelControl();
            this.cmbAMSEntryType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtISFNO = new DevExpress.XtraEditors.TextEdit();
            this.labACIEntryType = new DevExpress.XtraEditors.LabelControl();
            this.mscmbCountry = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labISFNO = new DevExpress.XtraEditors.LabelControl();
            this.panelScroll = new DevExpress.XtraEditors.XtraScrollableControl();
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barAddContainer = new DevExpress.XtraBars.BarButtonItem();
            this.barDelContainer = new DevExpress.XtraBars.BarButtonItem();
            this.bar7 = new DevExpress.XtraBars.Bar();
            this.barContainerForAMS = new DevExpress.XtraBars.BarManager(this.components);
            this.barAddAMSACIISF = new DevExpress.XtraBars.BarButtonItem();
            this.barDeleteAMSACIISF = new DevExpress.XtraBars.BarButtonItem();
            this.bar6 = new DevExpress.XtraBars.Bar();
            this.barAMSACIISFInfo = new DevExpress.XtraBars.BarManager(this.components);
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.lwGridControl1 = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemImageComboBox4 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemSpinEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemSpinEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.barDockControl9 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl10 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl11 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl12 = new DevExpress.XtraBars.BarDockControl();
            this.colMark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Shipper = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Consignee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NotifyParty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barConfirmedAMS = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReleaseDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReleaseDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuantityUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtChecker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtIssuePlace.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIssue.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIssue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPODName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOLName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfDeliveryName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotifyPartyDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfReceipt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipperDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtFinalDestination.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinalDestinationName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfReceiptName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtNotifyParty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFreightDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgentDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoodsDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtnQtyInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtRefNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOLCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPODCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbACIEntryType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastPortOfCall.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstPortOfCall.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlacePay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETA.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPETD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPETD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsWoodPacking.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIsWoodPacking.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtnQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPageBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtScac.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelexNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRleaseBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIssueType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkToAgent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMBLNO.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsBook.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteGateInDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteGateInDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkThirdPlacePay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowVoyage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowPreVoyage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETD.Properties)).BeginInit();
            this.navBarGroupControlContainer3.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtnInfo.Properties)).BeginInit();
            this.navBarGroupControlContainer4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPageAMS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelALL)).BeginInit();
            this.panelALL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelContainerDetails)).BeginInit();
            this.panelContainerDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS10.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS9.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS8.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS7.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAMSContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingContainers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAMSContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBoxType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelContent)).BeginInit();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).BeginInit();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            this.panelControl7.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsolidator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckConsolidator.Properties)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtStuffingLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckStuffingLocation.Properties)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtManufacturer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckManufacturer.Properties)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckSeller.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtSeller.Properties)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAMSShipper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelISFImporterRef)).BeginInit();
            this.panelISFImporterRef.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingAMSACIISF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateISFImporterRefDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateISFImporterRefDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtISFImporterRefLastName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtISFImporterRefFirstName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtISFImporterRef.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbISFImporterRef.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelRight)).BeginInit();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingPartyInfo.Properties)).BeginInit();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipToPatry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckShipTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAMSNotifyPartyDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAMSNotifyParty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBuyer)).BeginInit();
            this.panelBuyer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateBuyerDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBuyerDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuyerLastName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuyerFirstName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBuyer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckBuyerRef.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckBuyer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuyerImportNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBuyerImportNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelConsignee)).BeginInit();
            this.panelConsignee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateConsigneeDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateConsigneeDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeLastName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeFirstName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAMSConsignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckConsigneeRef.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbConsigneeNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBondRefNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBondRef.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBondActivityCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl8)).BeginInit();
            this.panelControl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCargoType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelAddAMSACIISF)).BeginInit();
            this.panelAddAMSACIISF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAMSACIISF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAMSACIISF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstPortOfCallDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstPortOfCallDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtETD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtETD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoyageNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVesselName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAMSNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIMO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAMSEntryType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtISFNO.Properties)).BeginInit();
            this.panelScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barContainerForAMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAMSACIISFInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            this.SuspendLayout();
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanHBLInfo);
            // 
            // dteReleaseDate
            // 
            this.dteReleaseDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ReleaseDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteReleaseDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteReleaseDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteReleaseDate.Location = new System.Drawing.Point(715, 1);
            this.dteReleaseDate.Name = "dteReleaseDate";
            this.dteReleaseDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteReleaseDate.Properties.Mask.EditMask = "";
            this.dteReleaseDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteReleaseDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteReleaseDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteReleaseDate.Size = new System.Drawing.Size(124, 21);
            this.dteReleaseDate.TabIndex = 5;
            // 
            // cmbReleaseType
            // 
            this.cmbReleaseType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ReleaseType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbReleaseType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbReleaseType.Location = new System.Drawing.Point(502, 1);
            this.cmbReleaseType.Name = "cmbReleaseType";
            this.cmbReleaseType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbReleaseType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbReleaseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReleaseType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbReleaseType.Size = new System.Drawing.Size(124, 21);
            this.cmbReleaseType.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbReleaseType.TabIndex = 6;
            this.cmbReleaseType.Click += new System.EventHandler(this.ReleaseType_Click);
            // 
            // cmbTransportClause
            // 
            this.cmbTransportClause.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "TransportClauseID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbTransportClause, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbTransportClause.Location = new System.Drawing.Point(502, 306);
            this.cmbTransportClause.Name = "cmbTransportClause";
            this.cmbTransportClause.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbTransportClause.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTransportClause.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTransportClause.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbTransportClause.Size = new System.Drawing.Size(124, 21);
            this.cmbTransportClause.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbTransportClause.TabIndex = 22;
            // 
            // cmbPaymentTerm
            // 
            this.cmbPaymentTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "PaymentTermID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbPaymentTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbPaymentTerm.Location = new System.Drawing.Point(715, 306);
            this.cmbPaymentTerm.Name = "cmbPaymentTerm";
            this.cmbPaymentTerm.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbPaymentTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbPaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaymentTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbPaymentTerm.Size = new System.Drawing.Size(124, 21);
            this.cmbPaymentTerm.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbPaymentTerm.TabIndex = 23;
            // 
            // cmbQuantityUnit
            // 
            this.cmbQuantityUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "QuantityUnitID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbQuantityUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbQuantityUnit.Location = new System.Drawing.Point(175, 32);
            this.cmbQuantityUnit.Name = "cmbQuantityUnit";
            this.cmbQuantityUnit.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbQuantityUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbQuantityUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQuantityUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbQuantityUnit.Size = new System.Drawing.Size(79, 21);
            this.cmbQuantityUnit.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbQuantityUnit.TabIndex = 1;
            // 
            // cmbMeasurementUnit
            // 
            this.cmbMeasurementUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "MeasurementUnitID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbMeasurementUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbMeasurementUnit.Location = new System.Drawing.Point(175, 78);
            this.cmbMeasurementUnit.Name = "cmbMeasurementUnit";
            this.cmbMeasurementUnit.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbMeasurementUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMeasurementUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMeasurementUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbMeasurementUnit.Size = new System.Drawing.Size(79, 21);
            this.cmbMeasurementUnit.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbMeasurementUnit.TabIndex = 5;
            // 
            // cmbWeightUnit
            // 
            this.cmbWeightUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "WeightUnitID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbWeightUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbWeightUnit.Location = new System.Drawing.Point(175, 55);
            this.cmbWeightUnit.Name = "cmbWeightUnit";
            this.cmbWeightUnit.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbWeightUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbWeightUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWeightUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbWeightUnit.Size = new System.Drawing.Size(79, 21);
            this.cmbWeightUnit.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbWeightUnit.TabIndex = 3;
            // 
            // stxtChecker
            // 
            this.stxtChecker.ContactType = ICP.Framework.CommonLibrary.Common.ContactType.Customer;
            this.stxtChecker.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CheckerName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtChecker.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "CheckerID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtChecker, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtChecker.Location = new System.Drawing.Point(60, 49);
            this.stxtChecker.Name = "stxtChecker";
            this.stxtChecker.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtChecker.Properties.ActionButtonIndex = 1;
            this.stxtChecker.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtChecker.Properties.Appearance.Options.UseBackColor = true;
            this.stxtChecker.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtChecker.Properties.CloseOnLostFocus = false;
            this.stxtChecker.Properties.CloseOnOuterMouseClick = false;
            this.stxtChecker.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtChecker.Properties.PopupSizeable = false;
            this.stxtChecker.Properties.ShowPopupCloseButton = false;
            this.stxtChecker.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtChecker.Size = new System.Drawing.Size(237, 21);
            this.stxtChecker.TabIndex = 4;
            // 
            // stxtIssuePlace
            // 
            this.stxtIssuePlace.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "IssuePlaceName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtIssuePlace.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "IssuePlaceID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtIssuePlace, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtIssuePlace.Location = new System.Drawing.Point(232, 3);
            this.stxtIssuePlace.Name = "stxtIssuePlace";
            this.stxtIssuePlace.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtIssuePlace.Properties.Appearance.Options.UseBackColor = true;
            this.stxtIssuePlace.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtIssuePlace.Size = new System.Drawing.Size(109, 21);
            this.stxtIssuePlace.TabIndex = 1;
            // 
            // dteIssue
            // 
            this.dteIssue.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IssueDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteIssue.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteIssue, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteIssue.Location = new System.Drawing.Point(449, 3);
            this.dteIssue.Name = "dteIssue";
            this.dteIssue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteIssue.Properties.Mask.EditMask = "";
            this.dteIssue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteIssue.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteIssue.Size = new System.Drawing.Size(93, 21);
            this.dteIssue.TabIndex = 2;
            // 
            // stxtPlaceOfDelivery
            // 
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "PlaceOfDeliveryID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PlaceOfDeliveryCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtPlaceOfDelivery, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPlaceOfDelivery.Location = new System.Drawing.Point(502, 258);
            this.stxtPlaceOfDelivery.Name = "stxtPlaceOfDelivery";
            this.stxtPlaceOfDelivery.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtPlaceOfDelivery.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlaceOfDelivery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlaceOfDelivery.Size = new System.Drawing.Size(101, 21);
            this.stxtPlaceOfDelivery.TabIndex = 18;
            // 
            // txtPODName
            // 
            this.txtPODName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PODName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPODName.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtPODName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPODName.Location = new System.Drawing.Point(607, 235);
            this.txtPODName.Name = "txtPODName";
            this.txtPODName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPODName.Size = new System.Drawing.Size(88, 21);
            this.txtPODName.TabIndex = 16;
            // 
            // txtPOLName
            // 
            this.txtPOLName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "POLName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPOLName.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtPOLName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPOLName.Location = new System.Drawing.Point(607, 211);
            this.txtPOLName.Name = "txtPOLName";
            this.txtPOLName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPOLName.Size = new System.Drawing.Size(88, 21);
            this.txtPOLName.TabIndex = 13;
            // 
            // txtPlaceOfDeliveryName
            // 
            this.txtPlaceOfDeliveryName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PlaceOfDeliveryName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPlaceOfDeliveryName.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtPlaceOfDeliveryName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPlaceOfDeliveryName.Location = new System.Drawing.Point(607, 258);
            this.txtPlaceOfDeliveryName.Name = "txtPlaceOfDeliveryName";
            this.txtPlaceOfDeliveryName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlaceOfDeliveryName.Size = new System.Drawing.Size(231, 21);
            this.txtPlaceOfDeliveryName.TabIndex = 19;
            // 
            // txtNotifyPartyDescription
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtNotifyPartyDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtNotifyPartyDescription.Location = new System.Drawing.Point(3, 260);
            this.txtNotifyPartyDescription.Name = "txtNotifyPartyDescription";
            this.txtNotifyPartyDescription.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNotifyPartyDescription.Properties.ReadOnly = true;
            this.txtNotifyPartyDescription.Size = new System.Drawing.Size(388, 90);
            this.txtNotifyPartyDescription.TabIndex = 5;
            // 
            // stxtPlaceOfReceipt
            // 
            this.stxtPlaceOfReceipt.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "PlaceOfReceiptID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPlaceOfReceipt.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PlaceOfReceiptCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtPlaceOfReceipt, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPlaceOfReceipt.Location = new System.Drawing.Point(502, 188);
            this.stxtPlaceOfReceipt.Name = "stxtPlaceOfReceipt";
            this.stxtPlaceOfReceipt.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtPlaceOfReceipt.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlaceOfReceipt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlaceOfReceipt.Size = new System.Drawing.Size(101, 21);
            this.stxtPlaceOfReceipt.TabIndex = 9;
            this.stxtPlaceOfReceipt.Tag = null;
            // 
            // txtConsigneeDescription
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtConsigneeDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtConsigneeDescription.Location = new System.Drawing.Point(3, 143);
            this.txtConsigneeDescription.Name = "txtConsigneeDescription";
            this.txtConsigneeDescription.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConsigneeDescription.Properties.ReadOnly = true;
            this.txtConsigneeDescription.Size = new System.Drawing.Size(388, 91);
            this.txtConsigneeDescription.TabIndex = 3;
            // 
            // txtShipperDescription
            // 
            this.txtShipperDescription.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtShipperDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtShipperDescription.Location = new System.Drawing.Point(3, 26);
            this.txtShipperDescription.Name = "txtShipperDescription";
            this.txtShipperDescription.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtShipperDescription.Properties.ReadOnly = true;
            this.txtShipperDescription.Size = new System.Drawing.Size(388, 90);
            this.txtShipperDescription.TabIndex = 1;
            // 
            // stxtFinalDestination
            // 
            this.stxtFinalDestination.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "FinalDestinationID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtFinalDestination.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "FinalDestinationCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtFinalDestination, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtFinalDestination.Location = new System.Drawing.Point(502, 281);
            this.stxtFinalDestination.Name = "stxtFinalDestination";
            this.stxtFinalDestination.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtFinalDestination.Properties.Appearance.Options.UseBackColor = true;
            this.stxtFinalDestination.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtFinalDestination.Size = new System.Drawing.Size(101, 21);
            this.stxtFinalDestination.TabIndex = 20;
            // 
            // txtFinalDestinationName
            // 
            this.txtFinalDestinationName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "FinalDestinationName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFinalDestinationName.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtFinalDestinationName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtFinalDestinationName.Location = new System.Drawing.Point(607, 281);
            this.txtFinalDestinationName.Name = "txtFinalDestinationName";
            this.txtFinalDestinationName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFinalDestinationName.Size = new System.Drawing.Size(231, 21);
            this.txtFinalDestinationName.TabIndex = 21;
            // 
            // txtPlaceOfReceiptName
            // 
            this.txtPlaceOfReceiptName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PlaceOfReceiptName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPlaceOfReceiptName.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtPlaceOfReceiptName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPlaceOfReceiptName.Location = new System.Drawing.Point(607, 188);
            this.txtPlaceOfReceiptName.Name = "txtPlaceOfReceiptName";
            this.txtPlaceOfReceiptName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlaceOfReceiptName.Size = new System.Drawing.Size(88, 21);
            this.txtPlaceOfReceiptName.TabIndex = 10;
            // 
            // stxtNotifyParty
            // 
            this.stxtNotifyParty.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "NotifyPartyName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtNotifyParty.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "NotifyPartyID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtNotifyParty, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtNotifyParty.Location = new System.Drawing.Point(64, 237);
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
            this.stxtNotifyParty.Size = new System.Drawing.Size(327, 21);
            this.stxtNotifyParty.TabIndex = 2;
            // 
            // stxtConsignee
            // 
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ConsigneeName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "ConsigneeID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtConsignee, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtConsignee.Location = new System.Drawing.Point(64, 119);
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
            this.stxtConsignee.Size = new System.Drawing.Size(327, 21);
            this.stxtConsignee.TabIndex = 1;
            // 
            // stxtShipper
            // 
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ShipperName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "ShipperID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtShipper, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtShipper.Location = new System.Drawing.Point(64, 2);
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
            this.stxtShipper.Size = new System.Drawing.Size(327, 21);
            this.stxtShipper.TabIndex = 0;
            // 
            // txtFreightDescription
            // 
            this.txtFreightDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "FreightDescription", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFreightDescription.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtFreightDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtFreightDescription.Location = new System.Drawing.Point(607, 249);
            this.txtFreightDescription.Name = "txtFreightDescription";
            this.txtFreightDescription.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFreightDescription.Size = new System.Drawing.Size(229, 66);
            this.txtFreightDescription.TabIndex = 9;
            // 
            // txtMarks
            // 
            this.txtMarks.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Marks", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtMarks, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtMarks.Location = new System.Drawing.Point(607, 25);
            this.txtMarks.Name = "txtMarks";
            this.txtMarks.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMarks.Size = new System.Drawing.Size(229, 92);
            this.txtMarks.TabIndex = 7;
            // 
            // txtAgentDescription
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtAgentDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtAgentDescription.Location = new System.Drawing.Point(502, 23);
            this.txtAgentDescription.Name = "txtAgentDescription";
            this.txtAgentDescription.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAgentDescription.Properties.ReadOnly = true;
            this.txtAgentDescription.Size = new System.Drawing.Size(336, 90);
            this.txtAgentDescription.TabIndex = 4;
            // 
            // txtGoodsDescription
            // 
            this.txtGoodsDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "GoodsDescription", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtGoodsDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dxErrorProvider1.SetIconAlignment(this.txtGoodsDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtGoodsDescription.Location = new System.Drawing.Point(3, 88);
            this.txtGoodsDescription.Name = "txtGoodsDescription";
            this.txtGoodsDescription.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGoodsDescription.Size = new System.Drawing.Size(270, 172);
            this.txtGoodsDescription.TabIndex = 0;
            // 
            // txtCtnQtyInfo
            // 
            this.txtCtnQtyInfo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CtnQtyInfo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCtnQtyInfo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCtnQtyInfo.Location = new System.Drawing.Point(607, 144);
            this.txtCtnQtyInfo.Name = "txtCtnQtyInfo";
            this.txtCtnQtyInfo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCtnQtyInfo.Size = new System.Drawing.Size(229, 76);
            this.txtCtnQtyInfo.TabIndex = 8;
            // 
            // stxtRefNo
            // 
            this.stxtRefNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "RefNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtRefNo.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "OceanBookingID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtRefNo.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtRefNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtRefNo.Location = new System.Drawing.Point(60, 2);
            this.stxtRefNo.Name = "stxtRefNo";
            this.stxtRefNo.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtRefNo.Properties.Appearance.Options.UseBackColor = true;
            this.stxtRefNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtRefNo.Size = new System.Drawing.Size(117, 21);
            this.stxtRefNo.TabIndex = 0;
            // 
            // txtPOLCode
            // 
            this.txtPOLCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "POLCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPOLCode.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "POLID", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtPOLCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPOLCode.Location = new System.Drawing.Point(502, 211);
            this.txtPOLCode.Name = "txtPOLCode";
            this.txtPOLCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtPOLCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtPOLCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPOLCode.Size = new System.Drawing.Size(101, 21);
            this.txtPOLCode.TabIndex = 12;
            this.txtPOLCode.Tag = null;
            // 
            // txtPODCode
            // 
            this.txtPODCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PODCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPODCode.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "PODID", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtPODCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPODCode.Location = new System.Drawing.Point(502, 235);
            this.txtPODCode.Name = "txtPODCode";
            this.txtPODCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtPODCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtPODCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPODCode.Size = new System.Drawing.Size(101, 21);
            this.txtPODCode.TabIndex = 15;
            this.txtPODCode.Tag = null;
            // 
            // cmbACIEntryType
            // 
            this.cmbACIEntryType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ACIEntryType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbACIEntryType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbACIEntryType.Location = new System.Drawing.Point(540, 29);
            this.cmbACIEntryType.Name = "cmbACIEntryType";
            this.cmbACIEntryType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbACIEntryType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbACIEntryType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbACIEntryType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbACIEntryType.Size = new System.Drawing.Size(285, 21);
            this.cmbACIEntryType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbACIEntryType.TabIndex = 95;
            // 
            // txtLastPortOfCall
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtLastPortOfCall, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtLastPortOfCall.Location = new System.Drawing.Point(444, 99);
            this.txtLastPortOfCall.Name = "txtLastPortOfCall";
            this.txtLastPortOfCall.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtLastPortOfCall.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtLastPortOfCall.Properties.Appearance.Options.UseBackColor = true;
            this.txtLastPortOfCall.Properties.Appearance.Options.UseForeColor = true;
            this.txtLastPortOfCall.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtLastPortOfCall.Size = new System.Drawing.Size(74, 21);
            this.txtLastPortOfCall.TabIndex = 110;
            this.txtLastPortOfCall.Tag = null;
            this.txtLastPortOfCall.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // txtFirstPortOfCall
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtFirstPortOfCall, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtFirstPortOfCall.Location = new System.Drawing.Point(644, 99);
            this.txtFirstPortOfCall.Name = "txtFirstPortOfCall";
            this.txtFirstPortOfCall.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtFirstPortOfCall.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtFirstPortOfCall.Properties.Appearance.Options.UseBackColor = true;
            this.txtFirstPortOfCall.Properties.Appearance.Options.UseForeColor = true;
            this.txtFirstPortOfCall.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtFirstPortOfCall.Size = new System.Drawing.Size(74, 21);
            this.txtFirstPortOfCall.TabIndex = 113;
            this.txtFirstPortOfCall.Tag = null;
            this.txtFirstPortOfCall.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // txtPOL
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtPOL, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPOL.Location = new System.Drawing.Point(132, 99);
            this.txtPOL.Name = "txtPOL";
            this.txtPOL.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPOL.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtPOL.Properties.Appearance.Options.UseBackColor = true;
            this.txtPOL.Properties.Appearance.Options.UseForeColor = true;
            this.txtPOL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPOL.Size = new System.Drawing.Size(74, 21);
            this.txtPOL.TabIndex = 115;
            this.txtPOL.Tag = null;
            this.txtPOL.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // stxtPlacePay
            // 
            this.stxtPlacePay.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "CollectbyAgentOrderID", true));
            this.stxtPlacePay.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CollectbyAgentNameOrder", true));
            this.stxtPlacePay.EditValue = "";
            this.stxtPlacePay.Enabled = false;
            this.dxErrorProvider1.SetIconAlignment(this.stxtPlacePay, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPlacePay.Location = new System.Drawing.Point(502, 331);
            this.stxtPlacePay.Name = "stxtPlacePay";
            this.stxtPlacePay.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.stxtPlacePay.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlacePay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlacePay.Size = new System.Drawing.Size(124, 21);
            this.stxtPlacePay.TabIndex = 24;
            this.stxtPlacePay.Tag = null;
            // 
            // txtBookNo
            // 
            this.txtBookNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "DeclareNo", true));
            this.txtBookNo.EditValue = "";
            this.txtBookNo.Enabled = false;
            this.dxErrorProvider1.SetIconAlignment(this.txtBookNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtBookNo.Location = new System.Drawing.Point(715, 333);
            this.txtBookNo.Name = "txtBookNo";
            this.txtBookNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBookNo.Size = new System.Drawing.Size(121, 21);
            this.txtBookNo.TabIndex = 32;
            // 
            // stxtVoyage
            // 
            this.stxtVoyage.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bindingSource1, "VesselVoyage", true));
            this.stxtVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "VoyageID", true));
            this.stxtVoyage.EditText = "";
            this.stxtVoyage.EditValue = null;
            this.stxtVoyage.Location = new System.Drawing.Point(502, 141);
            this.stxtVoyage.Name = "stxtVoyage";
            this.stxtVoyage.ReadOnly = false;
            this.stxtVoyage.RefreshButtonToolTip = "刷新以获取与输入相匹配的船名/航次";
            this.stxtVoyage.ShowRefreshButton = true;
            this.stxtVoyage.Size = new System.Drawing.Size(226, 21);
            this.stxtVoyage.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtVoyage.TabIndex = 7;
            this.stxtVoyage.ToolTip = "";
            // 
            // stxtPreVoyage
            // 
            this.stxtPreVoyage.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bindingSource1, "PreVesselVoyage", true));
            this.stxtPreVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "PreVoyageID", true));
            this.stxtPreVoyage.EditText = "";
            this.stxtPreVoyage.EditValue = null;
            this.stxtPreVoyage.Location = new System.Drawing.Point(502, 117);
            this.stxtPreVoyage.Name = "stxtPreVoyage";
            this.stxtPreVoyage.ReadOnly = false;
            this.stxtPreVoyage.RefreshButtonToolTip = "刷新以获取与输入相匹配的船名/航次";
            this.stxtPreVoyage.ShowRefreshButton = true;
            this.stxtPreVoyage.Size = new System.Drawing.Size(226, 21);
            this.stxtPreVoyage.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtPreVoyage.TabIndex = 5;
            this.stxtPreVoyage.ToolTip = "";
            // 
            // dtETA
            // 
            this.dtETA.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ETA", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtETA.EditValue = null;
            this.dtETA.Location = new System.Drawing.Point(738, 235);
            this.dtETA.Name = "dtETA";
            this.dtETA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtETA.Properties.Mask.EditMask = "";
            this.dtETA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtETA.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtETA.Size = new System.Drawing.Size(100, 21);
            this.dtETA.TabIndex = 17;
            // 
            // dtPETD
            // 
            this.dtPETD.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "PreETD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtPETD.EditValue = null;
            this.dtPETD.Location = new System.Drawing.Point(738, 189);
            this.dtPETD.Name = "dtPETD";
            this.dtPETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtPETD.Properties.Mask.EditMask = "";
            this.dtPETD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtPETD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtPETD.Size = new System.Drawing.Size(101, 21);
            this.dtPETD.TabIndex = 11;
            // 
            // chkIsWoodPacking
            // 
            this.chkIsWoodPacking.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsWoodPacking", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsWoodPacking.Location = new System.Drawing.Point(421, 4);
            this.chkIsWoodPacking.Name = "chkIsWoodPacking";
            this.chkIsWoodPacking.Properties.Caption = "Wood Packing";
            this.chkIsWoodPacking.Size = new System.Drawing.Size(144, 19);
            this.chkIsWoodPacking.TabIndex = 6;
            // 
            // labIssueBy
            // 
            this.labIssueBy.Location = new System.Drawing.Point(4, 6);
            this.labIssueBy.Name = "labIssueBy";
            this.labIssueBy.Size = new System.Drawing.Size(41, 14);
            this.labIssueBy.TabIndex = 6;
            this.labIssueBy.Text = "IssueBy";
            // 
            // labReleaseType
            // 
            this.labReleaseType.Location = new System.Drawing.Point(411, 6);
            this.labReleaseType.Name = "labReleaseType";
            this.labReleaseType.Size = new System.Drawing.Size(69, 14);
            this.labReleaseType.TabIndex = 6;
            this.labReleaseType.Text = "ReleaseType";
            // 
            // labIssueDate
            // 
            this.labIssueDate.Location = new System.Drawing.Point(362, 6);
            this.labIssueDate.Name = "labIssueDate";
            this.labIssueDate.Size = new System.Drawing.Size(58, 14);
            this.labIssueDate.TabIndex = 0;
            this.labIssueDate.Text = "Issue Date";
            // 
            // labReleaseDate
            // 
            this.labReleaseDate.Location = new System.Drawing.Point(636, 4);
            this.labReleaseDate.Name = "labReleaseDate";
            this.labReleaseDate.Size = new System.Drawing.Size(71, 14);
            this.labReleaseDate.TabIndex = 0;
            this.labReleaseDate.Text = "Release Date";
            // 
            // labChecker
            // 
            this.labChecker.Location = new System.Drawing.Point(3, 53);
            this.labChecker.Name = "labChecker";
            this.labChecker.Size = new System.Drawing.Size(44, 14);
            this.labChecker.TabIndex = 0;
            this.labChecker.Text = "Checker";
            // 
            // labMBLNo
            // 
            this.labMBLNo.Location = new System.Drawing.Point(193, 5);
            this.labMBLNo.Name = "labMBLNo";
            this.labMBLNo.Size = new System.Drawing.Size(39, 14);
            this.labMBLNo.TabIndex = 1;
            this.labMBLNo.Text = "MBLNO";
            // 
            // labRefNo
            // 
            this.labRefNo.Location = new System.Drawing.Point(3, 5);
            this.labRefNo.Name = "labRefNo";
            this.labRefNo.Size = new System.Drawing.Size(33, 14);
            this.labRefNo.TabIndex = 0;
            this.labRefNo.Text = "RefNo";
            // 
            // labIssuePlace
            // 
            this.labIssuePlace.Location = new System.Drawing.Point(165, 6);
            this.labIssuePlace.Name = "labIssuePlace";
            this.labIssuePlace.Size = new System.Drawing.Size(56, 14);
            this.labIssuePlace.TabIndex = 0;
            this.labIssuePlace.Text = "IssuePlace";
            // 
            // labHBlNo
            // 
            this.labHBlNo.Location = new System.Drawing.Point(3, 28);
            this.labHBlNo.Name = "labHBlNo";
            this.labHBlNo.Size = new System.Drawing.Size(17, 14);
            this.labHBlNo.TabIndex = 0;
            this.labHBlNo.Text = "NO";
            // 
            // labPaymentTerm
            // 
            this.labPaymentTerm.Location = new System.Drawing.Point(632, 309);
            this.labPaymentTerm.Name = "labPaymentTerm";
            this.labPaymentTerm.Size = new System.Drawing.Size(77, 14);
            this.labPaymentTerm.TabIndex = 11;
            this.labPaymentTerm.Text = "PaymentTerm";
            // 
            // labPlaceOfReceipt
            // 
            this.labPlaceOfReceipt.Location = new System.Drawing.Point(411, 192);
            this.labPlaceOfReceipt.Name = "labPlaceOfReceipt";
            this.labPlaceOfReceipt.Size = new System.Drawing.Size(82, 14);
            this.labPlaceOfReceipt.TabIndex = 5;
            this.labPlaceOfReceipt.Text = "PlaceOfReceipt";
            // 
            // labPlaceOfDelivery
            // 
            this.labPlaceOfDelivery.Location = new System.Drawing.Point(411, 262);
            this.labPlaceOfDelivery.Name = "labPlaceOfDelivery";
            this.labPlaceOfDelivery.Size = new System.Drawing.Size(83, 14);
            this.labPlaceOfDelivery.TabIndex = 5;
            this.labPlaceOfDelivery.Text = "PlaceOfDelivery";
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(411, 238);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(24, 14);
            this.labPOD.TabIndex = 0;
            this.labPOD.Text = "POD";
            // 
            // labPOL2
            // 
            this.labPOL2.Location = new System.Drawing.Point(411, 215);
            this.labPOL2.Name = "labPOL2";
            this.labPOL2.Size = new System.Drawing.Size(22, 14);
            this.labPOL2.TabIndex = 0;
            this.labPOL2.Text = "POL";
            // 
            // labETA
            // 
            this.labETA.Location = new System.Drawing.Point(700, 238);
            this.labETA.Name = "labETA";
            this.labETA.Size = new System.Drawing.Size(23, 14);
            this.labETA.TabIndex = 0;
            this.labETA.Text = "ETA";
            // 
            // labETD2
            // 
            this.labETD2.Location = new System.Drawing.Point(700, 191);
            this.labETD2.Name = "labETD2";
            this.labETD2.Size = new System.Drawing.Size(23, 14);
            this.labETD2.TabIndex = 0;
            this.labETD2.Text = "ETD";
            // 
            // labVoyage
            // 
            this.labVoyage.Location = new System.Drawing.Point(411, 145);
            this.labVoyage.Name = "labVoyage";
            this.labVoyage.Size = new System.Drawing.Size(41, 14);
            this.labVoyage.TabIndex = 0;
            this.labVoyage.Text = "Voyage";
            // 
            // labPreVoyage
            // 
            this.labPreVoyage.Location = new System.Drawing.Point(411, 121);
            this.labPreVoyage.Name = "labPreVoyage";
            this.labPreVoyage.Size = new System.Drawing.Size(59, 14);
            this.labPreVoyage.TabIndex = 0;
            this.labPreVoyage.Text = "PreVoyage";
            // 
            // labTransportClause
            // 
            this.labTransportClause.Location = new System.Drawing.Point(411, 309);
            this.labTransportClause.Name = "labTransportClause";
            this.labTransportClause.Size = new System.Drawing.Size(87, 14);
            this.labTransportClause.TabIndex = 6;
            this.labTransportClause.Text = "TransportClause";
            // 
            // labFinalDestination
            // 
            this.labFinalDestination.Location = new System.Drawing.Point(411, 285);
            this.labFinalDestination.Name = "labFinalDestination";
            this.labFinalDestination.Size = new System.Drawing.Size(84, 14);
            this.labFinalDestination.TabIndex = 0;
            this.labFinalDestination.Text = "FinalDestination";
            // 
            // labNotifyParty
            // 
            this.labNotifyParty.Location = new System.Drawing.Point(3, 239);
            this.labNotifyParty.Name = "labNotifyParty";
            this.labNotifyParty.Size = new System.Drawing.Size(60, 14);
            this.labNotifyParty.TabIndex = 0;
            this.labNotifyParty.Text = "NotifyParty";
            // 
            // labConsignee
            // 
            this.labConsignee.Location = new System.Drawing.Point(3, 121);
            this.labConsignee.Name = "labConsignee";
            this.labConsignee.Size = new System.Drawing.Size(56, 14);
            this.labConsignee.TabIndex = 0;
            this.labConsignee.Text = "Consignee";
            // 
            // labAgent
            // 
            this.labAgent.Location = new System.Drawing.Point(411, 6);
            this.labAgent.Name = "labAgent";
            this.labAgent.Size = new System.Drawing.Size(34, 14);
            this.labAgent.TabIndex = 0;
            this.labAgent.Text = "Agent";
            // 
            // labShipper
            // 
            this.labShipper.Location = new System.Drawing.Point(3, 6);
            this.labShipper.Name = "labShipper";
            this.labShipper.Size = new System.Drawing.Size(41, 14);
            this.labShipper.TabIndex = 0;
            this.labShipper.Text = "Shipper";
            // 
            // labDescriptionOfGoods
            // 
            this.labDescriptionOfGoods.Location = new System.Drawing.Point(297, 6);
            this.labDescriptionOfGoods.Name = "labDescriptionOfGoods";
            this.labDescriptionOfGoods.Size = new System.Drawing.Size(115, 14);
            this.labDescriptionOfGoods.TabIndex = 0;
            this.labDescriptionOfGoods.Text = "Description Of Goods";
            // 
            // labQuantity
            // 
            this.labQuantity.Location = new System.Drawing.Point(2, 35);
            this.labQuantity.Name = "labQuantity";
            this.labQuantity.Size = new System.Drawing.Size(47, 14);
            this.labQuantity.TabIndex = 40;
            this.labQuantity.Text = "Quantity";
            // 
            // labWeight
            // 
            this.labWeight.Location = new System.Drawing.Point(2, 58);
            this.labWeight.Name = "labWeight";
            this.labWeight.Size = new System.Drawing.Size(40, 14);
            this.labWeight.TabIndex = 38;
            this.labWeight.Text = "Weight";
            // 
            // labMeasurement
            // 
            this.labMeasurement.Location = new System.Drawing.Point(2, 81);
            this.labMeasurement.Name = "labMeasurement";
            this.labMeasurement.Size = new System.Drawing.Size(74, 14);
            this.labMeasurement.TabIndex = 39;
            this.labMeasurement.Text = "Measurement";
            // 
            // btnContainer
            // 
            this.btnContainer.Location = new System.Drawing.Point(3, 5);
            this.btnContainer.Name = "btnContainer";
            this.btnContainer.Size = new System.Drawing.Size(252, 23);
            this.btnContainer.TabIndex = 0;
            this.btnContainer.Text = "Container";
            this.btnContainer.Click += new System.EventHandler(this.btnContainer_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtGoodsDescription);
            this.groupBox2.Controls.Add(this.txtIsWoodPacking);
            this.groupBox2.Controls.Add(this.txtCtnQty);
            this.groupBox2.Location = new System.Drawing.Point(292, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(276, 296);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // txtIsWoodPacking
            // 
            this.txtIsWoodPacking.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "WoodPacking", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtIsWoodPacking.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtIsWoodPacking.EditValue = "WOOD PACKAGING MATERIAL IS\r\nUSED IN THE ";
            this.txtIsWoodPacking.Location = new System.Drawing.Point(3, 260);
            this.txtIsWoodPacking.MenuManager = this.barManager1;
            this.txtIsWoodPacking.Name = "txtIsWoodPacking";
            this.txtIsWoodPacking.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIsWoodPacking.Size = new System.Drawing.Size(270, 33);
            this.txtIsWoodPacking.TabIndex = 1;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.barSavingTools});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave,
            this.barSaveAs,
            this.barPrintBL,
            this.barCheck,
            this.barCheckDone,
            this.barClose,
            this.barRefresh,
            this.barSubCheck,
            this.barSubPrint,
            this.barBill,
            this.barReplyAgent,
            this.barSubEDI,
            this.barAMS,
            this.barACI,
            this.barAMSACI,
            this.barAMSISF,
            this.barISF,
            this.barAMSACIISF,
            this.btnAMSandACI,
            this.barbl,
            this.barblCHS,
            this.barBlENG,
            this.barCancel,
            this.barlabMessage,
            this.barSavingClose,
            this.barlabSeconds,
            this.barWebEdi,
            this.barConfirmedAMS});
            this.barManager1.MaxItemId = 37;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSavingClose, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSaveAs, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubPrint, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barReplyAgent, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubCheck, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubEDI, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barWebEdi),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBill, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barbl, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
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
            this.barSavingClose.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.SaveClose;
            this.barSavingClose.Id = 33;
            this.barSavingClose.Name = "barSavingClose";
            // 
            // barSaveAs
            // 
            this.barSaveAs.Caption = "Save&As";
            this.barSaveAs.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Save_Blue_16;
            this.barSaveAs.Id = 1;
            this.barSaveAs.Name = "barSaveAs";
            this.barSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSaveAs_ItemClick);
            // 
            // barSubPrint
            // 
            this.barSubPrint.Caption = "Print";
            this.barSubPrint.Id = 12;
            this.barSubPrint.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrintBL, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barSubPrint.Name = "barSubPrint";
            // 
            // barPrintBL
            // 
            this.barPrintBL.Caption = "&Print";
            this.barPrintBL.Id = 3;
            this.barPrintBL.Name = "barPrintBL";
            this.barPrintBL.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barReplyAgent
            // 
            this.barReplyAgent.Caption = "&ReplyAgent";
            this.barReplyAgent.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Transfer_16;
            this.barReplyAgent.Id = 15;
            this.barReplyAgent.Name = "barReplyAgent";
            this.barReplyAgent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barReplyAgent_ItemClick);
            // 
            // barSubCheck
            // 
            this.barSubCheck.Caption = "Check";
            this.barSubCheck.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Edit_16;
            this.barSubCheck.Id = 10;
            this.barSubCheck.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCheck, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCheckDone, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barSubCheck.Name = "barSubCheck";
            // 
            // barCheck
            // 
            this.barCheck.Caption = "Chec&k";
            this.barCheck.Id = 5;
            this.barCheck.Name = "barCheck";
            this.barCheck.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheck_ItemClick);
            // 
            // barCheckDone
            // 
            this.barCheckDone.Caption = "&Done";
            this.barCheckDone.Id = 6;
            this.barCheckDone.Name = "barCheckDone";
            this.barCheckDone.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckDone_ItemClick);
            // 
            // barSubEDI
            // 
            this.barSubEDI.Caption = "EDI";
            this.barSubEDI.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Transfer_16;
            this.barSubEDI.Id = 16;
            this.barSubEDI.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAMS),
            new DevExpress.XtraBars.LinkPersistInfo(this.barACI),
            new DevExpress.XtraBars.LinkPersistInfo(this.barISF),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAMSandACI),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAMSISF),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAMSACIISF),
            new DevExpress.XtraBars.LinkPersistInfo(this.barConfirmedAMS)});
            this.barSubEDI.Name = "barSubEDI";
            // 
            // barAMS
            // 
            this.barAMS.Caption = "AMS";
            this.barAMS.Id = 17;
            this.barAMS.Name = "barAMS";
            this.barAMS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAMS_ItemClick);
            // 
            // barACI
            // 
            this.barACI.Caption = "ACI";
            this.barACI.Id = 18;
            this.barACI.Name = "barACI";
            this.barACI.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barACI_ItemClick);
            // 
            // barISF
            // 
            this.barISF.Caption = "ISF";
            this.barISF.Id = 22;
            this.barISF.Name = "barISF";
            this.barISF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barISF_ItemClick);
            // 
            // btnAMSandACI
            // 
            this.btnAMSandACI.Caption = "AMS&&ACI";
            this.btnAMSandACI.Id = 24;
            this.btnAMSandACI.Name = "btnAMSandACI";
            this.btnAMSandACI.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAMSandACI_ItemClick);
            // 
            // barAMSISF
            // 
            this.barAMSISF.Caption = "AMS&&ISF";
            this.barAMSISF.Id = 20;
            this.barAMSISF.Name = "barAMSISF";
            this.barAMSISF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAMSandISF_ItemClick);
            // 
            // barAMSACIISF
            // 
            this.barAMSACIISF.Caption = "AMS&&ACI&&ISF";
            this.barAMSACIISF.Id = 23;
            this.barAMSACIISF.Name = "barAMSACIISF";
            this.barAMSACIISF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAMSAndACIAndISF_ItemClick);
            // 
            // barWebEdi
            // 
            this.barWebEdi.Caption = "WEBAMS";
            this.barWebEdi.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Transfer_16;
            this.barWebEdi.Id = 35;
            this.barWebEdi.Name = "barWebEdi";
            this.barWebEdi.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barWebEdi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barWebEdi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barWebEdi_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Refresh_16;
            this.barRefresh.Hint = "&Refresh";
            this.barRefresh.Id = 9;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barBill
            // 
            this.barBill.Caption = "&Bill";
            this.barBill.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.calculator;
            this.barBill.Id = 14;
            this.barBill.Name = "barBill";
            this.barBill.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu;
            this.barBill.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBill_ItemClick);
            // 
            // barbl
            // 
            this.barbl.Caption = "Mail BL Copy To Customer";
            this.barbl.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.mail;
            this.barbl.Id = 27;
            this.barbl.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barblCHS),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBlENG)});
            this.barbl.Name = "barbl";
            // 
            // barblCHS
            // 
            this.barblCHS.Caption = "Mail BL Copy To Customer (CHS)";
            this.barblCHS.Id = 28;
            this.barblCHS.Name = "barblCHS";
            this.barblCHS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barblCHS_ItemClick);
            // 
            // barBlENG
            // 
            this.barBlENG.Caption = "Mail BL Copy To Customer (ENG)";
            this.barBlENG.Id = 29;
            this.barBlENG.Name = "barBlENG";
            this.barBlENG.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBlENG_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Left_D_16;
            this.barClose.Id = 8;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barSavingTools
            // 
            this.barSavingTools.BarName = "barSavingTools";
            this.barSavingTools.DockCol = 0;
            this.barSavingTools.DockRow = 1;
            this.barSavingTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barSavingTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCancel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barlabMessage, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barlabSeconds)});
            this.barSavingTools.OptionsBar.AllowRename = true;
            this.barSavingTools.OptionsBar.UseWholeRow = true;
            this.barSavingTools.Text = "barSavingTools";
            // 
            // barCancel
            // 
            this.barCancel.Caption = "Cancel";
            this.barCancel.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Cancel_16;
            this.barCancel.Id = 31;
            this.barCancel.Name = "barCancel";
            // 
            // barlabMessage
            // 
            this.barlabMessage.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barlabMessage.Caption = "Message";
            this.barlabMessage.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.mail;
            this.barlabMessage.Id = 32;
            this.barlabMessage.Name = "barlabMessage";
            this.barlabMessage.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barlabSeconds
            // 
            this.barlabSeconds.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barlabSeconds.Caption = "0s";
            this.barlabSeconds.Id = 34;
            this.barlabSeconds.Name = "barlabSeconds";
            this.barlabSeconds.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1400, 53);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 1377);
            this.barDockControlBottom.Size = new System.Drawing.Size(1400, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 53);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 1324);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1400, 53);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 1324);
            // 
            // barAMSACI
            // 
            this.barAMSACI.Id = 21;
            this.barAMSACI.Name = "barAMSACI";
            // 
            // txtCtnQty
            // 
            this.txtCtnQty.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtCtnQty.EditValue = "SHIPPER\'S LOAD COUNT & SEAL(0*0)";
            this.txtCtnQty.Location = new System.Drawing.Point(3, 18);
            this.txtCtnQty.MenuManager = this.barManager1;
            this.txtCtnQty.Name = "txtCtnQty";
            this.txtCtnQty.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCtnQty.Properties.ReadOnly = true;
            this.txtCtnQty.Size = new System.Drawing.Size(270, 70);
            this.txtCtnQty.TabIndex = 0;
            // 
            // labFreightDescription
            // 
            this.labFreightDescription.Location = new System.Drawing.Point(607, 229);
            this.labFreightDescription.Name = "labFreightDescription";
            this.labFreightDescription.Size = new System.Drawing.Size(98, 14);
            this.labFreightDescription.TabIndex = 0;
            this.labFreightDescription.Text = "FreightDescription";
            // 
            // labContainerDescription
            // 
            this.labContainerDescription.Location = new System.Drawing.Point(4, 104);
            this.labContainerDescription.Name = "labContainerDescription";
            this.labContainerDescription.Size = new System.Drawing.Size(49, 14);
            this.labContainerDescription.TabIndex = 0;
            this.labContainerDescription.Text = "CTN Info";
            // 
            // labCtnQtyInfo
            // 
            this.labCtnQtyInfo.Location = new System.Drawing.Point(607, 125);
            this.labCtnQtyInfo.Name = "labCtnQtyInfo";
            this.labCtnQtyInfo.Size = new System.Drawing.Size(73, 14);
            this.labCtnQtyInfo.TabIndex = 0;
            this.labCtnQtyInfo.Text = "CTN Qty Info";
            // 
            // labMarks
            // 
            this.labMarks.Location = new System.Drawing.Point(607, 5);
            this.labMarks.Name = "labMarks";
            this.labMarks.Size = new System.Drawing.Size(30, 14);
            this.labMarks.TabIndex = 0;
            this.labMarks.Text = "Marks";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabPageBase;
            this.xtraTabControl1.Size = new System.Drawing.Size(878, 1348);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageBase,
            this.tabPageAMS});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // tabPageBase
            // 
            this.tabPageBase.Controls.Add(this.navBarControl1);
            this.tabPageBase.Name = "tabPageBase";
            this.tabPageBase.Size = new System.Drawing.Size(848, 1341);
            this.tabPageBase.Text = "Base";
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarBaseInfo;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer4);
            this.navBarControl1.Controls.Add(this.navBarControlContainerContact);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarBaseInfo,
            this.navBarBLInfo,
            this.navBarCargo,
            this.navBarIssueInfo,
            this.navBarContact});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 783;
            this.navBarControl1.Size = new System.Drawing.Size(848, 1341);
            this.navBarControl1.TabIndex = 3;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarBaseInfo
            // 
            this.navBarBaseInfo.Caption = "Base Info";
            this.navBarBaseInfo.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarBaseInfo.Expanded = true;
            this.navBarBaseInfo.GroupClientHeight = 77;
            this.navBarBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBaseInfo.Name = "navBarBaseInfo";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.panel1);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(844, 75);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel1.Controls.Add(this.mcmbBookingParty);
            this.panel1.Controls.Add(this.labelControl30);
            this.panel1.Controls.Add(this.txtScac);
            this.panel1.Controls.Add(this.txtTelexNo);
            this.panel1.Controls.Add(this.labScac);
            this.panel1.Controls.Add(this.txtRleaseBy);
            this.panel1.Controls.Add(this.labTelexNo);
            this.panel1.Controls.Add(this.labReleaseBy);
            this.panel1.Controls.Add(this.txtNo);
            this.panel1.Controls.Add(this.cmbIssueType);
            this.panel1.Controls.Add(this.labType);
            this.panel1.Controls.Add(this.stxtRefNo);
            this.panel1.Controls.Add(this.dteReleaseDate);
            this.panel1.Controls.Add(this.cmbReleaseType);
            this.panel1.Controls.Add(this.chkToAgent);
            this.panel1.Controls.Add(this.labReleaseType);
            this.panel1.Controls.Add(this.labHBlNo);
            this.panel1.Controls.Add(this.labReleaseDate);
            this.panel1.Controls.Add(this.labRefNo);
            this.panel1.Controls.Add(this.stxtChecker);
            this.panel1.Controls.Add(this.labMBLNo);
            this.panel1.Controls.Add(this.labChecker);
            this.panel1.Controls.Add(this.cmbMBLNO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(844, 75);
            this.panel1.TabIndex = 0;
            // 
            // mcmbBookingParty
            // 
            this.mcmbBookingParty.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bindingSource1, "BookingPartyName", true));
            this.mcmbBookingParty.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "BookingPartyID", true));
            this.mcmbBookingParty.EditText = "";
            this.mcmbBookingParty.EditValue = null;
            this.mcmbBookingParty.Location = new System.Drawing.Point(502, 49);
            this.mcmbBookingParty.Name = "mcmbBookingParty";
            this.mcmbBookingParty.ReadOnly = false;
            this.mcmbBookingParty.RefreshButtonToolTip = "";
            this.mcmbBookingParty.ShowRefreshButton = false;
            this.mcmbBookingParty.Size = new System.Drawing.Size(124, 21);
            this.mcmbBookingParty.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbBookingParty.TabIndex = 7;
            this.mcmbBookingParty.ToolTip = "";
            // 
            // labelControl30
            // 
            this.labelControl30.Location = new System.Drawing.Point(411, 53);
            this.labelControl30.Name = "labelControl30";
            this.labelControl30.Size = new System.Drawing.Size(71, 14);
            this.labelControl30.TabIndex = 807;
            this.labelControl30.Text = "BookingParty";
            // 
            // txtScac
            // 
            this.txtScac.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ScacCode", true));
            this.txtScac.Location = new System.Drawing.Point(715, 49);
            this.txtScac.Name = "txtScac";
            this.txtScac.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtScac.Size = new System.Drawing.Size(123, 21);
            this.txtScac.TabIndex = 14;
            this.txtScac.TabStop = false;
            // 
            // txtTelexNo
            // 
            this.txtTelexNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "TelexNo", true));
            this.txtTelexNo.Location = new System.Drawing.Point(715, 25);
            this.txtTelexNo.Name = "txtTelexNo";
            this.txtTelexNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTelexNo.Properties.ReadOnly = true;
            this.txtTelexNo.Size = new System.Drawing.Size(123, 21);
            this.txtTelexNo.TabIndex = 14;
            this.txtTelexNo.TabStop = false;
            // 
            // labScac
            // 
            this.labScac.Location = new System.Drawing.Point(636, 52);
            this.labScac.Name = "labScac";
            this.labScac.Size = new System.Drawing.Size(57, 14);
            this.labScac.TabIndex = 12;
            this.labScac.Text = "SCACCode";
            // 
            // txtRleaseBy
            // 
            this.txtRleaseBy.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ReleaseBy", true));
            this.txtRleaseBy.Location = new System.Drawing.Point(502, 25);
            this.txtRleaseBy.Name = "txtRleaseBy";
            this.txtRleaseBy.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRleaseBy.Properties.ReadOnly = true;
            this.txtRleaseBy.Size = new System.Drawing.Size(124, 21);
            this.txtRleaseBy.TabIndex = 15;
            this.txtRleaseBy.TabStop = false;
            // 
            // labTelexNo
            // 
            this.labTelexNo.Location = new System.Drawing.Point(635, 28);
            this.labTelexNo.Name = "labTelexNo";
            this.labTelexNo.Size = new System.Drawing.Size(45, 14);
            this.labTelexNo.TabIndex = 12;
            this.labTelexNo.Text = "TelexNo";
            // 
            // labReleaseBy
            // 
            this.labReleaseBy.Location = new System.Drawing.Point(411, 28);
            this.labReleaseBy.Name = "labReleaseBy";
            this.labReleaseBy.Size = new System.Drawing.Size(54, 14);
            this.labReleaseBy.TabIndex = 13;
            this.labReleaseBy.Text = "ReleaseBy";
            // 
            // txtNo
            // 
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "No", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtNo.Location = new System.Drawing.Point(60, 25);
            this.txtNo.MenuManager = this.barManager1;
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNo.Properties.ReadOnly = true;
            this.txtNo.Size = new System.Drawing.Size(117, 21);
            this.txtNo.TabIndex = 2;
            this.txtNo.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtNo_EditValueChanging);
            // 
            // cmbIssueType
            // 
            this.cmbIssueType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IssueType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbIssueType.Location = new System.Drawing.Point(249, 25);
            this.cmbIssueType.Name = "cmbIssueType";
            this.cmbIssueType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbIssueType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbIssueType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbIssueType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbIssueType.Size = new System.Drawing.Size(142, 21);
            this.cmbIssueType.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbIssueType.TabIndex = 3;
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(193, 28);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(28, 14);
            this.labType.TabIndex = 8;
            this.labType.Text = "Type";
            // 
            // chkToAgent
            // 
            this.chkToAgent.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsToAgent", true));
            this.chkToAgent.EditValue = true;
            this.chkToAgent.Location = new System.Drawing.Point(312, 51);
            this.chkToAgent.Name = "chkToAgent";
            this.chkToAgent.Properties.Caption = "ToAgent";
            this.chkToAgent.Size = new System.Drawing.Size(79, 19);
            this.chkToAgent.TabIndex = 5;
            this.chkToAgent.ToolTip = "代理是否可以下载该业务";
            this.chkToAgent.Click += new System.EventHandler(this.chkToAgent_Click);
            // 
            // cmbMBLNO
            // 
            this.cmbMBLNO.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "MBLNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbMBLNO.EditValue = "";
            this.cmbMBLNO.Location = new System.Drawing.Point(250, 1);
            this.cmbMBLNO.Name = "cmbMBLNO";
            this.cmbMBLNO.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbMBLNO.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMBLNO.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMBLNO.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cmbMBLNO.Size = new System.Drawing.Size(141, 21);
            this.cmbMBLNO.TabIndex = 1;
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.panel2);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(844, 362);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel2.Controls.Add(this.txtBookNo);
            this.panel2.Controls.Add(this.chkIsBook);
            this.panel2.Controls.Add(this.labGateInDate);
            this.panel2.Controls.Add(this.dteGateInDate);
            this.panel2.Controls.Add(this.stxtPlacePay);
            this.panel2.Controls.Add(this.chkThirdPlacePay);
            this.panel2.Controls.Add(this.stxtAgent);
            this.panel2.Controls.Add(this.chkShowVoyage);
            this.panel2.Controls.Add(this.chkShowPreVoyage);
            this.panel2.Controls.Add(this.cmbPaymentTerm);
            this.panel2.Controls.Add(this.labPlaceOfDelivery);
            this.panel2.Controls.Add(this.stxtShipper);
            this.panel2.Controls.Add(this.labPOD);
            this.panel2.Controls.Add(this.stxtPreVoyage);
            this.panel2.Controls.Add(this.dtETA);
            this.panel2.Controls.Add(this.stxtVoyage);
            this.panel2.Controls.Add(this.labETA);
            this.panel2.Controls.Add(this.labPreVoyage);
            this.panel2.Controls.Add(this.stxtPlaceOfDelivery);
            this.panel2.Controls.Add(this.labPOL2);
            this.panel2.Controls.Add(this.labPaymentTerm);
            this.panel2.Controls.Add(this.txtPODName);
            this.panel2.Controls.Add(this.labShipper);
            this.panel2.Controls.Add(this.txtPlaceOfDeliveryName);
            this.panel2.Controls.Add(this.labPlaceOfReceipt);
            this.panel2.Controls.Add(this.dtETD);
            this.panel2.Controls.Add(this.dtPETD);
            this.panel2.Controls.Add(this.labAgent);
            this.panel2.Controls.Add(this.labelControl1);
            this.panel2.Controls.Add(this.labETD2);
            this.panel2.Controls.Add(this.labVoyage);
            this.panel2.Controls.Add(this.labConsignee);
            this.panel2.Controls.Add(this.txtPOLName);
            this.panel2.Controls.Add(this.cmbTransportClause);
            this.panel2.Controls.Add(this.stxtConsignee);
            this.panel2.Controls.Add(this.labTransportClause);
            this.panel2.Controls.Add(this.labNotifyParty);
            this.panel2.Controls.Add(this.txtNotifyPartyDescription);
            this.panel2.Controls.Add(this.stxtNotifyParty);
            this.panel2.Controls.Add(this.stxtPlaceOfReceipt);
            this.panel2.Controls.Add(this.txtPlaceOfReceiptName);
            this.panel2.Controls.Add(this.txtConsigneeDescription);
            this.panel2.Controls.Add(this.txtFinalDestinationName);
            this.panel2.Controls.Add(this.txtAgentDescription);
            this.panel2.Controls.Add(this.labFinalDestination);
            this.panel2.Controls.Add(this.txtShipperDescription);
            this.panel2.Controls.Add(this.stxtFinalDestination);
            this.panel2.Controls.Add(this.txtPOLCode);
            this.panel2.Controls.Add(this.txtPODCode);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(844, 360);
            this.panel2.TabIndex = 1;
            // 
            // chkIsBook
            // 
            this.chkIsBook.Location = new System.Drawing.Point(632, 333);
            this.chkIsBook.Name = "chkIsBook";
            this.chkIsBook.Properties.Caption = "是否报关";
            this.chkIsBook.Size = new System.Drawing.Size(77, 19);
            this.chkIsBook.TabIndex = 31;
            this.chkIsBook.Tag = "";
            // 
            // labGateInDate
            // 
            this.labGateInDate.Location = new System.Drawing.Point(702, 168);
            this.labGateInDate.Name = "labGateInDate";
            this.labGateInDate.Size = new System.Drawing.Size(20, 14);
            this.labGateInDate.TabIndex = 30;
            this.labGateInDate.Text = "GID";
            // 
            // dteGateInDate
            // 
            this.dteGateInDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "GateInDate", true));
            this.dteGateInDate.EditValue = null;
            this.dteGateInDate.Location = new System.Drawing.Point(738, 165);
            this.dteGateInDate.Name = "dteGateInDate";
            this.dteGateInDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteGateInDate.Properties.Mask.EditMask = "";
            this.dteGateInDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteGateInDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteGateInDate.Size = new System.Drawing.Size(101, 21);
            this.dteGateInDate.TabIndex = 8;
            // 
            // chkThirdPlacePay
            // 
            this.chkThirdPlacePay.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsThirdPlacePayOrder", true));
            this.chkThirdPlacePay.Location = new System.Drawing.Point(409, 333);
            this.chkThirdPlacePay.MenuManager = this.barManager1;
            this.chkThirdPlacePay.Name = "chkThirdPlacePay";
            this.chkThirdPlacePay.Properties.Caption = "Third Pay.";
            this.chkThirdPlacePay.Size = new System.Drawing.Size(92, 19);
            this.chkThirdPlacePay.TabIndex = 27;
            this.chkThirdPlacePay.ToolTip = "Third Payment";
            this.chkThirdPlacePay.CheckedChanged += new System.EventHandler(this.chkThirdPlacePay_CheckedChanged);
            // 
            // stxtAgent
            // 
            this.stxtAgent.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "AgentID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtAgent.DataSource = null;
            this.stxtAgent.DisplayMember = "EName";
            this.stxtAgent.EditValue = null;
            this.stxtAgent.Location = new System.Drawing.Point(502, 1);
            this.stxtAgent.Margin = new System.Windows.Forms.Padding(0);
            this.stxtAgent.Name = "stxtAgent";
            this.stxtAgent.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Bottom;
            this.stxtAgent.Size = new System.Drawing.Size(336, 21);
            this.stxtAgent.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtAgent.TabIndex = 3;
            this.stxtAgent.Tag = null;
            this.stxtAgent.ValueMember = "ID";
            // 
            // chkShowVoyage
            // 
            this.chkShowVoyage.Location = new System.Drawing.Point(736, 140);
            this.chkShowVoyage.Name = "chkShowVoyage";
            this.chkShowVoyage.Properties.Caption = "Show";
            this.chkShowVoyage.Size = new System.Drawing.Size(60, 19);
            this.chkShowVoyage.TabIndex = 8;
            this.chkShowVoyage.Tag = "";
            this.chkShowVoyage.CheckedChanged += new System.EventHandler(this.chkShowVoyage_CheckedChanged);
            // 
            // chkShowPreVoyage
            // 
            this.chkShowPreVoyage.Location = new System.Drawing.Point(736, 120);
            this.chkShowPreVoyage.Name = "chkShowPreVoyage";
            this.chkShowPreVoyage.Properties.Caption = "Show";
            this.chkShowPreVoyage.Size = new System.Drawing.Size(60, 19);
            this.chkShowPreVoyage.TabIndex = 6;
            this.chkShowPreVoyage.CheckedChanged += new System.EventHandler(this.chkShowPreVoyage_CheckedChanged);
            // 
            // dtETD
            // 
            this.dtETD.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ETD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtETD.EditValue = null;
            this.dtETD.Location = new System.Drawing.Point(738, 212);
            this.dtETD.Name = "dtETD";
            this.dtETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtETD.Properties.Mask.EditMask = "";
            this.dtETD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtETD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtETD.Size = new System.Drawing.Size(101, 21);
            this.dtETD.TabIndex = 14;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(700, 215);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(23, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "ETD";
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.panel3);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(844, 317);
            this.navBarGroupControlContainer3.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel3.Controls.Add(this.numMeasurement);
            this.panel3.Controls.Add(this.numWeight);
            this.panel3.Controls.Add(this.numQuantity);
            this.panel3.Controls.Add(this.txtCtnInfo);
            this.panel3.Controls.Add(this.labDescriptionOfGoods);
            this.panel3.Controls.Add(this.labMarks);
            this.panel3.Controls.Add(this.chkIsWoodPacking);
            this.panel3.Controls.Add(this.labQuantity);
            this.panel3.Controls.Add(this.labWeight);
            this.panel3.Controls.Add(this.labMeasurement);
            this.panel3.Controls.Add(this.labCtnQtyInfo);
            this.panel3.Controls.Add(this.cmbQuantityUnit);
            this.panel3.Controls.Add(this.txtMarks);
            this.panel3.Controls.Add(this.cmbMeasurementUnit);
            this.panel3.Controls.Add(this.labContainerDescription);
            this.panel3.Controls.Add(this.cmbWeightUnit);
            this.panel3.Controls.Add(this.txtCtnQtyInfo);
            this.panel3.Controls.Add(this.btnContainer);
            this.panel3.Controls.Add(this.labFreightDescription);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.txtFreightDescription);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(844, 317);
            this.panel3.TabIndex = 2;
            // 
            // numMeasurement
            // 
            this.numMeasurement.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Measurement", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numMeasurement.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numMeasurement.Location = new System.Drawing.Point(79, 78);
            this.numMeasurement.Name = "numMeasurement";
            this.numMeasurement.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMeasurement.Properties.Mask.EditMask = "F3";
            this.numMeasurement.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numMeasurement.Size = new System.Drawing.Size(94, 21);
            this.numMeasurement.TabIndex = 4;
            // 
            // numWeight
            // 
            this.numWeight.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Weight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numWeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numWeight.Location = new System.Drawing.Point(79, 55);
            this.numWeight.Name = "numWeight";
            this.numWeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numWeight.Properties.Mask.EditMask = "F3";
            this.numWeight.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numWeight.Size = new System.Drawing.Size(94, 21);
            this.numWeight.TabIndex = 2;
            // 
            // numQuantity
            // 
            this.numQuantity.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Quantity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numQuantity.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numQuantity.Location = new System.Drawing.Point(79, 32);
            this.numQuantity.MenuManager = this.barManager1;
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numQuantity.Properties.IsFloatValue = false;
            this.numQuantity.Properties.Mask.EditMask = "N00";
            this.numQuantity.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numQuantity.Size = new System.Drawing.Size(94, 21);
            this.numQuantity.TabIndex = 0;
            // 
            // txtCtnInfo
            // 
            this.txtCtnInfo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ContainerDescription", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCtnInfo.Location = new System.Drawing.Point(2, 124);
            this.txtCtnInfo.MenuManager = this.barManager1;
            this.txtCtnInfo.Name = "txtCtnInfo";
            this.txtCtnInfo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCtnInfo.Properties.ReadOnly = true;
            this.txtCtnInfo.Size = new System.Drawing.Size(252, 191);
            this.txtCtnInfo.TabIndex = 7;
            // 
            // navBarGroupControlContainer4
            // 
            this.navBarGroupControlContainer4.Controls.Add(this.panel4);
            this.navBarGroupControlContainer4.Name = "navBarGroupControlContainer4";
            this.navBarGroupControlContainer4.Size = new System.Drawing.Size(844, 30);
            this.navBarGroupControlContainer4.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel4.Controls.Add(this.mcmbIssueBy);
            this.panel4.Controls.Add(this.stxtIssuePlace);
            this.panel4.Controls.Add(this.labIssueDate);
            this.panel4.Controls.Add(this.labIssuePlace);
            this.panel4.Controls.Add(this.labIssueBy);
            this.panel4.Controls.Add(this.dteIssue);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(844, 30);
            this.panel4.TabIndex = 3;
            // 
            // mcmbIssueBy
            // 
            this.mcmbIssueBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mcmbIssueBy.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bindingSource1, "IssueByName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mcmbIssueBy.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IssueByID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mcmbIssueBy.EditText = "";
            this.mcmbIssueBy.EditValue = null;
            this.mcmbIssueBy.Location = new System.Drawing.Point(64, 3);
            this.mcmbIssueBy.Name = "mcmbIssueBy";
            this.mcmbIssueBy.ReadOnly = false;
            this.mcmbIssueBy.RefreshButtonToolTip = "";
            this.mcmbIssueBy.ShowRefreshButton = false;
            this.mcmbIssueBy.Size = new System.Drawing.Size(79, 21);
            this.mcmbIssueBy.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.mcmbIssueBy.TabIndex = 0;
            this.mcmbIssueBy.ToolTip = "";
            // 
            // navBarControlContainerContact
            // 
            this.navBarControlContainerContact.Name = "navBarControlContainerContact";
            this.navBarControlContainerContact.Size = new System.Drawing.Size(844, 362);
            this.navBarControlContainerContact.TabIndex = 4;
            // 
            // navBarBLInfo
            // 
            this.navBarBLInfo.Caption = "BL Info";
            this.navBarBLInfo.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarBLInfo.Expanded = true;
            this.navBarBLInfo.GroupClientHeight = 364;
            this.navBarBLInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBLInfo.Name = "navBarBLInfo";
            // 
            // navBarCargo
            // 
            this.navBarCargo.Caption = "Cargo";
            this.navBarCargo.ControlContainer = this.navBarGroupControlContainer3;
            this.navBarCargo.Expanded = true;
            this.navBarCargo.GroupClientHeight = 319;
            this.navBarCargo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarCargo.Name = "navBarCargo";
            // 
            // navBarIssueInfo
            // 
            this.navBarIssueInfo.Caption = "Issue Info";
            this.navBarIssueInfo.ControlContainer = this.navBarGroupControlContainer4;
            this.navBarIssueInfo.Expanded = true;
            this.navBarIssueInfo.GroupClientHeight = 32;
            this.navBarIssueInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarIssueInfo.Name = "navBarIssueInfo";
            // 
            // navBarContact
            // 
            this.navBarContact.Caption = "Contact";
            this.navBarContact.ControlContainer = this.navBarControlContainerContact;
            this.navBarContact.Expanded = true;
            this.navBarContact.GroupClientHeight = 364;
            this.navBarContact.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarContact.Name = "navBarContact";
            // 
            // tabPageAMS
            // 
            this.tabPageAMS.Controls.Add(this.panelALL);
            this.tabPageAMS.Name = "tabPageAMS";
            this.tabPageAMS.Size = new System.Drawing.Size(848, 1341);
            this.tabPageAMS.Text = "AMS&&ACI&&ISF";
            // 
            // panelALL
            // 
            this.panelALL.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelALL.Controls.Add(this.panelContainerDetails);
            this.panelALL.Controls.Add(this.groupControl2);
            this.panelALL.Controls.Add(this.panelContent);
            this.panelALL.Controls.Add(this.panelAddAMSACIISF);
            this.panelALL.Controls.Add(this.panelTop);
            this.panelALL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelALL.Location = new System.Drawing.Point(0, 0);
            this.panelALL.Margin = new System.Windows.Forms.Padding(0);
            this.panelALL.Name = "panelALL";
            this.panelALL.Size = new System.Drawing.Size(848, 1341);
            this.panelALL.TabIndex = 101;
            // 
            // panelContainerDetails
            // 
            this.panelContainerDetails.Controls.Add(this.txtHS10);
            this.panelContainerDetails.Controls.Add(this.mscmbHS10);
            this.panelContainerDetails.Controls.Add(this.txtHS9);
            this.panelContainerDetails.Controls.Add(this.mscmbHS9);
            this.panelContainerDetails.Controls.Add(this.txtHS8);
            this.panelContainerDetails.Controls.Add(this.mscmbHS8);
            this.panelContainerDetails.Controls.Add(this.txtHS7);
            this.panelContainerDetails.Controls.Add(this.mscmbHS7);
            this.panelContainerDetails.Controls.Add(this.txtHS6);
            this.panelContainerDetails.Controls.Add(this.labelControl24);
            this.panelContainerDetails.Controls.Add(this.mscmbHS6);
            this.panelContainerDetails.Controls.Add(this.labelControl25);
            this.panelContainerDetails.Controls.Add(this.txtHS5);
            this.panelContainerDetails.Controls.Add(this.mscmbHS5);
            this.panelContainerDetails.Controls.Add(this.txtHS4);
            this.panelContainerDetails.Controls.Add(this.mscmbHS4);
            this.panelContainerDetails.Controls.Add(this.txtHS3);
            this.panelContainerDetails.Controls.Add(this.mscmbHS3);
            this.panelContainerDetails.Controls.Add(this.txtHS2);
            this.panelContainerDetails.Controls.Add(this.mscmbHS2);
            this.panelContainerDetails.Controls.Add(this.txtHS1);
            this.panelContainerDetails.Controls.Add(this.labelControl22);
            this.panelContainerDetails.Controls.Add(this.mscmbHS1);
            this.panelContainerDetails.Controls.Add(this.labelControl23);
            this.panelContainerDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelContainerDetails.Location = new System.Drawing.Point(0, 1155);
            this.panelContainerDetails.Name = "panelContainerDetails";
            this.panelContainerDetails.Size = new System.Drawing.Size(848, 149);
            this.panelContainerDetails.TabIndex = 76;
            // 
            // txtHS10
            // 
            this.txtHS10.Location = new System.Drawing.Point(453, 118);
            this.txtHS10.Name = "txtHS10";
            this.txtHS10.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHS10.Properties.MaxLength = 10;
            this.txtHS10.Size = new System.Drawing.Size(180, 21);
            this.txtHS10.TabIndex = 124;
            this.txtHS10.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // mscmbHS10
            // 
            this.mscmbHS10.EditText = "";
            this.mscmbHS10.EditValue = null;
            this.mscmbHS10.Location = new System.Drawing.Point(641, 118);
            this.mscmbHS10.Name = "mscmbHS10";
            this.mscmbHS10.ReadOnly = false;
            this.mscmbHS10.RefreshButtonToolTip = "";
            this.mscmbHS10.ShowRefreshButton = false;
            this.mscmbHS10.Size = new System.Drawing.Size(180, 21);
            this.mscmbHS10.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscmbHS10.TabIndex = 123;
            this.mscmbHS10.ToolTip = "";
            this.mscmbHS10.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // txtHS9
            // 
            this.txtHS9.Location = new System.Drawing.Point(453, 95);
            this.txtHS9.Name = "txtHS9";
            this.txtHS9.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHS9.Properties.MaxLength = 10;
            this.txtHS9.Size = new System.Drawing.Size(180, 21);
            this.txtHS9.TabIndex = 122;
            this.txtHS9.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // mscmbHS9
            // 
            this.mscmbHS9.EditText = "";
            this.mscmbHS9.EditValue = null;
            this.mscmbHS9.Location = new System.Drawing.Point(641, 95);
            this.mscmbHS9.Name = "mscmbHS9";
            this.mscmbHS9.ReadOnly = false;
            this.mscmbHS9.RefreshButtonToolTip = "";
            this.mscmbHS9.ShowRefreshButton = false;
            this.mscmbHS9.Size = new System.Drawing.Size(180, 21);
            this.mscmbHS9.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscmbHS9.TabIndex = 121;
            this.mscmbHS9.ToolTip = "";
            this.mscmbHS9.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // txtHS8
            // 
            this.txtHS8.Location = new System.Drawing.Point(453, 72);
            this.txtHS8.Name = "txtHS8";
            this.txtHS8.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHS8.Properties.MaxLength = 10;
            this.txtHS8.Size = new System.Drawing.Size(180, 21);
            this.txtHS8.TabIndex = 120;
            this.txtHS8.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // mscmbHS8
            // 
            this.mscmbHS8.EditText = "";
            this.mscmbHS8.EditValue = null;
            this.mscmbHS8.Location = new System.Drawing.Point(641, 72);
            this.mscmbHS8.Name = "mscmbHS8";
            this.mscmbHS8.ReadOnly = false;
            this.mscmbHS8.RefreshButtonToolTip = "";
            this.mscmbHS8.ShowRefreshButton = false;
            this.mscmbHS8.Size = new System.Drawing.Size(180, 21);
            this.mscmbHS8.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscmbHS8.TabIndex = 119;
            this.mscmbHS8.ToolTip = "";
            this.mscmbHS8.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // txtHS7
            // 
            this.txtHS7.Location = new System.Drawing.Point(453, 49);
            this.txtHS7.Name = "txtHS7";
            this.txtHS7.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHS7.Properties.MaxLength = 10;
            this.txtHS7.Size = new System.Drawing.Size(180, 21);
            this.txtHS7.TabIndex = 118;
            this.txtHS7.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // mscmbHS7
            // 
            this.mscmbHS7.EditText = "";
            this.mscmbHS7.EditValue = null;
            this.mscmbHS7.Location = new System.Drawing.Point(641, 49);
            this.mscmbHS7.Name = "mscmbHS7";
            this.mscmbHS7.ReadOnly = false;
            this.mscmbHS7.RefreshButtonToolTip = "";
            this.mscmbHS7.ShowRefreshButton = false;
            this.mscmbHS7.Size = new System.Drawing.Size(180, 21);
            this.mscmbHS7.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscmbHS7.TabIndex = 117;
            this.mscmbHS7.ToolTip = "";
            this.mscmbHS7.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // txtHS6
            // 
            this.txtHS6.Location = new System.Drawing.Point(453, 26);
            this.txtHS6.Name = "txtHS6";
            this.txtHS6.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHS6.Properties.MaxLength = 10;
            this.txtHS6.Size = new System.Drawing.Size(180, 21);
            this.txtHS6.TabIndex = 114;
            this.txtHS6.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl24
            // 
            this.labelControl24.Location = new System.Drawing.Point(455, 6);
            this.labelControl24.Name = "labelControl24";
            this.labelControl24.Size = new System.Drawing.Size(76, 14);
            this.labelControl24.TabIndex = 116;
            this.labelControl24.Text = "Harmonized #";
            // 
            // mscmbHS6
            // 
            this.mscmbHS6.EditText = "";
            this.mscmbHS6.EditValue = null;
            this.mscmbHS6.Location = new System.Drawing.Point(641, 26);
            this.mscmbHS6.Name = "mscmbHS6";
            this.mscmbHS6.ReadOnly = false;
            this.mscmbHS6.RefreshButtonToolTip = "";
            this.mscmbHS6.ShowRefreshButton = false;
            this.mscmbHS6.Size = new System.Drawing.Size(180, 21);
            this.mscmbHS6.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscmbHS6.TabIndex = 113;
            this.mscmbHS6.ToolTip = "";
            this.mscmbHS6.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // labelControl25
            // 
            this.labelControl25.Location = new System.Drawing.Point(641, 6);
            this.labelControl25.Name = "labelControl25";
            this.labelControl25.Size = new System.Drawing.Size(95, 14);
            this.labelControl25.TabIndex = 115;
            this.labelControl25.Text = "Country Of Origin";
            // 
            // txtHS5
            // 
            this.txtHS5.Location = new System.Drawing.Point(35, 118);
            this.txtHS5.Name = "txtHS5";
            this.txtHS5.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHS5.Properties.MaxLength = 10;
            this.txtHS5.Size = new System.Drawing.Size(180, 21);
            this.txtHS5.TabIndex = 112;
            this.txtHS5.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // mscmbHS5
            // 
            this.mscmbHS5.EditText = "";
            this.mscmbHS5.EditValue = null;
            this.mscmbHS5.Location = new System.Drawing.Point(223, 118);
            this.mscmbHS5.Name = "mscmbHS5";
            this.mscmbHS5.ReadOnly = false;
            this.mscmbHS5.RefreshButtonToolTip = "";
            this.mscmbHS5.ShowRefreshButton = false;
            this.mscmbHS5.Size = new System.Drawing.Size(180, 21);
            this.mscmbHS5.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscmbHS5.TabIndex = 111;
            this.mscmbHS5.ToolTip = "";
            this.mscmbHS5.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // txtHS4
            // 
            this.txtHS4.Location = new System.Drawing.Point(35, 95);
            this.txtHS4.Name = "txtHS4";
            this.txtHS4.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHS4.Properties.MaxLength = 10;
            this.txtHS4.Size = new System.Drawing.Size(180, 21);
            this.txtHS4.TabIndex = 110;
            this.txtHS4.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // mscmbHS4
            // 
            this.mscmbHS4.EditText = "";
            this.mscmbHS4.EditValue = null;
            this.mscmbHS4.Location = new System.Drawing.Point(223, 95);
            this.mscmbHS4.Name = "mscmbHS4";
            this.mscmbHS4.ReadOnly = false;
            this.mscmbHS4.RefreshButtonToolTip = "";
            this.mscmbHS4.ShowRefreshButton = false;
            this.mscmbHS4.Size = new System.Drawing.Size(180, 21);
            this.mscmbHS4.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscmbHS4.TabIndex = 109;
            this.mscmbHS4.ToolTip = "";
            this.mscmbHS4.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // txtHS3
            // 
            this.txtHS3.Location = new System.Drawing.Point(35, 72);
            this.txtHS3.Name = "txtHS3";
            this.txtHS3.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHS3.Properties.MaxLength = 10;
            this.txtHS3.Size = new System.Drawing.Size(180, 21);
            this.txtHS3.TabIndex = 108;
            this.txtHS3.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // mscmbHS3
            // 
            this.mscmbHS3.EditText = "";
            this.mscmbHS3.EditValue = null;
            this.mscmbHS3.Location = new System.Drawing.Point(223, 72);
            this.mscmbHS3.Name = "mscmbHS3";
            this.mscmbHS3.ReadOnly = false;
            this.mscmbHS3.RefreshButtonToolTip = "";
            this.mscmbHS3.ShowRefreshButton = false;
            this.mscmbHS3.Size = new System.Drawing.Size(180, 21);
            this.mscmbHS3.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscmbHS3.TabIndex = 107;
            this.mscmbHS3.ToolTip = "";
            this.mscmbHS3.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // txtHS2
            // 
            this.txtHS2.Location = new System.Drawing.Point(35, 49);
            this.txtHS2.Name = "txtHS2";
            this.txtHS2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHS2.Properties.MaxLength = 10;
            this.txtHS2.Size = new System.Drawing.Size(180, 21);
            this.txtHS2.TabIndex = 106;
            this.txtHS2.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // mscmbHS2
            // 
            this.mscmbHS2.EditText = "";
            this.mscmbHS2.EditValue = null;
            this.mscmbHS2.Location = new System.Drawing.Point(223, 49);
            this.mscmbHS2.Name = "mscmbHS2";
            this.mscmbHS2.ReadOnly = false;
            this.mscmbHS2.RefreshButtonToolTip = "";
            this.mscmbHS2.ShowRefreshButton = false;
            this.mscmbHS2.Size = new System.Drawing.Size(180, 21);
            this.mscmbHS2.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscmbHS2.TabIndex = 105;
            this.mscmbHS2.ToolTip = "";
            this.mscmbHS2.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // txtHS1
            // 
            this.txtHS1.Location = new System.Drawing.Point(35, 26);
            this.txtHS1.Name = "txtHS1";
            this.txtHS1.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHS1.Properties.MaxLength = 10;
            this.txtHS1.Size = new System.Drawing.Size(180, 21);
            this.txtHS1.TabIndex = 102;
            this.txtHS1.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl22
            // 
            this.labelControl22.Location = new System.Drawing.Point(37, 6);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(76, 14);
            this.labelControl22.TabIndex = 104;
            this.labelControl22.Text = "Harmonized #";
            // 
            // mscmbHS1
            // 
            this.mscmbHS1.EditText = "";
            this.mscmbHS1.EditValue = null;
            this.mscmbHS1.Location = new System.Drawing.Point(223, 26);
            this.mscmbHS1.Name = "mscmbHS1";
            this.mscmbHS1.ReadOnly = false;
            this.mscmbHS1.RefreshButtonToolTip = "";
            this.mscmbHS1.ShowRefreshButton = false;
            this.mscmbHS1.Size = new System.Drawing.Size(180, 21);
            this.mscmbHS1.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscmbHS1.TabIndex = 101;
            this.mscmbHS1.ToolTip = "";
            this.mscmbHS1.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // labelControl23
            // 
            this.labelControl23.Location = new System.Drawing.Point(223, 6);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(95, 14);
            this.labelControl23.TabIndex = 103;
            this.labelControl23.Text = "Country Of Origin";
            // 
            // groupControl2
            // 
            this.groupControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupControl2.Controls.Add(this.gcAMSContainer);
            this.groupControl2.Controls.Add(this.barDockControl7);
            this.groupControl2.Controls.Add(this.barDockControl8);
            this.groupControl2.Controls.Add(this.barDockControl6);
            this.groupControl2.Controls.Add(this.barDockControl5);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 978);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(848, 177);
            this.groupControl2.TabIndex = 75;
            this.groupControl2.Text = "groupControl2";
            // 
            // gcAMSContainer
            // 
            this.gcAMSContainer.DataSource = this.bindingContainers;
            this.gcAMSContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcAMSContainer.Location = new System.Drawing.Point(0, 26);
            this.gcAMSContainer.MainView = this.gvAMSContainer;
            this.gcAMSContainer.Name = "gcAMSContainer";
            this.gcAMSContainer.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbBoxType,
            this.repositoryItemTextEdit1,
            this.repositoryItemSpinEdit4,
            this.repositoryItemMemoEdit1});
            this.gcAMSContainer.Size = new System.Drawing.Size(848, 151);
            this.gcAMSContainer.TabIndex = 79;
            this.gcAMSContainer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAMSContainer});
            // 
            // bindingContainers
            // 
            this.bindingContainers.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.ContainerForAMS);
            // 
            // gvAMSContainer
            // 
            this.gvAMSContainer.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvAMSContainer.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colContainerNumber,
            this.colSeal,
            this.colKilos,
            this.colQuantity,
            this.colUnitOfMeasure,
            this.colFreeFormDescription});
            this.gvAMSContainer.GridControl = this.gcAMSContainer;
            this.gvAMSContainer.IndicatorWidth = 30;
            this.gvAMSContainer.Name = "gvAMSContainer";
            this.gvAMSContainer.OptionsView.ShowDetailButtons = false;
            this.gvAMSContainer.OptionsView.ShowGroupPanel = false;
            this.gvAMSContainer.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvAMSContainer_CellValueChanging);
            // 
            // colContainerNumber
            // 
            this.colContainerNumber.Caption = "ContainerNumber";
            this.colContainerNumber.ColumnEdit = this.cmbBoxType;
            this.colContainerNumber.FieldName = "ContainerNumber";
            this.colContainerNumber.Name = "colContainerNumber";
            this.colContainerNumber.Visible = true;
            this.colContainerNumber.VisibleIndex = 0;
            this.colContainerNumber.Width = 139;
            // 
            // cmbBoxType
            // 
            this.cmbBoxType.AutoHeight = false;
            this.cmbBoxType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBoxType.Name = "cmbBoxType";
            // 
            // colSeal
            // 
            this.colSeal.Caption = "Seal";
            this.colSeal.FieldName = "Seal";
            this.colSeal.Name = "colSeal";
            this.colSeal.OptionsColumn.ReadOnly = true;
            this.colSeal.Visible = true;
            this.colSeal.VisibleIndex = 1;
            this.colSeal.Width = 90;
            // 
            // colKilos
            // 
            this.colKilos.Caption = "Kilos";
            this.colKilos.ColumnEdit = this.repositoryItemSpinEdit4;
            this.colKilos.FieldName = "Kilos";
            this.colKilos.Name = "colKilos";
            this.colKilos.Visible = true;
            this.colKilos.VisibleIndex = 3;
            // 
            // repositoryItemSpinEdit4
            // 
            this.repositoryItemSpinEdit4.AutoHeight = false;
            this.repositoryItemSpinEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit4.Mask.EditMask = "n0";
            this.repositoryItemSpinEdit4.Name = "repositoryItemSpinEdit4";
            // 
            // colQuantity
            // 
            this.colQuantity.Caption = "Quantity";
            this.colQuantity.ColumnEdit = this.repositoryItemSpinEdit4;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 2;
            this.colQuantity.Width = 79;
            // 
            // colUnitOfMeasure
            // 
            this.colUnitOfMeasure.Caption = "UnitOfMeasure";
            this.colUnitOfMeasure.ColumnEdit = this.repositoryItemTextEdit1;
            this.colUnitOfMeasure.FieldName = "UnitOfMeasure";
            this.colUnitOfMeasure.Name = "colUnitOfMeasure";
            this.colUnitOfMeasure.OptionsColumn.ReadOnly = true;
            this.colUnitOfMeasure.Visible = true;
            this.colUnitOfMeasure.VisibleIndex = 4;
            this.colUnitOfMeasure.Width = 90;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // colFreeFormDescription
            // 
            this.colFreeFormDescription.Caption = "FreeFormDescription";
            this.colFreeFormDescription.FieldName = "FreeFormDescription";
            this.colFreeFormDescription.Name = "colFreeFormDescription";
            this.colFreeFormDescription.Visible = true;
            this.colFreeFormDescription.VisibleIndex = 5;
            this.colFreeFormDescription.Width = 202;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // barDockControl7
            // 
            this.barDockControl7.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl7.Location = new System.Drawing.Point(0, 26);
            this.barDockControl7.Size = new System.Drawing.Size(0, 151);
            // 
            // barDockControl8
            // 
            this.barDockControl8.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl8.Location = new System.Drawing.Point(848, 26);
            this.barDockControl8.Size = new System.Drawing.Size(0, 151);
            // 
            // barDockControl6
            // 
            this.barDockControl6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl6.Location = new System.Drawing.Point(0, 177);
            this.barDockControl6.Size = new System.Drawing.Size(848, 0);
            // 
            // barDockControl5
            // 
            this.barDockControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl5.Location = new System.Drawing.Point(0, 0);
            this.barDockControl5.Size = new System.Drawing.Size(848, 26);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.vScrollBar2);
            this.panelContent.Controls.Add(this.panelLeft);
            this.panelContent.Controls.Add(this.panelRight);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelContent.Location = new System.Drawing.Point(0, 271);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(848, 707);
            this.panelContent.TabIndex = 72;
            // 
            // vScrollBar2
            // 
            this.vScrollBar2.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar2.Location = new System.Drawing.Point(829, 2);
            this.vScrollBar2.Maximum = 270;
            this.vScrollBar2.Name = "vScrollBar2";
            this.vScrollBar2.Size = new System.Drawing.Size(17, 703);
            this.vScrollBar2.TabIndex = 107;
            this.vScrollBar2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar2_Scroll);
            // 
            // panelLeft
            // 
            this.panelLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelLeft.Controls.Add(this.panelControl7);
            this.panelLeft.Controls.Add(this.panelISFImporterRef);
            this.panelLeft.Controls.Add(this.panelControl6);
            this.panelLeft.Location = new System.Drawing.Point(2, 2);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(420, 1049);
            this.panelLeft.TabIndex = 105;
            // 
            // panelControl7
            // 
            this.panelControl7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl7.Controls.Add(this.panel9);
            this.panelControl7.Controls.Add(this.panel8);
            this.panelControl7.Controls.Add(this.panel7);
            this.panelControl7.Controls.Add(this.panel6);
            this.panelControl7.Controls.Add(this.panel5);
            this.panelControl7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl7.Location = new System.Drawing.Point(0, 123);
            this.panelControl7.Name = "panelControl7";
            this.panelControl7.Size = new System.Drawing.Size(420, 926);
            this.panelControl7.TabIndex = 106;
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.stxtConsolidator);
            this.panel9.Controls.Add(this.lblConsolidator);
            this.panel9.Controls.Add(this.ckConsolidator);
            this.panel9.Controls.Add(this.ConsolidatorDescription);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(2, 710);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(416, 216);
            this.panel9.TabIndex = 155;
            // 
            // stxtConsolidator
            // 
            this.stxtConsolidator.EditValue = "";
            this.stxtConsolidator.Location = new System.Drawing.Point(123, 21);
            this.stxtConsolidator.Name = "stxtConsolidator";
            this.stxtConsolidator.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtConsolidator.Properties.Appearance.Options.UseBackColor = true;
            this.stxtConsolidator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtConsolidator.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.stxtConsolidator.Size = new System.Drawing.Size(290, 21);
            this.stxtConsolidator.TabIndex = 144;
            this.stxtConsolidator.Enter += new System.EventHandler(this.stxtConsolidator_Enter);
            this.stxtConsolidator.KeyDown += new System.Windows.Forms.KeyEventHandler(this.stxtConsolidator_KeyDown);
            // 
            // lblConsolidator
            // 
            this.lblConsolidator.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lblConsolidator.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblConsolidator.Appearance.Options.UseFont = true;
            this.lblConsolidator.Appearance.Options.UseForeColor = true;
            this.lblConsolidator.Location = new System.Drawing.Point(33, 21);
            this.lblConsolidator.Name = "lblConsolidator";
            this.lblConsolidator.Size = new System.Drawing.Size(78, 18);
            this.lblConsolidator.TabIndex = 98;
            this.lblConsolidator.Text = "Consolidator";
            // 
            // ckConsolidator
            // 
            this.ckConsolidator.Location = new System.Drawing.Point(121, 0);
            this.ckConsolidator.MenuManager = this.barManager1;
            this.ckConsolidator.Name = "ckConsolidator";
            this.ckConsolidator.Properties.Caption = "Same As Stuffing Location ";
            this.ckConsolidator.Size = new System.Drawing.Size(287, 19);
            this.ckConsolidator.TabIndex = 100;
            this.ckConsolidator.CheckedChanged += new System.EventHandler(this.ckConsolidator_CheckedChanged);
            // 
            // ConsolidatorDescription
            // 
            this.ConsolidatorDescription.CityID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ConsolidatorDescription.CustomerDescriptionForAMS = ((ICP.Common.ServiceInterface.DataObjects.CustomerDescriptionForAMS)(resources.GetObject("ConsolidatorDescription.CustomerDescriptionForAMS")));
            this.ConsolidatorDescription.geographyService = null;
            this.ConsolidatorDescription.Location = new System.Drawing.Point(47, 42);
            this.ConsolidatorDescription.Margin = new System.Windows.Forms.Padding(0);
            this.ConsolidatorDescription.MaximumSize = new System.Drawing.Size(373, 175);
            this.ConsolidatorDescription.MinimumSize = new System.Drawing.Size(320, 125);
            this.ConsolidatorDescription.Name = "ConsolidatorDescription";
            this.ConsolidatorDescription.PostalCodeList = null;
            this.ConsolidatorDescription.Size = new System.Drawing.Size(373, 139);
            this.ConsolidatorDescription.TabIndex = 145;
            this.ConsolidatorDescription.ZipCode = null;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.stxtStuffingLocation);
            this.panel8.Controls.Add(this.lblStuffingLocation);
            this.panel8.Controls.Add(this.ckStuffingLocation);
            this.panel8.Controls.Add(this.StuffingDescription);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(2, 528);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(416, 182);
            this.panel8.TabIndex = 154;
            // 
            // stxtStuffingLocation
            // 
            this.stxtStuffingLocation.EditValue = "";
            this.stxtStuffingLocation.Location = new System.Drawing.Point(123, 20);
            this.stxtStuffingLocation.Name = "stxtStuffingLocation";
            this.stxtStuffingLocation.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtStuffingLocation.Properties.Appearance.Options.UseBackColor = true;
            this.stxtStuffingLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtStuffingLocation.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.stxtStuffingLocation.Size = new System.Drawing.Size(290, 21);
            this.stxtStuffingLocation.TabIndex = 142;
            this.stxtStuffingLocation.Enter += new System.EventHandler(this.stxtStuffingLocation_Enter);
            this.stxtStuffingLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.stxtStuffingLocation_KeyDown);
            // 
            // lblStuffingLocation
            // 
            this.lblStuffingLocation.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lblStuffingLocation.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblStuffingLocation.Appearance.Options.UseFont = true;
            this.lblStuffingLocation.Appearance.Options.UseForeColor = true;
            this.lblStuffingLocation.Location = new System.Drawing.Point(9, 19);
            this.lblStuffingLocation.Name = "lblStuffingLocation";
            this.lblStuffingLocation.Size = new System.Drawing.Size(102, 18);
            this.lblStuffingLocation.TabIndex = 81;
            this.lblStuffingLocation.Text = "StuffingLocation";
            // 
            // ckStuffingLocation
            // 
            this.ckStuffingLocation.Location = new System.Drawing.Point(122, 2);
            this.ckStuffingLocation.MenuManager = this.barManager1;
            this.ckStuffingLocation.Name = "ckStuffingLocation";
            this.ckStuffingLocation.Properties.Caption = "Same As Seller ";
            this.ckStuffingLocation.Size = new System.Drawing.Size(287, 19);
            this.ckStuffingLocation.TabIndex = 94;
            this.ckStuffingLocation.CheckedChanged += new System.EventHandler(this.ckStuffingLocation_CheckedChanged);
            // 
            // StuffingDescription
            // 
            this.StuffingDescription.CityID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.StuffingDescription.CustomerDescriptionForAMS = ((ICP.Common.ServiceInterface.DataObjects.CustomerDescriptionForAMS)(resources.GetObject("StuffingDescription.CustomerDescriptionForAMS")));
            this.StuffingDescription.geographyService = null;
            this.StuffingDescription.Location = new System.Drawing.Point(47, 41);
            this.StuffingDescription.Margin = new System.Windows.Forms.Padding(0);
            this.StuffingDescription.MaximumSize = new System.Drawing.Size(373, 175);
            this.StuffingDescription.MinimumSize = new System.Drawing.Size(320, 125);
            this.StuffingDescription.Name = "StuffingDescription";
            this.StuffingDescription.PostalCodeList = null;
            this.StuffingDescription.Size = new System.Drawing.Size(373, 136);
            this.StuffingDescription.TabIndex = 143;
            this.StuffingDescription.ZipCode = null;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.stxtManufacturer);
            this.panel7.Controls.Add(this.ckManufacturer);
            this.panel7.Controls.Add(this.lblManufacturer);
            this.panel7.Controls.Add(this.ManuDescription);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(2, 346);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(416, 182);
            this.panel7.TabIndex = 153;
            // 
            // stxtManufacturer
            // 
            this.stxtManufacturer.EditValue = "";
            this.stxtManufacturer.Location = new System.Drawing.Point(120, 21);
            this.stxtManufacturer.Name = "stxtManufacturer";
            this.stxtManufacturer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtManufacturer.Properties.Appearance.Options.UseBackColor = true;
            this.stxtManufacturer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtManufacturer.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.stxtManufacturer.Size = new System.Drawing.Size(290, 21);
            this.stxtManufacturer.TabIndex = 140;
            this.stxtManufacturer.Enter += new System.EventHandler(this.stxtManufacturer_Enter);
            this.stxtManufacturer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.stxtManufacturer_KeyDown);
            // 
            // ckManufacturer
            // 
            this.ckManufacturer.Location = new System.Drawing.Point(119, 1);
            this.ckManufacturer.MenuManager = this.barManager1;
            this.ckManufacturer.Name = "ckManufacturer";
            this.ckManufacturer.Properties.Caption = "Same As Seller ";
            this.ckManufacturer.Size = new System.Drawing.Size(287, 19);
            this.ckManufacturer.TabIndex = 93;
            this.ckManufacturer.CheckedChanged += new System.EventHandler(this.ckManufacturer_CheckedChanged);
            // 
            // lblManufacturer
            // 
            this.lblManufacturer.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lblManufacturer.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblManufacturer.Appearance.Options.UseFont = true;
            this.lblManufacturer.Appearance.Options.UseForeColor = true;
            this.lblManufacturer.Location = new System.Drawing.Point(19, 21);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(87, 18);
            this.lblManufacturer.TabIndex = 82;
            this.lblManufacturer.Text = "Manufacturer";
            // 
            // ManuDescription
            // 
            this.ManuDescription.CityID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ManuDescription.CustomerDescriptionForAMS = ((ICP.Common.ServiceInterface.DataObjects.CustomerDescriptionForAMS)(resources.GetObject("ManuDescription.CustomerDescriptionForAMS")));
            this.ManuDescription.geographyService = null;
            this.ManuDescription.Location = new System.Drawing.Point(44, 41);
            this.ManuDescription.Margin = new System.Windows.Forms.Padding(0);
            this.ManuDescription.MaximumSize = new System.Drawing.Size(373, 175);
            this.ManuDescription.MinimumSize = new System.Drawing.Size(320, 125);
            this.ManuDescription.Name = "ManuDescription";
            this.ManuDescription.PostalCodeList = null;
            this.ManuDescription.Size = new System.Drawing.Size(366, 140);
            this.ManuDescription.TabIndex = 141;
            this.ManuDescription.ZipCode = null;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.ckSeller);
            this.panel6.Controls.Add(this.stxtSeller);
            this.panel6.Controls.Add(this.lblSeller);
            this.panel6.Controls.Add(this.SellerDescription);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(2, 164);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(416, 182);
            this.panel6.TabIndex = 152;
            // 
            // ckSeller
            // 
            this.ckSeller.Location = new System.Drawing.Point(115, -1);
            this.ckSeller.MenuManager = this.barManager1;
            this.ckSeller.Name = "ckSeller";
            this.ckSeller.Properties.Caption = "Same As Shipper ";
            this.ckSeller.Size = new System.Drawing.Size(286, 19);
            this.ckSeller.TabIndex = 37;
            this.ckSeller.CheckedChanged += new System.EventHandler(this.ckSeller_CheckedChanged);
            // 
            // stxtSeller
            // 
            this.stxtSeller.EditValue = "";
            this.stxtSeller.Location = new System.Drawing.Point(118, 19);
            this.stxtSeller.Name = "stxtSeller";
            this.stxtSeller.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtSeller.Properties.Appearance.Options.UseBackColor = true;
            this.stxtSeller.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtSeller.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.stxtSeller.Size = new System.Drawing.Size(290, 21);
            this.stxtSeller.TabIndex = 148;
            this.stxtSeller.Enter += new System.EventHandler(this.stxtSeller_Enter);
            this.stxtSeller.KeyDown += new System.Windows.Forms.KeyEventHandler(this.stxtSeller_KeyDown);
            // 
            // lblSeller
            // 
            this.lblSeller.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lblSeller.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblSeller.Appearance.Options.UseFont = true;
            this.lblSeller.Appearance.Options.UseForeColor = true;
            this.lblSeller.Location = new System.Drawing.Point(72, 15);
            this.lblSeller.Name = "lblSeller";
            this.lblSeller.Size = new System.Drawing.Size(33, 18);
            this.lblSeller.TabIndex = 10;
            this.lblSeller.Text = "Seller";
            // 
            // SellerDescription
            // 
            this.SellerDescription.CityID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.SellerDescription.CustomerDescriptionForAMS = ((ICP.Common.ServiceInterface.DataObjects.CustomerDescriptionForAMS)(resources.GetObject("SellerDescription.CustomerDescriptionForAMS")));
            this.SellerDescription.geographyService = null;
            this.SellerDescription.Location = new System.Drawing.Point(42, 39);
            this.SellerDescription.Margin = new System.Windows.Forms.Padding(0);
            this.SellerDescription.MaximumSize = new System.Drawing.Size(373, 175);
            this.SellerDescription.MinimumSize = new System.Drawing.Size(320, 125);
            this.SellerDescription.Name = "SellerDescription";
            this.SellerDescription.PostalCodeList = null;
            this.SellerDescription.Size = new System.Drawing.Size(370, 141);
            this.SellerDescription.TabIndex = 139;
            this.SellerDescription.ZipCode = null;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.stxtAMSShipper);
            this.panel5.Controls.Add(this.ShipDescription);
            this.panel5.Controls.Add(this.labAMSShipper);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(2, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(416, 162);
            this.panel5.TabIndex = 151;
            // 
            // stxtAMSShipper
            // 
            this.stxtAMSShipper.EditValue = "";
            this.stxtAMSShipper.Location = new System.Drawing.Point(119, 4);
            this.stxtAMSShipper.Name = "stxtAMSShipper";
            this.stxtAMSShipper.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtAMSShipper.Properties.Appearance.Options.UseBackColor = true;
            this.stxtAMSShipper.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtAMSShipper.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.stxtAMSShipper.Size = new System.Drawing.Size(290, 21);
            this.stxtAMSShipper.TabIndex = 149;
            this.stxtAMSShipper.Enter += new System.EventHandler(this.stxtAMSShipper_Enter);
            this.stxtAMSShipper.KeyDown += new System.Windows.Forms.KeyEventHandler(this.stxtAMSShipper_KeyDown);
            // 
            // ShipDescription
            // 
            this.ShipDescription.CityID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ShipDescription.CustomerDescriptionForAMS = ((ICP.Common.ServiceInterface.DataObjects.CustomerDescriptionForAMS)(resources.GetObject("ShipDescription.CustomerDescriptionForAMS")));
            this.ShipDescription.geographyService = null;
            this.ShipDescription.Location = new System.Drawing.Point(43, 23);
            this.ShipDescription.Margin = new System.Windows.Forms.Padding(0);
            this.ShipDescription.MaximumSize = new System.Drawing.Size(373, 175);
            this.ShipDescription.MinimumSize = new System.Drawing.Size(320, 125);
            this.ShipDescription.Name = "ShipDescription";
            this.ShipDescription.PostalCodeList = null;
            this.ShipDescription.Size = new System.Drawing.Size(373, 135);
            this.ShipDescription.TabIndex = 150;
            this.ShipDescription.ZipCode = null;
            // 
            // labAMSShipper
            // 
            this.labAMSShipper.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labAMSShipper.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labAMSShipper.Appearance.Options.UseFont = true;
            this.labAMSShipper.Appearance.Options.UseForeColor = true;
            this.labAMSShipper.Location = new System.Drawing.Point(62, 6);
            this.labAMSShipper.Name = "labAMSShipper";
            this.labAMSShipper.Size = new System.Drawing.Size(47, 18);
            this.labAMSShipper.TabIndex = 68;
            this.labAMSShipper.Text = "Shipper";
            // 
            // panelISFImporterRef
            // 
            this.panelISFImporterRef.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelISFImporterRef.Controls.Add(this.cmbISFImporterRefCountry);
            this.panelISFImporterRef.Controls.Add(this.dateISFImporterRefDate);
            this.panelISFImporterRef.Controls.Add(this.labelControl11);
            this.panelISFImporterRef.Controls.Add(this.txtISFImporterRefLastName);
            this.panelISFImporterRef.Controls.Add(this.labelControl10);
            this.panelISFImporterRef.Controls.Add(this.txtISFImporterRefFirstName);
            this.panelISFImporterRef.Controls.Add(this.labelControl9);
            this.panelISFImporterRef.Controls.Add(this.labelControl8);
            this.panelISFImporterRef.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelISFImporterRef.Location = new System.Drawing.Point(0, 25);
            this.panelISFImporterRef.Margin = new System.Windows.Forms.Padding(0);
            this.panelISFImporterRef.Name = "panelISFImporterRef";
            this.panelISFImporterRef.Size = new System.Drawing.Size(420, 98);
            this.panelISFImporterRef.TabIndex = 101;
            // 
            // cmbISFImporterRefCountry
            // 
            this.cmbISFImporterRefCountry.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingAMSACIISF, "ISFImporterCountryOfIssuance", true));
            this.cmbISFImporterRefCountry.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bindingAMSACIISF, "IsfImporterCountryOfIssuanceName", true));
            this.cmbISFImporterRefCountry.EditText = "";
            this.cmbISFImporterRefCountry.EditValue = null;
            this.cmbISFImporterRefCountry.Location = new System.Drawing.Point(139, 2);
            this.cmbISFImporterRefCountry.Name = "cmbISFImporterRefCountry";
            this.cmbISFImporterRefCountry.ReadOnly = false;
            this.cmbISFImporterRefCountry.RefreshButtonToolTip = "";
            this.cmbISFImporterRefCountry.ShowRefreshButton = false;
            this.cmbISFImporterRefCountry.Size = new System.Drawing.Size(269, 21);
            this.cmbISFImporterRefCountry.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbISFImporterRefCountry.TabIndex = 103;
            this.cmbISFImporterRefCountry.ToolTip = "";
            this.cmbISFImporterRefCountry.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // bindingAMSACIISF
            // 
            this.bindingAMSACIISF.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanHBL2AmsAciIsf);
            // 
            // dateISFImporterRefDate
            // 
            this.dateISFImporterRefDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingAMSACIISF, "ISFImporterDateOfBirth", true));
            this.dateISFImporterRefDate.EditValue = null;
            this.dateISFImporterRefDate.Location = new System.Drawing.Point(123, 72);
            this.dateISFImporterRefDate.MenuManager = this.barManager1;
            this.dateISFImporterRefDate.Name = "dateISFImporterRefDate";
            this.dateISFImporterRefDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateISFImporterRefDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateISFImporterRefDate.Size = new System.Drawing.Size(285, 21);
            this.dateISFImporterRefDate.TabIndex = 108;
            this.dateISFImporterRefDate.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(43, 75);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(74, 14);
            this.labelControl11.TabIndex = 107;
            this.labelControl11.Text = "Date of Birth:";
            // 
            // txtISFImporterRefLastName
            // 
            this.txtISFImporterRefLastName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingAMSACIISF, "ISFImporterLastName", true));
            this.txtISFImporterRefLastName.Location = new System.Drawing.Point(123, 49);
            this.txtISFImporterRefLastName.Name = "txtISFImporterRefLastName";
            this.txtISFImporterRefLastName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtISFImporterRefLastName.Size = new System.Drawing.Size(285, 21);
            this.txtISFImporterRefLastName.TabIndex = 105;
            this.txtISFImporterRefLastName.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(56, 52);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(61, 14);
            this.labelControl10.TabIndex = 106;
            this.labelControl10.Text = "Last Name:";
            // 
            // txtISFImporterRefFirstName
            // 
            this.txtISFImporterRefFirstName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingAMSACIISF, "ISFImporterFirstName", true));
            this.txtISFImporterRefFirstName.Location = new System.Drawing.Point(123, 26);
            this.txtISFImporterRefFirstName.Name = "txtISFImporterRefFirstName";
            this.txtISFImporterRefFirstName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtISFImporterRefFirstName.Size = new System.Drawing.Size(285, 21);
            this.txtISFImporterRefFirstName.TabIndex = 102;
            this.txtISFImporterRefFirstName.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(56, 29);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(61, 14);
            this.labelControl9.TabIndex = 104;
            this.labelControl9.Text = "First Name:";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(16, 7);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(117, 14);
            this.labelControl8.TabIndex = 102;
            this.labelControl8.Text = "Country of Issuance: ";
            // 
            // panelControl6
            // 
            this.panelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl6.Controls.Add(this.txtISFImporterRef);
            this.panelControl6.Controls.Add(this.labelControl2);
            this.panelControl6.Controls.Add(this.cmbISFImporterRef);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl6.Location = new System.Drawing.Point(0, 0);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(420, 25);
            this.panelControl6.TabIndex = 105;
            // 
            // txtISFImporterRef
            // 
            this.txtISFImporterRef.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingAMSACIISF, "ISFImporterID", true));
            this.txtISFImporterRef.EditValue = "";
            this.txtISFImporterRef.Location = new System.Drawing.Point(194, 3);
            this.txtISFImporterRef.Name = "txtISFImporterRef";
            this.txtISFImporterRef.Size = new System.Drawing.Size(214, 21);
            this.txtISFImporterRef.TabIndex = 29;
            this.txtISFImporterRef.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(100, 14);
            this.labelControl2.TabIndex = 30;
            this.labelControl2.Text = "ISF Importer Ref#";
            // 
            // cmbISFImporterRef
            // 
            this.cmbISFImporterRef.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingAMSACIISF, "ISFImporterIDType", true));
            this.cmbISFImporterRef.Location = new System.Drawing.Point(123, 2);
            this.cmbISFImporterRef.Name = "cmbISFImporterRef";
            this.cmbISFImporterRef.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbISFImporterRef.Properties.Appearance.Options.UseBackColor = true;
            this.cmbISFImporterRef.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbISFImporterRef.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbISFImporterRef.Size = new System.Drawing.Size(65, 21);
            this.cmbISFImporterRef.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbISFImporterRef.TabIndex = 34;
            this.cmbISFImporterRef.SelectedIndexChanged += new System.EventHandler(this.cmbImporterRef_SelectedIndexChanged);
            this.cmbISFImporterRef.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // panelRight
            // 
            this.panelRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelRight.Controls.Add(this.panelControl4);
            this.panelRight.Controls.Add(this.panelBuyer);
            this.panelRight.Controls.Add(this.panelControl3);
            this.panelRight.Controls.Add(this.panelConsignee);
            this.panelRight.Controls.Add(this.panelControl2);
            this.panelRight.Controls.Add(this.panelControl1);
            this.panelRight.Controls.Add(this.panelControl8);
            this.panelRight.Location = new System.Drawing.Point(422, 2);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(412, 1049);
            this.panelRight.TabIndex = 106;
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.panel11);
            this.panelControl4.Controls.Add(this.panel10);
            this.panelControl4.Controls.Add(this.labAMSNotifyParty);
            this.panelControl4.Controls.Add(this.txtAMSNotifyPartyDescription);
            this.panelControl4.Controls.Add(this.stxtAMSNotifyParty);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl4.Location = new System.Drawing.Point(0, 705);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(412, 344);
            this.panelControl4.TabIndex = 105;
            // 
            // panel11
            // 
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.BookingDescription);
            this.panel11.Controls.Add(this.labelControl26);
            this.panel11.Controls.Add(this.stxtBookingPartyInfo);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 182);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(412, 162);
            this.panel11.TabIndex = 154;
            // 
            // BookingDescription
            // 
            this.BookingDescription.CityID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.BookingDescription.CustomerDescriptionForAMS = ((ICP.Common.ServiceInterface.DataObjects.CustomerDescriptionForAMS)(resources.GetObject("BookingDescription.CustomerDescriptionForAMS")));
            this.BookingDescription.geographyService = null;
            this.BookingDescription.Location = new System.Drawing.Point(42, 23);
            this.BookingDescription.Margin = new System.Windows.Forms.Padding(0);
            this.BookingDescription.MaximumSize = new System.Drawing.Size(373, 175);
            this.BookingDescription.MinimumSize = new System.Drawing.Size(320, 125);
            this.BookingDescription.Name = "BookingDescription";
            this.BookingDescription.PostalCodeList = null;
            this.BookingDescription.Size = new System.Drawing.Size(367, 135);
            this.BookingDescription.TabIndex = 152;
            this.BookingDescription.ZipCode = null;
            // 
            // labelControl26
            // 
            this.labelControl26.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelControl26.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl26.Appearance.Options.UseFont = true;
            this.labelControl26.Appearance.Options.UseForeColor = true;
            this.labelControl26.Location = new System.Drawing.Point(21, 3);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(84, 18);
            this.labelControl26.TabIndex = 104;
            this.labelControl26.Text = "BookingParty";
            // 
            // stxtBookingPartyInfo
            // 
            this.stxtBookingPartyInfo.EditValue = "";
            this.stxtBookingPartyInfo.Location = new System.Drawing.Point(119, 3);
            this.stxtBookingPartyInfo.Name = "stxtBookingPartyInfo";
            this.stxtBookingPartyInfo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtBookingPartyInfo.Properties.Appearance.Options.UseBackColor = true;
            this.stxtBookingPartyInfo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtBookingPartyInfo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.stxtBookingPartyInfo.Size = new System.Drawing.Size(287, 21);
            this.stxtBookingPartyInfo.TabIndex = 151;
            this.stxtBookingPartyInfo.Enter += new System.EventHandler(this.stxtBookingPartyInfo_Enter);
            this.stxtBookingPartyInfo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.stxtBookingPartyInfo_KeyDown);
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.stxtShipToPatry);
            this.panel10.Controls.Add(this.lblShipToPatry);
            this.panel10.Controls.Add(this.ckShipTo);
            this.panel10.Controls.Add(this.ShiptoDescription);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(412, 182);
            this.panel10.TabIndex = 153;
            // 
            // stxtShipToPatry
            // 
            this.stxtShipToPatry.EditValue = "";
            this.stxtShipToPatry.Location = new System.Drawing.Point(118, 19);
            this.stxtShipToPatry.Name = "stxtShipToPatry";
            this.stxtShipToPatry.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtShipToPatry.Properties.Appearance.Options.UseBackColor = true;
            this.stxtShipToPatry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtShipToPatry.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.stxtShipToPatry.Size = new System.Drawing.Size(287, 21);
            this.stxtShipToPatry.TabIndex = 149;
            this.stxtShipToPatry.SelectedIndexChanged += new System.EventHandler(this.stxtShipToPatry_SelectedIndexChanged);
            this.stxtShipToPatry.Enter += new System.EventHandler(this.stxtShipToPatry_Enter);
            this.stxtShipToPatry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.stxtShipToPatry_KeyDown);
            // 
            // lblShipToPatry
            // 
            this.lblShipToPatry.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lblShipToPatry.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblShipToPatry.Appearance.Options.UseFont = true;
            this.lblShipToPatry.Appearance.Options.UseForeColor = true;
            this.lblShipToPatry.Location = new System.Drawing.Point(60, 19);
            this.lblShipToPatry.Name = "lblShipToPatry";
            this.lblShipToPatry.Size = new System.Drawing.Size(44, 18);
            this.lblShipToPatry.TabIndex = 83;
            this.lblShipToPatry.Text = "ShipTo";
            // 
            // ckShipTo
            // 
            this.ckShipTo.Location = new System.Drawing.Point(116, 0);
            this.ckShipTo.MenuManager = this.barManager1;
            this.ckShipTo.Name = "ckShipTo";
            this.ckShipTo.Properties.Caption = "Same As Buyer ";
            this.ckShipTo.Size = new System.Drawing.Size(287, 19);
            this.ckShipTo.TabIndex = 92;
            this.ckShipTo.CheckedChanged += new System.EventHandler(this.ckShipTo_CheckedChanged);
            // 
            // ShiptoDescription
            // 
            this.ShiptoDescription.CityID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ShiptoDescription.CustomerDescriptionForAMS = ((ICP.Common.ServiceInterface.DataObjects.CustomerDescriptionForAMS)(resources.GetObject("ShiptoDescription.CustomerDescriptionForAMS")));
            this.ShiptoDescription.geographyService = null;
            this.ShiptoDescription.Location = new System.Drawing.Point(42, 39);
            this.ShiptoDescription.Margin = new System.Windows.Forms.Padding(0);
            this.ShiptoDescription.MaximumSize = new System.Drawing.Size(373, 175);
            this.ShiptoDescription.MinimumSize = new System.Drawing.Size(320, 125);
            this.ShiptoDescription.Name = "ShiptoDescription";
            this.ShiptoDescription.PostalCodeList = null;
            this.ShiptoDescription.Size = new System.Drawing.Size(370, 134);
            this.ShiptoDescription.TabIndex = 150;
            this.ShiptoDescription.ZipCode = null;
            // 
            // labAMSNotifyParty
            // 
            this.labAMSNotifyParty.Location = new System.Drawing.Point(44, 339);
            this.labAMSNotifyParty.Name = "labAMSNotifyParty";
            this.labAMSNotifyParty.Size = new System.Drawing.Size(60, 14);
            this.labAMSNotifyParty.TabIndex = 101;
            this.labAMSNotifyParty.Text = "NotifyParty";
            this.labAMSNotifyParty.Visible = false;
            // 
            // txtAMSNotifyPartyDescription
            // 
            this.txtAMSNotifyPartyDescription.Location = new System.Drawing.Point(116, 362);
            this.txtAMSNotifyPartyDescription.Name = "txtAMSNotifyPartyDescription";
            this.txtAMSNotifyPartyDescription.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAMSNotifyPartyDescription.Properties.ReadOnly = true;
            this.txtAMSNotifyPartyDescription.Size = new System.Drawing.Size(285, 90);
            this.txtAMSNotifyPartyDescription.TabIndex = 102;
            this.txtAMSNotifyPartyDescription.Visible = false;
            this.txtAMSNotifyPartyDescription.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // stxtAMSNotifyParty
            // 
            this.stxtAMSNotifyParty.geographyService = null;
            this.stxtAMSNotifyParty.Location = new System.Drawing.Point(116, 338);
            this.stxtAMSNotifyParty.MenuManager = this.barManager1;
            this.stxtAMSNotifyParty.Name = "stxtAMSNotifyParty";
            this.stxtAMSNotifyParty.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtAMSNotifyParty.Properties.ActionButtonIndex = 1;
            this.stxtAMSNotifyParty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtAMSNotifyParty.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtAMSNotifyParty.Properties.PopupSizeable = false;
            this.stxtAMSNotifyParty.Properties.ShowPopupCloseButton = false;
            this.stxtAMSNotifyParty.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtAMSNotifyParty.Size = new System.Drawing.Size(285, 21);
            this.stxtAMSNotifyParty.TabIndex = 103;
            this.stxtAMSNotifyParty.Visible = false;
            // 
            // panelBuyer
            // 
            this.panelBuyer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelBuyer.Controls.Add(this.cmbBuyerCountry);
            this.panelBuyer.Controls.Add(this.dateBuyerDate);
            this.panelBuyer.Controls.Add(this.labelControl16);
            this.panelBuyer.Controls.Add(this.txtBuyerLastName);
            this.panelBuyer.Controls.Add(this.labelControl17);
            this.panelBuyer.Controls.Add(this.txtBuyerFirstName);
            this.panelBuyer.Controls.Add(this.labelControl18);
            this.panelBuyer.Controls.Add(this.labelControl19);
            this.panelBuyer.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBuyer.Location = new System.Drawing.Point(0, 608);
            this.panelBuyer.Margin = new System.Windows.Forms.Padding(0);
            this.panelBuyer.Name = "panelBuyer";
            this.panelBuyer.Size = new System.Drawing.Size(412, 97);
            this.panelBuyer.TabIndex = 109;
            // 
            // cmbBuyerCountry
            // 
            this.cmbBuyerCountry.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingAMSACIISF, "ImporterOfPassportIssuanceCountry", true));
            this.cmbBuyerCountry.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bindingAMSACIISF, "ImporterOfPassportIssuanceCountryName", true));
            this.cmbBuyerCountry.EditText = "";
            this.cmbBuyerCountry.EditValue = null;
            this.cmbBuyerCountry.Location = new System.Drawing.Point(135, 3);
            this.cmbBuyerCountry.Name = "cmbBuyerCountry";
            this.cmbBuyerCountry.ReadOnly = false;
            this.cmbBuyerCountry.RefreshButtonToolTip = "";
            this.cmbBuyerCountry.ShowRefreshButton = false;
            this.cmbBuyerCountry.Size = new System.Drawing.Size(269, 21);
            this.cmbBuyerCountry.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbBuyerCountry.TabIndex = 103;
            this.cmbBuyerCountry.ToolTip = "";
            this.cmbBuyerCountry.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // dateBuyerDate
            // 
            this.dateBuyerDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingAMSACIISF, "ImporterOfRecordDOB", true));
            this.dateBuyerDate.EditValue = null;
            this.dateBuyerDate.Location = new System.Drawing.Point(119, 73);
            this.dateBuyerDate.MenuManager = this.barManager1;
            this.dateBuyerDate.Name = "dateBuyerDate";
            this.dateBuyerDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBuyerDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateBuyerDate.Size = new System.Drawing.Size(285, 21);
            this.dateBuyerDate.TabIndex = 108;
            this.dateBuyerDate.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(39, 76);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(74, 14);
            this.labelControl16.TabIndex = 107;
            this.labelControl16.Text = "Date of Birth:";
            // 
            // txtBuyerLastName
            // 
            this.txtBuyerLastName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingAMSACIISF, "ImporterOfRecordLastName", true));
            this.txtBuyerLastName.Location = new System.Drawing.Point(119, 50);
            this.txtBuyerLastName.Name = "txtBuyerLastName";
            this.txtBuyerLastName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuyerLastName.Size = new System.Drawing.Size(285, 21);
            this.txtBuyerLastName.TabIndex = 105;
            this.txtBuyerLastName.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(52, 53);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(61, 14);
            this.labelControl17.TabIndex = 106;
            this.labelControl17.Text = "Last Name:";
            // 
            // txtBuyerFirstName
            // 
            this.txtBuyerFirstName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingAMSACIISF, "ImporterOfRecordFirstName", true));
            this.txtBuyerFirstName.Location = new System.Drawing.Point(119, 27);
            this.txtBuyerFirstName.Name = "txtBuyerFirstName";
            this.txtBuyerFirstName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuyerFirstName.Size = new System.Drawing.Size(285, 21);
            this.txtBuyerFirstName.TabIndex = 102;
            this.txtBuyerFirstName.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(52, 30);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(61, 14);
            this.labelControl18.TabIndex = 104;
            this.labelControl18.Text = "First Name:";
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(12, 8);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(117, 14);
            this.labelControl19.TabIndex = 102;
            this.labelControl19.Text = "Country of Issuance: ";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl3.Controls.Add(this.BuyerDescription);
            this.panelControl3.Controls.Add(this.stxtBuyer);
            this.panelControl3.Controls.Add(this.ckBuyerRef);
            this.panelControl3.Controls.Add(this.ckBuyer);
            this.panelControl3.Controls.Add(this.labelControl3);
            this.panelControl3.Controls.Add(this.txtBuyerImportNumber);
            this.panelControl3.Controls.Add(this.cmbBuyerImportNumber);
            this.panelControl3.Controls.Add(this.lblBuyer);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 378);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(412, 230);
            this.panelControl3.TabIndex = 111;
            // 
            // BuyerDescription
            // 
            this.BuyerDescription.CityID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.BuyerDescription.CustomerDescriptionForAMS = ((ICP.Common.ServiceInterface.DataObjects.CustomerDescriptionForAMS)(resources.GetObject("BuyerDescription.CustomerDescriptionForAMS")));
            this.BuyerDescription.geographyService = null;
            this.BuyerDescription.Location = new System.Drawing.Point(42, 42);
            this.BuyerDescription.Margin = new System.Windows.Forms.Padding(0);
            this.BuyerDescription.MaximumSize = new System.Drawing.Size(373, 175);
            this.BuyerDescription.MinimumSize = new System.Drawing.Size(320, 125);
            this.BuyerDescription.Name = "BuyerDescription";
            this.BuyerDescription.PostalCodeList = null;
            this.BuyerDescription.Size = new System.Drawing.Size(370, 141);
            this.BuyerDescription.TabIndex = 148;
            this.BuyerDescription.ZipCode = null;
            // 
            // stxtBuyer
            // 
            this.stxtBuyer.EditValue = "";
            this.stxtBuyer.Location = new System.Drawing.Point(118, 21);
            this.stxtBuyer.Name = "stxtBuyer";
            this.stxtBuyer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtBuyer.Properties.Appearance.Options.UseBackColor = true;
            this.stxtBuyer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtBuyer.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.stxtBuyer.Size = new System.Drawing.Size(287, 21);
            this.stxtBuyer.TabIndex = 147;
            this.stxtBuyer.Enter += new System.EventHandler(this.stxtBuyer_Enter);
            this.stxtBuyer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.stxtBuyer_KeyDown);
            // 
            // ckBuyerRef
            // 
            this.ckBuyerRef.Location = new System.Drawing.Point(117, 183);
            this.ckBuyerRef.MenuManager = this.barManager1;
            this.ckBuyerRef.Name = "ckBuyerRef";
            this.ckBuyerRef.Properties.Caption = "Same As ISF Importer Ref#";
            this.ckBuyerRef.Size = new System.Drawing.Size(286, 19);
            this.ckBuyerRef.TabIndex = 108;
            this.ckBuyerRef.CheckedChanged += new System.EventHandler(this.ckBuyerRef_CheckedChanged);
            // 
            // ckBuyer
            // 
            this.ckBuyer.Location = new System.Drawing.Point(117, -1);
            this.ckBuyer.MenuManager = this.barManager1;
            this.ckBuyer.Name = "ckBuyer";
            this.ckBuyer.Properties.Caption = "Same As Consignee ";
            this.ckBuyer.Size = new System.Drawing.Size(287, 19);
            this.ckBuyer.TabIndex = 38;
            this.ckBuyer.CheckedChanged += new System.EventHandler(this.ckBuyer_CheckedChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(50, 205);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(57, 14);
            this.labelControl3.TabIndex = 89;
            this.labelControl3.Text = "Importer#";
            // 
            // txtBuyerImportNumber
            // 
            this.txtBuyerImportNumber.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingAMSACIISF, "ImporterOfRecordNumber", true));
            this.txtBuyerImportNumber.EditValue = "";
            this.txtBuyerImportNumber.Location = new System.Drawing.Point(189, 202);
            this.txtBuyerImportNumber.Name = "txtBuyerImportNumber";
            this.txtBuyerImportNumber.Size = new System.Drawing.Size(214, 21);
            this.txtBuyerImportNumber.TabIndex = 88;
            this.txtBuyerImportNumber.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // cmbBuyerImportNumber
            // 
            this.cmbBuyerImportNumber.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingAMSACIISF, "ImporterOfRecordNumberQualifier", true));
            this.cmbBuyerImportNumber.Location = new System.Drawing.Point(117, 202);
            this.cmbBuyerImportNumber.Name = "cmbBuyerImportNumber";
            this.cmbBuyerImportNumber.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbBuyerImportNumber.Properties.Appearance.Options.UseBackColor = true;
            this.cmbBuyerImportNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBuyerImportNumber.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbBuyerImportNumber.Size = new System.Drawing.Size(65, 21);
            this.cmbBuyerImportNumber.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbBuyerImportNumber.TabIndex = 91;
            this.cmbBuyerImportNumber.SelectedIndexChanged += new System.EventHandler(this.cmbImportNumber_SelectedIndexChanged);
            this.cmbBuyerImportNumber.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // lblBuyer
            // 
            this.lblBuyer.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lblBuyer.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblBuyer.Appearance.Options.UseFont = true;
            this.lblBuyer.Appearance.Options.UseForeColor = true;
            this.lblBuyer.Location = new System.Drawing.Point(66, 21);
            this.lblBuyer.Name = "lblBuyer";
            this.lblBuyer.Size = new System.Drawing.Size(38, 18);
            this.lblBuyer.TabIndex = 10;
            this.lblBuyer.Text = "Buyer";
            // 
            // panelConsignee
            // 
            this.panelConsignee.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelConsignee.Controls.Add(this.cmbConsigneeCountry);
            this.panelConsignee.Controls.Add(this.dateConsigneeDate);
            this.panelConsignee.Controls.Add(this.labelControl12);
            this.panelConsignee.Controls.Add(this.txtConsigneeLastName);
            this.panelConsignee.Controls.Add(this.labelControl13);
            this.panelConsignee.Controls.Add(this.txtConsigneeFirstName);
            this.panelConsignee.Controls.Add(this.labelControl14);
            this.panelConsignee.Controls.Add(this.labelControl15);
            this.panelConsignee.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelConsignee.Location = new System.Drawing.Point(0, 282);
            this.panelConsignee.Margin = new System.Windows.Forms.Padding(0);
            this.panelConsignee.Name = "panelConsignee";
            this.panelConsignee.Size = new System.Drawing.Size(412, 96);
            this.panelConsignee.TabIndex = 108;
            // 
            // cmbConsigneeCountry
            // 
            this.cmbConsigneeCountry.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingAMSACIISF, "ConsigneePassportIssuanceCountry", true));
            this.cmbConsigneeCountry.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bindingAMSACIISF, "ConsigneePassportIssuanceCountryName", true));
            this.cmbConsigneeCountry.EditText = "";
            this.cmbConsigneeCountry.EditValue = null;
            this.cmbConsigneeCountry.Location = new System.Drawing.Point(135, 2);
            this.cmbConsigneeCountry.Name = "cmbConsigneeCountry";
            this.cmbConsigneeCountry.ReadOnly = false;
            this.cmbConsigneeCountry.RefreshButtonToolTip = "";
            this.cmbConsigneeCountry.ShowRefreshButton = false;
            this.cmbConsigneeCountry.Size = new System.Drawing.Size(269, 21);
            this.cmbConsigneeCountry.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbConsigneeCountry.TabIndex = 103;
            this.cmbConsigneeCountry.ToolTip = "";
            this.cmbConsigneeCountry.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // dateConsigneeDate
            // 
            this.dateConsigneeDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingAMSACIISF, "ConsigneePassportDOB", true));
            this.dateConsigneeDate.EditValue = null;
            this.dateConsigneeDate.Location = new System.Drawing.Point(119, 72);
            this.dateConsigneeDate.MenuManager = this.barManager1;
            this.dateConsigneeDate.Name = "dateConsigneeDate";
            this.dateConsigneeDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateConsigneeDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateConsigneeDate.Size = new System.Drawing.Size(285, 21);
            this.dateConsigneeDate.TabIndex = 108;
            this.dateConsigneeDate.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(39, 75);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(74, 14);
            this.labelControl12.TabIndex = 107;
            this.labelControl12.Text = "Date of Birth:";
            // 
            // txtConsigneeLastName
            // 
            this.txtConsigneeLastName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingAMSACIISF, "ConsigneeLastName", true));
            this.txtConsigneeLastName.Location = new System.Drawing.Point(119, 49);
            this.txtConsigneeLastName.Name = "txtConsigneeLastName";
            this.txtConsigneeLastName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConsigneeLastName.Size = new System.Drawing.Size(285, 21);
            this.txtConsigneeLastName.TabIndex = 105;
            this.txtConsigneeLastName.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(52, 52);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(61, 14);
            this.labelControl13.TabIndex = 106;
            this.labelControl13.Text = "Last Name:";
            // 
            // txtConsigneeFirstName
            // 
            this.txtConsigneeFirstName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingAMSACIISF, "ConsigneeFirstName", true));
            this.txtConsigneeFirstName.Location = new System.Drawing.Point(119, 26);
            this.txtConsigneeFirstName.Name = "txtConsigneeFirstName";
            this.txtConsigneeFirstName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConsigneeFirstName.Size = new System.Drawing.Size(285, 21);
            this.txtConsigneeFirstName.TabIndex = 102;
            this.txtConsigneeFirstName.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(52, 29);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(61, 14);
            this.labelControl14.TabIndex = 104;
            this.labelControl14.Text = "First Name:";
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(12, 7);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(117, 14);
            this.labelControl15.TabIndex = 102;
            this.labelControl15.Text = "Country of Issuance: ";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.ConsigneeDescription);
            this.panelControl2.Controls.Add(this.stxtAMSConsignee);
            this.panelControl2.Controls.Add(this.labAMSConsignee);
            this.panelControl2.Controls.Add(this.ckConsigneeRef);
            this.panelControl2.Controls.Add(this.cmbConsigneeNumber);
            this.panelControl2.Controls.Add(this.txtConsigneeNumber);
            this.panelControl2.Controls.Add(this.lblCneeNumber);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 71);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(412, 211);
            this.panelControl2.TabIndex = 110;
            // 
            // ConsigneeDescription
            // 
            this.ConsigneeDescription.CityID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ConsigneeDescription.CustomerDescriptionForAMS = ((ICP.Common.ServiceInterface.DataObjects.CustomerDescriptionForAMS)(resources.GetObject("ConsigneeDescription.CustomerDescriptionForAMS")));
            this.ConsigneeDescription.geographyService = null;
            this.ConsigneeDescription.Location = new System.Drawing.Point(41, 24);
            this.ConsigneeDescription.Margin = new System.Windows.Forms.Padding(0);
            this.ConsigneeDescription.MaximumSize = new System.Drawing.Size(373, 175);
            this.ConsigneeDescription.MinimumSize = new System.Drawing.Size(320, 125);
            this.ConsigneeDescription.Name = "ConsigneeDescription";
            this.ConsigneeDescription.PostalCodeList = null;
            this.ConsigneeDescription.Size = new System.Drawing.Size(368, 141);
            this.ConsigneeDescription.TabIndex = 147;
            this.ConsigneeDescription.ZipCode = null;
            // 
            // stxtAMSConsignee
            // 
            this.stxtAMSConsignee.EditValue = "";
            this.stxtAMSConsignee.Location = new System.Drawing.Point(117, 3);
            this.stxtAMSConsignee.Name = "stxtAMSConsignee";
            this.stxtAMSConsignee.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtAMSConsignee.Properties.Appearance.Options.UseBackColor = true;
            this.stxtAMSConsignee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtAMSConsignee.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.stxtAMSConsignee.Size = new System.Drawing.Size(287, 21);
            this.stxtAMSConsignee.TabIndex = 146;
            this.stxtAMSConsignee.Enter += new System.EventHandler(this.stxtAMSConsignee_Enter);
            this.stxtAMSConsignee.KeyDown += new System.Windows.Forms.KeyEventHandler(this.stxtAMSConsignee_KeyDown);
            // 
            // labAMSConsignee
            // 
            this.labAMSConsignee.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labAMSConsignee.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labAMSConsignee.Appearance.Options.UseFont = true;
            this.labAMSConsignee.Appearance.Options.UseForeColor = true;
            this.labAMSConsignee.Location = new System.Drawing.Point(38, 3);
            this.labAMSConsignee.Name = "labAMSConsignee";
            this.labAMSConsignee.Size = new System.Drawing.Size(66, 18);
            this.labAMSConsignee.TabIndex = 67;
            this.labAMSConsignee.Text = "Consignee";
            // 
            // ckConsigneeRef
            // 
            this.ckConsigneeRef.Location = new System.Drawing.Point(115, 163);
            this.ckConsigneeRef.MenuManager = this.barManager1;
            this.ckConsigneeRef.Name = "ckConsigneeRef";
            this.ckConsigneeRef.Properties.Caption = "Same As ISF Importer Ref#";
            this.ckConsigneeRef.Size = new System.Drawing.Size(286, 19);
            this.ckConsigneeRef.TabIndex = 107;
            this.ckConsigneeRef.CheckedChanged += new System.EventHandler(this.ckConsigneeRef_CheckedChanged);
            // 
            // cmbConsigneeNumber
            // 
            this.cmbConsigneeNumber.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingAMSACIISF, "ConsigneeNumberQualifier", true));
            this.cmbConsigneeNumber.Location = new System.Drawing.Point(116, 184);
            this.cmbConsigneeNumber.Name = "cmbConsigneeNumber";
            this.cmbConsigneeNumber.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbConsigneeNumber.Properties.Appearance.Options.UseBackColor = true;
            this.cmbConsigneeNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbConsigneeNumber.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbConsigneeNumber.Size = new System.Drawing.Size(65, 21);
            this.cmbConsigneeNumber.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbConsigneeNumber.TabIndex = 106;
            this.cmbConsigneeNumber.SelectedIndexChanged += new System.EventHandler(this.cmbConsigneeNumber_SelectedIndexChanged);
            this.cmbConsigneeNumber.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // txtConsigneeNumber
            // 
            this.txtConsigneeNumber.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingAMSACIISF, "ConsigneeNumber", true));
            this.txtConsigneeNumber.EditValue = "";
            this.txtConsigneeNumber.Location = new System.Drawing.Point(186, 184);
            this.txtConsigneeNumber.Name = "txtConsigneeNumber";
            this.txtConsigneeNumber.Size = new System.Drawing.Size(214, 21);
            this.txtConsigneeNumber.TabIndex = 104;
            this.txtConsigneeNumber.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // lblCneeNumber
            // 
            this.lblCneeNumber.Location = new System.Drawing.Point(42, 185);
            this.lblCneeNumber.Name = "lblCneeNumber";
            this.lblCneeNumber.Size = new System.Drawing.Size(65, 14);
            this.lblCneeNumber.TabIndex = 105;
            this.lblCneeNumber.Text = "Consignee#";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.txtBondRefNumber);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.cmbBondRef);
            this.panelControl1.Controls.Add(this.cmbBondActivityCode);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 24);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(412, 47);
            this.panelControl1.TabIndex = 109;
            // 
            // txtBondRefNumber
            // 
            this.txtBondRefNumber.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingAMSACIISF, "BondReferenceNumber", true));
            this.txtBondRefNumber.EditValue = "";
            this.txtBondRefNumber.Location = new System.Drawing.Point(188, 1);
            this.txtBondRefNumber.Name = "txtBondRefNumber";
            this.txtBondRefNumber.Size = new System.Drawing.Size(214, 21);
            this.txtBondRefNumber.TabIndex = 32;
            this.txtBondRefNumber.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(53, 2);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(55, 14);
            this.labelControl4.TabIndex = 33;
            this.labelControl4.Text = "BondRef#";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(6, 29);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(104, 14);
            this.labelControl5.TabIndex = 35;
            this.labelControl5.Text = "Bond Activity Code";
            // 
            // cmbBondRef
            // 
            this.cmbBondRef.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingAMSACIISF, "BondReferenceType", true));
            this.cmbBondRef.Location = new System.Drawing.Point(117, 1);
            this.cmbBondRef.Name = "cmbBondRef";
            this.cmbBondRef.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbBondRef.Properties.Appearance.Options.UseBackColor = true;
            this.cmbBondRef.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBondRef.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbBondRef.Size = new System.Drawing.Size(65, 21);
            this.cmbBondRef.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbBondRef.TabIndex = 34;
            this.cmbBondRef.SelectedIndexChanged += new System.EventHandler(this.cmbBondRef_SelectedIndexChanged);
            this.cmbBondRef.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // cmbBondActivityCode
            // 
            this.cmbBondActivityCode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingAMSACIISF, "BondActivityCode", true));
            this.cmbBondActivityCode.Location = new System.Drawing.Point(117, 24);
            this.cmbBondActivityCode.Name = "cmbBondActivityCode";
            this.cmbBondActivityCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbBondActivityCode.Properties.Appearance.Options.UseBackColor = true;
            this.cmbBondActivityCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBondActivityCode.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbBondActivityCode.Size = new System.Drawing.Size(287, 21);
            this.cmbBondActivityCode.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbBondActivityCode.TabIndex = 36;
            this.cmbBondActivityCode.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // panelControl8
            // 
            this.panelControl8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl8.Controls.Add(this.labelControl21);
            this.panelControl8.Controls.Add(this.cmbCargoType);
            this.panelControl8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl8.Location = new System.Drawing.Point(0, 0);
            this.panelControl8.Name = "panelControl8";
            this.panelControl8.Size = new System.Drawing.Size(412, 24);
            this.panelControl8.TabIndex = 112;
            // 
            // labelControl21
            // 
            this.labelControl21.Location = new System.Drawing.Point(44, 5);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(63, 14);
            this.labelControl21.TabIndex = 38;
            this.labelControl21.Text = "Cargo Type";
            // 
            // cmbCargoType
            // 
            this.cmbCargoType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingAMSACIISF, "CargoTypeForAMS", true));
            this.cmbCargoType.Location = new System.Drawing.Point(116, 2);
            this.cmbCargoType.Name = "cmbCargoType";
            this.cmbCargoType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCargoType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCargoType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCargoType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCargoType.Size = new System.Drawing.Size(285, 21);
            this.cmbCargoType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCargoType.TabIndex = 37;
            this.cmbCargoType.SelectedIndexChanged += new System.EventHandler(this.cmbCargoType_SelectedIndexChanged);
            this.cmbCargoType.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // panelAddAMSACIISF
            // 
            this.panelAddAMSACIISF.Controls.Add(this.gcAMSACIISF);
            this.panelAddAMSACIISF.Controls.Add(this.barDockControl3);
            this.panelAddAMSACIISF.Controls.Add(this.barDockControl4);
            this.panelAddAMSACIISF.Controls.Add(this.barDockControl2);
            this.panelAddAMSACIISF.Controls.Add(this.barDockControl1);
            this.panelAddAMSACIISF.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAddAMSACIISF.Location = new System.Drawing.Point(0, 125);
            this.panelAddAMSACIISF.Margin = new System.Windows.Forms.Padding(0);
            this.panelAddAMSACIISF.Name = "panelAddAMSACIISF";
            this.panelAddAMSACIISF.Size = new System.Drawing.Size(848, 146);
            this.panelAddAMSACIISF.TabIndex = 71;
            // 
            // gcAMSACIISF
            // 
            this.gcAMSACIISF.DataSource = this.bindingAMSACIISF;
            this.gcAMSACIISF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcAMSACIISF.Location = new System.Drawing.Point(2, 28);
            this.gcAMSACIISF.MainView = this.gvAMSACIISF;
            this.gcAMSACIISF.Name = "gcAMSACIISF";
            this.gcAMSACIISF.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox});
            this.gcAMSACIISF.Size = new System.Drawing.Size(844, 116);
            this.gcAMSACIISF.TabIndex = 84;
            this.gcAMSACIISF.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAMSACIISF});
            // 
            // gvAMSACIISF
            // 
            this.gvAMSACIISF.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvAMSACIISF.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.colShipperDesc,
            this.colConsigneeDesc,
            this.colSellerDesc,
            this.colBuyerDesc,
            this.colShipToDesc,
            this.colManufacturerDesc,
            this.colStuffingLocationDesc,
            this.colConsolidatorDesc});
            this.gvAMSACIISF.GridControl = this.gcAMSACIISF;
            this.gvAMSACIISF.IndicatorWidth = 30;
            this.gvAMSACIISF.Name = "gvAMSACIISF";
            this.gvAMSACIISF.OptionsView.ColumnAutoWidth = false;
            this.gvAMSACIISF.OptionsView.ShowDetailButtons = false;
            this.gvAMSACIISF.OptionsView.ShowGroupPanel = false;
            this.gvAMSACIISF.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvAMSACIISF_FocusedRowChanged);
            this.gvAMSACIISF.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvAMSACIISF_CellValueChanging);
            // 
            // gridColumn11
            // 
            this.gridColumn11.ColumnEdit = this.repositoryItemComboBox;
            this.gridColumn11.FieldName = "Mark";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 0;
            this.gridColumn11.Width = 46;
            // 
            // repositoryItemComboBox
            // 
            this.repositoryItemComboBox.AutoHeight = false;
            this.repositoryItemComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G"});
            this.repositoryItemComboBox.Name = "repositoryItemComboBox";
            // 
            // colShipperDesc
            // 
            this.colShipperDesc.FieldName = "ShipperDesc";
            this.colShipperDesc.Name = "colShipperDesc";
            this.colShipperDesc.OptionsColumn.AllowEdit = false;
            this.colShipperDesc.OptionsColumn.AllowFocus = false;
            this.colShipperDesc.OptionsColumn.ReadOnly = true;
            this.colShipperDesc.Visible = true;
            this.colShipperDesc.VisibleIndex = 1;
            // 
            // colConsigneeDesc
            // 
            this.colConsigneeDesc.FieldName = "ConsigneeDesc";
            this.colConsigneeDesc.Name = "colConsigneeDesc";
            this.colConsigneeDesc.OptionsColumn.AllowEdit = false;
            this.colConsigneeDesc.OptionsColumn.AllowFocus = false;
            this.colConsigneeDesc.OptionsColumn.ReadOnly = true;
            this.colConsigneeDesc.Visible = true;
            this.colConsigneeDesc.VisibleIndex = 2;
            // 
            // colSellerDesc
            // 
            this.colSellerDesc.FieldName = "SellerDesc";
            this.colSellerDesc.Name = "colSellerDesc";
            this.colSellerDesc.OptionsColumn.AllowEdit = false;
            this.colSellerDesc.OptionsColumn.AllowFocus = false;
            this.colSellerDesc.OptionsColumn.ReadOnly = true;
            this.colSellerDesc.Visible = true;
            this.colSellerDesc.VisibleIndex = 3;
            // 
            // colBuyerDesc
            // 
            this.colBuyerDesc.FieldName = "BuyerDesc";
            this.colBuyerDesc.Name = "colBuyerDesc";
            this.colBuyerDesc.OptionsColumn.AllowEdit = false;
            this.colBuyerDesc.OptionsColumn.AllowFocus = false;
            this.colBuyerDesc.OptionsColumn.ReadOnly = true;
            this.colBuyerDesc.Visible = true;
            this.colBuyerDesc.VisibleIndex = 4;
            // 
            // colShipToDesc
            // 
            this.colShipToDesc.FieldName = "ShipToDesc";
            this.colShipToDesc.Name = "colShipToDesc";
            this.colShipToDesc.OptionsColumn.AllowEdit = false;
            this.colShipToDesc.OptionsColumn.AllowFocus = false;
            this.colShipToDesc.OptionsColumn.ReadOnly = true;
            this.colShipToDesc.Visible = true;
            this.colShipToDesc.VisibleIndex = 5;
            // 
            // colManufacturerDesc
            // 
            this.colManufacturerDesc.FieldName = "ManufacturerDesc";
            this.colManufacturerDesc.Name = "colManufacturerDesc";
            this.colManufacturerDesc.OptionsColumn.AllowEdit = false;
            this.colManufacturerDesc.OptionsColumn.AllowFocus = false;
            this.colManufacturerDesc.OptionsColumn.ReadOnly = true;
            this.colManufacturerDesc.Visible = true;
            this.colManufacturerDesc.VisibleIndex = 6;
            // 
            // colStuffingLocationDesc
            // 
            this.colStuffingLocationDesc.FieldName = "StuffingLocationDesc";
            this.colStuffingLocationDesc.Name = "colStuffingLocationDesc";
            this.colStuffingLocationDesc.OptionsColumn.AllowEdit = false;
            this.colStuffingLocationDesc.OptionsColumn.AllowFocus = false;
            this.colStuffingLocationDesc.OptionsColumn.ReadOnly = true;
            this.colStuffingLocationDesc.Visible = true;
            this.colStuffingLocationDesc.VisibleIndex = 7;
            // 
            // colConsolidatorDesc
            // 
            this.colConsolidatorDesc.FieldName = "ConsolidatorDesc";
            this.colConsolidatorDesc.Name = "colConsolidatorDesc";
            this.colConsolidatorDesc.OptionsColumn.AllowEdit = false;
            this.colConsolidatorDesc.OptionsColumn.AllowFocus = false;
            this.colConsolidatorDesc.OptionsColumn.ReadOnly = true;
            this.colConsolidatorDesc.Visible = true;
            this.colConsolidatorDesc.VisibleIndex = 8;
            // 
            // barDockControl3
            // 
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(2, 28);
            this.barDockControl3.Size = new System.Drawing.Size(0, 116);
            // 
            // barDockControl4
            // 
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(846, 28);
            this.barDockControl4.Size = new System.Drawing.Size(0, 116);
            // 
            // barDockControl2
            // 
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(2, 144);
            this.barDockControl2.Size = new System.Drawing.Size(844, 0);
            // 
            // barDockControl1
            // 
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(2, 2);
            this.barDockControl1.Size = new System.Drawing.Size(844, 26);
            // 
            // panelTop
            // 
            this.panelTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelTop.Controls.Add(this.txtFirstPortOfCallDate);
            this.panelTop.Controls.Add(this.txtETD);
            this.panelTop.Controls.Add(this.labelControl31);
            this.panelTop.Controls.Add(this.txtPOL);
            this.panelTop.Controls.Add(this.txtFirstPortOfCall);
            this.panelTop.Controls.Add(this.labelControl29);
            this.panelTop.Controls.Add(this.labelControl27);
            this.panelTop.Controls.Add(this.txtLastPortOfCall);
            this.panelTop.Controls.Add(this.txtVoyageNumber);
            this.panelTop.Controls.Add(this.labelControl28);
            this.panelTop.Controls.Add(this.txtVesselName);
            this.panelTop.Controls.Add(this.labelControl20);
            this.panelTop.Controls.Add(this.txtAMSNO);
            this.panelTop.Controls.Add(this.txtIMO);
            this.panelTop.Controls.Add(this.labelControl7);
            this.panelTop.Controls.Add(this.simpleButton1);
            this.panelTop.Controls.Add(this.labAMSEntryType);
            this.panelTop.Controls.Add(this.labAMSNO);
            this.panelTop.Controls.Add(this.cmbACIEntryType);
            this.panelTop.Controls.Add(this.cmbAMSEntryType);
            this.panelTop.Controls.Add(this.txtISFNO);
            this.panelTop.Controls.Add(this.labACIEntryType);
            this.panelTop.Controls.Add(this.mscmbCountry);
            this.panelTop.Controls.Add(this.labelControl6);
            this.panelTop.Controls.Add(this.labISFNO);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(848, 125);
            this.panelTop.TabIndex = 0;
            // 
            // txtFirstPortOfCallDate
            // 
            this.txtFirstPortOfCallDate.EditValue = null;
            this.txtFirstPortOfCallDate.Location = new System.Drawing.Point(724, 99);
            this.txtFirstPortOfCallDate.Name = "txtFirstPortOfCallDate";
            this.txtFirstPortOfCallDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFirstPortOfCallDate.Properties.Mask.EditMask = "";
            this.txtFirstPortOfCallDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtFirstPortOfCallDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtFirstPortOfCallDate.Size = new System.Drawing.Size(92, 21);
            this.txtFirstPortOfCallDate.TabIndex = 119;
            this.txtFirstPortOfCallDate.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // txtETD
            // 
            this.txtETD.EditValue = null;
            this.txtETD.Location = new System.Drawing.Point(212, 99);
            this.txtETD.Name = "txtETD";
            this.txtETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtETD.Properties.Mask.EditMask = "";
            this.txtETD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtETD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtETD.Size = new System.Drawing.Size(92, 21);
            this.txtETD.TabIndex = 118;
            this.txtETD.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl31
            // 
            this.labelControl31.Location = new System.Drawing.Point(37, 102);
            this.labelControl31.Name = "labelControl31";
            this.labelControl31.Size = new System.Drawing.Size(86, 14);
            this.labelControl31.TabIndex = 116;
            this.labelControl31.Text = "Port Of Loading";
            // 
            // labelControl29
            // 
            this.labelControl29.Location = new System.Drawing.Point(549, 102);
            this.labelControl29.Name = "labelControl29";
            this.labelControl29.Size = new System.Drawing.Size(87, 14);
            this.labelControl29.TabIndex = 112;
            this.labelControl29.Text = "First Port Of Call";
            // 
            // labelControl27
            // 
            this.labelControl27.Location = new System.Drawing.Point(349, 102);
            this.labelControl27.Name = "labelControl27";
            this.labelControl27.Size = new System.Drawing.Size(87, 14);
            this.labelControl27.TabIndex = 111;
            this.labelControl27.Text = "Last Port Of Call";
            // 
            // txtVoyageNumber
            // 
            this.txtVoyageNumber.Location = new System.Drawing.Point(540, 52);
            this.txtVoyageNumber.Name = "txtVoyageNumber";
            this.txtVoyageNumber.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVoyageNumber.Size = new System.Drawing.Size(285, 21);
            this.txtVoyageNumber.TabIndex = 109;
            this.txtVoyageNumber.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl28
            // 
            this.labelControl28.Location = new System.Drawing.Point(444, 55);
            this.labelControl28.Name = "labelControl28";
            this.labelControl28.Size = new System.Drawing.Size(88, 14);
            this.labelControl28.TabIndex = 108;
            this.labelControl28.Text = "Voyage Number";
            // 
            // txtVesselName
            // 
            this.txtVesselName.Location = new System.Drawing.Point(132, 52);
            this.txtVesselName.Name = "txtVesselName";
            this.txtVesselName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVesselName.Size = new System.Drawing.Size(285, 21);
            this.txtVesselName.TabIndex = 102;
            this.txtVesselName.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(61, 55);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(65, 14);
            this.labelControl20.TabIndex = 101;
            this.labelControl20.Text = "VesselName";
            // 
            // txtAMSNO
            // 
            this.txtAMSNO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "AMSNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtAMSNO.EditValue = "";
            this.txtAMSNO.Location = new System.Drawing.Point(132, 6);
            this.txtAMSNO.Name = "txtAMSNO";
            this.txtAMSNO.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAMSNO.Size = new System.Drawing.Size(285, 21);
            this.txtAMSNO.TabIndex = 88;
            this.txtAMSNO.EditValueChanged += new System.EventHandler(this.txtAMSNO_EditValueChanged);
            // 
            // txtIMO
            // 
            this.txtIMO.Location = new System.Drawing.Point(132, 75);
            this.txtIMO.Name = "txtIMO";
            this.txtIMO.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIMO.Size = new System.Drawing.Size(285, 21);
            this.txtIMO.TabIndex = 97;
            this.txtIMO.EditValueChanged += new System.EventHandler(this.txtSH1_EditValueChanged);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(102, 78);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(22, 14);
            this.labelControl7.TabIndex = 100;
            this.labelControl7.Text = "IMO";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(3, 73);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(95, 23);
            this.simpleButton1.TabIndex = 99;
            this.simpleButton1.Text = "GET IMO&&Flag";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labAMSEntryType
            // 
            this.labAMSEntryType.Location = new System.Drawing.Point(17, 28);
            this.labAMSEntryType.Name = "labAMSEntryType";
            this.labAMSEntryType.Size = new System.Drawing.Size(107, 14);
            this.labAMSEntryType.TabIndex = 92;
            this.labAMSEntryType.Text = "AMS/ISF EntryType";
            // 
            // labAMSNO
            // 
            this.labAMSNO.Location = new System.Drawing.Point(79, 7);
            this.labAMSNO.Name = "labAMSNO";
            this.labAMSNO.Size = new System.Drawing.Size(45, 14);
            this.labAMSNO.TabIndex = 91;
            this.labAMSNO.Text = "AMS NO";
            // 
            // cmbAMSEntryType
            // 
            this.cmbAMSEntryType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "AMSEntry", true));
            this.cmbAMSEntryType.Location = new System.Drawing.Point(132, 29);
            this.cmbAMSEntryType.Name = "cmbAMSEntryType";
            this.cmbAMSEntryType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbAMSEntryType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbAMSEntryType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAMSEntryType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbAMSEntryType.Size = new System.Drawing.Size(285, 21);
            this.cmbAMSEntryType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbAMSEntryType.TabIndex = 94;
            this.cmbAMSEntryType.SelectedIndexChanged += new System.EventHandler(this.cmbAMSEntryType_SelectedIndexChanged);
            // 
            // txtISFNO
            // 
            this.txtISFNO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ISFNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtISFNO.EditValue = "";
            this.txtISFNO.Location = new System.Drawing.Point(540, 6);
            this.txtISFNO.Name = "txtISFNO";
            this.txtISFNO.Size = new System.Drawing.Size(285, 21);
            this.txtISFNO.TabIndex = 89;
            // 
            // labACIEntryType
            // 
            this.labACIEntryType.Location = new System.Drawing.Point(452, 30);
            this.labACIEntryType.Name = "labACIEntryType";
            this.labACIEntryType.Size = new System.Drawing.Size(80, 14);
            this.labACIEntryType.TabIndex = 93;
            this.labACIEntryType.Text = "ACI EntryType";
            // 
            // mscmbCountry
            // 
            this.mscmbCountry.EditText = "";
            this.mscmbCountry.EditValue = null;
            this.mscmbCountry.Location = new System.Drawing.Point(540, 75);
            this.mscmbCountry.Name = "mscmbCountry";
            this.mscmbCountry.ReadOnly = false;
            this.mscmbCountry.RefreshButtonToolTip = "";
            this.mscmbCountry.ShowRefreshButton = false;
            this.mscmbCountry.Size = new System.Drawing.Size(285, 21);
            this.mscmbCountry.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscmbCountry.TabIndex = 96;
            this.mscmbCountry.ToolTip = "";
            this.mscmbCountry.EditValueChanged += new System.EventHandler(this.mscmbHS1_EditValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(511, 78);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(21, 14);
            this.labelControl6.TabIndex = 98;
            this.labelControl6.Text = "Flag";
            // 
            // labISFNO
            // 
            this.labISFNO.Location = new System.Drawing.Point(494, 7);
            this.labISFNO.Name = "labISFNO";
            this.labISFNO.Size = new System.Drawing.Size(38, 14);
            this.labISFNO.TabIndex = 90;
            this.labISFNO.Text = "ISF NO";
            // 
            // panelScroll
            // 
            this.panelScroll.Controls.Add(this.xtraTabControl1);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.Location = new System.Drawing.Point(0, 53);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(1400, 1324);
            this.panelScroll.TabIndex = 5;
            // 
            // bar4
            // 
            this.bar4.BarName = "Status bar";
            this.bar4.DockCol = 0;
            this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar4.Text = "Status bar";
            // 
            // bar3
            // 
            this.bar3.BarName = "Main menu";
            this.bar3.DockCol = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.Text = "Main menu";
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Tools";
            // 
            // barAddContainer
            // 
            this.barAddContainer.Caption = "Add Container";
            this.barAddContainer.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Add;
            this.barAddContainer.Id = 0;
            this.barAddContainer.Name = "barAddContainer";
            this.barAddContainer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddContainer_ItemClick);
            // 
            // barDelContainer
            // 
            this.barDelContainer.Caption = "Delete Container";
            this.barDelContainer.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Delete_16;
            this.barDelContainer.Id = 1;
            this.barDelContainer.Name = "barDelContainer";
            this.barDelContainer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelContainer_ItemClick);
            // 
            // bar7
            // 
            this.bar7.BarName = "Main menu";
            this.bar7.DockCol = 0;
            this.bar7.DockRow = 0;
            this.bar7.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar7.FloatLocation = new System.Drawing.Point(386, 313);
            this.bar7.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAddContainer, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelContainer, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar7.OptionsBar.MultiLine = true;
            this.bar7.OptionsBar.UseWholeRow = true;
            this.bar7.Text = "Main menu";
            // 
            // barContainerForAMS
            // 
            this.barContainerForAMS.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar7});
            this.barContainerForAMS.DockControls.Add(this.barDockControl5);
            this.barContainerForAMS.DockControls.Add(this.barDockControl6);
            this.barContainerForAMS.DockControls.Add(this.barDockControl7);
            this.barContainerForAMS.DockControls.Add(this.barDockControl8);
            this.barContainerForAMS.Form = this.groupControl2;
            this.barContainerForAMS.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAddContainer,
            this.barDelContainer});
            this.barContainerForAMS.MainMenu = this.bar7;
            this.barContainerForAMS.MaxItemId = 2;
            // 
            // barAddAMSACIISF
            // 
            this.barAddAMSACIISF.Caption = "Add AMS&&ACI&&ISFInfo";
            this.barAddAMSACIISF.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Add;
            this.barAddAMSACIISF.Id = 0;
            this.barAddAMSACIISF.Name = "barAddAMSACIISF";
            this.barAddAMSACIISF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddAMSACIISF_ItemClick);
            // 
            // barDeleteAMSACIISF
            // 
            this.barDeleteAMSACIISF.Caption = "Delete AMS&&ACI&&ISFInfo";
            this.barDeleteAMSACIISF.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Delete_16;
            this.barDeleteAMSACIISF.Id = 1;
            this.barDeleteAMSACIISF.Name = "barDeleteAMSACIISF";
            this.barDeleteAMSACIISF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // bar6
            // 
            this.bar6.BarName = "Main menu";
            this.bar6.DockCol = 0;
            this.bar6.DockRow = 0;
            this.bar6.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar6.FloatLocation = new System.Drawing.Point(31, 210);
            this.bar6.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAddAMSACIISF, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDeleteAMSACIISF, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar6.OptionsBar.MultiLine = true;
            this.bar6.OptionsBar.UseWholeRow = true;
            this.bar6.Text = "Main menu";
            // 
            // barAMSACIISFInfo
            // 
            this.barAMSACIISFInfo.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar6});
            this.barAMSACIISFInfo.DockControls.Add(this.barDockControl1);
            this.barAMSACIISFInfo.DockControls.Add(this.barDockControl2);
            this.barAMSACIISFInfo.DockControls.Add(this.barDockControl3);
            this.barAMSACIISFInfo.DockControls.Add(this.barDockControl4);
            this.barAMSACIISFInfo.Form = this.panelAddAMSACIISF;
            this.barAMSACIISFInfo.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAddAMSACIISF,
            this.barDeleteAMSACIISF});
            this.barAMSACIISFInfo.MainMenu = this.bar6;
            this.barAMSACIISFInfo.MaxItemId = 3;
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.lwGridControl1);
            this.panelControl5.Controls.Add(this.barDockControl9);
            this.panelControl5.Controls.Add(this.barDockControl10);
            this.panelControl5.Controls.Add(this.barDockControl11);
            this.panelControl5.Location = new System.Drawing.Point(0, 0);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(200, 100);
            this.panelControl5.TabIndex = 0;
            // 
            // lwGridControl1
            // 
            this.lwGridControl1.DataSource = this.bindingAMSACIISF;
            this.lwGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lwGridControl1.Location = new System.Drawing.Point(2, 2);
            this.lwGridControl1.MainView = this.gridView1;
            this.lwGridControl1.Name = "lwGridControl1";
            this.lwGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.repositoryItemCheckEdit2,
            this.repositoryItemImageComboBox4,
            this.repositoryItemTextEdit3,
            this.repositoryItemSpinEdit2,
            this.repositoryItemSpinEdit5,
            this.repositoryItemMemoEdit3,
            this.repositoryItemComboBox1});
            this.lwGridControl1.Size = new System.Drawing.Size(196, 96);
            this.lwGridControl1.TabIndex = 82;
            this.lwGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.GridControl = this.lwGridControl1;
            this.gridView1.IndicatorWidth = 30;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mark";
            this.gridColumn1.ColumnEdit = this.repositoryItemComboBox1;
            this.gridColumn1.FieldName = "Mark";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 51;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Shipper";
            this.gridColumn2.ColumnEdit = this.repositoryItemTextEdit3;
            this.gridColumn2.FieldName = "ShipperDesc";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 120;
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Consignee";
            this.gridColumn3.ColumnEdit = this.repositoryItemTextEdit3;
            this.gridColumn3.FieldName = "ConsigneeDesc";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 120;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "NotifyParty";
            this.gridColumn4.ColumnEdit = this.repositoryItemTextEdit3;
            this.gridColumn4.FieldName = "NotifyPartyDesc";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 120;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // repositoryItemImageComboBox4
            // 
            this.repositoryItemImageComboBox4.AutoHeight = false;
            this.repositoryItemImageComboBox4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox4.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("A", "A", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("B", "B", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("C", "C", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("D", "D", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("E", "E", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("F", "F", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("G", "G", -1)});
            this.repositoryItemImageComboBox4.Name = "repositoryItemImageComboBox4";
            this.repositoryItemImageComboBox4.NullText = "A";
            // 
            // repositoryItemSpinEdit2
            // 
            this.repositoryItemSpinEdit2.AutoHeight = false;
            this.repositoryItemSpinEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit2.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.repositoryItemSpinEdit2.Name = "repositoryItemSpinEdit2";
            // 
            // repositoryItemSpinEdit5
            // 
            this.repositoryItemSpinEdit5.AutoHeight = false;
            this.repositoryItemSpinEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit5.IsFloatValue = false;
            this.repositoryItemSpinEdit5.Mask.EditMask = "N00";
            this.repositoryItemSpinEdit5.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.repositoryItemSpinEdit5.Name = "repositoryItemSpinEdit5";
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // barDockControl9
            // 
            this.barDockControl9.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl9.Location = new System.Drawing.Point(2, 2);
            this.barDockControl9.Size = new System.Drawing.Size(0, 96);
            // 
            // barDockControl10
            // 
            this.barDockControl10.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl10.Location = new System.Drawing.Point(198, 2);
            this.barDockControl10.Size = new System.Drawing.Size(0, 96);
            // 
            // barDockControl11
            // 
            this.barDockControl11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl11.Location = new System.Drawing.Point(2, 98);
            this.barDockControl11.Size = new System.Drawing.Size(196, 0);
            // 
            // barDockControl12
            // 
            this.barDockControl12.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl12.Location = new System.Drawing.Point(2, 2);
            this.barDockControl12.Size = new System.Drawing.Size(846, 26);
            // 
            // colMark
            // 
            this.colMark.Caption = "Mark";
            this.colMark.FieldName = "Mark";
            this.colMark.Name = "colMark";
            this.colMark.Visible = true;
            this.colMark.VisibleIndex = 0;
            this.colMark.Width = 51;
            // 
            // Shipper
            // 
            this.Shipper.Caption = "Shipper";
            this.Shipper.FieldName = "ShipperDesc";
            this.Shipper.Name = "Shipper";
            this.Shipper.OptionsColumn.ReadOnly = true;
            this.Shipper.Visible = true;
            this.Shipper.VisibleIndex = 1;
            this.Shipper.Width = 120;
            // 
            // Consignee
            // 
            this.Consignee.Caption = "Consignee";
            this.Consignee.FieldName = "ConsigneeDesc";
            this.Consignee.Name = "Consignee";
            this.Consignee.OptionsColumn.ReadOnly = true;
            this.Consignee.Visible = true;
            this.Consignee.VisibleIndex = 2;
            this.Consignee.Width = 120;
            // 
            // NotifyParty
            // 
            this.NotifyParty.Caption = "NotifyParty";
            this.NotifyParty.FieldName = "NotifyPartyDesc";
            this.NotifyParty.Name = "NotifyParty";
            this.NotifyParty.OptionsColumn.ReadOnly = true;
            this.NotifyParty.Visible = true;
            this.NotifyParty.VisibleIndex = 3;
            this.NotifyParty.Width = 120;
            // 
            // colBuyer
            // 
            this.colBuyer.FieldName = "Buyer";
            this.colBuyer.Name = "colBuyer";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "FreeFormDescription";
            this.gridColumn10.FieldName = "FreeFormDescription";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 202;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "UnitOfMeasure";
            this.gridColumn9.FieldName = "UnitOfMeasure";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.ReadOnly = true;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 0;
            this.gridColumn9.Width = 90;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Quantity";
            this.gridColumn8.FieldName = "Quantity";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            this.gridColumn8.Width = 79;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Kilos";
            this.gridColumn7.FieldName = "Kilos";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Seal";
            this.gridColumn6.FieldName = "Seal";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            this.gridColumn6.Width = 90;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "ContainerNumber";
            this.gridColumn5.FieldName = "ContainerNumber";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 139;
            // 
            // barConfirmedAMS
            // 
            this.barConfirmedAMS.Caption = "Confirmed AMS";
            this.barConfirmedAMS.Id = 36;
            this.barConfirmedAMS.Name = "barConfirmedAMS";
            this.barConfirmedAMS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barConfirmedAMS_ItemClick);
            // 
            // HBLEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelScroll);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "HBLEditPart";
            this.Size = new System.Drawing.Size(1400, 1377);
            this.SmartPartClosing += new System.EventHandler<Microsoft.Practices.CompositeUI.SmartParts.WorkspaceCancelEventArgs>(this.HBLEditPart_SmartPartClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReleaseDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReleaseDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuantityUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtChecker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtIssuePlace.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIssue.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIssue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPODName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOLName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfDeliveryName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotifyPartyDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfReceipt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipperDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtFinalDestination.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinalDestinationName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfReceiptName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtNotifyParty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFreightDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgentDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoodsDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtnQtyInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtRefNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOLCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPODCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbACIEntryType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastPortOfCall.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstPortOfCall.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlacePay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETA.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPETD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPETD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsWoodPacking.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtIsWoodPacking.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtnQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPageBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtScac.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelexNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRleaseBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIssueType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkToAgent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMBLNO.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsBook.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteGateInDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteGateInDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkThirdPlacePay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowVoyage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowPreVoyage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETD.Properties)).EndInit();
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtnInfo.Properties)).EndInit();
            this.navBarGroupControlContainer4.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabPageAMS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelALL)).EndInit();
            this.panelALL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelContainerDetails)).EndInit();
            this.panelContainerDetails.ResumeLayout(false);
            this.panelContainerDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS10.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS9.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS8.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS7.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHS1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAMSContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingContainers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAMSContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBoxType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelContent)).EndInit();
            this.panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).EndInit();
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            this.panelControl7.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsolidator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckConsolidator.Properties)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtStuffingLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckStuffingLocation.Properties)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtManufacturer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckManufacturer.Properties)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckSeller.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtSeller.Properties)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAMSShipper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelISFImporterRef)).EndInit();
            this.panelISFImporterRef.ResumeLayout(false);
            this.panelISFImporterRef.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingAMSACIISF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateISFImporterRefDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateISFImporterRefDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtISFImporterRefLastName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtISFImporterRefFirstName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            this.panelControl6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtISFImporterRef.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbISFImporterRef.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelRight)).EndInit();
            this.panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingPartyInfo.Properties)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipToPatry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckShipTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAMSNotifyPartyDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAMSNotifyParty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBuyer)).EndInit();
            this.panelBuyer.ResumeLayout(false);
            this.panelBuyer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateBuyerDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBuyerDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuyerLastName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuyerFirstName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBuyer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckBuyerRef.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckBuyer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuyerImportNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBuyerImportNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelConsignee)).EndInit();
            this.panelConsignee.ResumeLayout(false);
            this.panelConsignee.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateConsigneeDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateConsigneeDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeLastName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeFirstName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAMSConsignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckConsigneeRef.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbConsigneeNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBondRefNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBondRef.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBondActivityCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl8)).EndInit();
            this.panelControl8.ResumeLayout(false);
            this.panelControl8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCargoType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelAddAMSACIISF)).EndInit();
            this.panelAddAMSACIISF.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAMSACIISF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAMSACIISF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstPortOfCallDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstPortOfCallDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtETD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtETD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoyageNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVesselName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAMSNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIMO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAMSEntryType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtISFNO.Properties)).EndInit();
            this.panelScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barContainerForAMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAMSACIISFInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.LabelControl labHBlNo;
        private DevExpress.XtraEditors.DateEdit dteReleaseDate;
        private DevExpress.XtraEditors.LabelControl labRefNo;
        private DevExpress.XtraEditors.LabelControl labMBLNo;
        private DevExpress.XtraEditors.LabelControl labIssueBy;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbReleaseType;
        private DevExpress.XtraEditors.LabelControl labReleaseType;
        private DevExpress.XtraEditors.LabelControl labIssueDate;
        private DevExpress.XtraEditors.LabelControl labReleaseDate;
        private DevExpress.XtraEditors.LabelControl labIssuePlace;
        private DevExpress.XtraEditors.DateEdit dteIssue;
        private DevExpress.XtraEditors.ButtonEdit stxtIssuePlace;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTransportClause;
        private DevExpress.XtraEditors.LabelControl labTransportClause;
        private DevExpress.XtraEditors.MemoEdit txtNotifyPartyDescription;
        private DevExpress.XtraEditors.MemoEdit txtConsigneeDescription;
        private DevExpress.XtraEditors.MemoEdit txtShipperDescription;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtNotifyParty;
        private DevExpress.XtraEditors.LabelControl labNotifyParty;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtConsignee;
        private DevExpress.XtraEditors.LabelControl labConsignee;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtShipper;
        private DevExpress.XtraEditors.LabelControl labShipper;
        private ICP.Business.Common.UI.BusinessContactPopupContainerEdit stxtChecker;
        private DevExpress.XtraEditors.LabelControl labChecker;
        private DevExpress.XtraEditors.MemoEdit txtAgentDescription;
        private DevExpress.XtraEditors.LabelControl labAgent;
        private DevExpress.XtraEditors.LabelControl labPlaceOfDelivery;
        private ICP.Common.UI.UCVoyageLookupEdit stxtVoyage;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private DevExpress.XtraEditors.LabelControl labPOL2;
        private DevExpress.XtraEditors.DateEdit dtETA;
        private DevExpress.XtraEditors.LabelControl labETA;
        private DevExpress.XtraEditors.DateEdit dtPETD;
        private DevExpress.XtraEditors.LabelControl labETD2;
        private DevExpress.XtraEditors.LabelControl labVoyage;
        private DevExpress.XtraEditors.TextEdit txtPODName;
        private DevExpress.XtraEditors.TextEdit txtPOLName;
        private ICP.Common.UI.UCVoyageLookupEdit stxtPreVoyage;
        private DevExpress.XtraEditors.LabelControl labPreVoyage;
        private DevExpress.XtraEditors.ButtonEdit stxtFinalDestination;
        private DevExpress.XtraEditors.LabelControl labFinalDestination;
        private DevExpress.XtraEditors.LabelControl labPlaceOfReceipt;
        private DevExpress.XtraEditors.ButtonEdit stxtPlaceOfDelivery;
        private DevExpress.XtraEditors.TextEdit txtPlaceOfDeliveryName;
        private ICP.FCM.Common.UI.UCButtonEdit stxtPlaceOfReceipt;
        private DevExpress.XtraEditors.TextEdit txtFinalDestinationName;
        private DevExpress.XtraEditors.TextEdit txtPlaceOfReceiptName;
        private DevExpress.XtraEditors.LabelControl labContainerDescription;
        private DevExpress.XtraEditors.MemoEdit txtMarks;
        private DevExpress.XtraEditors.LabelControl labMarks;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbPaymentTerm;
        private DevExpress.XtraEditors.LabelControl labPaymentTerm;
        private DevExpress.XtraEditors.CheckEdit chkIsWoodPacking;
        private DevExpress.XtraEditors.LabelControl labDescriptionOfGoods;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.MemoEdit txtGoodsDescription;
        private DevExpress.XtraEditors.SimpleButton btnContainer;
        private DevExpress.XtraEditors.LabelControl labQuantity;
        private DevExpress.XtraEditors.LabelControl labWeight;
        private DevExpress.XtraEditors.LabelControl labMeasurement;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbQuantityUnit;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMeasurementUnit;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbWeightUnit;
        private DevExpress.XtraEditors.MemoEdit txtFreightDescription;
        private DevExpress.XtraEditors.LabelControl labFreightDescription;
        private DevExpress.XtraEditors.MemoEdit txtCtnQtyInfo;
        private DevExpress.XtraEditors.LabelControl labCtnQtyInfo;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabPageBase;
        private DevExpress.XtraTab.XtraTabPage tabPageAMS;
        private DevExpress.XtraEditors.ButtonEdit stxtRefNo;
        private DevExpress.XtraEditors.XtraScrollableControl panelScroll;
        private DevExpress.XtraEditors.MemoEdit txtCtnQty;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private DevExpress.XtraNavBar.NavBarGroup navBarBLInfo;
        private DevExpress.XtraNavBar.NavBarGroup navBarCargo;
        private DevExpress.XtraEditors.CheckEdit chkShowPreVoyage;
        private DevExpress.XtraEditors.CheckEdit chkShowVoyage;
        private ICP.Framework.ClientComponents.Controls.ComboCustomerDescriptionControl stxtAgent;
        private ICP.FCM.Common.UI.UCButtonEdit txtPOLCode;
        private ICP.FCM.Common.UI.UCButtonEdit txtPODCode;
        private DevExpress.XtraEditors.ComboBoxEdit cmbMBLNO;
        private DevExpress.XtraEditors.MemoEdit txtIsWoodPacking;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer4;
        private DevExpress.XtraNavBar.NavBarGroup navBarIssueInfo;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbIssueType;
        private DevExpress.XtraEditors.LabelControl labType;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbIssueBy;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.MemoEdit txtCtnInfo;
        private DevExpress.XtraEditors.SpinEdit numMeasurement;
        private DevExpress.XtraEditors.SpinEdit numWeight;
        private DevExpress.XtraEditors.SpinEdit numQuantity;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dtETD;
        private DevExpress.XtraEditors.LabelControl lblBuyer;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtISFImporterRef;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbBondRef;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtBondRefNumber;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbBondActivityCode;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckEdit ckSeller;
        private DevExpress.XtraEditors.LabelControl lblSeller;
        private DevExpress.XtraEditors.CheckEdit ckBuyer;
        private DevExpress.XtraNavBar.NavBarGroup navBarContact;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarControlContainerContact;
        private WebBrowser webIMO;
        private DevExpress.XtraEditors.LabelControl labAMSConsignee;
        private DevExpress.XtraBars.Bar bar4;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.Bar bar1;
        private BindingSource bindingAMSACIISF;
        private DevExpress.XtraEditors.TextEdit txtAMSNO;
        private DevExpress.XtraEditors.TextEdit txtISFNO;
        private DevExpress.XtraEditors.LabelControl labAMSNO;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtIMO;
        private DevExpress.XtraEditors.LabelControl labISFNO;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        /// <summary>
        /// 
        /// </summary>
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mscmbCountry;
        private DevExpress.XtraEditors.LabelControl labACIEntryType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbAMSEntryType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbACIEntryType;
        private DevExpress.XtraEditors.LabelControl labAMSEntryType;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcAMSContainer;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvAMSContainer;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colSeal;
        private DevExpress.XtraGrid.Columns.GridColumn colKilos;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitOfMeasure;
        private DevExpress.XtraGrid.Columns.GridColumn colFreeFormDescription;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbBoxType;
        private BindingSource bindingContainers;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barSaveAs;
        private DevExpress.XtraBars.BarSubItem barSubPrint;
        private DevExpress.XtraBars.BarButtonItem barPrintBL;
        private DevExpress.XtraBars.BarButtonItem barReplyAgent;
        private DevExpress.XtraBars.BarSubItem barSubCheck;
        private DevExpress.XtraBars.BarButtonItem barCheck;
        private DevExpress.XtraBars.BarButtonItem barCheckDone;
        private DevExpress.XtraBars.BarSubItem barSubEDI;
        private DevExpress.XtraBars.BarButtonItem barAMS;
        private DevExpress.XtraBars.BarButtonItem barACI;
        private DevExpress.XtraBars.BarButtonItem barISF;
        private DevExpress.XtraBars.BarButtonItem barAMSISF;
        private DevExpress.XtraBars.BarButtonItem barAMSACIISF;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraBars.BarButtonItem barBill;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barAMSACI;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl7;
        private DevExpress.XtraBars.BarDockControl barDockControl8;
        private DevExpress.XtraBars.BarDockControl barDockControl6;
        private DevExpress.XtraBars.BarDockControl barDockControl5;
        private DevExpress.XtraBars.BarButtonItem barAddContainer;
        private DevExpress.XtraBars.BarButtonItem barDelContainer;
        private DevExpress.XtraBars.Bar bar7;
        private DevExpress.XtraBars.BarManager barContainerForAMS;
        private DevExpress.XtraBars.BarButtonItem barAddAMSACIISF;
        private DevExpress.XtraBars.BarButtonItem barDeleteAMSACIISF;
        private DevExpress.XtraBars.Bar bar6;
        private DevExpress.XtraBars.BarManager barAMSACIISFInfo;
        private DevExpress.XtraEditors.PanelControl panelALL;
        private DevExpress.XtraEditors.PanelControl panelAddAMSACIISF;
        private DevExpress.XtraEditors.PanelControl panelTop;
        private DevExpress.XtraEditors.PanelControl panelISFImporterRef;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtISFImporterRefLastName;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit txtISFImporterRefFirstName;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.DateEdit dateISFImporterRefDate;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox cmbISFImporterRefCountry;
        private DevExpress.XtraEditors.PanelControl panelContent;
        private DevExpress.XtraEditors.PanelControl panelLeft;
        private DevExpress.XtraEditors.LabelControl labAMSNotifyParty;
        private DevExpress.XtraEditors.CheckEdit ckConsolidator;
        private DevExpress.XtraEditors.LabelControl lblConsolidator;
        private ICP.Common.UI.Controls.CustomerPopupContainerForAMSEdit stxtAMSNotifyParty;
        private DevExpress.XtraEditors.MemoEdit txtAMSNotifyPartyDescription;
        private DevExpress.XtraEditors.LabelControl lblStuffingLocation;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbBuyerImportNumber;
        private DevExpress.XtraEditors.TextEdit txtBuyerImportNumber;
        private DevExpress.XtraEditors.LabelControl lblManufacturer;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit ckStuffingLocation;
        private DevExpress.XtraEditors.CheckEdit ckManufacturer;
        private DevExpress.XtraEditors.LabelControl lblShipToPatry;
        private DevExpress.XtraEditors.CheckEdit ckShipTo;
        private DevExpress.XtraEditors.PanelControl panelRight;
        private DevExpress.XtraEditors.PanelControl panelConsignee;
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox cmbConsigneeCountry;
        private DevExpress.XtraEditors.DateEdit dateConsigneeDate;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.TextEdit txtConsigneeLastName;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.TextEdit txtConsigneeFirstName;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.CheckEdit ckConsigneeRef;
        private DevExpress.XtraEditors.TextEdit txtConsigneeNumber;
        private DevExpress.XtraEditors.LabelControl lblCneeNumber;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbConsigneeNumber;
        private DevExpress.XtraEditors.PanelControl panelBuyer;
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox cmbBuyerCountry;
        private DevExpress.XtraEditors.DateEdit dateBuyerDate;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.TextEdit txtBuyerLastName;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.TextEdit txtBuyerFirstName;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.VScrollBar vScrollBar2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.CheckEdit ckBuyerRef;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl lwGridControl1;
        protected DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox4;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private DevExpress.XtraBars.BarDockControl barDockControl9;
        private DevExpress.XtraBars.BarDockControl barDockControl10;
        private DevExpress.XtraBars.BarDockControl barDockControl11;
        private DevExpress.XtraBars.BarDockControl barDockControl12;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl7;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraGrid.Columns.GridColumn colMark1;
        private DevExpress.XtraGrid.Columns.GridColumn colMark;
        private DevExpress.XtraGrid.Columns.GridColumn Shipper;
        private DevExpress.XtraGrid.Columns.GridColumn Consignee;
        private DevExpress.XtraGrid.Columns.GridColumn NotifyParty;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyer;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcAMSACIISF;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAMSACIISF;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperDesc;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeDesc;
        private DevExpress.XtraGrid.Columns.GridColumn colSellerDesc;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyerDesc;
        private DevExpress.XtraGrid.Columns.GridColumn colShipToDesc;
        private DevExpress.XtraGrid.Columns.GridColumn colManufacturerDesc;
        private DevExpress.XtraGrid.Columns.GridColumn colStuffingLocationDesc;
        private DevExpress.XtraGrid.Columns.GridColumn colConsolidatorDesc;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbISFImporterRef;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.TextEdit txtVesselName;
        private DevExpress.XtraEditors.PanelControl panelControl8;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCargoType;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.PanelControl panelContainerDetails;
        private DevExpress.XtraEditors.TextEdit txtHS1;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        /// <summary>
        /// 
        /// </summary>
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mscmbHS1;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        private DevExpress.XtraEditors.TextEdit txtHS10;
        /// <summary>
        /// 
        /// </summary>
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mscmbHS10;
        private DevExpress.XtraEditors.TextEdit txtHS9;
        /// <summary>
        /// 
        /// </summary>
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mscmbHS9;
        private DevExpress.XtraEditors.TextEdit txtHS8;
        /// <summary>
        /// 
        /// </summary>
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mscmbHS8;
        private DevExpress.XtraEditors.TextEdit txtHS7;
        /// <summary>
        /// 
        /// </summary>
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mscmbHS7;
        private DevExpress.XtraEditors.TextEdit txtHS6;
        private DevExpress.XtraEditors.LabelControl labelControl24;
        /// <summary>
        /// 
        /// </summary>
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mscmbHS6;
        private DevExpress.XtraEditors.LabelControl labelControl25;
        private DevExpress.XtraEditors.TextEdit txtHS5;
        /// <summary>
        /// 
        /// </summary>
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mscmbHS5;
        private DevExpress.XtraEditors.TextEdit txtHS4;
        /// <summary>
        /// 
        /// </summary>
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mscmbHS4;
        private DevExpress.XtraEditors.TextEdit txtHS3;
        /// <summary>
        /// 
        /// </summary>
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mscmbHS3;
        private DevExpress.XtraEditors.TextEdit txtHS2;
        /// <summary>
        /// 
        /// </summary>
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mscmbHS2;
        private DevExpress.XtraEditors.LabelControl labelControl28;
        private DevExpress.XtraEditors.TextEdit txtVoyageNumber;
        private DevExpress.XtraBars.BarButtonItem btnAMSandACI;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private ICP.FCM.Common.UI.UCButtonEdit txtLastPortOfCall;
        private DevExpress.XtraEditors.LabelControl labelControl29;
        private DevExpress.XtraEditors.LabelControl labelControl27;
        private ICP.FCM.Common.UI.UCButtonEdit txtFirstPortOfCall;
        private DevExpress.XtraEditors.LabelControl labelControl31;
        private ICP.FCM.Common.UI.UCButtonEdit txtPOL;
        private DevExpress.XtraEditors.DateEdit txtETD;
        private DevExpress.XtraEditors.DateEdit txtFirstPortOfCallDate;
        private DevExpress.XtraEditors.CheckEdit chkToAgent;
        private DevExpress.XtraBars.BarSubItem barbl;
        private DevExpress.XtraBars.BarButtonItem barblCHS;
        private DevExpress.XtraBars.BarButtonItem barBlENG;
        private DevExpress.XtraEditors.TextEdit txtTelexNo;
        private DevExpress.XtraEditors.TextEdit txtRleaseBy;
        private DevExpress.XtraEditors.LabelControl labTelexNo;
        private DevExpress.XtraEditors.LabelControl labReleaseBy;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbBookingParty;
        private DevExpress.XtraEditors.LabelControl labelControl30;
        private ICP.FCM.Common.UI.UCButtonEdit stxtPlacePay;
        private DevExpress.XtraEditors.CheckEdit chkThirdPlacePay;
        private DevExpress.XtraEditors.TextEdit txtScac;
        private DevExpress.XtraEditors.LabelControl labScac;
        private DevExpress.XtraEditors.LabelControl labGateInDate;
        private DevExpress.XtraEditors.DateEdit dteGateInDate;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private DevExpress.XtraBars.Bar barSavingTools;
        private DevExpress.XtraBars.BarButtonItem barCancel;
        private DevExpress.XtraBars.BarStaticItem barlabMessage;
        private DevExpress.XtraBars.BarButtonItem barSavingClose;
        private DevExpress.XtraBars.BarStaticItem barlabSeconds;
        private DevExpress.XtraEditors.TextEdit txtBookNo;
        private DevExpress.XtraEditors.CheckEdit chkIsBook;
        private DevExpress.XtraBars.BarButtonItem barWebEdi;
        private BL.HBL.AmsCustomerDes SellerDescription;
        private BL.HBL.AmsCustomerDes ConsolidatorDescription;
        private DevExpress.XtraEditors.ComboBoxEdit stxtConsolidator;
        private BL.HBL.AmsCustomerDes StuffingDescription;
        private DevExpress.XtraEditors.ComboBoxEdit stxtStuffingLocation;
        private BL.HBL.AmsCustomerDes ManuDescription;
        private DevExpress.XtraEditors.ComboBoxEdit stxtManufacturer;
        private BL.HBL.AmsCustomerDes BuyerDescription;
        private DevExpress.XtraEditors.ComboBoxEdit stxtBuyer;
        private BL.HBL.AmsCustomerDes ConsigneeDescription;
        private DevExpress.XtraEditors.ComboBoxEdit stxtAMSConsignee;
        private BL.HBL.AmsCustomerDes BookingDescription;
        private DevExpress.XtraEditors.ComboBoxEdit stxtBookingPartyInfo;
        private BL.HBL.AmsCustomerDes ShiptoDescription;
        private DevExpress.XtraEditors.ComboBoxEdit stxtShipToPatry;
        private DevExpress.XtraEditors.ComboBoxEdit stxtSeller;
        private DevExpress.XtraEditors.ComboBoxEdit stxtAMSShipper;
        private DevExpress.XtraEditors.LabelControl labAMSShipper;
        private BL.HBL.AmsCustomerDes ShipDescription;
        private Panel panel6;
        private Panel panel5;
        private Panel panel7;
        private Panel panel9;
        private Panel panel8;
        private Panel panel11;
        private Panel panel10;
        private DevExpress.XtraBars.BarButtonItem barConfirmedAMS;
    }
}
