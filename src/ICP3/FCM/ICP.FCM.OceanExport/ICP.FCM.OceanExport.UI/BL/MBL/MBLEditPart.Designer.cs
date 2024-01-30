using System.Windows.Forms;
namespace ICP.FCM.OceanExport.UI.MBL
{
    partial class MBLEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MBLEditPart));
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.dteBookingDate = new DevExpress.XtraEditors.DateEdit();
            this.cmbReleaseType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbQuantityUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbMeasurementUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbWeightUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtChecker = new ICP.Business.Common.UI.BusinessContactPopupContainerEdit();
            this.stxtIssuePlace = new DevExpress.XtraEditors.ButtonEdit();
            this.dteIssue = new DevExpress.XtraEditors.DateEdit();
            this.txtNotifyPartyDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtConsigneeDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtAgentDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtShipperDescription = new DevExpress.XtraEditors.MemoEdit();
            this.stxtNotifyParty = new ICP.Common.UI.Controls.CustomerForNewPopupContainerEdit();
            this.stxtConsignee = new ICP.Common.UI.Controls.CustomerForNewPopupContainerEdit();
            this.stxtShipper = new ICP.Common.UI.Controls.CustomerForNewPopupContainerEdit();
            this.chkIsWoodPacking = new DevExpress.XtraEditors.CheckEdit();
            this.txtGoodsDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtFreightDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtCtnQtyInfo = new DevExpress.XtraEditors.MemoEdit();
            this.txtMarks = new DevExpress.XtraEditors.MemoEdit();
            this.stxtRefNo = new DevExpress.XtraEditors.ButtonEdit();
            this.cmbPaymentTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtPlaceOfDelivery = new DevExpress.XtraEditors.ButtonEdit();
            this.txtPODName = new DevExpress.XtraEditors.TextEdit();
            this.txtPlaceOfDeliveryName = new DevExpress.XtraEditors.TextEdit();
            this.txtPOLName = new DevExpress.XtraEditors.TextEdit();
            this.cmbTransportClause = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtPlaceOfReceipt = new ICP.FCM.Common.UI.UCButtonEdit();
            this.txtPlaceOfReceiptName = new DevExpress.XtraEditors.TextEdit();
            this.txtFinalDestinationName = new DevExpress.XtraEditors.TextEdit();
            this.stxtFinalDestination = new DevExpress.XtraEditors.ButtonEdit();
            this.txtPOLCode = new ICP.FCM.Common.UI.UCButtonEdit();
            this.txtPODCode = new ICP.FCM.Common.UI.UCButtonEdit();
            this.dtPETD = new DevExpress.XtraEditors.DateEdit();
            this.stxtPlacePay = new ICP.FCM.Common.UI.UCButtonEdit();
            this.cmbWeightSite = new ICP.FCM.Common.UI.UCButtonEdit();
            this.txtWeightSite = new DevExpress.XtraEditors.TextEdit();
            this.dateVerifiedDate = new DevExpress.XtraEditors.DateEdit();
            this.dateWeightDate = new DevExpress.XtraEditors.DateEdit();
            this.txtNotify2 = new DevExpress.XtraEditors.MemoEdit();
            this.stxtPreVoyage = new ICP.Common.UI.UCVoyageLookupEdit();
            this.stxtVoyage = new ICP.Common.UI.UCVoyageLookupEdit();
            this.labIssueBy = new DevExpress.XtraEditors.LabelControl();
            this.labReleaseType = new DevExpress.XtraEditors.LabelControl();
            this.labIssueDate = new DevExpress.XtraEditors.LabelControl();
            this.labReleaseDate = new DevExpress.XtraEditors.LabelControl();
            this.labChecker = new DevExpress.XtraEditors.LabelControl();
            this.labRefNo = new DevExpress.XtraEditors.LabelControl();
            this.labIssuePlace = new DevExpress.XtraEditors.LabelControl();
            this.labMBLNo = new DevExpress.XtraEditors.LabelControl();
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
            this.txtCtnQty = new DevExpress.XtraEditors.MemoEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barSavingClose = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.barSubPrint = new DevExpress.XtraBars.BarSubItem();
            this.barPrintBL = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintLoadGoods = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintLoadContainer = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintLoadContainerCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barReplyAgent = new DevExpress.XtraBars.BarButtonItem();
            this.barSubCheck = new DevExpress.XtraBars.BarSubItem();
            this.barCheck = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckDone = new DevExpress.XtraBars.BarButtonItem();
            this.barEDIStyle = new DevExpress.XtraBars.BarButtonItem();
            this.barDeclaration = new DevExpress.XtraBars.BarSubItem();
            this.btnDeclaration = new DevExpress.XtraBars.BarButtonItem();
            this.bntImportDeclaration = new DevExpress.XtraBars.BarButtonItem();
            this.barAddHbl = new DevExpress.XtraBars.BarButtonItem();
            this.barGetCommodity = new DevExpress.XtraBars.BarButtonItem();
            this.barE_MBL = new DevExpress.XtraBars.BarButtonItem();
            this.barEDIANL = new DevExpress.XtraBars.BarSubItem();
            this.barBooking = new DevExpress.XtraBars.BarButtonItem();
            this.barPreplan = new DevExpress.XtraBars.BarButtonItem();
            this.barSupplement = new DevExpress.XtraBars.BarButtonItem();
            this.barWharf = new DevExpress.XtraBars.BarButtonItem();
            this.barVGM = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barBill = new DevExpress.XtraBars.BarButtonItem();
            this.barbl = new DevExpress.XtraBars.BarSubItem();
            this.barchs = new DevExpress.XtraBars.BarButtonItem();
            this.bareng = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveVGM = new DevExpress.XtraBars.BarButtonItem();
            this.barSavingTools = new DevExpress.XtraBars.Bar();
            this.barCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barlabMessage = new DevExpress.XtraBars.BarStaticItem();
            this.barlabSeconds = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.txtWoodPacking = new DevExpress.XtraEditors.MemoEdit();
            this.labFreightDescription = new DevExpress.XtraEditors.LabelControl();
            this.labContainerInfo = new DevExpress.XtraEditors.LabelControl();
            this.labCtnQtyInfo = new DevExpress.XtraEditors.LabelControl();
            this.labMarks = new DevExpress.XtraEditors.LabelControl();
            this.panelMain = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarBaseInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labContractName = new DevExpress.XtraEditors.LabelControl();
            this.mcmbBookingParty = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.chkAMS = new DevExpress.XtraEditors.CheckEdit();
            this.chkHasContract = new DevExpress.XtraEditors.CheckEdit();
            this.txtContractNo = new DevExpress.XtraEditors.ButtonEdit();
            this.txtTelexNo = new DevExpress.XtraEditors.TextEdit();
            this.txtRleaseBy = new DevExpress.XtraEditors.TextEdit();
            this.txtHBLNO = new DevExpress.XtraEditors.TextEdit();
            this.cmbIssueType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labTelexNo = new DevExpress.XtraEditors.LabelControl();
            this.labReleaseBy = new DevExpress.XtraEditors.LabelControl();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.labHBLNO = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkHasFee = new DevExpress.XtraEditors.CheckEdit();
            this.dteGateInDate = new DevExpress.XtraEditors.DateEdit();
            this.labGateInDate = new DevExpress.XtraEditors.LabelControl();
            this.txtNBPODCode = new DevExpress.XtraEditors.TextEdit();
            this.lblNBPODCode = new DevExpress.XtraEditors.LabelControl();
            this.chkThirdPlacePay = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dtETA = new DevExpress.XtraEditors.DateEdit();
            this.dtETD = new DevExpress.XtraEditors.DateEdit();
            this.labAMSInfo = new DevExpress.XtraEditors.LabelControl();
            this.stxtAgent = new ICP.Framework.ClientComponents.Controls.ComboCustomerDescriptionControl();
            this.chkShowVoyage = new DevExpress.XtraEditors.CheckEdit();
            this.chkShowPreVoyage = new DevExpress.XtraEditors.CheckEdit();
            this.labPlaceOfDelivery = new DevExpress.XtraEditors.LabelControl();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.labPreVoyage = new DevExpress.XtraEditors.LabelControl();
            this.labPOL2 = new DevExpress.XtraEditors.LabelControl();
            this.labPaymentTerm = new DevExpress.XtraEditors.LabelControl();
            this.labPlaceOfReceipt = new DevExpress.XtraEditors.LabelControl();
            this.labVoyage = new DevExpress.XtraEditors.LabelControl();
            this.labTransportClause = new DevExpress.XtraEditors.LabelControl();
            this.labFinalDestination = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkSpecial = new DevExpress.XtraEditors.CheckEdit();
            this.container = new ICP.Framework.ClientComponents.Controls.ContainerDemandControl();
            this.txtHSCode = new DevExpress.XtraEditors.TextEdit();
            this.IsManualControl = new DevExpress.XtraEditors.CheckEdit();
            this.labHSCode = new DevExpress.XtraEditors.LabelControl();
            this.numMeasurement = new DevExpress.XtraEditors.SpinEdit();
            this.numWeight = new DevExpress.XtraEditors.SpinEdit();
            this.numQuantity = new DevExpress.XtraEditors.SpinEdit();
            this.txtCtnInfo = new DevExpress.XtraEditors.MemoEdit();
            this.labNSITBLNotes = new DevExpress.XtraEditors.LabelControl();
            this.txtNSITBLNotes = new DevExpress.XtraEditors.MemoEdit();
            this.navBarGroupControlContainer4 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.mcmbIssueBy = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.navBarControlContainerContact = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navBarGroupControlContainer5 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labWeightingDate = new DevExpress.XtraEditors.LabelControl();
            this.labVerifiedDate = new DevExpress.XtraEditors.LabelControl();
            this.txtVerifiedPerson = new DevExpress.XtraEditors.TextEdit();
            this.labVerifiedPerson = new DevExpress.XtraEditors.LabelControl();
            this.labWeightSite = new DevExpress.XtraEditors.LabelControl();
            this.txtResponsiblePerson = new DevExpress.XtraEditors.TextEdit();
            this.labResponsiblePerson = new DevExpress.XtraEditors.LabelControl();
            this.txtResponsibleParty = new DevExpress.XtraEditors.TextEdit();
            this.labResponsibleParty = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer6 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel7 = new System.Windows.Forms.Panel();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.navBarBLInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarCargo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navAMS = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarIssueInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarContact = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuantityUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtChecker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtIssuePlace.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIssue.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIssue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotifyPartyDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgentDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipperDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtNotifyParty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsWoodPacking.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoodsDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFreightDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtnQtyInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtRefNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPODName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfDeliveryName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOLName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfReceipt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfReceiptName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinalDestinationName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtFinalDestination.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOLCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPODCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPETD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPETD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlacePay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightSite.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeightSite.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateVerifiedDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateVerifiedDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateWeightDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateWeightDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotify2.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtnQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWoodPacking.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).BeginInit();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAMS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasContract.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelexNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRleaseBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHBLNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIssueType.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkHasFee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteGateInDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteGateInDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNBPODCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkThirdPlacePay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETA.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowVoyage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowPreVoyage.Properties)).BeginInit();
            this.navBarGroupControlContainer3.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSpecial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHSCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsManualControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtnInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNSITBLNotes.Properties)).BeginInit();
            this.navBarGroupControlContainer4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.navBarGroupControlContainer5.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVerifiedPerson.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResponsiblePerson.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResponsibleParty.Properties)).BeginInit();
            this.navBarGroupControlContainer6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanMBLInfo);
            // 
            // txtNo
            // 
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "No", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtNo.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtNo.Location = new System.Drawing.Point(82, 38);
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNo.Size = new System.Drawing.Size(285, 21);
            this.txtNo.TabIndex = 2;
            // 
            // dteBookingDate
            // 
            this.dteBookingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ReleaseDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteBookingDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteBookingDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteBookingDate.Location = new System.Drawing.Point(670, 39);
            this.dteBookingDate.Name = "dteBookingDate";
            this.dteBookingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteBookingDate.Properties.Mask.EditMask = "";
            this.dteBookingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteBookingDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteBookingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteBookingDate.Size = new System.Drawing.Size(136, 21);
            this.dteBookingDate.TabIndex = 11;
            this.dteBookingDate.TabStop = false;
            // 
            // cmbReleaseType
            // 
            this.cmbReleaseType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ReleaseType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbReleaseType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbReleaseType.Location = new System.Drawing.Point(479, 38);
            this.cmbReleaseType.Name = "cmbReleaseType";
            this.cmbReleaseType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbReleaseType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbReleaseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReleaseType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbReleaseType.Size = new System.Drawing.Size(104, 21);
            this.cmbReleaseType.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbReleaseType.TabIndex = 8;
            // 
            // cmbQuantityUnit
            // 
            this.cmbQuantityUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "QuantityUnitID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbQuantityUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbQuantityUnit.Location = new System.Drawing.Point(179, 52);
            this.cmbQuantityUnit.Name = "cmbQuantityUnit";
            this.cmbQuantityUnit.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbQuantityUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbQuantityUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQuantityUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbQuantityUnit.Size = new System.Drawing.Size(79, 21);
            this.cmbQuantityUnit.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbQuantityUnit.TabIndex = 3;
            // 
            // cmbMeasurementUnit
            // 
            this.cmbMeasurementUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "MeasurementUnitID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbMeasurementUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbMeasurementUnit.Location = new System.Drawing.Point(179, 98);
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
            this.cmbWeightUnit.Location = new System.Drawing.Point(179, 75);
            this.cmbWeightUnit.Name = "cmbWeightUnit";
            this.cmbWeightUnit.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbWeightUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbWeightUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWeightUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbWeightUnit.Size = new System.Drawing.Size(79, 21);
            this.cmbWeightUnit.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbWeightUnit.TabIndex = 4;
            // 
            // stxtChecker
            // 
            this.stxtChecker.ContactType = ICP.Framework.CommonLibrary.Common.ContactType.Customer;
            this.stxtChecker.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CheckerName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtChecker.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "CheckerID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtChecker, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtChecker.Location = new System.Drawing.Point(82, 91);
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
            this.stxtChecker.Size = new System.Drawing.Size(285, 21);
            this.stxtChecker.TabIndex = 4;
            // 
            // stxtIssuePlace
            // 
            this.stxtIssuePlace.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "IssuePlaceName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtIssuePlace.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "IssuePlaceID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtIssuePlace, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtIssuePlace.Location = new System.Drawing.Point(331, 2);
            this.stxtIssuePlace.Name = "stxtIssuePlace";
            this.stxtIssuePlace.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtIssuePlace.Properties.Appearance.Options.UseBackColor = true;
            this.stxtIssuePlace.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtIssuePlace.Size = new System.Drawing.Size(204, 21);
            this.stxtIssuePlace.TabIndex = 1;
            // 
            // dteIssue
            // 
            this.dteIssue.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IssueDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteIssue.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteIssue, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteIssue.Location = new System.Drawing.Point(605, 2);
            this.dteIssue.Name = "dteIssue";
            this.dteIssue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteIssue.Properties.Mask.EditMask = "";
            this.dteIssue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteIssue.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteIssue.Size = new System.Drawing.Size(202, 21);
            this.dteIssue.TabIndex = 2;
            // 
            // txtNotifyPartyDescription
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtNotifyPartyDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtNotifyPartyDescription.Location = new System.Drawing.Point(5, 263);
            this.txtNotifyPartyDescription.Name = "txtNotifyPartyDescription";
            this.txtNotifyPartyDescription.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNotifyPartyDescription.Properties.ReadOnly = true;
            this.txtNotifyPartyDescription.Size = new System.Drawing.Size(362, 92);
            this.txtNotifyPartyDescription.TabIndex = 5;
            // 
            // txtConsigneeDescription
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtConsigneeDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtConsigneeDescription.Location = new System.Drawing.Point(5, 144);
            this.txtConsigneeDescription.Name = "txtConsigneeDescription";
            this.txtConsigneeDescription.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConsigneeDescription.Properties.ReadOnly = true;
            this.txtConsigneeDescription.Size = new System.Drawing.Size(362, 90);
            this.txtConsigneeDescription.TabIndex = 3;
            // 
            // txtAgentDescription
            // 
            this.txtAgentDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "AgentText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtAgentDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtAgentDescription.Location = new System.Drawing.Point(478, 27);
            this.txtAgentDescription.Name = "txtAgentDescription";
            this.txtAgentDescription.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAgentDescription.Size = new System.Drawing.Size(329, 90);
            this.txtAgentDescription.TabIndex = 7;
            // 
            // txtShipperDescription
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtShipperDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtShipperDescription.Location = new System.Drawing.Point(5, 26);
            this.txtShipperDescription.Name = "txtShipperDescription";
            this.txtShipperDescription.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtShipperDescription.Properties.ReadOnly = true;
            this.txtShipperDescription.Size = new System.Drawing.Size(362, 90);
            this.txtShipperDescription.TabIndex = 1;
            // 
            // stxtNotifyParty
            // 
            this.stxtNotifyParty.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "NotifyPartyName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtNotifyParty.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "NotifyPartyID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtNotifyParty, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtNotifyParty.Location = new System.Drawing.Point(82, 240);
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
            this.stxtNotifyParty.Size = new System.Drawing.Size(285, 21);
            this.stxtNotifyParty.TabIndex = 4;
            // 
            // stxtConsignee
            // 
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ConsigneeName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "ConsigneeID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtConsignee, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtConsignee.Location = new System.Drawing.Point(82, 121);
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
            this.stxtConsignee.Size = new System.Drawing.Size(285, 21);
            this.stxtConsignee.TabIndex = 2;
            // 
            // stxtShipper
            // 
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ShipperName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "ShipperID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtShipper, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtShipper.Location = new System.Drawing.Point(82, 3);
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
            this.stxtShipper.Size = new System.Drawing.Size(285, 21);
            this.stxtShipper.TabIndex = 0;
            // 
            // chkIsWoodPacking
            // 
            this.chkIsWoodPacking.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsWoodPacking", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.chkIsWoodPacking, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsWoodPacking.Location = new System.Drawing.Point(357, 2);
            this.chkIsWoodPacking.Name = "chkIsWoodPacking";
            this.chkIsWoodPacking.Properties.Caption = "Wood Packing";
            this.chkIsWoodPacking.Size = new System.Drawing.Size(103, 19);
            this.chkIsWoodPacking.TabIndex = 6;
            // 
            // txtGoodsDescription
            // 
            this.txtGoodsDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "GoodsDescription", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtGoodsDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dxErrorProvider1.SetIconAlignment(this.txtGoodsDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtGoodsDescription.Location = new System.Drawing.Point(3, 95);
            this.txtGoodsDescription.Name = "txtGoodsDescription";
            this.txtGoodsDescription.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGoodsDescription.Size = new System.Drawing.Size(270, 164);
            this.txtGoodsDescription.TabIndex = 0;
            // 
            // txtFreightDescription
            // 
            this.txtFreightDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "FreightDescription", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtFreightDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtFreightDescription.Location = new System.Drawing.Point(541, 255);
            this.txtFreightDescription.Name = "txtFreightDescription";
            this.txtFreightDescription.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFreightDescription.Size = new System.Drawing.Size(266, 88);
            this.txtFreightDescription.TabIndex = 9;
            // 
            // txtCtnQtyInfo
            // 
            this.txtCtnQtyInfo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CtnQtyInfo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCtnQtyInfo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCtnQtyInfo.Location = new System.Drawing.Point(541, 163);
            this.txtCtnQtyInfo.Name = "txtCtnQtyInfo";
            this.txtCtnQtyInfo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCtnQtyInfo.Size = new System.Drawing.Size(266, 63);
            this.txtCtnQtyInfo.TabIndex = 8;
            // 
            // txtMarks
            // 
            this.txtMarks.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Marks", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtMarks, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtMarks.Location = new System.Drawing.Point(541, 25);
            this.txtMarks.Name = "txtMarks";
            this.txtMarks.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMarks.Size = new System.Drawing.Size(266, 112);
            this.txtMarks.TabIndex = 7;
            // 
            // stxtRefNo
            // 
            this.stxtRefNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "RefNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtRefNo.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtRefNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtRefNo.Location = new System.Drawing.Point(82, 13);
            this.stxtRefNo.Name = "stxtRefNo";
            this.stxtRefNo.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtRefNo.Properties.Appearance.Options.UseBackColor = true;
            this.stxtRefNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtRefNo.Size = new System.Drawing.Size(131, 21);
            this.stxtRefNo.TabIndex = 0;
            // 
            // cmbPaymentTerm
            // 
            this.cmbPaymentTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "PaymentTermID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbPaymentTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbPaymentTerm.Location = new System.Drawing.Point(694, 316);
            this.cmbPaymentTerm.Name = "cmbPaymentTerm";
            this.cmbPaymentTerm.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbPaymentTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbPaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaymentTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbPaymentTerm.Size = new System.Drawing.Size(112, 21);
            this.cmbPaymentTerm.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbPaymentTerm.TabIndex = 27;
            // 
            // stxtPlaceOfDelivery
            // 
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "PlaceOfDeliveryID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PlaceOfDeliveryCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtPlaceOfDelivery, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPlaceOfDelivery.Location = new System.Drawing.Point(478, 270);
            this.stxtPlaceOfDelivery.Name = "stxtPlaceOfDelivery";
            this.stxtPlaceOfDelivery.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtPlaceOfDelivery.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlaceOfDelivery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlaceOfDelivery.Size = new System.Drawing.Size(108, 21);
            this.stxtPlaceOfDelivery.TabIndex = 21;
            // 
            // txtPODName
            // 
            this.txtPODName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PODName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPODName.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtPODName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPODName.Location = new System.Drawing.Point(592, 245);
            this.txtPODName.Name = "txtPODName";
            this.txtPODName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPODName.Size = new System.Drawing.Size(82, 21);
            this.txtPODName.TabIndex = 19;
            // 
            // txtPlaceOfDeliveryName
            // 
            this.txtPlaceOfDeliveryName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PlaceOfDeliveryName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPlaceOfDeliveryName.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtPlaceOfDeliveryName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPlaceOfDeliveryName.Location = new System.Drawing.Point(592, 270);
            this.txtPlaceOfDeliveryName.Name = "txtPlaceOfDeliveryName";
            this.txtPlaceOfDeliveryName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlaceOfDeliveryName.Size = new System.Drawing.Size(214, 21);
            this.txtPlaceOfDeliveryName.TabIndex = 22;
            // 
            // txtPOLName
            // 
            this.txtPOLName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "POLName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPOLName.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtPOLName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPOLName.Location = new System.Drawing.Point(592, 219);
            this.txtPOLName.Name = "txtPOLName";
            this.txtPOLName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPOLName.Size = new System.Drawing.Size(82, 21);
            this.txtPOLName.TabIndex = 16;
            // 
            // cmbTransportClause
            // 
            this.cmbTransportClause.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "TransportClauseID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbTransportClause, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbTransportClause.Location = new System.Drawing.Point(478, 317);
            this.cmbTransportClause.Name = "cmbTransportClause";
            this.cmbTransportClause.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbTransportClause.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTransportClause.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTransportClause.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbTransportClause.Size = new System.Drawing.Size(108, 21);
            this.cmbTransportClause.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbTransportClause.TabIndex = 25;
            // 
            // stxtPlaceOfReceipt
            // 
            this.stxtPlaceOfReceipt.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "PlaceOfReceiptID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPlaceOfReceipt.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PlaceOfReceiptCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtPlaceOfReceipt, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPlaceOfReceipt.Location = new System.Drawing.Point(478, 195);
            this.stxtPlaceOfReceipt.Name = "stxtPlaceOfReceipt";
            this.stxtPlaceOfReceipt.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtPlaceOfReceipt.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlaceOfReceipt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlaceOfReceipt.Size = new System.Drawing.Size(108, 21);
            this.stxtPlaceOfReceipt.TabIndex = 12;
            this.stxtPlaceOfReceipt.Tag = null;
            // 
            // txtPlaceOfReceiptName
            // 
            this.txtPlaceOfReceiptName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PlaceOfReceiptName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPlaceOfReceiptName.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtPlaceOfReceiptName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPlaceOfReceiptName.Location = new System.Drawing.Point(592, 195);
            this.txtPlaceOfReceiptName.Name = "txtPlaceOfReceiptName";
            this.txtPlaceOfReceiptName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlaceOfReceiptName.Size = new System.Drawing.Size(82, 21);
            this.txtPlaceOfReceiptName.TabIndex = 13;
            // 
            // txtFinalDestinationName
            // 
            this.txtFinalDestinationName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "FinalDestinationName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFinalDestinationName.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtFinalDestinationName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtFinalDestinationName.Location = new System.Drawing.Point(592, 293);
            this.txtFinalDestinationName.Name = "txtFinalDestinationName";
            this.txtFinalDestinationName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFinalDestinationName.Size = new System.Drawing.Size(214, 21);
            this.txtFinalDestinationName.TabIndex = 24;
            // 
            // stxtFinalDestination
            // 
            this.stxtFinalDestination.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "FinalDestinationID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtFinalDestination.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "FinalDestinationCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtFinalDestination, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtFinalDestination.Location = new System.Drawing.Point(478, 293);
            this.stxtFinalDestination.Name = "stxtFinalDestination";
            this.stxtFinalDestination.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtFinalDestination.Properties.Appearance.Options.UseBackColor = true;
            this.stxtFinalDestination.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtFinalDestination.Size = new System.Drawing.Size(108, 21);
            this.stxtFinalDestination.TabIndex = 23;
            // 
            // txtPOLCode
            // 
            this.txtPOLCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "POLCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPOLCode.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "POLID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPOLCode.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtPOLCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPOLCode.Location = new System.Drawing.Point(478, 219);
            this.txtPOLCode.Name = "txtPOLCode";
            this.txtPOLCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtPOLCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtPOLCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPOLCode.Size = new System.Drawing.Size(108, 21);
            this.txtPOLCode.TabIndex = 15;
            this.txtPOLCode.Tag = null;
            // 
            // txtPODCode
            // 
            this.txtPODCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PODCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPODCode.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "PODID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtPODCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPODCode.Location = new System.Drawing.Point(478, 246);
            this.txtPODCode.Name = "txtPODCode";
            this.txtPODCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtPODCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtPODCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPODCode.Size = new System.Drawing.Size(108, 21);
            this.txtPODCode.TabIndex = 18;
            this.txtPODCode.Tag = null;
            // 
            // dtPETD
            // 
            this.dtPETD.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "PreETD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtPETD.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dtPETD, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dtPETD.Location = new System.Drawing.Point(716, 196);
            this.dtPETD.Name = "dtPETD";
            this.dtPETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtPETD.Properties.Mask.EditMask = "";
            this.dtPETD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtPETD.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtPETD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtPETD.Size = new System.Drawing.Size(91, 21);
            this.dtPETD.TabIndex = 14;
            this.dtPETD.TabStop = false;
            // 
            // stxtPlacePay
            // 
            this.stxtPlacePay.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "CollectbyAgentOrderID", true));
            this.stxtPlacePay.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CollectbyAgentNameOrder", true));
            this.stxtPlacePay.EditValue = "";
            this.stxtPlacePay.Enabled = false;
            this.dxErrorProvider1.SetIconAlignment(this.stxtPlacePay, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPlacePay.Location = new System.Drawing.Point(478, 342);
            this.stxtPlacePay.Name = "stxtPlacePay";
            this.stxtPlacePay.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.stxtPlacePay.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlacePay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlacePay.Size = new System.Drawing.Size(108, 21);
            this.stxtPlacePay.TabIndex = 28;
            this.stxtPlacePay.Tag = null;
            // 
            // cmbWeightSite
            // 
            this.dxErrorProvider1.SetIconAlignment(this.cmbWeightSite, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbWeightSite.Location = new System.Drawing.Point(578, 13);
            this.cmbWeightSite.Name = "cmbWeightSite";
            this.cmbWeightSite.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbWeightSite.Properties.Appearance.Options.UseBackColor = true;
            this.cmbWeightSite.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbWeightSite.Size = new System.Drawing.Size(123, 21);
            this.cmbWeightSite.TabIndex = 42;
            this.cmbWeightSite.Tag = null;
            // 
            // txtWeightSite
            // 
            this.txtWeightSite.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtWeightSite, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtWeightSite.Location = new System.Drawing.Point(701, 13);
            this.txtWeightSite.Name = "txtWeightSite";
            this.txtWeightSite.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWeightSite.Size = new System.Drawing.Size(96, 21);
            this.txtWeightSite.TabIndex = 39;
            // 
            // dateVerifiedDate
            // 
            this.dateVerifiedDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dateVerifiedDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateVerifiedDate.Location = new System.Drawing.Point(578, 40);
            this.dateVerifiedDate.Name = "dateVerifiedDate";
            this.dateVerifiedDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateVerifiedDate.Properties.Mask.EditMask = "";
            this.dateVerifiedDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateVerifiedDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateVerifiedDate.Size = new System.Drawing.Size(125, 21);
            this.dateVerifiedDate.TabIndex = 46;
            // 
            // dateWeightDate
            // 
            this.dateWeightDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dateWeightDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateWeightDate.Location = new System.Drawing.Point(102, 40);
            this.dateWeightDate.Name = "dateWeightDate";
            this.dateWeightDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateWeightDate.Properties.Mask.EditMask = "";
            this.dateWeightDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateWeightDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateWeightDate.Size = new System.Drawing.Size(136, 21);
            this.dateWeightDate.TabIndex = 44;
            // 
            // txtNotify2
            // 
            this.txtNotify2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "NotifyParty2", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtNotify2, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtNotify2.Location = new System.Drawing.Point(8, 28);
            this.txtNotify2.Name = "txtNotify2";
            this.txtNotify2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNotify2.Size = new System.Drawing.Size(248, 100);
            this.txtNotify2.TabIndex = 10;
            // 
            // stxtPreVoyage
            // 
            this.stxtPreVoyage.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bindingSource1, "PreVesselVoyage", true));
            this.stxtPreVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "PreVoyageID", true));
            this.stxtPreVoyage.EditText = "";
            this.stxtPreVoyage.EditValue = null;
            this.stxtPreVoyage.Location = new System.Drawing.Point(478, 122);
            this.stxtPreVoyage.Name = "stxtPreVoyage";
            this.stxtPreVoyage.ReadOnly = false;
            this.stxtPreVoyage.RefreshButtonToolTip = "刷新以获取与输入相匹配的船名/航次";
            this.stxtPreVoyage.ShowRefreshButton = true;
            this.stxtPreVoyage.Size = new System.Drawing.Size(208, 21);
            this.stxtPreVoyage.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtPreVoyage.TabIndex = 8;
            this.stxtPreVoyage.ToolTip = "";
            // 
            // stxtVoyage
            // 
            this.stxtVoyage.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bindingSource1, "VesselVoyage", true));
            this.stxtVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "VoyageID", true));
            this.stxtVoyage.EditText = "";
            this.stxtVoyage.EditValue = null;
            this.stxtVoyage.Location = new System.Drawing.Point(478, 148);
            this.stxtVoyage.Name = "stxtVoyage";
            this.stxtVoyage.ReadOnly = false;
            this.stxtVoyage.RefreshButtonToolTip = "刷新以获取与输入相匹配的船名/航次";
            this.stxtVoyage.ShowRefreshButton = true;
            this.stxtVoyage.Size = new System.Drawing.Size(208, 21);
            this.stxtVoyage.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtVoyage.TabIndex = 10;
            this.stxtVoyage.ToolTip = "";
            // 
            // labIssueBy
            // 
            this.labIssueBy.Location = new System.Drawing.Point(5, 6);
            this.labIssueBy.Name = "labIssueBy";
            this.labIssueBy.Size = new System.Drawing.Size(45, 14);
            this.labIssueBy.TabIndex = 6;
            this.labIssueBy.Text = "Issue By";
            // 
            // labReleaseType
            // 
            this.labReleaseType.Location = new System.Drawing.Point(386, 41);
            this.labReleaseType.Name = "labReleaseType";
            this.labReleaseType.Size = new System.Drawing.Size(69, 14);
            this.labReleaseType.TabIndex = 6;
            this.labReleaseType.Text = "ReleaseType";
            // 
            // labIssueDate
            // 
            this.labIssueDate.Location = new System.Drawing.Point(541, 6);
            this.labIssueDate.Name = "labIssueDate";
            this.labIssueDate.Size = new System.Drawing.Size(58, 14);
            this.labIssueDate.TabIndex = 0;
            this.labIssueDate.Text = "Issue Date";
            // 
            // labReleaseDate
            // 
            this.labReleaseDate.Location = new System.Drawing.Point(594, 42);
            this.labReleaseDate.Name = "labReleaseDate";
            this.labReleaseDate.Size = new System.Drawing.Size(71, 14);
            this.labReleaseDate.TabIndex = 0;
            this.labReleaseDate.Text = "Release Date";
            // 
            // labChecker
            // 
            this.labChecker.Location = new System.Drawing.Point(3, 94);
            this.labChecker.Name = "labChecker";
            this.labChecker.Size = new System.Drawing.Size(44, 14);
            this.labChecker.TabIndex = 0;
            this.labChecker.Text = "Checker";
            // 
            // labRefNo
            // 
            this.labRefNo.Location = new System.Drawing.Point(5, 16);
            this.labRefNo.Name = "labRefNo";
            this.labRefNo.Size = new System.Drawing.Size(33, 14);
            this.labRefNo.TabIndex = 0;
            this.labRefNo.Text = "RefNo";
            // 
            // labIssuePlace
            // 
            this.labIssuePlace.Location = new System.Drawing.Point(267, 6);
            this.labIssuePlace.Name = "labIssuePlace";
            this.labIssuePlace.Size = new System.Drawing.Size(60, 14);
            this.labIssuePlace.TabIndex = 0;
            this.labIssuePlace.Text = "Issue Place";
            // 
            // labMBLNo
            // 
            this.labMBLNo.Location = new System.Drawing.Point(5, 40);
            this.labMBLNo.Name = "labMBLNo";
            this.labMBLNo.Size = new System.Drawing.Size(17, 14);
            this.labMBLNo.TabIndex = 0;
            this.labMBLNo.Text = "NO";
            // 
            // labNotifyParty
            // 
            this.labNotifyParty.Location = new System.Drawing.Point(5, 244);
            this.labNotifyParty.Name = "labNotifyParty";
            this.labNotifyParty.Size = new System.Drawing.Size(60, 14);
            this.labNotifyParty.TabIndex = 0;
            this.labNotifyParty.Text = "NotifyParty";
            // 
            // labConsignee
            // 
            this.labConsignee.Location = new System.Drawing.Point(5, 124);
            this.labConsignee.Name = "labConsignee";
            this.labConsignee.Size = new System.Drawing.Size(56, 14);
            this.labConsignee.TabIndex = 0;
            this.labConsignee.Text = "Consignee";
            // 
            // labAgent
            // 
            this.labAgent.Location = new System.Drawing.Point(386, 8);
            this.labAgent.Name = "labAgent";
            this.labAgent.Size = new System.Drawing.Size(34, 14);
            this.labAgent.TabIndex = 0;
            this.labAgent.Text = "Agent";
            // 
            // labShipper
            // 
            this.labShipper.Location = new System.Drawing.Point(5, 6);
            this.labShipper.Name = "labShipper";
            this.labShipper.Size = new System.Drawing.Size(41, 14);
            this.labShipper.TabIndex = 0;
            this.labShipper.Text = "Shipper";
            // 
            // labDescriptionOfGoods
            // 
            this.labDescriptionOfGoods.Location = new System.Drawing.Point(263, 3);
            this.labDescriptionOfGoods.Name = "labDescriptionOfGoods";
            this.labDescriptionOfGoods.Size = new System.Drawing.Size(81, 14);
            this.labDescriptionOfGoods.TabIndex = 0;
            this.labDescriptionOfGoods.Text = "Desc Of Goods";
            // 
            // labQuantity
            // 
            this.labQuantity.Location = new System.Drawing.Point(5, 55);
            this.labQuantity.Name = "labQuantity";
            this.labQuantity.Size = new System.Drawing.Size(47, 14);
            this.labQuantity.TabIndex = 40;
            this.labQuantity.Text = "Quantity";
            // 
            // labWeight
            // 
            this.labWeight.Location = new System.Drawing.Point(5, 78);
            this.labWeight.Name = "labWeight";
            this.labWeight.Size = new System.Drawing.Size(40, 14);
            this.labWeight.TabIndex = 38;
            this.labWeight.Text = "Weight";
            // 
            // labMeasurement
            // 
            this.labMeasurement.Location = new System.Drawing.Point(5, 101);
            this.labMeasurement.Name = "labMeasurement";
            this.labMeasurement.Size = new System.Drawing.Size(74, 14);
            this.labMeasurement.TabIndex = 39;
            this.labMeasurement.Text = "Measurement";
            // 
            // btnContainer
            // 
            this.btnContainer.Location = new System.Drawing.Point(1, 2);
            this.btnContainer.Name = "btnContainer";
            this.btnContainer.Size = new System.Drawing.Size(257, 23);
            this.btnContainer.TabIndex = 0;
            this.btnContainer.Text = "Container";
            this.btnContainer.Click += new System.EventHandler(this.btnContainer_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtGoodsDescription);
            this.groupBox2.Controls.Add(this.txtCtnQty);
            this.groupBox2.Controls.Add(this.txtWoodPacking);
            this.groupBox2.Location = new System.Drawing.Point(262, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(276, 371);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
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
            this.txtCtnQty.Size = new System.Drawing.Size(270, 77);
            this.txtCtnQty.TabIndex = 0;
            this.txtCtnQty.TabStop = false;
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
            this.barButtonItem3,
            this.barPrintBL,
            this.barReplyAgent,
            this.barCheck,
            this.barCheckDone,
            this.barE_MBL,
            this.barClose,
            this.barSubCheck,
            this.barRefresh,
            this.barSubPrint,
            this.barPrintLoadGoods,
            this.barBill,
            this.barEDIStyle,
            this.barbl,
            this.barchs,
            this.bareng,
            this.barCancel,
            this.barlabMessage,
            this.barSavingClose,
            this.barlabSeconds,
            this.barEDIANL,
            this.barBooking,
            this.barPreplan,
            this.barSupplement,
            this.barWharf,
            this.barAddHbl,
            this.barGetCommodity,
            this.barVGM,
            this.barButtonItem1,
            this.barSaveVGM,
            this.barPrintLoadContainer,
            this.barPrintLoadContainerCopy,
            this.btnDeclaration,
            this.bntImportDeclaration,
            this.barDeclaration});
            this.barManager1.MaxItemId = 46;
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barReplyAgent, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubCheck, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEDIStyle, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDeclaration, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAddHbl),
            new DevExpress.XtraBars.LinkPersistInfo(this.barGetCommodity),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barE_MBL, "", false, true, false, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEDIANL),
            new DevExpress.XtraBars.LinkPersistInfo(this.barRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBill, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barbl, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSaveVGM)});
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
            this.barSavingClose.Glyph = ((System.Drawing.Image)(resources.GetObject("barSavingClose.Glyph")));
            this.barSavingClose.Id = 29;
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
            // barSubPrint
            // 
            this.barSubPrint.Caption = "Print";
            this.barSubPrint.Glyph = ((System.Drawing.Image)(resources.GetObject("barSubPrint.Glyph")));
            this.barSubPrint.Id = 19;
            this.barSubPrint.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrintBL, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrintLoadGoods),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrintLoadContainer),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrintLoadContainerCopy)});
            this.barSubPrint.Name = "barSubPrint";
            // 
            // barPrintBL
            // 
            this.barPrintBL.Caption = "Print BL";
            this.barPrintBL.Id = 4;
            this.barPrintBL.Name = "barPrintBL";
            this.barPrintBL.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barPrintLoadGoods
            // 
            this.barPrintLoadGoods.Caption = "Print Load Goods";
            this.barPrintLoadGoods.Id = 20;
            this.barPrintLoadGoods.Name = "barPrintLoadGoods";
            this.barPrintLoadGoods.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrintLoadGoods_ItemClick);
            // 
            // barPrintLoadContainer
            // 
            this.barPrintLoadContainer.Caption = "Print Load Container";
            this.barPrintLoadContainer.Id = 41;
            this.barPrintLoadContainer.Name = "barPrintLoadContainer";
            this.barPrintLoadContainer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrintLoadContainer_ItemClick);
            // 
            // barPrintLoadContainerCopy
            // 
            this.barPrintLoadContainerCopy.Caption = "Print Load Container(Copy)";
            this.barPrintLoadContainerCopy.Id = 42;
            this.barPrintLoadContainerCopy.Name = "barPrintLoadContainerCopy";
            this.barPrintLoadContainerCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrintLoadContainerCopy_ItemClick);
            // 
            // barReplyAgent
            // 
            this.barReplyAgent.Caption = "&ReplyAgent";
            this.barReplyAgent.Id = 5;
            this.barReplyAgent.Name = "barReplyAgent";
            this.barReplyAgent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barReplyAgent_ItemClick);
            // 
            // barSubCheck
            // 
            this.barSubCheck.Caption = "Check";
            this.barSubCheck.Glyph = ((System.Drawing.Image)(resources.GetObject("barSubCheck.Glyph")));
            this.barSubCheck.Id = 12;
            this.barSubCheck.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCheck, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCheckDone, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barSubCheck.Name = "barSubCheck";
            // 
            // barCheck
            // 
            this.barCheck.Caption = "Chec&k";
            this.barCheck.Id = 6;
            this.barCheck.Name = "barCheck";
            this.barCheck.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheck_ItemClick);
            // 
            // barCheckDone
            // 
            this.barCheckDone.Caption = "Check&Done";
            this.barCheckDone.Id = 7;
            this.barCheckDone.Name = "barCheckDone";
            this.barCheckDone.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckDone_ItemClick);
            // 
            // barEDIStyle
            // 
            this.barEDIStyle.Caption = "EDI_Style";
            this.barEDIStyle.Glyph = ((System.Drawing.Image)(resources.GetObject("barEDIStyle.Glyph")));
            this.barEDIStyle.Id = 22;
            this.barEDIStyle.Name = "barEDIStyle";
            this.barEDIStyle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEDIStyle_ItemClick);
            // 
            // barDeclaration
            // 
            this.barDeclaration.Caption = "Declaration";
            this.barDeclaration.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.add_square;
            this.barDeclaration.Id = 45;
            this.barDeclaration.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnDeclaration, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bntImportDeclaration, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barDeclaration.Name = "barDeclaration";
            // 
            // btnDeclaration
            // 
            this.btnDeclaration.Caption = "Declaration";
            this.btnDeclaration.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Transfer_16;
            this.btnDeclaration.Id = 43;
            this.btnDeclaration.Name = "btnDeclaration";
            this.btnDeclaration.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bntDeclaration_ItemClick);
            // 
            // bntImportDeclaration
            // 
            this.bntImportDeclaration.Caption = "Import Declaration";
            this.bntImportDeclaration.Glyph = ((System.Drawing.Image)(resources.GetObject("bntImportDeclaration.Glyph")));
            this.bntImportDeclaration.Id = 44;
            this.bntImportDeclaration.Name = "bntImportDeclaration";
            this.bntImportDeclaration.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bntImportDeclaration_ItemClick);
            // 
            // barAddHbl
            // 
            this.barAddHbl.Caption = "批量增加报关单";
            this.barAddHbl.Glyph = ((System.Drawing.Image)(resources.GetObject("barAddHbl.Glyph")));
            this.barAddHbl.Id = 36;
            this.barAddHbl.Name = "barAddHbl";
            this.barAddHbl.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barAddHbl.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddHbl_ItemClick);
            // 
            // barGetCommodity
            // 
            this.barGetCommodity.Caption = "获取品名";
            this.barGetCommodity.Glyph = ((System.Drawing.Image)(resources.GetObject("barGetCommodity.Glyph")));
            this.barGetCommodity.Id = 37;
            this.barGetCommodity.Name = "barGetCommodity";
            this.barGetCommodity.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barGetCommodity.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barGetCommodity_ItemClick);
            // 
            // barE_MBL
            // 
            this.barE_MBL.Caption = "&E-MBL";
            this.barE_MBL.Glyph = ((System.Drawing.Image)(resources.GetObject("barE_MBL.Glyph")));
            this.barE_MBL.Id = 9;
            this.barE_MBL.Name = "barE_MBL";
            this.barE_MBL.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barE_MBL_ItemClick);
            // 
            // barEDIANL
            // 
            this.barEDIANL.Caption = "EDIANL";
            this.barEDIANL.Glyph = ((System.Drawing.Image)(resources.GetObject("barEDIANL.Glyph")));
            this.barEDIANL.Id = 31;
            this.barEDIANL.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBooking),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPreplan),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSupplement),
            new DevExpress.XtraBars.LinkPersistInfo(this.barWharf),
            new DevExpress.XtraBars.LinkPersistInfo(this.barVGM)});
            this.barEDIANL.Name = "barEDIANL";
            this.barEDIANL.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barBooking
            // 
            this.barBooking.Caption = "电子订舱";
            this.barBooking.Glyph = ((System.Drawing.Image)(resources.GetObject("barBooking.Glyph")));
            this.barBooking.Id = 32;
            this.barBooking.Name = "barBooking";
            this.barBooking.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBooking_ItemClick);
            // 
            // barPreplan
            // 
            this.barPreplan.Caption = "电子预配";
            this.barPreplan.Glyph = ((System.Drawing.Image)(resources.GetObject("barPreplan.Glyph")));
            this.barPreplan.Id = 33;
            this.barPreplan.Name = "barPreplan";
            this.barPreplan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPreplan_ItemClick);
            // 
            // barSupplement
            // 
            this.barSupplement.Caption = "电子补料";
            this.barSupplement.Glyph = ((System.Drawing.Image)(resources.GetObject("barSupplement.Glyph")));
            this.barSupplement.Id = 34;
            this.barSupplement.Name = "barSupplement";
            this.barSupplement.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSupplement_ItemClick);
            // 
            // barWharf
            // 
            this.barWharf.Caption = "电子码头";
            this.barWharf.Glyph = ((System.Drawing.Image)(resources.GetObject("barWharf.Glyph")));
            this.barWharf.Id = 35;
            this.barWharf.Name = "barWharf";
            this.barWharf.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barWharf_ItemClick);
            // 
            // barVGM
            // 
            this.barVGM.Caption = "VGM";
            this.barVGM.Glyph = ((System.Drawing.Image)(resources.GetObject("barVGM.Glyph")));
            this.barVGM.Id = 38;
            this.barVGM.Name = "barVGM";
            this.barVGM.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barVGM_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("barRefresh.Glyph")));
            this.barRefresh.Hint = "&Refresh";
            this.barRefresh.Id = 17;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barBill
            // 
            this.barBill.Caption = "&Bill";
            this.barBill.Glyph = ((System.Drawing.Image)(resources.GetObject("barBill.Glyph")));
            this.barBill.Id = 21;
            this.barBill.Name = "barBill";
            this.barBill.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBill_ItemClick);
            // 
            // barbl
            // 
            this.barbl.Caption = "Mail BL Copy To Customer";
            this.barbl.Glyph = ((System.Drawing.Image)(resources.GetObject("barbl.Glyph")));
            this.barbl.Id = 23;
            this.barbl.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barchs),
            new DevExpress.XtraBars.LinkPersistInfo(this.bareng)});
            this.barbl.Name = "barbl";
            // 
            // barchs
            // 
            this.barchs.Caption = "Mail BL Copy To Customer (CHS)";
            this.barchs.Id = 24;
            this.barchs.Name = "barchs";
            this.barchs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barchs_ItemClick);
            // 
            // bareng
            // 
            this.bareng.Caption = "Mail BL Copy To Customer (ENG)";
            this.bareng.Id = 25;
            this.bareng.Name = "bareng";
            this.bareng.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bareng_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = ((System.Drawing.Image)(resources.GetObject("barClose.Glyph")));
            this.barClose.Id = 10;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barSaveVGM
            // 
            this.barSaveVGM.Caption = "保存VGM";
            this.barSaveVGM.Glyph = ((System.Drawing.Image)(resources.GetObject("barSaveVGM.Glyph")));
            this.barSaveVGM.Id = 40;
            this.barSaveVGM.Name = "barSaveVGM";
            this.barSaveVGM.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSaveVGM.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barSaveVGM.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSaveVGM_ItemClick);
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
            this.barSavingTools.OptionsBar.UseWholeRow = true;
            this.barSavingTools.Text = "barSavingTools";
            // 
            // barCancel
            // 
            this.barCancel.Caption = "Cancel";
            this.barCancel.Glyph = ((System.Drawing.Image)(resources.GetObject("barCancel.Glyph")));
            this.barCancel.Id = 27;
            this.barCancel.Name = "barCancel";
            // 
            // barlabMessage
            // 
            this.barlabMessage.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barlabMessage.Caption = "Message";
            this.barlabMessage.Glyph = ((System.Drawing.Image)(resources.GetObject("barlabMessage.Glyph")));
            this.barlabMessage.Id = 28;
            this.barlabMessage.Name = "barlabMessage";
            this.barlabMessage.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barlabSeconds
            // 
            this.barlabSeconds.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barlabSeconds.Caption = "0s";
            this.barlabSeconds.Id = 30;
            this.barlabSeconds.Name = "barlabSeconds";
            this.barlabSeconds.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(982, 87);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 579);
            this.barDockControlBottom.Size = new System.Drawing.Size(982, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 87);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 492);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(982, 87);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 492);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "&Output";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "获取品名";
            this.barButtonItem1.Id = 39;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // txtWoodPacking
            // 
            this.txtWoodPacking.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "WoodPacking", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtWoodPacking.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtWoodPacking.EditValue = "WOOD PACKAGING MATERIAL IS\r\nUSED IN THE ";
            this.txtWoodPacking.Location = new System.Drawing.Point(3, 259);
            this.txtWoodPacking.MenuManager = this.barManager1;
            this.txtWoodPacking.Name = "txtWoodPacking";
            this.txtWoodPacking.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWoodPacking.Size = new System.Drawing.Size(270, 109);
            this.txtWoodPacking.TabIndex = 1;
            // 
            // labFreightDescription
            // 
            this.labFreightDescription.Location = new System.Drawing.Point(541, 235);
            this.labFreightDescription.Name = "labFreightDescription";
            this.labFreightDescription.Size = new System.Drawing.Size(98, 14);
            this.labFreightDescription.TabIndex = 0;
            this.labFreightDescription.Text = "FreightDescription";
            // 
            // labContainerInfo
            // 
            this.labContainerInfo.Location = new System.Drawing.Point(3, 123);
            this.labContainerInfo.Name = "labContainerInfo";
            this.labContainerInfo.Size = new System.Drawing.Size(49, 14);
            this.labContainerInfo.TabIndex = 0;
            this.labContainerInfo.Text = "CTN Info";
            // 
            // labCtnQtyInfo
            // 
            this.labCtnQtyInfo.Location = new System.Drawing.Point(541, 144);
            this.labCtnQtyInfo.Name = "labCtnQtyInfo";
            this.labCtnQtyInfo.Size = new System.Drawing.Size(73, 14);
            this.labCtnQtyInfo.TabIndex = 0;
            this.labCtnQtyInfo.Text = "CTN Qty Info";
            // 
            // labMarks
            // 
            this.labMarks.Location = new System.Drawing.Point(541, 4);
            this.labMarks.Name = "labMarks";
            this.labMarks.Size = new System.Drawing.Size(30, 14);
            this.labMarks.TabIndex = 0;
            this.labMarks.Text = "Marks";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.navBarControl1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 87);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(982, 492);
            this.panelMain.TabIndex = 0;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarBaseInfo;
            this.navBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer4);
            this.navBarControl1.Controls.Add(this.navBarControlContainerContact);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer5);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer6);
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarBaseInfo,
            this.navBarBLInfo,
            this.navBarCargo,
            this.navAMS,
            this.navBarIssueInfo,
            this.navBarContact,
            this.navBarGroup1});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItem1,
            this.navBarItem2});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(833, 487);
            this.navBarControl1.SkinExplorerBarViewScrollStyle = DevExpress.XtraNavBar.SkinExplorerBarViewScrollStyle.ScrollBar;
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarBaseInfo
            // 
            this.navBarBaseInfo.Caption = "Base Info";
            this.navBarBaseInfo.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarBaseInfo.Expanded = true;
            this.navBarBaseInfo.GroupClientHeight = 119;
            this.navBarBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBaseInfo.Name = "navBarBaseInfo";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.panel1);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(812, 117);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel1.Controls.Add(this.labContractName);
            this.panel1.Controls.Add(this.mcmbBookingParty);
            this.panel1.Controls.Add(this.labelControl14);
            this.panel1.Controls.Add(this.chkAMS);
            this.panel1.Controls.Add(this.chkHasContract);
            this.panel1.Controls.Add(this.txtContractNo);
            this.panel1.Controls.Add(this.txtTelexNo);
            this.panel1.Controls.Add(this.txtRleaseBy);
            this.panel1.Controls.Add(this.txtHBLNO);
            this.panel1.Controls.Add(this.stxtRefNo);
            this.panel1.Controls.Add(this.dteBookingDate);
            this.panel1.Controls.Add(this.cmbIssueType);
            this.panel1.Controls.Add(this.labTelexNo);
            this.panel1.Controls.Add(this.cmbReleaseType);
            this.panel1.Controls.Add(this.labReleaseBy);
            this.panel1.Controls.Add(this.labType);
            this.panel1.Controls.Add(this.labHBLNO);
            this.panel1.Controls.Add(this.labMBLNo);
            this.panel1.Controls.Add(this.labReleaseType);
            this.panel1.Controls.Add(this.txtNo);
            this.panel1.Controls.Add(this.labRefNo);
            this.panel1.Controls.Add(this.labReleaseDate);
            this.panel1.Controls.Add(this.stxtChecker);
            this.panel1.Controls.Add(this.labChecker);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(816, 120);
            this.panel1.TabIndex = 0;
            // 
            // labContractName
            // 
            this.labContractName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labContractName.Location = new System.Drawing.Point(589, 8);
            this.labContractName.Name = "labContractName";
            this.labContractName.Size = new System.Drawing.Size(215, 0);
            this.labContractName.TabIndex = 806;
            // 
            // mcmbBookingParty
            // 
            this.mcmbBookingParty.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "BookingPartyID", true));
            this.mcmbBookingParty.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bindingSource1, "BookingPartyName", true));
            this.mcmbBookingParty.EditText = "";
            this.mcmbBookingParty.EditValue = null;
            this.mcmbBookingParty.Location = new System.Drawing.Point(479, 91);
            this.mcmbBookingParty.Name = "mcmbBookingParty";
            this.mcmbBookingParty.ReadOnly = false;
            this.mcmbBookingParty.RefreshButtonToolTip = "";
            this.mcmbBookingParty.ShowRefreshButton = false;
            this.mcmbBookingParty.Size = new System.Drawing.Size(217, 21);
            this.mcmbBookingParty.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbBookingParty.TabIndex = 9;
            this.mcmbBookingParty.ToolTip = "";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(386, 94);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(71, 14);
            this.labelControl14.TabIndex = 805;
            this.labelControl14.Text = "BookingParty";
            // 
            // chkAMS
            // 
            this.chkAMS.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsCarrierSendAMS", true));
            this.chkAMS.Location = new System.Drawing.Point(700, 92);
            this.chkAMS.Name = "chkAMS";
            this.chkAMS.Properties.Caption = "CarrierSendAMS";
            this.chkAMS.Size = new System.Drawing.Size(112, 19);
            this.chkAMS.TabIndex = 7;
            // 
            // chkHasContract
            // 
            this.chkHasContract.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsContract", true));
            this.chkHasContract.Location = new System.Drawing.Point(384, 13);
            this.chkHasContract.Name = "chkHasContract";
            this.chkHasContract.Properties.Caption = "Contract";
            this.chkHasContract.Size = new System.Drawing.Size(69, 19);
            this.chkHasContract.TabIndex = 5;
            this.chkHasContract.CheckedChanged += new System.EventHandler(this.chkHasContract_CheckedChanged);
            // 
            // txtContractNo
            // 
            this.txtContractNo.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "ContractID", true));
            this.txtContractNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ContractNo", true));
            this.txtContractNo.EditValue = "";
            this.txtContractNo.Location = new System.Drawing.Point(478, 12);
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtContractNo.Size = new System.Drawing.Size(107, 21);
            this.txtContractNo.TabIndex = 6;
            this.txtContractNo.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtContractNo_ButtonClick);
            this.txtContractNo.Click += new System.EventHandler(this.txtContractNo_Click);
            this.txtContractNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContractNo_KeyDown);
            // 
            // txtTelexNo
            // 
            this.txtTelexNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "TelexNo", true));
            this.txtTelexNo.Location = new System.Drawing.Point(670, 64);
            this.txtTelexNo.Name = "txtTelexNo";
            this.txtTelexNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTelexNo.Properties.ReadOnly = true;
            this.txtTelexNo.Size = new System.Drawing.Size(136, 21);
            this.txtTelexNo.TabIndex = 3;
            this.txtTelexNo.TabStop = false;
            // 
            // txtRleaseBy
            // 
            this.txtRleaseBy.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ReleaseBy", true));
            this.txtRleaseBy.Location = new System.Drawing.Point(479, 64);
            this.txtRleaseBy.Name = "txtRleaseBy";
            this.txtRleaseBy.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRleaseBy.Properties.ReadOnly = true;
            this.txtRleaseBy.Size = new System.Drawing.Size(105, 21);
            this.txtRleaseBy.TabIndex = 3;
            this.txtRleaseBy.TabStop = false;
            // 
            // txtHBLNO
            // 
            this.txtHBLNO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "HBLNos", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtHBLNO.Location = new System.Drawing.Point(82, 64);
            this.txtHBLNO.MenuManager = this.barManager1;
            this.txtHBLNO.Name = "txtHBLNO";
            this.txtHBLNO.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHBLNO.Properties.ReadOnly = true;
            this.txtHBLNO.Size = new System.Drawing.Size(285, 21);
            this.txtHBLNO.TabIndex = 3;
            this.txtHBLNO.TabStop = false;
            // 
            // cmbIssueType
            // 
            this.cmbIssueType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IssueType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbIssueType.Location = new System.Drawing.Point(262, 13);
            this.cmbIssueType.Name = "cmbIssueType";
            this.cmbIssueType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbIssueType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbIssueType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbIssueType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbIssueType.Size = new System.Drawing.Size(105, 21);
            this.cmbIssueType.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbIssueType.TabIndex = 1;
            // 
            // labTelexNo
            // 
            this.labTelexNo.Location = new System.Drawing.Point(593, 67);
            this.labTelexNo.Name = "labTelexNo";
            this.labTelexNo.Size = new System.Drawing.Size(45, 14);
            this.labTelexNo.TabIndex = 0;
            this.labTelexNo.Text = "TelexNo";
            // 
            // labReleaseBy
            // 
            this.labReleaseBy.Location = new System.Drawing.Point(386, 67);
            this.labReleaseBy.Name = "labReleaseBy";
            this.labReleaseBy.Size = new System.Drawing.Size(54, 14);
            this.labReleaseBy.TabIndex = 0;
            this.labReleaseBy.Text = "ReleaseBy";
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(219, 16);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(28, 14);
            this.labType.TabIndex = 6;
            this.labType.Text = "Type";
            // 
            // labHBLNO
            // 
            this.labHBLNO.Location = new System.Drawing.Point(3, 67);
            this.labHBLNO.Name = "labHBLNO";
            this.labHBLNO.Size = new System.Drawing.Size(38, 14);
            this.labHBLNO.TabIndex = 0;
            this.labHBLNO.Text = "HBLNO";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.panel2);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(812, 368);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel2.Controls.Add(this.checkHasFee);
            this.panel2.Controls.Add(this.dteGateInDate);
            this.panel2.Controls.Add(this.labGateInDate);
            this.panel2.Controls.Add(this.txtNBPODCode);
            this.panel2.Controls.Add(this.lblNBPODCode);
            this.panel2.Controls.Add(this.stxtPlacePay);
            this.panel2.Controls.Add(this.chkThirdPlacePay);
            this.panel2.Controls.Add(this.labelControl3);
            this.panel2.Controls.Add(this.labelControl2);
            this.panel2.Controls.Add(this.labelControl1);
            this.panel2.Controls.Add(this.dtETA);
            this.panel2.Controls.Add(this.dtETD);
            this.panel2.Controls.Add(this.dtPETD);
            this.panel2.Controls.Add(this.labAMSInfo);
            this.panel2.Controls.Add(this.stxtAgent);
            this.panel2.Controls.Add(this.chkShowVoyage);
            this.panel2.Controls.Add(this.chkShowPreVoyage);
            this.panel2.Controls.Add(this.cmbPaymentTerm);
            this.panel2.Controls.Add(this.labPlaceOfDelivery);
            this.panel2.Controls.Add(this.labPOD);
            this.panel2.Controls.Add(this.stxtPreVoyage);
            this.panel2.Controls.Add(this.stxtVoyage);
            this.panel2.Controls.Add(this.labPreVoyage);
            this.panel2.Controls.Add(this.stxtPlaceOfDelivery);
            this.panel2.Controls.Add(this.labPOL2);
            this.panel2.Controls.Add(this.labPaymentTerm);
            this.panel2.Controls.Add(this.txtPODName);
            this.panel2.Controls.Add(this.txtPlaceOfDeliveryName);
            this.panel2.Controls.Add(this.labPlaceOfReceipt);
            this.panel2.Controls.Add(this.labVoyage);
            this.panel2.Controls.Add(this.txtPOLName);
            this.panel2.Controls.Add(this.cmbTransportClause);
            this.panel2.Controls.Add(this.labTransportClause);
            this.panel2.Controls.Add(this.stxtPlaceOfReceipt);
            this.panel2.Controls.Add(this.txtPlaceOfReceiptName);
            this.panel2.Controls.Add(this.txtFinalDestinationName);
            this.panel2.Controls.Add(this.labFinalDestination);
            this.panel2.Controls.Add(this.stxtFinalDestination);
            this.panel2.Controls.Add(this.stxtShipper);
            this.panel2.Controls.Add(this.labShipper);
            this.panel2.Controls.Add(this.labAgent);
            this.panel2.Controls.Add(this.labConsignee);
            this.panel2.Controls.Add(this.stxtConsignee);
            this.panel2.Controls.Add(this.labNotifyParty);
            this.panel2.Controls.Add(this.txtNotifyPartyDescription);
            this.panel2.Controls.Add(this.stxtNotifyParty);
            this.panel2.Controls.Add(this.txtConsigneeDescription);
            this.panel2.Controls.Add(this.txtAgentDescription);
            this.panel2.Controls.Add(this.txtShipperDescription);
            this.panel2.Controls.Add(this.txtPOLCode);
            this.panel2.Controls.Add(this.txtPODCode);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(816, 368);
            this.panel2.TabIndex = 1;
            // 
            // checkHasFee
            // 
            this.checkHasFee.Location = new System.Drawing.Point(590, 173);
            this.checkHasFee.Name = "checkHasFee";
            this.checkHasFee.Properties.Caption = "HasFee";
            this.checkHasFee.Size = new System.Drawing.Size(60, 19);
            this.checkHasFee.TabIndex = 572;
            this.checkHasFee.Tag = "";
            // 
            // dteGateInDate
            // 
            this.dteGateInDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "GateInDate", true));
            this.dteGateInDate.EditValue = null;
            this.dteGateInDate.Location = new System.Drawing.Point(716, 172);
            this.dteGateInDate.Name = "dteGateInDate";
            this.dteGateInDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteGateInDate.Properties.Mask.EditMask = "";
            this.dteGateInDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteGateInDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteGateInDate.Size = new System.Drawing.Size(91, 21);
            this.dteGateInDate.TabIndex = 571;
            // 
            // labGateInDate
            // 
            this.labGateInDate.Location = new System.Drawing.Point(676, 176);
            this.labGateInDate.Name = "labGateInDate";
            this.labGateInDate.Size = new System.Drawing.Size(27, 14);
            this.labGateInDate.TabIndex = 570;
            this.labGateInDate.Text = "GInD";
            // 
            // txtNBPODCode
            // 
            this.txtNBPODCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "NBPODCode", true));
            this.txtNBPODCode.Location = new System.Drawing.Point(694, 342);
            this.txtNBPODCode.MenuManager = this.barManager1;
            this.txtNBPODCode.Name = "txtNBPODCode";
            this.txtNBPODCode.Size = new System.Drawing.Size(112, 21);
            this.txtNBPODCode.TabIndex = 567;
            this.txtNBPODCode.Visible = false;
            // 
            // lblNBPODCode
            // 
            this.lblNBPODCode.Location = new System.Drawing.Point(595, 346);
            this.lblNBPODCode.Name = "lblNBPODCode";
            this.lblNBPODCode.Size = new System.Drawing.Size(67, 14);
            this.lblNBPODCode.TabIndex = 566;
            this.lblNBPODCode.Text = "NBPODCode";
            this.lblNBPODCode.Visible = false;
            // 
            // chkThirdPlacePay
            // 
            this.chkThirdPlacePay.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsThirdPlacePayOrder", true));
            this.chkThirdPlacePay.Location = new System.Drawing.Point(384, 342);
            this.chkThirdPlacePay.MenuManager = this.barManager1;
            this.chkThirdPlacePay.Name = "chkThirdPlacePay";
            this.chkThirdPlacePay.Properties.Caption = "Third Pay.";
            this.chkThirdPlacePay.Size = new System.Drawing.Size(92, 19);
            this.chkThirdPlacePay.TabIndex = 24;
            this.chkThirdPlacePay.ToolTip = "Third Payment";
            this.chkThirdPlacePay.CheckedChanged += new System.EventHandler(this.chkThirdPlacePay_CheckedChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(680, 249);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(23, 14);
            this.labelControl3.TabIndex = 565;
            this.labelControl3.Text = "ETA";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(680, 223);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(23, 14);
            this.labelControl2.TabIndex = 565;
            this.labelControl2.Text = "ETD";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(680, 198);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(23, 14);
            this.labelControl1.TabIndex = 565;
            this.labelControl1.Text = "ETD";
            // 
            // dtETA
            // 
            this.dtETA.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ETA", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtETA.EditValue = null;
            this.dtETA.Location = new System.Drawing.Point(716, 246);
            this.dtETA.Name = "dtETA";
            this.dtETA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtETA.Properties.Mask.EditMask = "";
            this.dtETA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtETA.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtETA.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtETA.Size = new System.Drawing.Size(91, 21);
            this.dtETA.TabIndex = 20;
            this.dtETA.TabStop = false;
            // 
            // dtETD
            // 
            this.dtETD.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ETD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtETD.EditValue = new System.DateTime(2012, 8, 10, 0, 0, 0, 0);
            this.dtETD.Location = new System.Drawing.Point(716, 221);
            this.dtETD.Name = "dtETD";
            this.dtETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtETD.Properties.Mask.EditMask = "";
            this.dtETD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtETD.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtETD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtETD.Size = new System.Drawing.Size(91, 21);
            this.dtETD.TabIndex = 17;
            this.dtETD.TabStop = false;
            // 
            // labAMSInfo
            // 
            this.labAMSInfo.Location = new System.Drawing.Point(386, 32);
            this.labAMSInfo.Name = "labAMSInfo";
            this.labAMSInfo.Size = new System.Drawing.Size(54, 14);
            this.labAMSInfo.TabIndex = 45;
            this.labAMSInfo.Text = "AMS Info.";
            // 
            // stxtAgent
            // 
            this.stxtAgent.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "AgentID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtAgent.DataSource = null;
            this.stxtAgent.DisplayMember = "EName";
            this.stxtAgent.EditValue = null;
            this.stxtAgent.Location = new System.Drawing.Point(478, 2);
            this.stxtAgent.Margin = new System.Windows.Forms.Padding(0);
            this.stxtAgent.Name = "stxtAgent";
            this.stxtAgent.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Bottom;
            this.stxtAgent.Size = new System.Drawing.Size(330, 21);
            this.stxtAgent.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtAgent.TabIndex = 6;
            this.stxtAgent.Tag = null;
            this.stxtAgent.ValueMember = "ID";
            // 
            // chkShowVoyage
            // 
            this.chkShowVoyage.Location = new System.Drawing.Point(706, 150);
            this.chkShowVoyage.Name = "chkShowVoyage";
            this.chkShowVoyage.Properties.Caption = "Show";
            this.chkShowVoyage.Size = new System.Drawing.Size(60, 19);
            this.chkShowVoyage.TabIndex = 11;
            this.chkShowVoyage.Tag = "";
            this.chkShowVoyage.CheckedChanged += new System.EventHandler(this.chkShowVoyage_CheckedChanged);
            // 
            // chkShowPreVoyage
            // 
            this.chkShowPreVoyage.Location = new System.Drawing.Point(705, 123);
            this.chkShowPreVoyage.Name = "chkShowPreVoyage";
            this.chkShowPreVoyage.Properties.Caption = "Show";
            this.chkShowPreVoyage.Size = new System.Drawing.Size(60, 19);
            this.chkShowPreVoyage.TabIndex = 9;
            this.chkShowPreVoyage.CheckedChanged += new System.EventHandler(this.chkShowPreVoyage_CheckedChanged);
            // 
            // labPlaceOfDelivery
            // 
            this.labPlaceOfDelivery.Location = new System.Drawing.Point(387, 275);
            this.labPlaceOfDelivery.Name = "labPlaceOfDelivery";
            this.labPlaceOfDelivery.Size = new System.Drawing.Size(83, 14);
            this.labPlaceOfDelivery.TabIndex = 38;
            this.labPlaceOfDelivery.Text = "PlaceOfDelivery";
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(387, 250);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(24, 14);
            this.labPOD.TabIndex = 21;
            this.labPOD.Text = "POD";
            // 
            // labPreVoyage
            // 
            this.labPreVoyage.Location = new System.Drawing.Point(386, 126);
            this.labPreVoyage.Name = "labPreVoyage";
            this.labPreVoyage.Size = new System.Drawing.Size(59, 14);
            this.labPreVoyage.TabIndex = 25;
            this.labPreVoyage.Text = "PreVoyage";
            // 
            // labPOL2
            // 
            this.labPOL2.Location = new System.Drawing.Point(387, 222);
            this.labPOL2.Name = "labPOL2";
            this.labPOL2.Size = new System.Drawing.Size(22, 14);
            this.labPOL2.TabIndex = 28;
            this.labPOL2.Text = "POL";
            // 
            // labPaymentTerm
            // 
            this.labPaymentTerm.Location = new System.Drawing.Point(594, 320);
            this.labPaymentTerm.Name = "labPaymentTerm";
            this.labPaymentTerm.Size = new System.Drawing.Size(81, 14);
            this.labPaymentTerm.TabIndex = 26;
            this.labPaymentTerm.Text = "Payment Term";
            // 
            // labPlaceOfReceipt
            // 
            this.labPlaceOfReceipt.Location = new System.Drawing.Point(387, 198);
            this.labPlaceOfReceipt.Name = "labPlaceOfReceipt";
            this.labPlaceOfReceipt.Size = new System.Drawing.Size(82, 14);
            this.labPlaceOfReceipt.TabIndex = 37;
            this.labPlaceOfReceipt.Text = "PlaceOfReceipt";
            // 
            // labVoyage
            // 
            this.labVoyage.Location = new System.Drawing.Point(386, 152);
            this.labVoyage.Name = "labVoyage";
            this.labVoyage.Size = new System.Drawing.Size(41, 14);
            this.labVoyage.TabIndex = 23;
            this.labVoyage.Text = "Voyage";
            // 
            // labTransportClause
            // 
            this.labTransportClause.Location = new System.Drawing.Point(386, 320);
            this.labTransportClause.Name = "labTransportClause";
            this.labTransportClause.Size = new System.Drawing.Size(87, 14);
            this.labTransportClause.TabIndex = 40;
            this.labTransportClause.Text = "TransportClause";
            // 
            // labFinalDestination
            // 
            this.labFinalDestination.Location = new System.Drawing.Point(387, 296);
            this.labFinalDestination.Name = "labFinalDestination";
            this.labFinalDestination.Size = new System.Drawing.Size(84, 14);
            this.labFinalDestination.TabIndex = 27;
            this.labFinalDestination.Text = "FinalDestination";
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.panel3);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(812, 397);
            this.navBarGroupControlContainer3.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel3.Controls.Add(this.chkSpecial);
            this.panel3.Controls.Add(this.container);
            this.panel3.Controls.Add(this.txtHSCode);
            this.panel3.Controls.Add(this.IsManualControl);
            this.panel3.Controls.Add(this.labHSCode);
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
            this.panel3.Controls.Add(this.labContainerInfo);
            this.panel3.Controls.Add(this.cmbWeightUnit);
            this.panel3.Controls.Add(this.txtCtnQtyInfo);
            this.panel3.Controls.Add(this.btnContainer);
            this.panel3.Controls.Add(this.labFreightDescription);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.txtFreightDescription);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(815, 398);
            this.panel3.TabIndex = 2;
            // 
            // chkSpecial
            // 
            this.chkSpecial.Location = new System.Drawing.Point(463, 2);
            this.chkSpecial.Name = "chkSpecial";
            this.chkSpecial.Properties.Caption = "Special";
            this.chkSpecial.Size = new System.Drawing.Size(69, 19);
            this.chkSpecial.TabIndex = 570;
            // 
            // container
            // 
            this.container.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.container.Appearance.Options.UseBackColor = true;
            this.container.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Container", true));
            this.container.Location = new System.Drawing.Point(1, 27);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(257, 21);
            this.container.SpecifiedBackColor = System.Drawing.Color.White;
            this.container.TabIndex = 47;
            // 
            // txtHSCode
            // 
            this.txtHSCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "HSCODE", true));
            this.txtHSCode.Location = new System.Drawing.Point(541, 366);
            this.txtHSCode.MenuManager = this.barManager1;
            this.txtHSCode.Name = "txtHSCode";
            this.txtHSCode.Size = new System.Drawing.Size(266, 21);
            this.txtHSCode.TabIndex = 569;
            // 
            // IsManualControl
            // 
            this.IsManualControl.Location = new System.Drawing.Point(153, 122);
            this.IsManualControl.MenuManager = this.barManager1;
            this.IsManualControl.Name = "IsManualControl";
            this.IsManualControl.Properties.Caption = "ManualControl";
            this.IsManualControl.Size = new System.Drawing.Size(103, 19);
            this.IsManualControl.TabIndex = 41;
            this.IsManualControl.CheckedChanged += new System.EventHandler(this.IsManualControl_CheckedChanged);
            // 
            // labHSCode
            // 
            this.labHSCode.Location = new System.Drawing.Point(541, 347);
            this.labHSCode.Name = "labHSCode";
            this.labHSCode.Size = new System.Drawing.Size(43, 14);
            this.labHSCode.TabIndex = 568;
            this.labHSCode.Text = "HSCode";
            // 
            // numMeasurement
            // 
            this.numMeasurement.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Measurement", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numMeasurement.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numMeasurement.Location = new System.Drawing.Point(84, 98);
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
            this.numMeasurement.TabIndex = 2;
            // 
            // numWeight
            // 
            this.numWeight.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Weight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numWeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numWeight.Location = new System.Drawing.Point(84, 75);
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
            this.numWeight.TabIndex = 1;
            // 
            // numQuantity
            // 
            this.numQuantity.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Quantity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numQuantity.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numQuantity.Location = new System.Drawing.Point(84, 52);
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
            this.txtCtnInfo.Location = new System.Drawing.Point(5, 143);
            this.txtCtnInfo.MenuManager = this.barManager1;
            this.txtCtnInfo.Name = "txtCtnInfo";
            this.txtCtnInfo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCtnInfo.Properties.ReadOnly = true;
            this.txtCtnInfo.Size = new System.Drawing.Size(251, 244);
            this.txtCtnInfo.TabIndex = 6;
            this.txtCtnInfo.TabStop = false;
            // 
            // labNSITBLNotes
            // 
            this.labNSITBLNotes.Location = new System.Drawing.Point(267, 8);
            this.labNSITBLNotes.Name = "labNSITBLNotes";
            this.labNSITBLNotes.Size = new System.Drawing.Size(193, 14);
            this.labNSITBLNotes.TabIndex = 0;
            this.labNSITBLNotes.Text = "Not shown in the bill of lading note";
            // 
            // txtNSITBLNotes
            // 
            this.txtNSITBLNotes.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "NSITBLNotes", true));
            this.txtNSITBLNotes.Location = new System.Drawing.Point(265, 28);
            this.txtNSITBLNotes.Name = "txtNSITBLNotes";
            this.txtNSITBLNotes.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNSITBLNotes.Size = new System.Drawing.Size(248, 100);
            this.txtNSITBLNotes.TabIndex = 9;
            // 
            // navBarGroupControlContainer4
            // 
            this.navBarGroupControlContainer4.Controls.Add(this.panel4);
            this.navBarGroupControlContainer4.Name = "navBarGroupControlContainer4";
            this.navBarGroupControlContainer4.Size = new System.Drawing.Size(812, 29);
            this.navBarGroupControlContainer4.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel4.Controls.Add(this.mcmbIssueBy);
            this.panel4.Controls.Add(this.dteIssue);
            this.panel4.Controls.Add(this.stxtIssuePlace);
            this.panel4.Controls.Add(this.labIssueDate);
            this.panel4.Controls.Add(this.labIssuePlace);
            this.panel4.Controls.Add(this.labIssueBy);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(816, 29);
            this.panel4.TabIndex = 0;
            // 
            // mcmbIssueBy
            // 
            this.mcmbIssueBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mcmbIssueBy.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bindingSource1, "IssueByName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mcmbIssueBy.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IssueByID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mcmbIssueBy.EditText = "";
            this.mcmbIssueBy.EditValue = null;
            this.mcmbIssueBy.Location = new System.Drawing.Point(56, 5);
            this.mcmbIssueBy.Name = "mcmbIssueBy";
            this.mcmbIssueBy.ReadOnly = false;
            this.mcmbIssueBy.RefreshButtonToolTip = "";
            this.mcmbIssueBy.ShowRefreshButton = false;
            this.mcmbIssueBy.Size = new System.Drawing.Size(200, 21);
            this.mcmbIssueBy.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.mcmbIssueBy.TabIndex = 0;
            this.mcmbIssueBy.ToolTip = "";
            // 
            // navBarControlContainerContact
            // 
            this.navBarControlContainerContact.Name = "navBarControlContainerContact";
            this.navBarControlContainerContact.Size = new System.Drawing.Size(812, 408);
            this.navBarControlContainerContact.TabIndex = 3;
            // 
            // navBarGroupControlContainer5
            // 
            this.navBarGroupControlContainer5.Controls.Add(this.panel5);
            this.navBarGroupControlContainer5.Name = "navBarGroupControlContainer5";
            this.navBarGroupControlContainer5.Size = new System.Drawing.Size(812, 67);
            this.navBarGroupControlContainer5.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel5.Controls.Add(this.dateWeightDate);
            this.panel5.Controls.Add(this.labWeightingDate);
            this.panel5.Controls.Add(this.dateVerifiedDate);
            this.panel5.Controls.Add(this.labVerifiedDate);
            this.panel5.Controls.Add(this.txtVerifiedPerson);
            this.panel5.Controls.Add(this.labVerifiedPerson);
            this.panel5.Controls.Add(this.labWeightSite);
            this.panel5.Controls.Add(this.cmbWeightSite);
            this.panel5.Controls.Add(this.txtWeightSite);
            this.panel5.Controls.Add(this.txtResponsiblePerson);
            this.panel5.Controls.Add(this.labResponsiblePerson);
            this.panel5.Controls.Add(this.txtResponsibleParty);
            this.panel5.Controls.Add(this.labResponsibleParty);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(816, 73);
            this.panel5.TabIndex = 4;
            // 
            // labWeightingDate
            // 
            this.labWeightingDate.Location = new System.Drawing.Point(7, 42);
            this.labWeightingDate.Name = "labWeightingDate";
            this.labWeightingDate.Size = new System.Drawing.Size(77, 14);
            this.labWeightingDate.TabIndex = 45;
            this.labWeightingDate.Text = "WeighingDate";
            // 
            // labVerifiedDate
            // 
            this.labVerifiedDate.Location = new System.Drawing.Point(487, 42);
            this.labVerifiedDate.Name = "labVerifiedDate";
            this.labVerifiedDate.Size = new System.Drawing.Size(67, 14);
            this.labVerifiedDate.TabIndex = 43;
            this.labVerifiedDate.Text = "VerifiedDate";
            // 
            // txtVerifiedPerson
            // 
            this.txtVerifiedPerson.Location = new System.Drawing.Point(345, 40);
            this.txtVerifiedPerson.Name = "txtVerifiedPerson";
            this.txtVerifiedPerson.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtVerifiedPerson.Properties.Appearance.Options.UseBackColor = true;
            this.txtVerifiedPerson.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVerifiedPerson.Properties.NullText = "请输入英文人名";
            this.txtVerifiedPerson.Size = new System.Drawing.Size(136, 21);
            this.txtVerifiedPerson.TabIndex = 45;
            // 
            // labVerifiedPerson
            // 
            this.labVerifiedPerson.Location = new System.Drawing.Point(242, 42);
            this.labVerifiedPerson.Name = "labVerifiedPerson";
            this.labVerifiedPerson.Size = new System.Drawing.Size(78, 14);
            this.labVerifiedPerson.TabIndex = 41;
            this.labVerifiedPerson.Text = "VerifiedPerson";
            // 
            // labWeightSite
            // 
            this.labWeightSite.Location = new System.Drawing.Point(487, 16);
            this.labWeightSite.Name = "labWeightSite";
            this.labWeightSite.Size = new System.Drawing.Size(83, 14);
            this.labWeightSite.TabIndex = 40;
            this.labWeightSite.Text = "Weighing Place";
            // 
            // txtResponsiblePerson
            // 
            this.txtResponsiblePerson.Location = new System.Drawing.Point(345, 13);
            this.txtResponsiblePerson.Name = "txtResponsiblePerson";
            this.txtResponsiblePerson.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtResponsiblePerson.Properties.Appearance.Options.UseBackColor = true;
            this.txtResponsiblePerson.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtResponsiblePerson.Properties.NullText = "请输入英文人名";
            this.txtResponsiblePerson.Size = new System.Drawing.Size(136, 21);
            this.txtResponsiblePerson.TabIndex = 41;
            // 
            // labResponsiblePerson
            // 
            this.labResponsiblePerson.Location = new System.Drawing.Point(242, 16);
            this.labResponsiblePerson.Name = "labResponsiblePerson";
            this.labResponsiblePerson.Size = new System.Drawing.Size(100, 14);
            this.labResponsiblePerson.TabIndex = 6;
            this.labResponsiblePerson.Text = "ResponsiblePerson";
            // 
            // txtResponsibleParty
            // 
            this.txtResponsibleParty.Location = new System.Drawing.Point(102, 13);
            this.txtResponsibleParty.Name = "txtResponsibleParty";
            this.txtResponsibleParty.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtResponsibleParty.Properties.Appearance.Options.UseBackColor = true;
            this.txtResponsibleParty.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtResponsibleParty.Properties.NullText = "请输入公司英文";
            this.txtResponsibleParty.Size = new System.Drawing.Size(136, 21);
            this.txtResponsibleParty.TabIndex = 40;
            // 
            // labResponsibleParty
            // 
            this.labResponsibleParty.Location = new System.Drawing.Point(7, 16);
            this.labResponsibleParty.Name = "labResponsibleParty";
            this.labResponsibleParty.Size = new System.Drawing.Size(91, 14);
            this.labResponsibleParty.TabIndex = 4;
            this.labResponsibleParty.Text = "ResponsibleParty";
            // 
            // navBarGroupControlContainer6
            // 
            this.navBarGroupControlContainer6.Controls.Add(this.panel7);
            this.navBarGroupControlContainer6.Name = "navBarGroupControlContainer6";
            this.navBarGroupControlContainer6.Size = new System.Drawing.Size(812, 137);
            this.navBarGroupControlContainer6.TabIndex = 5;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel7.Controls.Add(this.txtNotify2);
            this.panel7.Controls.Add(this.labelControl7);
            this.panel7.Controls.Add(this.txtNSITBLNotes);
            this.panel7.Controls.Add(this.labNSITBLNotes);
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(816, 137);
            this.panel7.TabIndex = 1;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(8, 8);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(67, 14);
            this.labelControl7.TabIndex = 7;
            this.labelControl7.Text = "NotifyParty2";
            // 
            // navBarBLInfo
            // 
            this.navBarBLInfo.Caption = "BL Info";
            this.navBarBLInfo.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarBLInfo.Expanded = true;
            this.navBarBLInfo.GroupClientHeight = 370;
            this.navBarBLInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBLInfo.Name = "navBarBLInfo";
            // 
            // navBarCargo
            // 
            this.navBarCargo.Caption = "Cargo";
            this.navBarCargo.ControlContainer = this.navBarGroupControlContainer3;
            this.navBarCargo.Expanded = true;
            this.navBarCargo.GroupClientHeight = 399;
            this.navBarCargo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarCargo.Name = "navBarCargo";
            // 
            // navAMS
            // 
            this.navAMS.Caption = "VGM Info";
            this.navAMS.ControlContainer = this.navBarGroupControlContainer5;
            this.navAMS.Expanded = true;
            this.navAMS.GroupClientHeight = 69;
            this.navAMS.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navAMS.Name = "navAMS";
            // 
            // navBarIssueInfo
            // 
            this.navBarIssueInfo.Caption = "Issue Info.";
            this.navBarIssueInfo.ControlContainer = this.navBarGroupControlContainer4;
            this.navBarIssueInfo.Expanded = true;
            this.navBarIssueInfo.GroupClientHeight = 31;
            this.navBarIssueInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarIssueInfo.Name = "navBarIssueInfo";
            // 
            // navBarContact
            // 
            this.navBarContact.Caption = "Contact";
            this.navBarContact.ControlContainer = this.navBarControlContainerContact;
            this.navBarContact.Expanded = true;
            this.navBarContact.GroupClientHeight = 410;
            this.navBarContact.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarContact.Name = "navBarContact";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "Other Info";
            this.navBarGroup1.ControlContainer = this.navBarGroupControlContainer6;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 139;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarItem1
            // 
            this.navBarItem1.Caption = "navBarItem1";
            this.navBarItem1.Name = "navBarItem1";
            // 
            // navBarItem2
            // 
            this.navBarItem2.Caption = "navBarItem2";
            this.navBarItem2.Name = "navBarItem2";
            // 
            // MBLEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "MBLEditPart";
            this.Size = new System.Drawing.Size(982, 579);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuantityUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtChecker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtIssuePlace.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIssue.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIssue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotifyPartyDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgentDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipperDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtNotifyParty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsWoodPacking.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoodsDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFreightDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtnQtyInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtRefNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPODName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfDeliveryName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOLName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfReceipt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfReceiptName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinalDestinationName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtFinalDestination.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOLCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPODCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPETD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPETD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlacePay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightSite.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeightSite.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateVerifiedDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateVerifiedDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateWeightDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateWeightDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotify2.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCtnQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWoodPacking.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).EndInit();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAMS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasContract.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelexNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRleaseBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHBLNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIssueType.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkHasFee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteGateInDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteGateInDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNBPODCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkThirdPlacePay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETA.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtETD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowVoyage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowPreVoyage.Properties)).EndInit();
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSpecial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHSCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsManualControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtnInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNSITBLNotes.Properties)).EndInit();
            this.navBarGroupControlContainer4.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.navBarGroupControlContainer5.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVerifiedPerson.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResponsiblePerson.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResponsibleParty.Properties)).EndInit();
            this.navBarGroupControlContainer6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.LabelControl labMBLNo;
        private DevExpress.XtraEditors.DateEdit dteBookingDate;
        private DevExpress.XtraEditors.LabelControl labRefNo;
        private DevExpress.XtraEditors.LabelControl labIssueBy;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbReleaseType;
        private DevExpress.XtraEditors.LabelControl labReleaseType;
        private DevExpress.XtraEditors.LabelControl labIssueDate;
        private DevExpress.XtraEditors.LabelControl labReleaseDate;
        private DevExpress.XtraEditors.LabelControl labIssuePlace;
        private DevExpress.XtraEditors.DateEdit dteIssue;
        private DevExpress.XtraEditors.ButtonEdit stxtIssuePlace;
        private DevExpress.XtraEditors.MemoEdit txtNotifyPartyDescription;
        private DevExpress.XtraEditors.MemoEdit txtConsigneeDescription;
        private DevExpress.XtraEditors.MemoEdit txtShipperDescription;
        private ICP.Common.UI.Controls.CustomerForNewPopupContainerEdit stxtNotifyParty;
        private DevExpress.XtraEditors.LabelControl labNotifyParty;
        private ICP.Common.UI.Controls.CustomerForNewPopupContainerEdit stxtConsignee;
        private DevExpress.XtraEditors.LabelControl labConsignee;
        private ICP.Common.UI.Controls.CustomerForNewPopupContainerEdit stxtShipper;
        private DevExpress.XtraEditors.LabelControl labShipper;
        private ICP.Business.Common.UI.BusinessContactPopupContainerEdit stxtChecker;
        private DevExpress.XtraEditors.LabelControl labChecker;
        private DevExpress.XtraEditors.MemoEdit txtAgentDescription;
        private DevExpress.XtraEditors.LabelControl labAgent;
        private DevExpress.XtraEditors.LabelControl labContainerInfo;
        private DevExpress.XtraEditors.LabelControl labMarks;
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
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barSaveAs;
        private DevExpress.XtraBars.BarButtonItem barPrintBL;
        private DevExpress.XtraBars.BarButtonItem barReplyAgent;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barCheck;
        private DevExpress.XtraBars.BarButtonItem barCheckDone;
        private DevExpress.XtraBars.BarButtonItem barE_MBL;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraEditors.ButtonEdit stxtRefNo;
        private DevExpress.XtraEditors.PanelControl panelMain;
        private DevExpress.XtraEditors.MemoEdit txtCtnQty;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private DevExpress.XtraNavBar.NavBarGroup navBarBLInfo;
        private DevExpress.XtraNavBar.NavBarGroup navBarCargo;
        private DevExpress.XtraEditors.CheckEdit chkShowVoyage;
        private DevExpress.XtraEditors.CheckEdit chkShowPreVoyage;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbPaymentTerm;
        private DevExpress.XtraEditors.LabelControl labPlaceOfDelivery;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private DevExpress.XtraEditors.LabelControl labPreVoyage;
        private DevExpress.XtraEditors.ButtonEdit stxtPlaceOfDelivery;
        private DevExpress.XtraEditors.LabelControl labPOL2;
        private DevExpress.XtraEditors.LabelControl labPaymentTerm;
        private DevExpress.XtraEditors.TextEdit txtPODName;
        private DevExpress.XtraEditors.TextEdit txtPlaceOfDeliveryName;
        private DevExpress.XtraEditors.LabelControl labPlaceOfReceipt;
        private DevExpress.XtraEditors.LabelControl labVoyage;
        private DevExpress.XtraEditors.TextEdit txtPOLName;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTransportClause;
        private DevExpress.XtraEditors.LabelControl labTransportClause;
        private ICP.FCM.Common.UI.UCButtonEdit stxtPlaceOfReceipt;
        private DevExpress.XtraEditors.TextEdit txtPlaceOfReceiptName;
        private DevExpress.XtraEditors.TextEdit txtFinalDestinationName;
        private DevExpress.XtraEditors.LabelControl labFinalDestination;
        private DevExpress.XtraEditors.ButtonEdit stxtFinalDestination;
        private ICP.Framework.ClientComponents.Controls.ComboCustomerDescriptionControl stxtAgent;
        private ICP.FCM.Common.UI.UCButtonEdit txtPOLCode;
        private ICP.FCM.Common.UI.UCButtonEdit txtPODCode;
        private DevExpress.XtraEditors.MemoEdit txtWoodPacking;
        private DevExpress.XtraNavBar.NavBarGroup navBarIssueInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer4;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbIssueType;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.LabelControl labHBLNO;
        private DevExpress.XtraBars.BarSubItem barSubCheck;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbIssueBy;
        private DevExpress.XtraEditors.TextEdit txtHBLNO;
        private DevExpress.XtraEditors.MemoEdit txtCtnInfo;
        private DevExpress.XtraEditors.SpinEdit numMeasurement;
        private DevExpress.XtraEditors.SpinEdit numWeight;
        private DevExpress.XtraEditors.SpinEdit numQuantity;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraEditors.LabelControl labAMSInfo;
        private DevExpress.XtraBars.BarSubItem barSubPrint;
        private DevExpress.XtraBars.BarButtonItem barPrintLoadGoods;
        private DevExpress.XtraEditors.ButtonEdit txtContractNo;
        private DevExpress.XtraBars.BarButtonItem barBill;
        private DevExpress.XtraBars.BarButtonItem barEDIStyle;
        private DevExpress.XtraEditors.MemoEdit txtMarks;
        private DevExpress.XtraEditors.DateEdit dtPETD;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dtETD;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dtETA;
        private ICP.Common.UI.UCVoyageLookupEdit stxtPreVoyage;
        private ICP.Common.UI.UCVoyageLookupEdit stxtVoyage;
        private DevExpress.XtraEditors.CheckEdit chkHasContract;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarControlContainerContact;
        private DevExpress.XtraNavBar.NavBarGroup navBarContact;
        private DevExpress.XtraBars.BarSubItem barbl;
        private DevExpress.XtraBars.BarButtonItem barchs;
        private DevExpress.XtraBars.BarButtonItem bareng;
        private DevExpress.XtraEditors.TextEdit txtRleaseBy;
        private DevExpress.XtraEditors.LabelControl labReleaseBy;
        private DevExpress.XtraEditors.TextEdit txtTelexNo;
        private DevExpress.XtraEditors.LabelControl labTelexNo;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbBookingParty;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.CheckEdit chkAMS;
        private ICP.FCM.Common.UI.UCButtonEdit stxtPlacePay;
        private DevExpress.XtraEditors.CheckEdit chkThirdPlacePay;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer5;
        private DevExpress.XtraNavBar.NavBarGroup navAMS;
        private DevExpress.XtraNavBar.NavBarItem navBarItem1;
        private DevExpress.XtraEditors.CheckEdit IsManualControl;
        private DevExpress.XtraEditors.LabelControl lblNBPODCode;
        private DevExpress.XtraEditors.TextEdit txtNBPODCode;
        private DevExpress.XtraEditors.DateEdit dteGateInDate;
        private DevExpress.XtraEditors.LabelControl labGateInDate;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private DevExpress.XtraBars.Bar barSavingTools;
        private DevExpress.XtraBars.BarButtonItem barCancel;
        private DevExpress.XtraBars.BarStaticItem barlabMessage;
        private DevExpress.XtraBars.BarButtonItem barSavingClose;
        private DevExpress.XtraBars.BarStaticItem barlabSeconds;
        private Framework.ClientComponents.Controls.ContainerDemandControl container;
        private DevExpress.XtraEditors.TextEdit txtHSCode;
        private DevExpress.XtraEditors.LabelControl labHSCode;
        private DevExpress.XtraBars.BarSubItem barEDIANL;
        private DevExpress.XtraBars.BarButtonItem barBooking;
        private DevExpress.XtraBars.BarButtonItem barPreplan;
        private DevExpress.XtraBars.BarButtonItem barSupplement;
        private DevExpress.XtraBars.BarButtonItem barWharf;
        private DevExpress.XtraEditors.CheckEdit chkSpecial;
        private DevExpress.XtraBars.BarButtonItem barAddHbl;
        private DevExpress.XtraBars.BarButtonItem barGetCommodity;
        private DevExpress.XtraBars.BarButtonItem barVGM;
        private DevExpress.XtraEditors.LabelControl labContractName;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraEditors.LabelControl labWeightSite;
        private FCM.Common.UI.UCButtonEdit cmbWeightSite;
        private DevExpress.XtraEditors.TextEdit txtWeightSite;
        private DevExpress.XtraEditors.TextEdit txtResponsiblePerson;
        private DevExpress.XtraEditors.LabelControl labResponsiblePerson;
        private DevExpress.XtraEditors.TextEdit txtResponsibleParty;
        private DevExpress.XtraEditors.LabelControl labResponsibleParty;
        private DevExpress.XtraEditors.DateEdit dateVerifiedDate;
        private DevExpress.XtraEditors.LabelControl labVerifiedDate;
        private DevExpress.XtraEditors.TextEdit txtVerifiedPerson;
        private DevExpress.XtraEditors.LabelControl labVerifiedPerson;
        private DevExpress.XtraBars.BarButtonItem barSaveVGM;
        private DevExpress.XtraEditors.DateEdit dateWeightDate;
        private DevExpress.XtraEditors.LabelControl labWeightingDate;
        private DevExpress.XtraBars.BarButtonItem barPrintLoadContainer;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer6;
        private Panel panel7;
        private DevExpress.XtraEditors.MemoEdit txtNotify2;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarItem navBarItem2;
        private DevExpress.XtraEditors.CheckEdit checkHasFee;
        private DevExpress.XtraBars.BarButtonItem barPrintLoadContainerCopy;
        private DevExpress.XtraBars.BarButtonItem btnDeclaration;
        private DevExpress.XtraBars.BarButtonItem bntImportDeclaration;
        private DevExpress.XtraBars.BarSubItem barDeclaration;
        private DevExpress.XtraEditors.LabelControl labNSITBLNotes;
        private DevExpress.XtraEditors.MemoEdit txtNSITBLNotes;
    }
}
