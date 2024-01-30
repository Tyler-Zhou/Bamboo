using System.Windows.Forms;
namespace ICP.FCM.OtherBusiness.UI.ECommerce
{
    partial class OBECEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OBECEditPart));
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.bsOtherInfo = new System.Windows.Forms.BindingSource(this.components);
            this.stxtCustomer = new DevExpress.XtraEditors.PopupContainerEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.barTruck = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.txtState = new DevExpress.XtraEditors.TextEdit();
            this.stxtAgentOfCarrier = new DevExpress.XtraEditors.ButtonEdit();
            this.txtGoods = new DevExpress.XtraEditors.TextEdit();
            this.stxtDeparture = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtDetination = new DevExpress.XtraEditors.ButtonEdit();
            this.lwPackages = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.spinPackages = new DevExpress.XtraEditors.SpinEdit();
            this.cmbWeightUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.spinWeight = new DevExpress.XtraEditors.SpinEdit();
            this.cmbMeasurementUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.spinMeasurement = new DevExpress.XtraEditors.SpinEdit();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtDepartureName = new DevExpress.XtraEditors.TextEdit();
            this.stxtDetinationName = new DevExpress.XtraEditors.TextEdit();
            this.treeOperation = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtOperationNo = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtConsignee = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.tabPageBase = new DevExpress.XtraTab.XtraTabPage();
            this.panelTabBaseInfo = new DevExpress.XtraEditors.PanelControl();
            this.navBarOrderInfo = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroupBaseInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelBaseInfo = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.cmbCurrency = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtWarehouseNo = new DevExpress.XtraEditors.TextEdit();
            this.txtTransferNo = new DevExpress.XtraEditors.TextEdit();
            this.txtExpressNo = new DevExpress.XtraEditors.TextEdit();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.memoRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.labOperation = new DevExpress.XtraEditors.LabelControl();
            this.labState = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labRevenueTon = new DevExpress.XtraEditors.LabelControl();
            this.labCostCurrency = new DevExpress.XtraEditors.LabelControl();
            this.labCost = new DevExpress.XtraEditors.LabelControl();
            this.labMeasurement = new DevExpress.XtraEditors.LabelControl();
            this.labAgentOfCarrier = new DevExpress.XtraEditors.LabelControl();
            this.spinRevenueTon = new DevExpress.XtraEditors.SpinEdit();
            this.spinCost = new DevExpress.XtraEditors.SpinEdit();
            this.labGoods = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.lblWarehouseNo = new DevExpress.XtraEditors.LabelControl();
            this.lblTransferNo = new DevExpress.XtraEditors.LabelControl();
            this.lblExpressNo = new DevExpress.XtraEditors.LabelControl();
            this.labPackages = new DevExpress.XtraEditors.LabelControl();
            this.labSalse = new DevExpress.XtraEditors.LabelControl();
            this.mcmbSales = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labBusinessDate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.dteBookingDate = new DevExpress.XtraEditors.DateEdit();
            this.tsbSalesDep = new ICP.Framework.ClientComponents.Controls.TreeSelectBox();
            this.labDeparture = new DevExpress.XtraEditors.LabelControl();
            this.labDetination = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.partDelegate = new ICP.FCM.Common.UI.CommonPart.PartBookingForCSP();
            this.navBarGroupCSPBooking = new DevExpress.XtraNavBar.NavBarGroup();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOtherInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgentOfCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoods.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDeparture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDetination.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lwPackages.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPackages.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMeasurement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDepartureName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDetinationName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeOperation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtOperationNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).BeginInit();
            this.tabPageBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelTabBaseInfo)).BeginInit();
            this.panelTabBaseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarOrderInfo)).BeginInit();
            this.navBarOrderInfo.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelBaseInfo)).BeginInit();
            this.panelBaseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarehouseNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTransferNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRevenueTon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // txtNo
            // 
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "NO", true));
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "NO", true));
            this.txtNo.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtNo.Location = new System.Drawing.Point(106, 5);
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.ReadOnly = true;
            this.txtNo.Size = new System.Drawing.Size(120, 21);
            this.txtNo.TabIndex = 29;
            this.txtNo.TabStop = false;
            // 
            // bsOtherInfo
            // 
            this.bsOtherInfo.DataSource = typeof(ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.OtherBusinessInfo);
            // 
            // stxtCustomer
            // 
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "CustomerID", true));
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "CustomerName", true));
            this.dxErrorProvider1.SetIconAlignment(this.stxtCustomer, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtCustomer.Location = new System.Drawing.Point(106, 86);
            this.stxtCustomer.MenuManager = this.barManager1;
            this.stxtCustomer.Name = "stxtCustomer";
            this.stxtCustomer.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.stxtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Close, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtCustomer.Properties.CloseOnLostFocus = false;
            this.stxtCustomer.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            this.stxtCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtCustomer.ShowToolTips = false;
            this.stxtCustomer.Size = new System.Drawing.Size(336, 21);
            toolTipTitleItem1.Text = "提示";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "没有业务";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.stxtCustomer.SuperTip = superToolTip1;
            this.stxtCustomer.TabIndex = 4;
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
            this.barRefresh,
            this.barButtonItem1,
            this.barTruck,
            this.barButtonItem2});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 10;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSaveAs, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barTruck, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem2, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
            // barSaveAs
            // 
            this.barSaveAs.Caption = "Save&As";
            this.barSaveAs.Glyph = ((System.Drawing.Image)(resources.GetObject("barSaveAs.Glyph")));
            this.barSaveAs.Id = 1;
            this.barSaveAs.Name = "barSaveAs";
            this.barSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSaveAs_ItemClick);
            // 
            // barTruck
            // 
            this.barTruck.Caption = "Truck";
            this.barTruck.Id = 8;
            this.barTruck.Name = "barTruck";
            this.barTruck.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barTruck_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "&Print";
            this.barPrint.Glyph = ((System.Drawing.Image)(resources.GetObject("barPrint.Glyph")));
            this.barPrint.Id = 2;
            this.barPrint.Name = "barPrint";
            this.barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("barRefresh.Glyph")));
            this.barRefresh.Id = 6;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Bill";
            this.barButtonItem2.Glyph = global::ICP.FCM.OtherBusiness.UI.Properties.Resources.Memo_16;
            this.barButtonItem2.Id = 9;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FCM.OtherBusiness.UI.Properties.Resources.Left_D_16;
            this.barClose.Id = 5;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1200, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 600);
            this.barDockControlBottom.Size = new System.Drawing.Size(1200, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 574);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1200, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 574);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Preview";
            this.barButtonItem1.Id = 7;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // txtState
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtState, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtState.Location = new System.Drawing.Point(322, 5);
            this.txtState.Name = "txtState";
            this.txtState.Properties.ReadOnly = true;
            this.txtState.Size = new System.Drawing.Size(120, 21);
            this.txtState.TabIndex = 32;
            this.txtState.TabStop = false;
            // 
            // stxtAgentOfCarrier
            // 
            this.stxtAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "AgentofCarrierID", true));
            this.stxtAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "AgengofCarrierName", true));
            this.stxtAgentOfCarrier.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtAgentOfCarrier, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtAgentOfCarrier.Location = new System.Drawing.Point(106, 140);
            this.stxtAgentOfCarrier.Name = "stxtAgentOfCarrier";
            this.stxtAgentOfCarrier.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtAgentOfCarrier.Properties.Appearance.Options.UseBackColor = true;
            this.stxtAgentOfCarrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtAgentOfCarrier.Size = new System.Drawing.Size(336, 21);
            this.stxtAgentOfCarrier.TabIndex = 9;
            // 
            // txtGoods
            // 
            this.txtGoods.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "Commodity", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtGoods, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtGoods.Location = new System.Drawing.Point(573, 140);
            this.txtGoods.Name = "txtGoods";
            this.txtGoods.Size = new System.Drawing.Size(354, 21);
            this.txtGoods.TabIndex = 18;
            this.txtGoods.TabStop = false;
            // 
            // stxtDeparture
            // 
            this.stxtDeparture.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "PolID", true));
            this.stxtDeparture.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "PolName", true));
            this.stxtDeparture.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtDeparture, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtDeparture.Location = new System.Drawing.Point(573, 5);
            this.stxtDeparture.Name = "stxtDeparture";
            this.stxtDeparture.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtDeparture.Properties.Appearance.Options.UseBackColor = true;
            this.stxtDeparture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtDeparture.Size = new System.Drawing.Size(120, 21);
            this.stxtDeparture.TabIndex = 11;
            // 
            // stxtDetination
            // 
            this.stxtDetination.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "PodID", true));
            this.stxtDetination.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "PodName", true));
            this.stxtDetination.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtDetination, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtDetination.Location = new System.Drawing.Point(573, 32);
            this.stxtDetination.Name = "stxtDetination";
            this.stxtDetination.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtDetination.Properties.Appearance.Options.UseBackColor = true;
            this.stxtDetination.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtDetination.Size = new System.Drawing.Size(120, 21);
            this.stxtDetination.TabIndex = 13;
            // 
            // lwPackages
            // 
            this.lwPackages.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "QuantityUnitID", true));
            this.dxErrorProvider1.SetIconAlignment(this.lwPackages, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lwPackages.Location = new System.Drawing.Point(698, 194);
            this.lwPackages.Name = "lwPackages";
            this.lwPackages.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lwPackages.Properties.Appearance.Options.UseBackColor = true;
            this.lwPackages.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lwPackages.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.lwPackages.Size = new System.Drawing.Size(227, 21);
            this.lwPackages.SpecifiedBackColor = System.Drawing.Color.White;
            this.lwPackages.TabIndex = 20;
            // 
            // spinPackages
            // 
            this.spinPackages.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "Quantity", true));
            this.spinPackages.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dxErrorProvider1.SetIconAlignment(this.spinPackages, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.spinPackages.Location = new System.Drawing.Point(573, 194);
            this.spinPackages.Name = "spinPackages";
            this.spinPackages.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinPackages.Properties.DisplayFormat.FormatString = "F3";
            this.spinPackages.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinPackages.Properties.EditFormat.FormatString = "F3";
            this.spinPackages.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinPackages.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinPackages.Properties.Mask.EditMask = "F3";
            this.spinPackages.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.spinPackages.Size = new System.Drawing.Size(120, 21);
            this.spinPackages.TabIndex = 19;
            // 
            // cmbWeightUnit
            // 
            this.cmbWeightUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "WeightUnitID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbWeightUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbWeightUnit.Location = new System.Drawing.Point(698, 221);
            this.cmbWeightUnit.Name = "cmbWeightUnit";
            this.cmbWeightUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbWeightUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbWeightUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWeightUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbWeightUnit.Size = new System.Drawing.Size(227, 21);
            this.cmbWeightUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbWeightUnit.TabIndex = 22;
            // 
            // spinWeight
            // 
            this.spinWeight.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "Weight", true));
            this.spinWeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dxErrorProvider1.SetIconAlignment(this.spinWeight, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.spinWeight.Location = new System.Drawing.Point(573, 221);
            this.spinWeight.Name = "spinWeight";
            this.spinWeight.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.spinWeight.Properties.Appearance.Options.UseBackColor = true;
            this.spinWeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinWeight.Properties.DisplayFormat.FormatString = "F3";
            this.spinWeight.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinWeight.Properties.EditFormat.FormatString = "F3";
            this.spinWeight.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinWeight.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinWeight.Properties.Mask.EditMask = "F3";
            this.spinWeight.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.spinWeight.Size = new System.Drawing.Size(120, 21);
            this.spinWeight.TabIndex = 21;
            // 
            // cmbMeasurementUnit
            // 
            this.cmbMeasurementUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "MeasurementUnitID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbMeasurementUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbMeasurementUnit.Location = new System.Drawing.Point(699, 245);
            this.cmbMeasurementUnit.Name = "cmbMeasurementUnit";
            this.cmbMeasurementUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbMeasurementUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMeasurementUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMeasurementUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbMeasurementUnit.Size = new System.Drawing.Size(227, 21);
            this.cmbMeasurementUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbMeasurementUnit.TabIndex = 24;
            // 
            // spinMeasurement
            // 
            this.spinMeasurement.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "Measurement", true));
            this.spinMeasurement.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dxErrorProvider1.SetIconAlignment(this.spinMeasurement, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.spinMeasurement.Location = new System.Drawing.Point(573, 245);
            this.spinMeasurement.Name = "spinMeasurement";
            this.spinMeasurement.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.spinMeasurement.Properties.Appearance.Options.UseBackColor = true;
            this.spinMeasurement.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinMeasurement.Properties.DisplayFormat.FormatString = "F3";
            this.spinMeasurement.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinMeasurement.Properties.EditFormat.FormatString = "F3";
            this.spinMeasurement.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinMeasurement.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinMeasurement.Properties.Mask.EditMask = "F3";
            this.spinMeasurement.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.spinMeasurement.Size = new System.Drawing.Size(120, 21);
            this.spinMeasurement.TabIndex = 23;
            // 
            // cmbCompany
            // 
            this.cmbCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "CompanyID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbCompany, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbCompany.Location = new System.Drawing.Point(106, 32);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCompany.Size = new System.Drawing.Size(120, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCompany.TabIndex = 0;
            // 
            // stxtDepartureName
            // 
            this.stxtDepartureName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "PolName", true));
            this.dxErrorProvider1.SetIconAlignment(this.stxtDepartureName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtDepartureName.Location = new System.Drawing.Point(700, 5);
            this.stxtDepartureName.Name = "stxtDepartureName";
            this.stxtDepartureName.Size = new System.Drawing.Size(227, 21);
            this.stxtDepartureName.TabIndex = 12;
            this.stxtDepartureName.TabStop = false;
            // 
            // stxtDetinationName
            // 
            this.stxtDetinationName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "PodName", true));
            this.dxErrorProvider1.SetIconAlignment(this.stxtDetinationName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtDetinationName.Location = new System.Drawing.Point(700, 32);
            this.stxtDetinationName.Name = "stxtDetinationName";
            this.stxtDetinationName.Size = new System.Drawing.Size(227, 21);
            this.stxtDetinationName.TabIndex = 14;
            this.stxtDetinationName.TabStop = false;
            // 
            // treeOperation
            // 
            this.treeOperation.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "OperatorID", true));
            this.dxErrorProvider1.SetIconAlignment(this.treeOperation, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.treeOperation.Location = new System.Drawing.Point(106, 59);
            this.treeOperation.Name = "treeOperation";
            this.treeOperation.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.treeOperation.Properties.Appearance.Options.UseBackColor = true;
            this.treeOperation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.treeOperation.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.treeOperation.Size = new System.Drawing.Size(120, 21);
            this.treeOperation.SpecifiedBackColor = System.Drawing.Color.White;
            this.treeOperation.TabIndex = 2;
            // 
            // cmbType
            // 
            this.cmbType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "OtOperationType", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbType.Location = new System.Drawing.Point(322, 32);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbType.Size = new System.Drawing.Size(120, 21);
            this.cmbType.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbType.TabIndex = 1;
            // 
            // stxtOperationNo
            // 
            this.stxtOperationNo.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "OperationID", true));
            this.stxtOperationNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "OperationNo", true));
            this.stxtOperationNo.EditValue = "";
            this.stxtOperationNo.Enabled = false;
            this.dxErrorProvider1.SetIconAlignment(this.stxtOperationNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtOperationNo.Location = new System.Drawing.Point(106, 194);
            this.stxtOperationNo.Name = "stxtOperationNo";
            this.stxtOperationNo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtOperationNo.Properties.Appearance.Options.UseBackColor = true;
            this.stxtOperationNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtOperationNo.Size = new System.Drawing.Size(336, 21);
            this.stxtOperationNo.TabIndex = 10;
            // 
            // stxtConsignee
            // 
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "ConsigneeID", true));
            this.stxtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "ConsigneeName", true));
            this.dxErrorProvider1.SetIconAlignment(this.stxtConsignee, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtConsignee.Location = new System.Drawing.Point(107, 167);
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
            this.stxtConsignee.Size = new System.Drawing.Size(335, 21);
            this.stxtConsignee.TabIndex = 720;
            // 
            // tabPageBase
            // 
            this.tabPageBase.Controls.Add(this.panelTabBaseInfo);
            this.tabPageBase.Name = "tabPageBase";
            this.tabPageBase.Size = new System.Drawing.Size(1170, 567);
            this.tabPageBase.Text = "Base Info";
            // 
            // panelTabBaseInfo
            // 
            this.panelTabBaseInfo.Controls.Add(this.navBarOrderInfo);
            this.panelTabBaseInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTabBaseInfo.Location = new System.Drawing.Point(0, 0);
            this.panelTabBaseInfo.Name = "panelTabBaseInfo";
            this.panelTabBaseInfo.Size = new System.Drawing.Size(1170, 567);
            this.panelTabBaseInfo.TabIndex = 5;
            // 
            // navBarOrderInfo
            // 
            this.navBarOrderInfo.ActiveGroup = this.navBarGroupBaseInfo;
            this.navBarOrderInfo.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarOrderInfo.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarOrderInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarOrderInfo.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroupBaseInfo,
            this.navBarGroupCSPBooking});
            this.navBarOrderInfo.Location = new System.Drawing.Point(2, 2);
            this.navBarOrderInfo.Name = "navBarOrderInfo";
            this.navBarOrderInfo.OptionsNavPane.ExpandedWidth = 810;
            this.navBarOrderInfo.Size = new System.Drawing.Size(964, 563);
            this.navBarOrderInfo.SkinExplorerBarViewScrollStyle = DevExpress.XtraNavBar.SkinExplorerBarViewScrollStyle.ScrollBar;
            this.navBarOrderInfo.TabIndex = 3;
            this.navBarOrderInfo.Text = "Order";
            // 
            // navBarGroupBaseInfo
            // 
            this.navBarGroupBaseInfo.Caption = "Base Info";
            this.navBarGroupBaseInfo.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarGroupBaseInfo.Expanded = true;
            this.navBarGroupBaseInfo.GroupClientHeight = 391;
            this.navBarGroupBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroupBaseInfo.Name = "navBarGroupBaseInfo";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.panelBaseInfo);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(943, 389);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // panelBaseInfo
            // 
            this.panelBaseInfo.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panelBaseInfo.Appearance.Options.UseBackColor = true;
            this.panelBaseInfo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelBaseInfo.Controls.Add(this.stxtConsignee);
            this.panelBaseInfo.Controls.Add(this.labelControl1);
            this.panelBaseInfo.Controls.Add(this.stxtOperationNo);
            this.panelBaseInfo.Controls.Add(this.labelControl9);
            this.panelBaseInfo.Controls.Add(this.cmbCurrency);
            this.panelBaseInfo.Controls.Add(this.txtGoods);
            this.panelBaseInfo.Controls.Add(this.txtWarehouseNo);
            this.panelBaseInfo.Controls.Add(this.txtTransferNo);
            this.panelBaseInfo.Controls.Add(this.txtExpressNo);
            this.panelBaseInfo.Controls.Add(this.txtNo);
            this.panelBaseInfo.Controls.Add(this.cmbType);
            this.panelBaseInfo.Controls.Add(this.treeOperation);
            this.panelBaseInfo.Controls.Add(this.labType);
            this.panelBaseInfo.Controls.Add(this.memoRemark);
            this.panelBaseInfo.Controls.Add(this.labCustomer);
            this.panelBaseInfo.Controls.Add(this.labRemark);
            this.panelBaseInfo.Controls.Add(this.labNo);
            this.panelBaseInfo.Controls.Add(this.labOperation);
            this.panelBaseInfo.Controls.Add(this.stxtCustomer);
            this.panelBaseInfo.Controls.Add(this.stxtDetinationName);
            this.panelBaseInfo.Controls.Add(this.labState);
            this.panelBaseInfo.Controls.Add(this.stxtDepartureName);
            this.panelBaseInfo.Controls.Add(this.txtState);
            this.panelBaseInfo.Controls.Add(this.labCompany);
            this.panelBaseInfo.Controls.Add(this.cmbCompany);
            this.panelBaseInfo.Controls.Add(this.labRevenueTon);
            this.panelBaseInfo.Controls.Add(this.labCostCurrency);
            this.panelBaseInfo.Controls.Add(this.labCost);
            this.panelBaseInfo.Controls.Add(this.labMeasurement);
            this.panelBaseInfo.Controls.Add(this.cmbMeasurementUnit);
            this.panelBaseInfo.Controls.Add(this.labAgentOfCarrier);
            this.panelBaseInfo.Controls.Add(this.spinRevenueTon);
            this.panelBaseInfo.Controls.Add(this.spinCost);
            this.panelBaseInfo.Controls.Add(this.spinMeasurement);
            this.panelBaseInfo.Controls.Add(this.stxtAgentOfCarrier);
            this.panelBaseInfo.Controls.Add(this.cmbWeightUnit);
            this.panelBaseInfo.Controls.Add(this.labGoods);
            this.panelBaseInfo.Controls.Add(this.labelControl7);
            this.panelBaseInfo.Controls.Add(this.lblWarehouseNo);
            this.panelBaseInfo.Controls.Add(this.lblTransferNo);
            this.panelBaseInfo.Controls.Add(this.lblExpressNo);
            this.panelBaseInfo.Controls.Add(this.spinWeight);
            this.panelBaseInfo.Controls.Add(this.lwPackages);
            this.panelBaseInfo.Controls.Add(this.labPackages);
            this.panelBaseInfo.Controls.Add(this.labSalse);
            this.panelBaseInfo.Controls.Add(this.spinPackages);
            this.panelBaseInfo.Controls.Add(this.mcmbSales);
            this.panelBaseInfo.Controls.Add(this.labBusinessDate);
            this.panelBaseInfo.Controls.Add(this.labelControl5);
            this.panelBaseInfo.Controls.Add(this.dteBookingDate);
            this.panelBaseInfo.Controls.Add(this.tsbSalesDep);
            this.panelBaseInfo.Controls.Add(this.stxtDetination);
            this.panelBaseInfo.Controls.Add(this.labDeparture);
            this.panelBaseInfo.Controls.Add(this.labDetination);
            this.panelBaseInfo.Controls.Add(this.stxtDeparture);
            this.panelBaseInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBaseInfo.Location = new System.Drawing.Point(0, 0);
            this.panelBaseInfo.Name = "panelBaseInfo";
            this.panelBaseInfo.Size = new System.Drawing.Size(943, 389);
            this.panelBaseInfo.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(7, 170);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(56, 14);
            this.labelControl1.TabIndex = 721;
            this.labelControl1.Text = "Consignee";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(7, 197);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(45, 14);
            this.labelControl9.TabIndex = 719;
            this.labelControl9.Text = "Business";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "CurrencyID", true));
            this.cmbCurrency.Location = new System.Drawing.Point(107, 221);
            this.cmbCurrency.MenuManager = this.barManager1;
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCurrency.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Size = new System.Drawing.Size(335, 21);
            this.cmbCurrency.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCurrency.TabIndex = 26;
            // 
            // txtWarehouseNo
            // 
            this.txtWarehouseNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "Mblno", true));
            this.txtWarehouseNo.Location = new System.Drawing.Point(573, 86);
            this.txtWarehouseNo.Name = "txtWarehouseNo";
            this.txtWarehouseNo.Size = new System.Drawing.Size(354, 21);
            this.txtWarehouseNo.TabIndex = 16;
            this.txtWarehouseNo.TabStop = false;
            // 
            // txtTransferNo
            // 
            this.txtTransferNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "Hblno", true));
            this.txtTransferNo.Location = new System.Drawing.Point(573, 113);
            this.txtTransferNo.Name = "txtTransferNo";
            this.txtTransferNo.Size = new System.Drawing.Size(354, 21);
            this.txtTransferNo.TabIndex = 17;
            this.txtTransferNo.TabStop = false;
            // 
            // txtExpressNo
            // 
            this.txtExpressNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "ExpressNo", true));
            this.txtExpressNo.Location = new System.Drawing.Point(573, 59);
            this.txtExpressNo.Name = "txtExpressNo";
            this.txtExpressNo.Size = new System.Drawing.Size(354, 21);
            this.txtExpressNo.TabIndex = 15;
            this.txtExpressNo.TabStop = false;
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(243, 35);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(28, 14);
            this.labType.TabIndex = 0;
            this.labType.Text = "Type";
            // 
            // memoRemark
            // 
            this.memoRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "Remark", true));
            this.memoRemark.Location = new System.Drawing.Point(105, 291);
            this.memoRemark.MenuManager = this.barManager1;
            this.memoRemark.Name = "memoRemark";
            this.memoRemark.Size = new System.Drawing.Size(820, 94);
            this.memoRemark.TabIndex = 26;
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(7, 89);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 0;
            this.labCustomer.Text = "Customer";
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(6, 291);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(40, 14);
            this.labRemark.TabIndex = 717;
            this.labRemark.Text = "Remark";
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(7, 8);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(17, 14);
            this.labNo.TabIndex = 0;
            this.labNo.Text = "NO";
            // 
            // labOperation
            // 
            this.labOperation.Location = new System.Drawing.Point(7, 62);
            this.labOperation.Name = "labOperation";
            this.labOperation.Size = new System.Drawing.Size(54, 14);
            this.labOperation.TabIndex = 708;
            this.labOperation.Text = "Operation";
            // 
            // labState
            // 
            this.labState.Location = new System.Drawing.Point(243, 8);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(30, 14);
            this.labState.TabIndex = 39;
            this.labState.Text = "State";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(7, 35);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 696;
            this.labCompany.Text = "Company";
            // 
            // labRevenueTon
            // 
            this.labRevenueTon.Location = new System.Drawing.Point(699, 170);
            this.labRevenueTon.Name = "labRevenueTon";
            this.labRevenueTon.Size = new System.Drawing.Size(70, 14);
            this.labRevenueTon.TabIndex = 693;
            this.labRevenueTon.Text = "RevenueTon";
            // 
            // labCostCurrency
            // 
            this.labCostCurrency.Location = new System.Drawing.Point(7, 224);
            this.labCostCurrency.Name = "labCostCurrency";
            this.labCostCurrency.Size = new System.Drawing.Size(76, 14);
            this.labCostCurrency.TabIndex = 693;
            this.labCostCurrency.Text = "Cost Currency";
            // 
            // labCost
            // 
            this.labCost.Location = new System.Drawing.Point(476, 170);
            this.labCost.Name = "labCost";
            this.labCost.Size = new System.Drawing.Size(24, 14);
            this.labCost.TabIndex = 693;
            this.labCost.Text = "Cost";
            // 
            // labMeasurement
            // 
            this.labMeasurement.Location = new System.Drawing.Point(476, 248);
            this.labMeasurement.Name = "labMeasurement";
            this.labMeasurement.Size = new System.Drawing.Size(74, 14);
            this.labMeasurement.TabIndex = 693;
            this.labMeasurement.Text = "Measurement";
            // 
            // labAgentOfCarrier
            // 
            this.labAgentOfCarrier.Location = new System.Drawing.Point(7, 143);
            this.labAgentOfCarrier.Name = "labAgentOfCarrier";
            this.labAgentOfCarrier.Size = new System.Drawing.Size(81, 14);
            this.labAgentOfCarrier.TabIndex = 662;
            this.labAgentOfCarrier.Text = "AgentOfCarrier";
            // 
            // spinRevenueTon
            // 
            this.spinRevenueTon.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "RevenueTon", true));
            this.spinRevenueTon.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinRevenueTon.Location = new System.Drawing.Point(805, 167);
            this.spinRevenueTon.Name = "spinRevenueTon";
            this.spinRevenueTon.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.spinRevenueTon.Properties.Appearance.Options.UseBackColor = true;
            this.spinRevenueTon.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinRevenueTon.Properties.DisplayFormat.FormatString = "F3";
            this.spinRevenueTon.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinRevenueTon.Properties.EditFormat.FormatString = "F3";
            this.spinRevenueTon.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinRevenueTon.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinRevenueTon.Properties.Mask.EditMask = "F3";
            this.spinRevenueTon.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.spinRevenueTon.Size = new System.Drawing.Size(120, 21);
            this.spinRevenueTon.TabIndex = 25;
            // 
            // spinCost
            // 
            this.spinCost.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "CostAmount", true));
            this.spinCost.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinCost.Location = new System.Drawing.Point(573, 167);
            this.spinCost.Name = "spinCost";
            this.spinCost.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.spinCost.Properties.Appearance.Options.UseBackColor = true;
            this.spinCost.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinCost.Properties.DisplayFormat.FormatString = "F3";
            this.spinCost.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinCost.Properties.EditFormat.FormatString = "F3";
            this.spinCost.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinCost.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinCost.Properties.Mask.EditMask = "F3";
            this.spinCost.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.spinCost.Size = new System.Drawing.Size(120, 21);
            this.spinCost.TabIndex = 25;
            // 
            // labGoods
            // 
            this.labGoods.Location = new System.Drawing.Point(478, 143);
            this.labGoods.Name = "labGoods";
            this.labGoods.Size = new System.Drawing.Size(34, 14);
            this.labGoods.TabIndex = 667;
            this.labGoods.Text = "Goods";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(476, 224);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(40, 14);
            this.labelControl7.TabIndex = 690;
            this.labelControl7.Text = "Weight";
            // 
            // lblWarehouseNo
            // 
            this.lblWarehouseNo.Location = new System.Drawing.Point(478, 89);
            this.lblWarehouseNo.Name = "lblWarehouseNo";
            this.lblWarehouseNo.Size = new System.Drawing.Size(81, 14);
            this.lblWarehouseNo.TabIndex = 667;
            this.lblWarehouseNo.Text = "Warehouse No";
            // 
            // lblTransferNo
            // 
            this.lblTransferNo.Location = new System.Drawing.Point(485, 116);
            this.lblTransferNo.Name = "lblTransferNo";
            this.lblTransferNo.Size = new System.Drawing.Size(64, 14);
            this.lblTransferNo.TabIndex = 667;
            this.lblTransferNo.Text = "Transfer No";
            // 
            // lblExpressNo
            // 
            this.lblExpressNo.Location = new System.Drawing.Point(478, 62);
            this.lblExpressNo.Name = "lblExpressNo";
            this.lblExpressNo.Size = new System.Drawing.Size(60, 14);
            this.lblExpressNo.TabIndex = 667;
            this.lblExpressNo.Text = "Express No";
            // 
            // labPackages
            // 
            this.labPackages.Location = new System.Drawing.Point(476, 197);
            this.labPackages.Name = "labPackages";
            this.labPackages.Size = new System.Drawing.Size(50, 14);
            this.labPackages.TabIndex = 687;
            this.labPackages.Text = "Packages";
            // 
            // labSalse
            // 
            this.labSalse.Location = new System.Drawing.Point(7, 116);
            this.labSalse.Name = "labSalse";
            this.labSalse.Size = new System.Drawing.Size(27, 14);
            this.labSalse.TabIndex = 669;
            this.labSalse.Text = "Sales";
            // 
            // mcmbSales
            // 
            this.mcmbSales.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsOtherInfo, "SalesName", true));
            this.mcmbSales.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "SalesID", true));
            this.mcmbSales.EditText = "";
            this.mcmbSales.EditValue = null;
            this.mcmbSales.Location = new System.Drawing.Point(106, 113);
            this.mcmbSales.Name = "mcmbSales";
            this.mcmbSales.ReadOnly = false;
            this.mcmbSales.RefreshButtonToolTip = "";
            this.mcmbSales.ShowRefreshButton = false;
            this.mcmbSales.Size = new System.Drawing.Size(120, 21);
            this.mcmbSales.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbSales.TabIndex = 5;
            this.mcmbSales.ToolTip = "";
            this.mcmbSales.EditValueChanged += new System.EventHandler(this.cmbSales_EditValueChanged);
            // 
            // labBusinessDate
            // 
            this.labBusinessDate.Location = new System.Drawing.Point(243, 62);
            this.labBusinessDate.Name = "labBusinessDate";
            this.labBusinessDate.Size = new System.Drawing.Size(71, 14);
            this.labBusinessDate.TabIndex = 685;
            this.labBusinessDate.Text = "BusinessDate";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(243, 116);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(53, 14);
            this.labelControl5.TabIndex = 671;
            this.labelControl5.Text = "Sales Dep";
            // 
            // dteBookingDate
            // 
            this.dteBookingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "OperationDate", true));
            this.dteBookingDate.EditValue = null;
            this.dteBookingDate.Location = new System.Drawing.Point(322, 59);
            this.dteBookingDate.Name = "dteBookingDate";
            this.dteBookingDate.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.dteBookingDate.Properties.Appearance.Options.UseBackColor = true;
            this.dteBookingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteBookingDate.Properties.Mask.EditMask = "";
            this.dteBookingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteBookingDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteBookingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteBookingDate.Size = new System.Drawing.Size(120, 21);
            this.dteBookingDate.TabIndex = 3;
            this.dteBookingDate.TabStop = false;
            // 
            // tsbSalesDep
            // 
            this.tsbSalesDep.AllText = "Selecte ALL";
            this.tsbSalesDep.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "SalesDepartmentID", true));
            this.tsbSalesDep.EditValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.tsbSalesDep.Location = new System.Drawing.Point(322, 113);
            this.tsbSalesDep.Name = "tsbSalesDep";
            this.tsbSalesDep.OnlyLeafNodeCanSelect = false;
            this.tsbSalesDep.ReadOnly = false;
            this.tsbSalesDep.Size = new System.Drawing.Size(120, 21);
            this.tsbSalesDep.SpecifiedBackColor = System.Drawing.Color.White;
            this.tsbSalesDep.TabIndex = 6;
            // 
            // labDeparture
            // 
            this.labDeparture.Location = new System.Drawing.Point(478, 8);
            this.labDeparture.Name = "labDeparture";
            this.labDeparture.Size = new System.Drawing.Size(55, 14);
            this.labDeparture.TabIndex = 673;
            this.labDeparture.Text = "Departure";
            // 
            // labDetination
            // 
            this.labDetination.Location = new System.Drawing.Point(478, 35);
            this.labDetination.Name = "labDetination";
            this.labDetination.Size = new System.Drawing.Size(56, 14);
            this.labDetination.TabIndex = 675;
            this.labDetination.Text = "Detination";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.partDelegate);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(943, 138);
            this.navBarGroupControlContainer2.TabIndex = 1;
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
            this.partDelegate.Size = new System.Drawing.Size(943, 138);
            this.partDelegate.TabIndex = 3;
            this.partDelegate.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("partDelegate.UsedMessages")));
            // 
            // navBarGroupCSPBooking
            // 
            this.navBarGroupCSPBooking.Caption = "CSP Booking";
            this.navBarGroupCSPBooking.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarGroupCSPBooking.Expanded = true;
            this.navBarGroupCSPBooking.GroupClientHeight = 140;
            this.navBarGroupCSPBooking.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroupCSPBooking.Name = "navBarGroupCSPBooking";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 26);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabPageBase;
            this.xtraTabControl1.Size = new System.Drawing.Size(1200, 574);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageBase});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Tools";
            // 
            // bar4
            // 
            this.bar4.BarName = "Status bar";
            this.bar4.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar4.DockCol = 0;
            this.bar4.DockRow = 0;
            this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar4.OptionsBar.AllowQuickCustomization = false;
            this.bar4.OptionsBar.DrawDragBorder = false;
            this.bar4.OptionsBar.UseWholeRow = true;
            this.bar4.Text = "Status bar";
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "navBarGroup1";
            this.navBarGroup2.Expanded = true;
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // OBECEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "OBECEditPart";
            this.Size = new System.Drawing.Size(1200, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOtherInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgentOfCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoods.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDeparture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDetination.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lwPackages.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPackages.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMeasurement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDepartureName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDetinationName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeOperation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtOperationNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsignee.Properties)).EndInit();
            this.tabPageBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelTabBaseInfo)).EndInit();
            this.panelTabBaseInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarOrderInfo)).EndInit();
            this.navBarOrderInfo.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelBaseInfo)).EndInit();
            this.panelBaseInfo.ResumeLayout(false);
            this.panelBaseInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarehouseNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTransferNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRevenueTon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBookingDate.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabPageBase;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.LabelControl labNo;
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
        private ICP.FCM.OtherBusiness.UI.Common.CargoDescriptionPart cargoDescriptionPart1;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraEditors.PanelControl panelTabBaseInfo;
        private DevExpress.XtraNavBar.NavBarControl navBarOrderInfo;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraEditors.PopupContainerEdit stxtCustomer;
        private DevExpress.XtraEditors.TextEdit txtState;
        private DevExpress.XtraEditors.LabelControl labState;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraEditors.ButtonEdit stxtAgentOfCarrier;
        private DevExpress.XtraEditors.LabelControl labAgentOfCarrier;
        private DevExpress.XtraEditors.TextEdit txtGoods;
        private DevExpress.XtraEditors.LabelControl labGoods;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbSales;
        private DevExpress.XtraEditors.LabelControl labSalse;
        private ICP.Framework.ClientComponents.Controls.TreeSelectBox tsbSalesDep;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ButtonEdit stxtDeparture;
        private DevExpress.XtraEditors.LabelControl labDeparture;
        private DevExpress.XtraEditors.ButtonEdit stxtDetination;
        private DevExpress.XtraEditors.LabelControl labDetination;
        private DevExpress.XtraEditors.LabelControl labBusinessDate;
        private DevExpress.XtraEditors.DateEdit dteBookingDate;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit lwPackages;
        private DevExpress.XtraEditors.LabelControl labPackages;
        private DevExpress.XtraEditors.SpinEdit spinPackages;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbWeightUnit;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SpinEdit spinWeight;
        private DevExpress.XtraEditors.LabelControl labMeasurement;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMeasurementUnit;
        private DevExpress.XtraEditors.SpinEdit spinMeasurement;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar4;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraEditors.TextEdit stxtDetinationName;
        private DevExpress.XtraEditors.TextEdit stxtDepartureName;
        private DevExpress.XtraEditors.LabelControl labOperation;
        private BindingSource bsOtherInfo;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private DevExpress.XtraEditors.MemoEdit memoRemark;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit treeOperation;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbType;
        private DevExpress.XtraBars.BarButtonItem barTruck;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraEditors.TextEdit txtExpressNo;
        private DevExpress.XtraEditors.LabelControl lblExpressNo;
        private DevExpress.XtraEditors.PanelControl panelBaseInfo;
        private DevExpress.XtraEditors.LabelControl lblTransferNo;
        private DevExpress.XtraEditors.TextEdit txtTransferNo;
        private DevExpress.XtraEditors.LabelControl lblWarehouseNo;
        private DevExpress.XtraEditors.TextEdit txtWarehouseNo;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCurrency;
        private DevExpress.XtraEditors.LabelControl labCost;
        private DevExpress.XtraEditors.SpinEdit spinCost;
        private DevExpress.XtraEditors.ButtonEdit stxtOperationNo;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.SpinEdit spinRevenueTon;
        private DevExpress.XtraEditors.LabelControl labRevenueTon;
        private DevExpress.XtraEditors.LabelControl labCostCurrency;
        private Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtConsignee;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupCSPBooking;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private FCM.Common.UI.CommonPart.PartBookingForCSP partDelegate;
    }
}
