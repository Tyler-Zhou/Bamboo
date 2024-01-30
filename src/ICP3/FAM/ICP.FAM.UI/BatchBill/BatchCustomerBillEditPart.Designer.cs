namespace ICP.FAM.UI.BatchBill
{
    partial class BatchCustomerBillEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchCustomerBillEditPart));
            this.panelInfo = new System.Windows.Forms.Panel();
            this.stxtCustomer = new ICP.FAM.UI.Comm.FAMCustomerPopupContainerEdit();
            this.bsBillInfo = new System.Windows.Forms.BindingSource(this.components);
            this.seTrem = new DevExpress.XtraEditors.SpinEdit();
            this.dteDueDate = new DevExpress.XtraEditors.DateEdit();
            this.dteAccountDate = new DevExpress.XtraEditors.DateEdit();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labDueDate = new DevExpress.XtraEditors.LabelControl();
            this.labTrem = new DevExpress.XtraEditors.LabelControl();
            this.labAccountDate = new DevExpress.XtraEditors.LabelControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barNewFee = new DevExpress.XtraBars.BarButtonItem();
            this.barRemove = new DevExpress.XtraBars.BarButtonItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.bsCurrencyRateData = new System.Windows.Forms.BindingSource(this.components);
            this.imageListType = new System.Windows.Forms.ImageList(this.components);
            this.bsChargeList = new System.Windows.Forms.BindingSource(this.components);
            this.feesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBoxFee = new System.Windows.Forms.Panel();
            this.gcChargeList = new DevExpress.XtraGrid.GridControl();
            this.gvChargeList = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colOperationNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnBusinessInfo = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colChargingCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChargingDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.seUnitPrice = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reQuantity = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colCurrencyID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkeCurrency = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rseAmount = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbUnit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colContainerNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsAgent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.checkIsVATInvoiced = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colIsSecondSale = new DevExpress.XtraGrid.Columns.GridColumn();
            this.checkIsSecondSale = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colIsInvoicedVAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsGST = new DevExpress.XtraGrid.Columns.GridColumn();
            this.checkIsGST = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbWay = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rtxtRemark = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.panelChargeTotal = new System.Windows.Forms.Panel();
            this.txtARTotal = new DevExpress.XtraEditors.TextEdit();
            this.labARTotal = new DevExpress.XtraEditors.LabelControl();
            this.txtApTotal = new DevExpress.XtraEditors.TextEdit();
            this.labAPTotal = new DevExpress.XtraEditors.LabelControl();
            this.txtChargeTotal = new DevExpress.XtraEditors.TextEdit();
            this.labChargeTotal = new DevExpress.XtraEditors.LabelControl();
            this.errorProviderBillInfo = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.errorProviderFee = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.rbtnConrainerNo = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBillInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTrem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDueDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDueDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteAccountDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteAccountDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCurrencyRateData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsChargeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.feesBindingSource)).BeginInit();
            this.groupBoxFee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcChargeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChargeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnBusinessInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seUnitPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkeCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rseAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsVATInvoiced)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsSecondSale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsGST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtRemark)).BeginInit();
            this.panelChargeTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtARTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtApTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChargeTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderBillInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnConrainerNo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.stxtCustomer);
            this.panelInfo.Controls.Add(this.seTrem);
            this.panelInfo.Controls.Add(this.dteDueDate);
            this.panelInfo.Controls.Add(this.dteAccountDate);
            this.panelInfo.Controls.Add(this.labCustomer);
            this.panelInfo.Controls.Add(this.labDueDate);
            this.panelInfo.Controls.Add(this.labTrem);
            this.panelInfo.Controls.Add(this.labAccountDate);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfo.Location = new System.Drawing.Point(0, 26);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(1193, 41);
            this.panelInfo.TabIndex = 1;
            // 
            // stxtCustomer
            // 
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsBillInfo, "CustomerID", true));
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsBillInfo, "CustomerName", true));
            this.stxtCustomer.Location = new System.Drawing.Point(71, 6);
            this.stxtCustomer.Name = "stxtCustomer";
            this.stxtCustomer.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtCustomer.Properties.ActionButtonIndex = 1;
            this.stxtCustomer.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.stxtCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.stxtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtCustomer.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtCustomer.Properties.PopupSizeable = false;
            this.stxtCustomer.Properties.ShowPopupCloseButton = false;
            this.stxtCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtCustomer.Size = new System.Drawing.Size(240, 21);
            this.stxtCustomer.TabIndex = 0;
            // 
            // bsBillInfo
            // 
            this.bsBillInfo.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.BillInfo);
            // 
            // seTrem
            // 
            this.seTrem.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.errorProviderFee.SetIconAlignment(this.seTrem, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.errorProviderBillInfo.SetIconAlignment(this.seTrem, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.seTrem.Location = new System.Drawing.Point(647, 7);
            this.seTrem.Name = "seTrem";
            this.seTrem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seTrem.Properties.IsFloatValue = false;
            this.seTrem.Properties.Mask.EditMask = "N00";
            this.seTrem.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.seTrem.Properties.ReadOnly = true;
            this.seTrem.Size = new System.Drawing.Size(102, 21);
            this.seTrem.TabIndex = 2;
            this.seTrem.TabStop = false;
            this.seTrem.EditValueChanged += new System.EventHandler(this.seTrem_EditValueChanged);
            // 
            // dteDueDate
            // 
            this.dteDueDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBillInfo, "DueDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteDueDate.EditValue = null;
            this.errorProviderBillInfo.SetIconAlignment(this.dteDueDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.errorProviderFee.SetIconAlignment(this.dteDueDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteDueDate.Location = new System.Drawing.Point(853, 7);
            this.dteDueDate.Name = "dteDueDate";
            this.dteDueDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDueDate.Properties.ReadOnly = true;
            this.dteDueDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteDueDate.Size = new System.Drawing.Size(102, 21);
            this.dteDueDate.TabIndex = 3;
            this.dteDueDate.TabStop = false;
            // 
            // dteAccountDate
            // 
            this.dteAccountDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsBillInfo, "AccountDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteAccountDate.EditValue = null;
            this.errorProviderBillInfo.SetIconAlignment(this.dteAccountDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.errorProviderFee.SetIconAlignment(this.dteAccountDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteAccountDate.Location = new System.Drawing.Point(401, 6);
            this.dteAccountDate.Name = "dteAccountDate";
            this.dteAccountDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteAccountDate.Properties.ReadOnly = true;
            this.dteAccountDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteAccountDate.Size = new System.Drawing.Size(102, 21);
            this.dteAccountDate.TabIndex = 1;
            this.dteAccountDate.TabStop = false;
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(7, 8);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 0;
            this.labCustomer.Text = "Customer";
            // 
            // labDueDate
            // 
            this.labDueDate.Location = new System.Drawing.Point(778, 10);
            this.labDueDate.Name = "labDueDate";
            this.labDueDate.Size = new System.Drawing.Size(48, 14);
            this.labDueDate.TabIndex = 0;
            this.labDueDate.Text = "DueDate";
            // 
            // labTrem
            // 
            this.labTrem.Location = new System.Drawing.Point(572, 10);
            this.labTrem.Name = "labTrem";
            this.labTrem.Size = new System.Drawing.Size(29, 14);
            this.labTrem.TabIndex = 0;
            this.labTrem.Text = "Trem";
            // 
            // labAccountDate
            // 
            this.labAccountDate.Location = new System.Drawing.Point(326, 9);
            this.labAccountDate.Name = "labAccountDate";
            this.labAccountDate.Size = new System.Drawing.Size(72, 14);
            this.labAccountDate.TabIndex = 0;
            this.labAccountDate.Text = "AccountDate";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave,
            this.barNewFee,
            this.barRemove,
            this.barStaticItem1,
            this.barPrint,
            this.barClose});
            this.barManager1.MaxItemId = 17;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = global::ICP.FAM.UI.Properties.Resources.Save_Blue_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "Print";
            this.barPrint.Glyph = global::ICP.FAM.UI.Properties.Resources.Print_16;
            this.barPrint.Id = 15;
            this.barPrint.Name = "barPrint";
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "Close";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 16;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 3";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.FloatLocation = new System.Drawing.Point(62, 383);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNewFee, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRemove, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Custom 3";
            // 
            // barNewFee
            // 
            this.barNewFee.Caption = "&NewFee";
            this.barNewFee.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_16;
            this.barNewFee.Id = 1;
            this.barNewFee.Name = "barNewFee";
            this.barNewFee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNewFee_ItemClick);
            // 
            // barRemove
            // 
            this.barRemove.Caption = "&Remove";
            this.barRemove.Glyph = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.barRemove.Id = 2;
            this.barRemove.Name = "barRemove";
            this.barRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRemove_ItemClick);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(1193, 27);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1193, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 521);
            this.barDockControlBottom.Size = new System.Drawing.Size(1193, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 495);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1193, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 495);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Appearance.BackColor = System.Drawing.Color.LawnGreen;
            this.barStaticItem1.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.barStaticItem1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.barStaticItem1.Appearance.Options.UseBackColor = true;
            this.barStaticItem1.Appearance.Options.UseBorderColor = true;
            this.barStaticItem1.Appearance.Options.UseForeColor = true;
            this.barStaticItem1.Caption = "The dispatched D/C fees are read only. Clicks [Revise Fee] if you want to modify " +
    "any one of them.";
            this.barStaticItem1.Id = 9;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bsCurrencyRateData
            // 
            this.bsCurrencyRateData.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.CurrencyRateData);
            // 
            // imageListType
            // 
            this.imageListType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListType.ImageStream")));
            this.imageListType.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListType.Images.SetKeyName(0, "-.png");
            this.imageListType.Images.SetKeyName(1, "+.png");
            this.imageListType.Images.SetKeyName(2, "-.png");
            this.imageListType.Images.SetKeyName(3, "+-.png");
            // 
            // bsChargeList
            // 
            this.bsChargeList.DataSource = this.feesBindingSource;
            this.bsChargeList.PositionChanged += new System.EventHandler(this.bsChargeList_PositionChanged);
            // 
            // feesBindingSource
            // 
            this.feesBindingSource.DataMember = "Fees";
            this.feesBindingSource.DataSource = this.bsBillInfo;
            // 
            // groupBoxFee
            // 
            this.groupBoxFee.Controls.Add(this.gcChargeList);
            this.groupBoxFee.Controls.Add(this.panelChargeTotal);
            this.groupBoxFee.Controls.Add(this.standaloneBarDockControl1);
            this.groupBoxFee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFee.Location = new System.Drawing.Point(0, 67);
            this.groupBoxFee.Name = "groupBoxFee";
            this.groupBoxFee.Size = new System.Drawing.Size(1193, 454);
            this.groupBoxFee.TabIndex = 6;
            // 
            // gcChargeList
            // 
            this.gcChargeList.DataSource = this.bsChargeList;
            this.gcChargeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcChargeList.Location = new System.Drawing.Point(0, 27);
            this.gcChargeList.MainView = this.gvChargeList;
            this.gcChargeList.MenuManager = this.barManager1;
            this.gcChargeList.Name = "gcChargeList";
            this.gcChargeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbWay,
            this.reQuantity,
            this.seUnitPrice,
            this.rtxtRemark,
            this.cmbUnit,
            this.rseAmount,
            this.checkIsVATInvoiced,
            this.lkeCurrency,
            this.checkIsSecondSale,
            this.checkIsGST,
            this.rbtnBusinessInfo,
            this.rbtnConrainerNo});
            this.gcChargeList.Size = new System.Drawing.Size(1193, 400);
            this.gcChargeList.TabIndex = 0;
            this.gcChargeList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvChargeList});
            // 
            // gvChargeList
            // 
            this.gvChargeList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOperationNo,
            this.colChargingCode,
            this.colChargingDescription,
            this.colUnitPrice,
            this.colQuantity,
            this.colCurrencyID,
            this.colAmount,
            this.colRemark,
            this.colUnitID,
            this.colContainerNo,
            this.colIsAgent,
            this.colIsSecondSale,
            this.colIsInvoicedVAT,
            this.colIsGST,
            this.colCreateByName,
            this.colCreateDate});
            this.gvChargeList.GridControl = this.gcChargeList;
            this.gvChargeList.IndicatorWidth = 28;
            this.gvChargeList.Name = "gvChargeList";
            this.gvChargeList.OptionsSelection.MultiSelect = true;
            this.gvChargeList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvChargeList.OptionsView.ShowGroupPanel = false;
            this.gvChargeList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvChargeList_RowCellClick);
            this.gvChargeList.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvChargeList_CustomDrawRowIndicator);
            this.gvChargeList.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.gvChargeList_FocusedColumnChanged);
            this.gvChargeList.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvChargeList_CellValueChanged);
            this.gvChargeList.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvChargeList_CellValueChanging);
            this.gvChargeList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvChargeList_KeyDown);
            // 
            // colOperationNo
            // 
            this.colOperationNo.Caption = "Operation No";
            this.colOperationNo.ColumnEdit = this.rbtnBusinessInfo;
            this.colOperationNo.FieldName = "OperationNo";
            this.colOperationNo.Name = "colOperationNo";
            this.colOperationNo.Visible = true;
            this.colOperationNo.VisibleIndex = 0;
            this.colOperationNo.Width = 120;
            // 
            // rbtnBusinessInfo
            // 
            this.rbtnBusinessInfo.AutoHeight = false;
            this.rbtnBusinessInfo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rbtnBusinessInfo.Name = "rbtnBusinessInfo";
            // 
            // colChargingCode
            // 
            this.colChargingCode.Caption = "Charging";
            this.colChargingCode.FieldName = "FeeCode";
            this.colChargingCode.Name = "colChargingCode";
            this.colChargingCode.Visible = true;
            this.colChargingCode.VisibleIndex = 1;
            this.colChargingCode.Width = 67;
            // 
            // colChargingDescription
            // 
            this.colChargingDescription.Caption = "Charging Description";
            this.colChargingDescription.FieldName = "ChargingDescription";
            this.colChargingDescription.Name = "colChargingDescription";
            this.colChargingDescription.Visible = true;
            this.colChargingDescription.VisibleIndex = 2;
            this.colChargingDescription.Width = 118;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.Caption = "UnitPrice";
            this.colUnitPrice.ColumnEdit = this.seUnitPrice;
            this.colUnitPrice.DisplayFormat.FormatString = "n";
            this.colUnitPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colUnitPrice.FieldName = "UnitPrice";
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.Visible = true;
            this.colUnitPrice.VisibleIndex = 3;
            this.colUnitPrice.Width = 79;
            // 
            // seUnitPrice
            // 
            this.seUnitPrice.AutoHeight = false;
            this.seUnitPrice.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seUnitPrice.Mask.EditMask = "F2";
            this.seUnitPrice.Name = "seUnitPrice";
            // 
            // colQuantity
            // 
            this.colQuantity.Caption = "Quantity";
            this.colQuantity.ColumnEdit = this.reQuantity;
            this.colQuantity.DisplayFormat.FormatString = "0.0000";
            this.colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 4;
            this.colQuantity.Width = 79;
            // 
            // reQuantity
            // 
            this.reQuantity.AutoHeight = false;
            this.reQuantity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.reQuantity.Mask.EditMask = "F4";
            this.reQuantity.Name = "reQuantity";
            // 
            // colCurrencyID
            // 
            this.colCurrencyID.Caption = "Currency";
            this.colCurrencyID.ColumnEdit = this.lkeCurrency;
            this.colCurrencyID.FieldName = "CurrencyID";
            this.colCurrencyID.Name = "colCurrencyID";
            this.colCurrencyID.Visible = true;
            this.colCurrencyID.VisibleIndex = 5;
            this.colCurrencyID.Width = 105;
            // 
            // lkeCurrency
            // 
            this.lkeCurrency.AutoHeight = false;
            this.lkeCurrency.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkeCurrency.Name = "lkeCurrency";
            // 
            // colAmount
            // 
            this.colAmount.Caption = "Amount";
            this.colAmount.ColumnEdit = this.rseAmount;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.OptionsColumn.TabStop = false;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 6;
            this.colAmount.Width = 49;
            // 
            // rseAmount
            // 
            this.rseAmount.AutoHeight = false;
            this.rseAmount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rseAmount.Mask.EditMask = "F2";
            this.rseAmount.Name = "rseAmount";
            // 
            // colRemark
            // 
            this.colRemark.Caption = "Remark";
            this.colRemark.CustomizationCaption = "Remark";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 7;
            this.colRemark.Width = 47;
            // 
            // colUnitID
            // 
            this.colUnitID.Caption = "Unit";
            this.colUnitID.ColumnEdit = this.cmbUnit;
            this.colUnitID.FieldName = "UnitID";
            this.colUnitID.Name = "colUnitID";
            this.colUnitID.Visible = true;
            this.colUnitID.VisibleIndex = 8;
            this.colUnitID.Width = 34;
            // 
            // cmbUnit
            // 
            this.cmbUnit.AutoHeight = false;
            this.cmbUnit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUnit.Name = "cmbUnit";
            // 
            // colContainerNo
            // 
            this.colContainerNo.Caption = "Container No";
            this.colContainerNo.ColumnEdit = this.rbtnConrainerNo;
            this.colContainerNo.FieldName = "ContainerNo";
            this.colContainerNo.Name = "colContainerNo";
            this.colContainerNo.Visible = true;
            this.colContainerNo.VisibleIndex = 9;
            this.colContainerNo.Width = 107;
            // 
            // colIsAgent
            // 
            this.colIsAgent.Caption = "IsAgent";
            this.colIsAgent.ColumnEdit = this.checkIsVATInvoiced;
            this.colIsAgent.FieldName = "IsAgent";
            this.colIsAgent.Name = "colIsAgent";
            this.colIsAgent.Visible = true;
            this.colIsAgent.VisibleIndex = 10;
            this.colIsAgent.Width = 33;
            // 
            // checkIsVATInvoiced
            // 
            this.checkIsVATInvoiced.AutoHeight = false;
            this.checkIsVATInvoiced.Name = "checkIsVATInvoiced";
            // 
            // colIsSecondSale
            // 
            this.colIsSecondSale.Caption = "IsSecondSale";
            this.colIsSecondSale.ColumnEdit = this.checkIsSecondSale;
            this.colIsSecondSale.FieldName = "IsSecondSale";
            this.colIsSecondSale.Name = "colIsSecondSale";
            this.colIsSecondSale.Visible = true;
            this.colIsSecondSale.VisibleIndex = 11;
            this.colIsSecondSale.Width = 38;
            // 
            // checkIsSecondSale
            // 
            this.checkIsSecondSale.AutoHeight = false;
            this.checkIsSecondSale.Name = "checkIsSecondSale";
            // 
            // colIsInvoicedVAT
            // 
            this.colIsInvoicedVAT.Caption = "IsInvoicedVAT";
            this.colIsInvoicedVAT.ColumnEdit = this.checkIsVATInvoiced;
            this.colIsInvoicedVAT.FieldName = "IsVATInvoiced";
            this.colIsInvoicedVAT.Name = "colIsInvoicedVAT";
            this.colIsInvoicedVAT.Visible = true;
            this.colIsInvoicedVAT.VisibleIndex = 12;
            this.colIsInvoicedVAT.Width = 58;
            // 
            // colIsGST
            // 
            this.colIsGST.Caption = "IsGST";
            this.colIsGST.ColumnEdit = this.checkIsGST;
            this.colIsGST.FieldName = "IsGST";
            this.colIsGST.Name = "colIsGST";
            this.colIsGST.Visible = true;
            this.colIsGST.VisibleIndex = 13;
            this.colIsGST.Width = 43;
            // 
            // checkIsGST
            // 
            this.checkIsGST.AutoHeight = false;
            this.checkIsGST.Name = "checkIsGST";
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "CreateBy";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.OptionsColumn.AllowEdit = false;
            this.colCreateByName.OptionsColumn.TabStop = false;
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 14;
            this.colCreateByName.Width = 40;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "CreateDate";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsColumn.AllowEdit = false;
            this.colCreateDate.OptionsColumn.TabStop = false;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 15;
            this.colCreateDate.Width = 96;
            // 
            // cmbWay
            // 
            this.cmbWay.AutoHeight = false;
            this.cmbWay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWay.Name = "cmbWay";
            this.cmbWay.SmallImages = this.imageListType;
            // 
            // rtxtRemark
            // 
            this.rtxtRemark.AutoHeight = false;
            this.rtxtRemark.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rtxtRemark.Name = "rtxtRemark";
            this.rtxtRemark.ShowIcon = false;
            // 
            // panelChargeTotal
            // 
            this.panelChargeTotal.Controls.Add(this.txtARTotal);
            this.panelChargeTotal.Controls.Add(this.labARTotal);
            this.panelChargeTotal.Controls.Add(this.txtApTotal);
            this.panelChargeTotal.Controls.Add(this.labAPTotal);
            this.panelChargeTotal.Controls.Add(this.txtChargeTotal);
            this.panelChargeTotal.Controls.Add(this.labChargeTotal);
            this.panelChargeTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelChargeTotal.Location = new System.Drawing.Point(0, 427);
            this.panelChargeTotal.Name = "panelChargeTotal";
            this.panelChargeTotal.Size = new System.Drawing.Size(1193, 27);
            this.panelChargeTotal.TabIndex = 10;
            // 
            // txtARTotal
            // 
            this.txtARTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtARTotal.Location = new System.Drawing.Point(237, 4);
            this.txtARTotal.Name = "txtARTotal";
            this.txtARTotal.Properties.ReadOnly = true;
            this.txtARTotal.Size = new System.Drawing.Size(280, 21);
            this.txtARTotal.TabIndex = 21;
            // 
            // labARTotal
            // 
            this.labARTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labARTotal.Location = new System.Drawing.Point(203, 7);
            this.labARTotal.Name = "labARTotal";
            this.labARTotal.Size = new System.Drawing.Size(19, 14);
            this.labARTotal.TabIndex = 10;
            this.labARTotal.Text = "AR:";
            // 
            // txtApTotal
            // 
            this.txtApTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApTotal.Location = new System.Drawing.Point(567, 3);
            this.txtApTotal.Name = "txtApTotal";
            this.txtApTotal.Properties.ReadOnly = true;
            this.txtApTotal.Size = new System.Drawing.Size(280, 21);
            this.txtApTotal.TabIndex = 22;
            // 
            // labAPTotal
            // 
            this.labAPTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labAPTotal.Location = new System.Drawing.Point(529, 6);
            this.labAPTotal.Name = "labAPTotal";
            this.labAPTotal.Size = new System.Drawing.Size(19, 14);
            this.labAPTotal.TabIndex = 10;
            this.labAPTotal.Text = "AP:";
            // 
            // txtChargeTotal
            // 
            this.txtChargeTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChargeTotal.Location = new System.Drawing.Point(942, 3);
            this.txtChargeTotal.Name = "txtChargeTotal";
            this.txtChargeTotal.Properties.ReadOnly = true;
            this.txtChargeTotal.Size = new System.Drawing.Size(248, 21);
            this.txtChargeTotal.TabIndex = 23;
            // 
            // labChargeTotal
            // 
            this.labChargeTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labChargeTotal.Appearance.Options.UseTextOptions = true;
            this.labChargeTotal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labChargeTotal.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labChargeTotal.Location = new System.Drawing.Point(862, 6);
            this.labChargeTotal.Name = "labChargeTotal";
            this.labChargeTotal.Size = new System.Drawing.Size(74, 14);
            this.labChargeTotal.TabIndex = 10;
            this.labChargeTotal.Text = "Total:";
            // 
            // errorProviderBillInfo
            // 
            this.errorProviderBillInfo.ContainerControl = this;
            this.errorProviderBillInfo.DataSource = this.bsBillInfo;
            // 
            // errorProviderFee
            // 
            this.errorProviderFee.ContainerControl = this;
            this.errorProviderFee.DataSource = this.bsChargeList;
            // 
            // rbtnConrainerNo
            // 
            this.rbtnConrainerNo.AutoHeight = false;
            this.rbtnConrainerNo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rbtnConrainerNo.Name = "rbtnConrainerNo";
            // 
            // BatchCustomerBillEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.groupBoxFee);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BatchCustomerBillEditPart";
            this.Size = new System.Drawing.Size(1193, 521);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBillInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTrem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDueDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDueDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteAccountDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteAccountDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCurrencyRateData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsChargeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.feesBindingSource)).EndInit();
            this.groupBoxFee.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcChargeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChargeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnBusinessInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seUnitPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkeCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rseAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsVATInvoiced)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsSecondSale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsGST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtRemark)).EndInit();
            this.panelChargeTotal.ResumeLayout(false);
            this.panelChargeTotal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtARTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtApTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChargeTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderBillInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnConrainerNo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsBillInfo;
        private System.Windows.Forms.Panel panelInfo;
        private DevExpress.XtraEditors.SpinEdit seTrem;
        private DevExpress.XtraEditors.DateEdit dteDueDate;
        private DevExpress.XtraEditors.DateEdit dteAccountDate;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.LabelControl labDueDate;
        private DevExpress.XtraEditors.LabelControl labTrem;
        private DevExpress.XtraEditors.LabelControl labAccountDate;
        private System.Windows.Forms.BindingSource bsChargeList;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barNewFee;
        private DevExpress.XtraBars.BarButtonItem barRemove;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private System.Windows.Forms.Panel groupBoxFee;
        private System.Windows.Forms.Panel panelChargeTotal;
        private DevExpress.XtraEditors.TextEdit txtChargeTotal;
        private DevExpress.XtraEditors.LabelControl labChargeTotal;
        private System.Windows.Forms.ImageList imageListType;
        private System.Windows.Forms.BindingSource feesBindingSource;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProviderBillInfo;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProviderFee;
        private System.Windows.Forms.BindingSource bsCurrencyRateData;
        private DevExpress.XtraEditors.TextEdit txtARTotal;
        private DevExpress.XtraEditors.LabelControl labARTotal;
        private DevExpress.XtraEditors.TextEdit txtApTotal;
        private DevExpress.XtraEditors.LabelControl labAPTotal;
        private DevExpress.XtraGrid.GridControl gcChargeList;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvChargeList;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbWay;
        private DevExpress.XtraGrid.Columns.GridColumn colChargingCode;
        private DevExpress.XtraGrid.Columns.GridColumn colChargingDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitPrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit seUnitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit reQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkeCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rseAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitID;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colIsAgent;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit checkIsVATInvoiced;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSecondSale;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit checkIsSecondSale;
        private DevExpress.XtraGrid.Columns.GridColumn colIsInvoicedVAT;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit rtxtRemark;
        public DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colIsGST;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit checkIsGST;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerNo;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnBusinessInfo;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private Comm.FAMCustomerPopupContainerEdit stxtCustomer;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnConrainerNo;
    }
}
