using System.Windows.Forms;
namespace ICP.FCM.AirImport.UI
{
    partial class OIBusinessEdit
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OIBusinessEdit));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.barAuditor = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barReturn = new DevExpress.XtraBars.BarButtonItem();
            this.barBill = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarSubItem();
            this.barPrintArrivalNotice = new DevExpress.XtraBars.BarButtonItem();
            this.barReleaseOrder = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintProfit = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintAuthority = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.TabMain = new DevExpress.XtraTab.XtraTabControl();
            this.TabBase = new DevExpress.XtraTab.XtraTabPage();
            this.bcMain = new DevExpress.XtraNavBar.NavBarControl();
            this.bgMain = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgcBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cmbSales = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.bsBusiness = new System.Windows.Forms.BindingSource(this.components);
            this.dteOrderDate = new DevExpress.XtraEditors.DateEdit();
            this.labOrderDate = new DevExpress.XtraEditors.LabelControl();
            this.cmbCustomerService = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labelControl50 = new DevExpress.XtraEditors.LabelControl();
            this.cmbFile = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labelControl43 = new DevExpress.XtraEditors.LabelControl();
            this.cmbSalesType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labelControl47 = new DevExpress.XtraEditors.LabelControl();
            this.cmbBookingMode = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerRefNo = new DevExpress.XtraEditors.TextEdit();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.gridControl1 = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsRecentTenOrders = new System.Windows.Forms.BindingSource(this.components);
            this.gvOrders = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbQtyUnit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rspinEditInt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.rspinEditFloat = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtPOLFilerName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.treeBoxSalesDep = new ICP.Framework.ClientComponents.Controls.TreeSelectBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.cmbPaymentTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.stxtCustomer = new DevExpress.XtraEditors.PopupContainerEdit();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.cmbTransportClause = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbTradeTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labTradeTerm = new DevExpress.XtraEditors.LabelControl();
            this.txtState = new DevExpress.XtraEditors.TextEdit();
            this.labState = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.bgcCom = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labWarehouseArrivedON = new DevExpress.XtraEditors.LabelControl();
            this.dtpWarehouseArrivedON = new DevExpress.XtraEditors.DateEdit();
            this.labStorageStartDate = new DevExpress.XtraEditors.LabelControl();
            this.dtpStorageStartDate = new DevExpress.XtraEditors.DateEdit();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtpClearanceDate = new DevExpress.XtraEditors.DateEdit();
            this.ckbIsClearance = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl48 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl49 = new DevExpress.XtraEditors.LabelControl();
            this.detWareHouseDate = new DevExpress.XtraEditors.DateEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.dateEdit4 = new DevExpress.XtraEditors.DateEdit();
            this.txtAgent = new ICP.Framework.ClientComponents.Controls.ComboCustomerDescriptionControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.txtCommodity = new DevExpress.XtraEditors.MemoEdit();
            this.cargoDescriptionPart1 = new ICP.FCM.AirImport.UI.CargoDescriptionPart();
            this.cmbCargoType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labelControl53 = new DevExpress.XtraEditors.LabelControl();
            this.labCargoType = new DevExpress.XtraEditors.LabelControl();
            this.txtCargoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIsCommodityInspection = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsQuarantineInspection = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsTruck = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsWarehouse = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsCustoms = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl46 = new DevExpress.XtraEditors.LabelControl();
            this.txtAgentNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl45 = new DevExpress.XtraEditors.LabelControl();
            this.numWeight = new DevExpress.XtraEditors.SpinEdit();
            this.groupRemark = new System.Windows.Forms.GroupBox();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtReleaseDate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl54 = new DevExpress.XtraEditors.LabelControl();
            this.ckbIsTelex = new DevExpress.XtraEditors.CheckEdit();
            this.ckbIsTransport = new DevExpress.XtraEditors.CheckEdit();
            this.ckbIsReleaseNotify = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.numMeasurement = new DevExpress.XtraEditors.SpinEdit();
            this.stxtConsignee = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.labelControl33 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.dtpETD = new DevExpress.XtraEditors.DateEdit();
            this.cmbCustoms = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.dtpETA = new DevExpress.XtraEditors.DateEdit();
            this.cmbWeightUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labelControl32 = new DevExpress.XtraEditors.LabelControl();
            this.stxtShipper = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.labelControl35 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl24 = new DevExpress.XtraEditors.LabelControl();
            this.cmbQuantityunit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labelControl25 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl34 = new DevExpress.XtraEditors.LabelControl();
            this.numQuantity = new DevExpress.XtraEditors.SpinEdit();
            this.labDetination = new DevExpress.XtraEditors.LabelControl();
            this.stxtNotifyParty = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.dtpDETA = new DevExpress.XtraEditors.DateEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.cmbWareHouse = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.cmbMeasurementUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtDeparture = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.labelDeparture = new DevExpress.XtraEditors.LabelControl();
            this.stxtDetination = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.stxtPlaceOfDelivery = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.bgcHBLList = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.UCHBLList = new ICP.FCM.AirImport.UI.UCBusinessBLList();
            this.bgcFee = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.UCOIOrderFeeEdit = new ICP.FCM.AirImport.UI.OIOrderFeeEdit();
            this.bgcMBL = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.txtFlightCountry = new DevExpress.XtraEditors.TextEdit();
            this.bsMBLInfo = new System.Windows.Forms.BindingSource(this.components);
            this.txtFlightFlag = new DevExpress.XtraEditors.TextEdit();
            this.labFlgflag = new DevExpress.XtraEditors.LabelControl();
            this.labelControl39 = new DevExpress.XtraEditors.LabelControl();
            this.stxtFinalWareHouse = new DevExpress.XtraEditors.ButtonEdit();
            this.txtGODate = new DevExpress.XtraEditors.DateEdit();
            this.labGODate = new DevExpress.XtraEditors.LabelControl();
            this.txtManifestNO = new DevExpress.XtraEditors.TextEdit();
            this.labManifestNO = new DevExpress.XtraEditors.LabelControl();
            this.labelControl40 = new DevExpress.XtraEditors.LabelControl();
            this.cmbMBLTransportClause = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtSubNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl52 = new DevExpress.XtraEditors.LabelControl();
            this.cmbFlightNo = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labelControl51 = new DevExpress.XtraEditors.LabelControl();
            this.stxtMBLNo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labFlightNo = new DevExpress.XtraEditors.LabelControl();
            this.txtITPalce = new DevExpress.XtraEditors.TextEdit();
            this.dtpITDate = new DevExpress.XtraEditors.DateEdit();
            this.stxtAgentOfCarrier = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl29 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl42 = new DevExpress.XtraEditors.LabelControl();
            this.mcmbAirCompany = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labelControl44 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl28 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl41 = new DevExpress.XtraEditors.LabelControl();
            this.cmbReleaseType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labelControl27 = new DevExpress.XtraEditors.LabelControl();
            this.txtITNo = new DevExpress.XtraEditors.TextEdit();
            this.bgCom = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgMBL = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgHBLList = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgFee = new DevExpress.XtraNavBar.NavBarGroup();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.pnlMain = new DevExpress.XtraEditors.XtraScrollableControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TabMain)).BeginInit();
            this.TabMain.SuspendLayout();
            this.TabBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bcMain)).BeginInit();
            this.bcMain.SuspendLayout();
            this.bgcBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsBusiness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteOrderDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteOrderDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerService.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBookingMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerRefNo.Properties)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtPOLFilerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTradeTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            this.bgcCom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpWarehouseArrivedON.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpWarehouseArrivedON.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStorageStartDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStorageStartDate.Properties)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpClearanceDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpClearanceDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsClearance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detWareHouseDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detWareHouseDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit4.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCargoType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCargoDescription.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCommodityInspection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsQuarantineInspection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTruck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsWarehouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCustoms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgentNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight.Properties)).BeginInit();
            this.groupRemark.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReleaseDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsTelex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsTransport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsReleaseNotify.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpETD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpETD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustoms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpETA.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuantityunit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtNotifyParty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDETA.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWareHouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDeparture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDetination.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).BeginInit();
            this.bgcHBLList.SuspendLayout();
            this.bgcFee.SuspendLayout();
            this.bgcMBL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlightCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMBLInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlightFlag.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtFinalWareHouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGODate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGODate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManifestNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMBLTransportClause.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtMBLNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITPalce.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpITDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpITDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgentOfCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
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
            this.barBill,
            this.barPrint,
            this.barPrintArrivalNotice,
            this.barReleaseOrder,
            this.barPrintProfit,
            this.barPrintAuthority,
            this.barClose,
            this.barSaveAs,
            this.barAuditor,
            this.barReturn,
            this.barRefresh});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 8;
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAuditor, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barReturn, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBill, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(S)";
            this.barSave.Glyph = global::ICP.FCM.AirImport.UI.Properties.Resources.Save_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barSaveAs
            // 
            this.barSaveAs.Caption = "另存为(&A)";
            this.barSaveAs.Glyph = global::ICP.FCM.AirImport.UI.Properties.Resources.Save_16;
            this.barSaveAs.Id = 4;
            this.barSaveAs.Name = "barSaveAs";
            this.barSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSaveAs_ItemClick);
            // 
            // barAuditor
            // 
            this.barAuditor.Caption = "审核并保存";
            this.barAuditor.Glyph = global::ICP.FCM.AirImport.UI.Properties.Resources.Save_Blue_16;
            this.barAuditor.Id = 5;
            this.barAuditor.Name = "barAuditor";
            this.barAuditor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAuditor_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "刷新";
            this.barRefresh.Glyph = global::ICP.FCM.AirImport.UI.Properties.Resources.Refresh_161;
            this.barRefresh.Id = 7;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barReturn
            // 
            this.barReturn.Caption = "打回(&R)";
            this.barReturn.Glyph = global::ICP.FCM.AirImport.UI.Properties.Resources.Return_16;
            this.barReturn.Id = 6;
            this.barReturn.Name = "barReturn";
            this.barReturn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barReturn_ItemClick);
            // 
            // barBill
            // 
            this.barBill.Caption = "帐单(&B)";
            this.barBill.Glyph = global::ICP.FCM.AirImport.UI.Properties.Resources.Memo_16;
            this.barBill.Id = 1;
            this.barBill.Name = "barBill";
            // 
            // barPrint
            // 
            this.barPrint.Caption = "打印";
            this.barPrint.Glyph = global::ICP.FCM.AirImport.UI.Properties.Resources.Print_16;
            this.barPrint.Id = 2;
            this.barPrint.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrintArrivalNotice),
            new DevExpress.XtraBars.LinkPersistInfo(this.barReleaseOrder),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrintProfit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrintAuthority)});
            this.barPrint.Name = "barPrint";
            // 
            // barPrintArrivalNotice
            // 
            this.barPrintArrivalNotice.Caption = "到港通知书";
            this.barPrintArrivalNotice.Glyph = global::ICP.FCM.AirImport.UI.Properties.Resources.Print_16;
            this.barPrintArrivalNotice.Id = 8;
            this.barPrintArrivalNotice.Name = "barPrintArrivalNotice";
            // 
            // barReleaseOrder
            // 
            this.barReleaseOrder.Caption = "放货通知书";
            this.barReleaseOrder.Glyph = global::ICP.FCM.AirImport.UI.Properties.Resources.Print_16;
            this.barReleaseOrder.Id = 9;
            this.barReleaseOrder.Name = "barReleaseOrder";
            // 
            // barPrintProfit
            // 
            this.barPrintProfit.Caption = "利润表";
            this.barPrintProfit.Glyph = global::ICP.FCM.AirImport.UI.Properties.Resources.Print_16;
            this.barPrintProfit.Id = 11;
            this.barPrintProfit.Name = "barPrintProfit";
            // 
            // barPrintAuthority
            // 
            this.barPrintAuthority.Caption = "Authority To Make Entry";
            this.barPrintAuthority.Glyph = global::ICP.FCM.AirImport.UI.Properties.Resources.Print_16;
            this.barPrintAuthority.Id = 14;
            this.barPrintAuthority.Name = "barPrintAuthority";
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭(&C)";
            this.barClose.Glyph = global::ICP.FCM.AirImport.UI.Properties.Resources.Left_D_16;
            this.barClose.Id = 3;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(900, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 733);
            this.barDockControlBottom.Size = new System.Drawing.Size(900, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 707);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(900, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 707);
            // 
            // TabMain
            // 
            this.TabMain.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.TabMain.Location = new System.Drawing.Point(3, 6);
            this.TabMain.Name = "TabMain";
            this.TabMain.SelectedTabPage = this.TabBase;
            this.TabMain.Size = new System.Drawing.Size(863, 1378);
            this.TabMain.TabIndex = 5;
            this.TabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabBase});
            // 
            // TabBase
            // 
            this.TabBase.Controls.Add(this.bcMain);
            this.TabBase.Name = "TabBase";
            this.TabBase.Size = new System.Drawing.Size(833, 1371);
            this.TabBase.Text = "基础";
            // 
            // bcMain
            // 
            this.bcMain.ActiveGroup = this.bgMain;
            this.bcMain.Controls.Add(this.bgcBase);
            this.bcMain.Controls.Add(this.bgcCom);
            this.bcMain.Controls.Add(this.bgcHBLList);
            this.bcMain.Controls.Add(this.bgcFee);
            this.bcMain.Controls.Add(this.bgcMBL);
            this.bcMain.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.bgMain,
            this.bgCom,
            this.bgMBL,
            this.bgHBLList,
            this.bgFee});
            this.bcMain.Location = new System.Drawing.Point(3, 3);
            this.bcMain.Name = "bcMain";
            this.bcMain.OptionsNavPane.ExpandedWidth = 892;
            this.bcMain.Size = new System.Drawing.Size(829, 1400);
            this.bcMain.TabIndex = 0;
            this.bcMain.Text = "重量";
            // 
            // bgMain
            // 
            this.bgMain.Caption = "基本信息";
            this.bgMain.ControlContainer = this.bgcBase;
            this.bgMain.Expanded = true;
            this.bgMain.GroupClientHeight = 111;
            this.bgMain.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgMain.Name = "bgMain";
            // 
            // bgcBase
            // 
            this.bgcBase.Controls.Add(this.cmbSales);
            this.bgcBase.Controls.Add(this.dteOrderDate);
            this.bgcBase.Controls.Add(this.labOrderDate);
            this.bgcBase.Controls.Add(this.cmbCustomerService);
            this.bgcBase.Controls.Add(this.labelControl50);
            this.bgcBase.Controls.Add(this.cmbFile);
            this.bgcBase.Controls.Add(this.labelControl43);
            this.bgcBase.Controls.Add(this.cmbSalesType);
            this.bgcBase.Controls.Add(this.labelControl47);
            this.bgcBase.Controls.Add(this.cmbBookingMode);
            this.bgcBase.Controls.Add(this.labelControl7);
            this.bgcBase.Controls.Add(this.txtCustomerRefNo);
            this.bgcBase.Controls.Add(this.popupContainerControl1);
            this.bgcBase.Controls.Add(this.txtPOLFilerName);
            this.bgcBase.Controls.Add(this.labelControl6);
            this.bgcBase.Controls.Add(this.treeBoxSalesDep);
            this.bgcBase.Controls.Add(this.labelControl5);
            this.bgcBase.Controls.Add(this.labelControl23);
            this.bgcBase.Controls.Add(this.cmbPaymentTerm);
            this.bgcBase.Controls.Add(this.labelControl4);
            this.bgcBase.Controls.Add(this.labelControl22);
            this.bgcBase.Controls.Add(this.stxtCustomer);
            this.bgcBase.Controls.Add(this.labCustomer);
            this.bgcBase.Controls.Add(this.cmbTransportClause);
            this.bgcBase.Controls.Add(this.cmbCompany);
            this.bgcBase.Controls.Add(this.labelControl1);
            this.bgcBase.Controls.Add(this.cmbTradeTerm);
            this.bgcBase.Controls.Add(this.labTradeTerm);
            this.bgcBase.Controls.Add(this.txtState);
            this.bgcBase.Controls.Add(this.labState);
            this.bgcBase.Controls.Add(this.labNo);
            this.bgcBase.Controls.Add(this.txtNo);
            this.bgcBase.Name = "bgcBase";
            this.bgcBase.Size = new System.Drawing.Size(825, 109);
            this.bgcBase.TabIndex = 0;
            // 
            // cmbSales
            // 
            this.cmbSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSales.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "SalesID", true));
            this.cmbSales.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsBusiness, "SalesName", true));
            this.cmbSales.EditText = "";
            this.cmbSales.EditValue = null;
            this.cmbSales.Location = new System.Drawing.Point(479, 6);
            this.cmbSales.Name = "cmbSales";
            this.cmbSales.ReadOnly = false;
            this.cmbSales.Size = new System.Drawing.Size(119, 21);
            this.cmbSales.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbSales.TabIndex = 8;
            this.cmbSales.ToolTip = "";
            // 
            // bsBusiness
            // 
            this.bsBusiness.DataSource = typeof(ICP.FCM.AirImport.ServiceInterface.AirBusinessInfo);
            // 
            // dteOrderDate
            // 
            this.dteOrderDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "BookingDate", true));
            this.dteOrderDate.EditValue = null;
            this.dteOrderDate.Location = new System.Drawing.Point(479, 83);
            this.dteOrderDate.Name = "dteOrderDate";
            this.dteOrderDate.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.dteOrderDate.Properties.Appearance.Options.UseBackColor = true;
            this.dteOrderDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteOrderDate.Properties.Mask.EditMask = "";
            this.dteOrderDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteOrderDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteOrderDate.Size = new System.Drawing.Size(119, 21);
            this.dteOrderDate.TabIndex = 13;
            // 
            // labOrderDate
            // 
            this.labOrderDate.Location = new System.Drawing.Point(417, 86);
            this.labOrderDate.Name = "labOrderDate";
            this.labOrderDate.Size = new System.Drawing.Size(48, 14);
            this.labOrderDate.TabIndex = 141;
            this.labOrderDate.Text = "委托日期";
            // 
            // cmbCustomerService
            // 
            this.cmbCustomerService.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "CustomerService", true));
            this.cmbCustomerService.Location = new System.Drawing.Point(687, 57);
            this.cmbCustomerService.Name = "cmbCustomerService";
            this.cmbCustomerService.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCustomerService.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCustomerService.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCustomerService.Size = new System.Drawing.Size(120, 21);
            this.cmbCustomerService.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCustomerService.TabIndex = 15;
            // 
            // labelControl50
            // 
            this.labelControl50.Location = new System.Drawing.Point(618, 59);
            this.labelControl50.Name = "labelControl50";
            this.labelControl50.Size = new System.Drawing.Size(24, 14);
            this.labelControl50.TabIndex = 61;
            this.labelControl50.Text = "客服";
            // 
            // cmbFile
            // 
            this.cmbFile.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "FilerId", true));
            this.cmbFile.Location = new System.Drawing.Point(687, 30);
            this.cmbFile.MenuManager = this.barManager1;
            this.cmbFile.Name = "cmbFile";
            this.cmbFile.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbFile.Properties.Appearance.Options.UseBackColor = true;
            this.cmbFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFile.Size = new System.Drawing.Size(120, 21);
            this.cmbFile.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbFile.TabIndex = 14;
            // 
            // labelControl43
            // 
            this.labelControl43.Location = new System.Drawing.Point(618, 32);
            this.labelControl43.Name = "labelControl43";
            this.labelControl43.Size = new System.Drawing.Size(24, 14);
            this.labelControl43.TabIndex = 61;
            this.labelControl43.Text = "文件";
            // 
            // cmbSalesType
            // 
            this.cmbSalesType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "SalesTypeID", true));
            this.cmbSalesType.Location = new System.Drawing.Point(268, 82);
            this.cmbSalesType.Name = "cmbSalesType";
            this.cmbSalesType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbSalesType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbSalesType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSalesType.Size = new System.Drawing.Size(123, 21);
            this.cmbSalesType.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbSalesType.TabIndex = 7;
            // 
            // labelControl47
            // 
            this.labelControl47.Location = new System.Drawing.Point(203, 84);
            this.labelControl47.Name = "labelControl47";
            this.labelControl47.Size = new System.Drawing.Size(48, 14);
            this.labelControl47.TabIndex = 57;
            this.labelControl47.Text = "揽货类型";
            // 
            // cmbBookingMode
            // 
            this.cmbBookingMode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "BookingMode", true));
            this.cmbBookingMode.Location = new System.Drawing.Point(479, 56);
            this.cmbBookingMode.MenuManager = this.barManager1;
            this.cmbBookingMode.Name = "cmbBookingMode";
            this.cmbBookingMode.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbBookingMode.Properties.Appearance.Options.UseBackColor = true;
            this.cmbBookingMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBookingMode.Size = new System.Drawing.Size(119, 21);
            this.cmbBookingMode.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbBookingMode.TabIndex = 12;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(417, 58);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(48, 14);
            this.labelControl7.TabIndex = 57;
            this.labelControl7.Text = "委托方式";
            // 
            // txtCustomerRefNo
            // 
            this.txtCustomerRefNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "CustomerNo", true));
            this.txtCustomerRefNo.Location = new System.Drawing.Point(317, 56);
            this.txtCustomerRefNo.Name = "txtCustomerRefNo";
            this.txtCustomerRefNo.Size = new System.Drawing.Size(74, 21);
            this.txtCustomerRefNo.TabIndex = 5;
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.gridControl1);
            this.popupContainerControl1.Location = new System.Drawing.Point(77, 132);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(653, 178);
            this.popupContainerControl1.TabIndex = 16;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bsRecentTenOrders;
            gridLevelNode1.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(8, 8);
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
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrders});
            // 
            // bsRecentTenOrders
            // 
            this.bsRecentTenOrders.DataSource = typeof(ICP.FCM.AirImport.ServiceInterface.AirOrderInfo);
            // 
            // gvOrders
            // 
            this.gvOrders.ChildGridLevelName = "Level1";
            this.gvOrders.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNo,
            this.colPOLName,
            this.colETD,
            this.colPODName,
            this.colETA,
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
            this.colNo.Caption = "业务号";
            this.colNo.FieldName = "RefNo";
            this.colNo.Name = "colNo";
            this.colNo.OptionsColumn.AllowEdit = false;
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 0;
            this.colNo.Width = 110;
            // 
            // colPOLName
            // 
            this.colPOLName.Caption = "装货港";
            this.colPOLName.FieldName = "POLName";
            this.colPOLName.Name = "colPOLName";
            this.colPOLName.OptionsColumn.AllowEdit = false;
            this.colPOLName.Visible = true;
            this.colPOLName.VisibleIndex = 1;
            this.colPOLName.Width = 100;
            // 
            // colETD
            // 
            this.colETD.Caption = "ETD";
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 2;
            this.colETD.Width = 65;
            // 
            // colPODName
            // 
            this.colPODName.Caption = "卸货港";
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.OptionsColumn.AllowEdit = false;
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 3;
            this.colPODName.Width = 100;
            // 
            // colETA
            // 
            this.colETA.Caption = "ETA";
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 4;
            this.colETA.Width = 65;
            // 
            // colShipperName
            // 
            this.colShipperName.Caption = "发货人";
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.OptionsColumn.AllowEdit = false;
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 5;
            this.colShipperName.Width = 100;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.Caption = "收货人";
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.OptionsColumn.AllowEdit = false;
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 6;
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
            // txtPOLFilerName
            // 
            this.txtPOLFilerName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "POLFilerName", true));
            this.txtPOLFilerName.Location = new System.Drawing.Point(687, 81);
            this.txtPOLFilerName.MenuManager = this.barManager1;
            this.txtPOLFilerName.Name = "txtPOLFilerName";
            this.txtPOLFilerName.Size = new System.Drawing.Size(120, 21);
            this.txtPOLFilerName.TabIndex = 16;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(618, 86);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 55;
            this.labelControl6.Text = "港前客服";
            // 
            // treeBoxSalesDep
            // 
            this.treeBoxSalesDep.AllText = "Selecte ALL";
            this.treeBoxSalesDep.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "SalesDepartmentID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.treeBoxSalesDep.EditValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.treeBoxSalesDep.Location = new System.Drawing.Point(687, 3);
            this.treeBoxSalesDep.Name = "treeBoxSalesDep";
            this.treeBoxSalesDep.ReadOnly = false;
            this.treeBoxSalesDep.Size = new System.Drawing.Size(120, 21);
            this.treeBoxSalesDep.SpecifiedBackColor = System.Drawing.Color.White;
            this.treeBoxSalesDep.TabIndex = 9;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(618, 6);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 53;
            this.labelControl5.Text = "揽货部门";
            // 
            // labelControl23
            // 
            this.labelControl23.Location = new System.Drawing.Point(417, 32);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(48, 14);
            this.labelControl23.TabIndex = 26;
            this.labelControl23.Text = "付款方式";
            // 
            // cmbPaymentTerm
            // 
            this.cmbPaymentTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "PaymentTermID", true));
            this.cmbPaymentTerm.Location = new System.Drawing.Point(479, 29);
            this.cmbPaymentTerm.MenuManager = this.barManager1;
            this.cmbPaymentTerm.Name = "cmbPaymentTerm";
            this.cmbPaymentTerm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbPaymentTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbPaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaymentTerm.Size = new System.Drawing.Size(119, 21);
            this.cmbPaymentTerm.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbPaymentTerm.TabIndex = 11;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(417, 6);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 51;
            this.labelControl4.Text = "揽货人";
            // 
            // labelControl22
            // 
            this.labelControl22.Location = new System.Drawing.Point(6, 85);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(48, 14);
            this.labelControl22.TabIndex = 24;
            this.labelControl22.Text = "运输条款";
            // 
            // stxtCustomer
            // 
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBusiness, "CustomerID", true));
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "CustomerName", true));
            this.stxtCustomer.Location = new System.Drawing.Point(74, 56);
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
            this.stxtCustomer.Size = new System.Drawing.Size(237, 21);
            toolTipTitleItem1.Text = "提示";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "没有业务";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.stxtCustomer.SuperTip = superToolTip1;
            this.stxtCustomer.TabIndex = 4;
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(6, 59);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(65, 14);
            this.labCustomer.TabIndex = 5;
            this.labCustomer.Text = "客户/参考号";
            // 
            // cmbTransportClause
            // 
            this.cmbTransportClause.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "TransportClauseID", true));
            this.cmbTransportClause.Location = new System.Drawing.Point(74, 82);
            this.cmbTransportClause.MenuManager = this.barManager1;
            this.cmbTransportClause.Name = "cmbTransportClause";
            this.cmbTransportClause.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbTransportClause.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTransportClause.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTransportClause.Size = new System.Drawing.Size(119, 21);
            this.cmbTransportClause.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbTransportClause.TabIndex = 10;
            // 
            // cmbCompany
            // 
            this.cmbCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "CompanyID", true));
            this.cmbCompany.Location = new System.Drawing.Point(74, 29);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(119, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbCompany.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "操作口岸";
            // 
            // cmbTradeTerm
            // 
            this.cmbTradeTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "TradeTermID", true));
            this.cmbTradeTerm.Location = new System.Drawing.Point(267, 30);
            this.cmbTradeTerm.MenuManager = this.barManager1;
            this.cmbTradeTerm.Name = "cmbTradeTerm";
            this.cmbTradeTerm.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbTradeTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTradeTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTradeTerm.Size = new System.Drawing.Size(124, 21);
            this.cmbTradeTerm.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbTradeTerm.TabIndex = 6;
            // 
            // labTradeTerm
            // 
            this.labTradeTerm.Location = new System.Drawing.Point(203, 32);
            this.labTradeTerm.Name = "labTradeTerm";
            this.labTradeTerm.Size = new System.Drawing.Size(48, 14);
            this.labTradeTerm.TabIndex = 45;
            this.labTradeTerm.Text = "贸易条款";
            // 
            // txtState
            // 
            this.txtState.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "StateDescription", true));
            this.txtState.Location = new System.Drawing.Point(267, 3);
            this.txtState.Name = "txtState";
            this.txtState.Properties.ReadOnly = true;
            this.txtState.Size = new System.Drawing.Size(124, 21);
            this.txtState.TabIndex = 1;
            // 
            // labState
            // 
            this.labState.Location = new System.Drawing.Point(203, 5);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(24, 14);
            this.labState.TabIndex = 43;
            this.labState.Text = "状态";
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(6, 6);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(36, 14);
            this.labNo.TabIndex = 41;
            this.labNo.Text = "业务号";
            // 
            // txtNo
            // 
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "No", true));
            this.txtNo.EditValue = "";
            this.txtNo.Location = new System.Drawing.Point(74, 3);
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.ReadOnly = true;
            this.txtNo.Size = new System.Drawing.Size(119, 21);
            this.txtNo.TabIndex = 0;
            this.txtNo.TabStop = false;
            // 
            // bgcCom
            // 
            this.bgcCom.Controls.Add(this.labelControl2);
            this.bgcCom.Controls.Add(this.labWarehouseArrivedON);
            this.bgcCom.Controls.Add(this.dtpWarehouseArrivedON);
            this.bgcCom.Controls.Add(this.labStorageStartDate);
            this.bgcCom.Controls.Add(this.dtpStorageStartDate);
            this.bgcCom.Controls.Add(this.groupBox4);
            this.bgcCom.Controls.Add(this.detWareHouseDate);
            this.bgcCom.Controls.Add(this.textEdit1);
            this.bgcCom.Controls.Add(this.labelControl13);
            this.bgcCom.Controls.Add(this.dateEdit4);
            this.bgcCom.Controls.Add(this.txtAgent);
            this.bgcCom.Controls.Add(this.labelControl12);
            this.bgcCom.Controls.Add(this.txtCommodity);
            this.bgcCom.Controls.Add(this.cargoDescriptionPart1);
            this.bgcCom.Controls.Add(this.cmbCargoType);
            this.bgcCom.Controls.Add(this.labelControl53);
            this.bgcCom.Controls.Add(this.labCargoType);
            this.bgcCom.Controls.Add(this.txtCargoDescription);
            this.bgcCom.Controls.Add(this.groupBox1);
            this.bgcCom.Controls.Add(this.labelControl46);
            this.bgcCom.Controls.Add(this.txtAgentNo);
            this.bgcCom.Controls.Add(this.labelControl45);
            this.bgcCom.Controls.Add(this.numWeight);
            this.bgcCom.Controls.Add(this.groupRemark);
            this.bgcCom.Controls.Add(this.labelControl9);
            this.bgcCom.Controls.Add(this.groupBox3);
            this.bgcCom.Controls.Add(this.labelControl19);
            this.bgcCom.Controls.Add(this.numMeasurement);
            this.bgcCom.Controls.Add(this.stxtConsignee);
            this.bgcCom.Controls.Add(this.labelControl33);
            this.bgcCom.Controls.Add(this.labelControl17);
            this.bgcCom.Controls.Add(this.dtpETD);
            this.bgcCom.Controls.Add(this.cmbCustoms);
            this.bgcCom.Controls.Add(this.labelControl10);
            this.bgcCom.Controls.Add(this.dtpETA);
            this.bgcCom.Controls.Add(this.cmbWeightUnit);
            this.bgcCom.Controls.Add(this.labelControl32);
            this.bgcCom.Controls.Add(this.stxtShipper);
            this.bgcCom.Controls.Add(this.labelControl35);
            this.bgcCom.Controls.Add(this.labelControl24);
            this.bgcCom.Controls.Add(this.cmbQuantityunit);
            this.bgcCom.Controls.Add(this.labelControl25);
            this.bgcCom.Controls.Add(this.labelControl11);
            this.bgcCom.Controls.Add(this.labelControl34);
            this.bgcCom.Controls.Add(this.numQuantity);
            this.bgcCom.Controls.Add(this.labDetination);
            this.bgcCom.Controls.Add(this.stxtNotifyParty);
            this.bgcCom.Controls.Add(this.dtpDETA);
            this.bgcCom.Controls.Add(this.labelControl18);
            this.bgcCom.Controls.Add(this.cmbWareHouse);
            this.bgcCom.Controls.Add(this.labelControl26);
            this.bgcCom.Controls.Add(this.cmbMeasurementUnit);
            this.bgcCom.Controls.Add(this.stxtDeparture);
            this.bgcCom.Controls.Add(this.labelControl16);
            this.bgcCom.Controls.Add(this.labelDeparture);
            this.bgcCom.Controls.Add(this.stxtDetination);
            this.bgcCom.Controls.Add(this.labelControl21);
            this.bgcCom.Controls.Add(this.stxtPlaceOfDelivery);
            this.bgcCom.Controls.Add(this.labelControl20);
            this.bgcCom.Controls.Add(this.labelControl3);
            this.bgcCom.Controls.Add(this.labelControl15);
            this.bgcCom.Name = "bgcCom";
            this.bgcCom.Size = new System.Drawing.Size(825, 372);
            this.bgcCom.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(618, 191);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 309;
            this.labelControl2.Text = "入仓时间";
            // 
            // labWarehouseArrivedON
            // 
            this.labWarehouseArrivedON.Location = new System.Drawing.Point(177, 217);
            this.labWarehouseArrivedON.Name = "labWarehouseArrivedON";
            this.labWarehouseArrivedON.Size = new System.Drawing.Size(125, 14);
            this.labWarehouseArrivedON.TabIndex = 308;
            this.labWarehouseArrivedON.Text = "Warehouse Arrived ON";
            // 
            // dtpWarehouseArrivedON
            // 
            this.dtpWarehouseArrivedON.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "WarehouseArrivedON", true));
            this.dtpWarehouseArrivedON.EditValue = null;
            this.dtpWarehouseArrivedON.Location = new System.Drawing.Point(303, 214);
            this.dtpWarehouseArrivedON.MenuManager = this.barManager1;
            this.dtpWarehouseArrivedON.Name = "dtpWarehouseArrivedON";
            this.dtpWarehouseArrivedON.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpWarehouseArrivedON.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpWarehouseArrivedON.Size = new System.Drawing.Size(87, 21);
            this.dtpWarehouseArrivedON.TabIndex = 307;
            // 
            // labStorageStartDate
            // 
            this.labStorageStartDate.Location = new System.Drawing.Point(6, 217);
            this.labStorageStartDate.Name = "labStorageStartDate";
            this.labStorageStartDate.Size = new System.Drawing.Size(72, 14);
            this.labStorageStartDate.TabIndex = 306;
            this.labStorageStartDate.Text = "堆存开始时间";
            // 
            // dtpStorageStartDate
            // 
            this.dtpStorageStartDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "StorageStartDate", true));
            this.dtpStorageStartDate.EditValue = null;
            this.dtpStorageStartDate.Location = new System.Drawing.Point(79, 214);
            this.dtpStorageStartDate.MenuManager = this.barManager1;
            this.dtpStorageStartDate.Name = "dtpStorageStartDate";
            this.dtpStorageStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStorageStartDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpStorageStartDate.Size = new System.Drawing.Size(92, 21);
            this.dtpStorageStartDate.TabIndex = 305;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtpClearanceDate);
            this.groupBox4.Controls.Add(this.ckbIsClearance);
            this.groupBox4.Controls.Add(this.labelControl48);
            this.groupBox4.Controls.Add(this.labelControl49);
            this.groupBox4.Location = new System.Drawing.Point(480, 235);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(328, 30);
            this.groupBox4.TabIndex = 304;
            this.groupBox4.TabStop = false;
            // 
            // dtpClearanceDate
            // 
            this.dtpClearanceDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "ClearanceDate", true));
            this.dtpClearanceDate.EditValue = null;
            this.dtpClearanceDate.Enabled = false;
            this.dtpClearanceDate.Location = new System.Drawing.Point(208, 7);
            this.dtpClearanceDate.Name = "dtpClearanceDate";
            this.dtpClearanceDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpClearanceDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpClearanceDate.Size = new System.Drawing.Size(117, 21);
            this.dtpClearanceDate.TabIndex = 2;
            // 
            // ckbIsClearance
            // 
            this.ckbIsClearance.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "IsClearance", true));
            this.ckbIsClearance.Location = new System.Drawing.Point(4, 9);
            this.ckbIsClearance.Name = "ckbIsClearance";
            this.ckbIsClearance.Properties.Caption = "清关";
            this.ckbIsClearance.Size = new System.Drawing.Size(100, 19);
            this.ckbIsClearance.TabIndex = 0;
            // 
            // labelControl48
            // 
            this.labelControl48.Location = new System.Drawing.Point(617, 13);
            this.labelControl48.Name = "labelControl48";
            this.labelControl48.Size = new System.Drawing.Size(36, 14);
            this.labelControl48.TabIndex = 85;
            this.labelControl48.Text = "清关日";
            // 
            // labelControl49
            // 
            this.labelControl49.Location = new System.Drawing.Point(138, 11);
            this.labelControl49.Name = "labelControl49";
            this.labelControl49.Size = new System.Drawing.Size(36, 14);
            this.labelControl49.TabIndex = 1;
            this.labelControl49.Text = "清关日";
            // 
            // detWareHouseDate
            // 
            this.detWareHouseDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "WareHouseDate", true));
            this.detWareHouseDate.EditValue = null;
            this.detWareHouseDate.Location = new System.Drawing.Point(688, 188);
            this.detWareHouseDate.Name = "detWareHouseDate";
            this.detWareHouseDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.detWareHouseDate.Properties.Appearance.Options.UseBackColor = true;
            this.detWareHouseDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detWareHouseDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.detWareHouseDate.Size = new System.Drawing.Size(119, 21);
            this.detWareHouseDate.TabIndex = 120;
            // 
            // textEdit1
            // 
            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "DOCPickupBy", true));
            this.textEdit1.Location = new System.Drawing.Point(268, 188);
            this.textEdit1.MenuManager = this.barManager1;
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(122, 21);
            this.textEdit1.TabIndex = 123;
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(202, 191);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(13, 14);
            this.labelControl13.TabIndex = 122;
            this.labelControl13.Text = "By";
            // 
            // dateEdit4
            // 
            this.dateEdit4.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "DOCPickupDate", true));
            this.dateEdit4.EditValue = null;
            this.dateEdit4.Location = new System.Drawing.Point(73, 188);
            this.dateEdit4.Name = "dateEdit4";
            this.dateEdit4.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.dateEdit4.Properties.Appearance.Options.UseBackColor = true;
            this.dateEdit4.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit4.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit4.Size = new System.Drawing.Size(120, 21);
            this.dateEdit4.TabIndex = 120;
            // 
            // txtAgent
            // 
            this.txtAgent.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "AgentID", true));
            this.txtAgent.DataSource = null;
            this.txtAgent.DisplayMember = "EName";
            this.txtAgent.EditValue = null;
            this.txtAgent.Location = new System.Drawing.Point(73, 5);
            this.txtAgent.Margin = new System.Windows.Forms.Padding(0);
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.txtAgent.Size = new System.Drawing.Size(204, 21);
            this.txtAgent.SpecifiedBackColor = System.Drawing.Color.White;
            this.txtAgent.TabIndex = 0;
            this.txtAgent.Tag = null;
            this.txtAgent.ValueMember = "ID";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(6, 191);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(60, 14);
            this.labelControl12.TabIndex = 121;
            this.labelControl12.Text = "取文件时间";
            // 
            // txtCommodity
            // 
            this.txtCommodity.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "Commodity", true));
            this.txtCommodity.Location = new System.Drawing.Point(480, 6);
            this.txtCommodity.Name = "txtCommodity";
            this.txtCommodity.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtCommodity.Properties.Appearance.Options.UseBackColor = true;
            this.txtCommodity.Size = new System.Drawing.Size(327, 21);
            this.txtCommodity.TabIndex = 303;
            // 
            // cargoDescriptionPart1
            // 
            this.cargoDescriptionPart1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.cargoDescriptionPart1.Appearance.Options.UseBackColor = true;
            this.cargoDescriptionPart1.Location = new System.Drawing.Point(765, 11);
            this.cargoDescriptionPart1.Name = "cargoDescriptionPart1";
            this.cargoDescriptionPart1.Size = new System.Drawing.Size(42, 21);
            this.cargoDescriptionPart1.TabIndex = 302;
            // 
            // cmbCargoType
            // 
            this.cmbCargoType.Location = new System.Drawing.Point(480, 111);
            this.cmbCargoType.Name = "cmbCargoType";
            this.cmbCargoType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCargoType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCargoType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCargoType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCargoType.Size = new System.Drawing.Size(120, 21);
            this.cmbCargoType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCargoType.TabIndex = 21;
            // 
            // labelControl53
            // 
            this.labelControl53.Location = new System.Drawing.Point(417, 140);
            this.labelControl53.Name = "labelControl53";
            this.labelControl53.Size = new System.Drawing.Size(48, 14);
            this.labelControl53.TabIndex = 301;
            this.labelControl53.Text = "货物描述";
            // 
            // labCargoType
            // 
            this.labCargoType.Location = new System.Drawing.Point(417, 114);
            this.labCargoType.Name = "labCargoType";
            this.labCargoType.Size = new System.Drawing.Size(48, 14);
            this.labCargoType.TabIndex = 301;
            this.labCargoType.Text = "货物类型";
            // 
            // txtCargoDescription
            // 
            this.txtCargoDescription.Location = new System.Drawing.Point(480, 138);
            this.txtCargoDescription.Name = "txtCargoDescription";
            this.txtCargoDescription.Size = new System.Drawing.Size(327, 45);
            this.txtCargoDescription.TabIndex = 22;
            this.txtCargoDescription.TabStop = false;
            this.txtCargoDescription.Properties.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIsCommodityInspection);
            this.groupBox1.Controls.Add(this.chkIsQuarantineInspection);
            this.groupBox1.Controls.Add(this.chkIsTruck);
            this.groupBox1.Controls.Add(this.chkIsWarehouse);
            this.groupBox1.Controls.Add(this.chkIsCustoms);
            this.groupBox1.Location = new System.Drawing.Point(74, 236);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 30);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // chkIsCommodityInspection
            // 
            this.chkIsCommodityInspection.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "IsCommodityInspection", true));
            this.chkIsCommodityInspection.Location = new System.Drawing.Point(199, 9);
            this.chkIsCommodityInspection.Name = "chkIsCommodityInspection";
            this.chkIsCommodityInspection.Properties.Caption = "商检";
            this.chkIsCommodityInspection.Size = new System.Drawing.Size(60, 19);
            this.chkIsCommodityInspection.TabIndex = 1;
            // 
            // chkIsQuarantineInspection
            // 
            this.chkIsQuarantineInspection.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "IsQuarantineInspection", true));
            this.chkIsQuarantineInspection.Location = new System.Drawing.Point(264, 9);
            this.chkIsQuarantineInspection.Name = "chkIsQuarantineInspection";
            this.chkIsQuarantineInspection.Properties.Caption = "质检";
            this.chkIsQuarantineInspection.Size = new System.Drawing.Size(60, 19);
            this.chkIsQuarantineInspection.TabIndex = 3;
            // 
            // chkIsTruck
            // 
            this.chkIsTruck.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "IsTruck", true));
            this.chkIsTruck.Location = new System.Drawing.Point(1, 9);
            this.chkIsTruck.Name = "chkIsTruck";
            this.chkIsTruck.Properties.Caption = "运输";
            this.chkIsTruck.Size = new System.Drawing.Size(60, 19);
            this.chkIsTruck.TabIndex = 0;
            // 
            // chkIsWarehouse
            // 
            this.chkIsWarehouse.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "IsWareHouse", true));
            this.chkIsWarehouse.Location = new System.Drawing.Point(70, 9);
            this.chkIsWarehouse.Name = "chkIsWarehouse";
            this.chkIsWarehouse.Properties.Caption = "仓储";
            this.chkIsWarehouse.Size = new System.Drawing.Size(60, 19);
            this.chkIsWarehouse.TabIndex = 0;
            // 
            // chkIsCustoms
            // 
            this.chkIsCustoms.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "IsCustoms", true));
            this.chkIsCustoms.Location = new System.Drawing.Point(135, 9);
            this.chkIsCustoms.Name = "chkIsCustoms";
            this.chkIsCustoms.Properties.Caption = "报关";
            this.chkIsCustoms.Size = new System.Drawing.Size(60, 19);
            this.chkIsCustoms.TabIndex = 4;
            // 
            // labelControl46
            // 
            this.labelControl46.Location = new System.Drawing.Point(418, 243);
            this.labelControl46.Name = "labelControl46";
            this.labelControl46.Size = new System.Drawing.Size(24, 14);
            this.labelControl46.TabIndex = 108;
            this.labelControl46.Text = "清关";
            // 
            // txtAgentNo
            // 
            this.txtAgentNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "AgentNo", true));
            this.txtAgentNo.Location = new System.Drawing.Point(280, 5);
            this.txtAgentNo.Name = "txtAgentNo";
            this.txtAgentNo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtAgentNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtAgentNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAgentNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAgentNo.Size = new System.Drawing.Size(110, 21);
            this.txtAgentNo.TabIndex = 1;
            // 
            // labelControl45
            // 
            this.labelControl45.Location = new System.Drawing.Point(7, 276);
            this.labelControl45.Name = "labelControl45";
            this.labelControl45.Size = new System.Drawing.Size(48, 14);
            this.labelControl45.TabIndex = 107;
            this.labelControl45.Text = "其它需求";
            // 
            // numWeight
            // 
            this.numWeight.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "Weight", true));
            this.numWeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numWeight.Location = new System.Drawing.Point(480, 59);
            this.numWeight.Name = "numWeight";
            this.numWeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numWeight.Properties.DisplayFormat.FormatString = "N00";
            this.numWeight.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numWeight.Properties.EditFormat.FormatString = "N00";
            this.numWeight.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numWeight.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numWeight.Properties.IsFloatValue = false;
            this.numWeight.Properties.Mask.EditMask = "N00";
            this.numWeight.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.numWeight.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numWeight.Size = new System.Drawing.Size(120, 21);
            this.numWeight.TabIndex = 17;
            // 
            // groupRemark
            // 
            this.groupRemark.Controls.Add(this.txtRemark);
            this.groupRemark.Location = new System.Drawing.Point(2, 304);
            this.groupRemark.Name = "groupRemark";
            this.groupRemark.Size = new System.Drawing.Size(825, 72);
            this.groupRemark.TabIndex = 36;
            this.groupRemark.TabStop = false;
            this.groupRemark.Text = "备注";
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "Remark", true));
            this.txtRemark.Location = new System.Drawing.Point(10, 15);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(797, 50);
            this.txtRemark.TabIndex = 0;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(6, 36);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(36, 14);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "发货人";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtReleaseDate);
            this.groupBox3.Controls.Add(this.labelControl54);
            this.groupBox3.Controls.Add(this.ckbIsTelex);
            this.groupBox3.Controls.Add(this.ckbIsTransport);
            this.groupBox3.Controls.Add(this.ckbIsReleaseNotify);
            this.groupBox3.Location = new System.Drawing.Point(73, 268);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(461, 30);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // txtReleaseDate
            // 
            this.txtReleaseDate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "ReleaseDate", true));
            this.txtReleaseDate.Location = new System.Drawing.Point(126, 7);
            this.txtReleaseDate.MenuManager = this.barManager1;
            this.txtReleaseDate.Name = "txtReleaseDate";
            this.txtReleaseDate.Properties.ReadOnly = true;
            this.txtReleaseDate.Size = new System.Drawing.Size(78, 21);
            this.txtReleaseDate.TabIndex = 3;
            // 
            // labelControl54
            // 
            this.labelControl54.Location = new System.Drawing.Point(73, 12);
            this.labelControl54.Name = "labelControl54";
            this.labelControl54.Size = new System.Drawing.Size(48, 14);
            this.labelControl54.TabIndex = 2;
            this.labelControl54.Text = "放货日期";
            // 
            // ckbIsTelex
            // 
            this.ckbIsTelex.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "IsTelex", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ckbIsTelex.Location = new System.Drawing.Point(2, 9);
            this.ckbIsTelex.Name = "ckbIsTelex";
            this.ckbIsTelex.Properties.Caption = "电放";
            this.ckbIsTelex.Size = new System.Drawing.Size(63, 19);
            this.ckbIsTelex.TabIndex = 0;
            // 
            // ckbIsTransport
            // 
            this.ckbIsTransport.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "IsTransport", true));
            this.ckbIsTransport.Location = new System.Drawing.Point(358, 10);
            this.ckbIsTransport.Name = "ckbIsTransport";
            this.ckbIsTransport.Properties.Caption = "转运";
            this.ckbIsTransport.Size = new System.Drawing.Size(76, 19);
            this.ckbIsTransport.TabIndex = 1;
            // 
            // ckbIsReleaseNotify
            // 
            this.ckbIsReleaseNotify.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "IsReleaseNotify", true));
            this.ckbIsReleaseNotify.Location = new System.Drawing.Point(228, 10);
            this.ckbIsReleaseNotify.Name = "ckbIsReleaseNotify";
            this.ckbIsReleaseNotify.Properties.Caption = "需要放货通知书";
            this.ckbIsReleaseNotify.Size = new System.Drawing.Size(113, 19);
            this.ckbIsReleaseNotify.TabIndex = 0;
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(417, 62);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(24, 14);
            this.labelControl19.TabIndex = 15;
            this.labelControl19.Text = "重量";
            // 
            // numMeasurement
            // 
            this.numMeasurement.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "Measurement", true));
            this.numMeasurement.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numMeasurement.Location = new System.Drawing.Point(480, 85);
            this.numMeasurement.Name = "numMeasurement";
            this.numMeasurement.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMeasurement.Properties.DisplayFormat.FormatString = "N00";
            this.numMeasurement.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numMeasurement.Properties.EditFormat.FormatString = "N00";
            this.numMeasurement.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numMeasurement.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numMeasurement.Properties.IsFloatValue = false;
            this.numMeasurement.Properties.Mask.EditMask = "N00";
            this.numMeasurement.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.numMeasurement.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numMeasurement.Size = new System.Drawing.Size(120, 21);
            this.numMeasurement.TabIndex = 19;
            // 
            // stxtConsignee
            // 
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBusiness, "ConsigneeID", true));
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "ConsigneeName", true));
            this.stxtConsignee.Location = new System.Drawing.Point(73, 59);
            this.stxtConsignee.Name = "stxtConsignee";
            this.stxtConsignee.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtConsignee.Properties.ActionButtonIndex = 1;
            this.stxtConsignee.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.stxtConsignee.Properties.Appearance.Options.UseBackColor = true;
            this.stxtConsignee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtConsignee.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtConsignee.Properties.PopupSizeable = false;
            this.stxtConsignee.Properties.ShowPopupCloseButton = false;
            this.stxtConsignee.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtConsignee.Size = new System.Drawing.Size(317, 21);
            this.stxtConsignee.TabIndex = 3;
            // 
            // labelControl33
            // 
            this.labelControl33.Location = new System.Drawing.Point(202, 140);
            this.labelControl33.Name = "labelControl33";
            this.labelControl33.Size = new System.Drawing.Size(36, 14);
            this.labelControl33.TabIndex = 66;
            this.labelControl33.Text = "到达日";
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(618, 36);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(48, 14);
            this.labelControl17.TabIndex = 14;
            this.labelControl17.Text = "数量单位";
            // 
            // dtpETD
            // 
            this.dtpETD.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "ETD", true));
            this.dtpETD.EditValue = null;
            this.dtpETD.Location = new System.Drawing.Point(268, 111);
            this.dtpETD.MenuManager = this.barManager1;
            this.dtpETD.Name = "dtpETD";
            this.dtpETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpETD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpETD.Size = new System.Drawing.Size(122, 21);
            this.dtpETD.BackColor = System.Drawing.SystemColors.Info;
            this.dtpETD.TabIndex = 7;
            // 
            // cmbCustoms
            // 
            this.cmbCustoms.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBusiness, "CustomsID", true));
            this.cmbCustoms.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "CustomsName", true));
            this.cmbCustoms.EditValue = "";
            this.cmbCustoms.Location = new System.Drawing.Point(481, 214);
            this.cmbCustoms.MenuManager = this.barManager1;
            this.cmbCustoms.Name = "cmbCustoms";
            this.cmbCustoms.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCustoms.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCustoms.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbCustoms.Size = new System.Drawing.Size(327, 21);
            this.cmbCustoms.TabIndex = 24;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(6, 62);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(36, 14);
            this.labelControl10.TabIndex = 2;
            this.labelControl10.Text = "收货人";
            // 
            // dtpETA
            // 
            this.dtpETA.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "ETA", true));
            this.dtpETA.EditValue = null;
            this.dtpETA.Location = new System.Drawing.Point(268, 137);
            this.dtpETA.MenuManager = this.barManager1;
            this.dtpETA.Name = "dtpETA";
            this.dtpETA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpETA.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpETA.Size = new System.Drawing.Size(122, 21);
            this.dtpETA.BackColor = System.Drawing.SystemColors.Info;
            this.dtpETA.TabIndex = 9;
            // 
            // cmbWeightUnit
            // 
            this.cmbWeightUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "WeightUnitID", true));
            this.cmbWeightUnit.Location = new System.Drawing.Point(688, 59);
            this.cmbWeightUnit.MenuManager = this.barManager1;
            this.cmbWeightUnit.Name = "cmbWeightUnit";
            this.cmbWeightUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbWeightUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbWeightUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWeightUnit.Size = new System.Drawing.Size(119, 21);
            this.cmbWeightUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbWeightUnit.TabIndex = 18;
            // 
            // labelControl32
            // 
            this.labelControl32.Location = new System.Drawing.Point(202, 114);
            this.labelControl32.Name = "labelControl32";
            this.labelControl32.Size = new System.Drawing.Size(36, 14);
            this.labelControl32.TabIndex = 64;
            this.labelControl32.Text = "起航日";
            // 
            // stxtShipper
            // 
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBusiness, "ShipperID", true));
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "ShipperName", true));
            this.stxtShipper.Location = new System.Drawing.Point(73, 33);
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
            this.stxtShipper.Size = new System.Drawing.Size(317, 21);
            this.stxtShipper.TabIndex = 2;
            // 
            // labelControl35
            // 
            this.labelControl35.Location = new System.Drawing.Point(6, 165);
            this.labelControl35.Name = "labelControl35";
            this.labelControl35.Size = new System.Drawing.Size(36, 14);
            this.labelControl35.TabIndex = 69;
            this.labelControl35.Text = "交货地";
            // 
            // labelControl24
            // 
            this.labelControl24.Location = new System.Drawing.Point(418, 217);
            this.labelControl24.Name = "labelControl24";
            this.labelControl24.Size = new System.Drawing.Size(36, 14);
            this.labelControl24.TabIndex = 29;
            this.labelControl24.Text = "报关行";
            // 
            // cmbQuantityunit
            // 
            this.cmbQuantityunit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "Quantityunitid", true));
            this.cmbQuantityunit.Location = new System.Drawing.Point(688, 33);
            this.cmbQuantityunit.MenuManager = this.barManager1;
            this.cmbQuantityunit.Name = "cmbQuantityunit";
            this.cmbQuantityunit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbQuantityunit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbQuantityunit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQuantityunit.Size = new System.Drawing.Size(119, 21);
            this.cmbQuantityunit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbQuantityunit.TabIndex = 16;
            // 
            // labelControl25
            // 
            this.labelControl25.Location = new System.Drawing.Point(417, 191);
            this.labelControl25.Name = "labelControl25";
            this.labelControl25.Size = new System.Drawing.Size(24, 14);
            this.labelControl25.TabIndex = 31;
            this.labelControl25.Text = "仓库";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(6, 88);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(36, 14);
            this.labelControl11.TabIndex = 2;
            this.labelControl11.Text = "通知人";
            // 
            // labelControl34
            // 
            this.labelControl34.Location = new System.Drawing.Point(202, 165);
            this.labelControl34.Name = "labelControl34";
            this.labelControl34.Size = new System.Drawing.Size(36, 14);
            this.labelControl34.TabIndex = 70;
            this.labelControl34.Text = "到港日";
            // 
            // numQuantity
            // 
            this.numQuantity.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "Quantity", true));
            this.numQuantity.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numQuantity.Location = new System.Drawing.Point(480, 33);
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
            this.numQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.numQuantity.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numQuantity.Size = new System.Drawing.Size(120, 21);
            this.numQuantity.TabIndex = 15;
            // 
            // labDetination
            // 
            this.labDetination.Location = new System.Drawing.Point(6, 140);
            this.labDetination.Name = "labDetination";
            this.labDetination.Size = new System.Drawing.Size(36, 14);
            this.labDetination.TabIndex = 63;
            this.labDetination.Text = "目的港";
            // 
            // stxtNotifyParty
            // 
            this.stxtNotifyParty.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBusiness, "NotifyPartyID", true));
            this.stxtNotifyParty.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "NotifyPartyName", true));
            this.stxtNotifyParty.Location = new System.Drawing.Point(73, 85);
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
            this.stxtNotifyParty.Size = new System.Drawing.Size(317, 21);
            this.stxtNotifyParty.TabIndex = 4;
            // 
            // dtpDETA
            // 
            this.dtpDETA.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "DETA", true));
            this.dtpDETA.EditValue = null;
            this.dtpDETA.Location = new System.Drawing.Point(268, 162);
            this.dtpDETA.MenuManager = this.barManager1;
            this.dtpDETA.Name = "dtpDETA";
            this.dtpDETA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDETA.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpDETA.Size = new System.Drawing.Size(122, 21);
            this.dtpDETA.TabIndex = 11;
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(618, 62);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(48, 14);
            this.labelControl18.TabIndex = 18;
            this.labelControl18.Text = "重量单位";
            // 
            // cmbWareHouse
            // 
            this.cmbWareHouse.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBusiness, "WareHouseID", true));
            this.cmbWareHouse.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "WareHouseName", true));
            this.cmbWareHouse.EditValue = "";
            this.cmbWareHouse.Location = new System.Drawing.Point(480, 188);
            this.cmbWareHouse.MenuManager = this.barManager1;
            this.cmbWareHouse.Name = "cmbWareHouse";
            this.cmbWareHouse.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbWareHouse.Properties.Appearance.Options.UseBackColor = true;
            this.cmbWareHouse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbWareHouse.Size = new System.Drawing.Size(120, 21);
            this.cmbWareHouse.TabIndex = 23;
            // 
            // labelControl26
            // 
            this.labelControl26.Location = new System.Drawing.Point(6, 245);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(48, 14);
            this.labelControl26.TabIndex = 32;
            this.labelControl26.Text = "本地服务";
            // 
            // cmbMeasurementUnit
            // 
            this.cmbMeasurementUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBusiness, "MeasurementUnitID", true));
            this.cmbMeasurementUnit.Location = new System.Drawing.Point(688, 85);
            this.cmbMeasurementUnit.MenuManager = this.barManager1;
            this.cmbMeasurementUnit.Name = "cmbMeasurementUnit";
            this.cmbMeasurementUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbMeasurementUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMeasurementUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMeasurementUnit.Size = new System.Drawing.Size(119, 21);
            this.cmbMeasurementUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbMeasurementUnit.TabIndex = 20;
            // 
            // stxtDeparture
            // 
            this.stxtDeparture.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBusiness, "POLID", true));
            this.stxtDeparture.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "POLName", true));
            this.stxtDeparture.Location = new System.Drawing.Point(73, 111);
            this.stxtDeparture.Name = "stxtDeparture";
            this.stxtDeparture.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.stxtDeparture.Properties.Appearance.Options.UseBackColor = true;
            this.stxtDeparture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtDeparture.Size = new System.Drawing.Size(120, 21);
            this.stxtDeparture.TabIndex = 6;
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(417, 36);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(24, 14);
            this.labelControl16.TabIndex = 11;
            this.labelControl16.Text = "数量";
            // 
            // labelDeparture
            // 
            this.labelDeparture.Location = new System.Drawing.Point(6, 114);
            this.labelDeparture.Name = "labelDeparture";
            this.labelDeparture.Size = new System.Drawing.Size(36, 14);
            this.labelDeparture.TabIndex = 61;
            this.labelDeparture.Text = "起运港";
            // 
            // stxtDetination
            // 
            this.stxtDetination.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBusiness, "PODID", true));
            this.stxtDetination.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "PODName", true));
            this.stxtDetination.Location = new System.Drawing.Point(73, 137);
            this.stxtDetination.Name = "stxtDetination";
            this.stxtDetination.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.stxtDetination.Properties.Appearance.Options.UseBackColor = true;
            this.stxtDetination.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtDetination.Size = new System.Drawing.Size(120, 21);
            this.stxtDetination.TabIndex = 8;
            // 
            // labelControl21
            // 
            this.labelControl21.Location = new System.Drawing.Point(417, 88);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(24, 14);
            this.labelControl21.TabIndex = 19;
            this.labelControl21.Text = "体积";
            // 
            // stxtPlaceOfDelivery
            // 
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBusiness, "PlaceOfDeliveryID", true));
            this.stxtPlaceOfDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBusiness, "PlaceOfDeliveryName", true));
            this.stxtPlaceOfDelivery.Location = new System.Drawing.Point(73, 162);
            this.stxtPlaceOfDelivery.Name = "stxtPlaceOfDelivery";
            this.stxtPlaceOfDelivery.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.stxtPlaceOfDelivery.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPlaceOfDelivery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPlaceOfDelivery.Size = new System.Drawing.Size(120, 21);
            this.stxtPlaceOfDelivery.TabIndex = 10;
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(618, 88);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(48, 14);
            this.labelControl20.TabIndex = 22;
            this.labelControl20.Text = "体积单位";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(6, 9);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(65, 14);
            this.labelControl3.TabIndex = 49;
            this.labelControl3.Text = "代理/参考号";
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(417, 9);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(24, 14);
            this.labelControl15.TabIndex = 9;
            this.labelControl15.Text = "品名";
            // 
            // bgcHBLList
            // 
            this.bgcHBLList.Controls.Add(this.UCHBLList);
            this.bgcHBLList.Name = "bgcHBLList";
            this.bgcHBLList.Size = new System.Drawing.Size(825, 105);
            this.bgcHBLList.TabIndex = 4;
            // 
            // UCHBLList
            // 
            this.UCHBLList.BaseMultiLanguageList = null;
            this.UCHBLList.BasePartList = null;
            this.UCHBLList.BusinessID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UCHBLList.CodeValuePairs = null;
            this.UCHBLList.ControlsList = null;
            this.UCHBLList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UCHBLList.FormName = "UCBusinessBLList";
            this.UCHBLList.IsChanged = false;
            this.UCHBLList.IsMultiLanguage = true;
            this.UCHBLList.Location = new System.Drawing.Point(0, 0);
            this.UCHBLList.Name = "UCHBLList";
            this.UCHBLList.Resources = null;
            this.UCHBLList.Size = new System.Drawing.Size(825, 105);
            this.UCHBLList.TabIndex = 0;
            this.UCHBLList.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("UCHBLList.UsedMessages")));
            // 
            // bgcFee
            // 
            this.bgcFee.Controls.Add(this.UCOIOrderFeeEdit);
            this.bgcFee.Name = "bgcFee";
            this.bgcFee.Size = new System.Drawing.Size(825, 240);
            this.bgcFee.TabIndex = 6;
            // 
            // UCOIOrderFeeEdit
            // 
            this.UCOIOrderFeeEdit.DefaultCustomerID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UCOIOrderFeeEdit.DefaultCustomerName = null;
            this.UCOIOrderFeeEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UCOIOrderFeeEdit.FormType = null;
            this.UCOIOrderFeeEdit.Location = new System.Drawing.Point(0, 0);
            this.UCOIOrderFeeEdit.Name = "UCOIOrderFeeEdit";
            this.UCOIOrderFeeEdit.Size = new System.Drawing.Size(825, 240);
            this.UCOIOrderFeeEdit.TabIndex = 0;
            // 
            // bgcMBL
            // 
            this.bgcMBL.Controls.Add(this.txtFlightCountry);
            this.bgcMBL.Controls.Add(this.txtFlightFlag);
            this.bgcMBL.Controls.Add(this.labFlgflag);
            this.bgcMBL.Controls.Add(this.labelControl39);
            this.bgcMBL.Controls.Add(this.stxtFinalWareHouse);
            this.bgcMBL.Controls.Add(this.txtGODate);
            this.bgcMBL.Controls.Add(this.labGODate);
            this.bgcMBL.Controls.Add(this.txtManifestNO);
            this.bgcMBL.Controls.Add(this.labManifestNO);
            this.bgcMBL.Controls.Add(this.labelControl40);
            this.bgcMBL.Controls.Add(this.cmbMBLTransportClause);
            this.bgcMBL.Controls.Add(this.txtSubNo);
            this.bgcMBL.Controls.Add(this.labelControl52);
            this.bgcMBL.Controls.Add(this.cmbFlightNo);
            this.bgcMBL.Controls.Add(this.labelControl51);
            this.bgcMBL.Controls.Add(this.stxtMBLNo);
            this.bgcMBL.Controls.Add(this.labFlightNo);
            this.bgcMBL.Controls.Add(this.txtITPalce);
            this.bgcMBL.Controls.Add(this.dtpITDate);
            this.bgcMBL.Controls.Add(this.stxtAgentOfCarrier);
            this.bgcMBL.Controls.Add(this.labelControl29);
            this.bgcMBL.Controls.Add(this.labelControl42);
            this.bgcMBL.Controls.Add(this.mcmbAirCompany);
            this.bgcMBL.Controls.Add(this.labelControl44);
            this.bgcMBL.Controls.Add(this.labelControl28);
            this.bgcMBL.Controls.Add(this.labelControl41);
            this.bgcMBL.Controls.Add(this.cmbReleaseType);
            this.bgcMBL.Controls.Add(this.labelControl27);
            this.bgcMBL.Controls.Add(this.txtITNo);
            this.bgcMBL.Name = "bgcMBL";
            this.bgcMBL.Size = new System.Drawing.Size(825, 133);
            this.bgcMBL.TabIndex = 7;
            // 
            // txtFlightCountry
            // 
            this.txtFlightCountry.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMBLInfo, "FlightCountry", true));
            this.txtFlightCountry.Location = new System.Drawing.Point(199, 107);
            this.txtFlightCountry.Name = "txtFlightCountry";
            this.txtFlightCountry.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtFlightCountry.Properties.Appearance.Options.UseBackColor = true;
            this.txtFlightCountry.Size = new System.Drawing.Size(191, 21);
            this.txtFlightCountry.TabIndex = 320;
            // 
            // bsMBLInfo
            // 
            this.bsMBLInfo.DataSource = typeof(ICP.FCM.AirImport.ServiceInterface.AirBusinessMBLInfo);
            // 
            // txtFlightFlag
            // 
            this.txtFlightFlag.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMBLInfo, "FlightFlag", true));
            this.txtFlightFlag.Location = new System.Drawing.Point(98, 107);
            this.txtFlightFlag.Name = "txtFlightFlag";
            this.txtFlightFlag.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtFlightFlag.Properties.Appearance.Options.UseBackColor = true;
            this.txtFlightFlag.Size = new System.Drawing.Size(96, 21);
            this.txtFlightFlag.TabIndex = 318;
            // 
            // labFlgflag
            // 
            this.labFlgflag.Location = new System.Drawing.Point(7, 110);
            this.labFlgflag.Name = "labFlgflag";
            this.labFlgflag.Size = new System.Drawing.Size(85, 14);
            this.labFlgflag.TabIndex = 319;
            this.labFlgflag.Text = "Flg flag/country";
            // 
            // labelControl39
            // 
            this.labelControl39.Location = new System.Drawing.Point(417, 32);
            this.labelControl39.Name = "labelControl39";
            this.labelControl39.Size = new System.Drawing.Size(36, 14);
            this.labelControl39.TabIndex = 311;
            this.labelControl39.Text = "提货地";
            // 
            // stxtFinalWareHouse
            // 
            this.stxtFinalWareHouse.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMBLInfo, "FinalWareHouseName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtFinalWareHouse.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsMBLInfo, "FinalWareHouseID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtFinalWareHouse.Location = new System.Drawing.Point(486, 29);
            this.stxtFinalWareHouse.Name = "stxtFinalWareHouse";
            this.stxtFinalWareHouse.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtFinalWareHouse.Properties.Appearance.Options.UseBackColor = true;
            this.stxtFinalWareHouse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtFinalWareHouse.Size = new System.Drawing.Size(321, 21);
            this.stxtFinalWareHouse.TabIndex = 310;
            // 
            // txtGODate
            // 
            this.txtGODate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsMBLInfo, "GODate", true));
            this.txtGODate.EditValue = null;
            this.txtGODate.Location = new System.Drawing.Point(688, 3);
            this.txtGODate.Name = "txtGODate";
            this.txtGODate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtGODate.Properties.Appearance.Options.UseBackColor = true;
            this.txtGODate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtGODate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtGODate.Size = new System.Drawing.Size(120, 21);
            this.txtGODate.TabIndex = 308;
            // 
            // labGODate
            // 
            this.labGODate.Location = new System.Drawing.Point(619, 7);
            this.labGODate.Name = "labGODate";
            this.labGODate.Size = new System.Drawing.Size(55, 14);
            this.labGODate.TabIndex = 309;
            this.labGODate.Text = "G.O. Date";
            // 
            // txtManifestNO
            // 
            this.txtManifestNO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMBLInfo, "ManifestNO", true));
            this.txtManifestNO.Location = new System.Drawing.Point(486, 3);
            this.txtManifestNO.Name = "txtManifestNO";
            this.txtManifestNO.Size = new System.Drawing.Size(116, 21);
            this.txtManifestNO.TabIndex = 307;
            // 
            // labManifestNO
            // 
            this.labManifestNO.Location = new System.Drawing.Point(417, 6);
            this.labManifestNO.Name = "labManifestNO";
            this.labManifestNO.Size = new System.Drawing.Size(66, 14);
            this.labManifestNO.TabIndex = 306;
            this.labManifestNO.Text = "Manifest NO";
            // 
            // labelControl40
            // 
            this.labelControl40.Location = new System.Drawing.Point(417, 84);
            this.labelControl40.Name = "labelControl40";
            this.labelControl40.Size = new System.Drawing.Size(36, 14);
            this.labelControl40.TabIndex = 57;
            this.labelControl40.Text = "转关号";
            // 
            // cmbMBLTransportClause
            // 
            this.cmbMBLTransportClause.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsMBLInfo, "MBLTransportClauseID", true));
            this.cmbMBLTransportClause.Location = new System.Drawing.Point(688, 107);
            this.cmbMBLTransportClause.MenuManager = this.barManager1;
            this.cmbMBLTransportClause.Name = "cmbMBLTransportClause";
            this.cmbMBLTransportClause.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbMBLTransportClause.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMBLTransportClause.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMBLTransportClause.Size = new System.Drawing.Size(119, 21);
            this.cmbMBLTransportClause.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbMBLTransportClause.TabIndex = 12;
            // 
            // txtSubNo
            // 
            this.txtSubNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMBLInfo, "SubNo", true));
            this.txtSubNo.Location = new System.Drawing.Point(269, 3);
            this.txtSubNo.MenuManager = this.barManager1;
            this.txtSubNo.Name = "txtSubNo";
            this.txtSubNo.Size = new System.Drawing.Size(122, 21);
            this.txtSubNo.TabIndex = 1;
            // 
            // labelControl52
            // 
            this.labelControl52.Location = new System.Drawing.Point(619, 110);
            this.labelControl52.Name = "labelControl52";
            this.labelControl52.Size = new System.Drawing.Size(48, 14);
            this.labelControl52.TabIndex = 113;
            this.labelControl52.Text = "运输条款";
            // 
            // cmbFlightNo
            // 
            this.cmbFlightNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsMBLInfo, "FlightID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbFlightNo.EditText = "";
            this.cmbFlightNo.EditValue = null;
            this.cmbFlightNo.Location = new System.Drawing.Point(74, 81);
            this.cmbFlightNo.Name = "cmbFlightNo";
            this.cmbFlightNo.ReadOnly = false;
            this.cmbFlightNo.Size = new System.Drawing.Size(318, 21);
            this.cmbFlightNo.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbFlightNo.TabIndex = 674;
            this.cmbFlightNo.ToolTip = "";
            // 
            // labelControl51
            // 
            this.labelControl51.Location = new System.Drawing.Point(203, 6);
            this.labelControl51.Name = "labelControl51";
            this.labelControl51.Size = new System.Drawing.Size(48, 14);
            this.labelControl51.TabIndex = 111;
            this.labelControl51.Text = "子提单号";
            // 
            // stxtMBLNo
            // 
            this.stxtMBLNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMBLInfo, "MBLNo", true));
            this.stxtMBLNo.EditValue = "";
            this.stxtMBLNo.Location = new System.Drawing.Point(74, 3);
            this.stxtMBLNo.Name = "stxtMBLNo";
            this.stxtMBLNo.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.stxtMBLNo.Properties.Appearance.Options.UseBackColor = true;
            this.stxtMBLNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtMBLNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.stxtMBLNo.Size = new System.Drawing.Size(119, 21);
            this.stxtMBLNo.TabIndex = 0;
            // 
            // labFlightNo
            // 
            this.labFlightNo.Location = new System.Drawing.Point(7, 84);
            this.labFlightNo.Name = "labFlightNo";
            this.labFlightNo.Size = new System.Drawing.Size(36, 14);
            this.labFlightNo.TabIndex = 75;
            this.labFlightNo.Text = "航班号";
            // 
            // txtITPalce
            // 
            this.txtITPalce.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMBLInfo, "ITPalce", true));
            this.txtITPalce.Location = new System.Drawing.Point(486, 55);
            this.txtITPalce.Name = "txtITPalce";
            this.txtITPalce.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtITPalce.Properties.Appearance.Options.UseBackColor = true;
            this.txtITPalce.Size = new System.Drawing.Size(321, 21);
            this.txtITPalce.TabIndex = 10;
            // 
            // dtpITDate
            // 
            this.dtpITDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsMBLInfo, "ITDate", true));
            this.dtpITDate.EditValue = null;
            this.dtpITDate.Location = new System.Drawing.Point(688, 81);
            this.dtpITDate.Name = "dtpITDate";
            this.dtpITDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.dtpITDate.Properties.Appearance.Options.UseBackColor = true;
            this.dtpITDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpITDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpITDate.Size = new System.Drawing.Size(119, 21);
            this.dtpITDate.TabIndex = 9;
            // 
            // stxtAgentOfCarrier
            // 
            this.stxtAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMBLInfo, "AgentOfCarrierName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsMBLInfo, "AgentOfCarrierID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtAgentOfCarrier.Location = new System.Drawing.Point(74, 55);
            this.stxtAgentOfCarrier.Name = "stxtAgentOfCarrier";
            this.stxtAgentOfCarrier.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.stxtAgentOfCarrier.Properties.Appearance.Options.UseBackColor = true;
            this.stxtAgentOfCarrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtAgentOfCarrier.Size = new System.Drawing.Size(318, 21);
            this.stxtAgentOfCarrier.TabIndex = 3;
            // 
            // labelControl29
            // 
            this.labelControl29.Location = new System.Drawing.Point(7, 58);
            this.labelControl29.Name = "labelControl29";
            this.labelControl29.Size = new System.Drawing.Size(36, 14);
            this.labelControl29.TabIndex = 59;
            this.labelControl29.Text = "承运人";
            // 
            // labelControl42
            // 
            this.labelControl42.Location = new System.Drawing.Point(417, 58);
            this.labelControl42.Name = "labelControl42";
            this.labelControl42.Size = new System.Drawing.Size(48, 14);
            this.labelControl42.TabIndex = 57;
            this.labelControl42.Text = "转关地点";
            // 
            // mcmbAirCompany
            // 
            this.mcmbAirCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mcmbAirCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsMBLInfo, "AirCompanyID", true));
            this.mcmbAirCompany.EditText = "";
            this.mcmbAirCompany.EditValue = null;
            this.mcmbAirCompany.Location = new System.Drawing.Point(74, 28);
            this.mcmbAirCompany.Name = "mcmbAirCompany";
            this.mcmbAirCompany.ReadOnly = false;
            this.mcmbAirCompany.Size = new System.Drawing.Size(318, 21);
            this.mcmbAirCompany.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.mcmbAirCompany.TabIndex = 2;
            this.mcmbAirCompany.ToolTip = "";
            // 
            // labelControl44
            // 
            this.labelControl44.Location = new System.Drawing.Point(417, 110);
            this.labelControl44.Name = "labelControl44";
            this.labelControl44.Size = new System.Drawing.Size(48, 14);
            this.labelControl44.TabIndex = 84;
            this.labelControl44.Text = "放货类型";
            // 
            // labelControl28
            // 
            this.labelControl28.Location = new System.Drawing.Point(7, 32);
            this.labelControl28.Name = "labelControl28";
            this.labelControl28.Size = new System.Drawing.Size(48, 14);
            this.labelControl28.TabIndex = 59;
            this.labelControl28.Text = "航空公司";
            // 
            // labelControl41
            // 
            this.labelControl41.Location = new System.Drawing.Point(620, 85);
            this.labelControl41.Name = "labelControl41";
            this.labelControl41.Size = new System.Drawing.Size(36, 14);
            this.labelControl41.TabIndex = 70;
            this.labelControl41.Text = "转关日";
            // 
            // cmbReleaseType
            // 
            this.cmbReleaseType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsMBLInfo, "ReleaseType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbReleaseType.Location = new System.Drawing.Point(486, 107);
            this.cmbReleaseType.MenuManager = this.barManager1;
            this.cmbReleaseType.Name = "cmbReleaseType";
            this.cmbReleaseType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbReleaseType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbReleaseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReleaseType.Size = new System.Drawing.Size(116, 21);
            this.cmbReleaseType.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbReleaseType.TabIndex = 11;
            // 
            // labelControl27
            // 
            this.labelControl27.Location = new System.Drawing.Point(7, 6);
            this.labelControl27.Name = "labelControl27";
            this.labelControl27.Size = new System.Drawing.Size(48, 14);
            this.labelControl27.TabIndex = 57;
            this.labelControl27.Text = "主提单号";
            // 
            // txtITNo
            // 
            this.txtITNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMBLInfo, "ITNo", true));
            this.txtITNo.Location = new System.Drawing.Point(486, 82);
            this.txtITNo.Name = "txtITNo";
            this.txtITNo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtITNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtITNo.Size = new System.Drawing.Size(116, 21);
            this.txtITNo.TabIndex = 8;
            // 
            // bgCom
            // 
            this.bgCom.Caption = "委托信息";
            this.bgCom.ControlContainer = this.bgcCom;
            this.bgCom.Expanded = true;
            this.bgCom.GroupClientHeight = 374;
            this.bgCom.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgCom.Name = "bgCom";
            // 
            // bgMBL
            // 
            this.bgMBL.Caption = "MAWB信息";
            this.bgMBL.ControlContainer = this.bgcMBL;
            this.bgMBL.Expanded = true;
            this.bgMBL.GroupClientHeight = 135;
            this.bgMBL.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgMBL.Name = "bgMBL";
            // 
            // bgHBLList
            // 
            this.bgHBLList.Caption = "HAWB列表信息";
            this.bgHBLList.ControlContainer = this.bgcHBLList;
            this.bgHBLList.Expanded = true;
            this.bgHBLList.GroupClientHeight = 107;
            this.bgHBLList.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgHBLList.Name = "bgHBLList";
            // 
            // bgFee
            // 
            this.bgFee.Caption = "费用信息";
            this.bgFee.ControlContainer = this.bgcFee;
            this.bgFee.Expanded = true;
            this.bgFee.GroupClientHeight = 242;
            this.bgFee.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgFee.Name = "bgFee";
            // 
            // bar1
            // 
            this.bar1.BarName = "Main menu";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSaveAs, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAuditor, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBill, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Main menu";
            // 
            // bar3
            // 
            this.bar3.BarName = "Main menu";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSaveAs, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAuditor, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBill, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar3.OptionsBar.MultiLine = true;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Main menu";
            // 
            // barManager2
            // 
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.Form = this;
            this.barManager2.MaxItemId = 11;
            // 
            // barDockControl1
            // 
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Size = new System.Drawing.Size(900, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 733);
            this.barDockControl2.Size = new System.Drawing.Size(900, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 0);
            this.barDockControl3.Size = new System.Drawing.Size(0, 733);
            // 
            // barDockControl4
            // 
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(900, 0);
            this.barDockControl4.Size = new System.Drawing.Size(0, 733);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.TabMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 26);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(900, 707);
            this.pnlMain.TabIndex = 71;
            // 
            // OIBusinessEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "OIBusinessEdit";
            this.Size = new System.Drawing.Size(900, 733);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TabMain)).EndInit();
            this.TabMain.ResumeLayout(false);
            this.TabBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bcMain)).EndInit();
            this.bcMain.ResumeLayout(false);
            this.bgcBase.ResumeLayout(false);
            this.bgcBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsBusiness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteOrderDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteOrderDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerService.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBookingMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerRefNo.Properties)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtPOLFilerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTradeTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            this.bgcCom.ResumeLayout(false);
            this.bgcCom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpWarehouseArrivedON.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpWarehouseArrivedON.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStorageStartDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStorageStartDate.Properties)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpClearanceDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpClearanceDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsClearance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detWareHouseDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detWareHouseDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit4.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCargoType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCargoDescription.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCommodityInspection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsQuarantineInspection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTruck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsWarehouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCustoms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgentNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight.Properties)).EndInit();
            this.groupRemark.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReleaseDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsTelex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsTransport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsReleaseNotify.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpETD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpETD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustoms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpETA.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuantityunit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtNotifyParty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDETA.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWareHouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDeparture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDetination.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPlaceOfDelivery.Properties)).EndInit();
            this.bgcHBLList.ResumeLayout(false);
            this.bgcFee.ResumeLayout(false);
            this.bgcMBL.ResumeLayout(false);
            this.bgcMBL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlightCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMBLInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlightFlag.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtFinalWareHouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGODate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGODate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManifestNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMBLTransportClause.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtMBLNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITPalce.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpITDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpITDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgentOfCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barBill;
        private DevExpress.XtraBars.BarSubItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barPrintArrivalNotice;
        private DevExpress.XtraBars.BarButtonItem barReleaseOrder;
        private DevExpress.XtraBars.BarButtonItem barPrintProfit;
        private DevExpress.XtraBars.BarButtonItem barPrintAuthority;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarButtonItem barSaveAs;
        private DevExpress.XtraBars.BarButtonItem barAuditor;
        private DevExpress.XtraTab.XtraTabControl TabMain;
        private DevExpress.XtraTab.XtraTabPage TabBase;
        private DevExpress.XtraNavBar.NavBarControl bcMain;
        private DevExpress.XtraNavBar.NavBarGroup bgMain;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcBase;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbBookingMode;
        private DevExpress.XtraEditors.TextEdit txtPOLFilerName;
        private ICP.Framework.ClientComponents.Controls.TreeSelectBox treeBoxSalesDep;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbPaymentTerm;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.PopupContainerEdit stxtCustomer;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTransportClause;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTradeTerm;
        private DevExpress.XtraEditors.LabelControl labTradeTerm;
        private DevExpress.XtraEditors.TextEdit txtState;
        private DevExpress.XtraEditors.LabelControl labState;
        private DevExpress.XtraEditors.LabelControl labNo;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.LabelControl labelControl27;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbReleaseType;
        private DevExpress.XtraEditors.LabelControl labelControl28;
        private DevExpress.XtraEditors.LabelControl labelControl44;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbAirCompany;
        private DevExpress.XtraEditors.LabelControl labelControl29;
        private DevExpress.XtraEditors.ButtonEdit stxtAgentOfCarrier;
        private DevExpress.XtraEditors.TextEdit txtITPalce;
        private DevExpress.XtraEditors.LabelControl labFlightNo;
        private DevExpress.XtraEditors.DateEdit dtpITDate;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox cmbFlightNo;
        private DevExpress.XtraEditors.LabelControl labelControl41;
        private DevExpress.XtraEditors.LabelControl labelControl42;
        private DevExpress.XtraEditors.TextEdit txtITNo;
        private DevExpress.XtraEditors.LabelControl labelControl40;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcHBLList;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcFee;
        private OIOrderFeeEdit UCOIOrderFeeEdit;
        private DevExpress.XtraNavBar.NavBarGroup bgCom;
        private DevExpress.XtraNavBar.NavBarGroup bgHBLList;
        private DevExpress.XtraNavBar.NavBarGroup bgFee;

        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private UCBusinessBLList UCHBLList;
        private DevExpress.XtraEditors.XtraScrollableControl pnlMain;
        private DevExpress.XtraEditors.TextEdit txtCustomerRefNo;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbSalesType;
        private DevExpress.XtraEditors.LabelControl labelControl47;
        private DevExpress.XtraEditors.LabelControl labelControl51;
        private DevExpress.XtraEditors.TextEdit txtSubNo;
        private DevExpress.XtraEditors.ComboBoxEdit stxtMBLNo;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMBLTransportClause;
        private DevExpress.XtraEditors.LabelControl labelControl52;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbFile;
        private DevExpress.XtraNavBar.NavBarGroup bgMBL;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcMBL;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcCom;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.CheckEdit chkIsCommodityInspection;
        private DevExpress.XtraEditors.CheckEdit chkIsQuarantineInspection;
        private DevExpress.XtraEditors.CheckEdit chkIsTruck;
        private DevExpress.XtraEditors.CheckEdit chkIsWarehouse;
        private DevExpress.XtraEditors.CheckEdit chkIsCustoms;
        private DevExpress.XtraEditors.LabelControl labelControl46;
        private DevExpress.XtraEditors.TextEdit txtAgentNo;
        private DevExpress.XtraEditors.LabelControl labelControl45;
        private DevExpress.XtraEditors.SpinEdit numWeight;
        private System.Windows.Forms.GroupBox groupRemark;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.CheckEdit ckbIsTelex;
        private DevExpress.XtraEditors.CheckEdit ckbIsTransport;
        private DevExpress.XtraEditors.CheckEdit ckbIsReleaseNotify;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.SpinEdit numMeasurement;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtConsignee;
        private DevExpress.XtraEditors.LabelControl labelControl33;
        private DevExpress.XtraEditors.DateEdit dtpETD;
        private DevExpress.XtraEditors.ButtonEdit cmbCustoms;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.DateEdit dtpETA;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbWeightUnit;
        private DevExpress.XtraEditors.LabelControl labelControl32;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtShipper;
        private DevExpress.XtraEditors.LabelControl labelControl35;
        private DevExpress.XtraEditors.LabelControl labelControl24;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbQuantityunit;
        private DevExpress.XtraEditors.LabelControl labelControl25;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl34;
        private DevExpress.XtraEditors.SpinEdit numQuantity;
        private DevExpress.XtraEditors.LabelControl labDetination;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtNotifyParty;
        private DevExpress.XtraEditors.DateEdit dtpDETA;
        private DevExpress.XtraEditors.ButtonEdit cmbWareHouse;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMeasurementUnit;
        private DevExpress.XtraEditors.ButtonEdit stxtDeparture;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl labelDeparture;
        private DevExpress.XtraEditors.ButtonEdit stxtDetination;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.ButtonEdit stxtPlaceOfDelivery;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl43;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.DateEdit dteOrderDate;
        private DevExpress.XtraEditors.LabelControl labOrderDate;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCustomerService;
        private DevExpress.XtraEditors.LabelControl labelControl50;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCargoType;
        private DevExpress.XtraEditors.LabelControl labCargoType;
        private DevExpress.XtraEditors.MemoEdit txtCargoDescription;
        private DevExpress.XtraEditors.LabelControl labelControl53;
        private System.Windows.Forms.BindingSource bsBusiness;
        private System.Windows.Forms.BindingSource bsMBLInfo;
        private DevExpress.XtraEditors.MemoEdit txtCommodity;
        private CargoDescriptionPart cargoDescriptionPart1;
        private BindingSource bsRecentTenOrders;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOrders;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbQtyUnit;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox2;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox3;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rspinEditInt;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rspinEditFloat;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem barReturn;
        private ICP.Framework.ClientComponents.Controls.ComboCustomerDescriptionControl txtAgent;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox cmbSales;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraGrid.Columns.GridColumn colETA;
        private DevExpress.XtraGrid.Columns.GridColumn colETD;
        private DevExpress.XtraEditors.TextEdit txtReleaseDate;
        private DevExpress.XtraEditors.LabelControl labelControl54;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.DateEdit dateEdit4;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.DateEdit detWareHouseDate;
        private DevExpress.XtraEditors.TextEdit txtManifestNO;
        private DevExpress.XtraEditors.LabelControl labManifestNO;
        private GroupBox groupBox4;
        private DevExpress.XtraEditors.DateEdit dtpClearanceDate;
        private DevExpress.XtraEditors.CheckEdit ckbIsClearance;
        private DevExpress.XtraEditors.LabelControl labelControl48;
        private DevExpress.XtraEditors.LabelControl labelControl49;
        private DevExpress.XtraEditors.DateEdit txtGODate;
        private DevExpress.XtraEditors.LabelControl labGODate;
        private DevExpress.XtraEditors.LabelControl labelControl39;
        private DevExpress.XtraEditors.ButtonEdit stxtFinalWareHouse;
        private DevExpress.XtraEditors.TextEdit txtFlightCountry;
        private DevExpress.XtraEditors.TextEdit txtFlightFlag;
        private DevExpress.XtraEditors.LabelControl labFlgflag;
        private DevExpress.XtraEditors.LabelControl labWarehouseArrivedON;
        private DevExpress.XtraEditors.DateEdit dtpWarehouseArrivedON;
        private DevExpress.XtraEditors.LabelControl labStorageStartDate;
        private DevExpress.XtraEditors.DateEdit dtpStorageStartDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}
