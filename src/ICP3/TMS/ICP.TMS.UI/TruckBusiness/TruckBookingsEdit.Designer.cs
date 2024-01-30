namespace ICP.TMS.UI
{
    partial class TruckBookingsEdit
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.mainPanel = new DevExpress.XtraEditors.PanelControl();
            this.bcMain = new DevExpress.XtraNavBar.NavBarControl();
            this.bgBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgcBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.dtBookingDate = new DevExpress.XtraEditors.DateEdit();
            this.bsTruckInfo = new System.Windows.Forms.BindingSource(this.components);
            this.labBookingDate = new DevExpress.XtraEditors.LabelControl();
            this.cmbCustomer = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.cmbBookingMode = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labBookingMode = new DevExpress.XtraEditors.LabelControl();
            this.cmbSales = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labSalesType = new DevExpress.XtraEditors.LabelControl();
            this.labSales = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerRefNo = new DevExpress.XtraEditors.TextEdit();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.cmbSalesType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.bgcBusiness = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.stxtVoyageNo = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtVesselName = new DevExpress.XtraEditors.ButtonEdit();
            this.cmbCarrierID = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.stxtContainerDescription = new ICP.TMS.UI.ContainerDemandControl();
            this.labDeliveryDate = new DevExpress.XtraEditors.LabelControl();
            this.labPickUpAtDate = new DevExpress.XtraEditors.LabelControl();
            this.dtpDeliveryDate = new DevExpress.XtraEditors.DateEdit();
            this.dtpPickUpAtDate = new DevExpress.XtraEditors.DateEdit();
            this.stxtDeliveryAtID = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtReturnLocationID = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.labDeliveryAt = new DevExpress.XtraEditors.LabelControl();
            this.labContainerDescription = new DevExpress.XtraEditors.LabelControl();
            this.labReturnLocation = new DevExpress.XtraEditors.LabelControl();
            this.stxtPickUpAtID = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.labVoyageNo = new DevExpress.XtraEditors.LabelControl();
            this.labPickUpAt = new DevExpress.XtraEditors.LabelControl();
            this.labVesselName = new DevExpress.XtraEditors.LabelControl();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.cmbType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.txtMBLNo = new DevExpress.XtraEditors.TextEdit();
            this.labMBLNo = new DevExpress.XtraEditors.LabelControl();
            this.bgcContainer = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.gcBox = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvBox = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colIndexNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colContainerNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContainerType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbContainerType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colTrayNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastFreeDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDriverID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbDriverID = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colTruckNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbTruckID = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colTruckPlace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTruckDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPickUpAtDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliveryDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReturnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barDockControl7 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl8 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl6 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl5 = new DevExpress.XtraBars.BarDockControl();
            this.bgBusiness = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgContainer = new DevExpress.XtraNavBar.NavBarGroup();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barNew = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bcMain)).BeginInit();
            this.bcMain.SuspendLayout();
            this.bgcBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtBookingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBookingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTruckInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBookingMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerRefNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            this.bgcBusiness.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtVoyageNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtVesselName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDeliveryDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDeliveryDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPickUpAtDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPickUpAtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDeliveryAtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtReturnLocationID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPickUpAtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMBLNo.Properties)).BeginInit();
            this.bgcContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbContainerType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDriverID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTruckID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
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
            this.barRefresh,
            this.barClose});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(&S)";
            this.barSave.Glyph = global::ICP.TMS.UI.Properties.Resources.Save_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "刷新(&R)";
            this.barRefresh.Glyph = global::ICP.TMS.UI.Properties.Resources.Refresh_16;
            this.barRefresh.Id = 1;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭(&C)";
            this.barClose.Glyph = global::ICP.TMS.UI.Properties.Resources.Close_16;
            this.barClose.Id = 2;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(860, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 608);
            this.barDockControlBottom.Size = new System.Drawing.Size(860, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 582);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(860, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 582);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.bcMain);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 26);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(860, 582);
            this.mainPanel.TabIndex = 9;
            // 
            // bcMain
            // 
            this.bcMain.ActiveGroup = this.bgBase;
            this.bcMain.Controls.Add(this.bgcBase);
            this.bcMain.Controls.Add(this.bgcBusiness);
            this.bcMain.Controls.Add(this.bgcContainer);
            this.bcMain.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.bgBase,
            this.bgBusiness,
            this.bgContainer});
            this.bcMain.Location = new System.Drawing.Point(3, 7);
            this.bcMain.Name = "bcMain";
            this.bcMain.OptionsNavPane.ExpandedWidth = 827;
            this.bcMain.Size = new System.Drawing.Size(850, 580);
            this.bcMain.TabIndex = 6;
            this.bcMain.Text = "navBarControl1";
            // 
            // bgBase
            // 
            this.bgBase.Caption = "基本信息";
            this.bgBase.ControlContainer = this.bgcBase;
            this.bgBase.Expanded = true;
            this.bgBase.GroupClientHeight = 62;
            this.bgBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgBase.Name = "bgBase";
            // 
            // bgcBase
            // 
            this.bgcBase.Controls.Add(this.dtBookingDate);
            this.bgcBase.Controls.Add(this.labBookingDate);
            this.bgcBase.Controls.Add(this.cmbCustomer);
            this.bgcBase.Controls.Add(this.cmbBookingMode);
            this.bgcBase.Controls.Add(this.labBookingMode);
            this.bgcBase.Controls.Add(this.cmbSales);
            this.bgcBase.Controls.Add(this.labSalesType);
            this.bgcBase.Controls.Add(this.labSales);
            this.bgcBase.Controls.Add(this.txtCustomerRefNo);
            this.bgcBase.Controls.Add(this.labCustomer);
            this.bgcBase.Controls.Add(this.cmbCompany);
            this.bgcBase.Controls.Add(this.labCompany);
            this.bgcBase.Controls.Add(this.labNo);
            this.bgcBase.Controls.Add(this.cmbSalesType);
            this.bgcBase.Controls.Add(this.txtNo);
            this.bgcBase.Name = "bgcBase";
            this.bgcBase.Size = new System.Drawing.Size(846, 60);
            this.bgcBase.TabIndex = 0;
            // 
            // dtBookingDate
            // 
            this.dtBookingDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfo, "BookingDate", true));
            this.dtBookingDate.EditValue = null;
            this.dtBookingDate.Location = new System.Drawing.Point(721, 33);
            this.dtBookingDate.Name = "dtBookingDate";
            this.dtBookingDate.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.dtBookingDate.Properties.Appearance.Options.UseBackColor = true;
            this.dtBookingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtBookingDate.Properties.Mask.EditMask = "";
            this.dtBookingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtBookingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtBookingDate.Size = new System.Drawing.Size(108, 21);
            this.dtBookingDate.TabIndex = 7;
            // 
            // bsTruckInfo
            // 
            this.bsTruckInfo.DataSource = typeof(ICP.TMS.ServiceInterface.TruckBookingsInfo);
            // 
            // labBookingDate
            // 
            this.labBookingDate.Location = new System.Drawing.Point(631, 36);
            this.labBookingDate.Name = "labBookingDate";
            this.labBookingDate.Size = new System.Drawing.Size(48, 14);
            this.labBookingDate.TabIndex = 145;
            this.labBookingDate.Text = "委托日期";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsTruckInfo, "CustomerName", true));
            this.cmbCustomer.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfo, "CustomerID", true));
            this.cmbCustomer.EditText = "";
            this.cmbCustomer.EditValue = null;
            this.cmbCustomer.Location = new System.Drawing.Point(103, 33);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.ReadOnly = false;
            this.cmbCustomer.Size = new System.Drawing.Size(223, 21);
            this.cmbCustomer.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbCustomer.TabIndex = 2;
            this.cmbCustomer.ToolTip = "";
            // 
            // cmbBookingMode
            // 
            this.cmbBookingMode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfo, "Bookingmode", true));
            this.cmbBookingMode.Location = new System.Drawing.Point(721, 6);
            this.cmbBookingMode.Name = "cmbBookingMode";
            this.cmbBookingMode.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbBookingMode.Properties.Appearance.Options.UseBackColor = true;
            this.cmbBookingMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBookingMode.Size = new System.Drawing.Size(108, 21);
            this.cmbBookingMode.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbBookingMode.TabIndex = 6;
            // 
            // labBookingMode
            // 
            this.labBookingMode.Location = new System.Drawing.Point(631, 9);
            this.labBookingMode.Name = "labBookingMode";
            this.labBookingMode.Size = new System.Drawing.Size(48, 14);
            this.labBookingMode.TabIndex = 144;
            this.labBookingMode.Text = "委托方式";
            // 
            // cmbSales
            // 
            this.cmbSales.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfo, "SalesID", true));
            this.cmbSales.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsTruckInfo, "SalesName", true));
            this.cmbSales.EditText = "";
            this.cmbSales.EditValue = null;
            this.cmbSales.Location = new System.Drawing.Point(505, 6);
            this.cmbSales.Name = "cmbSales";
            this.cmbSales.ReadOnly = false;
            this.cmbSales.Size = new System.Drawing.Size(120, 21);
            this.cmbSales.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbSales.TabIndex = 4;
            this.cmbSales.ToolTip = "";
            // 
            // labSalesType
            // 
            this.labSalesType.Location = new System.Drawing.Point(428, 36);
            this.labSalesType.Name = "labSalesType";
            this.labSalesType.Size = new System.Drawing.Size(48, 14);
            this.labSalesType.TabIndex = 61;
            this.labSalesType.Text = "揽货类型";
            // 
            // labSales
            // 
            this.labSales.Location = new System.Drawing.Point(428, 9);
            this.labSales.Name = "labSales";
            this.labSales.Size = new System.Drawing.Size(36, 14);
            this.labSales.TabIndex = 60;
            this.labSales.Text = "揽货人";
            // 
            // txtCustomerRefNo
            // 
            this.txtCustomerRefNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "CustomerRefNo", true));
            this.txtCustomerRefNo.Location = new System.Drawing.Point(332, 33);
            this.txtCustomerRefNo.Name = "txtCustomerRefNo";
            this.txtCustomerRefNo.Size = new System.Drawing.Size(86, 21);
            this.txtCustomerRefNo.TabIndex = 3;
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(3, 36);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(65, 14);
            this.labCustomer.TabIndex = 47;
            this.labCustomer.Text = "客户/参考号";
            // 
            // cmbCompany
            // 
            this.cmbCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfo, "CompanyID", true));
            this.cmbCompany.Location = new System.Drawing.Point(298, 6);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(120, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbCompany.TabIndex = 1;
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(243, 9);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(48, 14);
            this.labCompany.TabIndex = 44;
            this.labCompany.Text = "口岸公司";
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(4, 9);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(36, 14);
            this.labNo.TabIndex = 45;
            this.labNo.Text = "业务号";
            // 
            // cmbSalesType
            // 
            this.cmbSalesType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfo, "SalesTypeID", true));
            this.cmbSalesType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "SalesTypeName", true));
            this.cmbSalesType.Location = new System.Drawing.Point(505, 33);
            this.cmbSalesType.Name = "cmbSalesType";
            this.cmbSalesType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbSalesType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbSalesType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSalesType.Size = new System.Drawing.Size(120, 21);
            this.cmbSalesType.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbSalesType.TabIndex = 5;
            // 
            // txtNo
            // 
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "No", true));
            this.txtNo.EditValue = "";
            this.txtNo.Location = new System.Drawing.Point(103, 6);
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.ReadOnly = true;
            this.txtNo.Size = new System.Drawing.Size(127, 21);
            this.txtNo.TabIndex = 0;
            this.txtNo.TabStop = false;
            // 
            // bgcBusiness
            // 
            this.bgcBusiness.Controls.Add(this.stxtVoyageNo);
            this.bgcBusiness.Controls.Add(this.stxtVesselName);
            this.bgcBusiness.Controls.Add(this.cmbCarrierID);
            this.bgcBusiness.Controls.Add(this.txtRemark);
            this.bgcBusiness.Controls.Add(this.stxtContainerDescription);
            this.bgcBusiness.Controls.Add(this.labDeliveryDate);
            this.bgcBusiness.Controls.Add(this.labPickUpAtDate);
            this.bgcBusiness.Controls.Add(this.dtpDeliveryDate);
            this.bgcBusiness.Controls.Add(this.dtpPickUpAtDate);
            this.bgcBusiness.Controls.Add(this.stxtDeliveryAtID);
            this.bgcBusiness.Controls.Add(this.stxtReturnLocationID);
            this.bgcBusiness.Controls.Add(this.labRemark);
            this.bgcBusiness.Controls.Add(this.labDeliveryAt);
            this.bgcBusiness.Controls.Add(this.labContainerDescription);
            this.bgcBusiness.Controls.Add(this.labReturnLocation);
            this.bgcBusiness.Controls.Add(this.stxtPickUpAtID);
            this.bgcBusiness.Controls.Add(this.labVoyageNo);
            this.bgcBusiness.Controls.Add(this.labPickUpAt);
            this.bgcBusiness.Controls.Add(this.labVesselName);
            this.bgcBusiness.Controls.Add(this.labCarrier);
            this.bgcBusiness.Controls.Add(this.cmbType);
            this.bgcBusiness.Controls.Add(this.labType);
            this.bgcBusiness.Controls.Add(this.txtMBLNo);
            this.bgcBusiness.Controls.Add(this.labMBLNo);
            this.bgcBusiness.Name = "bgcBusiness";
            this.bgcBusiness.Size = new System.Drawing.Size(846, 166);
            this.bgcBusiness.TabIndex = 1;
            this.bgcBusiness.Click += new System.EventHandler(this.bgcBusiness_Click);
            // 
            // stxtVoyageNo
            // 
            this.stxtVoyageNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "VoyageNo", true));
            this.stxtVoyageNo.Location = new System.Drawing.Point(298, 61);
            this.stxtVoyageNo.Name = "stxtVoyageNo";
            this.stxtVoyageNo.Size = new System.Drawing.Size(120, 21);
            this.stxtVoyageNo.TabIndex = 4;
            // 
            // stxtVesselName
            // 
            this.stxtVesselName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "VesselName", true));
            this.stxtVesselName.Location = new System.Drawing.Point(103, 60);
            this.stxtVesselName.MenuManager = this.barManager1;
            this.stxtVesselName.Name = "stxtVesselName";
            this.stxtVesselName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtVesselName.Size = new System.Drawing.Size(127, 21);
            this.stxtVesselName.TabIndex = 3;
            // 
            // cmbCarrierID
            // 
            this.cmbCarrierID.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfo, "CarrierID", true));
            this.cmbCarrierID.EditText = "";
            this.cmbCarrierID.EditValue = null;
            this.cmbCarrierID.Location = new System.Drawing.Point(103, 34);
            this.cmbCarrierID.Name = "cmbCarrierID";
            this.cmbCarrierID.ReadOnly = false;
            this.cmbCarrierID.Size = new System.Drawing.Size(315, 21);
            this.cmbCarrierID.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbCarrierID.TabIndex = 2;
            this.cmbCarrierID.ToolTip = "";
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "Remark", true));
            this.txtRemark.Location = new System.Drawing.Point(103, 115);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(726, 43);
            this.txtRemark.TabIndex = 11;
            // 
            // stxtContainerDescription
            // 
            this.stxtContainerDescription.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.stxtContainerDescription.Appearance.BackColor2 = System.Drawing.Color.LightYellow;
            this.stxtContainerDescription.Appearance.Options.UseBackColor = true;
            this.stxtContainerDescription.Location = new System.Drawing.Point(103, 88);
            this.stxtContainerDescription.Name = "stxtContainerDescription";
            this.stxtContainerDescription.Size = new System.Drawing.Size(522, 21);
            this.stxtContainerDescription.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtContainerDescription.TabIndex = 10;
            // 
            // labDeliveryDate
            // 
            this.labDeliveryDate.Location = new System.Drawing.Point(631, 37);
            this.labDeliveryDate.Name = "labDeliveryDate";
            this.labDeliveryDate.Size = new System.Drawing.Size(48, 14);
            this.labDeliveryDate.TabIndex = 51;
            this.labDeliveryDate.Text = "交货日期";
            // 
            // labPickUpAtDate
            // 
            this.labPickUpAtDate.Location = new System.Drawing.Point(428, 38);
            this.labPickUpAtDate.Name = "labPickUpAtDate";
            this.labPickUpAtDate.Size = new System.Drawing.Size(48, 14);
            this.labPickUpAtDate.TabIndex = 51;
            this.labPickUpAtDate.Text = "提柜日期";
            // 
            // dtpDeliveryDate
            // 
            this.dtpDeliveryDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfo, "DeliveryDate", true));
            this.dtpDeliveryDate.EditValue = null;
            this.dtpDeliveryDate.Location = new System.Drawing.Point(721, 34);
            this.dtpDeliveryDate.Name = "dtpDeliveryDate";
            this.dtpDeliveryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDeliveryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpDeliveryDate.Size = new System.Drawing.Size(108, 21);
            this.dtpDeliveryDate.TabIndex = 9;
            // 
            // dtpPickUpAtDate
            // 
            this.dtpPickUpAtDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfo, "PickUpAtDate", true));
            this.dtpPickUpAtDate.EditValue = null;
            this.dtpPickUpAtDate.Location = new System.Drawing.Point(505, 30);
            this.dtpPickUpAtDate.Name = "dtpPickUpAtDate";
            this.dtpPickUpAtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpPickUpAtDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpPickUpAtDate.Size = new System.Drawing.Size(120, 21);
            this.dtpPickUpAtDate.TabIndex = 6;
            // 
            // stxtDeliveryAtID
            // 
            this.stxtDeliveryAtID.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTruckInfo, "DeliveryAtID", true));
            this.stxtDeliveryAtID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "DeliveryAtName", true));
            this.stxtDeliveryAtID.Location = new System.Drawing.Point(722, 6);
            this.stxtDeliveryAtID.Name = "stxtDeliveryAtID";
            this.stxtDeliveryAtID.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtDeliveryAtID.Properties.ActionButtonIndex = 1;
            this.stxtDeliveryAtID.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtDeliveryAtID.Properties.Appearance.Options.UseBackColor = true;
            this.stxtDeliveryAtID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtDeliveryAtID.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtDeliveryAtID.Properties.PopupSizeable = false;
            this.stxtDeliveryAtID.Properties.ShowPopupCloseButton = false;
            this.stxtDeliveryAtID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtDeliveryAtID.Size = new System.Drawing.Size(108, 21);
            this.stxtDeliveryAtID.TabIndex = 8;
            // 
            // stxtReturnLocationID
            // 
            this.stxtReturnLocationID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "ReturnLocationName", true));
            this.stxtReturnLocationID.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTruckInfo, "ReturnLocationID", true));
            this.stxtReturnLocationID.Location = new System.Drawing.Point(505, 56);
            this.stxtReturnLocationID.Name = "stxtReturnLocationID";
            this.stxtReturnLocationID.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtReturnLocationID.Properties.ActionButtonIndex = 1;
            this.stxtReturnLocationID.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtReturnLocationID.Properties.Appearance.Options.UseBackColor = true;
            this.stxtReturnLocationID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtReturnLocationID.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtReturnLocationID.Properties.PopupSizeable = false;
            this.stxtReturnLocationID.Properties.ShowPopupCloseButton = false;
            this.stxtReturnLocationID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtReturnLocationID.Size = new System.Drawing.Size(120, 21);
            this.stxtReturnLocationID.TabIndex = 7;
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(4, 118);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(24, 14);
            this.labRemark.TabIndex = 49;
            this.labRemark.Text = "备注";
            // 
            // labDeliveryAt
            // 
            this.labDeliveryAt.Location = new System.Drawing.Point(631, 9);
            this.labDeliveryAt.Name = "labDeliveryAt";
            this.labDeliveryAt.Size = new System.Drawing.Size(48, 14);
            this.labDeliveryAt.TabIndex = 49;
            this.labDeliveryAt.Text = "交货地点";
            // 
            // labContainerDescription
            // 
            this.labContainerDescription.Location = new System.Drawing.Point(4, 91);
            this.labContainerDescription.Name = "labContainerDescription";
            this.labContainerDescription.Size = new System.Drawing.Size(36, 14);
            this.labContainerDescription.TabIndex = 49;
            this.labContainerDescription.Text = "箱描述";
            // 
            // labReturnLocation
            // 
            this.labReturnLocation.Location = new System.Drawing.Point(428, 64);
            this.labReturnLocation.Name = "labReturnLocation";
            this.labReturnLocation.Size = new System.Drawing.Size(48, 14);
            this.labReturnLocation.TabIndex = 49;
            this.labReturnLocation.Text = "还柜地点";
            // 
            // stxtPickUpAtID
            // 
            this.stxtPickUpAtID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "PickUpAtName", true));
            this.stxtPickUpAtID.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTruckInfo, "PickUpAtID", true));
            this.stxtPickUpAtID.Location = new System.Drawing.Point(505, 2);
            this.stxtPickUpAtID.Name = "stxtPickUpAtID";
            this.stxtPickUpAtID.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtPickUpAtID.Properties.ActionButtonIndex = 1;
            this.stxtPickUpAtID.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtPickUpAtID.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPickUpAtID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtPickUpAtID.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtPickUpAtID.Properties.PopupSizeable = false;
            this.stxtPickUpAtID.Properties.ShowPopupCloseButton = false;
            this.stxtPickUpAtID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtPickUpAtID.Size = new System.Drawing.Size(120, 21);
            this.stxtPickUpAtID.TabIndex = 5;
            // 
            // labVoyageNo
            // 
            this.labVoyageNo.Location = new System.Drawing.Point(243, 63);
            this.labVoyageNo.Name = "labVoyageNo";
            this.labVoyageNo.Size = new System.Drawing.Size(24, 14);
            this.labVoyageNo.TabIndex = 49;
            this.labVoyageNo.Text = "航次";
            // 
            // labPickUpAt
            // 
            this.labPickUpAt.Location = new System.Drawing.Point(428, 10);
            this.labPickUpAt.Name = "labPickUpAt";
            this.labPickUpAt.Size = new System.Drawing.Size(48, 14);
            this.labPickUpAt.TabIndex = 49;
            this.labPickUpAt.Text = "提柜地点";
            // 
            // labVesselName
            // 
            this.labVesselName.Location = new System.Drawing.Point(4, 63);
            this.labVesselName.Name = "labVesselName";
            this.labVesselName.Size = new System.Drawing.Size(24, 14);
            this.labVesselName.TabIndex = 49;
            this.labVesselName.Text = "船名";
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(4, 37);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(24, 14);
            this.labCarrier.TabIndex = 49;
            this.labCarrier.Text = "船东";
            // 
            // cmbType
            // 
            this.cmbType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfo, "TruckType", true));
            this.cmbType.Location = new System.Drawing.Point(298, 7);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(120, 21);
            this.cmbType.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbType.TabIndex = 1;
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(243, 9);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(29, 14);
            this.labType.TabIndex = 8;
            this.labType.Text = "进/出";
            // 
            // txtMBLNo
            // 
            this.txtMBLNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfo, "MBLNo", true));
            this.txtMBLNo.Location = new System.Drawing.Point(103, 6);
            this.txtMBLNo.Name = "txtMBLNo";
            this.txtMBLNo.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.txtMBLNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtMBLNo.Size = new System.Drawing.Size(127, 21);
            this.txtMBLNo.TabIndex = 0;
            // 
            // labMBLNo
            // 
            this.labMBLNo.Location = new System.Drawing.Point(4, 9);
            this.labMBLNo.Name = "labMBLNo";
            this.labMBLNo.Size = new System.Drawing.Size(60, 14);
            this.labMBLNo.TabIndex = 7;
            this.labMBLNo.Text = "船东提单号";
            // 
            // bgcContainer
            // 
            this.bgcContainer.Controls.Add(this.gcBox);
            this.bgcContainer.Controls.Add(this.barDockControl7);
            this.bgcContainer.Controls.Add(this.barDockControl8);
            this.bgcContainer.Controls.Add(this.barDockControl6);
            this.bgcContainer.Controls.Add(this.barDockControl5);
            this.bgcContainer.Name = "bgcContainer";
            this.bgcContainer.Size = new System.Drawing.Size(846, 251);
            this.bgcContainer.TabIndex = 3;
            // 
            // gcBox
            // 
            this.gcBox.DataSource = this.bsList;
            this.gcBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBox.Location = new System.Drawing.Point(0, 26);
            this.gcBox.MainView = this.gvBox;
            this.gcBox.Name = "gcBox";
            this.gcBox.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.CmbState,
            this.cmbDriverID,
            this.cmbTruckID,
            this.cmbContainerType});
            this.gcBox.Size = new System.Drawing.Size(846, 225);
            this.gcBox.TabIndex = 4;
            this.gcBox.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBox});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.TMS.ServiceInterface.TruckContainersList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gvBox
            // 
            this.gvBox.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIndexNo,
            this.colState,
            this.colContainerNo,
            this.colContainerType,
            this.colTrayNo,
            this.colLastFreeDate,
            this.colDriverID,
            this.colTruckNo,
            this.colTruckPlace,
            this.colTruckDate,
            this.colPickUpAtDate,
            this.colDeliveryDate,
            this.colReturnDate,
            this.colRemark});
            this.gvBox.GridControl = this.gcBox;
            this.gvBox.Name = "gvBox";
            this.gvBox.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvBox.OptionsSelection.MultiSelect = true;
            this.gvBox.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvBox.OptionsView.ColumnAutoWidth = false;
            this.gvBox.OptionsView.EnableAppearanceEvenRow = true;
            this.gvBox.OptionsView.RowAutoHeight = true;
            this.gvBox.OptionsView.ShowGroupPanel = false;
            this.gvBox.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvBox.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvBox_CellValueChanged);
            this.gvBox.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvBox_CellValueChanging);
            // 
            // colIndexNo
            // 
            this.colIndexNo.Caption = "序号";
            this.colIndexNo.FieldName = "IndexNo";
            this.colIndexNo.Name = "colIndexNo";
            this.colIndexNo.Visible = true;
            this.colIndexNo.VisibleIndex = 0;
            // 
            // colState
            // 
            this.colState.Caption = "状态";
            this.colState.ColumnEdit = this.CmbState;
            this.colState.FieldName = "State";
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowEdit = false;
            this.colState.Visible = true;
            this.colState.VisibleIndex = 1;
            // 
            // CmbState
            // 
            this.CmbState.AutoHeight = false;
            this.CmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbState.Name = "CmbState";
            // 
            // colContainerNo
            // 
            this.colContainerNo.Caption = "箱号";
            this.colContainerNo.FieldName = "No";
            this.colContainerNo.Name = "colContainerNo";
            this.colContainerNo.Visible = true;
            this.colContainerNo.VisibleIndex = 2;
            // 
            // colContainerType
            // 
            this.colContainerType.Caption = "箱型";
            this.colContainerType.ColumnEdit = this.cmbContainerType;
            this.colContainerType.FieldName = "TypeID";
            this.colContainerType.Name = "colContainerType";
            this.colContainerType.Visible = true;
            this.colContainerType.VisibleIndex = 3;
            // 
            // cmbContainerType
            // 
            this.cmbContainerType.AutoHeight = false;
            this.cmbContainerType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbContainerType.Name = "cmbContainerType";
            // 
            // colTrayNo
            // 
            this.colTrayNo.Caption = "托盘号";
            this.colTrayNo.FieldName = "TrayNo";
            this.colTrayNo.Name = "colTrayNo";
            this.colTrayNo.Visible = true;
            this.colTrayNo.VisibleIndex = 4;
            // 
            // colLastFreeDate
            // 
            this.colLastFreeDate.Caption = "免堆日";
            this.colLastFreeDate.FieldName = "LastFreeDate";
            this.colLastFreeDate.Name = "colLastFreeDate";
            this.colLastFreeDate.Visible = true;
            this.colLastFreeDate.VisibleIndex = 5;
            // 
            // colDriverID
            // 
            this.colDriverID.Caption = "司机";
            this.colDriverID.ColumnEdit = this.cmbDriverID;
            this.colDriverID.FieldName = "DriverID";
            this.colDriverID.Name = "colDriverID";
            this.colDriverID.Visible = true;
            this.colDriverID.VisibleIndex = 6;
            // 
            // cmbDriverID
            // 
            this.cmbDriverID.AutoHeight = false;
            this.cmbDriverID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDriverID.Name = "cmbDriverID";
            // 
            // colTruckNo
            // 
            this.colTruckNo.Caption = "车牌号";
            this.colTruckNo.ColumnEdit = this.cmbTruckID;
            this.colTruckNo.FieldName = "CarID";
            this.colTruckNo.Name = "colTruckNo";
            this.colTruckNo.Visible = true;
            this.colTruckNo.VisibleIndex = 7;
            // 
            // cmbTruckID
            // 
            this.cmbTruckID.AutoHeight = false;
            this.cmbTruckID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTruckID.Name = "cmbTruckID";
            // 
            // colTruckPlace
            // 
            this.colTruckPlace.Caption = "地点";
            this.colTruckPlace.FieldName = "TruckPlace";
            this.colTruckPlace.Name = "colTruckPlace";
            this.colTruckPlace.Visible = true;
            this.colTruckPlace.VisibleIndex = 8;
            // 
            // colTruckDate
            // 
            this.colTruckDate.Caption = "派车日";
            this.colTruckDate.FieldName = "TruckDate";
            this.colTruckDate.Name = "colTruckDate";
            this.colTruckDate.Visible = true;
            this.colTruckDate.VisibleIndex = 9;
            // 
            // colPickUpAtDate
            // 
            this.colPickUpAtDate.Caption = "提柜日";
            this.colPickUpAtDate.FieldName = "PickUpAtDate";
            this.colPickUpAtDate.Name = "colPickUpAtDate";
            this.colPickUpAtDate.Visible = true;
            this.colPickUpAtDate.VisibleIndex = 10;
            // 
            // colDeliveryDate
            // 
            this.colDeliveryDate.Caption = "交货日";
            this.colDeliveryDate.FieldName = "DeliveryDate";
            this.colDeliveryDate.Name = "colDeliveryDate";
            this.colDeliveryDate.Visible = true;
            this.colDeliveryDate.VisibleIndex = 11;
            // 
            // colReturnDate
            // 
            this.colReturnDate.Caption = "还柜日";
            this.colReturnDate.FieldName = "ReturnDate";
            this.colReturnDate.Name = "colReturnDate";
            this.colReturnDate.Visible = true;
            this.colReturnDate.VisibleIndex = 12;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "备注";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 13;
            // 
            // barDockControl7
            // 
            this.barDockControl7.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl7.Location = new System.Drawing.Point(0, 26);
            this.barDockControl7.Size = new System.Drawing.Size(0, 225);
            // 
            // barDockControl8
            // 
            this.barDockControl8.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl8.Location = new System.Drawing.Point(846, 26);
            this.barDockControl8.Size = new System.Drawing.Size(0, 225);
            // 
            // barDockControl6
            // 
            this.barDockControl6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl6.Location = new System.Drawing.Point(0, 251);
            this.barDockControl6.Size = new System.Drawing.Size(846, 0);
            // 
            // barDockControl5
            // 
            this.barDockControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl5.Location = new System.Drawing.Point(0, 0);
            this.barDockControl5.Size = new System.Drawing.Size(846, 26);
            // 
            // bgBusiness
            // 
            this.bgBusiness.Caption = "业务信息";
            this.bgBusiness.ControlContainer = this.bgcBusiness;
            this.bgBusiness.Expanded = true;
            this.bgBusiness.GroupClientHeight = 168;
            this.bgBusiness.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgBusiness.Name = "bgBusiness";
            // 
            // bgContainer
            // 
            this.bgContainer.Caption = "箱列表";
            this.bgContainer.ControlContainer = this.bgcContainer;
            this.bgContainer.Expanded = true;
            this.bgContainer.GroupClientHeight = 253;
            this.bgContainer.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgContainer.Name = "bgContainer";
            // 
            // barDockControl1
            // 
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl1.Location = new System.Drawing.Point(2, 28);
            this.barDockControl1.Size = new System.Drawing.Size(0, 221);
            // 
            // barDockControl2
            // 
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl2.Location = new System.Drawing.Point(844, 28);
            this.barDockControl2.Size = new System.Drawing.Size(0, 221);
            // 
            // barDockControl3
            // 
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl3.Location = new System.Drawing.Point(2, 249);
            this.barDockControl3.Size = new System.Drawing.Size(842, 0);
            // 
            // barDockControl4
            // 
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl4.Location = new System.Drawing.Point(2, 2);
            this.barDockControl4.Size = new System.Drawing.Size(842, 26);
            // 
            // barManager2
            // 
            this.barManager2.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager2.DockControls.Add(this.barDockControl5);
            this.barManager2.DockControls.Add(this.barDockControl6);
            this.barManager2.DockControls.Add(this.barDockControl7);
            this.barManager2.DockControls.Add(this.barDockControl8);
            this.barManager2.Form = this.bgcContainer;
            this.barManager2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barNew,
            this.barDelete});
            this.barManager2.MainMenu = this.bar3;
            this.barManager2.MaxItemId = 2;
            // 
            // bar3
            // 
            this.bar3.BarName = "Main menu";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar3.OptionsBar.MultiLine = true;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Main menu";
            // 
            // barNew
            // 
            this.barNew.Caption = "新增(&N)";
            this.barNew.Glyph = global::ICP.TMS.UI.Properties.Resources.Add_File_16;
            this.barNew.Id = 0;
            this.barNew.Name = "barNew";
            this.barNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNew_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "删除(&D)";
            this.barDelete.Glyph = global::ICP.TMS.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // TruckBookingsEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "TruckBookingsEdit";
            this.Size = new System.Drawing.Size(860, 608);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bcMain)).EndInit();
            this.bcMain.ResumeLayout(false);
            this.bgcBase.ResumeLayout(false);
            this.bgcBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtBookingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBookingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTruckInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBookingMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerRefNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            this.bgcBusiness.ResumeLayout(false);
            this.bgcBusiness.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtVoyageNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtVesselName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDeliveryDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDeliveryDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPickUpAtDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPickUpAtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDeliveryAtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtReturnLocationID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPickUpAtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMBLNo.Properties)).EndInit();
            this.bgcContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbContainerType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDriverID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTruckID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraEditors.PanelControl mainPanel;
        private DevExpress.XtraNavBar.NavBarControl bcMain;
        private DevExpress.XtraNavBar.NavBarGroup bgBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcBase;
        private DevExpress.XtraEditors.DateEdit dtBookingDate;
        private DevExpress.XtraEditors.LabelControl labBookingDate;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbBookingMode;
        private DevExpress.XtraEditors.LabelControl labBookingMode;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox cmbSales;
        private DevExpress.XtraEditors.LabelControl labSalesType;
        private DevExpress.XtraEditors.LabelControl labSales;
        private DevExpress.XtraEditors.TextEdit txtCustomerRefNo;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.LabelControl labNo;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbSalesType;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcBusiness;
        private DevExpress.XtraEditors.ButtonEdit stxtVoyageNo;
        private DevExpress.XtraEditors.ButtonEdit stxtVesselName;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox cmbCarrierID;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private ContainerDemandControl stxtContainerDescription;
        private DevExpress.XtraEditors.LabelControl labDeliveryDate;
        private DevExpress.XtraEditors.LabelControl labPickUpAtDate;
        private DevExpress.XtraEditors.DateEdit dtpDeliveryDate;
        private DevExpress.XtraEditors.DateEdit dtpPickUpAtDate;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtDeliveryAtID;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtReturnLocationID;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private DevExpress.XtraEditors.LabelControl labDeliveryAt;
        private DevExpress.XtraEditors.LabelControl labContainerDescription;
        private DevExpress.XtraEditors.LabelControl labReturnLocation;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtPickUpAtID;
        private DevExpress.XtraEditors.LabelControl labVoyageNo;
        private DevExpress.XtraEditors.LabelControl labPickUpAt;
        private DevExpress.XtraEditors.LabelControl labVesselName;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbType;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.TextEdit txtMBLNo;
        private DevExpress.XtraEditors.LabelControl labMBLNo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcContainer;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcBox;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvBox;
        private DevExpress.XtraGrid.Columns.GridColumn colIndexNo;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerNo;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerType;
        private DevExpress.XtraGrid.Columns.GridColumn colTrayNo;
        private DevExpress.XtraGrid.Columns.GridColumn colLastFreeDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDriverID;
        private DevExpress.XtraGrid.Columns.GridColumn colTruckNo;
        private DevExpress.XtraGrid.Columns.GridColumn colTruckPlace;
        private DevExpress.XtraGrid.Columns.GridColumn colTruckDate;
        private DevExpress.XtraGrid.Columns.GridColumn colPickUpAtDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliveryDate;
        private DevExpress.XtraGrid.Columns.GridColumn colReturnDate;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraNavBar.NavBarGroup bgBusiness;
        private DevExpress.XtraNavBar.NavBarGroup bgContainer;
        private System.Windows.Forms.BindingSource bsTruckInfo;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox CmbState;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbDriverID;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbTruckID;
        private DevExpress.XtraBars.BarDockControl barDockControl7;
        private DevExpress.XtraBars.BarDockControl barDockControl8;
        private DevExpress.XtraBars.BarDockControl barDockControl6;
        private DevExpress.XtraBars.BarDockControl barDockControl5;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem barNew;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbContainerType;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox cmbCustomer;
    }
}
