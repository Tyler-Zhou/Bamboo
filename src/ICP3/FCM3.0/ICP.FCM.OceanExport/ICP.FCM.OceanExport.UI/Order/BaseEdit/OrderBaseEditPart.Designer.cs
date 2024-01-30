using System.Windows.Forms;
namespace ICP.FCM.OceanExport.UI.Order
{
    partial class OrderBaseEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderBaseEditPart));
            this.bsOrderInfo = new System.Windows.Forms.BindingSource(this.components);
            this.dteOrderDate = new DevExpress.XtraEditors.DateEdit();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.stxtPOD = new DevExpress.XtraEditors.ButtonEdit();
            this.dteClosingDate = new DevExpress.XtraEditors.DateEdit();
            this.dteEstimatedDeliveryDate = new DevExpress.XtraEditors.DateEdit();
            this.dteExpectedShipDate = new DevExpress.XtraEditors.DateEdit();
            this.dteExpectedArriveDate = new DevExpress.XtraEditors.DateEdit();
            this.stxtPlaceOfDelivery = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtConsignee = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtPOL = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtShipper = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtBookingCustomer = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.txtCargoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtMBLRequirements = new DevExpress.XtraEditors.MemoEdit();
            this.txtHBLRequirements = new DevExpress.XtraEditors.MemoEdit();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.chkIsOnlyMBL = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsWarehouse = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsQuarantineInspection = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsCommodityInspection = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsCustoms = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsTruck = new DevExpress.XtraEditors.CheckEdit();
            this.stxtPlaceOfReceipt = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtCustomer = new DevExpress.XtraEditors.PopupContainerEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.gridControl1 = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsOrders = new System.Windows.Forms.BindingSource(this.components);
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
            this.txtState = new DevExpress.XtraEditors.TextEdit();
            this.txtCommodity = new DevExpress.XtraEditors.MemoEdit();
            this.mcmbSales = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.mcmbBookinger = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.mcmOverseasFiler = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbTransportClause = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbTradeTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbWeightUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbMeasurementUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbQuantityUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbSalesType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbPaymentTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbMBLPaymentTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbHBLPaymentTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbBookingMode = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtAgent = new DevExpress.XtraEditors.TextEdit();
            this.cmbMBLReleaseType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbHBLReleaseType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbCargoType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.containerDemandControl1 = new ICP.Framework.ClientComponents.Controls.ContainerDemandControl();
            this.tabPagePO = new DevExpress.XtraTab.XtraTabPage();
            this.orderPOEditPart1 = new ICP.FCM.OceanExport.UI.Order.OrderPOEditPart();
            this.orderFeeEditPart1 = new ICP.FCM.OceanExport.UI.Order.OrderFeeEditPart();
            this.tabPageBase = new DevExpress.XtraTab.XtraTabPage();
            this.panelScroll = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarBaseInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labState = new DevExpress.XtraEditors.LabelControl();
            this.labAbroadOP = new DevExpress.XtraEditors.LabelControl();
            this.trsSalesDep = new ICP.Framework.ClientComponents.Controls.TreeSelectBox();
            this.labPaymentTerm = new DevExpress.XtraEditors.LabelControl();
            this.labTransportClause = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.labTradeTerm = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labOrderDate = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labSales = new DevExpress.XtraEditors.LabelControl();
            this.labSalesType = new DevExpress.XtraEditors.LabelControl();
            this.labSalesDepartment = new DevExpress.XtraEditors.LabelControl();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.labBookingMode = new DevExpress.XtraEditors.LabelControl();
            this.labOperatorName = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mcmbCarrier = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.groupHBL = new System.Windows.Forms.GroupBox();
            this.labHBLPaymentTerm = new DevExpress.XtraEditors.LabelControl();
            this.labHBLReleaseType = new DevExpress.XtraEditors.LabelControl();
            this.groupMBL = new System.Windows.Forms.GroupBox();
            this.labMBLPaymentTerm = new DevExpress.XtraEditors.LabelControl();
            this.labMBLReleaseType = new DevExpress.XtraEditors.LabelControl();
            this.labAgent = new DevExpress.XtraEditors.LabelControl();
            this.groupLocalService = new System.Windows.Forms.GroupBox();
            this.labExpectedArriveDate = new DevExpress.XtraEditors.LabelControl();
            this.labClosingDate = new DevExpress.XtraEditors.LabelControl();
            this.labExpectedShipDate = new DevExpress.XtraEditors.LabelControl();
            this.labEstimatedDeliveryDate = new DevExpress.XtraEditors.LabelControl();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.stxtFinalDestination = new DevExpress.XtraEditors.ButtonEdit();
            this.labFinalDestination = new DevExpress.XtraEditors.LabelControl();
            this.labCommodity = new DevExpress.XtraEditors.LabelControl();
            this.cargoDescriptionPart1 = new ICP.FCM.OceanExport.UI.Common.CargoDescriptionPart();
            this.labCargoType = new DevExpress.XtraEditors.LabelControl();
            this.labBookingCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labShipper = new DevExpress.XtraEditors.LabelControl();
            this.labQuantity = new DevExpress.XtraEditors.LabelControl();
            this.labPlaceOfReceipt = new DevExpress.XtraEditors.LabelControl();
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.labWeight = new DevExpress.XtraEditors.LabelControl();
            this.labConsignee = new DevExpress.XtraEditors.LabelControl();
            this.labPlaceOfDelivery = new DevExpress.XtraEditors.LabelControl();
            this.labMeasurement = new DevExpress.XtraEditors.LabelControl();
            this.numQuantity = new DevExpress.XtraEditors.SpinEdit();
            this.numWeight = new DevExpress.XtraEditors.SpinEdit();
            this.numMeasurement = new DevExpress.XtraEditors.SpinEdit();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.groupRemark = new System.Windows.Forms.GroupBox();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navBarDelegateInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navFee = new DevExpress.XtraNavBar.NavBarGroup();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.bsOrderInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteOrderDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteOrderDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEstimatedDeliveryDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEstimatedDeliveryDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedShipDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedShipDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedArriveDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedArriveDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCargoDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMBLRequirements.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHBLRequirements.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOnlyMBL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsWarehouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsQuarantineInspection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCommodityInspection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCustoms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTruck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfReceipt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQtyUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditFloat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbSales.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbBookinger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmOverseasFiler.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTradeTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuantityUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMBLPaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLPaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBookingMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMBLReleaseType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLReleaseType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCargoType.Properties)).BeginInit();
            this.tabPagePO.SuspendLayout();
            this.tabPageBase.SuspendLayout();
            this.panelScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.navBarGroupControlContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupHBL.SuspendLayout();
            this.groupMBL.SuspendLayout();
            this.groupLocalService.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtFinalDestination.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurement.Properties)).BeginInit();
            this.groupRemark.SuspendLayout();
            this.navBarGroupControlContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bsOrderInfo
            // 
            this.bsOrderInfo.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanOrderInfo);
            // 
            // dteOrderDate
            // 
            this.dteOrderDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "BookingDate", true));
            this.dteOrderDate.EditValue = null;
            this.dteOrderDate.Location = new System.Drawing.Point(687, 85);
            this.dteOrderDate.Name = "dteOrderDate";
            this.dteOrderDate.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.dteOrderDate.Properties.Appearance.Options.UseBackColor = true;
            this.dteOrderDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteOrderDate.Properties.Mask.EditMask = "";
            this.dteOrderDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteOrderDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteOrderDate.Size = new System.Drawing.Size(102, 21);
            this.dteOrderDate.TabIndex = 140;
            // 
            // txtNo
            // 
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "RefNo", true));
            this.txtNo.EditValue = "";
            this.txtNo.Location = new System.Drawing.Point(101, 3);
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.ReadOnly = true;
            this.txtNo.Size = new System.Drawing.Size(119, 21);
            this.txtNo.TabIndex = 0;
            this.txtNo.TabStop = false;
            // 
            // stxtPOD
            // 
            this.stxtPOD.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOrderInfo, "PODID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPOD.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "PODName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPOD.Location = new System.Drawing.Point(101, 136);
            this.stxtPOD.Name = "stxtPOD";
            this.stxtPOD.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtPOD.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPOD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPOD.Size = new System.Drawing.Size(283, 21);
            this.stxtPOD.TabIndex = 200;
            // 
            // dteClosingDate
            // 
            this.dteClosingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "ClosingDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteClosingDate.EditValue = null;
            this.dteClosingDate.Location = new System.Drawing.Point(86, 53);
            this.dteClosingDate.Name = "dteClosingDate";
            this.dteClosingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteClosingDate.Properties.Mask.EditMask = "";
            this.dteClosingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteClosingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteClosingDate.Size = new System.Drawing.Size(103, 21);
            this.dteClosingDate.TabIndex = 330;
            // 
            // dteEstimatedDeliveryDate
            // 
            this.dteEstimatedDeliveryDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "EstimatedDeliveryDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteEstimatedDeliveryDate.EditValue = null;
            this.dteEstimatedDeliveryDate.Location = new System.Drawing.Point(86, 79);
            this.dteEstimatedDeliveryDate.Name = "dteEstimatedDeliveryDate";
            this.dteEstimatedDeliveryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEstimatedDeliveryDate.Properties.Mask.EditMask = "";
            this.dteEstimatedDeliveryDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteEstimatedDeliveryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteEstimatedDeliveryDate.Size = new System.Drawing.Size(103, 21);
            this.dteEstimatedDeliveryDate.TabIndex = 350;
            // 
            // dteExpectedShipDate
            // 
            this.dteExpectedShipDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "ExpectedShipDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteExpectedShipDate.EditValue = null;
            this.dteExpectedShipDate.Location = new System.Drawing.Point(286, 53);
            this.dteExpectedShipDate.Name = "dteExpectedShipDate";
            this.dteExpectedShipDate.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.dteExpectedShipDate.Properties.Appearance.Options.UseBackColor = true;
            this.dteExpectedShipDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteExpectedShipDate.Properties.Mask.EditMask = "";
            this.dteExpectedShipDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteExpectedShipDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteExpectedShipDate.Size = new System.Drawing.Size(103, 21);
            this.dteExpectedShipDate.TabIndex = 340;
            // 
            // dteExpectedArriveDate
            // 
            this.dteExpectedArriveDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "ExpectedArriveDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteExpectedArriveDate.EditValue = null;
            this.dteExpectedArriveDate.Location = new System.Drawing.Point(286, 80);
            this.dteExpectedArriveDate.Name = "dteExpectedArriveDate";
            this.dteExpectedArriveDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteExpectedArriveDate.Properties.Mask.EditMask = "";
            this.dteExpectedArriveDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteExpectedArriveDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteExpectedArriveDate.Size = new System.Drawing.Size(103, 21);
            this.dteExpectedArriveDate.TabIndex = 360;
            // 
            // stxtPlaceOfDelivery
            // 
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "PlaceOfDeliveryName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOrderInfo, "PlaceOfDeliveryID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPlaceOfDelivery.Location = new System.Drawing.Point(101, 163);
            this.stxtPlaceOfDelivery.Name = "stxtPlaceOfDelivery";
            this.stxtPlaceOfDelivery.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtPlaceOfDelivery.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlaceOfDelivery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlaceOfDelivery.Size = new System.Drawing.Size(283, 21);
            this.stxtPlaceOfDelivery.TabIndex = 210;
            // 
            // stxtConsignee
            // 
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOrderInfo, "ConsigneeID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "ConsigneeName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtConsignee.Location = new System.Drawing.Point(101, 55);
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
            this.stxtConsignee.TabIndex = 170;
            // 
            // stxtPOL
            // 
            this.stxtPOL.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOrderInfo, "POLID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPOL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "POLName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPOL.Location = new System.Drawing.Point(101, 109);
            this.stxtPOL.Name = "stxtPOL";
            this.stxtPOL.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtPOL.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPOL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPOL.Size = new System.Drawing.Size(283, 21);
            this.stxtPOL.TabIndex = 190;
            // 
            // stxtShipper
            // 
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOrderInfo, "ShipperID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "ShipperName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtShipper.Location = new System.Drawing.Point(101, 28);
            this.stxtShipper.Name = "stxtShipper";
            this.stxtShipper.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtShipper.Properties.ActionButtonIndex = 1;
            this.stxtShipper.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtShipper.Properties.Appearance.Options.UseBackColor = true;
            this.stxtShipper.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtShipper.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtShipper.Properties.PopupSizeable = false;
            this.stxtShipper.Properties.ShowPopupCloseButton = false;
            this.stxtShipper.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtShipper.Size = new System.Drawing.Size(283, 21);
            this.stxtShipper.TabIndex = 160;
            // 
            // stxtBookingCustomer
            // 
            this.stxtBookingCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOrderInfo, "BookingCustomerID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtBookingCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "BookingCustomerName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtBookingCustomer.Location = new System.Drawing.Point(101, 1);
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
            this.stxtBookingCustomer.Size = new System.Drawing.Size(283, 21);
            this.stxtBookingCustomer.TabIndex = 150;
            // 
            // txtCargoDescription
            // 
            this.txtCargoDescription.Location = new System.Drawing.Point(250, 271);
            this.txtCargoDescription.Name = "txtCargoDescription";
            this.txtCargoDescription.Properties.ReadOnly = true;
            this.txtCargoDescription.Size = new System.Drawing.Size(134, 48);
            this.txtCargoDescription.TabIndex = 15;
            this.txtCargoDescription.TabStop = false;
            // 
            // txtMBLRequirements
            // 
            this.txtMBLRequirements.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "MBLRequirements", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtMBLRequirements.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMBLRequirements.Location = new System.Drawing.Point(3, 68);
            this.txtMBLRequirements.Name = "txtMBLRequirements";
            this.txtMBLRequirements.Size = new System.Drawing.Size(183, 124);
            this.txtMBLRequirements.TabIndex = 450;
            // 
            // txtHBLRequirements
            // 
            this.txtHBLRequirements.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "HBLRequirements", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtHBLRequirements.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtHBLRequirements.Location = new System.Drawing.Point(3, 68);
            this.txtHBLRequirements.Name = "txtHBLRequirements";
            this.txtHBLRequirements.Size = new System.Drawing.Size(191, 124);
            this.txtHBLRequirements.TabIndex = 480;
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "Remark", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemark.Location = new System.Drawing.Point(3, 18);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(795, 79);
            this.txtRemark.TabIndex = 490;
            // 
            // chkIsOnlyMBL
            // 
            this.chkIsOnlyMBL.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "IsOnlyMBL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsOnlyMBL.Location = new System.Drawing.Point(328, 121);
            this.chkIsOnlyMBL.Name = "chkIsOnlyMBL";
            this.chkIsOnlyMBL.Properties.Caption = "OnlyMBL";
            this.chkIsOnlyMBL.Size = new System.Drawing.Size(75, 19);
            this.chkIsOnlyMBL.TabIndex = 420;
            // 
            // chkIsWarehouse
            // 
            this.chkIsWarehouse.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "IsWarehouse", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsWarehouse.Location = new System.Drawing.Point(249, 17);
            this.chkIsWarehouse.Name = "chkIsWarehouse";
            this.chkIsWarehouse.Properties.Caption = "Warehouse";
            this.chkIsWarehouse.Size = new System.Drawing.Size(75, 19);
            this.chkIsWarehouse.TabIndex = 410;
            // 
            // chkIsQuarantineInspection
            // 
            this.chkIsQuarantineInspection.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "IsQuarantineInspection", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsQuarantineInspection.Location = new System.Drawing.Point(186, 17);
            this.chkIsQuarantineInspection.Name = "chkIsQuarantineInspection";
            this.chkIsQuarantineInspection.Properties.Caption = "QuarantineInspection";
            this.chkIsQuarantineInspection.Size = new System.Drawing.Size(75, 19);
            this.chkIsQuarantineInspection.TabIndex = 400;
            // 
            // chkIsCommodityInspection
            // 
            this.chkIsCommodityInspection.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "IsCommodityInspection", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsCommodityInspection.Location = new System.Drawing.Point(124, 17);
            this.chkIsCommodityInspection.Name = "chkIsCommodityInspection";
            this.chkIsCommodityInspection.Properties.Caption = "CommodityInspection";
            this.chkIsCommodityInspection.Size = new System.Drawing.Size(75, 19);
            this.chkIsCommodityInspection.TabIndex = 390;
            // 
            // chkIsCustoms
            // 
            this.chkIsCustoms.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "IsCustoms", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsCustoms.Location = new System.Drawing.Point(68, 17);
            this.chkIsCustoms.Name = "chkIsCustoms";
            this.chkIsCustoms.Properties.Caption = "Customs";
            this.chkIsCustoms.Size = new System.Drawing.Size(75, 19);
            this.chkIsCustoms.TabIndex = 380;
            // 
            // chkIsTruck
            // 
            this.chkIsTruck.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "IsTruck", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsTruck.Location = new System.Drawing.Point(9, 17);
            this.chkIsTruck.Name = "chkIsTruck";
            this.chkIsTruck.Properties.Caption = "Truck";
            this.chkIsTruck.Size = new System.Drawing.Size(75, 19);
            this.chkIsTruck.TabIndex = 370;
            // 
            // stxtPlaceOfReceipt
            // 
            this.stxtPlaceOfReceipt.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "PlaceOfReceiptName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPlaceOfReceipt.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOrderInfo, "PlaceOfReceiptID", true));
            this.stxtPlaceOfReceipt.Location = new System.Drawing.Point(101, 82);
            this.stxtPlaceOfReceipt.Name = "stxtPlaceOfReceipt";
            this.stxtPlaceOfReceipt.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtPlaceOfReceipt.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlaceOfReceipt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlaceOfReceipt.Size = new System.Drawing.Size(283, 21);
            this.stxtPlaceOfReceipt.TabIndex = 180;
            // 
            // stxtCustomer
            // 
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOrderInfo, "CustomerID", true));
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "CustomerName", true));
            this.stxtCustomer.Location = new System.Drawing.Point(101, 59);
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
            this.stxtCustomer.Size = new System.Drawing.Size(286, 21);
            toolTipTitleItem1.Text = "提示";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "没有业务";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.stxtCustomer.SuperTip = superToolTip1;
            this.stxtCustomer.TabIndex = 40;
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
            this.barPrint,
            this.barClose,
            this.barRefresh});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 7;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSaveAs, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Save_Blue_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barSaveAs
            // 
            this.barSaveAs.Caption = "Save&As";
            this.barSaveAs.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Save_16;
            this.barSaveAs.Id = 1;
            this.barSaveAs.Name = "barSaveAs";
            this.barSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSaveAs_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "&Print";
            this.barPrint.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Print_16;
            this.barPrint.Id = 2;
            this.barPrint.Name = "barPrint";
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Refresh_161;
            this.barRefresh.Id = 6;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Left_D_16;
            this.barClose.Id = 5;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(862, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 880);
            this.barDockControlBottom.Size = new System.Drawing.Size(862, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 854);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(862, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 854);
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.gridControl1);
            this.popupContainerControl1.Location = new System.Drawing.Point(101, 114);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(653, 178);
            this.popupContainerControl1.TabIndex = 12;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bsOrders;
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
            // bsOrders
            // 
            this.bsOrders.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanOrderList);
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
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(286, 3);
            this.txtState.Name = "txtState";
            this.txtState.Properties.ReadOnly = true;
            this.txtState.Size = new System.Drawing.Size(101, 21);
            this.txtState.TabIndex = 1;
            this.txtState.TabStop = false;
            // 
            // txtCommodity
            // 
            this.txtCommodity.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "Commodity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCommodity.Location = new System.Drawing.Point(101, 217);
            this.txtCommodity.Name = "txtCommodity";
            this.txtCommodity.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtCommodity.Properties.Appearance.Options.UseBackColor = true;
            this.txtCommodity.Size = new System.Drawing.Size(283, 21);
            this.txtCommodity.TabIndex = 230;
            // 
            // mcmbSales
            // 
            this.mcmbSales.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "SalesID", true));
            this.mcmbSales.Enabled = false;
            this.mcmbSales.Location = new System.Drawing.Point(488, 4);
            this.mcmbSales.Name = "mcmbSales";
            this.mcmbSales.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.mcmbSales.Properties.Appearance.Options.UseBackColor = true;
            this.mcmbSales.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mcmbSales.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.mcmbSales.Size = new System.Drawing.Size(103, 21);
            this.mcmbSales.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbSales.TabIndex = 70;
            // 
            // mcmbBookinger
            // 
            this.mcmbBookinger.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "BookingerID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mcmbBookinger.Location = new System.Drawing.Point(687, 31);
            this.mcmbBookinger.Name = "mcmbBookinger";
            this.mcmbBookinger.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.mcmbBookinger.Properties.Appearance.Options.UseBackColor = true;
            this.mcmbBookinger.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mcmbBookinger.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.mcmbBookinger.Size = new System.Drawing.Size(102, 21);
            this.mcmbBookinger.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbBookinger.TabIndex = 100;
            // 
            // mcmOverseasFiler
            // 
            this.mcmOverseasFiler.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "OverSeasFilerId", true));
            this.mcmOverseasFiler.Location = new System.Drawing.Point(687, 5);
            this.mcmOverseasFiler.Name = "mcmOverseasFiler";
            this.mcmOverseasFiler.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.mcmOverseasFiler.Properties.Appearance.Options.UseBackColor = true;
            this.mcmOverseasFiler.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mcmOverseasFiler.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.mcmOverseasFiler.Size = new System.Drawing.Size(102, 21);
            this.mcmOverseasFiler.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmOverseasFiler.TabIndex = 80;
            // 
            // cmbCompany
            // 
            this.cmbCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "CompanyID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbCompany.Location = new System.Drawing.Point(101, 31);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCompany.Size = new System.Drawing.Size(119, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbCompany.TabIndex = 20;
            // 
            // cmbTransportClause
            // 
            this.cmbTransportClause.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "TransportClauseID", true));
            this.cmbTransportClause.Location = new System.Drawing.Point(488, 58);
            this.cmbTransportClause.Name = "cmbTransportClause";
            this.cmbTransportClause.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbTransportClause.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTransportClause.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTransportClause.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbTransportClause.Size = new System.Drawing.Size(103, 21);
            this.cmbTransportClause.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbTransportClause.TabIndex = 110;
            // 
            // cmbTradeTerm
            // 
            this.cmbTradeTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "TradeTermID", true));
            this.cmbTradeTerm.Location = new System.Drawing.Point(101, 86);
            this.cmbTradeTerm.Name = "cmbTradeTerm";
            this.cmbTradeTerm.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbTradeTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTradeTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTradeTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbTradeTerm.Size = new System.Drawing.Size(99, 21);
            this.cmbTradeTerm.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbTradeTerm.TabIndex = 50;
            // 
            // cmbWeightUnit
            // 
            this.cmbWeightUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "WeightUnitID", true));
            this.cmbWeightUnit.Location = new System.Drawing.Point(174, 272);
            this.cmbWeightUnit.Name = "cmbWeightUnit";
            this.cmbWeightUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbWeightUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbWeightUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWeightUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbWeightUnit.Size = new System.Drawing.Size(52, 21);
            this.cmbWeightUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbWeightUnit.TabIndex = 270;
            // 
            // cmbMeasurementUnit
            // 
            this.cmbMeasurementUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "MeasurementUnitID", true));
            this.cmbMeasurementUnit.Location = new System.Drawing.Point(174, 298);
            this.cmbMeasurementUnit.Name = "cmbMeasurementUnit";
            this.cmbMeasurementUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbMeasurementUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMeasurementUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMeasurementUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbMeasurementUnit.Size = new System.Drawing.Size(52, 21);
            this.cmbMeasurementUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbMeasurementUnit.TabIndex = 290;
            // 
            // cmbQuantityUnit
            // 
            this.cmbQuantityUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "QuantityUnitID", true));
            this.cmbQuantityUnit.Location = new System.Drawing.Point(174, 244);
            this.cmbQuantityUnit.Name = "cmbQuantityUnit";
            this.cmbQuantityUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbQuantityUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbQuantityUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQuantityUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbQuantityUnit.Size = new System.Drawing.Size(52, 21);
            this.cmbQuantityUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbQuantityUnit.TabIndex = 250;
            // 
            // cmbSalesType
            // 
            this.cmbSalesType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "SalesTypeID", true));
            this.cmbSalesType.Location = new System.Drawing.Point(286, 85);
            this.cmbSalesType.Name = "cmbSalesType";
            this.cmbSalesType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbSalesType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbSalesType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSalesType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbSalesType.Size = new System.Drawing.Size(101, 21);
            this.cmbSalesType.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbSalesType.TabIndex = 60;
            // 
            // cmbPaymentTerm
            // 
            this.cmbPaymentTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "PaymentTermID", true));
            this.cmbPaymentTerm.Location = new System.Drawing.Point(488, 85);
            this.cmbPaymentTerm.Name = "cmbPaymentTerm";
            this.cmbPaymentTerm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbPaymentTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbPaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaymentTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbPaymentTerm.Size = new System.Drawing.Size(103, 21);
            this.cmbPaymentTerm.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbPaymentTerm.TabIndex = 130;
            // 
            // cmbMBLPaymentTerm
            // 
            this.cmbMBLPaymentTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "MBLPaymentTermID", true));
            this.cmbMBLPaymentTerm.Location = new System.Drawing.Point(84, 14);
            this.cmbMBLPaymentTerm.Name = "cmbMBLPaymentTerm";
            this.cmbMBLPaymentTerm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbMBLPaymentTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMBLPaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMBLPaymentTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbMBLPaymentTerm.Size = new System.Drawing.Size(103, 21);
            this.cmbMBLPaymentTerm.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbMBLPaymentTerm.TabIndex = 430;
            // 
            // cmbHBLPaymentTerm
            // 
            this.cmbHBLPaymentTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "HBLPaymentTermID", true));
            this.cmbHBLPaymentTerm.Location = new System.Drawing.Point(89, 15);
            this.cmbHBLPaymentTerm.Name = "cmbHBLPaymentTerm";
            this.cmbHBLPaymentTerm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbHBLPaymentTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbHBLPaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHBLPaymentTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbHBLPaymentTerm.Size = new System.Drawing.Size(103, 21);
            this.cmbHBLPaymentTerm.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbHBLPaymentTerm.TabIndex = 460;
            // 
            // cmbType
            // 
            this.cmbType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "OEOperationType", true));
            this.cmbType.Location = new System.Drawing.Point(286, 32);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbType.Size = new System.Drawing.Size(101, 21);
            this.cmbType.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbType.TabIndex = 30;
            // 
            // cmbBookingMode
            // 
            this.cmbBookingMode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "BookingMode", true));
            this.cmbBookingMode.Location = new System.Drawing.Point(687, 58);
            this.cmbBookingMode.Name = "cmbBookingMode";
            this.cmbBookingMode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbBookingMode.Properties.Appearance.Options.UseBackColor = true;
            this.cmbBookingMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBookingMode.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbBookingMode.Size = new System.Drawing.Size(102, 21);
            this.cmbBookingMode.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbBookingMode.TabIndex = 120;
            // 
            // txtAgent
            // 
            this.txtAgent.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "AgentName", true));
            this.txtAgent.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOrderInfo, "AgentID", true));
            this.txtAgent.Location = new System.Drawing.Point(86, 3);
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.Properties.ReadOnly = true;
            this.txtAgent.Size = new System.Drawing.Size(303, 21);
            this.txtAgent.TabIndex = 41;
            this.txtAgent.TabStop = false;
            // 
            // cmbMBLReleaseType
            // 
            this.cmbMBLReleaseType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "MBLReleaseType", true));
            this.cmbMBLReleaseType.Location = new System.Drawing.Point(84, 42);
            this.cmbMBLReleaseType.Name = "cmbMBLReleaseType";
            this.cmbMBLReleaseType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbMBLReleaseType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMBLReleaseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMBLReleaseType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbMBLReleaseType.Size = new System.Drawing.Size(103, 21);
            this.cmbMBLReleaseType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbMBLReleaseType.TabIndex = 440;
            // 
            // cmbHBLReleaseType
            // 
            this.cmbHBLReleaseType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "HBLReleaseType", true));
            this.cmbHBLReleaseType.Location = new System.Drawing.Point(88, 42);
            this.cmbHBLReleaseType.Name = "cmbHBLReleaseType";
            this.cmbHBLReleaseType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbHBLReleaseType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbHBLReleaseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHBLReleaseType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbHBLReleaseType.Size = new System.Drawing.Size(103, 21);
            this.cmbHBLReleaseType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbHBLReleaseType.TabIndex = 470;
            // 
            // cmbCargoType
            // 
            this.cmbCargoType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "CargoType", true));
            this.cmbCargoType.Location = new System.Drawing.Point(315, 244);
            this.cmbCargoType.Name = "cmbCargoType";
            this.cmbCargoType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCargoType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCargoType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCargoType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCargoType.Size = new System.Drawing.Size(72, 21);
            this.cmbCargoType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCargoType.TabIndex = 300;
            // 
            // containerDemandControl1
            // 
            this.containerDemandControl1.Location = new System.Drawing.Point(3, 323);
            this.containerDemandControl1.Name = "containerDemandControl1";
            this.containerDemandControl1.Size = new System.Drawing.Size(381, 21);
            this.containerDemandControl1.SpecifiedBackColor = System.Drawing.Color.White;
            this.containerDemandControl1.TabIndex = 310;
            // 
            // tabPagePO
            // 
            this.tabPagePO.Controls.Add(this.orderPOEditPart1);
            this.tabPagePO.Name = "tabPagePO";
            this.tabPagePO.Size = new System.Drawing.Size(832, 847);
            this.tabPagePO.Text = "PO";
            // 
            // orderPOEditPart1
            // 
            this.orderPOEditPart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.orderPOEditPart1.IsDesignMode = true;
            this.orderPOEditPart1.IsOrderPO = false;
            this.orderPOEditPart1.Location = new System.Drawing.Point(0, 0);
            this.orderPOEditPart1.Name = "orderPOEditPart1";
            this.orderPOEditPart1.OrderId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.orderPOEditPart1.Size = new System.Drawing.Size(805, 843);
            this.orderPOEditPart1.TabIndex = 0;
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
            this.orderFeeEditPart1.Size = new System.Drawing.Size(801, 183);
            this.orderFeeEditPart1.TabIndex = 0;
            this.orderFeeEditPart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("orderFeeEditPart1.UsedMessages")));
            // 
            // tabPageBase
            // 
            this.tabPageBase.Controls.Add(this.panelScroll);
            this.tabPageBase.Name = "tabPageBase";
            this.tabPageBase.Size = new System.Drawing.Size(832, 847);
            this.tabPageBase.Text = "Base Info";
            // 
            // panelScroll
            // 
            this.panelScroll.Controls.Add(this.navBarControl1);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.Location = new System.Drawing.Point(0, 0);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(832, 847);
            this.panelScroll.TabIndex = 5;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarBaseInfo;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarBaseInfo,
            this.navBarDelegateInfo,
            this.navFee});
            this.navBarControl1.Location = new System.Drawing.Point(3, 4);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 810;
            this.navBarControl1.Size = new System.Drawing.Size(805, 840);
            this.navBarControl1.TabIndex = 3;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarBaseInfo
            // 
            this.navBarBaseInfo.Caption = "Base Info";
            this.navBarBaseInfo.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarBaseInfo.Expanded = true;
            this.navBarBaseInfo.GroupClientHeight = 115;
            this.navBarBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBaseInfo.Name = "navBarBaseInfo";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.cmbBookingMode);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbType);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbPaymentTerm);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbSalesType);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbTradeTerm);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbTransportClause);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.mcmOverseasFiler);
            this.navBarGroupControlContainer1.Controls.Add(this.mcmbBookinger);
            this.navBarGroupControlContainer1.Controls.Add(this.mcmbSales);
            this.navBarGroupControlContainer1.Controls.Add(this.txtState);
            this.navBarGroupControlContainer1.Controls.Add(this.labState);
            this.navBarGroupControlContainer1.Controls.Add(this.labAbroadOP);
            this.navBarGroupControlContainer1.Controls.Add(this.trsSalesDep);
            this.navBarGroupControlContainer1.Controls.Add(this.labPaymentTerm);
            this.navBarGroupControlContainer1.Controls.Add(this.popupContainerControl1);
            this.navBarGroupControlContainer1.Controls.Add(this.stxtCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.labTransportClause);
            this.navBarGroupControlContainer1.Controls.Add(this.labNo);
            this.navBarGroupControlContainer1.Controls.Add(this.dteOrderDate);
            this.navBarGroupControlContainer1.Controls.Add(this.txtNo);
            this.navBarGroupControlContainer1.Controls.Add(this.labTradeTerm);
            this.navBarGroupControlContainer1.Controls.Add(this.labCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.labOrderDate);
            this.navBarGroupControlContainer1.Controls.Add(this.labCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.labSales);
            this.navBarGroupControlContainer1.Controls.Add(this.labSalesType);
            this.navBarGroupControlContainer1.Controls.Add(this.labSalesDepartment);
            this.navBarGroupControlContainer1.Controls.Add(this.labType);
            this.navBarGroupControlContainer1.Controls.Add(this.labBookingMode);
            this.navBarGroupControlContainer1.Controls.Add(this.labOperatorName);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(801, 113);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // labState
            // 
            this.labState.Location = new System.Drawing.Point(226, 7);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(30, 14);
            this.labState.TabIndex = 39;
            this.labState.Text = "State";
            // 
            // labAbroadOP
            // 
            this.labAbroadOP.Location = new System.Drawing.Point(607, 8);
            this.labAbroadOP.Name = "labAbroadOP";
            this.labAbroadOP.Size = new System.Drawing.Size(59, 14);
            this.labAbroadOP.TabIndex = 37;
            this.labAbroadOP.Text = "Abroad OP";
            // 
            // trsSalesDep
            // 
            this.trsSalesDep.AllText = "Selecte ALL";
            this.trsSalesDep.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "SalesDepartmentID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trsSalesDep.EditValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.trsSalesDep.Location = new System.Drawing.Point(488, 31);
            this.trsSalesDep.Name = "trsSalesDep";
            this.trsSalesDep.ReadOnly = false;
            this.trsSalesDep.Size = new System.Drawing.Size(103, 21);
            this.trsSalesDep.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.trsSalesDep.TabIndex = 90;
            // 
            // labPaymentTerm
            // 
            this.labPaymentTerm.Location = new System.Drawing.Point(402, 88);
            this.labPaymentTerm.Name = "labPaymentTerm";
            this.labPaymentTerm.Size = new System.Drawing.Size(77, 14);
            this.labPaymentTerm.TabIndex = 0;
            this.labPaymentTerm.Text = "PaymentTerm";
            // 
            // labTransportClause
            // 
            this.labTransportClause.Location = new System.Drawing.Point(402, 61);
            this.labTransportClause.Name = "labTransportClause";
            this.labTransportClause.Size = new System.Drawing.Size(28, 14);
            this.labTransportClause.TabIndex = 0;
            this.labTransportClause.Text = "Code";
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(4, 6);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(17, 14);
            this.labNo.TabIndex = 0;
            this.labNo.Text = "NO";
            // 
            // labTradeTerm
            // 
            this.labTradeTerm.Location = new System.Drawing.Point(4, 90);
            this.labTradeTerm.Name = "labTradeTerm";
            this.labTradeTerm.Size = new System.Drawing.Size(61, 14);
            this.labTradeTerm.TabIndex = 0;
            this.labTradeTerm.Text = "TradeTerm";
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(4, 62);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 0;
            this.labCustomer.Text = "Customer";
            // 
            // labOrderDate
            // 
            this.labOrderDate.Location = new System.Drawing.Point(607, 89);
            this.labOrderDate.Name = "labOrderDate";
            this.labOrderDate.Size = new System.Drawing.Size(69, 14);
            this.labOrderDate.TabIndex = 0;
            this.labOrderDate.Text = "BookingDate";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(4, 34);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 0;
            this.labCompany.Text = "Company";
            // 
            // labSales
            // 
            this.labSales.Location = new System.Drawing.Point(402, 7);
            this.labSales.Name = "labSales";
            this.labSales.Size = new System.Drawing.Size(27, 14);
            this.labSales.TabIndex = 0;
            this.labSales.Text = "Sales";
            // 
            // labSalesType
            // 
            this.labSalesType.Location = new System.Drawing.Point(206, 88);
            this.labSalesType.Name = "labSalesType";
            this.labSalesType.Size = new System.Drawing.Size(55, 14);
            this.labSalesType.TabIndex = 0;
            this.labSalesType.Text = "SalesType";
            // 
            // labSalesDepartment
            // 
            this.labSalesDepartment.Location = new System.Drawing.Point(402, 34);
            this.labSalesDepartment.Name = "labSalesDepartment";
            this.labSalesDepartment.Size = new System.Drawing.Size(53, 14);
            this.labSalesDepartment.TabIndex = 0;
            this.labSalesDepartment.Text = "Sales Dep";
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(224, 34);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(28, 14);
            this.labType.TabIndex = 0;
            this.labType.Text = "Type";
            // 
            // labBookingMode
            // 
            this.labBookingMode.Location = new System.Drawing.Point(607, 62);
            this.labBookingMode.Name = "labBookingMode";
            this.labBookingMode.Size = new System.Drawing.Size(73, 14);
            this.labBookingMode.TabIndex = 0;
            this.labBookingMode.Text = "BookingMode";
            // 
            // labOperatorName
            // 
            this.labOperatorName.Location = new System.Drawing.Point(607, 35);
            this.labOperatorName.Name = "labOperatorName";
            this.labOperatorName.Size = new System.Drawing.Size(16, 14);
            this.labOperatorName.TabIndex = 0;
            this.labOperatorName.Text = "OP";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.panel2);
            this.navBarGroupControlContainer2.Controls.Add(this.panel1);
            this.navBarGroupControlContainer2.Controls.Add(this.groupRemark);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(801, 453);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtAgent);
            this.panel2.Controls.Add(this.mcmbCarrier);
            this.panel2.Controls.Add(this.groupHBL);
            this.panel2.Controls.Add(this.groupMBL);
            this.panel2.Controls.Add(this.chkIsOnlyMBL);
            this.panel2.Controls.Add(this.labAgent);
            this.panel2.Controls.Add(this.groupLocalService);
            this.panel2.Controls.Add(this.dteClosingDate);
            this.panel2.Controls.Add(this.labExpectedArriveDate);
            this.panel2.Controls.Add(this.dteEstimatedDeliveryDate);
            this.panel2.Controls.Add(this.dteExpectedShipDate);
            this.panel2.Controls.Add(this.dteExpectedArriveDate);
            this.panel2.Controls.Add(this.labClosingDate);
            this.panel2.Controls.Add(this.labExpectedShipDate);
            this.panel2.Controls.Add(this.labEstimatedDeliveryDate);
            this.panel2.Controls.Add(this.labCarrier);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(395, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(403, 353);
            this.panel2.TabIndex = 1;
            // 
            // mcmbCarrier
            // 
            this.mcmbCarrier.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "CarrierID", true));
            this.mcmbCarrier.EditText = "";
            this.mcmbCarrier.EditValue = null;
            this.mcmbCarrier.Location = new System.Drawing.Point(86, 27);
            this.mcmbCarrier.Name = "mcmbCarrier";
            this.mcmbCarrier.ReadOnly = false;
            this.mcmbCarrier.RefreshButtonToolTip = "";
            this.mcmbCarrier.ShowRefreshButton = false;
            this.mcmbCarrier.Size = new System.Drawing.Size(305, 21);
            this.mcmbCarrier.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbCarrier.TabIndex = 320;
            this.mcmbCarrier.ToolTip = "";
            // 
            // groupHBL
            // 
            this.groupHBL.Controls.Add(this.cmbHBLPaymentTerm);
            this.groupHBL.Controls.Add(this.cmbHBLReleaseType);
            this.groupHBL.Controls.Add(this.txtHBLRequirements);
            this.groupHBL.Controls.Add(this.labHBLPaymentTerm);
            this.groupHBL.Controls.Add(this.labHBLReleaseType);
            this.groupHBL.Location = new System.Drawing.Point(197, 148);
            this.groupHBL.Name = "groupHBL";
            this.groupHBL.Size = new System.Drawing.Size(197, 195);
            this.groupHBL.TabIndex = 9;
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
            this.labHBLReleaseType.Location = new System.Drawing.Point(7, 44);
            this.labHBLReleaseType.Name = "labHBLReleaseType";
            this.labHBLReleaseType.Size = new System.Drawing.Size(69, 14);
            this.labHBLReleaseType.TabIndex = 0;
            this.labHBLReleaseType.Text = "ReleaseType";
            // 
            // groupMBL
            // 
            this.groupMBL.Controls.Add(this.cmbMBLReleaseType);
            this.groupMBL.Controls.Add(this.cmbMBLPaymentTerm);
            this.groupMBL.Controls.Add(this.txtMBLRequirements);
            this.groupMBL.Controls.Add(this.labMBLPaymentTerm);
            this.groupMBL.Controls.Add(this.labMBLReleaseType);
            this.groupMBL.Location = new System.Drawing.Point(5, 148);
            this.groupMBL.Name = "groupMBL";
            this.groupMBL.Size = new System.Drawing.Size(189, 195);
            this.groupMBL.TabIndex = 8;
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
            this.labMBLReleaseType.Location = new System.Drawing.Point(7, 44);
            this.labMBLReleaseType.Name = "labMBLReleaseType";
            this.labMBLReleaseType.Size = new System.Drawing.Size(69, 14);
            this.labMBLReleaseType.TabIndex = 0;
            this.labMBLReleaseType.Text = "ReleaseType";
            // 
            // labAgent
            // 
            this.labAgent.Location = new System.Drawing.Point(7, 4);
            this.labAgent.Name = "labAgent";
            this.labAgent.Size = new System.Drawing.Size(34, 14);
            this.labAgent.TabIndex = 35;
            this.labAgent.Text = "Agent";
            // 
            // groupLocalService
            // 
            this.groupLocalService.Controls.Add(this.chkIsWarehouse);
            this.groupLocalService.Controls.Add(this.chkIsQuarantineInspection);
            this.groupLocalService.Controls.Add(this.chkIsCommodityInspection);
            this.groupLocalService.Controls.Add(this.chkIsCustoms);
            this.groupLocalService.Controls.Add(this.chkIsTruck);
            this.groupLocalService.Location = new System.Drawing.Point(6, 104);
            this.groupLocalService.Name = "groupLocalService";
            this.groupLocalService.Size = new System.Drawing.Size(319, 42);
            this.groupLocalService.TabIndex = 6;
            this.groupLocalService.TabStop = false;
            this.groupLocalService.Text = "LocalService";
            // 
            // labExpectedArriveDate
            // 
            this.labExpectedArriveDate.Location = new System.Drawing.Point(211, 85);
            this.labExpectedArriveDate.Name = "labExpectedArriveDate";
            this.labExpectedArriveDate.Size = new System.Drawing.Size(55, 14);
            this.labExpectedArriveDate.TabIndex = 27;
            this.labExpectedArriveDate.Text = "Exp.Arrive";
            // 
            // labClosingDate
            // 
            this.labClosingDate.Location = new System.Drawing.Point(7, 56);
            this.labClosingDate.Name = "labClosingDate";
            this.labClosingDate.Size = new System.Drawing.Size(63, 14);
            this.labClosingDate.TabIndex = 27;
            this.labClosingDate.Text = "ClosingDate";
            // 
            // labExpectedShipDate
            // 
            this.labExpectedShipDate.Location = new System.Drawing.Point(211, 56);
            this.labExpectedShipDate.Name = "labExpectedShipDate";
            this.labExpectedShipDate.Size = new System.Drawing.Size(47, 14);
            this.labExpectedShipDate.TabIndex = 27;
            this.labExpectedShipDate.Text = "Exp.Ship";
            // 
            // labEstimatedDeliveryDate
            // 
            this.labEstimatedDeliveryDate.Location = new System.Drawing.Point(7, 82);
            this.labEstimatedDeliveryDate.Name = "labEstimatedDeliveryDate";
            this.labEstimatedDeliveryDate.Size = new System.Drawing.Size(63, 14);
            this.labEstimatedDeliveryDate.TabIndex = 27;
            this.labEstimatedDeliveryDate.Text = "Est.Delivery";
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(7, 30);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(34, 14);
            this.labCarrier.TabIndex = 3;
            this.labCarrier.Text = "Carrier";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbCargoType);
            this.panel1.Controls.Add(this.cmbWeightUnit);
            this.panel1.Controls.Add(this.cmbMeasurementUnit);
            this.panel1.Controls.Add(this.cmbQuantityUnit);
            this.panel1.Controls.Add(this.stxtFinalDestination);
            this.panel1.Controls.Add(this.labFinalDestination);
            this.panel1.Controls.Add(this.labCommodity);
            this.panel1.Controls.Add(this.txtCommodity);
            this.panel1.Controls.Add(this.containerDemandControl1);
            this.panel1.Controls.Add(this.cargoDescriptionPart1);
            this.panel1.Controls.Add(this.labCargoType);
            this.panel1.Controls.Add(this.labBookingCustomer);
            this.panel1.Controls.Add(this.stxtBookingCustomer);
            this.panel1.Controls.Add(this.labShipper);
            this.panel1.Controls.Add(this.stxtShipper);
            this.panel1.Controls.Add(this.labQuantity);
            this.panel1.Controls.Add(this.labPlaceOfReceipt);
            this.panel1.Controls.Add(this.labPOL);
            this.panel1.Controls.Add(this.stxtPlaceOfReceipt);
            this.panel1.Controls.Add(this.stxtPOL);
            this.panel1.Controls.Add(this.labWeight);
            this.panel1.Controls.Add(this.labConsignee);
            this.panel1.Controls.Add(this.labPlaceOfDelivery);
            this.panel1.Controls.Add(this.labMeasurement);
            this.panel1.Controls.Add(this.stxtConsignee);
            this.panel1.Controls.Add(this.txtCargoDescription);
            this.panel1.Controls.Add(this.stxtPlaceOfDelivery);
            this.panel1.Controls.Add(this.numQuantity);
            this.panel1.Controls.Add(this.numWeight);
            this.panel1.Controls.Add(this.numMeasurement);
            this.panel1.Controls.Add(this.labPOD);
            this.panel1.Controls.Add(this.stxtPOD);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 353);
            this.panel1.TabIndex = 0;
            // 
            // stxtFinalDestination
            // 
            this.stxtFinalDestination.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderInfo, "FinalDestinationName", true));
            this.stxtFinalDestination.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOrderInfo, "FinalDestinationId", true));
            this.stxtFinalDestination.EditValue = "";
            this.stxtFinalDestination.Location = new System.Drawing.Point(101, 190);
            this.stxtFinalDestination.Name = "stxtFinalDestination";
            this.stxtFinalDestination.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtFinalDestination.Properties.Appearance.Options.UseBackColor = true;
            this.stxtFinalDestination.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtFinalDestination.Size = new System.Drawing.Size(283, 21);
            this.stxtFinalDestination.TabIndex = 220;
            // 
            // labFinalDestination
            // 
            this.labFinalDestination.Location = new System.Drawing.Point(4, 192);
            this.labFinalDestination.Name = "labFinalDestination";
            this.labFinalDestination.Size = new System.Drawing.Size(84, 14);
            this.labFinalDestination.TabIndex = 38;
            this.labFinalDestination.Text = "FinalDestination";
            // 
            // labCommodity
            // 
            this.labCommodity.Location = new System.Drawing.Point(4, 219);
            this.labCommodity.Name = "labCommodity";
            this.labCommodity.Size = new System.Drawing.Size(61, 14);
            this.labCommodity.TabIndex = 36;
            this.labCommodity.Text = "Commodity";
            // 
            // cargoDescriptionPart1
            // 
            this.cargoDescriptionPart1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.cargoDescriptionPart1.Appearance.Options.UseBackColor = true;
            this.cargoDescriptionPart1.Location = new System.Drawing.Point(386, 221);
            this.cargoDescriptionPart1.Name = "cargoDescriptionPart1";
            this.cargoDescriptionPart1.Size = new System.Drawing.Size(19, 21);
            this.cargoDescriptionPart1.TabIndex = 14;
            // 
            // labCargoType
            // 
            this.labCargoType.Location = new System.Drawing.Point(250, 247);
            this.labCargoType.Name = "labCargoType";
            this.labCargoType.Size = new System.Drawing.Size(59, 14);
            this.labCargoType.TabIndex = 0;
            this.labCargoType.Text = "CargoType";
            // 
            // labBookingCustomer
            // 
            this.labBookingCustomer.Location = new System.Drawing.Point(4, 3);
            this.labBookingCustomer.Name = "labBookingCustomer";
            this.labBookingCustomer.Size = new System.Drawing.Size(95, 14);
            this.labBookingCustomer.TabIndex = 0;
            this.labBookingCustomer.Text = "BookingCustomer";
            // 
            // labShipper
            // 
            this.labShipper.Location = new System.Drawing.Point(4, 30);
            this.labShipper.Name = "labShipper";
            this.labShipper.Size = new System.Drawing.Size(41, 14);
            this.labShipper.TabIndex = 3;
            this.labShipper.Text = "Shipper";
            // 
            // labQuantity
            // 
            this.labQuantity.Location = new System.Drawing.Point(4, 246);
            this.labQuantity.Name = "labQuantity";
            this.labQuantity.Size = new System.Drawing.Size(47, 14);
            this.labQuantity.TabIndex = 0;
            this.labQuantity.Text = "Quantity";
            // 
            // labPlaceOfReceipt
            // 
            this.labPlaceOfReceipt.Location = new System.Drawing.Point(4, 84);
            this.labPlaceOfReceipt.Name = "labPlaceOfReceipt";
            this.labPlaceOfReceipt.Size = new System.Drawing.Size(82, 14);
            this.labPlaceOfReceipt.TabIndex = 3;
            this.labPlaceOfReceipt.Text = "PlaceOfReceipt";
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(4, 111);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(22, 14);
            this.labPOL.TabIndex = 3;
            this.labPOL.Text = "POL";
            // 
            // labWeight
            // 
            this.labWeight.Location = new System.Drawing.Point(4, 273);
            this.labWeight.Name = "labWeight";
            this.labWeight.Size = new System.Drawing.Size(40, 14);
            this.labWeight.TabIndex = 0;
            this.labWeight.Text = "Weight";
            // 
            // labConsignee
            // 
            this.labConsignee.Location = new System.Drawing.Point(4, 57);
            this.labConsignee.Name = "labConsignee";
            this.labConsignee.Size = new System.Drawing.Size(56, 14);
            this.labConsignee.TabIndex = 3;
            this.labConsignee.Text = "Consignee";
            // 
            // labPlaceOfDelivery
            // 
            this.labPlaceOfDelivery.Location = new System.Drawing.Point(4, 165);
            this.labPlaceOfDelivery.Name = "labPlaceOfDelivery";
            this.labPlaceOfDelivery.Size = new System.Drawing.Size(83, 14);
            this.labPlaceOfDelivery.TabIndex = 3;
            this.labPlaceOfDelivery.Text = "PlaceOfDelivery";
            // 
            // labMeasurement
            // 
            this.labMeasurement.Location = new System.Drawing.Point(4, 300);
            this.labMeasurement.Name = "labMeasurement";
            this.labMeasurement.Size = new System.Drawing.Size(74, 14);
            this.labMeasurement.TabIndex = 0;
            this.labMeasurement.Text = "Measurement";
            // 
            // numQuantity
            // 
            this.numQuantity.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "Quantity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numQuantity.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numQuantity.Location = new System.Drawing.Point(101, 244);
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numQuantity.Properties.DisplayFormat.FormatString = "N00";
            this.numQuantity.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numQuantity.Properties.EditFormat.FormatString = "N00";
            this.numQuantity.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numQuantity.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numQuantity.Properties.IsFloatValue = false;
            this.numQuantity.Properties.Mask.EditMask = "N00";
            this.numQuantity.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numQuantity.Size = new System.Drawing.Size(67, 21);
            this.numQuantity.TabIndex = 240;
            // 
            // numWeight
            // 
            this.numWeight.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "Weight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numWeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numWeight.Location = new System.Drawing.Point(101, 271);
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
            this.numWeight.Size = new System.Drawing.Size(67, 21);
            this.numWeight.TabIndex = 260;
            // 
            // numMeasurement
            // 
            this.numMeasurement.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOrderInfo, "Measurement", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numMeasurement.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numMeasurement.Location = new System.Drawing.Point(101, 298);
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
            this.numMeasurement.Size = new System.Drawing.Size(67, 21);
            this.numMeasurement.TabIndex = 280;
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(4, 138);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(24, 14);
            this.labPOD.TabIndex = 3;
            this.labPOD.Text = "POD";
            // 
            // groupRemark
            // 
            this.groupRemark.Controls.Add(this.txtRemark);
            this.groupRemark.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupRemark.Location = new System.Drawing.Point(0, 353);
            this.groupRemark.Name = "groupRemark";
            this.groupRemark.Size = new System.Drawing.Size(801, 100);
            this.groupRemark.TabIndex = 3;
            this.groupRemark.TabStop = false;
            this.groupRemark.Text = "Remark";
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.orderFeeEditPart1);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(801, 183);
            this.navBarGroupControlContainer3.TabIndex = 2;
            // 
            // navBarDelegateInfo
            // 
            this.navBarDelegateInfo.Caption = "DelegateInfo";
            this.navBarDelegateInfo.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarDelegateInfo.Expanded = true;
            this.navBarDelegateInfo.GroupClientHeight = 455;
            this.navBarDelegateInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarDelegateInfo.Name = "navBarDelegateInfo";
            // 
            // navFee
            // 
            this.navFee.Caption = "Fee";
            this.navFee.ControlContainer = this.navBarGroupControlContainer3;
            this.navFee.Expanded = true;
            this.navFee.GroupClientHeight = 185;
            this.navFee.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navFee.Name = "navFee";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 26);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabPageBase;
            this.xtraTabControl1.Size = new System.Drawing.Size(862, 854);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageBase,
            this.tabPagePO});
            // 
            // OrderBaseEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "OrderBaseEditPart";
            this.Size = new System.Drawing.Size(862, 880);
            ((System.ComponentModel.ISupportInitialize)(this.bsOrderInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteOrderDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteOrderDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteClosingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEstimatedDeliveryDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEstimatedDeliveryDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedShipDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedShipDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedArriveDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpectedArriveDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBookingCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCargoDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMBLRequirements.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHBLRequirements.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOnlyMBL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsWarehouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsQuarantineInspection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCommodityInspection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCustoms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTruck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfReceipt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQtyUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditFloat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbSales.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbBookinger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmOverseasFiler.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTradeTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuantityUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMBLPaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLPaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBookingMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMBLReleaseType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLReleaseType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCargoType.Properties)).EndInit();
            this.tabPagePO.ResumeLayout(false);
            this.tabPageBase.ResumeLayout(false);
            this.panelScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupHBL.ResumeLayout(false);
            this.groupHBL.PerformLayout();
            this.groupMBL.ResumeLayout(false);
            this.groupMBL.PerformLayout();
            this.groupLocalService.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtFinalDestination.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurement.Properties)).EndInit();
            this.groupRemark.ResumeLayout(false);
            this.navBarGroupControlContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsOrderInfo;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabPageBase;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupHBL;
        private DevExpress.XtraEditors.MemoEdit txtHBLRequirements;
        private DevExpress.XtraEditors.LabelControl labHBLPaymentTerm;
        private DevExpress.XtraEditors.LabelControl labHBLReleaseType;
        private DevExpress.XtraEditors.LabelControl labQuantity;
        private System.Windows.Forms.GroupBox groupMBL;
        private DevExpress.XtraEditors.MemoEdit txtMBLRequirements;
        private DevExpress.XtraEditors.LabelControl labMBLPaymentTerm;
        private DevExpress.XtraEditors.LabelControl labMBLReleaseType;
        private DevExpress.XtraEditors.CheckEdit chkIsOnlyMBL;
        private DevExpress.XtraEditors.LabelControl labWeight;
        private System.Windows.Forms.GroupBox groupLocalService;
        private DevExpress.XtraEditors.CheckEdit chkIsWarehouse;
        private DevExpress.XtraEditors.CheckEdit chkIsQuarantineInspection;
        private DevExpress.XtraEditors.CheckEdit chkIsCommodityInspection;
        private DevExpress.XtraEditors.CheckEdit chkIsCustoms;
        private DevExpress.XtraEditors.CheckEdit chkIsTruck;
        private ICP.Framework.ClientComponents.Controls.ContainerDemandControl containerDemandControl1;
        private DevExpress.XtraEditors.LabelControl labMeasurement;
        private DevExpress.XtraEditors.MemoEdit txtCargoDescription;
        private DevExpress.XtraEditors.LabelControl labCargoType;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labBookingCustomer;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtBookingCustomer;
        private DevExpress.XtraEditors.LabelControl labShipper;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtShipper;
        private DevExpress.XtraEditors.LabelControl labPOL;
        private DevExpress.XtraEditors.ButtonEdit stxtPOL;
        private DevExpress.XtraEditors.LabelControl labConsignee;
        private DevExpress.XtraEditors.LabelControl labExpectedArriveDate;
        private DevExpress.XtraEditors.LabelControl labPlaceOfDelivery;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtConsignee;
        private DevExpress.XtraEditors.LabelControl labEstimatedDeliveryDate;
        private DevExpress.XtraEditors.ButtonEdit stxtPlaceOfDelivery;
        private DevExpress.XtraEditors.LabelControl labExpectedShipDate;
        private DevExpress.XtraEditors.LabelControl labClosingDate;
        private DevExpress.XtraEditors.DateEdit dteExpectedArriveDate;
        private DevExpress.XtraEditors.LabelControl labTransportClause;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private DevExpress.XtraEditors.DateEdit dteExpectedShipDate;
        private DevExpress.XtraEditors.DateEdit dteEstimatedDeliveryDate;
        private DevExpress.XtraEditors.LabelControl labPaymentTerm;
        private DevExpress.XtraEditors.DateEdit dteClosingDate;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private DevExpress.XtraEditors.ButtonEdit stxtPOD;
        private DevExpress.XtraEditors.LabelControl labOrderDate;
        private DevExpress.XtraEditors.LabelControl labSalesType;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.LabelControl labOperatorName;
        private DevExpress.XtraEditors.LabelControl labBookingMode;
        private DevExpress.XtraEditors.LabelControl labSalesDepartment;
        private DevExpress.XtraEditors.LabelControl labSales;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.LabelControl labTradeTerm;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.LabelControl labNo;
        private DevExpress.XtraEditors.DateEdit dteOrderDate;
        private DevExpress.XtraTab.XtraTabPage tabPagePO;
        private OrderPOEditPart orderPOEditPart1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barSaveAs;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private OrderFeeEditPart orderFeeEditPart1;
        private ICP.FCM.OceanExport.UI.Common.CargoDescriptionPart cargoDescriptionPart1;
        private DevExpress.XtraEditors.SpinEdit numQuantity;
        private DevExpress.XtraEditors.SpinEdit numWeight;
        private DevExpress.XtraEditors.SpinEdit numMeasurement;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraEditors.XtraScrollableControl panelScroll;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroup navBarDelegateInfo;
        private DevExpress.XtraEditors.LabelControl labPlaceOfReceipt;
        private DevExpress.XtraEditors.ButtonEdit stxtPlaceOfReceipt;
        private BindingSource bsOrders;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraEditors.PopupContainerEdit stxtCustomer;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOrders;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbQtyUnit;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox2;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox3;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rspinEditInt;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rspinEditFloat;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private GroupBox groupRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colClosingDate;
        private DevExpress.XtraNavBar.NavBarGroup navFee;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbCarrier;
        private ICP.Framework.ClientComponents.Controls.TreeSelectBox trsSalesDep;
        private DevExpress.XtraEditors.LabelControl labAgent;
        private DevExpress.XtraEditors.LabelControl labAbroadOP;
        private DevExpress.XtraEditors.TextEdit txtState;
        private DevExpress.XtraEditors.LabelControl labState;
        private DevExpress.XtraEditors.ButtonEdit stxtFinalDestination;
        private DevExpress.XtraEditors.LabelControl labFinalDestination;
        private DevExpress.XtraEditors.LabelControl labCommodity;
        private DevExpress.XtraEditors.MemoEdit txtCommodity;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit mcmbSales;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit mcmOverseasFiler;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit mcmbBookinger;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTransportClause;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTradeTerm;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbWeightUnit;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMeasurementUnit;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbQuantityUnit;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbSalesType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbPaymentTerm;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMBLPaymentTerm;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbHBLPaymentTerm;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbBookingMode;
        private DevExpress.XtraEditors.TextEdit txtAgent;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMBLReleaseType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbHBLReleaseType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCargoType;
    }
}
