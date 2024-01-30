using System.Windows.Forms;
namespace ICP.FCM.AirExport.UI.MBL
{
    partial class MAWBEditPart
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
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            this.bindingSource1 = new System.Windows.Forms.BindingSource();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.stxtChecker = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtIssuePlace = new DevExpress.XtraEditors.ButtonEdit();
            this.dteIssue = new DevExpress.XtraEditors.DateEdit();
            this.txtConsigneeDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtShipperDescription = new DevExpress.XtraEditors.MemoEdit();
            this.stxtConsignee = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtShipper = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.txtFreightDescription = new DevExpress.XtraEditors.MemoEdit();
            this.stxtRefNo = new DevExpress.XtraEditors.ButtonEdit();
            this.txtDetinationName = new DevExpress.XtraEditors.TextEdit();
            this.txtDepartureName = new DevExpress.XtraEditors.TextEdit();
            this.txtDepartureCode = new DevExpress.XtraEditors.ButtonEdit();
            this.txtDetinationCode = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtAgentOfCarrier = new DevExpress.XtraEditors.ButtonEdit();
            this.dteBookingDate = new DevExpress.XtraEditors.DateEdit();
            this.cmbReleaseType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtNotifyParty = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.cmbMeasurementUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.spinEdit7 = new DevExpress.XtraEditors.SpinEdit();
            this.cmbPaymentTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbQuantityUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbTarifflevel = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtPlaceOfDelivery = new DevExpress.XtraEditors.ButtonEdit();
            this.txtPlaceOfDeliveryName = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.barSubPrint = new DevExpress.XtraBars.BarSubItem();
            this.barPrintBL = new DevExpress.XtraBars.BarButtonItem();
            this.barReplyAgent = new DevExpress.XtraBars.BarButtonItem();
            this.barSubCheck = new DevExpress.XtraBars.BarButtonItem();
            this.barE_MBL = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barBill = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barCheck = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckDone = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintLoadGoods = new DevExpress.XtraBars.BarButtonItem();
            this.labIssueBy = new DevExpress.XtraEditors.LabelControl();
            this.labIssueDate = new DevExpress.XtraEditors.LabelControl();
            this.labChecker = new DevExpress.XtraEditors.LabelControl();
            this.labRefNo = new DevExpress.XtraEditors.LabelControl();
            this.labIssuePlace = new DevExpress.XtraEditors.LabelControl();
            this.labMAWBNO = new DevExpress.XtraEditors.LabelControl();
            this.labConsignee = new DevExpress.XtraEditors.LabelControl();
            this.labAgent = new DevExpress.XtraEditors.LabelControl();
            this.labShipper = new DevExpress.XtraEditors.LabelControl();
            this.labFreightDescription = new DevExpress.XtraEditors.LabelControl();
            this.panelScroll = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarBaseInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labReleaseType = new DevExpress.XtraEditors.LabelControl();
            this.labReleaseDate = new DevExpress.XtraEditors.LabelControl();
            this.txtHBLNO = new DevExpress.XtraEditors.TextEdit();
            this.labHBLNO = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labPlaceOfDelivery = new DevExpress.XtraEditors.LabelControl();
            this.mcmbAirCompany = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labAirCompany = new DevExpress.XtraEditors.LabelControl();
            this.labFlightNo = new DevExpress.XtraEditors.LabelControl();
            this.cmbFlightNo = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labNotifyParty = new DevExpress.XtraEditors.LabelControl();
            this.txtIATACode = new DevExpress.XtraEditors.TextEdit();
            this.labIATACode = new DevExpress.XtraEditors.LabelControl();
            this.txtAgentAccountNo = new DevExpress.XtraEditors.TextEdit();
            this.labAgentAccountNo = new DevExpress.XtraEditors.LabelControl();
            this.txtConsigneeAccountNo = new DevExpress.XtraEditors.TextEdit();
            this.labConsigneeAccountNo = new DevExpress.XtraEditors.LabelControl();
            this.txtShipperAccountNo = new DevExpress.XtraEditors.TextEdit();
            this.labShipperAccount = new DevExpress.XtraEditors.LabelControl();
            this.txtBy3 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtportto3 = new DevExpress.XtraEditors.TextEdit();
            this.labToBy3 = new DevExpress.XtraEditors.LabelControl();
            this.txtBy2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtportto2 = new DevExpress.XtraEditors.TextEdit();
            this.labToBy2 = new DevExpress.XtraEditors.LabelControl();
            this.txtBy1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtportto1 = new DevExpress.XtraEditors.TextEdit();
            this.labToBy1 = new DevExpress.XtraEditors.LabelControl();
            this.labAgentOfCarrier = new DevExpress.XtraEditors.LabelControl();
            this.stxtAgent = new ICP.Framework.ClientComponents.Controls.ComboCustomerDescriptionControl();
            this.labDetination = new DevExpress.XtraEditors.LabelControl();
            this.dteETA = new DevExpress.XtraEditors.DateEdit();
            this.labETA = new DevExpress.XtraEditors.LabelControl();
            this.labDeparture = new DevExpress.XtraEditors.LabelControl();
            this.dteETD = new DevExpress.XtraEditors.DateEdit();
            this.labETD = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtCarrierChargers = new DevExpress.XtraEditors.SpinEdit();
            this.txtTax = new DevExpress.XtraEditors.SpinEdit();
            this.txtValuationCharge = new DevExpress.XtraEditors.SpinEdit();
            this.txtAgentChargers = new DevExpress.XtraEditors.SpinEdit();
            this.txtChargesDestination = new DevExpress.XtraEditors.TextEdit();
            this.labChargesDestination = new DevExpress.XtraEditors.LabelControl();
            this.txtCCCharges = new DevExpress.XtraEditors.TextEdit();
            this.labCCCharges = new DevExpress.XtraEditors.LabelControl();
            this.txtCurrencyConversionRate = new DevExpress.XtraEditors.TextEdit();
            this.labConversionRates = new DevExpress.XtraEditors.LabelControl();
            this.labTarifflevel = new DevExpress.XtraEditors.LabelControl();
            this.labTax = new DevExpress.XtraEditors.LabelControl();
            this.labValuationCharge = new DevExpress.XtraEditors.LabelControl();
            this.labCarrierChargers = new DevExpress.XtraEditors.LabelControl();
            this.labAgentChargers = new DevExpress.XtraEditors.LabelControl();
            this.labMeasurement = new DevExpress.XtraEditors.LabelControl();
            this.txtInsurance = new DevExpress.XtraEditors.TextEdit();
            this.txtCustoms = new DevExpress.XtraEditors.TextEdit();
            this.labInsurance = new DevExpress.XtraEditors.LabelControl();
            this.cmbOther = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labDCLCustoms = new DevExpress.XtraEditors.LabelControl();
            this.labOther = new DevExpress.XtraEditors.LabelControl();
            this.labPaymentTerm = new DevExpress.XtraEditors.LabelControl();
            this.txtCarriage = new DevExpress.XtraEditors.TextEdit();
            this.labDCLCarriage = new DevExpress.XtraEditors.LabelControl();
            this.txtGLBS = new DevExpress.XtraEditors.SpinEdit();
            this.txtChLBS = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtChKGS = new DevExpress.XtraEditors.SpinEdit();
            this.txtGKGS = new DevExpress.XtraEditors.SpinEdit();
            this.numQuantity = new DevExpress.XtraEditors.SpinEdit();
            this.labQuantity = new DevExpress.XtraEditors.LabelControl();
            this.labWeight = new DevExpress.XtraEditors.LabelControl();
            this.labChargeKGSLBS = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.OtherChargesBindingSource = new System.Windows.Forms.BindingSource();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.txtOtherChargers = new DevExpress.XtraEditors.MemoEdit();
            this.radioGroup2 = new DevExpress.XtraEditors.RadioGroup();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.txtIATAAmount = new DevExpress.XtraEditors.SpinEdit();
            this.txtIATAPrice = new DevExpress.XtraEditors.SpinEdit();
            this.txtBuyAmount = new DevExpress.XtraEditors.SpinEdit();
            this.cmbIATACur = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labIATARate = new DevExpress.XtraEditors.LabelControl();
            this.txtBuyPrice = new DevExpress.XtraEditors.SpinEdit();
            this.labBuyRate = new DevExpress.XtraEditors.LabelControl();
            this.txtHandingInfo = new DevExpress.XtraEditors.MemoEdit();
            this.cmbBuyCur = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labHandlingInfomation = new DevExpress.XtraEditors.LabelControl();
            this.txtMarks = new DevExpress.XtraEditors.MemoEdit();
            this.labMarks = new DevExpress.XtraEditors.LabelControl();
            this.txtGoodsDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labGoodsDescription = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer4 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.mcmbIssueBy = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.navBarBLInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarCargo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarIssueInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.barManager2 = new DevExpress.XtraBars.BarManager();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelect = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtChecker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtIssuePlace.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIssue.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIssue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipperDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFreightDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtRefNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDetinationName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartureName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartureCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDetinationCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgentOfCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtNotifyParty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit7.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuantityUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTarifflevel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfDeliveryName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelScroll)).BeginInit();
            this.panelScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHBLNO.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIATACode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgentAccountNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeAccountNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipperAccountNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBy3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtportto3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBy2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtportto2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBy1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtportto1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties)).BeginInit();
            this.navBarGroupControlContainer3.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrierChargers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValuationCharge.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgentChargers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChargesDestination.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCCCharges.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyConversionRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInsurance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustoms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOther.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarriage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGLBS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChLBS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChKGS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGKGS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OtherChargesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherChargers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIATAAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIATAPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuyAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIATACur.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuyPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHandingInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBuyCur.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoodsDescription.Properties)).BeginInit();
            this.navBarGroupControlContainer4.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            this.SuspendLayout();
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.AirExport.ServiceInterface.DataObjects.AirMBLInfo);
            // 
            // txtNo
            // 
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "No", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtNo.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtNo.Location = new System.Drawing.Point(279, 4);
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNo.Size = new System.Drawing.Size(113, 21);
            this.txtNo.TabIndex = 1;
            // 
            // stxtChecker
            // 
            this.stxtChecker.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CheckerName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtChecker.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "CheckerID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtChecker, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtChecker.Location = new System.Drawing.Point(502, 3);
            this.stxtChecker.Name = "stxtChecker";
            this.stxtChecker.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtChecker.Properties.Appearance.Options.UseBackColor = true;
            this.stxtChecker.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtChecker.Size = new System.Drawing.Size(295, 21);
            this.stxtChecker.TabIndex = 3;
            // 
            // stxtIssuePlace
            // 
            this.stxtIssuePlace.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "IssuePlaceName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtIssuePlace.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "IssuePlaceID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtIssuePlace, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtIssuePlace.Location = new System.Drawing.Point(282, 1);
            this.stxtIssuePlace.Name = "stxtIssuePlace";
            this.stxtIssuePlace.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtIssuePlace.Properties.Appearance.Options.UseBackColor = true;
            this.stxtIssuePlace.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtIssuePlace.Size = new System.Drawing.Size(110, 21);
            this.stxtIssuePlace.TabIndex = 1;
            // 
            // dteIssue
            // 
            this.dteIssue.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IssueDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteIssue.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteIssue, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteIssue.Location = new System.Drawing.Point(502, 1);
            this.dteIssue.Name = "dteIssue";
            this.dteIssue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteIssue.Properties.Mask.EditMask = "";
            this.dteIssue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteIssue.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteIssue.Size = new System.Drawing.Size(110, 21);
            this.dteIssue.TabIndex = 2;
            // 
            // txtConsigneeDescription
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtConsigneeDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtConsigneeDescription.Location = new System.Drawing.Point(65, 157);
            this.txtConsigneeDescription.Name = "txtConsigneeDescription";
            this.txtConsigneeDescription.Properties.ReadOnly = true;
            this.txtConsigneeDescription.Size = new System.Drawing.Size(327, 105);
            this.txtConsigneeDescription.TabIndex = 3;
            // 
            // txtShipperDescription
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtShipperDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtShipperDescription.Location = new System.Drawing.Point(65, 26);
            this.txtShipperDescription.Name = "txtShipperDescription";
            this.txtShipperDescription.Properties.ReadOnly = true;
            this.txtShipperDescription.Size = new System.Drawing.Size(327, 105);
            this.txtShipperDescription.TabIndex = 1;
            // 
            // stxtConsignee
            // 
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ConsigneeName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "ConsigneeID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtConsignee, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtConsignee.Location = new System.Drawing.Point(65, 134);
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
            this.stxtConsignee.Size = new System.Drawing.Size(203, 21);
            this.stxtConsignee.TabIndex = 2;
            // 
            // stxtShipper
            // 
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ShipperName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "ShipperID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtShipper, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtShipper.Location = new System.Drawing.Point(65, 3);
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
            this.stxtShipper.Size = new System.Drawing.Size(203, 21);
            this.stxtShipper.TabIndex = 0;
            // 
            // txtFreightDescription
            // 
            this.txtFreightDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "FreightDescription", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtFreightDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtFreightDescription.Location = new System.Drawing.Point(624, 243);
            this.txtFreightDescription.Name = "txtFreightDescription";
            this.txtFreightDescription.Size = new System.Drawing.Size(173, 82);
            this.txtFreightDescription.TabIndex = 11;
            // 
            // stxtRefNo
            // 
            this.stxtRefNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "RefNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtRefNo.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtRefNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtRefNo.Location = new System.Drawing.Point(65, 3);
            this.stxtRefNo.Name = "stxtRefNo";
            this.stxtRefNo.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtRefNo.Properties.Appearance.Options.UseBackColor = true;
            this.stxtRefNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtRefNo.Size = new System.Drawing.Size(148, 21);
            this.stxtRefNo.TabIndex = 0;
            // 
            // txtDetinationName
            // 
            this.txtDetinationName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "DetinationName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDetinationName.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtDetinationName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtDetinationName.Location = new System.Drawing.Point(633, 98);
            this.txtDetinationName.Name = "txtDetinationName";
            this.txtDetinationName.Size = new System.Drawing.Size(164, 21);
            this.txtDetinationName.TabIndex = 22;
            // 
            // txtDepartureName
            // 
            this.txtDepartureName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "DepartureName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDepartureName.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtDepartureName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtDepartureName.Location = new System.Drawing.Point(633, 74);
            this.txtDepartureName.Name = "txtDepartureName";
            this.txtDepartureName.Size = new System.Drawing.Size(164, 21);
            this.txtDepartureName.TabIndex = 19;
            // 
            // txtDepartureCode
            // 
            this.txtDepartureCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "DepartureCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDepartureCode.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "DepartureID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDepartureCode.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtDepartureCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtDepartureCode.Location = new System.Drawing.Point(502, 74);
            this.txtDepartureCode.Name = "txtDepartureCode";
            this.txtDepartureCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtDepartureCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtDepartureCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDepartureCode.Size = new System.Drawing.Size(125, 21);
            this.txtDepartureCode.TabIndex = 18;
            // 
            // txtDetinationCode
            // 
            this.txtDetinationCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "DetinationCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDetinationCode.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "DetinationID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtDetinationCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtDetinationCode.Location = new System.Drawing.Point(502, 98);
            this.txtDetinationCode.Name = "txtDetinationCode";
            this.txtDetinationCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtDetinationCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtDetinationCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDetinationCode.Size = new System.Drawing.Size(125, 21);
            this.txtDetinationCode.TabIndex = 21;
            // 
            // stxtAgentOfCarrier
            // 
            this.stxtAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "AgentOfCarrierName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "AgentOfCarrierID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtAgentOfCarrier, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtAgentOfCarrier.Location = new System.Drawing.Point(502, 50);
            this.stxtAgentOfCarrier.Name = "stxtAgentOfCarrier";
            this.stxtAgentOfCarrier.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtAgentOfCarrier.Properties.Appearance.Options.UseBackColor = true;
            this.stxtAgentOfCarrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtAgentOfCarrier.Size = new System.Drawing.Size(295, 21);
            this.stxtAgentOfCarrier.TabIndex = 549;
            // 
            // dteBookingDate
            // 
            this.dteBookingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ReleaseDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteBookingDate.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteBookingDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteBookingDate.Location = new System.Drawing.Point(687, 27);
            this.dteBookingDate.Name = "dteBookingDate";
            this.dteBookingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteBookingDate.Properties.Mask.EditMask = "";
            this.dteBookingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteBookingDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteBookingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteBookingDate.Size = new System.Drawing.Size(110, 21);
            this.dteBookingDate.TabIndex = 9;
            this.dteBookingDate.TabStop = false;
            // 
            // cmbReleaseType
            // 
            this.cmbReleaseType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ReleaseType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbReleaseType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbReleaseType.Location = new System.Drawing.Point(502, 27);
            this.cmbReleaseType.Name = "cmbReleaseType";
            this.cmbReleaseType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbReleaseType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbReleaseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReleaseType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbReleaseType.Size = new System.Drawing.Size(117, 21);
            this.cmbReleaseType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbReleaseType.TabIndex = 8;
            // 
            // stxtNotifyParty
            // 
            this.stxtNotifyParty.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "NotifyPartyName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtNotifyParty.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "NotifyPartyID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtNotifyParty, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtNotifyParty.Location = new System.Drawing.Point(65, 266);
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
            this.stxtNotifyParty.TabIndex = 671;
            // 
            // cmbMeasurementUnit
            // 
            this.cmbMeasurementUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "MeasurementUnitID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbMeasurementUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbMeasurementUnit.Location = new System.Drawing.Point(342, 53);
            this.cmbMeasurementUnit.Name = "cmbMeasurementUnit";
            this.cmbMeasurementUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbMeasurementUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMeasurementUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMeasurementUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbMeasurementUnit.Size = new System.Drawing.Size(59, 21);
            this.cmbMeasurementUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbMeasurementUnit.TabIndex = 689;
            // 
            // spinEdit7
            // 
            this.spinEdit7.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Measurement", true));
            this.spinEdit7.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dxErrorProvider1.SetIconAlignment(this.spinEdit7, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.spinEdit7.Location = new System.Drawing.Point(277, 53);
            this.spinEdit7.Name = "spinEdit7";
            this.spinEdit7.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit7.Properties.DisplayFormat.FormatString = "F3";
            this.spinEdit7.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit7.Properties.EditFormat.FormatString = "F3";
            this.spinEdit7.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit7.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinEdit7.Properties.Mask.EditMask = "F3";
            this.spinEdit7.Size = new System.Drawing.Size(59, 21);
            this.spinEdit7.TabIndex = 688;
            // 
            // cmbPaymentTerm
            // 
            this.cmbPaymentTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "PaymentTermID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbPaymentTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbPaymentTerm.Location = new System.Drawing.Point(65, 29);
            this.cmbPaymentTerm.Name = "cmbPaymentTerm";
            this.cmbPaymentTerm.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbPaymentTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbPaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaymentTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbPaymentTerm.Size = new System.Drawing.Size(48, 21);
            this.cmbPaymentTerm.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbPaymentTerm.TabIndex = 674;
            // 
            // cmbQuantityUnit
            // 
            this.cmbQuantityUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "QuantityUnitID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbQuantityUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbQuantityUnit.Location = new System.Drawing.Point(138, 53);
            this.cmbQuantityUnit.Name = "cmbQuantityUnit";
            this.cmbQuantityUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbQuantityUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbQuantityUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQuantityUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbQuantityUnit.Size = new System.Drawing.Size(59, 21);
            this.cmbQuantityUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbQuantityUnit.TabIndex = 670;
            // 
            // cmbTarifflevel
            // 
            this.cmbTarifflevel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "RateClass", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbTarifflevel, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbTarifflevel.Location = new System.Drawing.Point(353, 200);
            this.cmbTarifflevel.Name = "cmbTarifflevel";
            this.cmbTarifflevel.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbTarifflevel.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTarifflevel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTarifflevel.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbTarifflevel.Size = new System.Drawing.Size(48, 21);
            this.cmbTarifflevel.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbTarifflevel.TabIndex = 699;
            // 
            // stxtPlaceOfDelivery
            // 
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "PlaceOfDeliveryID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PlaceOfDeliveryCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtPlaceOfDelivery, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPlaceOfDelivery.Location = new System.Drawing.Point(502, 122);
            this.stxtPlaceOfDelivery.Name = "stxtPlaceOfDelivery";
            this.stxtPlaceOfDelivery.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtPlaceOfDelivery.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlaceOfDelivery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlaceOfDelivery.Size = new System.Drawing.Size(125, 21);
            this.stxtPlaceOfDelivery.TabIndex = 681;
            // 
            // txtPlaceOfDeliveryName
            // 
            this.txtPlaceOfDeliveryName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PlaceOfDeliveryName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPlaceOfDeliveryName.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtPlaceOfDeliveryName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPlaceOfDeliveryName.Location = new System.Drawing.Point(633, 122);
            this.txtPlaceOfDeliveryName.Name = "txtPlaceOfDeliveryName";
            this.txtPlaceOfDeliveryName.Size = new System.Drawing.Size(164, 21);
            this.txtPlaceOfDeliveryName.TabIndex = 682;
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
            this.barBill});
            this.barManager1.MaxItemId = 22;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSaveAs, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barReplyAgent, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubCheck, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barE_MBL, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBill, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Save_Blue_16;
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
            // barSubPrint
            // 
            this.barSubPrint.Caption = "Print";
            this.barSubPrint.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Print_16;
            this.barSubPrint.Id = 19;
            this.barSubPrint.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrintBL, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barSubPrint.Name = "barSubPrint";
            // 
            // barPrintBL
            // 
            this.barPrintBL.Caption = "Print BL";
            this.barPrintBL.Id = 4;
            this.barPrintBL.Name = "barPrintBL";
            this.barPrintBL.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
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
            this.barSubCheck.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Check_16;
            this.barSubCheck.Id = 12;
            this.barSubCheck.Name = "barSubCheck";
            this.barSubCheck.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSubCheck_ItemClick);
            // 
            // barE_MBL
            // 
            this.barE_MBL.Caption = "&E-MBL";
            this.barE_MBL.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Center_16;
            this.barE_MBL.Id = 9;
            this.barE_MBL.Name = "barE_MBL";
            this.barE_MBL.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barE_MBL_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Refresh_161;
            this.barRefresh.Hint = "&Refresh";
            this.barRefresh.Id = 17;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barBill
            // 
            this.barBill.Caption = "帐单(&B)";
            this.barBill.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Transfer_16;
            this.barBill.Id = 21;
            this.barBill.Name = "barBill";
            this.barBill.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBill_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FCM.AirExport.UI.Properties.Resources.Left_D_16;
            this.barClose.Id = 10;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(828, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 497);
            this.barDockControlBottom.Size = new System.Drawing.Size(828, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 471);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(828, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 471);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "&Output";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barCheck
            // 
            this.barCheck.Caption = "Chec&k";
            this.barCheck.Id = 6;
            this.barCheck.Name = "barCheck";
            // 
            // barCheckDone
            // 
            this.barCheckDone.Caption = "Check&Done";
            this.barCheckDone.Id = 7;
            this.barCheckDone.Name = "barCheckDone";
            // 
            // barPrintLoadGoods
            // 
            this.barPrintLoadGoods.Caption = "Print Load Goods";
            this.barPrintLoadGoods.Id = 20;
            this.barPrintLoadGoods.Name = "barPrintLoadGoods";
            // 
            // labIssueBy
            // 
            this.labIssueBy.Location = new System.Drawing.Point(5, 4);
            this.labIssueBy.Name = "labIssueBy";
            this.labIssueBy.Size = new System.Drawing.Size(41, 14);
            this.labIssueBy.TabIndex = 6;
            this.labIssueBy.Text = "IssueBy";
            // 
            // labIssueDate
            // 
            this.labIssueDate.Location = new System.Drawing.Point(413, 4);
            this.labIssueDate.Name = "labIssueDate";
            this.labIssueDate.Size = new System.Drawing.Size(58, 14);
            this.labIssueDate.TabIndex = 0;
            this.labIssueDate.Text = "Issue Date";
            // 
            // labChecker
            // 
            this.labChecker.Location = new System.Drawing.Point(413, 6);
            this.labChecker.Name = "labChecker";
            this.labChecker.Size = new System.Drawing.Size(44, 14);
            this.labChecker.TabIndex = 0;
            this.labChecker.Text = "Checker";
            // 
            // labRefNo
            // 
            this.labRefNo.Location = new System.Drawing.Point(5, 6);
            this.labRefNo.Name = "labRefNo";
            this.labRefNo.Size = new System.Drawing.Size(33, 14);
            this.labRefNo.TabIndex = 0;
            this.labRefNo.Text = "RefNo";
            // 
            // labIssuePlace
            // 
            this.labIssuePlace.Location = new System.Drawing.Point(217, 4);
            this.labIssuePlace.Name = "labIssuePlace";
            this.labIssuePlace.Size = new System.Drawing.Size(56, 14);
            this.labIssuePlace.TabIndex = 0;
            this.labIssuePlace.Text = "IssuePlace";
            // 
            // labMAWBNO
            // 
            this.labMAWBNO.Location = new System.Drawing.Point(216, 6);
            this.labMAWBNO.Name = "labMAWBNO";
            this.labMAWBNO.Size = new System.Drawing.Size(57, 14);
            this.labMAWBNO.TabIndex = 0;
            this.labMAWBNO.Text = "MAWB NO";
            // 
            // labConsignee
            // 
            this.labConsignee.Location = new System.Drawing.Point(5, 137);
            this.labConsignee.Name = "labConsignee";
            this.labConsignee.Size = new System.Drawing.Size(56, 14);
            this.labConsignee.TabIndex = 0;
            this.labConsignee.Text = "Consignee";
            // 
            // labAgent
            // 
            this.labAgent.Location = new System.Drawing.Point(413, 29);
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
            // labFreightDescription
            // 
            this.labFreightDescription.Location = new System.Drawing.Point(624, 226);
            this.labFreightDescription.Name = "labFreightDescription";
            this.labFreightDescription.Size = new System.Drawing.Size(98, 14);
            this.labFreightDescription.TabIndex = 0;
            this.labFreightDescription.Text = "FreightDescription";
            // 
            // panelScroll
            // 
            this.panelScroll.Controls.Add(this.navBarControl1);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.Location = new System.Drawing.Point(0, 26);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(828, 471);
            this.panelScroll.TabIndex = 0;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarBaseInfo;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer4);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarBaseInfo,
            this.navBarBLInfo,
            this.navBarCargo,
            this.navBarIssueInfo});
            this.navBarControl1.Location = new System.Drawing.Point(2, 2);
            this.navBarControl1.MaximumSize = new System.Drawing.Size(825, 0);
            this.navBarControl1.MinimumSize = new System.Drawing.Size(825, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(825, 467);
            this.navBarControl1.SkinExplorerBarViewScrollStyle = DevExpress.XtraNavBar.SkinExplorerBarViewScrollStyle.ScrollBar;
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarBaseInfo
            // 
            this.navBarBaseInfo.Caption = "Base Info";
            this.navBarBaseInfo.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarBaseInfo.Expanded = true;
            this.navBarBaseInfo.GroupClientHeight = 52;
            this.navBarBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBaseInfo.Name = "navBarBaseInfo";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.panel1);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(804, 50);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel1.Controls.Add(this.dteBookingDate);
            this.panel1.Controls.Add(this.cmbReleaseType);
            this.panel1.Controls.Add(this.labReleaseType);
            this.panel1.Controls.Add(this.labReleaseDate);
            this.panel1.Controls.Add(this.txtHBLNO);
            this.panel1.Controls.Add(this.stxtRefNo);
            this.panel1.Controls.Add(this.labHBLNO);
            this.panel1.Controls.Add(this.labMAWBNO);
            this.panel1.Controls.Add(this.txtNo);
            this.panel1.Controls.Add(this.labRefNo);
            this.panel1.Controls.Add(this.stxtChecker);
            this.panel1.Controls.Add(this.labChecker);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 50);
            this.panel1.TabIndex = 0;
            // 
            // labReleaseType
            // 
            this.labReleaseType.Location = new System.Drawing.Point(413, 30);
            this.labReleaseType.Name = "labReleaseType";
            this.labReleaseType.Size = new System.Drawing.Size(69, 14);
            this.labReleaseType.TabIndex = 10;
            this.labReleaseType.Text = "ReleaseType";
            // 
            // labReleaseDate
            // 
            this.labReleaseDate.Location = new System.Drawing.Point(633, 30);
            this.labReleaseDate.Name = "labReleaseDate";
            this.labReleaseDate.Size = new System.Drawing.Size(71, 14);
            this.labReleaseDate.TabIndex = 7;
            this.labReleaseDate.Text = "Release Date";
            // 
            // txtHBLNO
            // 
            this.txtHBLNO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "HAWBNos", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtHBLNO.Location = new System.Drawing.Point(65, 27);
            this.txtHBLNO.MenuManager = this.barManager1;
            this.txtHBLNO.Name = "txtHBLNO";
            this.txtHBLNO.Properties.ReadOnly = true;
            this.txtHBLNO.Size = new System.Drawing.Size(327, 21);
            this.txtHBLNO.TabIndex = 6;
            this.txtHBLNO.TabStop = false;
            // 
            // labHBLNO
            // 
            this.labHBLNO.Location = new System.Drawing.Point(5, 30);
            this.labHBLNO.Name = "labHBLNO";
            this.labHBLNO.Size = new System.Drawing.Size(38, 14);
            this.labHBLNO.TabIndex = 0;
            this.labHBLNO.Text = "HBLNO";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.panel2);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(804, 290);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel2.Controls.Add(this.labPlaceOfDelivery);
            this.panel2.Controls.Add(this.stxtPlaceOfDelivery);
            this.panel2.Controls.Add(this.txtPlaceOfDeliveryName);
            this.panel2.Controls.Add(this.mcmbAirCompany);
            this.panel2.Controls.Add(this.labAirCompany);
            this.panel2.Controls.Add(this.labFlightNo);
            this.panel2.Controls.Add(this.cmbFlightNo);
            this.panel2.Controls.Add(this.labNotifyParty);
            this.panel2.Controls.Add(this.stxtNotifyParty);
            this.panel2.Controls.Add(this.txtIATACode);
            this.panel2.Controls.Add(this.labIATACode);
            this.panel2.Controls.Add(this.txtAgentAccountNo);
            this.panel2.Controls.Add(this.labAgentAccountNo);
            this.panel2.Controls.Add(this.txtConsigneeAccountNo);
            this.panel2.Controls.Add(this.labConsigneeAccountNo);
            this.panel2.Controls.Add(this.txtShipperAccountNo);
            this.panel2.Controls.Add(this.labShipperAccount);
            this.panel2.Controls.Add(this.txtBy3);
            this.panel2.Controls.Add(this.labelControl5);
            this.panel2.Controls.Add(this.txtportto3);
            this.panel2.Controls.Add(this.labToBy3);
            this.panel2.Controls.Add(this.txtBy2);
            this.panel2.Controls.Add(this.labelControl3);
            this.panel2.Controls.Add(this.txtportto2);
            this.panel2.Controls.Add(this.labToBy2);
            this.panel2.Controls.Add(this.txtBy1);
            this.panel2.Controls.Add(this.labelControl1);
            this.panel2.Controls.Add(this.txtportto1);
            this.panel2.Controls.Add(this.labToBy1);
            this.panel2.Controls.Add(this.labAgentOfCarrier);
            this.panel2.Controls.Add(this.stxtAgentOfCarrier);
            this.panel2.Controls.Add(this.stxtAgent);
            this.panel2.Controls.Add(this.labDetination);
            this.panel2.Controls.Add(this.dteETA);
            this.panel2.Controls.Add(this.labETA);
            this.panel2.Controls.Add(this.labDeparture);
            this.panel2.Controls.Add(this.txtDetinationName);
            this.panel2.Controls.Add(this.dteETD);
            this.panel2.Controls.Add(this.labETD);
            this.panel2.Controls.Add(this.txtDepartureName);
            this.panel2.Controls.Add(this.stxtShipper);
            this.panel2.Controls.Add(this.labShipper);
            this.panel2.Controls.Add(this.labAgent);
            this.panel2.Controls.Add(this.labConsignee);
            this.panel2.Controls.Add(this.stxtConsignee);
            this.panel2.Controls.Add(this.txtConsigneeDescription);
            this.panel2.Controls.Add(this.txtShipperDescription);
            this.panel2.Controls.Add(this.txtDepartureCode);
            this.panel2.Controls.Add(this.txtDetinationCode);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 290);
            this.panel2.TabIndex = 0;
            // 
            // labPlaceOfDelivery
            // 
            this.labPlaceOfDelivery.Location = new System.Drawing.Point(413, 125);
            this.labPlaceOfDelivery.Name = "labPlaceOfDelivery";
            this.labPlaceOfDelivery.Size = new System.Drawing.Size(83, 14);
            this.labPlaceOfDelivery.TabIndex = 683;
            this.labPlaceOfDelivery.Text = "PlaceOfDelivery";
            // 
            // mcmbAirCompany
            // 
            this.mcmbAirCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "AirCompanyID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mcmbAirCompany.EditText = "";
            this.mcmbAirCompany.EditValue = null;
            this.mcmbAirCompany.Location = new System.Drawing.Point(502, 170);
            this.mcmbAirCompany.Name = "mcmbAirCompany";
            this.mcmbAirCompany.ReadOnly = false;
            this.mcmbAirCompany.RefreshButtonToolTip = "";
            this.mcmbAirCompany.ShowRefreshButton = false;
            this.mcmbAirCompany.Size = new System.Drawing.Size(295, 21);
            this.mcmbAirCompany.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.mcmbAirCompany.TabIndex = 680;
            this.mcmbAirCompany.ToolTip = "";
            // 
            // labAirCompany
            // 
            this.labAirCompany.Location = new System.Drawing.Point(413, 173);
            this.labAirCompany.Name = "labAirCompany";
            this.labAirCompany.Size = new System.Drawing.Size(64, 14);
            this.labAirCompany.TabIndex = 679;
            this.labAirCompany.Text = "AirCompany";
            // 
            // labFlightNo
            // 
            this.labFlightNo.Location = new System.Drawing.Point(413, 149);
            this.labFlightNo.Name = "labFlightNo";
            this.labFlightNo.Size = new System.Drawing.Size(50, 14);
            this.labFlightNo.TabIndex = 673;
            this.labFlightNo.Text = "Flight NO";
            // 
            // cmbFlightNo
            // 
            this.cmbFlightNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "FilightNoID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbFlightNo.EditText = "";
            this.cmbFlightNo.EditValue = null;
            this.cmbFlightNo.Location = new System.Drawing.Point(502, 146);
            this.cmbFlightNo.Name = "cmbFlightNo";
            this.cmbFlightNo.ReadOnly = false;
            this.cmbFlightNo.RefreshButtonToolTip = "";
            this.cmbFlightNo.ShowRefreshButton = false;
            this.cmbFlightNo.Size = new System.Drawing.Size(295, 21);
            this.cmbFlightNo.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbFlightNo.TabIndex = 674;
            this.cmbFlightNo.ToolTip = "";
            // 
            // labNotifyParty
            // 
            this.labNotifyParty.Location = new System.Drawing.Point(5, 270);
            this.labNotifyParty.Name = "labNotifyParty";
            this.labNotifyParty.Size = new System.Drawing.Size(60, 14);
            this.labNotifyParty.TabIndex = 670;
            this.labNotifyParty.Text = "NotifyParty";
            // 
            // txtIATACode
            // 
            this.txtIATACode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "AgentIATACode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtIATACode.Location = new System.Drawing.Point(502, 3);
            this.txtIATACode.MenuManager = this.barManager1;
            this.txtIATACode.Name = "txtIATACode";
            this.txtIATACode.Size = new System.Drawing.Size(117, 21);
            this.txtIATACode.TabIndex = 669;
            // 
            // labIATACode
            // 
            this.labIATACode.Location = new System.Drawing.Point(413, 5);
            this.labIATACode.Name = "labIATACode";
            this.labIATACode.Size = new System.Drawing.Size(59, 14);
            this.labIATACode.TabIndex = 668;
            this.labIATACode.Text = "IATA code";
            // 
            // txtAgentAccountNo
            // 
            this.txtAgentAccountNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "AgentAccountNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtAgentAccountNo.Location = new System.Drawing.Point(687, 2);
            this.txtAgentAccountNo.MenuManager = this.barManager1;
            this.txtAgentAccountNo.Name = "txtAgentAccountNo";
            this.txtAgentAccountNo.Size = new System.Drawing.Size(110, 21);
            this.txtAgentAccountNo.TabIndex = 667;
            // 
            // labAgentAccountNo
            // 
            this.labAgentAccountNo.Location = new System.Drawing.Point(633, 6);
            this.labAgentAccountNo.Name = "labAgentAccountNo";
            this.labAgentAccountNo.Size = new System.Drawing.Size(45, 14);
            this.labAgentAccountNo.TabIndex = 666;
            this.labAgentAccountNo.Text = "Acc. NO";
            // 
            // txtConsigneeAccountNo
            // 
            this.txtConsigneeAccountNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ConsigneeAccountNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtConsigneeAccountNo.Location = new System.Drawing.Point(322, 134);
            this.txtConsigneeAccountNo.MenuManager = this.barManager1;
            this.txtConsigneeAccountNo.Name = "txtConsigneeAccountNo";
            this.txtConsigneeAccountNo.Size = new System.Drawing.Size(70, 21);
            this.txtConsigneeAccountNo.TabIndex = 665;
            // 
            // labConsigneeAccountNo
            // 
            this.labConsigneeAccountNo.Location = new System.Drawing.Point(274, 137);
            this.labConsigneeAccountNo.Name = "labConsigneeAccountNo";
            this.labConsigneeAccountNo.Size = new System.Drawing.Size(43, 14);
            this.labConsigneeAccountNo.TabIndex = 664;
            this.labConsigneeAccountNo.Text = "Acc. No";
            // 
            // txtShipperAccountNo
            // 
            this.txtShipperAccountNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ShipperAccountNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtShipperAccountNo.Location = new System.Drawing.Point(322, 3);
            this.txtShipperAccountNo.MenuManager = this.barManager1;
            this.txtShipperAccountNo.Name = "txtShipperAccountNo";
            this.txtShipperAccountNo.Size = new System.Drawing.Size(70, 21);
            this.txtShipperAccountNo.TabIndex = 663;
            // 
            // labShipperAccount
            // 
            this.labShipperAccount.Location = new System.Drawing.Point(274, 6);
            this.labShipperAccount.Name = "labShipperAccount";
            this.labShipperAccount.Size = new System.Drawing.Size(45, 14);
            this.labShipperAccount.TabIndex = 662;
            this.labShipperAccount.Text = "Acc. NO";
            // 
            // txtBy3
            // 
            this.txtBy3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "TranshipmentPort3By", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBy3.Location = new System.Drawing.Point(655, 266);
            this.txtBy3.MenuManager = this.barManager1;
            this.txtBy3.Name = "txtBy3";
            this.txtBy3.Size = new System.Drawing.Size(142, 21);
            this.txtBy3.TabIndex = 661;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Maroon;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Location = new System.Drawing.Point(645, 269);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(5, 14);
            this.labelControl5.TabIndex = 660;
            this.labelControl5.Text = "/";
            // 
            // txtportto3
            // 
            this.txtportto3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "TranshipmentPort3", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtportto3.Location = new System.Drawing.Point(502, 266);
            this.txtportto3.MenuManager = this.barManager1;
            this.txtportto3.Name = "txtportto3";
            this.txtportto3.Size = new System.Drawing.Size(135, 21);
            this.txtportto3.TabIndex = 659;
            // 
            // labToBy3
            // 
            this.labToBy3.Location = new System.Drawing.Point(413, 269);
            this.labToBy3.Name = "labToBy3";
            this.labToBy3.Size = new System.Drawing.Size(40, 14);
            this.labToBy3.TabIndex = 658;
            this.labToBy3.Text = "To/By3";
            // 
            // txtBy2
            // 
            this.txtBy2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "TranshipmentPort2By", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBy2.Location = new System.Drawing.Point(655, 242);
            this.txtBy2.MenuManager = this.barManager1;
            this.txtBy2.Name = "txtBy2";
            this.txtBy2.Size = new System.Drawing.Size(142, 21);
            this.txtBy2.TabIndex = 562;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Maroon;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(645, 248);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(5, 14);
            this.labelControl3.TabIndex = 561;
            this.labelControl3.Text = "/";
            // 
            // txtportto2
            // 
            this.txtportto2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "TranshipmentPort2", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtportto2.Location = new System.Drawing.Point(502, 242);
            this.txtportto2.MenuManager = this.barManager1;
            this.txtportto2.Name = "txtportto2";
            this.txtportto2.Size = new System.Drawing.Size(135, 21);
            this.txtportto2.TabIndex = 560;
            // 
            // labToBy2
            // 
            this.labToBy2.Location = new System.Drawing.Point(413, 245);
            this.labToBy2.Name = "labToBy2";
            this.labToBy2.Size = new System.Drawing.Size(40, 14);
            this.labToBy2.TabIndex = 559;
            this.labToBy2.Text = "To/By2";
            // 
            // txtBy1
            // 
            this.txtBy1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "TranshipmentPort1By", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBy1.Location = new System.Drawing.Point(655, 218);
            this.txtBy1.MenuManager = this.barManager1;
            this.txtBy1.Name = "txtBy1";
            this.txtBy1.Size = new System.Drawing.Size(142, 21);
            this.txtBy1.TabIndex = 558;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Maroon;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(645, 221);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(5, 14);
            this.labelControl1.TabIndex = 557;
            this.labelControl1.Text = "/";
            // 
            // txtportto1
            // 
            this.txtportto1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "TranshipmentPort1", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtportto1.Location = new System.Drawing.Point(502, 218);
            this.txtportto1.MenuManager = this.barManager1;
            this.txtportto1.Name = "txtportto1";
            this.txtportto1.Size = new System.Drawing.Size(135, 21);
            this.txtportto1.TabIndex = 556;
            // 
            // labToBy1
            // 
            this.labToBy1.Location = new System.Drawing.Point(413, 221);
            this.labToBy1.Name = "labToBy1";
            this.labToBy1.Size = new System.Drawing.Size(40, 14);
            this.labToBy1.TabIndex = 555;
            this.labToBy1.Text = "To/By1";
            // 
            // labAgentOfCarrier
            // 
            this.labAgentOfCarrier.Location = new System.Drawing.Point(413, 53);
            this.labAgentOfCarrier.Name = "labAgentOfCarrier";
            this.labAgentOfCarrier.Size = new System.Drawing.Size(81, 14);
            this.labAgentOfCarrier.TabIndex = 545;
            this.labAgentOfCarrier.Text = "AgentOfCarrier";
            // 
            // stxtAgent
            // 
            this.stxtAgent.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "AgentID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtAgent.DataSource = null;
            this.stxtAgent.DisplayMember = "EName";
            this.stxtAgent.EditValue = null;
            this.stxtAgent.Location = new System.Drawing.Point(502, 26);
            this.stxtAgent.Margin = new System.Windows.Forms.Padding(0);
            this.stxtAgent.Name = "stxtAgent";
            this.stxtAgent.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Bottom;
            this.stxtAgent.Size = new System.Drawing.Size(295, 21);
            this.stxtAgent.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtAgent.TabIndex = 6;
            this.stxtAgent.Tag = null;
            this.stxtAgent.ValueMember = "ID";
            // 
            // labDetination
            // 
            this.labDetination.Location = new System.Drawing.Point(413, 101);
            this.labDetination.Name = "labDetination";
            this.labDetination.Size = new System.Drawing.Size(56, 14);
            this.labDetination.TabIndex = 21;
            this.labDetination.Text = "Detination";
            // 
            // dteETA
            // 
            this.dteETA.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ETA", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteETA.EditValue = null;
            this.dteETA.Location = new System.Drawing.Point(687, 194);
            this.dteETA.Name = "dteETA";
            this.dteETA.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.dteETA.Properties.Appearance.Options.UseBackColor = true;
            this.dteETA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteETA.Properties.Mask.EditMask = "";
            this.dteETA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteETA.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteETA.Size = new System.Drawing.Size(110, 21);
            this.dteETA.TabIndex = 15;
            this.dteETA.TabStop = false;
            // 
            // labETA
            // 
            this.labETA.Location = new System.Drawing.Point(633, 197);
            this.labETA.Name = "labETA";
            this.labETA.Size = new System.Drawing.Size(23, 14);
            this.labETA.TabIndex = 22;
            this.labETA.Text = "ETA";
            // 
            // labDeparture
            // 
            this.labDeparture.Location = new System.Drawing.Point(413, 77);
            this.labDeparture.Name = "labDeparture";
            this.labDeparture.Size = new System.Drawing.Size(55, 14);
            this.labDeparture.TabIndex = 28;
            this.labDeparture.Text = "Departure";
            // 
            // dteETD
            // 
            this.dteETD.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ETD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteETD.EditValue = null;
            this.dteETD.Location = new System.Drawing.Point(502, 194);
            this.dteETD.Name = "dteETD";
            this.dteETD.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.dteETD.Properties.Appearance.Options.UseBackColor = true;
            this.dteETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteETD.Properties.Mask.EditMask = "";
            this.dteETD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteETD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteETD.Size = new System.Drawing.Size(125, 21);
            this.dteETD.TabIndex = 14;
            this.dteETD.TabStop = false;
            // 
            // labETD
            // 
            this.labETD.Location = new System.Drawing.Point(413, 197);
            this.labETD.Name = "labETD";
            this.labETD.Size = new System.Drawing.Size(23, 14);
            this.labETD.TabIndex = 24;
            this.labETD.Text = "ETD";
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.panel3);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(804, 400);
            this.navBarGroupControlContainer3.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel3.Controls.Add(this.txtCarrierChargers);
            this.panel3.Controls.Add(this.txtTax);
            this.panel3.Controls.Add(this.txtValuationCharge);
            this.panel3.Controls.Add(this.txtAgentChargers);
            this.panel3.Controls.Add(this.txtChargesDestination);
            this.panel3.Controls.Add(this.labChargesDestination);
            this.panel3.Controls.Add(this.txtCCCharges);
            this.panel3.Controls.Add(this.labCCCharges);
            this.panel3.Controls.Add(this.txtCurrencyConversionRate);
            this.panel3.Controls.Add(this.labConversionRates);
            this.panel3.Controls.Add(this.cmbTarifflevel);
            this.panel3.Controls.Add(this.labTarifflevel);
            this.panel3.Controls.Add(this.labTax);
            this.panel3.Controls.Add(this.labValuationCharge);
            this.panel3.Controls.Add(this.labCarrierChargers);
            this.panel3.Controls.Add(this.labAgentChargers);
            this.panel3.Controls.Add(this.labMeasurement);
            this.panel3.Controls.Add(this.cmbMeasurementUnit);
            this.panel3.Controls.Add(this.spinEdit7);
            this.panel3.Controls.Add(this.txtInsurance);
            this.panel3.Controls.Add(this.txtCustoms);
            this.panel3.Controls.Add(this.labInsurance);
            this.panel3.Controls.Add(this.cmbOther);
            this.panel3.Controls.Add(this.labDCLCustoms);
            this.panel3.Controls.Add(this.labOther);
            this.panel3.Controls.Add(this.labPaymentTerm);
            this.panel3.Controls.Add(this.txtCarriage);
            this.panel3.Controls.Add(this.cmbPaymentTerm);
            this.panel3.Controls.Add(this.labDCLCarriage);
            this.panel3.Controls.Add(this.txtGLBS);
            this.panel3.Controls.Add(this.txtChLBS);
            this.panel3.Controls.Add(this.labelControl4);
            this.panel3.Controls.Add(this.labelControl2);
            this.panel3.Controls.Add(this.txtChKGS);
            this.panel3.Controls.Add(this.txtGKGS);
            this.panel3.Controls.Add(this.numQuantity);
            this.panel3.Controls.Add(this.labQuantity);
            this.panel3.Controls.Add(this.labWeight);
            this.panel3.Controls.Add(this.labChargeKGSLBS);
            this.panel3.Controls.Add(this.cmbQuantityUnit);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.radioGroup2);
            this.panel3.Controls.Add(this.radioGroup1);
            this.panel3.Controls.Add(this.txtIATAAmount);
            this.panel3.Controls.Add(this.txtIATAPrice);
            this.panel3.Controls.Add(this.txtBuyAmount);
            this.panel3.Controls.Add(this.cmbIATACur);
            this.panel3.Controls.Add(this.labIATARate);
            this.panel3.Controls.Add(this.txtBuyPrice);
            this.panel3.Controls.Add(this.labBuyRate);
            this.panel3.Controls.Add(this.txtHandingInfo);
            this.panel3.Controls.Add(this.cmbBuyCur);
            this.panel3.Controls.Add(this.labHandlingInfomation);
            this.panel3.Controls.Add(this.txtMarks);
            this.panel3.Controls.Add(this.labMarks);
            this.panel3.Controls.Add(this.txtGoodsDescription);
            this.panel3.Controls.Add(this.labGoodsDescription);
            this.panel3.Controls.Add(this.labFreightDescription);
            this.panel3.Controls.Add(this.txtFreightDescription);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 400);
            this.panel3.TabIndex = 2;
            // 
            // txtCarrierChargers
            // 
            this.txtCarrierChargers.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CarrierCharger", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCarrierChargers.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCarrierChargers.Location = new System.Drawing.Point(306, 175);
            this.txtCarrierChargers.MenuManager = this.barManager1;
            this.txtCarrierChargers.Name = "txtCarrierChargers";
            this.txtCarrierChargers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCarrierChargers.Properties.IsFloatValue = false;
            this.txtCarrierChargers.Properties.Mask.EditMask = "N00";
            this.txtCarrierChargers.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.txtCarrierChargers.Size = new System.Drawing.Size(95, 21);
            this.txtCarrierChargers.TabIndex = 709;
            // 
            // txtTax
            // 
            this.txtTax.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Tax", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtTax.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTax.Location = new System.Drawing.Point(230, 200);
            this.txtTax.MenuManager = this.barManager1;
            this.txtTax.Name = "txtTax";
            this.txtTax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtTax.Properties.IsFloatValue = false;
            this.txtTax.Properties.Mask.EditMask = "N00";
            this.txtTax.Size = new System.Drawing.Size(60, 21);
            this.txtTax.TabIndex = 708;
            // 
            // txtValuationCharge
            // 
            this.txtValuationCharge.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ValuationCharge", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtValuationCharge.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtValuationCharge.Location = new System.Drawing.Point(103, 200);
            this.txtValuationCharge.MenuManager = this.barManager1;
            this.txtValuationCharge.Name = "txtValuationCharge";
            this.txtValuationCharge.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtValuationCharge.Properties.IsFloatValue = false;
            this.txtValuationCharge.Properties.Mask.EditMask = "N00";
            this.txtValuationCharge.Size = new System.Drawing.Size(91, 21);
            this.txtValuationCharge.TabIndex = 707;
            // 
            // txtAgentChargers
            // 
            this.txtAgentChargers.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "AgentCharger", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtAgentChargers.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAgentChargers.Location = new System.Drawing.Point(103, 175);
            this.txtAgentChargers.MenuManager = this.barManager1;
            this.txtAgentChargers.Name = "txtAgentChargers";
            this.txtAgentChargers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtAgentChargers.Properties.IsFloatValue = false;
            this.txtAgentChargers.Properties.Mask.EditMask = "N00";
            this.txtAgentChargers.Size = new System.Drawing.Size(91, 21);
            this.txtAgentChargers.TabIndex = 706;
            // 
            // txtChargesDestination
            // 
            this.txtChargesDestination.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ChargesAtDestination", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtChargesDestination.Location = new System.Drawing.Point(574, 376);
            this.txtChargesDestination.MenuManager = this.barManager1;
            this.txtChargesDestination.Name = "txtChargesDestination";
            this.txtChargesDestination.Size = new System.Drawing.Size(223, 21);
            this.txtChargesDestination.TabIndex = 705;
            // 
            // labChargesDestination
            // 
            this.labChargesDestination.Location = new System.Drawing.Point(413, 379);
            this.labChargesDestination.Name = "labChargesDestination";
            this.labChargesDestination.Size = new System.Drawing.Size(123, 14);
            this.labChargesDestination.TabIndex = 704;
            this.labChargesDestination.Text = "Charges at Destination";
            // 
            // txtCCCharges
            // 
            this.txtCCCharges.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "DestinationCurrencyAmount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCCCharges.Location = new System.Drawing.Point(574, 353);
            this.txtCCCharges.MenuManager = this.barManager1;
            this.txtCCCharges.Name = "txtCCCharges";
            this.txtCCCharges.Size = new System.Drawing.Size(223, 21);
            this.txtCCCharges.TabIndex = 703;
            // 
            // labCCCharges
            // 
            this.labCCCharges.Location = new System.Drawing.Point(413, 356);
            this.labCCCharges.Name = "labCCCharges";
            this.labCCCharges.Size = new System.Drawing.Size(155, 14);
            this.labCCCharges.TabIndex = 702;
            this.labCCCharges.Text = "CC Charges in Dest.Currency";
            // 
            // txtCurrencyConversionRate
            // 
            this.txtCurrencyConversionRate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CurrencyConversionRate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCurrencyConversionRate.Location = new System.Drawing.Point(574, 330);
            this.txtCurrencyConversionRate.MenuManager = this.barManager1;
            this.txtCurrencyConversionRate.Name = "txtCurrencyConversionRate";
            this.txtCurrencyConversionRate.Size = new System.Drawing.Size(223, 21);
            this.txtCurrencyConversionRate.TabIndex = 701;
            // 
            // labConversionRates
            // 
            this.labConversionRates.Location = new System.Drawing.Point(413, 333);
            this.labConversionRates.Name = "labConversionRates";
            this.labConversionRates.Size = new System.Drawing.Size(145, 14);
            this.labConversionRates.TabIndex = 700;
            this.labConversionRates.Text = "Currency Conversion Rates";
            // 
            // labTarifflevel
            // 
            this.labTarifflevel.Location = new System.Drawing.Point(294, 203);
            this.labTarifflevel.Name = "labTarifflevel";
            this.labTarifflevel.Size = new System.Drawing.Size(56, 14);
            this.labTarifflevel.TabIndex = 698;
            this.labTarifflevel.Text = "Tariff level";
            // 
            // labTax
            // 
            this.labTax.Location = new System.Drawing.Point(203, 203);
            this.labTax.Name = "labTax";
            this.labTax.Size = new System.Drawing.Size(20, 14);
            this.labTax.TabIndex = 696;
            this.labTax.Text = "Tax";
            // 
            // labValuationCharge
            // 
            this.labValuationCharge.Location = new System.Drawing.Point(5, 203);
            this.labValuationCharge.Name = "labValuationCharge";
            this.labValuationCharge.Size = new System.Drawing.Size(92, 14);
            this.labValuationCharge.TabIndex = 694;
            this.labValuationCharge.Text = "Valuation Charge";
            // 
            // labCarrierChargers
            // 
            this.labCarrierChargers.Location = new System.Drawing.Point(203, 178);
            this.labCarrierChargers.Name = "labCarrierChargers";
            this.labCarrierChargers.Size = new System.Drawing.Size(85, 14);
            this.labCarrierChargers.TabIndex = 692;
            this.labCarrierChargers.Text = "Carrier Chargers";
            // 
            // labAgentChargers
            // 
            this.labAgentChargers.Location = new System.Drawing.Point(5, 178);
            this.labAgentChargers.Name = "labAgentChargers";
            this.labAgentChargers.Size = new System.Drawing.Size(85, 14);
            this.labAgentChargers.TabIndex = 690;
            this.labAgentChargers.Text = "Agent Chargers";
            // 
            // labMeasurement
            // 
            this.labMeasurement.Location = new System.Drawing.Point(203, 56);
            this.labMeasurement.Name = "labMeasurement";
            this.labMeasurement.Size = new System.Drawing.Size(23, 14);
            this.labMeasurement.TabIndex = 687;
            this.labMeasurement.Text = "VOL";
            // 
            // txtInsurance
            // 
            this.txtInsurance.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "InsuranceAmount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtInsurance.Location = new System.Drawing.Point(277, 29);
            this.txtInsurance.MenuManager = this.barManager1;
            this.txtInsurance.Name = "txtInsurance";
            this.txtInsurance.Size = new System.Drawing.Size(124, 21);
            this.txtInsurance.TabIndex = 684;
            // 
            // txtCustoms
            // 
            this.txtCustoms.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "DeclaredValueForCustoms", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCustoms.Location = new System.Drawing.Point(277, 5);
            this.txtCustoms.MenuManager = this.barManager1;
            this.txtCustoms.Name = "txtCustoms";
            this.txtCustoms.Size = new System.Drawing.Size(124, 21);
            this.txtCustoms.TabIndex = 672;
            // 
            // labInsurance
            // 
            this.labInsurance.Location = new System.Drawing.Point(203, 32);
            this.labInsurance.Name = "labInsurance";
            this.labInsurance.Size = new System.Drawing.Size(53, 14);
            this.labInsurance.TabIndex = 682;
            this.labInsurance.Text = "Insurance";
            // 
            // cmbOther
            // 
            this.cmbOther.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "OtherPaymentTermID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbOther.Location = new System.Drawing.Point(149, 29);
            this.cmbOther.MenuManager = this.barManager1;
            this.cmbOther.Name = "cmbOther";
            this.cmbOther.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbOther.Properties.Appearance.Options.UseBackColor = true;
            this.cmbOther.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOther.Size = new System.Drawing.Size(48, 21);
            this.cmbOther.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbOther.TabIndex = 680;
            // 
            // labDCLCustoms
            // 
            this.labDCLCustoms.Location = new System.Drawing.Point(203, 8);
            this.labDCLCustoms.Name = "labDCLCustoms";
            this.labDCLCustoms.Size = new System.Drawing.Size(71, 14);
            this.labDCLCustoms.TabIndex = 669;
            this.labDCLCustoms.Text = "DCL Customs";
            // 
            // labOther
            // 
            this.labOther.Location = new System.Drawing.Point(116, 32);
            this.labOther.Name = "labOther";
            this.labOther.Size = new System.Drawing.Size(32, 14);
            this.labOther.TabIndex = 679;
            this.labOther.Text = "Other";
            // 
            // labPaymentTerm
            // 
            this.labPaymentTerm.Location = new System.Drawing.Point(5, 32);
            this.labPaymentTerm.Name = "labPaymentTerm";
            this.labPaymentTerm.Size = new System.Drawing.Size(77, 14);
            this.labPaymentTerm.TabIndex = 678;
            this.labPaymentTerm.Text = "PaymentTerm";
            // 
            // txtCarriage
            // 
            this.txtCarriage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "DeclaredValueForCarriage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCarriage.Location = new System.Drawing.Point(79, 5);
            this.txtCarriage.MenuManager = this.barManager1;
            this.txtCarriage.Name = "txtCarriage";
            this.txtCarriage.Size = new System.Drawing.Size(118, 21);
            this.txtCarriage.TabIndex = 667;
            // 
            // labDCLCarriage
            // 
            this.labDCLCarriage.Location = new System.Drawing.Point(5, 8);
            this.labDCLCarriage.Name = "labDCLCarriage";
            this.labDCLCarriage.Size = new System.Drawing.Size(68, 14);
            this.labDCLCarriage.TabIndex = 666;
            this.labDCLCarriage.Text = "DCL Carriage";
            // 
            // txtGLBS
            // 
            this.txtGLBS.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "GrossLBS", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtGLBS.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGLBS.Location = new System.Drawing.Point(249, 77);
            this.txtGLBS.Name = "txtGLBS";
            this.txtGLBS.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtGLBS.Properties.Appearance.Options.UseBackColor = true;
            this.txtGLBS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtGLBS.Properties.Mask.EditMask = "F3";
            this.txtGLBS.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.txtGLBS.Size = new System.Drawing.Size(152, 21);
            this.txtGLBS.TabIndex = 686;
            // 
            // txtChLBS
            // 
            this.txtChLBS.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ChargeLBS", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtChLBS.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtChLBS.Location = new System.Drawing.Point(249, 102);
            this.txtChLBS.Name = "txtChLBS";
            this.txtChLBS.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtChLBS.Properties.Appearance.Options.UseBackColor = true;
            this.txtChLBS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtChLBS.Properties.Mask.EditMask = "F3";
            this.txtChLBS.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.txtChLBS.Size = new System.Drawing.Size(152, 21);
            this.txtChLBS.TabIndex = 685;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Maroon;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(233, 105);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(5, 14);
            this.labelControl4.TabIndex = 683;
            this.labelControl4.Text = "/";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Maroon;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(232, 80);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(5, 14);
            this.labelControl2.TabIndex = 681;
            this.labelControl2.Text = "/";
            // 
            // txtChKGS
            // 
            this.txtChKGS.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ChargeKGS", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtChKGS.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtChKGS.Location = new System.Drawing.Point(65, 102);
            this.txtChKGS.Name = "txtChKGS";
            this.txtChKGS.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtChKGS.Properties.Appearance.Options.UseBackColor = true;
            this.txtChKGS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtChKGS.Properties.Mask.EditMask = "F3";
            this.txtChKGS.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.txtChKGS.Size = new System.Drawing.Size(152, 21);
            this.txtChKGS.TabIndex = 673;
            // 
            // txtGKGS
            // 
            this.txtGKGS.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "GrossKGS", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtGKGS.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGKGS.Location = new System.Drawing.Point(65, 77);
            this.txtGKGS.Name = "txtGKGS";
            this.txtGKGS.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtGKGS.Properties.Appearance.Options.UseBackColor = true;
            this.txtGKGS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtGKGS.Properties.Mask.EditMask = "F3";
            this.txtGKGS.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.txtGKGS.Size = new System.Drawing.Size(152, 21);
            this.txtGKGS.TabIndex = 671;
            // 
            // numQuantity
            // 
            this.numQuantity.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Quantity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numQuantity.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numQuantity.Location = new System.Drawing.Point(65, 53);
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
            this.numQuantity.Size = new System.Drawing.Size(67, 21);
            this.numQuantity.TabIndex = 668;
            // 
            // labQuantity
            // 
            this.labQuantity.Location = new System.Drawing.Point(5, 56);
            this.labQuantity.Name = "labQuantity";
            this.labQuantity.Size = new System.Drawing.Size(47, 14);
            this.labQuantity.TabIndex = 677;
            this.labQuantity.Text = "Quantity";
            // 
            // labWeight
            // 
            this.labWeight.Location = new System.Drawing.Point(5, 80);
            this.labWeight.Name = "labWeight";
            this.labWeight.Size = new System.Drawing.Size(80, 14);
            this.labWeight.TabIndex = 675;
            this.labWeight.Text = "Gross KGS/LBS";
            // 
            // labChargeKGSLBS
            // 
            this.labChargeKGSLBS.Location = new System.Drawing.Point(5, 105);
            this.labChargeKGSLBS.Name = "labChargeKGSLBS";
            this.labChargeKGSLBS.Size = new System.Drawing.Size(89, 14);
            this.labChargeKGSLBS.TabIndex = 676;
            this.labChargeKGSLBS.Text = "Charge KGS/LBS";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtOtherChargers);
            this.groupBox1.Location = new System.Drawing.Point(5, 223);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 176);
            this.groupBox1.TabIndex = 659;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Other Chargers";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.panelControl3);
            this.groupBox2.Location = new System.Drawing.Point(4, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(227, 156);
            this.groupBox2.TabIndex = 658;
            this.groupBox2.TabStop = false;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.gridControl1);
            this.panelControl3.Controls.Add(this.barDockControl3);
            this.panelControl3.Controls.Add(this.barDockControl4);
            this.panelControl3.Controls.Add(this.barDockControl2);
            this.panelControl3.Controls.Add(this.barDockControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(3, 18);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(221, 135);
            this.panelControl3.TabIndex = 38;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.OtherChargesBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 26);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit1});
            this.gridControl1.Size = new System.Drawing.Size(221, 109);
            this.gridControl1.TabIndex = 26;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // OtherChargesBindingSource
            // 
            this.OtherChargesBindingSource.DataMember = "OtherChargeList";
            this.OtherChargesBindingSource.DataSource = this.bindingSource1;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colItem,
            this.colAmount});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 30;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.mainGridView_CustomDrawRowIndicator);
            // 
            // colItem
            // 
            this.colItem.Caption = "Item";
            this.colItem.FieldName = "ChargeName";
            this.colItem.Name = "colItem";
            this.colItem.Visible = true;
            this.colItem.VisibleIndex = 0;
            this.colItem.Width = 126;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "Amount";
            this.colAmount.ColumnEdit = this.repositoryItemSpinEdit1;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 1;
            this.colAmount.Width = 59;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // barDockControl3
            // 
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 26);
            this.barDockControl3.Size = new System.Drawing.Size(0, 109);
            // 
            // barDockControl4
            // 
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(221, 26);
            this.barDockControl4.Size = new System.Drawing.Size(0, 109);
            // 
            // barDockControl2
            // 
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 135);
            this.barDockControl2.Size = new System.Drawing.Size(221, 0);
            // 
            // barDockControl1
            // 
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Size = new System.Drawing.Size(221, 26);
            // 
            // txtOtherChargers
            // 
            this.txtOtherChargers.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "OtherChargeDescription", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtOtherChargers.Location = new System.Drawing.Point(237, 19);
            this.txtOtherChargers.MenuManager = this.barManager1;
            this.txtOtherChargers.Name = "txtOtherChargers";
            this.txtOtherChargers.Size = new System.Drawing.Size(144, 150);
            this.txtOtherChargers.TabIndex = 657;
            // 
            // radioGroup2
            // 
            this.radioGroup2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IATAChargeableWeightUnitIsKGS", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.radioGroup2.Location = new System.Drawing.Point(277, 149);
            this.radioGroup2.MenuManager = this.barManager1;
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "K"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "L")});
            this.radioGroup2.Size = new System.Drawing.Size(124, 26);
            this.radioGroup2.TabIndex = 59;
            // 
            // radioGroup1
            // 
            this.radioGroup1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ChargeableWeightUnitIsKGS", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.radioGroup1.Location = new System.Drawing.Point(277, 124);
            this.radioGroup1.MenuManager = this.barManager1;
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "K"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "L")});
            this.radioGroup1.Size = new System.Drawing.Size(124, 26);
            this.radioGroup1.TabIndex = 55;
            // 
            // txtIATAAmount
            // 
            this.txtIATAAmount.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IATARateAmount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtIATAAmount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtIATAAmount.Location = new System.Drawing.Point(203, 151);
            this.txtIATAAmount.MenuManager = this.barManager1;
            this.txtIATAAmount.Name = "txtIATAAmount";
            this.txtIATAAmount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtIATAAmount.Properties.DisplayFormat.FormatString = "F3";
            this.txtIATAAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIATAAmount.Properties.EditFormat.FormatString = "F3";
            this.txtIATAAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIATAAmount.Properties.Mask.EditMask = "F3";
            this.txtIATAAmount.Size = new System.Drawing.Size(71, 21);
            this.txtIATAAmount.TabIndex = 58;
            // 
            // txtIATAPrice
            // 
            this.txtIATAPrice.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IATARateCharge", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtIATAPrice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtIATAPrice.Location = new System.Drawing.Point(135, 151);
            this.txtIATAPrice.MenuManager = this.barManager1;
            this.txtIATAPrice.Name = "txtIATAPrice";
            this.txtIATAPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtIATAPrice.Properties.DisplayFormat.FormatString = "F3";
            this.txtIATAPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIATAPrice.Properties.EditFormat.FormatString = "F3";
            this.txtIATAPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIATAPrice.Properties.Mask.EditMask = "F3";
            this.txtIATAPrice.Size = new System.Drawing.Size(59, 21);
            this.txtIATAPrice.TabIndex = 57;
            // 
            // txtBuyAmount
            // 
            this.txtBuyAmount.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "RateAmount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBuyAmount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBuyAmount.Location = new System.Drawing.Point(203, 127);
            this.txtBuyAmount.MenuManager = this.barManager1;
            this.txtBuyAmount.Name = "txtBuyAmount";
            this.txtBuyAmount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtBuyAmount.Properties.DisplayFormat.FormatString = "F3";
            this.txtBuyAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtBuyAmount.Properties.EditFormat.FormatString = "F3";
            this.txtBuyAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtBuyAmount.Properties.Mask.EditMask = "F3";
            this.txtBuyAmount.Size = new System.Drawing.Size(71, 21);
            this.txtBuyAmount.TabIndex = 54;
            // 
            // cmbIATACur
            // 
            this.cmbIATACur.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IATACurrencyID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbIATACur.Location = new System.Drawing.Point(65, 151);
            this.cmbIATACur.MenuManager = this.barManager1;
            this.cmbIATACur.Name = "cmbIATACur";
            this.cmbIATACur.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbIATACur.Size = new System.Drawing.Size(59, 21);
            this.cmbIATACur.TabIndex = 56;
            // 
            // labIATARate
            // 
            this.labIATARate.Location = new System.Drawing.Point(5, 154);
            this.labIATARate.Name = "labIATARate";
            this.labIATARate.Size = new System.Drawing.Size(57, 14);
            this.labIATARate.TabIndex = 0;
            this.labIATARate.Text = "IATA Rate";
            // 
            // txtBuyPrice
            // 
            this.txtBuyPrice.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "RateCharge", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBuyPrice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBuyPrice.Location = new System.Drawing.Point(135, 127);
            this.txtBuyPrice.MenuManager = this.barManager1;
            this.txtBuyPrice.Name = "txtBuyPrice";
            this.txtBuyPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtBuyPrice.Properties.DisplayFormat.FormatString = "F3";
            this.txtBuyPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtBuyPrice.Properties.EditFormat.FormatString = "F3";
            this.txtBuyPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtBuyPrice.Properties.Mask.EditMask = "F3";
            this.txtBuyPrice.Size = new System.Drawing.Size(59, 21);
            this.txtBuyPrice.TabIndex = 53;
            // 
            // labBuyRate
            // 
            this.labBuyRate.Location = new System.Drawing.Point(5, 130);
            this.labBuyRate.Name = "labBuyRate";
            this.labBuyRate.Size = new System.Drawing.Size(49, 14);
            this.labBuyRate.TabIndex = 51;
            this.labBuyRate.Text = "Buy Rate";
            // 
            // txtHandingInfo
            // 
            this.txtHandingInfo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "HandingInformation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtHandingInfo.Location = new System.Drawing.Point(413, 242);
            this.txtHandingInfo.MenuManager = this.barManager1;
            this.txtHandingInfo.Name = "txtHandingInfo";
            this.txtHandingInfo.Size = new System.Drawing.Size(197, 82);
            this.txtHandingInfo.TabIndex = 60;
            // 
            // cmbBuyCur
            // 
            this.cmbBuyCur.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CurrencyID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbBuyCur.Location = new System.Drawing.Point(65, 127);
            this.cmbBuyCur.MenuManager = this.barManager1;
            this.cmbBuyCur.Name = "cmbBuyCur";
            this.cmbBuyCur.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbBuyCur.Properties.Appearance.Options.UseBackColor = true;
            this.cmbBuyCur.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBuyCur.Size = new System.Drawing.Size(59, 21);
            this.cmbBuyCur.TabIndex = 52;
            // 
            // labHandlingInfomation
            // 
            this.labHandlingInfomation.Location = new System.Drawing.Point(413, 226);
            this.labHandlingInfomation.Name = "labHandlingInfomation";
            this.labHandlingInfomation.Size = new System.Drawing.Size(109, 14);
            this.labHandlingInfomation.TabIndex = 59;
            this.labHandlingInfomation.Text = "Handling Infomation";
            // 
            // txtMarks
            // 
            this.txtMarks.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Marks", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtMarks.Location = new System.Drawing.Point(413, 132);
            this.txtMarks.MenuManager = this.barManager1;
            this.txtMarks.Name = "txtMarks";
            this.txtMarks.Size = new System.Drawing.Size(384, 90);
            this.txtMarks.TabIndex = 58;
            // 
            // labMarks
            // 
            this.labMarks.Location = new System.Drawing.Point(413, 117);
            this.labMarks.Name = "labMarks";
            this.labMarks.Size = new System.Drawing.Size(30, 14);
            this.labMarks.TabIndex = 57;
            this.labMarks.Text = "Marks";
            // 
            // txtGoodsDescription
            // 
            this.txtGoodsDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "GoodsDescription", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtGoodsDescription.Location = new System.Drawing.Point(413, 22);
            this.txtGoodsDescription.MenuManager = this.barManager1;
            this.txtGoodsDescription.Name = "txtGoodsDescription";
            this.txtGoodsDescription.Size = new System.Drawing.Size(384, 90);
            this.txtGoodsDescription.TabIndex = 56;
            // 
            // labGoodsDescription
            // 
            this.labGoodsDescription.Location = new System.Drawing.Point(413, 6);
            this.labGoodsDescription.Name = "labGoodsDescription";
            this.labGoodsDescription.Size = new System.Drawing.Size(98, 14);
            this.labGoodsDescription.TabIndex = 55;
            this.labGoodsDescription.Text = "Goods Description";
            // 
            // navBarGroupControlContainer4
            // 
            this.navBarGroupControlContainer4.Controls.Add(this.panel4);
            this.navBarGroupControlContainer4.Name = "navBarGroupControlContainer4";
            this.navBarGroupControlContainer4.Size = new System.Drawing.Size(804, 165);
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
            this.panel4.Size = new System.Drawing.Size(800, 165);
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
            this.mcmbIssueBy.Location = new System.Drawing.Point(65, 1);
            this.mcmbIssueBy.Name = "mcmbIssueBy";
            this.mcmbIssueBy.ReadOnly = false;
            this.mcmbIssueBy.RefreshButtonToolTip = "";
            this.mcmbIssueBy.ShowRefreshButton = false;
            this.mcmbIssueBy.Size = new System.Drawing.Size(122, 21);
            this.mcmbIssueBy.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.mcmbIssueBy.TabIndex = 0;
            this.mcmbIssueBy.ToolTip = "";
            // 
            // navBarBLInfo
            // 
            this.navBarBLInfo.Caption = "BL Info";
            this.navBarBLInfo.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarBLInfo.Expanded = true;
            this.navBarBLInfo.GroupClientHeight = 292;
            this.navBarBLInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBLInfo.Name = "navBarBLInfo";
            // 
            // navBarCargo
            // 
            this.navBarCargo.Caption = "Cargo";
            this.navBarCargo.ControlContainer = this.navBarGroupControlContainer3;
            this.navBarCargo.Expanded = true;
            this.navBarCargo.GroupClientHeight = 402;
            this.navBarCargo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarCargo.Name = "navBarCargo";
            // 
            // navBarIssueInfo
            // 
            this.navBarIssueInfo.Caption = "Issue Info";
            this.navBarIssueInfo.ControlContainer = this.navBarGroupControlContainer4;
            this.navBarIssueInfo.Expanded = true;
            this.navBarIssueInfo.GroupClientHeight = 167;
            this.navBarIssueInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarIssueInfo.Name = "navBarIssueInfo";
            // 
            // barManager2
            // 
            this.barManager2.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.Form = this.panelControl3;
            this.barManager2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAdd,
            this.barDelect});
            this.barManager2.MainMenu = this.bar3;
            this.barManager2.MaxItemId = 5;
            // 
            // bar3
            // 
            this.bar3.BarName = "Main menu";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.FloatLocation = new System.Drawing.Point(382, 187);
            this.bar3.FloatSize = new System.Drawing.Size(148, 24);
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelect)});
            this.bar3.OptionsBar.MultiLine = true;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "新增(&A)";
            this.barAdd.Id = 3;
            this.barAdd.Name = "barAdd";
            this.barAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barDelect
            // 
            this.barDelect.Caption = "删除(&D)";
            this.barDelect.Id = 4;
            this.barDelect.Name = "barDelect";
            this.barDelect.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barDelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelect_ItemClick);
            // 
            // MAWBEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelScroll);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "MAWBEditPart";
            this.Size = new System.Drawing.Size(828, 497);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtChecker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtIssuePlace.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIssue.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIssue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipperDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFreightDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtRefNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDetinationName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartureName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartureCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDetinationCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgentOfCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtNotifyParty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit7.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuantityUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTarifflevel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfDeliveryName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelScroll)).EndInit();
            this.panelScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHBLNO.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIATACode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgentAccountNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeAccountNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipperAccountNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBy3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtportto3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBy2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtportto2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBy1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtportto1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties)).EndInit();
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrierChargers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValuationCharge.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgentChargers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChargesDestination.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCCCharges.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyConversionRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInsurance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustoms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOther.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarriage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGLBS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChLBS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChKGS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGKGS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OtherChargesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherChargers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIATAAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIATAPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuyAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIATACur.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuyPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHandingInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBuyCur.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoodsDescription.Properties)).EndInit();
            this.navBarGroupControlContainer4.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            this.ResumeLayout(false);

        }    

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.LabelControl labMAWBNO;
        private DevExpress.XtraEditors.LabelControl labRefNo;
        private DevExpress.XtraEditors.LabelControl labIssueBy;
        private DevExpress.XtraEditors.LabelControl labIssueDate;
        private DevExpress.XtraEditors.LabelControl labIssuePlace;
        private DevExpress.XtraEditors.DateEdit dteIssue;
        private DevExpress.XtraEditors.ButtonEdit stxtIssuePlace;
        private DevExpress.XtraEditors.MemoEdit txtConsigneeDescription;
        private DevExpress.XtraEditors.MemoEdit txtShipperDescription;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtConsignee;
        private DevExpress.XtraEditors.LabelControl labConsignee;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtShipper;
        private DevExpress.XtraEditors.LabelControl labShipper;
        private DevExpress.XtraEditors.ButtonEdit stxtChecker;
        private DevExpress.XtraEditors.LabelControl labChecker;
        private DevExpress.XtraEditors.LabelControl labAgent;
        private DevExpress.XtraEditors.MemoEdit txtFreightDescription;
        private DevExpress.XtraEditors.LabelControl labFreightDescription;
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
        private DevExpress.XtraEditors.PanelControl panelScroll;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private DevExpress.XtraNavBar.NavBarGroup navBarBLInfo;
        private DevExpress.XtraNavBar.NavBarGroup navBarCargo;
        private DevExpress.XtraEditors.LabelControl labDetination;
        private DevExpress.XtraEditors.DateEdit dteETA;
        private DevExpress.XtraEditors.LabelControl labETA;
        private DevExpress.XtraEditors.LabelControl labDeparture;
        private DevExpress.XtraEditors.TextEdit txtDetinationName;
        private DevExpress.XtraEditors.DateEdit dteETD;
        private DevExpress.XtraEditors.LabelControl labETD;
        private DevExpress.XtraEditors.TextEdit txtDepartureName;
        private ICP.Framework.ClientComponents.Controls.ComboCustomerDescriptionControl stxtAgent;
        private DevExpress.XtraEditors.ButtonEdit txtDepartureCode;
        private DevExpress.XtraEditors.ButtonEdit txtDetinationCode;
        private DevExpress.XtraNavBar.NavBarGroup navBarIssueInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer4;
        private DevExpress.XtraEditors.LabelControl labHBLNO;
        private DevExpress.XtraBars.BarButtonItem barSubCheck;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbIssueBy;
        private DevExpress.XtraEditors.TextEdit txtHBLNO;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraBars.BarSubItem barSubPrint;
        private DevExpress.XtraBars.BarButtonItem barPrintLoadGoods;
        private DevExpress.XtraEditors.LabelControl labAgentOfCarrier;
        private DevExpress.XtraEditors.ButtonEdit stxtAgentOfCarrier;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtportto1;
        private DevExpress.XtraEditors.LabelControl labToBy1;
        private DevExpress.XtraEditors.TextEdit txtBy1;
        private DevExpress.XtraEditors.TextEdit txtBy2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtportto2;
        private DevExpress.XtraEditors.LabelControl labToBy2;
        private DevExpress.XtraEditors.LabelControl labBuyRate;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbBuyCur;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.SpinEdit txtBuyAmount;
        private DevExpress.XtraEditors.SpinEdit txtBuyPrice;
        private DevExpress.XtraEditors.LabelControl labGoodsDescription;
        private DevExpress.XtraEditors.RadioGroup radioGroup2;
        private DevExpress.XtraEditors.SpinEdit txtIATAAmount;
        private DevExpress.XtraEditors.SpinEdit txtIATAPrice;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbIATACur;
        private DevExpress.XtraEditors.LabelControl labIATARate;
        private DevExpress.XtraEditors.LabelControl labMarks;
        private DevExpress.XtraEditors.MemoEdit txtGoodsDescription;
        private DevExpress.XtraEditors.LabelControl labHandlingInfomation;
        private DevExpress.XtraEditors.MemoEdit txtMarks;
        private DevExpress.XtraEditors.MemoEdit txtHandingInfo;
        private DevExpress.XtraEditors.DateEdit dteBookingDate;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbReleaseType;
        private DevExpress.XtraEditors.LabelControl labReleaseType;
        private DevExpress.XtraEditors.LabelControl labReleaseDate;
        private DevExpress.XtraEditors.MemoEdit txtOtherChargers;
        private DevExpress.XtraEditors.TextEdit txtBy3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtportto3;
        private DevExpress.XtraEditors.LabelControl labToBy3;
        private DevExpress.XtraEditors.TextEdit txtShipperAccountNo;
        private DevExpress.XtraEditors.LabelControl labShipperAccount;
        private DevExpress.XtraEditors.TextEdit txtConsigneeAccountNo;
        private DevExpress.XtraEditors.LabelControl labConsigneeAccountNo;
        private DevExpress.XtraEditors.TextEdit txtAgentAccountNo;
        private DevExpress.XtraEditors.LabelControl labAgentAccountNo;
        private DevExpress.XtraEditors.TextEdit txtIATACode;
        private DevExpress.XtraEditors.LabelControl labIATACode;
        private GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl labNotifyParty;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtNotifyParty;
        private DevExpress.XtraEditors.LabelControl labMeasurement;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMeasurementUnit;
        private DevExpress.XtraEditors.SpinEdit spinEdit7;
        private DevExpress.XtraEditors.TextEdit txtInsurance;
        private DevExpress.XtraEditors.TextEdit txtCustoms;
        private DevExpress.XtraEditors.LabelControl labInsurance;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbOther;
        private DevExpress.XtraEditors.LabelControl labDCLCustoms;
        private DevExpress.XtraEditors.LabelControl labOther;
        private DevExpress.XtraEditors.LabelControl labPaymentTerm;
        private DevExpress.XtraEditors.TextEdit txtCarriage;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbPaymentTerm;
        private DevExpress.XtraEditors.LabelControl labDCLCarriage;
        private DevExpress.XtraEditors.SpinEdit txtGLBS;
        private DevExpress.XtraEditors.SpinEdit txtChLBS;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit txtChKGS;
        private DevExpress.XtraEditors.SpinEdit txtGKGS;
        private DevExpress.XtraEditors.SpinEdit numQuantity;
        private DevExpress.XtraEditors.LabelControl labQuantity;
        private DevExpress.XtraEditors.LabelControl labWeight;
        private DevExpress.XtraEditors.LabelControl labChargeKGSLBS;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbQuantityUnit;
        private DevExpress.XtraEditors.LabelControl labCarrierChargers;
        private DevExpress.XtraEditors.LabelControl labAgentChargers;
        private DevExpress.XtraEditors.LabelControl labFlightNo;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox cmbFlightNo;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTarifflevel;
        private DevExpress.XtraEditors.LabelControl labTarifflevel;
        private DevExpress.XtraEditors.LabelControl labTax;
        private DevExpress.XtraEditors.LabelControl labValuationCharge;
        private DevExpress.XtraEditors.TextEdit txtChargesDestination;
        private DevExpress.XtraEditors.LabelControl labChargesDestination;
        private DevExpress.XtraEditors.TextEdit txtCCCharges;
        private DevExpress.XtraEditors.LabelControl labCCCharges;
        private DevExpress.XtraEditors.TextEdit txtCurrencyConversionRate;
        private DevExpress.XtraEditors.LabelControl labConversionRates;
        private BindingSource OtherChargesBindingSource;
        private DevExpress.XtraEditors.SpinEdit txtCarrierChargers;
        private DevExpress.XtraEditors.SpinEdit txtTax;
        private DevExpress.XtraEditors.SpinEdit txtValuationCharge;
        private DevExpress.XtraEditors.SpinEdit txtAgentChargers;
        private GroupBox groupBox2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colItem;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barDelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbAirCompany;
        private DevExpress.XtraEditors.LabelControl labAirCompany;
        private DevExpress.XtraEditors.LabelControl labPlaceOfDelivery;
        private DevExpress.XtraEditors.ButtonEdit stxtPlaceOfDelivery;
        private DevExpress.XtraEditors.TextEdit txtPlaceOfDeliveryName;
        private DevExpress.XtraBars.BarButtonItem barBill;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
    }
}
