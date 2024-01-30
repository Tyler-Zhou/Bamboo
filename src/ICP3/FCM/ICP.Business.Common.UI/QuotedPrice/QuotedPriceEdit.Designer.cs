namespace ICP.Business.Common.UI.QuotedPrice
{
    partial class QuotedPriceEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuotedPriceEditPart));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.bsQuotedPrice = new System.Windows.Forms.BindingSource(this.components);
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labCommodity = new DevExpress.XtraEditors.LabelControl();
            this.labRates = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.cmbTransportClause = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtRates = new System.Windows.Forms.RichTextBox();
            this.stxtCustomer = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.panelMain = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainerBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.pnlBaseInfo = new DevExpress.XtraEditors.PanelControl();
            this.txtCommodity = new DevExpress.XtraEditors.TextEdit();
            this.cmbPaymentType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbTargetType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labTransportClause = new DevExpress.XtraEditors.LabelControl();
            this.txtFromDate = new DevExpress.XtraEditors.DateEdit();
            this.labToDate = new DevExpress.XtraEditors.LabelControl();
            this.labPaymentType = new DevExpress.XtraEditors.LabelControl();
            this.txtToDate = new DevExpress.XtraEditors.DateEdit();
            this.labTargetType = new DevExpress.XtraEditors.LabelControl();
            this.labFromDate = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainerRates = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.quotedPriceRatesPart = new ICP.Business.Common.UI.QuotedPrice.QuotedPriceRatesPart();
            this.navBarGroupRates = new DevExpress.XtraNavBar.NavBarGroup();
            this.devErrorCheck = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsQuotedPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).BeginInit();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainerBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBaseInfo)).BeginInit();
            this.pnlBaseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTargetType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties)).BeginInit();
            this.navBarGroupControlContainerRates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.devErrorCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave,
            this.barClose});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Save;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Left_D_16;
            this.barClose.Id = 2;
            this.barClose.Name = "barClose";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(923, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 529);
            this.barDockControlBottom.Size = new System.Drawing.Size(923, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 503);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(923, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 503);
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(20, 20);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(15, 14);
            this.labNo.TabIndex = 9;
            this.labNo.Text = "No";
            // 
            // txtNo
            // 
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsQuotedPrice, "No", true));
            this.txtNo.Location = new System.Drawing.Point(100, 19);
            this.txtNo.MenuManager = this.barManager1;
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.txtNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtNo.Properties.ReadOnly = true;
            this.txtNo.Size = new System.Drawing.Size(150, 21);
            this.txtNo.TabIndex = 20;
            // 
            // bsQuotedPrice
            // 
            this.bsQuotedPrice.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.QuotedPriceOrderInfo);
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(271, 47);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 12;
            this.labCustomer.Text = "Customer";
            this.labCustomer.ToolTip = "Shipping Line";
            // 
            // labCommodity
            // 
            this.labCommodity.Location = new System.Drawing.Point(535, 20);
            this.labCommodity.Name = "labCommodity";
            this.labCommodity.Size = new System.Drawing.Size(61, 14);
            this.labCommodity.TabIndex = 17;
            this.labCommodity.Text = "Commodity";
            this.labCommodity.ToolTip = "Commodity";
            // 
            // labRates
            // 
            this.labRates.Location = new System.Drawing.Point(20, 101);
            this.labRates.Name = "labRates";
            this.labRates.Size = new System.Drawing.Size(30, 14);
            this.labRates.TabIndex = 19;
            this.labRates.Text = "Rates";
            this.labRates.ToolTip = "Rates";
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // cmbTransportClause
            // 
            this.cmbTransportClause.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsQuotedPrice, "TransportClauseID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbTransportClause, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.devErrorCheck.SetIconAlignment(this.cmbTransportClause, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbTransportClause.Location = new System.Drawing.Point(366, 19);
            this.cmbTransportClause.Name = "cmbTransportClause";
            this.cmbTransportClause.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbTransportClause.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTransportClause.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTransportClause.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbTransportClause.Size = new System.Drawing.Size(150, 21);
            this.cmbTransportClause.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbTransportClause.TabIndex = 0;
            // 
            // txtRates
            // 
            this.txtRates.Location = new System.Drawing.Point(100, 100);
            this.txtRates.Name = "txtRates";
            this.txtRates.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtRates.Size = new System.Drawing.Size(663, 118);
            this.txtRates.TabIndex = 8;
            this.txtRates.Text = "";
            // 
            // stxtCustomer
            // 
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsQuotedPrice, "CustomerName", true));
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsQuotedPrice, "CustomerID", true));
            this.stxtCustomer.Location = new System.Drawing.Point(366, 46);
            this.stxtCustomer.Name = "stxtCustomer";
            this.stxtCustomer.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtCustomer.Properties.ActionButtonIndex = 1;
            this.stxtCustomer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.stxtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtCustomer.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtCustomer.Properties.PopupSizeable = false;
            this.stxtCustomer.Properties.ShowPopupCloseButton = false;
            this.stxtCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtCustomer.Size = new System.Drawing.Size(399, 21);
            this.stxtCustomer.TabIndex = 1;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.navBarControl1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 26);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(923, 503);
            this.panelMain.TabIndex = 0;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroupBase;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainerBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainerRates);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroupBase,
            this.navBarGroupRates});
            this.navBarControl1.Location = new System.Drawing.Point(2, 2);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 823;
            this.navBarControl1.Size = new System.Drawing.Size(854, 499);
            this.navBarControl1.SkinExplorerBarViewScrollStyle = DevExpress.XtraNavBar.SkinExplorerBarViewScrollStyle.ScrollBar;
            this.navBarControl1.TabIndex = 22;
            this.navBarControl1.Text = "navBarControlMain";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Caption = "Base Info";
            this.navBarGroupBase.ControlContainer = this.navBarGroupControlContainerBase;
            this.navBarGroupBase.Expanded = true;
            this.navBarGroupBase.GroupClientHeight = 233;
            this.navBarGroupBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroupBase.Name = "navBarGroupBase";
            // 
            // navBarGroupControlContainerBase
            // 
            this.navBarGroupControlContainerBase.Controls.Add(this.pnlBaseInfo);
            this.navBarGroupControlContainerBase.Name = "navBarGroupControlContainerBase";
            this.navBarGroupControlContainerBase.Size = new System.Drawing.Size(833, 231);
            this.navBarGroupControlContainerBase.TabIndex = 0;
            // 
            // pnlBaseInfo
            // 
            this.pnlBaseInfo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBaseInfo.Controls.Add(this.txtCommodity);
            this.pnlBaseInfo.Controls.Add(this.txtNo);
            this.pnlBaseInfo.Controls.Add(this.cmbPaymentType);
            this.pnlBaseInfo.Controls.Add(this.cmbTargetType);
            this.pnlBaseInfo.Controls.Add(this.cmbTransportClause);
            this.pnlBaseInfo.Controls.Add(this.txtRates);
            this.pnlBaseInfo.Controls.Add(this.stxtCustomer);
            this.pnlBaseInfo.Controls.Add(this.labRates);
            this.pnlBaseInfo.Controls.Add(this.labTransportClause);
            this.pnlBaseInfo.Controls.Add(this.txtFromDate);
            this.pnlBaseInfo.Controls.Add(this.labNo);
            this.pnlBaseInfo.Controls.Add(this.labCustomer);
            this.pnlBaseInfo.Controls.Add(this.labToDate);
            this.pnlBaseInfo.Controls.Add(this.labPaymentType);
            this.pnlBaseInfo.Controls.Add(this.txtToDate);
            this.pnlBaseInfo.Controls.Add(this.labTargetType);
            this.pnlBaseInfo.Controls.Add(this.labFromDate);
            this.pnlBaseInfo.Controls.Add(this.labCommodity);
            this.pnlBaseInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBaseInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlBaseInfo.Name = "pnlBaseInfo";
            this.pnlBaseInfo.Size = new System.Drawing.Size(833, 231);
            this.pnlBaseInfo.TabIndex = 22;
            // 
            // txtCommodity
            // 
            this.txtCommodity.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsQuotedPrice, "Commodity", true));
            this.txtCommodity.Location = new System.Drawing.Point(615, 19);
            this.txtCommodity.Name = "txtCommodity";
            this.txtCommodity.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtCommodity.Properties.Appearance.Options.UseBackColor = true;
            this.txtCommodity.Size = new System.Drawing.Size(150, 21);
            this.txtCommodity.TabIndex = 20;
            // 
            // cmbPaymentType
            // 
            this.cmbPaymentType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsQuotedPrice, "PaymentType", true));
            this.cmbPaymentType.Location = new System.Drawing.Point(100, 73);
            this.cmbPaymentType.Name = "cmbPaymentType";
            this.cmbPaymentType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbPaymentType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbPaymentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaymentType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbPaymentType.Size = new System.Drawing.Size(150, 21);
            this.cmbPaymentType.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbPaymentType.TabIndex = 0;
            // 
            // cmbTargetType
            // 
            this.cmbTargetType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsQuotedPrice, "TargetType", true));
            this.cmbTargetType.Location = new System.Drawing.Point(100, 46);
            this.cmbTargetType.Name = "cmbTargetType";
            this.cmbTargetType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbTargetType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTargetType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTargetType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbTargetType.Size = new System.Drawing.Size(150, 21);
            this.cmbTargetType.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbTargetType.TabIndex = 0;
            // 
            // labTransportClause
            // 
            this.labTransportClause.Location = new System.Drawing.Point(271, 20);
            this.labTransportClause.Name = "labTransportClause";
            this.labTransportClause.Size = new System.Drawing.Size(87, 14);
            this.labTransportClause.TabIndex = 11;
            this.labTransportClause.Text = "TransportClause";
            // 
            // txtFromDate
            // 
            this.txtFromDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsQuotedPrice, "FromDate", true));
            this.txtFromDate.EditValue = null;
            this.txtFromDate.Location = new System.Drawing.Point(366, 73);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtFromDate.Properties.Appearance.Options.UseBackColor = true;
            this.txtFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFromDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtFromDate.Size = new System.Drawing.Size(150, 21);
            this.txtFromDate.TabIndex = 21;
            // 
            // labToDate
            // 
            this.labToDate.Location = new System.Drawing.Point(535, 76);
            this.labToDate.Name = "labToDate";
            this.labToDate.Size = new System.Drawing.Size(45, 14);
            this.labToDate.TabIndex = 17;
            this.labToDate.Text = "To Date";
            this.labToDate.ToolTip = "To Date";
            // 
            // labPaymentType
            // 
            this.labPaymentType.Location = new System.Drawing.Point(20, 78);
            this.labPaymentType.Name = "labPaymentType";
            this.labPaymentType.Size = new System.Drawing.Size(80, 14);
            this.labPaymentType.TabIndex = 13;
            this.labPaymentType.Text = "Payment Type";
            // 
            // txtToDate
            // 
            this.txtToDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsQuotedPrice, "ToDate", true));
            this.txtToDate.EditValue = null;
            this.txtToDate.Location = new System.Drawing.Point(615, 73);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtToDate.Properties.Appearance.Options.UseBackColor = true;
            this.txtToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtToDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtToDate.Size = new System.Drawing.Size(150, 21);
            this.txtToDate.TabIndex = 21;
            // 
            // labTargetType
            // 
            this.labTargetType.Location = new System.Drawing.Point(20, 51);
            this.labTargetType.Name = "labTargetType";
            this.labTargetType.Size = new System.Drawing.Size(28, 14);
            this.labTargetType.TabIndex = 13;
            this.labTargetType.Text = "Type";
            // 
            // labFromDate
            // 
            this.labFromDate.Location = new System.Drawing.Point(271, 76);
            this.labFromDate.Name = "labFromDate";
            this.labFromDate.Size = new System.Drawing.Size(57, 14);
            this.labFromDate.TabIndex = 13;
            this.labFromDate.Text = "From Date";
            // 
            // navBarGroupControlContainerRates
            // 
            this.navBarGroupControlContainerRates.Controls.Add(this.quotedPriceRatesPart);
            this.navBarGroupControlContainerRates.Name = "navBarGroupControlContainerRates";
            this.navBarGroupControlContainerRates.Size = new System.Drawing.Size(833, 298);
            this.navBarGroupControlContainerRates.TabIndex = 1;
            // 
            // quotedPriceRatesPart
            // 
            this.quotedPriceRatesPart.BaseMultiLanguageList = null;
            this.quotedPriceRatesPart.BasePartList = null;
            this.quotedPriceRatesPart.CodeValuePairs = null;
            this.quotedPriceRatesPart.ControlsList = null;
            this.quotedPriceRatesPart.DataSource = ((object)(resources.GetObject("quotedPriceRatesPart.DataSource")));
            this.quotedPriceRatesPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quotedPriceRatesPart.EditMode = ICP.Framework.CommonLibrary.Common.EditMode.New;
            this.quotedPriceRatesPart.FormName = "QuotedPriceCommunicationPart";
            this.quotedPriceRatesPart.IsMultiLanguage = true;
            this.quotedPriceRatesPart.IsView = false;
            this.quotedPriceRatesPart.Location = new System.Drawing.Point(0, 0);
            this.quotedPriceRatesPart.Name = "quotedPriceRatesPart";
            this.quotedPriceRatesPart.Resources = null;
            this.quotedPriceRatesPart.RootWorkItem = null;
            this.quotedPriceRatesPart.Size = new System.Drawing.Size(833, 298);
            this.quotedPriceRatesPart.SyncLocalData = false;
            this.quotedPriceRatesPart.TabIndex = 0;
            this.quotedPriceRatesPart.Title = "";
            this.quotedPriceRatesPart.UsedMessages = null;
            // 
            // navBarGroupRates
            // 
            this.navBarGroupRates.Caption = "Rates Info";
            this.navBarGroupRates.ControlContainer = this.navBarGroupControlContainerRates;
            this.navBarGroupRates.Expanded = true;
            this.navBarGroupRates.GroupClientHeight = 300;
            this.navBarGroupRates.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroupRates.Name = "navBarGroupRates";
            // 
            // devErrorCheck
            // 
            this.devErrorCheck.ContainerControl = this;
            // 
            // QuotedPriceEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "QuotedPriceEditPart";
            this.Size = new System.Drawing.Size(923, 529);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsQuotedPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).EndInit();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainerBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBaseInfo)).EndInit();
            this.pnlBaseInfo.ResumeLayout(false);
            this.pnlBaseInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTargetType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties)).EndInit();
            this.navBarGroupControlContainerRates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.devErrorCheck)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.LabelControl labNo;
        private System.Windows.Forms.BindingSource bsQuotedPrice;
        private DevExpress.XtraEditors.LabelControl labRates;
        private DevExpress.XtraEditors.LabelControl labCommodity;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private System.Windows.Forms.RichTextBox txtRates;
        private Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtCustomer;
        private DevExpress.XtraEditors.PanelControl panelMain;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTransportClause;
        private DevExpress.XtraEditors.LabelControl labTransportClause;
        private DevExpress.XtraEditors.LabelControl labFromDate;
        private DevExpress.XtraEditors.DateEdit txtToDate;
        private DevExpress.XtraEditors.LabelControl labToDate;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider devErrorCheck;
        private DevExpress.XtraEditors.DateEdit txtFromDate;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupBase;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupRates;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainerBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainerRates;
        private QuotedPriceRatesPart quotedPriceRatesPart;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraEditors.PanelControl pnlBaseInfo;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTargetType;
        private DevExpress.XtraEditors.LabelControl labTargetType;
        private DevExpress.XtraEditors.TextEdit txtCommodity;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbPaymentType;
        private DevExpress.XtraEditors.LabelControl labPaymentType;
    }
}
